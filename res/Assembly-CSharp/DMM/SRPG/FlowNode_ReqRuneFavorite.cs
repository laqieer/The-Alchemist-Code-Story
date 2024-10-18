// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRuneFavorite
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
  [FlowNode.NodeType("Rune/Req/Favorite", 32741)]
  [FlowNode.Pin(1, "ON", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "OFF", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "反転", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(101, "⇒ ON", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "⇒ OFF", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqRuneFavorite : FlowNode_Network
  {
    protected const int PIN_IN_FAVORITE_ON = 1;
    protected const int PIN_IN_FAVORITE_OFF = 2;
    protected const int PIN_IN_FAVORITE_INVERT = 3;
    protected const int PIN_OUT_FAVORITE_ON = 101;
    protected const int PIN_OUT_FAVORITE_OFF = 102;
    private BindRuneData mRuneData;

    public override void OnActivate(int pinID)
    {
      if (this.mRuneData == null)
      {
        DebugUtility.LogError("mRuneData が null");
      }
      else
      {
        ReqRuneFavorite.RequestParam rp;
        switch (pinID)
        {
          case 1:
            rp = new ReqRuneFavorite.RequestParam(this.mRuneData.iid, true);
            break;
          case 2:
            rp = new ReqRuneFavorite.RequestParam(this.mRuneData.iid, false);
            break;
          case 3:
            rp = new ReqRuneFavorite.RequestParam(this.mRuneData.iid, !this.mRuneData.IsFavorite);
            break;
          default:
            return;
        }
        if (rp == null)
        {
          DebugUtility.LogError("RequestParam が null");
        }
        else
        {
          ((Behaviour) this).enabled = true;
          this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
          this.ExecRequest((WebAPI) new ReqRuneFavorite(rp, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
      }
    }

    private void Success(int successPinID)
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(successPinID);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqRuneFavorite.Response json = (ReqRuneFavorite.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRuneFavorite.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRuneFavorite.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          WebAPI.JSON_BodyResponse<ReqRuneFavorite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRuneFavorite.Response>>(www.text);
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
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          if (json.rune == null)
            return;
          if (json.rune.IsFavorite)
            this.Success(101);
          else
            this.Success(102);
        }
      }
    }

    public void SetRuneData(BindRuneData rune) => this.mRuneData = rune;

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqRuneFavorite.Response body;
    }
  }
}
