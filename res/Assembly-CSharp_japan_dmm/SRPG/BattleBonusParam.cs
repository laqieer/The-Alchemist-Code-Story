// Decompiled with JetBrains decompiler
// Type: SRPG.BattleBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class BattleBonusParam
  {
    public static readonly int MAX_BATTLE_BONUS = Enum.GetNames(typeof (BattleBonus)).Length;
    public short[] values = new short[BattleBonusParam.MAX_BATTLE_BONUS];
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[77]
    {
      ParamTypes.EffectRange,
      ParamTypes.EffectScope,
      ParamTypes.EffectHeight,
      ParamTypes.HitRate,
      ParamTypes.AvoidRate,
      ParamTypes.CriticalRate,
      ParamTypes.SlashAttack,
      ParamTypes.PierceAttack,
      ParamTypes.BlowAttack,
      ParamTypes.ShotAttack,
      ParamTypes.MagicAttack,
      ParamTypes.ReactionAttack,
      ParamTypes.JumpAttack,
      ParamTypes.GainJewel,
      ParamTypes.UsedJewelRate,
      ParamTypes.ActionCount,
      ParamTypes.GutsRate,
      ParamTypes.AutoJewel,
      ParamTypes.ChargeTimeRate,
      ParamTypes.CastTimeRate,
      ParamTypes.BuffTurn,
      ParamTypes.DebuffTurn,
      ParamTypes.CombinationRange,
      ParamTypes.HpCostRate,
      ParamTypes.SkillUseCount,
      ParamTypes.PoisonDamage,
      ParamTypes.PoisonTurn,
      ParamTypes.Resist_Slash,
      ParamTypes.Resist_Pierce,
      ParamTypes.Resist_Blow,
      ParamTypes.Resist_Shot,
      ParamTypes.Resist_Magic,
      ParamTypes.Resist_Reaction,
      ParamTypes.Resist_Jump,
      ParamTypes.Avoid_Slash,
      ParamTypes.Avoid_Pierce,
      ParamTypes.Avoid_Blow,
      ParamTypes.Avoid_Shot,
      ParamTypes.Avoid_Magic,
      ParamTypes.Avoid_Reaction,
      ParamTypes.Avoid_Jump,
      ParamTypes.GainJewelRate,
      ParamTypes.UsedJewel,
      ParamTypes.UnitDefenseFire,
      ParamTypes.UnitDefenseWater,
      ParamTypes.UnitDefenseWind,
      ParamTypes.UnitDefenseThunder,
      ParamTypes.UnitDefenseShine,
      ParamTypes.UnitDefenseDark,
      ParamTypes.CriticalDamageRate,
      ParamTypes.NoDivAttack,
      ParamTypes.Resist_NoDiv,
      ParamTypes.Avoid_NoDiv,
      ParamTypes.Resist_BuffHpMax,
      ParamTypes.Resist_BuffAtk,
      ParamTypes.Resist_BuffDef,
      ParamTypes.Resist_BuffMag,
      ParamTypes.Resist_BuffMnd,
      ParamTypes.Resist_BuffRec,
      ParamTypes.Resist_BuffDex,
      ParamTypes.Resist_BuffSpd,
      ParamTypes.Resist_BuffCri,
      ParamTypes.Resist_BuffLuk,
      ParamTypes.Resist_BuffMov,
      ParamTypes.Resist_BuffJmp,
      ParamTypes.Resist_DebuffHpMax,
      ParamTypes.Resist_DebuffAtk,
      ParamTypes.Resist_DebuffDef,
      ParamTypes.Resist_DebuffMag,
      ParamTypes.Resist_DebuffMnd,
      ParamTypes.Resist_DebuffRec,
      ParamTypes.Resist_DebuffDex,
      ParamTypes.Resist_DebuffSpd,
      ParamTypes.Resist_DebuffCri,
      ParamTypes.Resist_DebuffLuk,
      ParamTypes.Resist_DebuffMov,
      ParamTypes.Resist_DebuffJmp
    };

    [IgnoreMember]
    public short this[BattleBonus type]
    {
      get => this.values[(int) type];
      set => this.values[(int) type] = value;
    }

    public void Clear() => Array.Clear((Array) this.values, 0, this.values.Length);

    public void CopyTo(BattleBonusParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(BattleBonusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] += src.values[index];
      }
    }

    public void Sub(BattleBonusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] - (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] - (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] -= src.values[index];
      }
    }

    public void AddRate(BattleBonusParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] += (short) ((int) this.values[index] * (int) src.values[index] / 100);
      }
    }

    public void SubRateRoundDown(long percent)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        int num = GameUtility.CalcSubRateRoundDown((long) this.values[index], percent);
        this.values[index] = (short) Mathf.Clamp(num, (int) BaseStatus.SHORT_PARAM_MIN, (int) BaseStatus.SHORT_PARAM_MAX);
      }
    }

    public void ReplceHighest(BattleBonusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(BattleBonusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ChoiceHighest(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void ChoiceLowest(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void AddConvRate(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] += (short) ((int) scale.values[index] * (int) base_status.values[index] / 100);
    }

    public void SubConvRate(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] -= (short) ((int) scale.values[index] * (int) base_status.values[index] / 100);
    }

    public void Mul(int mul_val)
    {
      if (mul_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] = (short) ((int) this.values[index] * mul_val);
    }

    public void Div(int div_val)
    {
      if (div_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] = (short) ((int) this.values[index] / div_val);
    }

    public void Swap(BattleBonusParam src, bool is_rev)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        GameUtility.swap<short>(ref this.values[index], ref src.values[index]);
        if (is_rev)
        {
          this.values[index] *= (short) -1;
          src.values[index] *= (short) -1;
        }
      }
    }

    public ParamTypes GetParamTypes(int index) => BattleBonusParam.ConvertParamTypes[index];
  }
}
