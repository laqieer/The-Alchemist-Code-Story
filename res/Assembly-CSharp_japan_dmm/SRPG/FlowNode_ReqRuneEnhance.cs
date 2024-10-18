// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Rune/Req/Enhance", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "強化上限", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "素材不足", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 104)]
  public class FlowNode_ReqRuneEnhance : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_IS_UPPER_LIMIT = 102;
    protected const int PIN_OUT_NOT_ENOUGH_MATERIAL = 103;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 104;
    private static BindRuneData mTargetRune;
    private static BindRuneData mBackupRune;
    private static float mBackupGauge;
    private static bool mIsUseEnforceGauge;
    private static bool mIsResultSuccess;

    public static BindRuneData GetTargetRune() => FlowNode_ReqRuneEnhance.mTargetRune;

    public static BindRuneData GetBackupRune() => FlowNode_ReqRuneEnhance.mBackupRune;

    public static float GetBackupGauge() => FlowNode_ReqRuneEnhance.mBackupGauge;

    public static bool IsResultSuccess() => FlowNode_ReqRuneEnhance.mIsResultSuccess;

    public static void Clear()
    {
      FlowNode_ReqRuneEnhance.mTargetRune = (BindRuneData) null;
      FlowNode_ReqRuneEnhance.mBackupRune = (BindRuneData) null;
      FlowNode_ReqRuneEnhance.mBackupGauge = 0.0f;
      FlowNode_ReqRuneEnhance.mIsUseEnforceGauge = false;
      FlowNode_ReqRuneEnhance.mIsResultSuccess = false;
    }

    public static void SetTarget(BindRuneData rune_data, bool is_use_enforce_gauge)
    {
      if (rune_data == null)
        return;
      FlowNode_ReqRuneEnhance.mTargetRune = rune_data;
      FlowNode_ReqRuneEnhance.mIsUseEnforceGauge = false;
      if (!is_use_enforce_gauge || !RuneUtility.IsCanUseGauge(rune_data))
        return;
      FlowNode_ReqRuneEnhance.mIsUseEnforceGauge = is_use_enforce_gauge;
    }

    public ReqRuneEnhance.RequestParam CreateReqRuneEnhance()
    {
      if (FlowNode_ReqRuneEnhance.mTargetRune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneEnhance.");
        return (ReqRuneEnhance.RequestParam) null;
      }
      RuneData rune = FlowNode_ReqRuneEnhance.mTargetRune.Rune;
      if (rune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneEnhance RuneData is null.");
        return (ReqRuneEnhance.RequestParam) null;
      }
      ReqRuneEnhance.RequestParam reqRuneEnhance = new ReqRuneEnhance.RequestParam();
      reqRuneEnhance.rune_id = (long) rune.UniqueID;
      reqRuneEnhance.is_enforce_failure_gauge = !FlowNode_ReqRuneEnhance.mIsUseEnforceGauge ? 0 : 1;
      FlowNode_ReqRuneEnhance.mBackupRune = FlowNode_ReqRuneEnhance.mTargetRune.CreateCopyRune();
      this.SetBackupGauge();
      return reqRuneEnhance;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneEnhance.RequestParam reqRuneEnhance = this.CreateReqRuneEnhance();
      if (reqRuneEnhance == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneEnhance(reqRuneEnhance, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneEnhance.Response response = (ReqRuneEnhance.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneEnhance.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneEnhance.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.Rune_IsUpperLimitForEnforce:
            this.ActivateOutputLinks(102);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotEnoughMaterial:
            this.ActivateOutputLinks(103);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotEnoughCurrency:
            this.ActivateOutputLinks(104);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotHaveRune:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqRuneEnhance.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneEnhance.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          response = jsonObject.body;
        }
        if (response == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(response.runes, false);
            MonoSingleton<GameManager>.Instance.Deserialize(response.rune_enforce_gauge);
            MonoSingleton<GameManager>.Instance.Deserialize(response.items);
            MonoSingleton<GameManager>.Instance.Deserialize(response.player);
            FlowNode_ReqRuneEnhance.mIsResultSuccess = response.result == 1;
            FlowNode_ReqRuneEnhance.mIsUseEnforceGauge = false;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    private void SetBackupGauge()
    {
      if (FlowNode_ReqRuneEnhance.mTargetRune == null || MonoSingleton<GameManager>.Instance.Player.GetRuneEnforceGauge() == null)
      {
        FlowNode_ReqRuneEnhance.mBackupGauge = 0.0f;
      }
      else
      {
        int rarity = FlowNode_ReqRuneEnhance.mTargetRune.Rarity;
        List<RuneEnforceGaugeData> runeEnforceGauge = MonoSingleton<GameManager>.Instance.Player.GetRuneEnforceGauge();
        for (int index = 0; index < runeEnforceGauge.Count; ++index)
        {
          if ((int) runeEnforceGauge[index].rare == rarity)
          {
            FlowNode_ReqRuneEnhance.mBackupGauge = (float) runeEnforceGauge[index].val;
            break;
          }
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqRuneEnhance.Response body;
    }
  }
}
