// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBoxReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Gacha/Box/Reset", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "BOXが存在しない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "リセット条件未達成", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "創世編開催期間外", FlowNode.PinTypes.Output, 202)]
  public class FlowNode_ReqBoxReset : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_SUCCESS = 100;
    private const int PIN_OT_NOT_EXIST_BOX = 200;
    private const int PIN_OT_NOT_RESET_BOX = 201;
    private const int PIN_OT_OUT_OF_PERIOD = 202;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqBoxReset(BoxGachaManager.CurrentBoxGachaStatus.Iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.BoxGacha_NotExistBox:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(200);
            this.enabled = false;
            break;
          case Network.EErrCode.BoxGacha_NotResetBox:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(201);
            this.enabled = false;
            break;
          default:
            if (errCode == Network.EErrCode.Genesis_OutOfPeriod)
            {
              Network.RemoveAPI();
              Network.ResetError();
              this.ActivateOutputLinks(202);
              this.enabled = false;
              break;
            }
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqBoxReset.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqBoxReset.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          DebugUtility.LogError("ReqBoxResetのレスポンスのパースに失敗しています.");
          this.OnRetry();
        }
        else
        {
          try
          {
            if (BoxGachaManager.CurrentBoxGachaStatus == null)
              BoxGachaManager.CurrentBoxGachaStatus = new BoxGachaManager.BoxGachaStatus();
            BoxGachaManager.CurrentBoxGachaStatus.Deserialize(jsonObject.body);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
