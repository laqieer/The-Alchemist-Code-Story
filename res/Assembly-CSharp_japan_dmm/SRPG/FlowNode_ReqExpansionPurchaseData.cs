// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqExpansionPurchaseData
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
  [FlowNode.NodeType("ExpansionPurchase/ReqExpansionPurchaseData", 32741)]
  [FlowNode.Pin(10, "Request", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqExpansionPurchaseData : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 10;
    private const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqExpansionPurchase(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
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
        ReqExpansionPurchase.Response body;
        if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
        {
          FlowNode_ReqExpansionPurchaseData.MP_ReqExpansionPurchaseResponse purchaseResponse = SerializerCompressorHelper.Decode<FlowNode_ReqExpansionPurchaseData.MP_ReqExpansionPurchaseResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(purchaseResponse != null, "mpRes == null");
          body = purchaseResponse.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<ReqExpansionPurchase.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqExpansionPurchase.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.expansions);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_ReqExpansionPurchaseResponse : WebAPI.JSON_BaseResponse
    {
      public ReqExpansionPurchase.Response body;
    }
  }
}
