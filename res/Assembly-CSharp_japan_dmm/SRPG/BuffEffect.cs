// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class BuffEffect
  {
    public BuffEffectParam param;
    public List<BuffEffect.BuffTarget> targets;

    [IgnoreMember]
    public BuffEffect.BuffTarget this[ParamTypes type]
    {
      get
      {
        return this.targets != null ? this.targets.Find((Predicate<BuffEffect.BuffTarget>) (p => p.paramType == type)) : (BuffEffect.BuffTarget) null;
      }
    }

    public static BuffEffect CreateBuffEffect(BuffEffectParam param, int rank, int rankcap)
    {
      if (param == null || param.buffs == null || param.buffs.Length == 0)
        return (BuffEffect) null;
      BuffEffect buffEffect = new BuffEffect();
      buffEffect.param = param;
      buffEffect.targets = new List<BuffEffect.BuffTarget>(param.buffs.Length);
      buffEffect.UpdateCurrentValues(rank, rankcap);
      return buffEffect;
    }

    public void UpdateCurrentValues(int rank, int rankcap)
    {
      if (this.param == null || this.param.buffs == null || this.param.buffs.Length == 0)
      {
        this.Clear();
      }
      else
      {
        int length = this.param.buffs.Length;
        if (this.targets == null)
          this.targets = new List<BuffEffect.BuffTarget>(length);
        if (this.targets.Count > length)
          this.targets.RemoveRange(length, this.targets.Count - length);
        while (this.targets.Count < length)
          this.targets.Add(new BuffEffect.BuffTarget());
        for (int index = 0; index < length; ++index)
        {
          int valueIni = this.param.buffs[index].value_ini;
          int valueMax = this.param.buffs[index].value_max;
          int rankValue = BuffEffect.GetRankValue(rank, rankcap, valueIni, valueMax);
          this.targets[index].value = (OInt) rankValue;
          this.targets[index].value_one = (OInt) this.param.buffs[index].value_one;
          this.targets[index].calcType = this.param.buffs[index].calc;
          this.targets[index].paramType = this.param.buffs[index].type;
          this.targets[index].tokkou = (OString) this.param.buffs[index].tokkou;
          this.targets[index].buffType = !BuffEffectParam.IsNegativeValueIsBuff(this.param.buffs[index].type) ? (rankValue >= 0 ? BuffTypes.Buff : BuffTypes.Debuff) : (rankValue <= 0 ? BuffTypes.Buff : BuffTypes.Debuff);
        }
      }
    }

    public static int GetRankValue(int rank, int rankcap, int ini, int max)
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

    private void Clear()
    {
      this.param = (BuffEffectParam) null;
      this.targets = (List<BuffEffect.BuffTarget>) null;
    }

    public bool CheckBuffCalcType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        if (buff == this.targets[index].buffType && calc == this.targets[index].calcType)
          return true;
      }
      return false;
    }

    public bool CheckBuffCalcType(
      BuffTypes buff,
      SkillParamCalcTypes calc,
      bool is_negative_value_is_buff)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = this.targets[index];
        if (buff == target.buffType && calc == target.calcType && BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff)
          return true;
      }
      return false;
    }

    public bool CheckEnableBuffTarget(Unit target)
    {
      if (this.param == null)
        return false;
      bool flag1 = true;
      if (this.param.sex != ESex.Unknown)
        flag1 &= this.param.sex == target.UnitParam.sex;
      if (this.param.elem != 0)
      {
        int num = 1 << (int) (target.Element - (byte) 1 & (EElement) 31);
        flag1 &= (this.param.elem & num) == num;
      }
      if (!string.IsNullOrEmpty(this.param.job) && target.Job != null)
        flag1 &= this.param.job == target.Job.Param.origin;
      if (!string.IsNullOrEmpty(this.param.buki))
      {
        if (target.Job != null)
          flag1 &= this.param.buki == target.Job.Param.buki;
        else
          flag1 &= this.param.buki == target.UnitParam.dbuki;
      }
      if (!string.IsNullOrEmpty(this.param.birth))
        flag1 &= this.param.birth == (string) target.UnitParam.birth;
      if (this.param.tags != null)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) this.param.tags);
        bool flag2 = false;
        string[] tags = target.GetTags();
        if (tags != null)
        {
          for (int index = 0; index < tags.Length; ++index)
          {
            if (stringList.Contains(tags[index]))
            {
              flag2 = true;
              break;
            }
          }
        }
        flag1 &= flag2;
      }
      if (!string.IsNullOrEmpty(this.param.un_group))
      {
        UnitGroupParam unitGroup = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(this.param.un_group);
        if (unitGroup != null)
          flag1 &= unitGroup.IsInGroup(target.UnitParam.iname);
      }
      if (this.param.custom_targets != null && (this.param.cond == ESkillCondition.CardSkill || this.param.cond == ESkillCondition.CardLsSkill))
        flag1 &= BuffEffectParam.CheckCustomTarget(this.param.custom_targets, target, this.param.cond);
      return flag1;
    }

    private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      if (calc != SkillParamCalcTypes.Scale)
        return BuffMethodTypes.Add;
      return buff == BuffTypes.Buff ? BuffMethodTypes.Highest : BuffMethodTypes.Lowest;
    }

    private static void SetBuffValue(BuffMethodTypes type, ref OInt param, int value)
    {
      switch (type)
      {
        case BuffMethodTypes.Add:
          param = (OInt) ((int) param + value);
          break;
        case BuffMethodTypes.Highest:
          if ((int) param >= value)
            break;
          param = (OInt) value;
          break;
        case BuffMethodTypes.Lowest:
          if ((int) param <= value)
            break;
          param = (OInt) value;
          break;
      }
    }

    private static void SetBuffValue(BuffMethodTypes type, ref OShort param, int value)
    {
      switch (type)
      {
        case BuffMethodTypes.Add:
          param = (OShort) ((int) param + value);
          break;
        case BuffMethodTypes.Highest:
          if ((int) param >= value)
            break;
          param = (OShort) value;
          break;
        case BuffMethodTypes.Lowest:
          if ((int) param <= value)
            break;
          param = (OShort) value;
          break;
      }
    }

    private static void SetBuffValue(BuffMethodTypes type, ref short param, int value)
    {
      switch (type)
      {
        case BuffMethodTypes.Add:
          param += (short) value;
          break;
        case BuffMethodTypes.Highest:
          if ((int) param >= value)
            break;
          param = (short) value;
          break;
        case BuffMethodTypes.Lowest:
          if ((int) param <= value)
            break;
          param = (short) value;
          break;
      }
    }

    private static void SetBuffValue(
      BuffMethodTypes type,
      ref TokkouParam param,
      int value,
      string tag)
    {
      if (string.IsNullOrEmpty(tag))
        return;
      TokkouValue tokkouValue = param.Find(tag);
      if (tokkouValue == null)
      {
        param.Add(new TokkouValue()
        {
          tag = tag,
          value = (short) value
        });
      }
      else
      {
        switch (type)
        {
          case BuffMethodTypes.Add:
            tokkouValue.value += (short) value;
            break;
          case BuffMethodTypes.Highest:
            if ((int) tokkouValue.value >= value)
              break;
            tokkouValue.value = (short) value;
            break;
          case BuffMethodTypes.Lowest:
            if ((int) tokkouValue.value <= value)
              break;
            tokkouValue.value = (short) value;
            break;
        }
      }
    }

    public static void SetDrawBuffValues(
      ParamTypes param_type,
      BuffMethodTypes method_type,
      ref DrawBaseStatus draw_status,
      int value)
    {
      if (draw_status == null)
        return;
      short num = (short) draw_status[param_type];
      BuffEffect.SetBuffValue(method_type, ref num, value);
      draw_status.SetAditionalValue(param_type, (int) num);
    }

    public static void SetBuffValues(
      ParamTypes param_type,
      BuffMethodTypes method_type,
      ref BaseStatus status,
      int value,
      string tag = null)
    {
      switch (param_type)
      {
        case ParamTypes.Hp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.HpMax:
          BuffEffect.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.Mp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[0], value);
          break;
        case ParamTypes.MpIni:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[1], value);
          break;
        case ParamTypes.Atk:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[2], value);
          break;
        case ParamTypes.Def:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[3], value);
          break;
        case ParamTypes.Mag:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[4], value);
          break;
        case ParamTypes.Mnd:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[5], value);
          break;
        case ParamTypes.Rec:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[6], value);
          break;
        case ParamTypes.Dex:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[7], value);
          break;
        case ParamTypes.Spd:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[8], value);
          break;
        case ParamTypes.Cri:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[9], value);
          break;
        case ParamTypes.Luk:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[10], value);
          break;
        case ParamTypes.Mov:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[11], value);
          break;
        case ParamTypes.Jmp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[12], value);
          break;
        case ParamTypes.EffectRange:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[0], value);
          break;
        case ParamTypes.EffectScope:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[1], value);
          break;
        case ParamTypes.EffectHeight:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[2], value);
          break;
        case ParamTypes.Assist_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[1], value);
          break;
        case ParamTypes.Assist_Water:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[2], value);
          break;
        case ParamTypes.Assist_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[3], value);
          break;
        case ParamTypes.Assist_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[4], value);
          break;
        case ParamTypes.Assist_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[5], value);
          break;
        case ParamTypes.Assist_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[6], value);
          break;
        case ParamTypes.Assist_Poison:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          break;
        case ParamTypes.Assist_Paralysed:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          break;
        case ParamTypes.Assist_Stun:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          break;
        case ParamTypes.Assist_Sleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          break;
        case ParamTypes.Assist_Charm:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          break;
        case ParamTypes.Assist_Stone:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          break;
        case ParamTypes.Assist_Blind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          break;
        case ParamTypes.Assist_DisableSkill:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          break;
        case ParamTypes.Assist_DisableMove:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          break;
        case ParamTypes.Assist_DisableAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          break;
        case ParamTypes.Assist_Zombie:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[10], value);
          break;
        case ParamTypes.Assist_DeathSentence:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          break;
        case ParamTypes.Assist_Berserk:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          break;
        case ParamTypes.Assist_Knockback:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[13], value);
          break;
        case ParamTypes.Assist_ResistBuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[14], value);
          break;
        case ParamTypes.Assist_ResistDebuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[15], value);
          break;
        case ParamTypes.Assist_Stop:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          break;
        case ParamTypes.Assist_Fast:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          break;
        case ParamTypes.Assist_Slow:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          break;
        case ParamTypes.Assist_AutoHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[19], value);
          break;
        case ParamTypes.Assist_Donsoku:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          break;
        case ParamTypes.Assist_Rage:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          break;
        case ParamTypes.Assist_GoodSleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[22], value);
          break;
        case ParamTypes.Assist_ConditionAll:
          if (status is DrawBaseStatus)
          {
            DrawBaseStatus draw_status = status as DrawBaseStatus;
            BuffEffect.SetDrawBuffValues(param_type, method_type, ref draw_status, value);
            break;
          }
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[1], value);
          break;
        case ParamTypes.Resist_Water:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[2], value);
          break;
        case ParamTypes.Resist_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[3], value);
          break;
        case ParamTypes.Resist_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[4], value);
          break;
        case ParamTypes.Resist_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[5], value);
          break;
        case ParamTypes.Resist_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[6], value);
          break;
        case ParamTypes.Resist_Poison:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          break;
        case ParamTypes.Resist_Paralysed:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          break;
        case ParamTypes.Resist_Stun:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          break;
        case ParamTypes.Resist_Sleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          break;
        case ParamTypes.Resist_Charm:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          break;
        case ParamTypes.Resist_Stone:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          break;
        case ParamTypes.Resist_Blind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          break;
        case ParamTypes.Resist_DisableSkill:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          break;
        case ParamTypes.Resist_DisableMove:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          break;
        case ParamTypes.Resist_DisableAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          break;
        case ParamTypes.Resist_Zombie:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[10], value);
          break;
        case ParamTypes.Resist_DeathSentence:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          break;
        case ParamTypes.Resist_Berserk:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          break;
        case ParamTypes.Resist_Knockback:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[13], value);
          break;
        case ParamTypes.Resist_ResistBuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[14], value);
          break;
        case ParamTypes.Resist_ResistDebuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[15], value);
          break;
        case ParamTypes.Resist_Stop:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          break;
        case ParamTypes.Resist_Fast:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[17], value);
          break;
        case ParamTypes.Resist_Slow:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          break;
        case ParamTypes.Resist_AutoHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[19], value);
          break;
        case ParamTypes.Resist_Donsoku:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          break;
        case ParamTypes.Resist_Rage:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          break;
        case ParamTypes.Resist_GoodSleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[22], value);
          break;
        case ParamTypes.Resist_ConditionAll:
          if (status is DrawBaseStatus)
          {
            DrawBaseStatus draw_status = status as DrawBaseStatus;
            BuffEffect.SetDrawBuffValues(param_type, method_type, ref draw_status, value);
            break;
          }
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.HitRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[3], value);
          break;
        case ParamTypes.AvoidRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[4], value);
          break;
        case ParamTypes.CriticalRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[5], value);
          break;
        case ParamTypes.GainJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[13], value);
          break;
        case ParamTypes.UsedJewelRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[14], value);
          break;
        case ParamTypes.ActionCount:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[15], value);
          break;
        case ParamTypes.SlashAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[6], value);
          break;
        case ParamTypes.PierceAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[7], value);
          break;
        case ParamTypes.BlowAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[8], value);
          break;
        case ParamTypes.ShotAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[9], value);
          break;
        case ParamTypes.MagicAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[10], value);
          break;
        case ParamTypes.ReactionAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[11], value);
          break;
        case ParamTypes.JumpAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[12], value);
          break;
        case ParamTypes.GutsRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[16], value);
          break;
        case ParamTypes.AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[17], value);
          break;
        case ParamTypes.ChargeTimeRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[18], value);
          break;
        case ParamTypes.CastTimeRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[19], value);
          break;
        case ParamTypes.BuffTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[20], value);
          break;
        case ParamTypes.DebuffTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[21], value);
          break;
        case ParamTypes.CombinationRange:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[22], value);
          break;
        case ParamTypes.HpCostRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[23], value);
          break;
        case ParamTypes.SkillUseCount:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[24], value);
          break;
        case ParamTypes.PoisonDamage:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[25], value);
          break;
        case ParamTypes.PoisonTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[26], value);
          break;
        case ParamTypes.Assist_AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[23], value);
          break;
        case ParamTypes.Resist_AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[23], value);
          break;
        case ParamTypes.Assist_DisableHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_DisableHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.Resist_Slash:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[27], value);
          break;
        case ParamTypes.Resist_Pierce:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[28], value);
          break;
        case ParamTypes.Resist_Blow:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[29], value);
          break;
        case ParamTypes.Resist_Shot:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[30], value);
          break;
        case ParamTypes.Resist_Magic:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[31], value);
          break;
        case ParamTypes.Resist_Reaction:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[32], value);
          break;
        case ParamTypes.Resist_Jump:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[33], value);
          break;
        case ParamTypes.Avoid_Slash:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[34], value);
          break;
        case ParamTypes.Avoid_Pierce:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[35], value);
          break;
        case ParamTypes.Avoid_Blow:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[36], value);
          break;
        case ParamTypes.Avoid_Shot:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[37], value);
          break;
        case ParamTypes.Avoid_Magic:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[38], value);
          break;
        case ParamTypes.Avoid_Reaction:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[39], value);
          break;
        case ParamTypes.Avoid_Jump:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[40], value);
          break;
        case ParamTypes.GainJewelRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[41], value);
          break;
        case ParamTypes.UsedJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[42], value);
          break;
        case ParamTypes.Assist_SingleAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[25], value);
          break;
        case ParamTypes.Assist_AreaAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[26], value);
          break;
        case ParamTypes.Resist_SingleAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[25], value);
          break;
        case ParamTypes.Resist_AreaAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[26], value);
          break;
        case ParamTypes.Assist_DecCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[27], value);
          break;
        case ParamTypes.Assist_IncCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[28], value);
          break;
        case ParamTypes.Resist_DecCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[27], value);
          break;
        case ParamTypes.Resist_IncCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[28], value);
          break;
        case ParamTypes.Assist_ESA_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[29], value);
          break;
        case ParamTypes.Assist_ESA_Water:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[30], value);
          break;
        case ParamTypes.Assist_ESA_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[31], value);
          break;
        case ParamTypes.Assist_ESA_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[32], value);
          break;
        case ParamTypes.Assist_ESA_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[33], value);
          break;
        case ParamTypes.Assist_ESA_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[34], value);
          break;
        case ParamTypes.Resist_ESA_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[29], value);
          break;
        case ParamTypes.Resist_ESA_Water:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[30], value);
          break;
        case ParamTypes.Resist_ESA_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[31], value);
          break;
        case ParamTypes.Resist_ESA_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[32], value);
          break;
        case ParamTypes.Resist_ESA_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[33], value);
          break;
        case ParamTypes.Resist_ESA_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[34], value);
          break;
        case ParamTypes.UnitDefenseFire:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[43], value);
          break;
        case ParamTypes.UnitDefenseWater:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[44], value);
          break;
        case ParamTypes.UnitDefenseWind:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[45], value);
          break;
        case ParamTypes.UnitDefenseThunder:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[46], value);
          break;
        case ParamTypes.UnitDefenseShine:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[47], value);
          break;
        case ParamTypes.UnitDefenseDark:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[48], value);
          break;
        case ParamTypes.Assist_MaxDamageHp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[35], value);
          break;
        case ParamTypes.Assist_MaxDamageMp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[36], value);
          break;
        case ParamTypes.Resist_MaxDamageHp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[35], value);
          break;
        case ParamTypes.Resist_MaxDamageMp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[36], value);
          break;
        case ParamTypes.Tokkou:
          BuffEffect.SetBuffValue(method_type, ref status.tokkou, value, tag);
          break;
        case ParamTypes.Assist_SideAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[37], value);
          break;
        case ParamTypes.Assist_BackAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[38], value);
          break;
        case ParamTypes.Resist_SideAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[37], value);
          break;
        case ParamTypes.Resist_BackAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[38], value);
          break;
        case ParamTypes.Assist_ObstReaction:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[39], value);
          break;
        case ParamTypes.Resist_ObstReaction:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[39], value);
          break;
        case ParamTypes.CriticalDamageRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[49], value);
          break;
        case ParamTypes.NoDivAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[50], value);
          break;
        case ParamTypes.Resist_NoDiv:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[51], value);
          break;
        case ParamTypes.Avoid_NoDiv:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[52], value);
          break;
        case ParamTypes.Resist_BuffHpMax:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[53], value);
          break;
        case ParamTypes.Resist_BuffAtk:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[54], value);
          break;
        case ParamTypes.Resist_BuffDef:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[55], value);
          break;
        case ParamTypes.Resist_BuffMag:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[56], value);
          break;
        case ParamTypes.Resist_BuffMnd:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[57], value);
          break;
        case ParamTypes.Resist_BuffRec:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[58], value);
          break;
        case ParamTypes.Resist_BuffDex:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[59], value);
          break;
        case ParamTypes.Resist_BuffSpd:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[60], value);
          break;
        case ParamTypes.Resist_BuffCri:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[61], value);
          break;
        case ParamTypes.Resist_BuffLuk:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[62], value);
          break;
        case ParamTypes.Resist_BuffMov:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[63], value);
          break;
        case ParamTypes.Resist_BuffJmp:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[64], value);
          break;
        case ParamTypes.Resist_DebuffHpMax:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[65], value);
          break;
        case ParamTypes.Resist_DebuffAtk:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[66], value);
          break;
        case ParamTypes.Resist_DebuffDef:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[67], value);
          break;
        case ParamTypes.Resist_DebuffMag:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[68], value);
          break;
        case ParamTypes.Resist_DebuffMnd:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[69], value);
          break;
        case ParamTypes.Resist_DebuffRec:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[70], value);
          break;
        case ParamTypes.Resist_DebuffDex:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[71], value);
          break;
        case ParamTypes.Resist_DebuffSpd:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[72], value);
          break;
        case ParamTypes.Resist_DebuffCri:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[73], value);
          break;
        case ParamTypes.Resist_DebuffLuk:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[74], value);
          break;
        case ParamTypes.Resist_DebuffMov:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[75], value);
          break;
        case ParamTypes.Resist_DebuffJmp:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[76], value);
          break;
        case ParamTypes.Assist_ForcedTargeting:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[40], value);
          break;
        case ParamTypes.Resist_ForcedTargeting:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[40], value);
          break;
        case ParamTypes.Tokubou:
          BuffEffect.SetBuffValue(method_type, ref status.tokubou, value, tag);
          break;
      }
    }

    public void CalcBuffStatus(
      ref BaseStatus status,
      EElement element,
      BuffTypes buffType,
      bool is_check_negative_value,
      bool is_negative_value_is_buff,
      SkillParamCalcTypes calcType,
      int up_count = 0,
      int coef = 10000,
      int coefDecreaseRate = 0)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = this.targets[index];
        if (target.buffType == buffType && (!is_check_negative_value || BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff) && target.calcType == calcType && (element == EElement.None || !BuffEffect.IsElementBuff(target.paramType) || BuffEffect.IsMatchElementBuff(element, target.paramType)))
        {
          BuffMethodTypes buffMethodType = this.GetBuffMethodType(target.buffType, calcType);
          ParamTypes paramType = target.paramType;
          int val1 = (int) target.value;
          if ((bool) this.param.mIsUpBuff)
          {
            val1 = (int) target.value_one * up_count;
            if (val1 > 0)
              val1 = Math.Min(val1, (int) target.value);
            else if (val1 < 0)
              val1 = Math.Max(val1, (int) target.value);
          }
          if (coefDecreaseRate != 0)
            val1 = GameUtility.CalcSubRateRoundDown((long) val1, (long) coefDecreaseRate);
          string tokkou = (string) target.tokkou;
          if (coef != 10000)
            val1 = (int) ((long) val1 * (long) coef / 10000L);
          BuffEffect.SetBuffValues(paramType, buffMethodType, ref status, val1, tokkou);
        }
      }
    }

    public void GetBaseStatus(ref BaseStatus total_add, ref BaseStatus total_scale)
    {
      if (total_add == null || total_scale == null)
        return;
      total_add.Clear();
      total_scale.Clear();
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      this.CalcBuffStatus(ref status1, EElement.None, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
      this.CalcBuffStatus(ref status2, EElement.None, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
      this.CalcBuffStatus(ref status3, EElement.None, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
      this.CalcBuffStatus(ref status4, EElement.None, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
      this.CalcBuffStatus(ref status5, EElement.None, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
      this.CalcBuffStatus(ref status6, EElement.None, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
      total_add.Add(status1);
      total_add.Add(status2);
      total_add.Add(status4);
      total_add.Add(status5);
      total_scale.Add(status3);
      total_scale.Add(status6);
    }

    public static bool IsElementBuff(ParamTypes paramType)
    {
      switch (paramType)
      {
        case ParamTypes.Assist_Fire:
        case ParamTypes.Assist_Water:
        case ParamTypes.Assist_Wind:
        case ParamTypes.Assist_Thunder:
        case ParamTypes.Assist_Shine:
        case ParamTypes.Assist_Dark:
        case ParamTypes.UnitDefenseFire:
        case ParamTypes.UnitDefenseWater:
        case ParamTypes.UnitDefenseWind:
        case ParamTypes.UnitDefenseThunder:
        case ParamTypes.UnitDefenseShine:
        case ParamTypes.UnitDefenseDark:
          return true;
        default:
          return false;
      }
    }

    public static bool IsMatchElementBuff(EElement buffTargetElement, ParamTypes paramType)
    {
      switch (paramType)
      {
        case ParamTypes.Assist_Fire:
        case ParamTypes.UnitDefenseFire:
          return buffTargetElement == EElement.Fire;
        case ParamTypes.Assist_Water:
        case ParamTypes.UnitDefenseWater:
          return buffTargetElement == EElement.Water;
        case ParamTypes.Assist_Wind:
        case ParamTypes.UnitDefenseWind:
          return buffTargetElement == EElement.Wind;
        case ParamTypes.Assist_Thunder:
        case ParamTypes.UnitDefenseThunder:
          return buffTargetElement == EElement.Thunder;
        case ParamTypes.Assist_Shine:
        case ParamTypes.UnitDefenseShine:
          return buffTargetElement == EElement.Shine;
        case ParamTypes.Assist_Dark:
        case ParamTypes.UnitDefenseDark:
          return buffTargetElement == EElement.Dark;
        default:
          return false;
      }
    }

    public static BaseStatus CreateBaseStatus(
      int value,
      ParamTypes paramType,
      BuffMethodTypes buffMethodType,
      EElement buffTargetElement = EElement.None,
      bool isDrawBaseStatus = false)
    {
      if (paramType == ParamTypes.None)
        return (BaseStatus) null;
      if (buffTargetElement != EElement.None && BuffEffect.IsElementBuff(paramType) && !BuffEffect.IsMatchElementBuff(buffTargetElement, paramType))
        return (BaseStatus) null;
      BaseStatus status = !isDrawBaseStatus ? new BaseStatus() : (BaseStatus) new DrawBaseStatus();
      BuffEffect.SetBuffValues(paramType, buffMethodType, ref status, value);
      return status;
    }

    [MessagePackObject(true)]
    public class BuffTarget
    {
      public BuffTypes buffType;
      public SkillParamCalcTypes calcType;
      public ParamTypes paramType;
      public OInt value;
      public OInt value_one;
      public OString tokkou;
    }

    [MessagePackObject(true)]
    public struct BuffValues
    {
      public ParamTypes param_type { get; set; }

      public BuffMethodTypes method_type { get; set; }

      public int value { get; set; }
    }
  }
}
