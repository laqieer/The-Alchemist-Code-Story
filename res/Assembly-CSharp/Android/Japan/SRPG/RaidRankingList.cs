// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RaidRankingList
  {
    private RaidRankingData mMyInfo;
    private List<RaidRankingData> mRanking;

    public RaidRankingData MyInfo
    {
      get
      {
        return this.mMyInfo;
      }
    }

    public List<RaidRankingData> Ranking
    {
      get
      {
        return this.mRanking;
      }
    }

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
