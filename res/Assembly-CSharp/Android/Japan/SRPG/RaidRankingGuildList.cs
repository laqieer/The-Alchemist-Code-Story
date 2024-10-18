// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingGuildList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RaidRankingGuildList
  {
    private RaidRankingGuildData mMyGuildInfo;
    private List<RaidRankingGuildData> mRanking;

    public RaidRankingGuildData MyGuildInfo
    {
      get
      {
        return this.mMyGuildInfo;
      }
    }

    public List<RaidRankingGuildData> Ranking
    {
      get
      {
        return this.mRanking;
      }
    }

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
