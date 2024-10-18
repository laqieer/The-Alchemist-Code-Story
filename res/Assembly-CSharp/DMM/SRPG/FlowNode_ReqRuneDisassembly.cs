// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneDisassembly
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
  [FlowNode.NodeType("Rune/Req/Disassembly", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "装備中含む", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "ゼニーor幻晶石不足", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "お気に入り含む", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_ReqRuneDisassembly : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_INCLUDES_EQUIPPED_RUNES = 110;
    protected const int PIN_OUT_NOT_ENOUGH_CURRENCY = 111;
    protected const int PIN_OUT_IS_FAVORITE = 112;
    private static List<BindRuneData> mTargetRunes;
    private static ReqRuneDisassembly.Response mKeepResponse;

    public static List<BindRuneData> GetTargetRunes() => FlowNode_ReqRuneDisassembly.mTargetRunes;

    public static ReqRuneDisassembly.Response GetResponse()
    {
      return FlowNode_ReqRuneDisassembly.mKeepResponse;
    }

    public static void Clear()
    {
      FlowNode_ReqRuneDisassembly.mTargetRunes = (List<BindRuneData>) null;
    }

    public static void SetTarget(List<BindRuneData> runes)
    {
      if (runes == null)
        return;
      FlowNode_ReqRuneDisassembly.mTargetRunes = new List<BindRuneData>();
      foreach (BindRuneData rune in runes)
      {
        BindRuneData copyRune = rune.CreateCopyRune();
        FlowNode_ReqRuneDisassembly.mTargetRunes.Add(copyRune);
      }
    }

    public ReqRuneDisassembly.RequestParam CreateReqRuneDisassembly()
    {
      if (FlowNode_ReqRuneDisassembly.mTargetRunes == null)
      {
        DebugUtility.LogError("Failed CreateReqRuneDisassembly.");
        return (ReqRuneDisassembly.RequestParam) null;
      }
      ReqRuneDisassembly.RequestParam reqRuneDisassembly = new ReqRuneDisassembly.RequestParam();
      List<long> longList = new List<long>();
      foreach (BindRuneData mTargetRune in FlowNode_ReqRuneDisassembly.mTargetRunes)
        longList.Add(mTargetRune.iid);
      reqRuneDisassembly.rune_ids = longList.ToArray();
      return reqRuneDisassembly;
    }

    public override void OnActivate(int pinID)
    {
      ReqRuneDisassembly.RequestParam reqRuneDisassembly = this.CreateReqRuneDisassembly();
      if (reqRuneDisassembly == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqRuneDisassembly(reqRuneDisassembly, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneDisassembly.Response response = (ReqRuneDisassembly.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneDisassembly.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneDisassembly.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
        switch (errCode)
        {
          case SRPG.Network.EErrCode.Rune_NotEnoughCurrency:
            this.ActivateOutputLinks(111);
            ((Behaviour) this).enabled = false;
            SRPG.Network.ResetError();
            SRPG.Network.RemoveAPI();
            break;
          case SRPG.Network.EErrCode.Rune_NotHaveRune:
            this.OnFailed();
            break;
          case SRPG.Network.EErrCode.Rune_IncludesEquippedRunes:
            this.ActivateOutputLinks(110);
            ((Behaviour) this).enabled = false;
            SRPG.Network.ResetError();
            SRPG.Network.RemoveAPI();
            break;
          default:
            if (errCode == SRPG.Network.EErrCode.Rune_IsFavorite)
            {
              this.ActivateOutputLinks(112);
              ((Behaviour) this).enabled = false;
              SRPG.Network.ResetError();
              SRPG.Network.RemoveAPI();
              break;
            }
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqRuneDisassembly.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneDisassembly.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(response.items);
            MonoSingleton<GameManager>.Instance.Deserialize(response.player);
            List<long> longList = new List<long>();
            foreach (BindRuneData mTargetRune in FlowNode_ReqRuneDisassembly.mTargetRunes)
              longList.Add(mTargetRune.iid);
            MonoSingleton<GameManager>.Instance.Player.RemoveRunes(longList.ToArray());
            MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(response.rune_storage_used);
            FlowNode_ReqRuneDisassembly.mKeepResponse = response;
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
      public ReqRuneDisassembly.Response body;
    }
  }
}
