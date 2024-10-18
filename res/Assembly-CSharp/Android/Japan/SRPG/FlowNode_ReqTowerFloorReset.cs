// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerFloorReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

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
      this.enabled = true;
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
      this.enabled = false;
    }
  }
}
