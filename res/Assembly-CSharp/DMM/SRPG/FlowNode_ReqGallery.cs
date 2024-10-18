// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGallery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqGallery", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "InProgress", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1000, "ハイライト期間外", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "ハイライトデータ作成中", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "ハイライト表示可能", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1010, "ハイライト未受け取り", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1011, "ハイライト受け取り済み", FlowNode.PinTypes.Output, 1011)]
  public class FlowNode_ReqGallery : FlowNode_Network
  {
    public const int PIN_IN_REQUEST = 0;
    public const int PIN_OUT_SUCCESS = 100;
    public const int PIN_OUT_INPROGRESS = 101;
    public const int PIN_OUT_HIGHLIGHT_OUTOFPERIOD = 1000;
    public const int PIN_OUT_HIGHLIGHT_INPROGRESS = 1001;
    public const int PIN_OUT_HIGHLIGHT_COMPLETED = 1002;
    public const int PIN_OUT_HIGHLIGHT_NOT_REWARDED = 1010;
    public const int PIN_OUT_HIGHLIGHT_REWARDED = 1011;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        ((Behaviour) this).enabled = true;
        this.ExecRequest((WebAPI) new ReqGallery(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Gallery_MigrationInProgress)
        {
          Network.ResetError();
          Network.RemoveAPI();
          this.ActivateOutputLinks(101);
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGallery.Json_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGallery.Json_Response>>(www.text);
        if (jsonObject.body == null)
        {
          this.ActivateOutputLinks(1000);
          this.ActivateOutputLinks(1011);
        }
        else
        {
          switch (jsonObject.body.highlight_status)
          {
            case 0:
              this.ActivateOutputLinks(1000);
              break;
            case 1:
              this.ActivateOutputLinks(1001);
              break;
            case 2:
              this.ActivateOutputLinks(1002);
              break;
          }
          switch (jsonObject.body.is_highlight_rewarded)
          {
            case 0:
              this.ActivateOutputLinks(1010);
              break;
            case 1:
              this.ActivateOutputLinks(1011);
              break;
          }
        }
        Network.RemoveAPI();
        this.Success();
      }
    }

    [Serializable]
    private class Json_Response
    {
      public string highlight_iname;
      public int highlight_status;
      public int is_highlight_rewarded;
    }
  }
}
