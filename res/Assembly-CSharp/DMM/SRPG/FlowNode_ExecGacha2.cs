﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExecGacha2
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
  [FlowNode.Pin(13, "Success(簡易演出使用)", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "NoGachaPickup", FlowNode.PinTypes.Output, 14)]
  public class FlowNode_ExecGacha2 : FlowNode_Network
  {
    private const int PIN_OT_SUCCESS_DECISION_REDRAW = 11;
    private const int PIN_OT_ERROR_OUT_OF_PERIOD = 12;
    private const int PIN_OT_SUCCESS_SIMPLE_ANIM = 13;
    private const int PIN_OT_NO_GACHA_PICKUP = 14;
    private GachaTypes mCurrentGachaType;
    private bool mUseOneMore;
    private bool mSimpleAnim;
    private List<string> DownloadUnits = new List<string>();
    private List<string> DownloadArtifacts = new List<string>();
    private List<string> DownloadConceptCards = new List<string>();
    private List<AssetList.Item> mQueue;

    private FlowNode_ExecGacha2.ExecType API { get; set; }

    public void OnExecGacha(GachaRequestParam _rparam)
    {
      if (_rparam == null)
        this.Failure();
      this.mUseOneMore = _rparam.IsUseOneMore;
      this.mSimpleAnim = _rparam.IsSimpleAnim;
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
        if (_param.IsPaid && MonoSingleton<GameManager>.Instance.Player.PaidCoin < _param.FixCost)
        {
          this.ActivateOutputLinks(8);
          ((Behaviour) this).enabled = false;
          return;
        }
        if (!_param.IsTicketGacha && !_param.IsFree && !_param.IsDailyFree)
        {
          if (_param.CostType == GachaCostType.GOLD)
          {
            if (MonoSingleton<GameManager>.Instance.Player.Gold < _param.FixCost)
            {
              this.ActivateOutputLinks(6);
              ((Behaviour) this).enabled = false;
              return;
            }
          }
          else if (_param.CostType == GachaCostType.COIN && MonoSingleton<GameManager>.Instance.Player.Coin < _param.FixCost)
          {
            this.ActivateOutputLinks(7);
            ((Behaviour) this).enabled = false;
            return;
          }
        }
        this.mCurrentGachaType = !_param.IsGold ? GachaTypes.Rare : GachaTypes.Normal;
      }
      this.ExecGacha(_param.Iname, !_param.IsFixFree ? 0 : 1, !_param.IsTicketGacha ? 0 : _param.Num);
    }

    private void ExecGacha(string iname, int is_free = 0, int num = 0, int is_decision = 0)
    {
      this.ExecRequest((WebAPI) new ReqGachaExec(iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), is_free, num, is_decision));
      ((Behaviour) this).enabled = true;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      int pinID = 4;
      if (this.API == FlowNode_ExecGacha2.ExecType.DEFAULT)
        pinID = !this.mSimpleAnim ? 4 : 13;
      else if (this.API == FlowNode_ExecGacha2.ExecType.DECISION)
        pinID = 11;
      this.ActivateOutputLinks(pinID);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.mUseOneMore = false;
      this.ActivateOutputLinks(5);
    }

    private void PaidGachaLimitOver()
    {
      ((Behaviour) this).enabled = false;
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
        ((Behaviour) this).enabled = false;
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
            if (errCode != Network.EErrCode.GachaOutofPeriod)
            {
              if (errCode == Network.EErrCode.NoGachaPickup)
              {
                this.ActivateOutputLinks(14);
                ((Behaviour) this).enabled = false;
                Network.RemoveAPI();
                Network.ResetError();
                break;
              }
              this.OnRetry();
              break;
            }
            this.OutofPeriod();
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
        NotifyList.mNotifyEnable = false;
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
        if (jsonObject.body.runes != null && jsonObject.body.runes.Length > 0)
        {
          MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(jsonObject.body.rune_storage_used);
          MonoSingleton<GameManager>.Instance.Player.OnDirtyRuneData();
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
      return (IEnumerator) new FlowNode_ExecGacha2.\u003CAsyncGachaResultData\u003Ec__Iterator0()
      {
        drops = drops,
        \u0024this = this
      };
    }

    private enum ExecType : byte
    {
      NONE,
      DEFAULT,
      DECISION,
    }
  }
}
