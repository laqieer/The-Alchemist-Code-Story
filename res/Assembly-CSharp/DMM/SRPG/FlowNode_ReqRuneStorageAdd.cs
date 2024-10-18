// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneStorageAdd
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
  [FlowNode.NodeType("Rune/Req/StorageAdd", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "倉庫枠上限", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "幻晶石不足", FlowNode.PinTypes.Output, 103)]
  public class FlowNode_ReqRuneStorageAdd : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_IS_UPPER_LIMIT_FOR_STORAGE = 102;
    protected const int PIN_OUT_NOT_ENOUGH_COIN = 103;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = false;
      if (MonoSingleton<GameManager>.Instance.Player.Coin < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneStorageCoinCost)
      {
        this.ActivateOutputLinks(103);
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqRuneStorageAdd(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneStorageAdd.Response json = (ReqRuneStorageAdd.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneStorageAdd.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneStorageAdd.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        json = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.Rune_IsUpperLimitForStorage:
            this.ActivateOutputLinks(102);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.Rune_NotEnoughCoin:
            this.ActivateOutputLinks(103);
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
          WebAPI.JSON_BodyResponse<ReqRuneStorageAdd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneStorageAdd.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          json = jsonObject.body;
        }
        if (json == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(json);
            MonoSingleton<GameManager>.Instance.Deserialize(json.player);
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
      public ReqRuneStorageAdd.Response body;
    }
  }
}
