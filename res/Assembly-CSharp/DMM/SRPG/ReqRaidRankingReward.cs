// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRankingReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidRankingReward : WebAPI
  {
    public ReqRaidRankingReward(Network.ResponseCallback response)
    {
      this.name = "raidboss/reward/ranking";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class RewardResponse
    {
      public Json_RaidRankRewardInfoData my_info;
      public string reward_id;
    }

    [Serializable]
    public class GuildRewardResponse
    {
      public JSON_ViewGuild guild;
      public Json_RaidRankRewardInfoData beat;
      public string guild_reward_id;
      public string reward_id;
    }

    [Serializable]
    public class Response
    {
      public int status;
      public int period_id;
      public ReqRaidRankingReward.RewardResponse beat;
      public ReqRaidRankingReward.RewardResponse rescue;
      public ReqRaidRankingReward.GuildRewardResponse my_guild_info;
    }
  }
}
