// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankClassParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public int ScheduleId => this.mScheduleId;

    public RankMatchClass Class => this.mClass;

    public int UpPoint => this.mUpPoint;

    public int DownPoint => this.mDownPoint;

    public int DownLosingStreak => this.mDownLosingStreak;

    public string RewardId => this.mRewardId;

    public int WinPointMax => this.mWinPointMax;

    public int WinPointMin => this.mWinPointMin;

    public int LosePointMax => this.mLosePointMax;

    public int LosePointMin => this.mLosePointMin;

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
