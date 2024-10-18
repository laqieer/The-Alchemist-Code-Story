// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqWorldRaidReward
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
  [FlowNode.NodeType("WorldRaid/Req/WorldRaidReward", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqWorldRaidReward : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.WorldRaidRewardParamList == null)
      {
        this.Success();
      }
      else
      {
        DateTime serverTime = TimeManager.ServerTime;
        WorldRaidParam worldRaidParam = (WorldRaidParam) null;
        for (int index = 0; index < instance.WorldRaidParamList.Count; ++index)
        {
          if (!(instance.WorldRaidParamList[index].ChallengeEndAt > serverTime) && (worldRaidParam == null || worldRaidParam.ChallengeEndAt < instance.WorldRaidParamList[index].ChallengeEndAt))
            worldRaidParam = instance.WorldRaidParamList[index];
        }
        if (worldRaidParam == null)
        {
          this.Success();
        }
        else
        {
          try
          {
            if (TimeManager.FromUnixTime(PlayerPrefsUtility.GetLong(PlayerPrefsUtility.WORLDRAID_REWARD_RECEIVE_DATE)) >= worldRaidParam.ChallengeEndAt)
            {
              this.Success();
              return;
            }
          }
          catch (Exception ex)
          {
          }
          ((Behaviour) this).enabled = true;
          this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
          this.ExecRequest((WebAPI) new ReqWorldRaidReward(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
      ReqWorldRaidReward.Response response = (ReqWorldRaidReward.Response) null;
      if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        FlowNode_ReqWorldRaidReward.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqWorldRaidReward.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        SRPG.Network.RemoveAPI();
        DebugUtility.Log("world raid reward status : " + response.status.ToString());
        PlayerPrefsUtility.SetLong(PlayerPrefsUtility.WORLDRAID_REWARD_RECEIVE_DATE, TimeManager.FromDateTime(TimeManager.ServerTime));
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqWorldRaidReward.Response body;
    }
  }
}
