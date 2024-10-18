// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFirstChargeState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/ReqFirstChargeState", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqFirstChargeState : FlowNode_Network
  {
    private const int INPUT_REQUEST = 0;
    private const int OUTPUT_SUCCESS = 10;
    private const int OUTPUT_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqFirstChargeState(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    private void _Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(0);
    }

    private void _Failed()
    {
      Network.RemoveAPI();
      Network.ResetError();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(11);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this._Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeState.JSON_FirstChargeState> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeState.JSON_FirstChargeState>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = jsonObject.body == null ? 0 : jsonObject.body.charge_bonus;
        this._Success();
      }
    }

    public class JSON_FirstChargeState
    {
      public int charge_bonus;
    }
  }
}
