// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneResetStatusEvo
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
  [FlowNode.NodeType("Rune/Req/ResetStatusEvo", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "素材不足", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "指定した枠に覚醒効果がない", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_ReqRuneResetStatusEvo : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_ENOUGH_MATERIAL = 110;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 111;
    protected const int PIN_OUT_NOT_FOUND_EVO_SLOT = 112;
    private static BindRuneData mTargetRune;
    private static int mCostIndex;
    private static int mSelectedEvoSlot;

    public static BindRuneData GetTargetRune() => FlowNode_ReqRuneResetStatusEvo.mTargetRune;

    public static void Clear()
    {
      FlowNode_ReqRuneResetStatusEvo.mTargetRune = (BindRuneData) null;
      FlowNode_ReqRuneResetStatusEvo.mCostIndex = 0;
    }

    public static void SetTarget(BindRuneData rune_data, int cost_index, int evo_index)
    {
      if (rune_data == null)
        return;
      RuneData rune = rune_data.Rune;
      if (rune == null)
        return;
      FlowNode_ReqRuneResetStatusEvo.mTargetRune = rune_data;
      FlowNode_ReqRuneResetStatusEvo.mCostIndex = cost_index;
      FlowNode_ReqRuneResetStatusEvo.mSelectedEvoSlot = rune.GetEvoSlot(evo_index);
    }

    public ReqRuneResetStatusEvo.RequestParam CreateReqRuneResetStatusEvo()
    {
      if (FlowNode_ReqRuneResetStatusEvo.mTargetRune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneResetStatusEvo.");
        return (ReqRuneResetStatusEvo.RequestParam) null;
      }
      RuneData rune = FlowNode_ReqRuneResetStatusEvo.mTargetRune.Rune;
      if (rune != null)
        return new ReqRuneResetStatusEvo.RequestParam((long) rune.UniqueID, FlowNode_ReqRuneResetStatusEvo.mCostIndex, FlowNode_ReqRuneResetStatusEvo.mSelectedEvoSlot);
      DebugUtility.LogError("Failed CreateReqRuneResetStatusEvo RuneData is null.");
      return (ReqRuneResetStatusEvo.RequestParam) null;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneResetStatusEvo.RequestParam runeResetStatusEvo = this.CreateReqRuneResetStatusEvo();
      if (runeResetStatusEvo == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneResetStatusEvo(runeResetStatusEvo, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneResetStatusEvo.Response response = (ReqRuneResetStatusEvo.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneResetStatusEvo.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneResetStatusEvo.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
            this.ActivateOutputLinks(110);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotEnoughCurrency:
            this.ActivateOutputLinks(111);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotHaveRune:
            this.OnFailed();
            break;
          case SRPG.Network.EErrCode.Rune_NotFoundEvoSlot:
            this.ActivateOutputLinks(112);
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
          WebAPI.JSON_BodyResponse<ReqRuneResetStatusEvo.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneResetStatusEvo.Response>>(www.text);
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
      public ReqRuneResetStatusEvo.Response body;
    }
  }
}
