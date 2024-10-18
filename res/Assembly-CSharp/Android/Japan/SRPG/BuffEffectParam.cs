// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class BuffEffectParam
  {
    public string iname;
    public string job;
    public string buki;
    public string birth;
    public ESex sex;
    public string un_group;
    public int elem;
    public ESkillCondition cond;
    public OInt rate;
    public OInt turn;
    public EffectCheckTargets chk_target;
    public EffectCheckTimings chk_timing;
    public OBool mIsUpBuff;
    public EffectCheckTimings mUpTiming;
    public EAppType mAppType;
    public int mAppMct;
    public EEffRange mEffRange;
    public BuffFlags mFlags;
    public string[] custom_targets;
    public string[] tags;
    public BuffEffectParam.Buff[] buffs;

    public BuffEffectParam.Buff this[ParamTypes type]
    {
      get
      {
        if (this.buffs != null)
          return Array.Find<BuffEffectParam.Buff>(this.buffs, (Predicate<BuffEffectParam.Buff>) (p => p.type == type));
        return (BuffEffectParam.Buff) null;
      }
    }

    public bool Deserialize(JSON_BuffEffectParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.job = json.job;
      this.buki = json.buki;
      this.birth = json.birth;
      this.sex = (ESex) json.sex;
      this.un_group = json.un_group;
      this.elem = Convert.ToInt32(json.elem.ToString("d7"), 2);
      this.rate = (OInt) json.rate;
      this.turn = (OInt) json.turn;
      this.chk_target = (EffectCheckTargets) json.chktgt;
      this.chk_timing = (EffectCheckTimings) json.timing;
      this.cond = (ESkillCondition) json.cond;
      this.mIsUpBuff = (OBool) false;
      this.mUpTiming = (EffectCheckTimings) json.up_timing;
      this.mAppType = (EAppType) json.app_type;
      this.mAppMct = json.app_mct;
      this.mEffRange = (EEffRange) json.eff_range;
      this.mFlags = (BuffFlags) 0;
      if (json.is_up_rep != 0)
        this.mFlags |= BuffFlags.UpReplenish;
      if (json.is_no_dis != 0)
        this.mFlags |= BuffFlags.NoDisabled;
      if (json.is_no_bt != 0)
        this.mFlags |= BuffFlags.NoBuffTurn;
      ParamTypes type1 = (ParamTypes) json.type1;
      ParamTypes type2 = (ParamTypes) json.type2;
      ParamTypes type3 = (ParamTypes) json.type3;
      ParamTypes type4 = (ParamTypes) json.type4;
      ParamTypes type5 = (ParamTypes) json.type5;
      ParamTypes type6 = (ParamTypes) json.type6;
      ParamTypes type7 = (ParamTypes) json.type7;
      ParamTypes type8 = (ParamTypes) json.type8;
      ParamTypes type9 = (ParamTypes) json.type9;
      ParamTypes type10 = (ParamTypes) json.type10;
      ParamTypes type11 = (ParamTypes) json.type11;
      int length = 0;
      if (type1 != ParamTypes.None)
        ++length;
      if (type2 != ParamTypes.None)
        ++length;
      if (type3 != ParamTypes.None)
        ++length;
      if (type4 != ParamTypes.None)
        ++length;
      if (type5 != ParamTypes.None)
        ++length;
      if (type6 != ParamTypes.None)
        ++length;
      if (type7 != ParamTypes.None)
        ++length;
      if (type8 != ParamTypes.None)
        ++length;
      if (type9 != ParamTypes.None)
        ++length;
      if (type10 != ParamTypes.None)
        ++length;
      if (type11 != ParamTypes.None)
        ++length;
      if (length > 0)
      {
        this.buffs = new BuffEffectParam.Buff[length];
        int index = 0;
        if (type1 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type1;
          this.buffs[index].value_ini = (OInt) json.vini1;
          this.buffs[index].value_max = (OInt) json.vmax1;
          this.buffs[index].value_one = (OInt) json.vone1;
          this.buffs[index].tokkou = (OString) json.tktag1;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc1;
          ++index;
        }
        if (type2 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type2;
          this.buffs[index].value_ini = (OInt) json.vini2;
          this.buffs[index].value_max = (OInt) json.vmax2;
          this.buffs[index].value_one = (OInt) json.vone2;
          this.buffs[index].tokkou = (OString) json.tktag2;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc2;
          ++index;
        }
        if (type3 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type3;
          this.buffs[index].value_ini = (OInt) json.vini3;
          this.buffs[index].value_max = (OInt) json.vmax3;
          this.buffs[index].value_one = (OInt) json.vone3;
          this.buffs[index].tokkou = (OString) json.tktag3;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc3;
          ++index;
        }
        if (type4 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type4;
          this.buffs[index].value_ini = (OInt) json.vini4;
          this.buffs[index].value_max = (OInt) json.vmax4;
          this.buffs[index].value_one = (OInt) json.vone4;
          this.buffs[index].tokkou = (OString) json.tktag4;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc4;
          ++index;
        }
        if (type5 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type5;
          this.buffs[index].value_ini = (OInt) json.vini5;
          this.buffs[index].value_max = (OInt) json.vmax5;
          this.buffs[index].value_one = (OInt) json.vone5;
          this.buffs[index].tokkou = (OString) json.tktag5;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc5;
          ++index;
        }
        if (type6 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type6;
          this.buffs[index].value_ini = (OInt) json.vini6;
          this.buffs[index].value_max = (OInt) json.vmax6;
          this.buffs[index].value_one = (OInt) json.vone6;
          this.buffs[index].tokkou = (OString) json.tktag6;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc6;
          ++index;
        }
        if (type7 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type7;
          this.buffs[index].value_ini = (OInt) json.vini7;
          this.buffs[index].value_max = (OInt) json.vmax7;
          this.buffs[index].value_one = (OInt) json.vone7;
          this.buffs[index].tokkou = (OString) json.tktag7;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc7;
          ++index;
        }
        if (type8 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type8;
          this.buffs[index].value_ini = (OInt) json.vini8;
          this.buffs[index].value_max = (OInt) json.vmax8;
          this.buffs[index].value_one = (OInt) json.vone8;
          this.buffs[index].tokkou = (OString) json.tktag8;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc8;
          ++index;
        }
        if (type9 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type9;
          this.buffs[index].value_ini = (OInt) json.vini9;
          this.buffs[index].value_max = (OInt) json.vmax9;
          this.buffs[index].value_one = (OInt) json.vone9;
          this.buffs[index].tokkou = (OString) json.tktag9;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc9;
          ++index;
        }
        if (type10 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type10;
          this.buffs[index].value_ini = (OInt) json.vini10;
          this.buffs[index].value_max = (OInt) json.vmax10;
          this.buffs[index].value_one = (OInt) json.vone10;
          this.buffs[index].tokkou = (OString) json.tktag10;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc10;
          ++index;
        }
        if (type11 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type11;
          this.buffs[index].value_ini = (OInt) json.vini11;
          this.buffs[index].value_max = (OInt) json.vmax11;
          this.buffs[index].value_one = (OInt) json.vone11;
          this.buffs[index].tokkou = (OString) json.tktag11;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc11;
          int num = index + 1;
        }
        foreach (BuffEffectParam.Buff buff in this.buffs)
        {
          if ((int) buff.value_one != 0)
          {
            this.mIsUpBuff = (OBool) true;
            break;
          }
        }
      }
      if (json.custom_targets != null)
      {
        this.custom_targets = new string[json.custom_targets.Length];
        for (int index = 0; index < json.custom_targets.Length; ++index)
          this.custom_targets[index] = json.custom_targets[index];
      }
      if (!string.IsNullOrEmpty(json.tag))
      {
        this.tags = json.tag.Split(',');
        if (this.tags != null && this.tags.Length == 0)
          this.tags = (string[]) null;
      }
      return true;
    }

    public bool IsUpReplenish
    {
      get
      {
        return (this.mFlags & BuffFlags.UpReplenish) != (BuffFlags) 0;
      }
    }

    public bool IsNoDisabled
    {
      get
      {
        return (this.mFlags & BuffFlags.NoDisabled) != (BuffFlags) 0;
      }
    }

    public static bool IsNegativeValueIsBuff(ParamTypes param_type)
    {
      switch (param_type)
      {
        case ParamTypes.ChargeTimeRate:
        case ParamTypes.CastTimeRate:
        case ParamTypes.HpCostRate:
          return true;
        default:
          if (param_type != ParamTypes.UsedJewelRate && param_type != ParamTypes.UsedJewel)
            return false;
          goto case ParamTypes.ChargeTimeRate;
      }
    }

    public bool IsNoBuffTurn
    {
      get
      {
        return (this.mFlags & BuffFlags.NoBuffTurn) != (BuffFlags) 0;
      }
    }

    public static bool CheckCustomTarget(string[] custom_targets, Unit target)
    {
      if (custom_targets == null)
        return true;
      foreach (string customTarget1 in custom_targets)
      {
        if (!string.IsNullOrEmpty(customTarget1))
        {
          CustomTargetParam customTarget2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCustomTarget(customTarget1);
          if (customTarget2 != null)
          {
            if (customTarget2.units != null)
            {
              bool flag = false;
              foreach (string unit in customTarget2.units)
              {
                if (target.UnitParam.iname == unit)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                continue;
            }
            if (customTarget2.jobs != null)
            {
              if (target.Job != null)
              {
                bool flag = false;
                foreach (string job in customTarget2.jobs)
                {
                  if (target.Job.JobID == job)
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  continue;
              }
              else
                continue;
            }
            if (customTarget2.unit_groups != null)
            {
              bool flag = false;
              foreach (string unitGroup1 in customTarget2.unit_groups)
              {
                UnitGroupParam unitGroup2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(unitGroup1);
                if (unitGroup2 == null)
                  Debug.LogWarning((object) "存在しないユニットグループ識別子が設定されている : CustomTarget");
                else if (unitGroup2.IsInGroup(target.UnitParam.iname))
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                continue;
            }
            if (customTarget2.job_groups != null)
            {
              if (target.Job != null)
              {
                bool flag = false;
                foreach (string jobGroup1 in customTarget2.job_groups)
                {
                  JobGroupParam jobGroup2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobGroup(jobGroup1);
                  if (jobGroup2 == null)
                    Debug.LogWarning((object) "存在しないジョブグループ識別子が設定されている : CustomTarget");
                  else if (jobGroup2.IsInGroup(target.Job.JobID))
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  continue;
              }
              else
                continue;
            }
            JobData[] jobs = target.UnitData.Jobs;
            if ((string.IsNullOrEmpty(customTarget2.first_job) || jobs != null && jobs.Length >= 1 && !(jobs[0].JobID != customTarget2.first_job)) && (string.IsNullOrEmpty(customTarget2.second_job) || jobs != null && jobs.Length >= 2 && !(jobs[1].JobID != customTarget2.second_job)) && ((string.IsNullOrEmpty(customTarget2.third_job) || jobs != null && jobs.Length >= 3 && !(jobs[2].JobID != customTarget2.third_job)) && ((customTarget2.sex == ESex.Unknown || customTarget2.sex == target.UnitParam.sex) && (customTarget2.birth_id == 0 || customTarget2.birth_id == target.UnitParam.birthID))))
            {
              if (customTarget2.element != 0)
              {
                int num = 1 << (int) (target.Element - 1 & (EElement) 31);
                if ((customTarget2.element & num) != num)
                  continue;
              }
              return true;
            }
          }
        }
      }
      return false;
    }

    public class Buff
    {
      public ParamTypes type;
      public OInt value_ini;
      public OInt value_max;
      public OInt value_one;
      public OString tokkou;
      public SkillParamCalcTypes calc;
    }
  }
}
