// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqDrawCard
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
  [FlowNode.NodeType("DrawCard/Req/GetStatus", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "OutOfPeriod", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_ReqDrawCard : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_OUT_OF_PERIOD = 201;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqDrawCard(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqDrawCard.Response response = (ReqDrawCard.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqDrawCard.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqDrawCard.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.DrawCard_OutOfPeriod)
        {
          SRPG.Network.RemoveAPI();
          SRPG.Network.ResetError();
          this.ActivateOutputLinks(201);
          ((Behaviour) this).enabled = false;
        }
        else
          this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqDrawCard.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqDrawCard.Response>>(www.text);
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
            if (response.drawcard_current_status == null)
              throw new Exception("ReqDrawCard: illegal DrawCard drawcard_current_status data!");
            DrawCardParam.DrawCardStepCount = response.drawcard_current_status.step;
            DrawCardParam.DrawCardEnabled = response.drawcard_current_status.is_finish == 0;
            if (!DrawCardParam.EntrySelectDrawCards(response.drawcard_current_status.draw_infos))
              throw new Exception("ReqDrawCard: illegal select DrawCard information!");
            if (response.rewards == null)
              throw new Exception("ReqDrawCard: illegal DrawCard drawcard_rewards data!");
            if (!DrawCardParam.EntryRewardDrawCards(response.rewards))
              throw new Exception("ReqDrawCard: illegal reward DrawCard information!");
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
      public ReqDrawCard.Response body;
    }
  }
}
