﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Unit/SetEquip", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetEquip : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long selectedUnitUniqueId = (long) GlobalVars.SelectedUnitUniqueID;
        int selectedUnitJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
        this.ExecRequest((WebAPI) new ReqJobEquip(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(selectedUnitUniqueId).Jobs[selectedUnitJobIndex].UniqueID, (long) (int) GlobalVars.SelectedEquipmentSlot, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetEquip:
            this.OnFailed();
            break;
          case Network.EErrCode.NoEquipItem:
          case Network.EErrCode.Equipped:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        Network.RemoveAPI();
        long selectedUnitUniqueId = (long) GlobalVars.SelectedUnitUniqueID;
        int selectedUnitJobIndex = (int) GlobalVars.SelectedUnitJobIndex;
        MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(selectedUnitUniqueId).SetJobIndex(selectedUnitJobIndex);
        this.Success();
      }
    }
  }
}
