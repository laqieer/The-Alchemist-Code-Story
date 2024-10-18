// Decompiled with JetBrains decompiler
// Type: SRPG.EnchantParam
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
  public class EnchantParam
  {
    public static readonly int MAX_ENCHANT = Enum.GetNames(typeof (EnchantTypes)).Length;
    public short[] values = new short[EnchantParam.MAX_ENCHANT];
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[41]
    {
      ParamTypes.Assist_Poison,
      ParamTypes.Assist_Paralysed,
      ParamTypes.Assist_Stun,
      ParamTypes.Assist_Sleep,
      ParamTypes.Assist_Charm,
      ParamTypes.Assist_Stone,
      ParamTypes.Assist_Blind,
      ParamTypes.Assist_DisableSkill,
      ParamTypes.Assist_DisableMove,
      ParamTypes.Assist_DisableAttack,
      ParamTypes.Assist_Zombie,
      ParamTypes.Assist_DeathSentence,
      ParamTypes.Assist_Berserk,
      ParamTypes.Assist_Knockback,
      ParamTypes.Assist_ResistBuff,
      ParamTypes.Assist_ResistDebuff,
      ParamTypes.Assist_Stun,
      ParamTypes.Assist_Fast,
      ParamTypes.Assist_Slow,
      ParamTypes.Assist_AutoHeal,
      ParamTypes.Assist_Donsoku,
      ParamTypes.Assist_Rage,
      ParamTypes.Assist_GoodSleep,
      ParamTypes.Assist_AutoJewel,
      ParamTypes.Assist_DisableHeal,
      ParamTypes.Assist_SingleAttack,
      ParamTypes.Assist_AreaAttack,
      ParamTypes.Assist_DecCT,
      ParamTypes.Assist_IncCT,
      ParamTypes.Assist_ESA_Fire,
      ParamTypes.Assist_ESA_Water,
      ParamTypes.Assist_ESA_Wind,
      ParamTypes.Assist_ESA_Thunder,
      ParamTypes.Assist_ESA_Shine,
      ParamTypes.Assist_ESA_Dark,
      ParamTypes.Assist_MaxDamageHp,
      ParamTypes.Assist_MaxDamageMp,
      ParamTypes.Assist_SideAttack,
      ParamTypes.Assist_BackAttack,
      ParamTypes.Assist_ObstReaction,
      ParamTypes.Assist_ForcedTargeting
    };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[41]
    {
      ParamTypes.Resist_Poison,
      ParamTypes.Resist_Paralysed,
      ParamTypes.Resist_Stun,
      ParamTypes.Resist_Sleep,
      ParamTypes.Resist_Charm,
      ParamTypes.Resist_Stone,
      ParamTypes.Resist_Blind,
      ParamTypes.Resist_DisableSkill,
      ParamTypes.Resist_DisableMove,
      ParamTypes.Resist_DisableAttack,
      ParamTypes.Resist_Zombie,
      ParamTypes.Resist_DeathSentence,
      ParamTypes.Resist_Berserk,
      ParamTypes.Resist_Knockback,
      ParamTypes.Resist_ResistBuff,
      ParamTypes.Resist_ResistDebuff,
      ParamTypes.Resist_Stun,
      ParamTypes.Resist_Fast,
      ParamTypes.Resist_Slow,
      ParamTypes.Resist_AutoHeal,
      ParamTypes.Resist_Donsoku,
      ParamTypes.Resist_Rage,
      ParamTypes.Resist_GoodSleep,
      ParamTypes.Resist_AutoJewel,
      ParamTypes.Resist_DisableHeal,
      ParamTypes.Resist_SingleAttack,
      ParamTypes.Resist_AreaAttack,
      ParamTypes.Resist_DecCT,
      ParamTypes.Resist_IncCT,
      ParamTypes.Resist_ESA_Fire,
      ParamTypes.Resist_ESA_Water,
      ParamTypes.Resist_ESA_Wind,
      ParamTypes.Resist_ESA_Thunder,
      ParamTypes.Resist_ESA_Shine,
      ParamTypes.Resist_ESA_Dark,
      ParamTypes.Resist_MaxDamageHp,
      ParamTypes.Resist_MaxDamageMp,
      ParamTypes.Resist_SideAttack,
      ParamTypes.Resist_BackAttack,
      ParamTypes.Resist_ObstReaction,
      ParamTypes.Resist_ForcedTargeting
    };

    [IgnoreMember]
    public short this[EnchantTypes type]
    {
      get => this.values[(int) type];
      set => this.values[(int) type] = value;
    }

    public short poison
    {
      get => this.values[0];
      set => this.values[0] = value;
    }

    public short paralyse
    {
      get => this.values[1];
      set => this.values[1] = value;
    }

    public short stun
    {
      get => this.values[2];
      set => this.values[2] = value;
    }

    public short sleep
    {
      get => this.values[3];
      set => this.values[3] = value;
    }

    public short charm
    {
      get => this.values[4];
      set => this.values[4] = value;
    }

    public short stone
    {
      get => this.values[5];
      set => this.values[5] = value;
    }

    public short blind
    {
      get => this.values[6];
      set => this.values[6] = value;
    }

    public short notskl
    {
      get => this.values[7];
      set => this.values[7] = value;
    }

    public short notmov
    {
      get => this.values[8];
      set => this.values[8] = value;
    }

    public short notatk
    {
      get => this.values[9];
      set => this.values[9] = value;
    }

    public short zombie
    {
      get => this.values[10];
      set => this.values[10] = value;
    }

    public short death
    {
      get => this.values[11];
      set => this.values[11] = value;
    }

    public short berserk
    {
      get => this.values[12];
      set => this.values[12] = value;
    }

    public short knockback
    {
      get => this.values[13];
      set => this.values[13] = value;
    }

    public short resist_buff
    {
      get => this.values[14];
      set => this.values[14] = value;
    }

    public short resist_debuff
    {
      get => this.values[15];
      set => this.values[15] = value;
    }

    public short stop
    {
      get => this.values[16];
      set => this.values[16] = value;
    }

    public short fast
    {
      get => this.values[17];
      set => this.values[17] = value;
    }

    public short slow
    {
      get => this.values[18];
      set => this.values[18] = value;
    }

    public short auto_heal
    {
      get => this.values[19];
      set => this.values[19] = value;
    }

    public short donsoku
    {
      get => this.values[20];
      set => this.values[20] = value;
    }

    public short rage
    {
      get => this.values[21];
      set => this.values[21] = value;
    }

    public short good_sleep
    {
      get => this.values[22];
      set => this.values[22] = value;
    }

    public short auto_jewel
    {
      get => this.values[23];
      set => this.values[23] = value;
    }

    public short notheal
    {
      get => this.values[24];
      set => this.values[24] = value;
    }

    public short single_attack
    {
      get => this.values[25];
      set => this.values[25] = value;
    }

    public short area_attack
    {
      get => this.values[26];
      set => this.values[26] = value;
    }

    public short dec_ct
    {
      get => this.values[27];
      set => this.values[27] = value;
    }

    public short inc_ct
    {
      get => this.values[28];
      set => this.values[28] = value;
    }

    public short esa_fire
    {
      get => this.values[29];
      set => this.values[29] = value;
    }

    public short esa_water
    {
      get => this.values[30];
      set => this.values[30] = value;
    }

    public short esa_wind
    {
      get => this.values[31];
      set => this.values[31] = value;
    }

    public short esa_thunder
    {
      get => this.values[32];
      set => this.values[32] = value;
    }

    public short esa_shine
    {
      get => this.values[33];
      set => this.values[33] = value;
    }

    public short esa_dark
    {
      get => this.values[34];
      set => this.values[34] = value;
    }

    public short max_damage_hp
    {
      get => this.values[35];
      set => this.values[35] = value;
    }

    public short max_damage_mp
    {
      get => this.values[36];
      set => this.values[36] = value;
    }

    public short side_attack
    {
      get => this.values[37];
      set => this.values[37] = value;
    }

    public short back_attack
    {
      get => this.values[38];
      set => this.values[38] = value;
    }

    public short obst_reaction
    {
      get => this.values[39];
      set => this.values[39] = value;
    }

    public short forced_targeting
    {
      get => this.values[40];
      set => this.values[40] = value;
    }

    [IgnoreMember]
    public short this[EUnitCondition condition]
    {
      get
      {
        if (condition >= EUnitCondition.Poison && condition <= EUnitCondition.Sleep)
        {
          switch (condition)
          {
            case EUnitCondition.Poison:
              return this.poison;
            case EUnitCondition.Paralysed:
              return this.paralyse;
            case EUnitCondition.Stun:
              return this.stun;
            case EUnitCondition.Sleep:
              return this.sleep;
          }
        }
        switch (condition)
        {
          case EUnitCondition.Charm:
            return this.charm;
          case EUnitCondition.Stone:
            return this.stone;
          case EUnitCondition.Blindness:
            return this.blind;
          case EUnitCondition.DisableSkill:
            return this.notskl;
          case EUnitCondition.DisableMove:
            return this.notmov;
          case EUnitCondition.DisableAttack:
            return this.notatk;
          case EUnitCondition.Zombie:
            return this.zombie;
          case EUnitCondition.DeathSentence:
            return this.death;
          case EUnitCondition.Berserk:
            return this.berserk;
          case EUnitCondition.Stop:
            return this.stop;
          case EUnitCondition.Fast:
            return this.fast;
          case EUnitCondition.Slow:
            return this.slow;
          case EUnitCondition.AutoHeal:
            return this.auto_heal;
          case EUnitCondition.Donsoku:
            return this.donsoku;
          case EUnitCondition.Rage:
            return this.rage;
          case EUnitCondition.GoodSleep:
            return this.good_sleep;
          case EUnitCondition.AutoJewel:
            return this.auto_jewel;
          case EUnitCondition.DisableHeal:
            return this.notheal;
          default:
            return 0;
        }
      }
      set
      {
        if (condition >= EUnitCondition.Poison && condition <= EUnitCondition.Sleep)
        {
          switch (condition)
          {
            case EUnitCondition.Poison:
              this.poison = value;
              return;
            case EUnitCondition.Paralysed:
              this.paralyse = value;
              return;
            case EUnitCondition.Stun:
              this.stun = value;
              return;
            case EUnitCondition.Sleep:
              this.sleep = value;
              return;
          }
        }
        switch (condition)
        {
          case EUnitCondition.Charm:
            this.charm = value;
            break;
          case EUnitCondition.Stone:
            this.stone = value;
            break;
          case EUnitCondition.Blindness:
            this.blind = value;
            break;
          case EUnitCondition.DisableSkill:
            this.notskl = value;
            break;
          case EUnitCondition.DisableMove:
            this.notmov = value;
            break;
          case EUnitCondition.DisableAttack:
            this.notatk = value;
            break;
          case EUnitCondition.Zombie:
            this.zombie = value;
            break;
          case EUnitCondition.DeathSentence:
            this.death = value;
            break;
          case EUnitCondition.Berserk:
            this.berserk = value;
            break;
          case EUnitCondition.Stop:
            this.stop = value;
            break;
          case EUnitCondition.Fast:
            this.fast = value;
            break;
          case EUnitCondition.Slow:
            this.slow = value;
            break;
          case EUnitCondition.AutoHeal:
            this.auto_heal = value;
            break;
          case EUnitCondition.Donsoku:
            this.donsoku = value;
            break;
          case EUnitCondition.Rage:
            this.rage = value;
            break;
          case EUnitCondition.GoodSleep:
            this.good_sleep = value;
            break;
          case EUnitCondition.AutoJewel:
            this.auto_jewel = value;
            break;
          case EUnitCondition.DisableHeal:
            this.notheal = value;
            break;
        }
      }
    }

    public void Clear() => Array.Clear((Array) this.values, 0, this.values.Length);

    public void CopyTo(EnchantParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(EnchantParam src)
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

    public void Sub(EnchantParam src)
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

    public void AddRate(EnchantParam src)
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

    public void ReplceHighest(EnchantParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(EnchantParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ChoiceHighest(EnchantParam scale, EnchantParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void ChoiceLowest(EnchantParam scale, EnchantParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void AddConvRate(EnchantParam scale, EnchantParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] += (short) ((int) scale.values[index] * (int) base_status.values[index] / 100);
    }

    public void SubConvRate(EnchantParam scale, EnchantParam base_status)
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

    public void Swap(EnchantParam src, bool is_rev)
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

    public ParamTypes GetAssistParamTypes(int index) => EnchantParam.ConvertAssistParamTypes[index];

    public ParamTypes GetResistParamTypes(int index) => EnchantParam.ConvertResistParamTypes[index];
  }
}
