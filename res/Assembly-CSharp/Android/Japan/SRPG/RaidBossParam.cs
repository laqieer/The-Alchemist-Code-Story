// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class RaidBossParam : RaidMasterParam<JSON_RaidBossParam>
  {
    private int mId;
    private int mStampIndex;
    private int mPeriodId;
    private int mWeight;
    private string mName;
    private int mHP;
    private string mUnitIName;
    private string mQuestIName;
    private string mTimeLimit;
    private TimeSpan mTimeLimitSpan;
    private int mBattleRewardId;
    private int mBeatRewardId;
    private int mDamageRatioRewardId;
    private string mBuffId;
    private string mMobBuffId;
    private bool mIsBoss;

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public int StampIndex
    {
      get
      {
        return this.mStampIndex;
      }
    }

    public int PeriodId
    {
      get
      {
        return this.mPeriodId;
      }
    }

    public int Weight
    {
      get
      {
        return this.mWeight;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int HP
    {
      get
      {
        return this.mHP;
      }
    }

    public string UnitIName
    {
      get
      {
        return this.mUnitIName;
      }
    }

    public string QuestIName
    {
      get
      {
        return this.mQuestIName;
      }
    }

    public string TimeLimit
    {
      get
      {
        return this.mTimeLimit;
      }
    }

    public TimeSpan TimeLimitSpan
    {
      get
      {
        return this.mTimeLimitSpan;
      }
    }

    public int BattleRewardId
    {
      get
      {
        return this.mBattleRewardId;
      }
    }

    public int BeatRewardId
    {
      get
      {
        return this.mBeatRewardId;
      }
    }

    public int DamageRatioRewardId
    {
      get
      {
        return this.mDamageRatioRewardId;
      }
    }

    public string BuffId
    {
      get
      {
        return this.mBuffId;
      }
    }

    public string MobBuffId
    {
      get
      {
        return this.mMobBuffId;
      }
    }

    public bool IsBoss
    {
      get
      {
        return this.mIsBoss;
      }
    }

    public override bool Deserialize(JSON_RaidBossParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mStampIndex = json.stamp_index;
      this.mPeriodId = json.period_id;
      this.mWeight = json.weight;
      this.mName = json.name;
      this.mHP = json.hp;
      this.mUnitIName = json.unit_iname;
      this.mQuestIName = json.quest_iname;
      this.mTimeLimit = json.time_limit;
      this.mBattleRewardId = json.battle_reward_id;
      this.mBeatRewardId = json.beat_reward_id;
      this.mDamageRatioRewardId = json.damage_ratio_reward_id;
      this.mBuffId = json.buff_id;
      this.mMobBuffId = json.mob_buff_id;
      this.mIsBoss = json.is_boss == 1;
      string[] strArray = this.mTimeLimit.Split(':');
      int[] numArray = new int[3];
      if (strArray.Length == 2)
        numArray[2] = 0;
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = int.Parse(strArray[index]);
      this.mTimeLimitSpan = new TimeSpan(numArray[0], numArray[1], numArray[2]);
      return true;
    }

    public static int CalcMaxHP(RaidBossParam param, int round)
    {
      int num = 0;
      if (round > 1)
      {
        BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.BuffId);
        RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(param.PeriodId);
        if (buffEffectParam != null)
        {
          for (int index = 0; index < buffEffectParam.buffs.Length; ++index)
          {
            if (buffEffectParam.buffs[index].type == ParamTypes.Hp || buffEffectParam.buffs[index].type == ParamTypes.HpMax)
            {
              int rankValue = RaidBossParam.GetRankValue(round, raidPeriod.RoundBuffMax, (int) buffEffectParam.buffs[index].value_ini, (int) buffEffectParam.buffs[index].value_max);
              switch (buffEffectParam.buffs[index].calc)
              {
                case SkillParamCalcTypes.Add:
                  num += rankValue;
                  continue;
                case SkillParamCalcTypes.Scale:
                  num += param.HP * rankValue / 100;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      return param.HP + num;
    }

    private static int GetRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      long num3 = (long) (max - ini) * 100L / (long) num1;
      return (int) ((long) ini + num3 * (long) num2 / 100L);
    }
  }
}
