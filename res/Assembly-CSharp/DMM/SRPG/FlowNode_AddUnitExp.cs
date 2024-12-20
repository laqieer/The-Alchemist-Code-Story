﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AddUnitExp
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
  [FlowNode.NodeType("System/Unit/AddUnitExp", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AddUnitExp : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        Dictionary<string, int> usedUnitExpItems = GlobalVars.UsedUnitExpItems;
        if (usedUnitExpItems.Count < 1)
        {
          this.Success();
        }
        else
        {
          int num = 0;
          foreach (KeyValuePair<string, int> keyValuePair in usedUnitExpItems)
            num += keyValuePair.Value;
          if (num < 1)
          {
            this.Success();
          }
          else
          {
            this.ExecRequest((WebAPI) new ReqUnitExpAdd((long) GlobalVars.SelectedUnitUniqueID, usedUnitExpItems, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            ((Behaviour) this).enabled = true;
            GlobalVars.UsedUnitExpItems.Clear();
          }
        }
      }
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
        if (Network.ErrCode == Network.EErrCode.ExpMaterialShort)
          this.OnFailed();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          int old_point = 0;
          UnitData unitDataByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          if (unitDataByUniqueId1 != null)
          {
            if (unitDataByUniqueId1.IsRental)
              old_point = unitDataByUniqueId1.RentalFavoritePoint;
          }
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          UnitData unitDataByUniqueId2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
          if (unitDataByUniqueId2 != null && unitDataByUniqueId2.IsRental)
            UnitRentalParam.SendNotificationIsNeed(unitDataByUniqueId2.UnitID, old_point, unitDataByUniqueId2.RentalFavoritePoint);
          this.Success();
        }
      }
    }
  }
}
