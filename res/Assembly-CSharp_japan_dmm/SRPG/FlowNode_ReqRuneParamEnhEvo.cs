// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneParamEnhEvo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Rune/Req/RuneParamEnhEvo", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "素材不足", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "指定した枠に覚醒効果がない", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(104, "リクエスト作成失敗", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqRuneParamEnhEvo : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_ENOUGH_MATERIAL = 102;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 103;
    protected const int PIN_OUT_NOT_FOUND_EVO_SLOT = 104;
    protected const int PIN_OUT_NULL_REQUEST = 110;
    private static BindRuneData mTargetRune;
    private static BindRuneData mBackupRune;
    private static int mSelectedEvoSlot;
    private static int mSelectedEvoIndex;
    private static int mCostIndex;
    private static bool mIsResultSuccess;

    public static BindRuneData TargetRune => FlowNode_ReqRuneParamEnhEvo.mTargetRune;

    public static BindRuneData BackupRune => FlowNode_ReqRuneParamEnhEvo.mBackupRune;

    public static bool IsResultSuccess => FlowNode_ReqRuneParamEnhEvo.mIsResultSuccess;

    public static int SelectedEvoSlot => FlowNode_ReqRuneParamEnhEvo.mSelectedEvoSlot;

    public static int SelectedEvoIndex => FlowNode_ReqRuneParamEnhEvo.mSelectedEvoIndex;

    public static void Clear()
    {
      FlowNode_ReqRuneParamEnhEvo.mTargetRune = (BindRuneData) null;
      FlowNode_ReqRuneParamEnhEvo.mBackupRune = (BindRuneData) null;
      FlowNode_ReqRuneParamEnhEvo.mSelectedEvoSlot = 0;
      FlowNode_ReqRuneParamEnhEvo.mSelectedEvoIndex = 0;
      FlowNode_ReqRuneParamEnhEvo.mCostIndex = 0;
      FlowNode_ReqRuneParamEnhEvo.mIsResultSuccess = false;
    }

    public static void SetTarget(BindRuneData rune_data, int cost_index, int evo_index)
    {
      if (rune_data == null)
        return;
      RuneData rune = rune_data.Rune;
      if (rune == null)
        return;
      FlowNode_ReqRuneParamEnhEvo.mTargetRune = rune_data;
      FlowNode_ReqRuneParamEnhEvo.mCostIndex = cost_index;
      FlowNode_ReqRuneParamEnhEvo.mSelectedEvoIndex = evo_index;
      FlowNode_ReqRuneParamEnhEvo.mSelectedEvoSlot = rune.GetEvoSlot(evo_index);
    }

    public ReqRuneParamEnhEvo.RequestParam CreateReqRuneParamEnhEvo()
    {
      if (FlowNode_ReqRuneParamEnhEvo.mTargetRune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneParamEnhEvo.");
        return (ReqRuneParamEnhEvo.RequestParam) null;
      }
      RuneData rune = FlowNode_ReqRuneParamEnhEvo.mTargetRune.Rune;
      if (rune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneParamEnhEvo RuneData is null.");
        return (ReqRuneParamEnhEvo.RequestParam) null;
      }
      ReqRuneParamEnhEvo.RequestParam reqRuneParamEnhEvo = new ReqRuneParamEnhEvo.RequestParam((long) rune.UniqueID, FlowNode_ReqRuneParamEnhEvo.mCostIndex, FlowNode_ReqRuneParamEnhEvo.mSelectedEvoSlot);
      FlowNode_ReqRuneParamEnhEvo.mBackupRune = FlowNode_ReqRuneParamEnhEvo.mTargetRune.CreateCopyRune();
      return reqRuneParamEnhEvo;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneParamEnhEvo.RequestParam reqRuneParamEnhEvo = this.CreateReqRuneParamEnhEvo();
      if (reqRuneParamEnhEvo == null)
      {
        this.ActivateOutputLinks(110);
      }
      else
      {
        if (pinID != 1)
          return;
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqRuneParamEnhEvo(reqRuneParamEnhEvo, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
    }

    public void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneParamEnhEvo.Response response = (ReqRuneParamEnhEvo.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneParamEnhEvo.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneParamEnhEvo.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.Rune_NotEnoughMaterial:
            this.ActivateOutputLinks(102);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotEnoughCurrency:
            this.ActivateOutputLinks(103);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotHaveRune:
            this.OnFailed();
            break;
          case SRPG.Network.EErrCode.Rune_NotFoundEvoSlot:
            this.ActivateOutputLinks(104);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
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
          WebAPI.JSON_BodyResponse<ReqRuneParamEnhEvo.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneParamEnhEvo.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(response.items);
            MonoSingleton<GameManager>.Instance.Deserialize(response.player);
            FlowNode_ReqRuneParamEnhEvo.mIsResultSuccess = response.result == 1;
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

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqRuneParamEnhEvo.Response body;
    }
  }
}
