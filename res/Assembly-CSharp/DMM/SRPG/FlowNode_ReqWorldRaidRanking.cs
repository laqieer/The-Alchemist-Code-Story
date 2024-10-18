// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqWorldRaidRanking
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
  [FlowNode.NodeType("WorldRaid/Req/WorldRaidRanking", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "DuplicationRequest", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(201, "Out of Period", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_ReqWorldRaidRanking : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 101;
    private const int PIN_OUT_DUPLICATION_REQUEST = 102;
    private const int PIN_OUT_FAILED_OUTOFPERIOD = 201;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (((Behaviour) this).enabled)
      {
        this.ActivateOutputLinks(102);
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        string bossIname = string.Empty;
        int page = 0;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidRankingWindow.Instance, (UnityEngine.Object) null))
        {
          bossIname = WorldRaidRankingWindow.Instance.GetTargetBossInfo();
          page = WorldRaidRankingWindow.Instance.GetNowRankingPage();
        }
        if (string.IsNullOrEmpty(bossIname))
          this.OnFailed();
        else
          this.ExecRequest((WebAPI) new ReqWorldRaidRanking(bossIname, page, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqWorldRaidRanking.Response worldraid_rankings = (ReqWorldRaidRanking.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqWorldRaidRanking.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqWorldRaidRanking.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        worldraid_rankings = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.WorldRaid_OutOfPeriod:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(201)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.WorldRaid_NotAppearLastBoss:
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
          WebAPI.JSON_BodyResponse<ReqWorldRaidRanking.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqWorldRaidRanking.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          worldraid_rankings = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidRankingWindow.Instance, (UnityEngine.Object) null))
            WorldRaidRankingWindow.Instance.SetRankingData(worldraid_rankings);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.OnFailed();
          return;
        }
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqWorldRaidRanking.Response body;
    }
  }
}
