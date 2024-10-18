﻿// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WeatherData
  {
    private WeatherParam mWeatherParam;
    private List<BuffEffect> mBuffEffectLists = new List<BuffEffect>();
    private List<CondEffect> mCondEffectLists = new List<CondEffect>();
    private Unit mModifyUnit;
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    private OInt mChangeClock;
    private static WeatherSetParam mCurrentWeatherSet;
    private static bool mIsAllowWeatherChange;
    private static WeatherData mCurrentWeatherData;
    private static bool mIsEntryConditionLog;
    private static bool mIsExecuteUpdate;

    public WeatherParam WeatherParam => this.mWeatherParam;

    public List<BuffEffect> BuffEffectLists => this.mBuffEffectLists;

    public List<CondEffect> CondEffectLists => this.mCondEffectLists;

    public Unit ModifyUnit => this.mModifyUnit;

    public OInt Rank => this.mRank;

    public OInt RankCap => this.mRankCap;

    public OInt ChangeClock => this.mChangeClock;

    public static WeatherSetParam CurrentWeatherSet => WeatherData.mCurrentWeatherSet;

    public static bool IsAllowWeatherChange => WeatherData.mIsAllowWeatherChange;

    public static WeatherData CurrentWeatherData => WeatherData.mCurrentWeatherData;

    public static bool IsEntryConditionLog
    {
      get => WeatherData.mIsEntryConditionLog;
      set => WeatherData.mIsEntryConditionLog = value;
    }

    public static bool IsExecuteUpdate
    {
      get => WeatherData.mIsExecuteUpdate;
      set => WeatherData.mIsExecuteUpdate = value;
    }

    private void setup(string iname, Unit modify_unit, int rank, int rankcap)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect))
        return;
      this.mWeatherParam = instanceDirect.MasterParam.GetWeatherParam(iname);
      if (this.mWeatherParam == null)
        return;
      this.mRankCap = (OInt) Math.Max(rankcap, 1);
      this.mRank = (OInt) Math.Min(rank, (int) this.mRankCap);
      this.mBuffEffectLists.Clear();
      foreach (string buffIdList in this.mWeatherParam.BuffIdLists)
      {
        BuffEffect buffEffect = BuffEffect.CreateBuffEffect(instanceDirect.MasterParam.GetBuffEffectParam(buffIdList), rank, rankcap);
        if (buffEffect != null)
          this.mBuffEffectLists.Add(buffEffect);
      }
      this.mCondEffectLists.Clear();
      foreach (string condIdList in this.mWeatherParam.CondIdLists)
      {
        CondEffect condEffect = CondEffect.CreateCondEffect(instanceDirect.MasterParam.GetCondEffectParam(condIdList), rank, rankcap);
        if (condEffect != null)
          this.mCondEffectLists.Add(condEffect);
      }
      this.mModifyUnit = modify_unit;
    }

    private void updatePassive(Unit target)
    {
      if (target == null)
        return;
      this.detachPassive(target);
      this.attachBuffPassive(target);
      this.attachCond(WeatherData.eCondAttachType.PASSIVE, target);
      target.CalcCurrentStatus();
    }

    private void detachPassive(Unit target)
    {
      if (target == null)
        return;
      for (int index = 0; index < target.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = target.BuffAttachments[index];
        if (buffAttachment.UseCondition == ESkillCondition.Weather && (bool) buffAttachment.IsPassive)
          target.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < target.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = target.CondAttachments[index];
        if (condAttachment.UseCondition == ESkillCondition.Weather && (bool) condAttachment.IsPassive)
          target.CondAttachments.RemoveAt(index--);
      }
    }

    private bool attachBuffPassive(Unit target)
    {
      if (target == null)
        return false;
      bool flag = false;
      foreach (BuffEffect mBuffEffectList in this.mBuffEffectLists)
      {
        if (mBuffEffectList != null && mBuffEffectList.param.chk_timing == EffectCheckTimings.Eternal && mBuffEffectList.CheckEnableBuffTarget(target))
        {
          BaseStatus status1 = new BaseStatus();
          BaseStatus status2 = new BaseStatus();
          BaseStatus status3 = new BaseStatus();
          BaseStatus status4 = new BaseStatus();
          BaseStatus status5 = new BaseStatus();
          BaseStatus status6 = new BaseStatus();
          mBuffEffectList.CalcBuffStatus(ref status1, target.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
          mBuffEffectList.CalcBuffStatus(ref status2, target.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
          mBuffEffectList.CalcBuffStatus(ref status3, target.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
          mBuffEffectList.CalcBuffStatus(ref status4, target.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
          mBuffEffectList.CalcBuffStatus(ref status5, target.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
          mBuffEffectList.CalcBuffStatus(ref status6, target.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, false))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Buff, false, SkillParamCalcTypes.Add, status1);
            target.SetBuffAttachment(buffAttachment);
          }
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, true))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Buff, true, SkillParamCalcTypes.Add, status2);
            target.SetBuffAttachment(buffAttachment);
          }
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Buff, false, SkillParamCalcTypes.Scale, status3);
            target.SetBuffAttachment(buffAttachment);
          }
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, false))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, status4);
            target.SetBuffAttachment(buffAttachment);
          }
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, true))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, status5);
            target.SetBuffAttachment(buffAttachment);
          }
          if (mBuffEffectList.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale))
          {
            BuffAttachment buffAttachment = this.createBuffAttachment(target, mBuffEffectList, BuffTypes.Debuff, false, SkillParamCalcTypes.Scale, status6);
            target.SetBuffAttachment(buffAttachment);
          }
          flag = true;
        }
      }
      return flag;
    }

    private BuffAttachment createBuffAttachment(
      Unit target,
      BuffEffect effect,
      BuffTypes buff_type,
      bool is_negative_value_is_buff,
      SkillParamCalcTypes calc_type,
      BaseStatus status)
    {
      if (effect == null)
        return (BuffAttachment) null;
      BuffAttachment buffAttachment = new BuffAttachment(effect);
      buffAttachment.user = this.mModifyUnit;
      buffAttachment.skill = (SkillData) null;
      buffAttachment.skilltarget = SkillEffectTargets.Self;
      buffAttachment.IsPassive = (OBool) true;
      buffAttachment.CheckTarget = (Unit) null;
      buffAttachment.DuplicateCount = 0;
      buffAttachment.CheckTiming = effect.param.chk_timing;
      buffAttachment.turn = effect.param.turn;
      buffAttachment.BuffType = buff_type;
      buffAttachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
      buffAttachment.CalcType = calc_type;
      buffAttachment.UseCondition = ESkillCondition.Weather;
      status.CopyTo(buffAttachment.status);
      return buffAttachment;
    }

    private bool attachCond(
      WeatherData.eCondAttachType cond_at_type,
      Unit target,
      RandXorshift rand = null)
    {
      bool flag = false;
      foreach (CondEffect mCondEffectList in this.mCondEffectLists)
      {
        if (mCondEffectList != null && mCondEffectList.param != null && mCondEffectList.param.conditions != null)
        {
          switch (cond_at_type)
          {
            case WeatherData.eCondAttachType.PASSIVE:
              if (mCondEffectList.param.chk_timing == EffectCheckTimings.Eternal)
                break;
              continue;
            case WeatherData.eCondAttachType.TURN:
              if (mCondEffectList.param.chk_timing != EffectCheckTimings.Eternal)
                break;
              continue;
          }
          if (mCondEffectList.CheckEnableCondTarget(target))
          {
            ConditionEffectTypes type = mCondEffectList.param.type;
            switch (type)
            {
              case ConditionEffectTypes.CureCondition:
                for (int index = 0; index < mCondEffectList.param.conditions.Length; ++index)
                {
                  EUnitCondition condition = mCondEffectList.param.conditions[index];
                  this.cureCond(target, condition);
                }
                break;
              case ConditionEffectTypes.FailCondition:
                if ((int) mCondEffectList.value != 0)
                {
                  EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
                  for (int index = 0; index < mCondEffectList.param.conditions.Length; ++index)
                  {
                    EUnitCondition condition = mCondEffectList.param.conditions[index];
                    if (!target.IsDisableUnitCondition(condition) && this.checkFailCond(target, (int) mCondEffectList.value, (int) enchantResist[condition], condition, rand))
                      this.failCond(target, mCondEffectList, type, condition);
                  }
                  break;
                }
                break;
              case ConditionEffectTypes.ForcedFailCondition:
                for (int index = 0; index < mCondEffectList.param.conditions.Length; ++index)
                {
                  EUnitCondition condition = mCondEffectList.param.conditions[index];
                  this.failCond(target, mCondEffectList, type, condition);
                }
                break;
              case ConditionEffectTypes.RandomFailCondition:
                if ((int) mCondEffectList.value != 0)
                {
                  EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
                  int index = rand == null ? 0 : (int) ((long) rand.Get() % (long) mCondEffectList.param.conditions.Length);
                  EUnitCondition condition = mCondEffectList.param.conditions[index];
                  if (!target.IsDisableUnitCondition(condition) && this.checkFailCond(target, (int) mCondEffectList.value, (int) enchantResist[condition], condition, rand))
                  {
                    this.failCond(target, mCondEffectList, type, condition);
                    break;
                  }
                  break;
                }
                break;
              case ConditionEffectTypes.DisableCondition:
                for (int index = 0; index < mCondEffectList.param.conditions.Length; ++index)
                {
                  CondAttachment condAttachment = this.createCondAttachment(target, mCondEffectList, type, mCondEffectList.param.conditions[index]);
                  CondAttachment sameCondAttachment = this.getSameCondAttachment(target, condAttachment);
                  if (sameCondAttachment != null)
                    sameCondAttachment.turn = condAttachment.turn;
                  else
                    target.SetCondAttachment(condAttachment);
                }
                break;
            }
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool checkFailCond(
      Unit target,
      int val,
      int resist,
      EUnitCondition condition,
      RandXorshift rand)
    {
      if (val <= 0)
        return false;
      int num1 = val - resist;
      if (num1 <= 0)
        return false;
      int num2 = 0;
      if (rand != null)
        num2 = (int) (rand.Get() % 100U);
      return num1 > num2;
    }

    private void cureCond(Unit target, EUnitCondition condition)
    {
      target.CureCondEffects(condition);
    }

    private void failCond(
      Unit target,
      CondEffect effect,
      ConditionEffectTypes effect_type,
      EUnitCondition condition)
    {
      CondAttachment condAttachment = this.createCondAttachment(target, effect, effect_type, condition);
      CondAttachment sameCondAttachment = this.getSameCondAttachment(target, condAttachment);
      if (sameCondAttachment != null)
      {
        sameCondAttachment.turn = condAttachment.turn;
      }
      else
      {
        target.SetCondAttachment(condAttachment);
        if (!WeatherData.mIsEntryConditionLog)
          return;
        SceneBattle instance = SceneBattle.Instance;
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
          return;
        BattleCore battle = instance.Battle;
        if (battle == null)
          return;
        LogFailCondition logFailCondition = battle.Log<LogFailCondition>();
        logFailCondition.self = target;
        logFailCondition.source = (Unit) null;
        logFailCondition.condition = condition;
        TacticsUnitController unitController = instance.FindUnitController(target);
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
          return;
        unitController.LockUpdateBadStatus(condition);
      }
    }

    private CondAttachment createCondAttachment(
      Unit target,
      CondEffect effect,
      ConditionEffectTypes type,
      EUnitCondition condition)
    {
      if (type == ConditionEffectTypes.None)
        return (CondAttachment) null;
      if (effect == null)
        return (CondAttachment) null;
      CondAttachment condAttachment = new CondAttachment(effect.param);
      condAttachment.user = (Unit) null;
      condAttachment.skill = (SkillData) null;
      condAttachment.skilltarget = SkillEffectTargets.Self;
      condAttachment.CondId = effect.param.iname;
      condAttachment.IsPassive = (OBool) (effect.param.chk_timing == EffectCheckTimings.Eternal);
      condAttachment.UseCondition = ESkillCondition.Weather;
      condAttachment.CondType = type;
      condAttachment.Condition = condition;
      int num = (int) effect.turn;
      if (num < 2)
        num = 2;
      condAttachment.turn = (OInt) num;
      condAttachment.CheckTiming = effect.param.chk_timing;
      condAttachment.CheckTarget = target;
      if (condAttachment.IsFailCondition())
        condAttachment.IsCurse = effect.IsCurse;
      condAttachment.SetupLinkageBuff();
      return condAttachment;
    }

    private CondAttachment getSameCondAttachment(Unit target, CondAttachment new_cond)
    {
      if (target == null || new_cond == null)
        return (CondAttachment) null;
      for (int index = 0; index < target.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = target.CondAttachments[index];
        if (condAttachment.UseCondition == ESkillCondition.Weather && condAttachment.CondType == new_cond.CondType && condAttachment.Condition == new_cond.Condition && condAttachment.CheckTiming == new_cond.CheckTiming)
          return condAttachment;
      }
      return (CondAttachment) null;
    }

    public int GetResistRate(int now_clock) => 0;

    public static void Initialize(WeatherSetParam weather_set = null, bool is_allow_weather_change = false)
    {
      WeatherData.mCurrentWeatherSet = weather_set;
      WeatherData.mIsAllowWeatherChange = is_allow_weather_change;
      WeatherData.mCurrentWeatherData = (WeatherData) null;
      WeatherData.mIsEntryConditionLog = true;
      WeatherData.IsExecuteUpdate = true;
    }

    public static bool UpdateWeather(List<Unit> units, int now_clock, RandXorshift rand = null)
    {
      if (!WeatherData.mIsExecuteUpdate)
        return false;
      bool flag = false;
      WeatherSetParam currentWeatherSet = WeatherData.mCurrentWeatherSet;
      if (currentWeatherSet != null)
      {
        string iname = (string) null;
        if (WeatherData.mCurrentWeatherData == null)
        {
          iname = currentWeatherSet.GetStartWeather(rand);
          if (string.IsNullOrEmpty(iname))
            return false;
        }
        else
        {
          int mChangeClock = (int) WeatherData.mCurrentWeatherData.mChangeClock;
          if (mChangeClock != 0 && now_clock >= mChangeClock)
          {
            WeatherData.mCurrentWeatherData.mChangeClock = (OInt) currentWeatherSet.GetNextChangeClock(now_clock, rand);
            iname = currentWeatherSet.GetChangeWeather(rand);
          }
        }
        if (!string.IsNullOrEmpty(iname) && WeatherData.ChangeWeather(iname, units, now_clock, rand) != null)
          flag = true;
      }
      if (units != null && WeatherData.mCurrentWeatherData != null)
      {
        WeatherData currentWeatherData = WeatherData.mCurrentWeatherData;
        foreach (Unit unit in units)
        {
          if (!unit.IsGimmick && unit.IsEntry && !unit.IsSub)
            currentWeatherData.attachCond(WeatherData.eCondAttachType.TURN, unit, rand);
        }
      }
      return flag;
    }

    public static WeatherData ChangeWeather(
      string iname,
      List<Unit> units,
      int now_clock,
      RandXorshift rand,
      Unit modify_unit = null,
      int rank = 1,
      int rankcap = 1)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherData) null;
      if (WeatherData.mCurrentWeatherData != null && WeatherData.mCurrentWeatherData.WeatherParam.Iname == iname)
        return (WeatherData) null;
      WeatherData weatherData = new WeatherData();
      weatherData.setup(iname, modify_unit, rank, rankcap);
      if (weatherData.mWeatherParam == null)
        return (WeatherData) null;
      if (WeatherData.mCurrentWeatherSet != null)
        weatherData.mChangeClock = (OInt) WeatherData.mCurrentWeatherSet.GetNextChangeClock(now_clock, rand);
      WeatherData.mCurrentWeatherData = weatherData;
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
      {
        BattleCore battle = instance.Battle;
        if (battle != null)
        {
          LogWeather logWeather = battle.Log<LogWeather>();
          if (logWeather != null)
            logWeather.WeatherData = weatherData;
        }
      }
      if (units != null)
      {
        foreach (Unit unit in units)
        {
          if (!unit.IsGimmick)
            weatherData.updatePassive(unit);
        }
      }
      return weatherData;
    }

    public static void SuspendWeather(
      string iname,
      List<Unit> units,
      Unit modify_unit,
      int rank,
      int rankcap,
      int change_clock)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      WeatherData weatherData = new WeatherData();
      weatherData.setup(iname, modify_unit, rank, rankcap);
      if (weatherData.mWeatherParam == null)
        return;
      weatherData.mChangeClock = (OInt) change_clock;
      if (units != null)
      {
        foreach (Unit unit in units)
        {
          if (!unit.IsGimmick)
            weatherData.updatePassive(unit);
        }
      }
      WeatherData.mCurrentWeatherData = weatherData;
    }

    private enum eCondAttachType
    {
      PASSIVE,
      TURN,
    }
  }
}
