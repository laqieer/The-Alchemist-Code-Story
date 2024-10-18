﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTobiraOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Unit/ReqTobira/ReqTobiraOpen")]
  [FlowNode.Pin(0, "扉を解放する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "扉を解放した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTobiraOpen : FlowNode_Network
  {
    public const int INPUT_REQUEST = 0;
    public const int OUTPUT_REQUEST = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqTobiraOpen((long) GlobalVars.SelectedUnitUniqueID, (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory, new Network.ResponseCallback(this.TobiraOpenCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        ((Behaviour) this).enabled = false;
        MonoSingleton<GameManager>.Instance.Player.OnOpenTobiraTrophy((long) GlobalVars.SelectedUnitUniqueID);
      }
    }

    private void TobiraOpenCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }
  }
}
