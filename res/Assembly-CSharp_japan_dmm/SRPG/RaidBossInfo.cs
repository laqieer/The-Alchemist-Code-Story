// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class RaidBossInfo
  {
    private int mNo;
    private int mBossId;
    private int mRound;
    private int mMaxHP;
    private int mHP;
    private long mStartTime;
    private bool mIsReward;
    private bool mIsTimeOver;
    private RaidBossParam mRaidBossParam;

    public int No => this.mNo;

    public int BossId => this.mBossId;

    public int Round => this.mRound;

    public int MaxHP => this.mMaxHP;

    public int HP => this.mHP;

    public long StartTime => this.mStartTime;

    public bool IsReward => this.mIsReward;

    public bool IsTimeOver
    {
      get
      {
        if (this.mIsTimeOver)
          return true;
        if (TimeManager.FromUnixTime(this.mStartTime).AddDays((double) this.mRaidBossParam.TimeLimitSpan.Days).AddHours((double) this.mRaidBossParam.TimeLimitSpan.Hours).AddMinutes((double) this.mRaidBossParam.TimeLimitSpan.Minutes) < TimeManager.ServerTime)
          this.mIsTimeOver = true;
        return this.mIsTimeOver;
      }
    }

    public RaidBossParam RaidBossParam => this.mRaidBossParam;

    public bool Deserialize(JSON_RaidBossInfo json)
    {
      this.mNo = json.no;
      this.mBossId = json.boss_id;
      this.mRound = json.round;
      this.mHP = json.current_hp;
      this.mStartTime = json.start_time;
      this.mIsReward = json.is_reward == 1;
      this.mIsTimeOver = json.is_timeover == 1;
      this.mRaidBossParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(this.mBossId);
      if (this.mRaidBossParam == null)
        return false;
      this.mMaxHP = RaidBossParam.CalcMaxHP(this.mRaidBossParam, this.mRound);
      return true;
    }
  }
}
