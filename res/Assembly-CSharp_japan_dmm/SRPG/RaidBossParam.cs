// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
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
    private int mDamageAmountRewardId;
    private string mBuffId;
    private string mMobBuffId;
    private bool mIsBoss;

    public int Id => this.mId;

    public int StampIndex => this.mStampIndex;

    public int PeriodId => this.mPeriodId;

    public int Weight => this.mWeight;

    public string Name => this.mName;

    public int HP => this.mHP;

    public string UnitIName => this.mUnitIName;

    public string QuestIName => this.mQuestIName;

    public string TimeLimit => this.mTimeLimit;

    public TimeSpan TimeLimitSpan => this.mTimeLimitSpan;

    public int BattleRewardId => this.mBattleRewardId;

    public int BeatRewardId => this.mBeatRewardId;

    public int DamageRatioRewardId => this.mDamageRatioRewardId;

    public int DamageAmountRewardId => this.mDamageAmountRewardId;

    public string BuffId => this.mBuffId;

    public string MobBuffId => this.mMobBuffId;

    public bool IsBoss => this.mIsBoss;

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
      this.mDamageAmountRewardId = json.damage_amount_reward_id;
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
              int rankValue = BuffEffect.GetRankValue(round, raidPeriod.RoundBuffMax, buffEffectParam.buffs[index].value_ini, buffEffectParam.buffs[index].value_max);
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
  }
}
