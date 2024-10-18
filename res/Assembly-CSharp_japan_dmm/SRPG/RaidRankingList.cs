// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidRankingList
  {
    private RaidRankingData mMyInfo;
    private List<RaidRankingData> mRanking;

    public RaidRankingData MyInfo => this.mMyInfo;

    public List<RaidRankingData> Ranking => this.mRanking;

    public bool Deserialize(Json_RaidRankingList json)
    {
      this.mMyInfo = new RaidRankingData();
      this.mRanking = new List<RaidRankingData>();
      if (json.my_info != null && !this.mMyInfo.Deserialize(json.my_info))
        return false;
      if (json.ranking != null)
      {
        for (int index = 0; index < json.ranking.Length; ++index)
        {
          RaidRankingData raidRankingData = new RaidRankingData();
          if (raidRankingData.Deserialize(json.ranking[index]))
            this.mRanking.Add(raidRankingData);
        }
      }
      return true;
    }
  }
}
