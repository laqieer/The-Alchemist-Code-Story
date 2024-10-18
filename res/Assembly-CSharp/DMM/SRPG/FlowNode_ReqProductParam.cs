// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Master/ReqProductParam", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqProductParam : FlowNode_Network
  {
    public bool IsLoginBefore = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqProductParam(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Object.op_Equality((Object) this, (Object) null))
        Network.RemoveAPI();
      else if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ProductParamResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ProductParamResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ProductParamResponse productParamResponse = new ProductParamResponse();
        if (!productParamResponse.Deserialize(jsonObject.body))
          this.Failure();
        else
          this.StartCoroutine(this.CheckPaymentInit(productParamResponse));
      }
    }

    [DebuggerHidden]
    private IEnumerator CheckPaymentInit(ProductParamResponse param)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqProductParam.\u003CCheckPaymentInit\u003Ec__Iterator0()
      {
        param = param,
        \u0024this = this
      };
    }
  }
}
