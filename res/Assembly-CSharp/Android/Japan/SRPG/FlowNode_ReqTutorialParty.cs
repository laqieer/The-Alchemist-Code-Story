// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTutorialParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

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
        long uniqueid = unitDataList[index] == null ? 0L : unitDataList[index].UniqueID;
        partyOfType.SetUnitUniqueID(index, uniqueid);
      }
      this.ExecRequest((WebAPI) new ReqParty(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), false, false, false));
      this.enabled = true;
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
        for (int index1 = 0; index1 < 12; ++index1)
        {
          int num = index1;
          if (index1 != 9)
          {
            PartyData party = new PartyData((PlayerPartyTypes) num);
            party.Deserialize(json);
            PartyWindow2.EditPartyTypes editPartyType = ((PlayerPartyTypes) num).ToEditPartyType();
            List<PartyEditData> teams = new List<PartyEditData>();
            int maxTeamCount = editPartyType.GetMaxTeamCount();
            for (int index2 = 0; index2 < maxTeamCount; ++index2)
            {
              PartyEditData partyEditData = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(index2), party);
              teams.Add(partyEditData);
            }
            PartyUtility.SaveTeamPresets(editPartyType, 0, teams, false);
          }
        }
      }
      Network.RemoveAPI();
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }
  }
}
