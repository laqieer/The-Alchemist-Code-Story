// Decompiled with JetBrains decompiler
// Type: SRPG.BaseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class BaseStatus
  {
    public static readonly short SHORT_PARAM_MAX = 30000;
    public static readonly short SHORT_PARAM_MIN = -30000;
    public StatusParam param = new StatusParam();
    public ElementParam element_assist = new ElementParam();
    public ElementParam element_resist = new ElementParam();
    public EnchantParam enchant_assist = new EnchantParam();
    public EnchantParam enchant_resist = new EnchantParam();
    public BattleBonusParam bonus = new BattleBonusParam();
    public TokkouParam tokkou = new TokkouParam();
    public TokkouParam tokubou = new TokkouParam();

    public BaseStatus()
    {
    }

    public BaseStatus(BaseStatus src) => src.CopyTo(this);

    [IgnoreMember]
    public OInt this[StatusTypes type]
    {
      get => this.param[type];
      set => this.param[type] = value;
    }

    [IgnoreMember]
    public OInt this[EnchantCategory category, EElement element]
    {
      get
      {
        return (OInt) (category != EnchantCategory.Assist ? this.element_resist[element] : this.element_assist[element]);
      }
      set
      {
        if (category == EnchantCategory.Assist)
          this.element_assist[element] = (short) (int) value;
        else
          this.element_resist[element] = (short) (int) value;
      }
    }

    [IgnoreMember]
    public OInt this[EnchantCategory category, EnchantTypes type]
    {
      get
      {
        return (OInt) (category != EnchantCategory.Assist ? this.enchant_resist[type] : this.enchant_assist[type]);
      }
      set
      {
        if (category == EnchantCategory.Assist)
          this.enchant_assist[type] = (short) (int) value;
        else
          this.enchant_resist[type] = (short) (int) value;
      }
    }

    [IgnoreMember]
    public OInt this[BattleBonus type]
    {
      get => (OInt) this.bonus[type];
      set => this.bonus[type] = (short) (int) value;
    }

    [IgnoreMember]
    public virtual int this[ParamTypes type]
    {
      get
      {
        switch (type)
        {
          case ParamTypes.Hp:
            return (int) this[StatusTypes.Hp];
          case ParamTypes.HpMax:
            return (int) this[StatusTypes.Hp];
          case ParamTypes.Mp:
            return (int) this[StatusTypes.Mp];
          case ParamTypes.MpIni:
            return (int) this[StatusTypes.MpIni];
          case ParamTypes.Atk:
            return (int) this[StatusTypes.Atk];
          case ParamTypes.Def:
            return (int) this[StatusTypes.Def];
          case ParamTypes.Mag:
            return (int) this[StatusTypes.Mag];
          case ParamTypes.Mnd:
            return (int) this[StatusTypes.Mnd];
          case ParamTypes.Rec:
            return (int) this[StatusTypes.Rec];
          case ParamTypes.Dex:
            return (int) this[StatusTypes.Dex];
          case ParamTypes.Spd:
            return (int) this[StatusTypes.Spd];
          case ParamTypes.Cri:
            return (int) this[StatusTypes.Cri];
          case ParamTypes.Luk:
            return (int) this[StatusTypes.Luk];
          case ParamTypes.Mov:
            return (int) this[StatusTypes.Mov];
          case ParamTypes.Jmp:
            return (int) this[StatusTypes.Jmp];
          case ParamTypes.EffectRange:
            return (int) this[BattleBonus.EffectRange];
          case ParamTypes.EffectScope:
            return (int) this[BattleBonus.EffectScope];
          case ParamTypes.EffectHeight:
            return (int) this[BattleBonus.EffectHeight];
          case ParamTypes.Assist_Fire:
            return (int) this[EnchantCategory.Assist, EElement.Fire];
          case ParamTypes.Assist_Water:
            return (int) this[EnchantCategory.Assist, EElement.Water];
          case ParamTypes.Assist_Wind:
            return (int) this[EnchantCategory.Assist, EElement.Wind];
          case ParamTypes.Assist_Thunder:
            return (int) this[EnchantCategory.Assist, EElement.Thunder];
          case ParamTypes.Assist_Shine:
            return (int) this[EnchantCategory.Assist, EElement.Shine];
          case ParamTypes.Assist_Dark:
            return (int) this[EnchantCategory.Assist, EElement.Dark];
          case ParamTypes.Assist_Poison:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Poison];
          case ParamTypes.Assist_Paralysed:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Paralysed];
          case ParamTypes.Assist_Stun:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Stun];
          case ParamTypes.Assist_Sleep:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Sleep];
          case ParamTypes.Assist_Charm:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Charm];
          case ParamTypes.Assist_Stone:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Stone];
          case ParamTypes.Assist_Blind:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Blind];
          case ParamTypes.Assist_DisableSkill:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DisableSkill];
          case ParamTypes.Assist_DisableMove:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DisableMove];
          case ParamTypes.Assist_DisableAttack:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DisableAttack];
          case ParamTypes.Assist_Zombie:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Zombie];
          case ParamTypes.Assist_DeathSentence:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DeathSentence];
          case ParamTypes.Assist_Berserk:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Berserk];
          case ParamTypes.Assist_Knockback:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Knockback];
          case ParamTypes.Assist_ResistBuff:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ResistBuff];
          case ParamTypes.Assist_ResistDebuff:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ResistDebuff];
          case ParamTypes.Assist_Stop:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Stop];
          case ParamTypes.Assist_Fast:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Fast];
          case ParamTypes.Assist_Slow:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Slow];
          case ParamTypes.Assist_AutoHeal:
            return (int) this[EnchantCategory.Assist, EnchantTypes.AutoHeal];
          case ParamTypes.Assist_Donsoku:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Donsoku];
          case ParamTypes.Assist_Rage:
            return (int) this[EnchantCategory.Assist, EnchantTypes.Rage];
          case ParamTypes.Assist_GoodSleep:
            return (int) this[EnchantCategory.Assist, EnchantTypes.GoodSleep];
          case ParamTypes.Resist_Fire:
            return (int) this[EnchantCategory.Resist, EElement.Fire];
          case ParamTypes.Resist_Water:
            return (int) this[EnchantCategory.Resist, EElement.Water];
          case ParamTypes.Resist_Wind:
            return (int) this[EnchantCategory.Resist, EElement.Wind];
          case ParamTypes.Resist_Thunder:
            return (int) this[EnchantCategory.Resist, EElement.Thunder];
          case ParamTypes.Resist_Shine:
            return (int) this[EnchantCategory.Resist, EElement.Shine];
          case ParamTypes.Resist_Dark:
            return (int) this[EnchantCategory.Resist, EElement.Dark];
          case ParamTypes.Resist_Poison:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Poison];
          case ParamTypes.Resist_Paralysed:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Paralysed];
          case ParamTypes.Resist_Stun:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Stun];
          case ParamTypes.Resist_Sleep:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Sleep];
          case ParamTypes.Resist_Charm:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Charm];
          case ParamTypes.Resist_Stone:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Stone];
          case ParamTypes.Resist_Blind:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Blind];
          case ParamTypes.Resist_DisableSkill:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DisableSkill];
          case ParamTypes.Resist_DisableMove:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DisableMove];
          case ParamTypes.Resist_DisableAttack:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DisableAttack];
          case ParamTypes.Resist_Zombie:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Zombie];
          case ParamTypes.Resist_DeathSentence:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DeathSentence];
          case ParamTypes.Resist_Berserk:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Berserk];
          case ParamTypes.Resist_Knockback:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Knockback];
          case ParamTypes.Resist_ResistBuff:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ResistBuff];
          case ParamTypes.Resist_ResistDebuff:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ResistDebuff];
          case ParamTypes.Resist_Stop:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Stop];
          case ParamTypes.Resist_Fast:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Fast];
          case ParamTypes.Resist_Slow:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Slow];
          case ParamTypes.Resist_AutoHeal:
            return (int) this[EnchantCategory.Resist, EnchantTypes.AutoHeal];
          case ParamTypes.Resist_Donsoku:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Donsoku];
          case ParamTypes.Resist_Rage:
            return (int) this[EnchantCategory.Resist, EnchantTypes.Rage];
          case ParamTypes.Resist_GoodSleep:
            return (int) this[EnchantCategory.Resist, EnchantTypes.GoodSleep];
          case ParamTypes.HitRate:
            return (int) this[BattleBonus.HitRate];
          case ParamTypes.AvoidRate:
            return (int) this[BattleBonus.AvoidRate];
          case ParamTypes.CriticalRate:
            return (int) this[BattleBonus.CriticalRate];
          case ParamTypes.GainJewel:
            return (int) this[BattleBonus.GainJewel];
          case ParamTypes.UsedJewelRate:
            return (int) this[BattleBonus.UsedJewelRate];
          case ParamTypes.ActionCount:
            return (int) this[BattleBonus.ActionCount];
          case ParamTypes.SlashAttack:
            return (int) this[BattleBonus.SlashAttack];
          case ParamTypes.PierceAttack:
            return (int) this[BattleBonus.PierceAttack];
          case ParamTypes.BlowAttack:
            return (int) this[BattleBonus.BlowAttack];
          case ParamTypes.ShotAttack:
            return (int) this[BattleBonus.ShotAttack];
          case ParamTypes.MagicAttack:
            return (int) this[BattleBonus.MagicAttack];
          case ParamTypes.ReactionAttack:
            return (int) this[BattleBonus.ReactionAttack];
          case ParamTypes.JumpAttack:
            return (int) this[BattleBonus.JumpAttack];
          case ParamTypes.GutsRate:
            return (int) this[BattleBonus.GutsRate];
          case ParamTypes.AutoJewel:
            return (int) this[BattleBonus.AutoJewel];
          case ParamTypes.ChargeTimeRate:
            return (int) this[BattleBonus.ChargeTimeRate];
          case ParamTypes.CastTimeRate:
            return (int) this[BattleBonus.CastTimeRate];
          case ParamTypes.BuffTurn:
            return (int) this[BattleBonus.BuffTurn];
          case ParamTypes.DebuffTurn:
            return (int) this[BattleBonus.DebuffTurn];
          case ParamTypes.CombinationRange:
            return (int) this[BattleBonus.CombinationRange];
          case ParamTypes.HpCostRate:
            return (int) this[BattleBonus.HpCostRate];
          case ParamTypes.SkillUseCount:
            return (int) this[BattleBonus.SkillUseCount];
          case ParamTypes.PoisonDamage:
            return (int) this[BattleBonus.PoisonDamage];
          case ParamTypes.PoisonTurn:
            return (int) this[BattleBonus.PoisonTurn];
          case ParamTypes.Assist_AutoJewel:
            return (int) this[EnchantCategory.Assist, EnchantTypes.AutoJewel];
          case ParamTypes.Resist_AutoJewel:
            return (int) this[EnchantCategory.Resist, EnchantTypes.AutoJewel];
          case ParamTypes.Assist_DisableHeal:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DisableHeal];
          case ParamTypes.Resist_DisableHeal:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DisableHeal];
          case ParamTypes.Resist_Slash:
            return (int) this[BattleBonus.Resist_Slash];
          case ParamTypes.Resist_Pierce:
            return (int) this[BattleBonus.Resist_Pierce];
          case ParamTypes.Resist_Blow:
            return (int) this[BattleBonus.Resist_Blow];
          case ParamTypes.Resist_Shot:
            return (int) this[BattleBonus.Resist_Shot];
          case ParamTypes.Resist_Magic:
            return (int) this[BattleBonus.Resist_Magic];
          case ParamTypes.Resist_Reaction:
            return (int) this[BattleBonus.Resist_Reaction];
          case ParamTypes.Resist_Jump:
            return (int) this[BattleBonus.Resist_Jump];
          case ParamTypes.Avoid_Slash:
            return (int) this[BattleBonus.Avoid_Slash];
          case ParamTypes.Avoid_Pierce:
            return (int) this[BattleBonus.Avoid_Pierce];
          case ParamTypes.Avoid_Blow:
            return (int) this[BattleBonus.Avoid_Blow];
          case ParamTypes.Avoid_Shot:
            return (int) this[BattleBonus.Avoid_Shot];
          case ParamTypes.Avoid_Magic:
            return (int) this[BattleBonus.Avoid_Magic];
          case ParamTypes.Avoid_Reaction:
            return (int) this[BattleBonus.Avoid_Reaction];
          case ParamTypes.Avoid_Jump:
            return (int) this[BattleBonus.Avoid_Jump];
          case ParamTypes.GainJewelRate:
            return (int) this[BattleBonus.GainJewelRate];
          case ParamTypes.UsedJewel:
            return (int) this[BattleBonus.UsedJewel];
          case ParamTypes.Assist_SingleAttack:
            return (int) this[EnchantCategory.Assist, EnchantTypes.SingleAttack];
          case ParamTypes.Assist_AreaAttack:
            return (int) this[EnchantCategory.Assist, EnchantTypes.AreaAttack];
          case ParamTypes.Resist_SingleAttack:
            return (int) this[EnchantCategory.Resist, EnchantTypes.SingleAttack];
          case ParamTypes.Resist_AreaAttack:
            return (int) this[EnchantCategory.Resist, EnchantTypes.AreaAttack];
          case ParamTypes.Assist_DecCT:
            return (int) this[EnchantCategory.Assist, EnchantTypes.DecCT];
          case ParamTypes.Assist_IncCT:
            return (int) this[EnchantCategory.Assist, EnchantTypes.IncCT];
          case ParamTypes.Resist_DecCT:
            return (int) this[EnchantCategory.Resist, EnchantTypes.DecCT];
          case ParamTypes.Resist_IncCT:
            return (int) this[EnchantCategory.Resist, EnchantTypes.IncCT];
          case ParamTypes.Assist_ESA_Fire:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Fire];
          case ParamTypes.Assist_ESA_Water:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Water];
          case ParamTypes.Assist_ESA_Wind:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Wind];
          case ParamTypes.Assist_ESA_Thunder:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Thunder];
          case ParamTypes.Assist_ESA_Shine:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Shine];
          case ParamTypes.Assist_ESA_Dark:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ESA_Dark];
          case ParamTypes.Resist_ESA_Fire:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Fire];
          case ParamTypes.Resist_ESA_Water:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Water];
          case ParamTypes.Resist_ESA_Wind:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Wind];
          case ParamTypes.Resist_ESA_Thunder:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Thunder];
          case ParamTypes.Resist_ESA_Shine:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Shine];
          case ParamTypes.Resist_ESA_Dark:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ESA_Dark];
          case ParamTypes.UnitDefenseFire:
            return (int) this[BattleBonus.UnitDefenseFire];
          case ParamTypes.UnitDefenseWater:
            return (int) this[BattleBonus.UnitDefenseWater];
          case ParamTypes.UnitDefenseWind:
            return (int) this[BattleBonus.UnitDefenseWind];
          case ParamTypes.UnitDefenseThunder:
            return (int) this[BattleBonus.UnitDefenseThunder];
          case ParamTypes.UnitDefenseShine:
            return (int) this[BattleBonus.UnitDefenseShine];
          case ParamTypes.UnitDefenseDark:
            return (int) this[BattleBonus.UnitDefenseDark];
          case ParamTypes.Assist_MaxDamageHp:
            return (int) this[EnchantCategory.Assist, EnchantTypes.MaxDamageHp];
          case ParamTypes.Assist_MaxDamageMp:
            return (int) this[EnchantCategory.Assist, EnchantTypes.MaxDamageMp];
          case ParamTypes.Resist_MaxDamageHp:
            return (int) this[EnchantCategory.Resist, EnchantTypes.MaxDamageHp];
          case ParamTypes.Resist_MaxDamageMp:
            return (int) this[EnchantCategory.Resist, EnchantTypes.MaxDamageMp];
          case ParamTypes.Tokkou:
            return 0;
          case ParamTypes.Assist_SideAttack:
            return (int) this[EnchantCategory.Assist, EnchantTypes.SideAttack];
          case ParamTypes.Assist_BackAttack:
            return (int) this[EnchantCategory.Assist, EnchantTypes.BackAttack];
          case ParamTypes.Resist_SideAttack:
            return (int) this[EnchantCategory.Resist, EnchantTypes.SideAttack];
          case ParamTypes.Resist_BackAttack:
            return (int) this[EnchantCategory.Resist, EnchantTypes.BackAttack];
          case ParamTypes.Assist_ObstReaction:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ObstReaction];
          case ParamTypes.Resist_ObstReaction:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ObstReaction];
          case ParamTypes.CriticalDamageRate:
            return (int) this[BattleBonus.CriticalDamageRate];
          case ParamTypes.NoDivAttack:
            return (int) this[BattleBonus.NoDivAttack];
          case ParamTypes.Resist_NoDiv:
            return (int) this[BattleBonus.Resist_NoDiv];
          case ParamTypes.Avoid_NoDiv:
            return (int) this[BattleBonus.Avoid_NoDiv];
          case ParamTypes.Resist_BuffHpMax:
            return (int) this[BattleBonus.Resist_BuffHpMax];
          case ParamTypes.Resist_BuffAtk:
            return (int) this[BattleBonus.Resist_BuffAtk];
          case ParamTypes.Resist_BuffDef:
            return (int) this[BattleBonus.Resist_BuffDef];
          case ParamTypes.Resist_BuffMag:
            return (int) this[BattleBonus.Resist_BuffMag];
          case ParamTypes.Resist_BuffMnd:
            return (int) this[BattleBonus.Resist_BuffMnd];
          case ParamTypes.Resist_BuffRec:
            return (int) this[BattleBonus.Resist_BuffRec];
          case ParamTypes.Resist_BuffDex:
            return (int) this[BattleBonus.Resist_BuffDex];
          case ParamTypes.Resist_BuffSpd:
            return (int) this[BattleBonus.Resist_BuffSpd];
          case ParamTypes.Resist_BuffCri:
            return (int) this[BattleBonus.Resist_BuffCri];
          case ParamTypes.Resist_BuffLuk:
            return (int) this[BattleBonus.Resist_BuffLuk];
          case ParamTypes.Resist_BuffMov:
            return (int) this[BattleBonus.Resist_BuffMov];
          case ParamTypes.Resist_BuffJmp:
            return (int) this[BattleBonus.Resist_BuffJmp];
          case ParamTypes.Resist_DebuffHpMax:
            return (int) this[BattleBonus.Resist_DebuffHpMax];
          case ParamTypes.Resist_DebuffAtk:
            return (int) this[BattleBonus.Resist_DebuffAtk];
          case ParamTypes.Resist_DebuffDef:
            return (int) this[BattleBonus.Resist_DebuffDef];
          case ParamTypes.Resist_DebuffMag:
            return (int) this[BattleBonus.Resist_DebuffMag];
          case ParamTypes.Resist_DebuffMnd:
            return (int) this[BattleBonus.Resist_DebuffMnd];
          case ParamTypes.Resist_DebuffRec:
            return (int) this[BattleBonus.Resist_DebuffRec];
          case ParamTypes.Resist_DebuffDex:
            return (int) this[BattleBonus.Resist_DebuffDex];
          case ParamTypes.Resist_DebuffSpd:
            return (int) this[BattleBonus.Resist_DebuffSpd];
          case ParamTypes.Resist_DebuffCri:
            return (int) this[BattleBonus.Resist_DebuffCri];
          case ParamTypes.Resist_DebuffLuk:
            return (int) this[BattleBonus.Resist_DebuffLuk];
          case ParamTypes.Resist_DebuffMov:
            return (int) this[BattleBonus.Resist_DebuffMov];
          case ParamTypes.Resist_DebuffJmp:
            return (int) this[BattleBonus.Resist_DebuffJmp];
          case ParamTypes.Assist_ForcedTargeting:
            return (int) this[EnchantCategory.Assist, EnchantTypes.ForcedTargeting];
          case ParamTypes.Resist_ForcedTargeting:
            return (int) this[EnchantCategory.Resist, EnchantTypes.ForcedTargeting];
          case ParamTypes.Tokubou:
            return 0;
          default:
            return 0;
        }
      }
    }

    public void Clear()
    {
      this.param.Clear();
      this.element_assist.Clear();
      this.element_resist.Clear();
      this.enchant_assist.Clear();
      this.enchant_resist.Clear();
      this.bonus.Clear();
      this.tokkou.Clear();
      this.tokubou.Clear();
    }

    public void CopyTo(BaseStatus dsc)
    {
      this.param.CopyTo(dsc.param);
      this.element_assist.CopyTo(dsc.element_assist);
      this.element_resist.CopyTo(dsc.element_resist);
      this.enchant_assist.CopyTo(dsc.enchant_assist);
      this.enchant_resist.CopyTo(dsc.enchant_resist);
      this.bonus.CopyTo(dsc.bonus);
      this.tokkou.CopyTo(dsc.tokkou);
      this.tokubou.CopyTo(dsc.tokubou);
    }

    public void Add(BaseStatus src)
    {
      this.param.Add(src.param);
      this.element_assist.Add(src.element_assist);
      this.element_resist.Add(src.element_resist);
      this.enchant_assist.Add(src.enchant_assist);
      this.enchant_resist.Add(src.enchant_resist);
      this.bonus.Add(src.bonus);
      this.tokkou.Add(src.tokkou);
      this.tokubou.Add(src.tokubou);
    }

    public void Sub(BaseStatus src)
    {
      this.param.Sub(src.param);
      this.element_assist.Sub(src.element_assist);
      this.element_resist.Sub(src.element_resist);
      this.enchant_assist.Sub(src.enchant_assist);
      this.enchant_resist.Sub(src.enchant_resist);
      this.bonus.Sub(src.bonus);
    }

    public void AddRate(BaseStatus src)
    {
      this.param.AddRate(src.param);
      this.element_assist.AddRate(src.element_assist);
      this.element_resist.AddRate(src.element_resist);
      this.enchant_assist.AddRate(src.enchant_assist);
      this.enchant_resist.AddRate(src.enchant_resist);
      this.bonus.AddRate(src.bonus);
    }

    public void AddRate(StatusParam src) => this.param.AddRate(src);

    public void SubRateRoundDown(long percent)
    {
      this.param.SubRateRoundDown(percent);
      this.element_assist.SubRateRoundDown(percent);
      this.element_resist.SubRateRoundDown(percent);
      this.enchant_assist.SubRateRoundDown(percent);
      this.enchant_resist.SubRateRoundDown(percent);
      this.bonus.SubRateRoundDown(percent);
      this.tokkou.SubRateRoundDown(percent);
      this.tokubou.SubRateRoundDown(percent);
    }

    public void ReplaceHighest(BaseStatus comp)
    {
      this.param.ReplceHighest(comp.param);
      this.element_assist.ReplceHighest(comp.element_assist);
      this.element_resist.ReplceHighest(comp.element_resist);
      this.enchant_assist.ReplceHighest(comp.enchant_assist);
      this.enchant_resist.ReplceHighest(comp.enchant_resist);
      this.bonus.ReplceHighest(comp.bonus);
      this.tokkou.ReplceHighest(comp.tokkou);
      this.tokubou.ReplceHighest(comp.tokubou);
    }

    public void ReplaceLowest(BaseStatus comp)
    {
      this.param.ReplceLowest(comp.param);
      this.element_assist.ReplceLowest(comp.element_assist);
      this.element_resist.ReplceLowest(comp.element_resist);
      this.enchant_assist.ReplceLowest(comp.enchant_assist);
      this.enchant_resist.ReplceLowest(comp.enchant_resist);
      this.bonus.ReplceLowest(comp.bonus);
      this.tokkou.ReplceLowest(comp.tokkou);
      this.tokubou.ReplceLowest(comp.tokubou);
    }

    public void ChoiceHighest(BaseStatus scale, BaseStatus base_status)
    {
      this.param.ChoiceHighest(scale.param, base_status.param);
      this.element_assist.ChoiceHighest(scale.element_assist, base_status.element_assist);
      this.element_resist.ChoiceHighest(scale.element_resist, base_status.element_resist);
      this.enchant_assist.ChoiceHighest(scale.enchant_assist, base_status.enchant_assist);
      this.enchant_resist.ChoiceHighest(scale.enchant_resist, base_status.enchant_resist);
      this.bonus.ChoiceHighest(scale.bonus, base_status.bonus);
    }

    public void ChoiceLowest(BaseStatus scale, BaseStatus base_status)
    {
      this.param.ChoiceLowest(scale.param, base_status.param);
      this.element_assist.ChoiceLowest(scale.element_assist, base_status.element_assist);
      this.element_resist.ChoiceLowest(scale.element_resist, base_status.element_resist);
      this.enchant_assist.ChoiceLowest(scale.enchant_assist, base_status.enchant_assist);
      this.enchant_resist.ChoiceLowest(scale.enchant_resist, base_status.enchant_resist);
      this.bonus.ChoiceLowest(scale.bonus, base_status.bonus);
    }

    public void AddConvRate(BaseStatus scale, BaseStatus base_status)
    {
      this.param.AddConvRate(scale.param, base_status.param);
      this.element_assist.AddConvRate(scale.element_assist, base_status.element_assist);
      this.element_resist.AddConvRate(scale.element_resist, base_status.element_resist);
      this.enchant_assist.AddConvRate(scale.enchant_assist, base_status.enchant_assist);
      this.enchant_resist.AddConvRate(scale.enchant_resist, base_status.enchant_resist);
      this.bonus.AddConvRate(scale.bonus, base_status.bonus);
    }

    public void SubConvRate(BaseStatus scale, BaseStatus base_status)
    {
      this.param.SubConvRate(scale.param, base_status.param);
      this.element_assist.SubConvRate(scale.element_assist, base_status.element_assist);
      this.element_resist.SubConvRate(scale.element_resist, base_status.element_resist);
      this.enchant_assist.SubConvRate(scale.enchant_assist, base_status.enchant_assist);
      this.enchant_resist.SubConvRate(scale.enchant_resist, base_status.enchant_resist);
      this.bonus.SubConvRate(scale.bonus, base_status.bonus);
    }

    public void Mul(int mul_val)
    {
      this.param.Mul(mul_val);
      this.element_assist.Mul(mul_val);
      this.element_resist.Mul(mul_val);
      this.enchant_assist.Mul(mul_val);
      this.enchant_resist.Mul(mul_val);
      this.bonus.Mul(mul_val);
    }

    public void Div(int div_val)
    {
      this.param.Div(div_val);
      this.element_assist.Div(div_val);
      this.element_resist.Div(div_val);
      this.enchant_assist.Div(div_val);
      this.enchant_resist.Div(div_val);
      this.bonus.Div(div_val);
    }

    public void Swap(BaseStatus src, bool is_rev = false)
    {
      this.param.Swap(src.param, is_rev);
      this.element_assist.Swap(src.element_assist, is_rev);
      this.element_resist.Swap(src.element_resist, is_rev);
      this.enchant_assist.Swap(src.enchant_assist, is_rev);
      this.enchant_resist.Swap(src.enchant_resist, is_rev);
      this.bonus.Swap(src.bonus, is_rev);
    }
  }
}
