﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqProductParam", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqProductParam : FlowNode_Network
  {
    public bool IsLoginBefore = true;
    private bool isConnecting;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.isConnecting)
        return;
      this.isConnecting = true;
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
      this.isConnecting = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.isConnecting = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
      {
        Network.RemoveAPI();
        this.isConnecting = false;
      }
      else if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
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
      return (IEnumerator) new FlowNode_ReqProductParam.\u003CCheckPaymentInit\u003Ec__IteratorD7() { param = param, \u003C\u0024\u003Eparam = param, \u003C\u003Ef__this = this };
    }
  }
}
