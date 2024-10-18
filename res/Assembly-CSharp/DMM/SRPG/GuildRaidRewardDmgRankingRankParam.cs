// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardDmgRankingRankParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardDmgRankingRankParam
  {
    public int RankStart { get; private set; }

    public int RankEnd { get; private set; }

    public string RewardRoundId { get; private set; }

    public bool Deserialize(JSON_GuildRaidRewardDmgRankingRankParam json)
    {
      this.RankStart = json.rank_start;
      this.RankEnd = json.rank_end;
      this.RewardRoundId = json.reward_round_id;
      return true;
    }
  }
}
