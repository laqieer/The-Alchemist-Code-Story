// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidParam
  {
    private string mIname;
    private DateTime mBeginAt;
    private DateTime mChallengeEndAt;
    private DateTime mEndAt;
    private string mDescURL;
    private string mDescTitle;
    private List<WorldRaidParam.BossInfo> mBossInfoList = new List<WorldRaidParam.BossInfo>();
    public const char TIME_SPLIT_SEPARATOR = ':';

    public string Iname => this.mIname;

    public DateTime BeginAt => this.mBeginAt;

    public DateTime ChallengeEndAt => this.mChallengeEndAt;

    public DateTime EndAt => this.mEndAt;

    public string DescURL => this.mDescURL;

    public string DescTitle => this.mDescTitle;

    public List<WorldRaidParam.BossInfo> BossInfoList => this.mBossInfoList;

    public void Deserialize(JSON_WorldRaidParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mBeginAt = DateTime.MinValue;
      this.mChallengeEndAt = DateTime.MaxValue;
      this.mEndAt = DateTime.MaxValue;
      DateTime result1 = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.event_day))
        DateTime.TryParse(json.event_day, out result1);
      if (!string.IsNullOrEmpty(json.begin_time))
      {
        string[] strArray = json.begin_time.Split(':');
        if (strArray.Length >= 2)
        {
          int result2 = 0;
          int result3 = 0;
          int.TryParse(strArray[0], out result2);
          int.TryParse(strArray[1], out result3);
          TimeSpan timeSpan = new TimeSpan(result2, result3, 0);
          this.mBeginAt = result1.Add(timeSpan);
        }
      }
      if (!string.IsNullOrEmpty(json.ch_end_time))
      {
        string[] strArray = json.ch_end_time.Split(':');
        if (strArray.Length >= 2)
        {
          int result4 = 0;
          int result5 = 0;
          int.TryParse(strArray[0], out result4);
          int.TryParse(strArray[1], out result5);
          TimeSpan timeSpan = new TimeSpan(result4, result5, 0);
          this.mChallengeEndAt = result1.Add(timeSpan);
        }
      }
      if (!string.IsNullOrEmpty(json.end_time))
      {
        string[] strArray = json.end_time.Split(':');
        if (strArray.Length >= 2)
        {
          int result6 = 0;
          int result7 = 0;
          int.TryParse(strArray[0], out result6);
          int.TryParse(strArray[1], out result7);
          TimeSpan timeSpan = new TimeSpan(result6, result7, 0);
          this.mEndAt = result1.Add(timeSpan);
        }
      }
      if (this.ChallengeEndAt > this.mEndAt)
        this.mChallengeEndAt = this.mEndAt;
      this.mDescURL = json.desc_url;
      this.mDescTitle = json.desc_title;
      this.mBossInfoList.Clear();
      if (json.boss == null || json.boss.Length == 0)
        return;
      for (int index = 0; index < json.boss.Length; ++index)
      {
        WorldRaidParam.BossInfo bossInfo = new WorldRaidParam.BossInfo();
        bossInfo.Deserialize(json.boss[index]);
        this.mBossInfoList.Add(bossInfo);
      }
    }

    public bool IsWithinPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.BeginAt <= serverTime && serverTime < this.EndAt;
    }

    public bool IsWithinChallenge()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.BeginAt <= serverTime && serverTime < this.ChallengeEndAt;
    }

    public WorldRaidParam.BossInfo GetBossInfo(string wrb_iname)
    {
      return string.IsNullOrEmpty(wrb_iname) ? (WorldRaidParam.BossInfo) null : this.mBossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (t => t.BossId == wrb_iname));
    }

    public static void Deserialize(ref List<WorldRaidParam> list, JSON_WorldRaidParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidParam worldRaidParam = new WorldRaidParam();
        worldRaidParam.Deserialize(json[index]);
        list.Add(worldRaidParam);
      }
    }

    public static WorldRaidParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidParamList == null ? (WorldRaidParam) null : instance.WorldRaidParamList.Find((Predicate<WorldRaidParam>) (p => p.Iname == iname));
    }

    public class BossInfo
    {
      private bool mIsLastBoss;
      private string mBossId;
      private WorldRaidBossParam mBossParam;
      private string mBeatRewardId;
      private WorldRaidRewardParam mBeatRewardParam;
      private string mLaRewardId;
      private WorldRaidRewardParam mLaRewardParam;
      private string mDmgRewardId;
      private WorldRaidDamageRewardParam mDmgRewardParam;
      private string mRankRewardId;
      private WorldRaidRankingRewardParam mRankRewardParam;
      private string mBossBuffId;
      private BuffEffectParam mBossBuffParam;

      public bool IsLastBoss => this.mIsLastBoss;

      public string BossId => this.mBossId;

      public WorldRaidBossParam BossParam
      {
        get
        {
          if (this.mBossParam == null && !string.IsNullOrEmpty(this.mBossId))
            this.mBossParam = WorldRaidBossParam.GetParam(this.mBossId);
          return this.mBossParam;
        }
      }

      public string BeatRewardId => this.mBeatRewardId;

      public WorldRaidRewardParam BeatRewardParam
      {
        get
        {
          if (this.mBeatRewardParam == null && !string.IsNullOrEmpty(this.mBeatRewardId))
            this.mBeatRewardParam = WorldRaidRewardParam.GetParam(this.mBeatRewardId);
          return this.mBeatRewardParam;
        }
      }

      public string LaRewardId => this.mLaRewardId;

      public WorldRaidRewardParam LaRewardParam
      {
        get
        {
          if (this.mLaRewardParam == null && !string.IsNullOrEmpty(this.mLaRewardId))
            this.mLaRewardParam = WorldRaidRewardParam.GetParam(this.mLaRewardId);
          return this.mLaRewardParam;
        }
      }

      public string DmgRewardId => this.mDmgRewardId;

      public WorldRaidDamageRewardParam DmgRewardParam
      {
        get
        {
          if (this.mDmgRewardParam == null && !string.IsNullOrEmpty(this.mDmgRewardId))
            this.mDmgRewardParam = WorldRaidDamageRewardParam.GetParam(this.mDmgRewardId);
          return this.mDmgRewardParam;
        }
      }

      public string RankRewardId => this.mRankRewardId;

      public WorldRaidRankingRewardParam RankRewardParam
      {
        get
        {
          if (this.mRankRewardParam == null && !string.IsNullOrEmpty(this.mRankRewardId))
            this.mRankRewardParam = WorldRaidRankingRewardParam.GetParam(this.mRankRewardId);
          return this.mRankRewardParam;
        }
      }

      public string BossBuffId => this.mBossBuffId;

      public BuffEffectParam BossBuffParam
      {
        get
        {
          if (this.mBossBuffParam == null && !string.IsNullOrEmpty(this.mBossBuffId))
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.MasterParam != null)
              this.mBossBuffParam = instance.MasterParam.GetBuffEffectParam(this.mBossBuffId);
          }
          return this.mBossBuffParam;
        }
      }

      public void Deserialize(JSON_WorldRaidParam.BossInfo json)
      {
        if (json == null)
          return;
        this.mIsLastBoss = json.is_last_boss != 0;
        this.mBossId = json.boss_id;
        this.mBeatRewardId = json.beat_reward_id;
        this.mLaRewardId = json.la_reward_id;
        this.mDmgRewardId = json.dmg_reward_id;
        this.mRankRewardId = json.rank_reward_id;
        this.mBossBuffId = json.eb_buff_id;
      }
    }

    public enum eTimeSplit
    {
      HOUR,
      MIN,
      MAX,
    }
  }
}
