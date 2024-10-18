// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchSeasonResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
