// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/ReqRaidReward", 32741)]
  [FlowNode.Pin(0, "Req", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Guild Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Player", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "Guild", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(30, "Cancel", FlowNode.PinTypes.Output, 30)]
  public class FlowNode_ReqRaidReward : FlowNode_Network
  {
    private const int PIN_IN_REQ = 0;
    private const int PIN_IN_NEXT = 1;
    private const int PIN_OUT_PLAYER = 10;
    private const int PIN_OUT_GUILD = 20;
    private const int PIN_OUT_CANCEL = 30;
    private bool isChecked;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        if (this.isChecked)
          return;
        this.isChecked = true;
        if (MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidRewardPeriod() == null)
        {
          this.ActivateOutputLinks(30);
          return;
        }
        DateTime minValue = DateTime.MinValue;
        DateTime dateTime;
        try
        {
          dateTime = TimeManager.FromUnixTime(PlayerPrefsUtility.GetLong(PlayerPrefsUtility.RAID_RANKING_RECEIVE_DATE));
        }
        catch (Exception ex)
        {
          dateTime = DateTime.MinValue;
        }
        if (dateTime.AddHours(1.0) >= TimeManager.ServerTime)
        {
          this.ActivateOutputLinks(30);
          return;
        }
        this.ExecRequest((WebAPI) new ReqRaidRankingReward(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.mRaidRankRewardResult != null && (!string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.GuildReward) || !string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.GuildMemberReward)))
        this.ActivateOutputLinks(20);
      else
        this.ActivateOutputLinks(30);
    }

    public override void OnSuccess(WWWResult www)
    {
      DebugUtility.Log(nameof (OnSuccess));
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Raid_NotRewardReady:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            this.ActivateOutputLinks(30);
            break;
          case Network.EErrCode.Raid_RankRewardOutOfPeriod:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            this.ActivateOutputLinks(30);
            break;
          case Network.EErrCode.Raid_RankRewardAlreadyReceived:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            this.ActivateOutputLinks(30);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqRaidRankingReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidRankingReward.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          Network.RemoveAPI();
          ((Behaviour) this).enabled = false;
          PlayerPrefsUtility.SetLong(PlayerPrefsUtility.RAID_RANKING_RECEIVE_DATE, TimeManager.FromDateTime(TimeManager.ServerTime));
          if (jsonObject.body.status != 1)
          {
            this.ActivateOutputLinks(30);
          }
          else
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            instance.Player.mRaidRankRewardResult.Deserialize(jsonObject.body);
            if (string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.Reward) && string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.RescueReward))
            {
              if (!string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.GuildReward) || !string.IsNullOrEmpty(instance.Player.mRaidRankRewardResult.GuildMemberReward))
                this.ActivateOutputLinks(20);
              else
                this.ActivateOutputLinks(30);
            }
            else
              this.ActivateOutputLinks(10);
          }
        }
      }
    }

    private enum RaidRewardRankingStatus
    {
      RankingOutSide,
      GetReward,
      RewardNoneReceive,
      RewardTimeOut,
      RewardReceiveed,
    }
  }
}
