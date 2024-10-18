// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestUnitPieceShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/RequestUnitPieceShopItems", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_RequestUnitPieceShopItems : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OUT_SUCCESS = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqUnitPieceShopItemList(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqUnitPieceShopItemList.Response json = (ReqUnitPieceShopItemList.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_RequestUnitPieceShopItems.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_RequestUnitPieceShopItems.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        json = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqUnitPieceShopItemList.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqUnitPieceShopItemList.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          json = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (!MonoSingleton<GameManager>.Instance.Player.UnitPieceShopData.Deserialize(json))
          this.OnFailed();
        else
          this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqUnitPieceShopItemList.Response body;
    }
  }
}
