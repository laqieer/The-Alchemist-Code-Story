// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBoxDrawStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Gacha/Box/DrawStatus", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "BOXが存在しない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "開催期間外", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "キーイベントが閉じられている", FlowNode.PinTypes.Output, 202)]
  public class FlowNode_ReqBoxDrawStatus : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_SUCCESS = 100;
    private const int PIN_OT_NOT_EXIST_BOX = 200;
    private const int PIN_OT_OUT_OF_PERIOD = 201;
    private const int PIN_OT_KEY_CLOSE = 202;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqBoxDrawStatus(BoxGachaManager.CurrentBoxGachaStatus.Iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Advance_KeyClose:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(202);
            ((Behaviour) this).enabled = false;
            break;
          case Network.EErrCode.BoxGacha_NotExistBox:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(200);
            ((Behaviour) this).enabled = false;
            break;
          case Network.EErrCode.Genesis_OutOfPeriod:
          case Network.EErrCode.Advance_NotOpen:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(201);
            ((Behaviour) this).enabled = false;
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqBoxDrawStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqBoxDrawStatus.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          DebugUtility.LogError("ReqBoxDrawStatusのレスポンスのパースに失敗しています.");
          this.OnRetry();
        }
        else
        {
          try
          {
            BoxGachaLineupListWindow.Instance?.DeserializeLineupList(new JSON_BoxGachaSteps()
            {
              step = jsonObject.body.total_step,
              total_num = jsonObject.body.total_num,
              box_items = jsonObject.body.box_items,
              total_num_feature = jsonObject.body.total_num_feature,
              total_num_normal = jsonObject.body.total_num_normal,
              remain_num_feature = jsonObject.body.remain_num_feature,
              remain_num_normal = jsonObject.body.remain_num_normal
            });
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
