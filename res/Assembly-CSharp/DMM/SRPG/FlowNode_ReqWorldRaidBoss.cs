// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqWorldRaidBoss
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
  [FlowNode.NodeType("WorldRaid/Req/WorldRaidBoss", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "Reload Top", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Out of Period", FlowNode.PinTypes.Output, 202)]
  public class FlowNode_ReqWorldRaidBoss : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 101;
    private const int PIN_OUT_RELOAD_TOP = 201;
    private const int PIN_OUT_FAILED_OUTOFPERIOD = 202;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
      {
        this.OnFailed();
      }
      else
      {
        string iname1 = currentWorldRaidParam.Iname;
        if (string.IsNullOrEmpty(iname1))
        {
          this.OnFailed();
        }
        else
        {
          WorldRaidBossParam worldRaidBossParam = WorldRaidBossManager.Instance.GetCurrentWorldRaidBossParam();
          if (worldRaidBossParam == null)
          {
            this.OnFailed();
          }
          else
          {
            string iname2 = worldRaidBossParam.Iname;
            if (string.IsNullOrEmpty(iname2))
              this.OnFailed();
            else
              this.ExecRequest((WebAPI) new ReqWorldRaidBoss(iname1, iname2, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          }
        }
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
      ReqWorldRaidBoss.Response response = (ReqWorldRaidBoss.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqWorldRaidBoss.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqWorldRaidBoss.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.WorldRaid_OutOfPeriod)
        {
          SRPG.Network.RemoveAPI();
          SRPG.Network.ResetError();
          ((Behaviour) this).enabled = false;
          UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(202)), systemModal: true);
        }
        else
          this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqWorldRaidBoss.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqWorldRaidBoss.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          response = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          if (!WorldRaidBossManager.SetBossData(response.boss))
          {
            this.OnFailed();
            return;
          }
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
      public ReqWorldRaidBoss.Response body;
    }
  }
}
