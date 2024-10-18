﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExecTutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Gacha/ExecTutorialGacha", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Request(チュートリアル引き直し確定)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Success(引き直し確定)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Pending(保留中の召喚がある)", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_ExecTutorialGacha : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_IN_DECISION = 1;
    private const int PIN_OT_SUCCESS = 10;
    private const int PIN_OT_FAILED = 11;
    private const int PIN_OT_SUCCESS_DECISION_REDRAW = 12;
    private const int PIN_OT_PENDING = 13;
    [SerializeField]
    private string TutorialGachaID = string.Empty;

    private FlowNode_ExecTutorialGacha.ExecType API { get; set; }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (((Behaviour) this).enabled)
            break;
          if (FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
          {
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", string.Empty);
            this.ActivateOutputLinks(13);
            break;
          }
          if (string.IsNullOrEmpty(this.TutorialGachaID))
          {
            DebugUtility.LogError("チュートリアル召喚で使用したいIDが指定されていません");
            break;
          }
          this.API = FlowNode_ExecTutorialGacha.ExecType.DEFAULT;
          this.ExecRequest((WebAPI) new ReqGachaExec(this.TutorialGachaID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), 0, 0, 0));
          ((Behaviour) this).enabled = true;
          break;
        case 1:
          if (((Behaviour) this).enabled || string.IsNullOrEmpty(this.TutorialGachaID))
            break;
          this.API = FlowNode_ExecTutorialGacha.ExecType.DECISION;
          this.ExecRequest((WebAPI) new ReqGachaExec(this.TutorialGachaID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), is_decision: 1));
          ((Behaviour) this).enabled = true;
          break;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.NoGacha)
          this.OnFailed();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        List<GachaDropData> a_drops = new List<GachaDropData>();
        if (jsonObject.body.add != null && jsonObject.body.add.Length > 0)
        {
          foreach (Json_DropInfo json in jsonObject.body.add)
          {
            GachaDropData gachaDropData = new GachaDropData();
            if (gachaDropData.Deserialize(json))
              a_drops.Add(gachaDropData);
          }
        }
        GachaReceiptData a_receipt = new GachaReceiptData();
        a_receipt.Deserialize(jsonObject.body.receipt);
        GachaResultData.Init(a_drops, a_receipt: a_receipt, a_is_pending: jsonObject.body.is_pending, a_redraw_rest: jsonObject.body.rest);
        MyMetaps.TrackSpend(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a_receipt.type), a_receipt.iname, a_receipt.val);
        if (this.API == FlowNode_ExecTutorialGacha.ExecType.DECISION)
        {
          this.Success();
        }
        else
        {
          GachaDropData drop = GachaResultData.drops[0];
          if (drop == null)
          {
            DebugUtility.LogError("召喚結果が存在しません");
            this.Failure();
          }
          else
            this.StartCoroutine(this.AsyncDownload(drop.unit));
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncDownload(UnitParam _uparam)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ExecTutorialGacha.\u003CAsyncDownload\u003Ec__Iterator0()
      {
        _uparam = _uparam,
        \u0024this = this
      };
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      int pinID = 10;
      if (this.API == FlowNode_ExecTutorialGacha.ExecType.DECISION)
        pinID = 12;
      this.ActivateOutputLinks(pinID);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(11);
    }

    private enum ExecType : byte
    {
      NONE,
      DEFAULT,
      DECISION,
    }
  }
}
