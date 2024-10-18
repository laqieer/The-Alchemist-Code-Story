// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Request/QRCodeAccess", 32741)]
  [FlowNode.Pin(0, "CheckQRCodeAccess", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "NotFoundCampaign", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "NotFoundSerial", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "OutOfTerm", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "AlreadyInputed", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "OverUse", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(100, "CheckQRCodeAccessHome", FlowNode.PinTypes.Input, 100)]
  public class FlowNode_ReqQRCodeAccess : FlowNode_Network
  {
    private const int PIN_INPUT_CHECK_QRCODE_ACCESS_HOME = 100;
    private const int PIN_OUTPUT_SUCCESS = 1;
    private bool end_callback;

    public override void OnActivate(int pinID)
    {
      if (Object.op_Inequality((Object) FlowNode_OnUrlSchemeLaunch.Instance, (Object) null))
        FlowNode_OnUrlSchemeLaunch.Instance.UpdatePendingParam();
      if (pinID == 0)
      {
        this.CheckQRCode();
        this.end_callback = false;
      }
      else
      {
        if (pinID != 100)
          return;
        this.CheckQRCode();
        this.end_callback = true;
      }
    }

    private void CheckQRCode()
    {
      int qrCampaignId = FlowNode_OnUrlSchemeLaunch.QRCampaignID;
      string qrSerialId = FlowNode_OnUrlSchemeLaunch.QRSerialID;
      if (qrCampaignId != -1 && !string.IsNullOrEmpty(qrSerialId))
      {
        ((Behaviour) this).enabled = true;
        this.ExecRequest((WebAPI) new ReqQRCodeAccess(qrCampaignId, qrSerialId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
        FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
        FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
      }
      else
        this.Finished();
    }

    private void Finished(string msg = null)
    {
      FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
      FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
      FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
      ((Behaviour) this).enabled = false;
      if (!string.IsNullOrEmpty(msg))
        UIUtility.SystemMessage((string) null, msg, new UIUtility.DialogResultEvent(this.EndCallback));
      else
        this.ActivateOutputLinks(1);
    }

    private void EndCallback(GameObject go)
    {
      if (!this.end_callback)
        return;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QR_OutOfPeriod:
          case Network.EErrCode.QR_InvalidQRSerial:
          case Network.EErrCode.QR_CanNotReward:
          case Network.EErrCode.QR_LockSerialCampaign:
          case Network.EErrCode.QR_AlreadyRewardSkin:
            Network.RemoveAPI();
            Network.ResetError();
            this.Finished(Network.ErrMsg);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqQRCodeAccess.JSON_QRCodeAccess> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqQRCodeAccess.JSON_QRCodeAccess>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body.items != null)
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        this.Finished(jsonObject.body.message);
      }
    }

    private class JSON_QRCodeAccess
    {
      public Json_Item[] items;
      public string message = string.Empty;
    }
  }
}
