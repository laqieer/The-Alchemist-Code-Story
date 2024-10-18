// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidRankingReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidRankingReward : WebAPI
  {
    public ReqGuildRaidRankingReward(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/reward";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RewardResponse
    {
      public Json_RaidRankRewardInfoData my_info;
      public string reward_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public int status;
      public int period_id;
      public JSON_GuildRaidGuildRanking my_guild_info;
    }
  }
}
