// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchSeasonResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RankMatchSeasonResult
  {
    private int mScheduleId;
    private int mScore;
    private int mRank;
    private RankMatchClass mClass;

    public int ScheduleId => this.mScheduleId;

    public int Score => this.mScore;

    public int Rank => this.mRank;

    public RankMatchClass Class => this.mClass;

    public void Deserialize(ReqRankMatchReward.Response res)
    {
      this.mScheduleId = res.schedule_id;
      this.mScore = res.score;
      this.mRank = res.rank;
      this.mClass = (RankMatchClass) res.type;
    }
  }
}
