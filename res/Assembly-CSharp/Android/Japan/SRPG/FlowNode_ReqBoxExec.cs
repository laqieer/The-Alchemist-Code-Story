﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBoxExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Gacha/Box/Exec", 32741)]
  [FlowNode.Pin(0, "IN 単発交換リクエスト", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "IN N回交換リクエスト", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "OT Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "OT BOXが存在しない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "OT コスト不足", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "OT BOXが空", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "OT 創世編開催期間外", FlowNode.PinTypes.Output, 203)]
  public class FlowNode_ReqBoxExec : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_IN_REQUEST_MULTI = 1;
    private const int PIN_OT_SUCCESS = 100;
    private const int PIN_OT_NOT_EXIST_BOX = 200;
    private const int PIN_OT_COST_SHORT = 201;
    private const int PIN_OT_REMAIN_SHORT = 202;
    private const int PIN_OT_OUT_OF_PERIOD = 203;

    public override void OnActivate(int pinID)
    {
      string box_iname = string.Empty;
      int lottery_num = 0;
      switch (pinID)
      {
        case 0:
          box_iname = BoxGachaManager.CurrentBoxGachaStatus.Iname;
          lottery_num = 1;
          break;
        case 1:
          box_iname = BoxGachaManager.CurrentBoxGachaStatus.Iname;
          lottery_num = BoxGachaManager.CurrentBoxGachaStatus.MultiCount;
          break;
      }
      if (string.IsNullOrEmpty(box_iname) || lottery_num <= 0)
        return;
      this.ExecRequest((WebAPI) new ReqBoxExec(box_iname, lottery_num, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.BoxGacha_NotExistBox:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(200);
            this.enabled = false;
            break;
          case Network.EErrCode.BoxGacha_CostShort:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(201);
            this.enabled = false;
            break;
          case Network.EErrCode.BoxGacha_RemainShort:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(202);
            this.enabled = false;
            break;
          case Network.EErrCode.Genesis_OutOfPeriod:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(203);
            this.enabled = false;
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqBoxExec.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqBoxExec.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          DebugUtility.LogError("ReqBoxExecのレスポンスのパースに失敗しています.");
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.units);
            if (jsonObject.body.artifacts != null)
              MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.artifacts, true);
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
