// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRune
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
  [FlowNode.NodeType("ConceptCard/Req/ReqRune", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqRune : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    private bool mIsDataOverride;

    public ReqGetRune.RequestParam CreateReqGetRune(out bool isDataOverride)
    {
      long _last_iid = 0;
      isDataOverride = true;
      return new ReqGetRune.RequestParam(_last_iid);
    }

    public override void OnActivate(int pinID)
    {
      ReqGetRune.RequestParam reqGetRune = this.CreateReqGetRune(out this.mIsDataOverride);
      if (reqGetRune == null || pinID != 1)
        return;
      if (GlobalVars.IsDirtyRuneData.Get())
      {
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqGetRune(reqGetRune, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
      else
        this.ActivateOutputLinks(101);
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGetRune.Response json = (ReqGetRune.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRune.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRune.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          WebAPI.JSON_BodyResponse<ReqGetRune.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGetRune.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(json, this.mIsDataOverride);
            MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(json.rune_storage_used);
            GlobalVars.IsDirtyRuneData.Set(false);
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
      public ReqGetRune.Response body;
    }
  }
}
