// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTutorialParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tutorial/StartParty")]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqTutorialParty : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PlayerPartyTypes type = PlayerPartyTypes.Normal;
      PartyData partyOfType = player.FindPartyOfType(type);
      List<UnitData> units = player.Units;
      List<UnitData> unitDataList = new List<UnitData>();
      for (int mainmemberStart = partyOfType.MAINMEMBER_START; mainmemberStart < partyOfType.MAX_MAINMEMBER && mainmemberStart <= units.Count; ++mainmemberStart)
      {
        if (!units[mainmemberStart].UnitParam.IsHero())
          unitDataList.Add(units[mainmemberStart]);
      }
      for (int count = unitDataList.Count; count < partyOfType.MAX_UNIT; ++count)
        unitDataList.Add((UnitData) null);
      for (int index = 0; index < unitDataList.Count; ++index)
      {
        long uniqueId = unitDataList[index] == null ? 0L : unitDataList[index].UniqueID;
        partyOfType.SetUnitUniqueID(index, uniqueId);
      }
      this.ExecRequest((WebAPI) new ReqParty(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), ignoreEmpty: false));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoUnitParty:
          case Network.EErrCode.IllegalParty:
            this.OnFailed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      try
      {
        if (jsonObject.body == null)
          throw new InvalidJSONException();
        instance.Deserialize(jsonObject.body.player);
        instance.Deserialize(jsonObject.body.parties);
      }
      catch (Exception ex)
      {
        FlowNode_Network.Retry();
        return;
      }
      Json_Party[] parties = jsonObject.body.parties;
      if (parties != null && parties.Length > 0)
      {
        Json_Party json = parties[0];
        for (int index1 = 0; index1 < 17; ++index1)
        {
          int type = index1;
          if (index1 != 9 && index1 != 14)
          {
            PartyData party = new PartyData((PlayerPartyTypes) type);
            party.Deserialize(json);
            PartyWindow2.EditPartyTypes editPartyType = ((PlayerPartyTypes) type).ToEditPartyType();
            List<PartyEditData> teams = new List<PartyEditData>();
            int maxTeamCount = editPartyType.GetMaxTeamCount();
            for (int index2 = 0; index2 < maxTeamCount; ++index2)
            {
              PartyEditData partyEditData = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(index2), party);
              teams.Add(partyEditData);
            }
            PartyUtility.SaveTeamPresets(editPartyType, 0, teams);
          }
        }
      }
      Network.RemoveAPI();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }
  }
}
