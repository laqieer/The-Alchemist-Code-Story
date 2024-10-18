// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentPrepare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Payment/Prepare", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  public class FlowNode_PaymentPrepare : FlowNode_Network
  {
    public bool mErrorSuccess = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.SelectedProductIname == null)
      {
        if (this.mErrorSuccess)
          this.Success();
        else
          this.Failure();
      }
      else
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqProductChargePrepare(GlobalVars.SelectedProductIname, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      DebugUtility.Log("PaymentPrepare success");
      FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/prepare", nameof (Success));
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("PaymentPrepare failure");
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(200);
    }

    public override void OnSuccess(WWWResult www)
    {
      string errMsg = SRPG.Network.ErrMsg;
      if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        WebAPI.JSON_BaseResponse jsonBaseResponse = SerializerCompressorHelper.Decode<WebAPI.JSON_BaseResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(jsonBaseResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) jsonBaseResponse.stat;
        string statMsg = jsonBaseResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
      }
      if (SRPG.Network.IsError)
      {
        UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
        FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/prepare", "NetworkError:" + SRPG.Network.ErrCode.ToString());
        this.Failure();
      }
      else
        this.Success();
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
    }
  }
}
