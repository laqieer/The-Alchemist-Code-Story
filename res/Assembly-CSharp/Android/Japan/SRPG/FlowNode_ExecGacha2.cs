﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExecGacha2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace SRPG
{
  [FlowNode.NodeType("System/Gacha/ExecGacha2", 32741)]
  [FlowNode.Pin(10, "Single Gacha", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "Multi Gacha", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "Free Gacha", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "Comp Gacha", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(4, "Success", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "Failed", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(6, "ゼニー不足", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "幻晶石不足", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(8, "有償幻晶石不足", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(9, "有償召喚リセット待ち", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(11, "Success(引き直し召喚確定)", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Error(引き直し召喚の期間外実行)", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_ExecGacha2 : FlowNode_Network
  {
    private List<string> DownloadUnits = new List<string>();
    private List<string> DownloadArtifacts = new List<string>();
    private List<string> DownloadConceptCards = new List<string>();
    private const int PIN_OT_SUCCESS_DECISION_REDRAW = 11;
    private const int PIN_OT_ERROR_OUT_OF_PERIOD = 12;
    private GachaTypes mCurrentGachaType;
    private bool mUseOneMore;
    private List<AssetList.Item> mQueue;

    private FlowNode_ExecGacha2.ExecType API { get; set; }

    public void OnExecGacha(GachaRequestParam _rparam)
    {
      if (_rparam == null)
        this.Failure();
      this.mUseOneMore = _rparam.IsUseOneMore;
      this.API = FlowNode_ExecGacha2.ExecType.DEFAULT;
      this.Request(_rparam);
    }

    public void OnExecGachaDecision(GachaRequestParam _rparam)
    {
      if (_rparam == null)
        this.Failure();
      this.API = FlowNode_ExecGacha2.ExecType.DECISION;
      this.ExecGacha(_rparam.Iname, !_rparam.IsFree ? 0 : 1, !_rparam.IsTicketGacha ? 0 : _rparam.Num, 1);
    }

    public void Request(GachaRequestParam _param)
    {
      if (!_param.IsRedrawConfirm)
      {
        if (_param.IsPaid && MonoSingleton<GameManager>.Instance.Player.PaidCoin < _param.Cost)
        {
          this.ActivateOutputLinks(8);
          this.enabled = false;
          return;
        }
        if (!_param.IsTicketGacha && !_param.IsFree)
        {
          if (_param.CostType == GachaCostType.GOLD)
          {
            if (MonoSingleton<GameManager>.Instance.Player.Gold < _param.Cost)
            {
              this.ActivateOutputLinks(6);
              this.enabled = false;
              return;
            }
          }
          else if (_param.CostType == GachaCostType.COIN && MonoSingleton<GameManager>.Instance.Player.Coin < _param.Cost)
          {
            this.ActivateOutputLinks(7);
            this.enabled = false;
            return;
          }
        }
        this.mCurrentGachaType = !_param.IsGold ? GachaTypes.Rare : GachaTypes.Normal;
      }
      this.ExecGacha(_param.Iname, !_param.IsFree ? 0 : 1, !_param.IsTicketGacha ? 0 : _param.Num, 0);
    }

    private void ExecGacha(string iname, int is_free = 0, int num = 0, int is_decision = 0)
    {
      this.ExecRequest((WebAPI) new ReqGachaExec(iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), is_free, num, is_decision));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      if (this.API == FlowNode_ExecGacha2.ExecType.DEFAULT)
      {
        this.ActivateOutputLinks(4);
      }
      else
      {
        if (this.API != FlowNode_ExecGacha2.ExecType.DECISION)
          return;
        this.ActivateOutputLinks(11);
      }
    }

    private void Failure()
    {
      this.enabled = false;
      this.mUseOneMore = false;
      this.ActivateOutputLinks(5);
    }

    private void PaidGachaLimitOver()
    {
      this.enabled = false;
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(9);
    }

    private void OutofPeriod()
    {
      if (this.API == FlowNode_ExecGacha2.ExecType.DECISION)
      {
        this.OnFailed();
      }
      else
      {
        this.enabled = false;
        Network.RemoveAPI();
        Network.ResetError();
        this.ActivateOutputLinks(12);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.NoGacha:
            this.OnFailed();
            break;
          case Network.EErrCode.GachaCostShort:
            this.OnBack();
            break;
          case Network.EErrCode.GachaItemMax:
            this.OnBack();
            break;
          case Network.EErrCode.GachaPaidLimitOver:
            this.PaidGachaLimitOver();
            break;
          default:
            if (errCode == Network.EErrCode.GachaOutofPeriod)
            {
              this.OutofPeriod();
              break;
            }
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ItemData itemDataByItemId1 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID("IT_US_SUMMONS_01");
        int num1 = itemDataByItemId1 != null ? itemDataByItemId1.Num : 0;
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
        ItemData itemDataByItemId2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID("IT_US_SUMMONS_01");
        int num2 = itemDataByItemId2 != null ? itemDataByItemId2.Num : 0;
        List<int> a_summonCoins = new List<int>();
        a_summonCoins.Add(num1);
        a_summonCoins.Add(num2);
        List<GachaDropData> gachaDropDataList = new List<GachaDropData>();
        if (jsonObject.body.add != null && jsonObject.body.add.Length > 0)
        {
          foreach (Json_DropInfo json in jsonObject.body.add)
          {
            GachaDropData gachaDropData = new GachaDropData();
            if (gachaDropData.Deserialize(json))
              gachaDropDataList.Add(gachaDropData);
          }
        }
        List<GachaDropData> a_dropMails = new List<GachaDropData>();
        if (jsonObject.body.add_mail != null)
        {
          foreach (Json_DropInfo json in jsonObject.body.add_mail)
          {
            GachaDropData gachaDropData = new GachaDropData();
            if (gachaDropData.Deserialize(json))
              a_dropMails.Add(gachaDropData);
          }
        }
        for (int index = 0; index < gachaDropDataList.Count; ++index)
        {
          if (gachaDropDataList[index].type == GachaDropData.Type.ConceptCard)
          {
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
            break;
          }
        }
        GachaReceiptData a_receipt = new GachaReceiptData();
        a_receipt.Deserialize(jsonObject.body.receipt);
        GachaResultData.Init(gachaDropDataList, a_dropMails, a_summonCoins, a_receipt, this.mUseOneMore, jsonObject.body.is_pending, jsonObject.body.rest);
        if (!GachaResultData.IsRedrawGacha || GachaResultData.IsRedrawGacha && this.API == FlowNode_ExecGacha2.ExecType.DECISION)
        {
          MyMetaps.TrackSpend(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a_receipt.type), a_receipt.iname, a_receipt.val);
          MonoSingleton<GameManager>.Instance.Player.OnGacha(this.mCurrentGachaType, gachaDropDataList.Count);
        }
        if (this.API == FlowNode_ExecGacha2.ExecType.DECISION)
        {
          FlowNode_Variable.Set("REDRAW_GACHA_PENDING", string.Empty);
          GachaResultData.Reset();
          this.Success();
        }
        else
          this.StartCoroutine(this.AsyncGachaResultData(gachaDropDataList));
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncGachaResultData(List<GachaDropData> drops)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ExecGacha2.\u003CAsyncGachaResultData\u003Ec__Iterator0() { drops = drops, \u0024this = this };
    }

    private enum ExecType : byte
    {
      NONE,
      DEFAULT,
      DECISION,
    }
  }
}
