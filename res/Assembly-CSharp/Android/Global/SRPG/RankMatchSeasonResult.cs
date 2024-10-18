// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchSeasonResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class RankMatchSeasonResult
  {
    private int mScheduleId;
    private int mScore;
    private int mRank;
    private RankMatchClass mClass;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public int Score
    {
      get
      {
        return this.mScore;
      }
    }

    public int Rank
    {
      get
      {
        return this.mRank;
      }
    }

    public RankMatchClass Class
    {
      get
      {
        return this.mClass;
      }
    }

    public void Deserialize(ReqRankMatchReward.Response res)
    {
      this.mScheduleId = res.schedule_id;
      this.mScore = res.score;
      this.mRank = res.rank;
      this.mClass = (RankMatchClass) res.type;
    }
  }
}
