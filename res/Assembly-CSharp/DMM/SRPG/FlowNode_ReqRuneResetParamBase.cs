// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneResetParamBase
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
  [FlowNode.NodeType("Rune/Req/ResetParamBase", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "素材不足", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 111)]
  public class FlowNode_ReqRuneResetParamBase : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_ENOUGH_MATERIAL = 110;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 111;
    private static BindRuneData mTargetRune;
    private static BindRuneData mBackupRune;
    private static int mCostIndex;

    public static BindRuneData GetTargetRune() => FlowNode_ReqRuneResetParamBase.mTargetRune;

    public static BindRuneData GetBackupRune() => FlowNode_ReqRuneResetParamBase.mBackupRune;

    public static void Clear()
    {
      FlowNode_ReqRuneResetParamBase.mTargetRune = (BindRuneData) null;
      FlowNode_ReqRuneResetParamBase.mCostIndex = 0;
    }

    public static void SetTarget(BindRuneData rune_data, int cost_index)
    {
      if (rune_data == null)
        return;
      FlowNode_ReqRuneResetParamBase.mTargetRune = rune_data;
      FlowNode_ReqRuneResetParamBase.mCostIndex = cost_index;
    }

    public ReqRuneResetParamBase.RequestParam CreateReqRuneResetParamBase()
    {
      if (FlowNode_ReqRuneResetParamBase.mTargetRune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneResetParamBase.");
        return (ReqRuneResetParamBase.RequestParam) null;
      }
      RuneData rune = FlowNode_ReqRuneResetParamBase.mTargetRune.Rune;
      if (rune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneResetParamBase RuneData is null.");
        return (ReqRuneResetParamBase.RequestParam) null;
      }
      ReqRuneResetParamBase.RequestParam runeResetParamBase = new ReqRuneResetParamBase.RequestParam((long) rune.UniqueID, FlowNode_ReqRuneResetParamBase.mCostIndex);
      FlowNode_ReqRuneResetParamBase.mBackupRune = FlowNode_ReqRuneResetParamBase.mTargetRune.CreateCopyRune();
      return runeResetParamBase;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneResetParamBase.RequestParam runeResetParamBase = this.CreateReqRuneResetParamBase();
      if (runeResetParamBase == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneResetParamBase(runeResetParamBase, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneResetParamBase.Response response = (ReqRuneResetParamBase.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneResetParamBase.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneResetParamBase.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqRuneResetParamBase.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneResetParamBase.Response>>(www.text);
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
      public ReqRuneResetParamBase.Response body;
    }
  }
}
