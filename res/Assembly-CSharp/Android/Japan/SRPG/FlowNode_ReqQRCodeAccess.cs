// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
  public class FlowNode_ReqQRCodeAccess : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      int qrCampaignId = FlowNode_OnUrlSchemeLaunch.QRCampaignID;
      string qrSerialId = FlowNode_OnUrlSchemeLaunch.QRSerialID;
      if (qrCampaignId != -1 && !string.IsNullOrEmpty(qrSerialId))
      {
        this.enabled = true;
        this.ExecRequest((WebAPI) new ReqQRCodeAccess(qrCampaignId, qrSerialId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.Finished((string) null);
    }

    private void Finished(string msg = null)
    {
      FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
      FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
      FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
      this.enabled = false;
      if (string.IsNullOrEmpty(msg))
        return;
      UIUtility.SystemMessage((string) null, msg, (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
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
      public string message = string.Empty;
      public Json_Item[] items;
    }
  }
}
