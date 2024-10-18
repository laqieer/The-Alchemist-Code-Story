// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidSeasonResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidSeasonResult
  {
    public int mId;
    public JSON_GuildRaidGuildData mGuild;
    public JSON_GuildRaidRankingRewardData mRanking;

    public int Id => this.mId;

    public JSON_GuildRaidGuildData Guild => this.mGuild;

    public JSON_GuildRaidRankingRewardData Ranking => this.mRanking;

    public void Deserialize(ReqGuildRaidRankingReward.Response res)
    {
      this.mId = res.period_id;
      this.mGuild = res.my_guild_info.guild;
      this.mRanking = res.my_guild_info.ranking;
    }
  }
}
