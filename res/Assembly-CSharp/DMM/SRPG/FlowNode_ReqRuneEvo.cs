// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneEvo
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
  [FlowNode.NodeType("Rune/Req/Evo", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "素材不足", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "覚醒上限", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(113, "強化回数不足", FlowNode.PinTypes.Output, 113)]
  public class FlowNode_ReqRuneEvo : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_ENOUGH_MATERIAL = 110;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 111;
    protected const int PIN_OUT_IS_UPPER_LIMIT_FOR_EVO = 112;
    protected const int PIN_OUT_CAN_NOT_EVO_ENFORCE_SHORT = 113;
    private static BindRuneData mTargetRune;

    public static BindRuneData GetTargetRune() => FlowNode_ReqRuneEvo.mTargetRune;

    public static void Clear() => FlowNode_ReqRuneEvo.mTargetRune = (BindRuneData) null;

    public static void SetTarget(BindRuneData rune_data)
    {
      if (rune_data == null)
        return;
      FlowNode_ReqRuneEvo.mTargetRune = rune_data;
    }

    public ReqRuneEvo.RequestParam CreateReqRuneEvo()
    {
      if (FlowNode_ReqRuneEvo.mTargetRune == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneEvo.");
        return (ReqRuneEvo.RequestParam) null;
      }
      return new ReqRuneEvo.RequestParam()
      {
        rune_id = FlowNode_ReqRuneEvo.mTargetRune.iid
      };
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneEvo.RequestParam reqRuneEvo = this.CreateReqRuneEvo();
      if (reqRuneEvo == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneEvo(reqRuneEvo, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneEvo.Response response = (ReqRuneEvo.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneEvo.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneEvo.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.Rune_IsUpperLimitForEvo:
            this.ActivateOutputLinks(112);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_CanNotEvoEnforceShort:
            this.ActivateOutputLinks(113);
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
          WebAPI.JSON_BodyResponse<ReqRuneEvo.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneEvo.Response>>(www.text);
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
            if (response.items == null)
              throw new Exception("ReqRuneEquip: illegal DrawCard response.units data!");
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
      public ReqRuneEvo.Response body;
    }
  }
}
