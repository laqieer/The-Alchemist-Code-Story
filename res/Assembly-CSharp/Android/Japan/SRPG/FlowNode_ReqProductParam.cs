// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;

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
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
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
      return (IEnumerator) new FlowNode_ReqProductParam.\u003CCheckPaymentInit\u003Ec__Iterator0() { param = param, \u0024this = this };
    }
  }
}
