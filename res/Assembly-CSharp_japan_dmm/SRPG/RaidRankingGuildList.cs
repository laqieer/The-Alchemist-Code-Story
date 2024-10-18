// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingGuildList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidRankingGuildList
  {
    private RaidRankingGuildData mMyGuildInfo;
    private List<RaidRankingGuildData> mRanking;

    public RaidRankingGuildData MyGuildInfo => this.mMyGuildInfo;

    public List<RaidRankingGuildData> Ranking => this.mRanking;

    public bool Deserialize(Json_RaidRankingGuildList json)
    {
      this.mMyGuildInfo = new RaidRankingGuildData();
      this.mRanking = new List<RaidRankingGuildData>();
      if (json.my_guild_info != null && !this.mMyGuildInfo.Deserialize(json.my_guild_info))
        return false;
      if (json.ranking != null)
      {
        for (int index = 0; index < json.ranking.Length; ++index)
        {
          RaidRankingGuildData rankingGuildData = new RaidRankingGuildData();
          if (rankingGuildData.Deserialize(json.ranking[index]))
            this.mRanking.Add(rankingGuildData);
        }
      }
      return true;
    }
  }
}
