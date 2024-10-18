// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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

    public int No
    {
      get
      {
        return this.mNo;
      }
    }

    public int BossId
    {
      get
      {
        return this.mBossId;
      }
    }

    public int Round
    {
      get
      {
        return this.mRound;
      }
    }

    public int MaxHP
    {
      get
      {
        return this.mMaxHP;
      }
    }

    public int HP
    {
      get
      {
        return this.mHP;
      }
    }

    public long StartTime
    {
      get
      {
        return this.mStartTime;
      }
    }

    public bool IsReward
    {
      get
      {
        return this.mIsReward;
      }
    }

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

    public RaidBossParam RaidBossParam
    {
      get
      {
        return this.mRaidBossParam;
      }
    }

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
