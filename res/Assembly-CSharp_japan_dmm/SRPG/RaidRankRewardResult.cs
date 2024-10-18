// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankRewardResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidRankRewardResult
  {
    private int mStatus;
    private int mPeriodId;
    private int mRank;
    private int mScore;
    private string mReward;
    private int mResqueRank;
    private int mResqueScore;
    private string mRescueReward;
    private ViewGuildData mGuild;
    private int mGuildRank;
    private int mGuildScore;
    private string mGuildReward;
    private string mGuildMemberReward;

    public int Status => this.mStatus;

    public int PeriodId => this.mPeriodId;

    public int Score => this.mScore;

    public int Rank => this.mRank;

    public string Reward => this.mReward;

    public int ResqueScore => this.mResqueScore;

    public int ResqueRank => this.mResqueRank;

    public string RescueReward => this.mRescueReward;

    public ViewGuildData Guild => this.mGuild;

    public int GuildScore => this.mGuildScore;

    public int GuildRank => this.mGuildRank;

    public string GuildReward => this.mGuildReward;

    public string GuildMemberReward => this.mGuildMemberReward;

    public void Deserialize(ReqRaidRankingReward.Response res)
    {
      this.mStatus = res.status;
      this.mPeriodId = res.period_id;
      if (res.beat != null && res.beat.my_info != null)
      {
        this.mRank = res.beat.my_info.rank;
        this.mScore = res.beat.my_info.score;
        this.mReward = res.beat.reward_id;
      }
      if (res.rescue != null && res.rescue.my_info != null)
      {
        this.mResqueRank = res.rescue.my_info.rank;
        this.mResqueScore = res.rescue.my_info.score;
        this.mRescueReward = res.rescue.reward_id;
      }
      if (res.my_guild_info == null || res.my_guild_info.beat == null)
        return;
      this.mGuildRank = res.my_guild_info.beat.rank;
      this.mGuildScore = res.my_guild_info.beat.score;
      this.mGuildReward = res.my_guild_info.guild_reward_id;
      this.mGuildMemberReward = res.my_guild_info.reward_id;
      if (res.my_guild_info.guild == null)
        return;
      this.mGuild = new ViewGuildData();
      this.mGuild.Deserialize(res.my_guild_info.guild);
    }
  }
}
