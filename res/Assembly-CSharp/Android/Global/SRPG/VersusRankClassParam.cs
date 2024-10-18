// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankClassParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankClassParam
  {
    private int mScheduleId;
    private RankMatchClass mClass;
    private int mUpPoint;
    private int mDownPoint;
    private int mDownLosingStreak;
    private string mRewardId;
    private int mWinPointMax;
    private int mWinPointMin;
    private int mLosePointMax;
    private int mLosePointMin;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public RankMatchClass Class
    {
      get
      {
        return this.mClass;
      }
    }

    public int UpPoint
    {
      get
      {
        return this.mUpPoint;
      }
    }

    public int DownPoint
    {
      get
      {
        return this.mDownPoint;
      }
    }

    public int DownLosingStreak
    {
      get
      {
        return this.mDownLosingStreak;
      }
    }

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public int WinPointMax
    {
      get
      {
        return this.mWinPointMax;
      }
    }

    public int WinPointMin
    {
      get
      {
        return this.mWinPointMin;
      }
    }

    public int LosePointMax
    {
      get
      {
        return this.mLosePointMax;
      }
    }

    public int LosePointMin
    {
      get
      {
        return this.mLosePointMin;
      }
    }

    public bool Deserialize(JSON_VersusRankClassParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.schedule_id;
      this.mClass = (RankMatchClass) json.type;
      this.mUpPoint = json.up_pt;
      this.mDownPoint = json.down_pt;
      this.mDownLosingStreak = json.down_losing_streak;
      this.mRewardId = json.reward_id;
      this.mWinPointMax = json.win_pt_max;
      this.mWinPointMin = json.win_pt_min;
      this.mLosePointMax = json.lose_pt_max;
      this.mLosePointMin = json.lose_pt_min;
      return true;
    }
  }
}
