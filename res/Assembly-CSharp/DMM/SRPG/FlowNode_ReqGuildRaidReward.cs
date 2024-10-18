// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRaidReward
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
  [FlowNode.NodeType("GuildRaid/ReqGuildRaidReward", 32741)]
  [FlowNode.Pin(0, "Req", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Ok", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "Cancel", FlowNode.PinTypes.Output, 20)]
  public class FlowNode_ReqGuildRaidReward : FlowNode_Network
  {
    private const int PIN_IN_REQ = 0;
    private const int PIN_OUT_OK = 10;
    private const int PIN_OUT_CANCEL = 20;
    private bool isChecked;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.isChecked)
        return;
      this.isChecked = true;
      if (MonoSingleton<GameManager>.Instance.GetActiveGuildRaidRankingPeriod() == null)
      {
        this.ActivateOutputLinks(20);
      }
      else
      {
        DateTime minValue = DateTime.MinValue;
        DateTime dateTime;
        try
        {
          dateTime = TimeManager.FromUnixTime(PlayerPrefsUtility.GetLong(PlayerPrefsUtility.GUILDRAID_RANKING_RECEIVE_DATE));
        }
        catch (Exception ex)
        {
          dateTime = DateTime.MinValue;
        }
        if (dateTime.AddHours(1.0) >= TimeManager.ServerTime && MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
        {
          this.ActivateOutputLinks(20);
        }
        else
        {
          this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
          this.ExecRequest((WebAPI) new ReqGuildRaidRankingReward(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
      }
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildRaidRankingReward.Response res = (ReqGuildRaidRankingReward.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildRaidReward.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildRaidReward.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        res = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.Raid_NotRewardReady:
          case SRPG.Network.EErrCode.Raid_RankRewardOutOfPeriod:
          case SRPG.Network.EErrCode.Raid_RankRewardAlreadyReceived:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(20)), systemModal: true);
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
          WebAPI.JSON_BodyResponse<ReqGuildRaidRankingReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRaidRankingReward.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          res = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        SRPG.Network.RemoveAPI();
        ((Behaviour) this).enabled = false;
        PlayerPrefsUtility.SetLong(PlayerPrefsUtility.GUILDRAID_RANKING_RECEIVE_DATE, TimeManager.FromDateTime(TimeManager.ServerTime));
        if (res.status != 1)
          this.ActivateOutputLinks(20);
        else if (string.IsNullOrEmpty(res.my_guild_info.ranking.reward_id))
        {
          this.ActivateOutputLinks(20);
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.mGuildRaidSeasonResult.Deserialize(res);
          this.Success();
        }
      }
    }

    private enum GuildRaidRewardRankingStatus
    {
      RankingOutSide,
      GetReward,
      RewardNoneReceive,
      RewardTimeOut,
      RewardReceiveed,
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGuildRaidRankingReward.Response body;
    }
  }
}
