// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerFloorReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Request/Tower/Floor/Reset", 32741)]
  [FlowNode.Pin(1, "敵の引継ぎ情報リセット", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "リセット完了", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTowerFloorReset : FlowNode_Network
  {
    private const int INPUT_REQUEST_RESET = 1;
    private const int OUTPUT_REQUEST_RESET = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse == null)
        return;
      TowerFloorParam currentFloor = towerResuponse.GetCurrentFloor();
      if (currentFloor == null)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqTowerFloorReset(GlobalVars.SelectedTowerID, currentFloor.iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<ReqTowerFloorReset.Json_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorReset.Json_Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      try
      {
        MonoSingleton<GameManager>.Instance.Player.SetTowerFloorResetCoin(jsonObject.body);
        MonoSingleton<GameManager>.Instance.TowerResuponse.OnFloorReset();
        this.ActivateOutputLinks(100);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      ((Behaviour) this).enabled = false;
    }
  }
}
