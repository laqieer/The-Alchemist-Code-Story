// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqOverWriteParty
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
  [FlowNode.NodeType("System/Party/ReqOverWriteParty", 32741)]
  [FlowNode.Pin(1, "取得開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "取得完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqOverWriteParty : FlowNode_Network
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_END = 101;
    [SerializeField]
    [BitMask]
    private FlowNode_ReqOverWriteParty.ExecConditions mExecConditions;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.CheckConditions())
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqOverWriteParty(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
      else
        this.ActivateOutputLinks(101);
    }

    private bool CheckConditions()
    {
      return (this.mExecConditions & FlowNode_ReqOverWriteParty.ExecConditions.ResumeBattle) != (FlowNode_ReqOverWriteParty.ExecConditions) 0 && (long) GlobalVars.BtlID != 0L;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        ReqOverWriteParty.Response body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<ReqOverWriteParty.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqOverWriteParty.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        else
        {
          FlowNode_ReqOverWriteParty.MP_OverWritePartyResponse writePartyResponse = SerializerCompressorHelper.Decode<FlowNode_ReqOverWriteParty.MP_OverWritePartyResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(writePartyResponse != null, "mpRes == null");
          body = writePartyResponse.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          if (body != null)
            MonoSingleton<GameManager>.Instance.Player.Deserialize(body.party_decks);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(101);
      }
    }

    [Flags]
    public enum ExecConditions
    {
      ResumeBattle = 1,
    }

    [MessagePackObject(true)]
    public class MP_OverWritePartyResponse : WebAPI.JSON_BaseResponse
    {
      public ReqOverWriteParty.Response body;
    }
  }
}
