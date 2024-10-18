// Decompiled with JetBrains decompiler
// Type: SRPG.SkillData
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
  public class SkillData
  {
    private SkillParam mSkillParam;
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    private OInt mCastSpeed;
    private OInt mEffectRate;
    private OInt mEffectValue;
    private OInt mEffectRange;
    private OInt mElementValue;
    private OInt mControlDamageRate;
    private OInt mControlDamageValue;
    private OInt mControlChargeTimeRate;
    private OInt mControlChargeTimeValue;
    private OInt mShieldTurn;
    private OInt mShieldValue;
    public BuffEffect mTargetBuffEffect;
    public CondEffect mTargetCondEffect;
    public BuffEffect mSelfBuffEffect;
    public CondEffect mSelfCondEffect;
    private OInt mUseRate;
    private SkillLockCondition mUseCondition;
    private bool mCheckCount;
    private OBool mIsCollabo;
    private string mReplaceSkillId;
    public string m_BaseSkillIname;
    private AbilityData m_OwnerAbility;
    private SkillDeriveParam m_SkillDeriveParam;
    private ConceptCardData m_ConceptCardData;
    private int mProtectValue;
    private bool m_IsDecreaseEffectOnSubSlot;
    private DependStateSpcEffParam mDependStateSpcEffParam;
    private DependStateSpcEffParam mDependStateSpcEffParamSelf;
    private const int ANTI_SHIELD_RATE_MAX = 100;
    private short mAntiShieldIgnoreRate = -1;
    private short mAntiShieldDestroyRate = -1;

    public SkillParam SkillParam => this.mSkillParam;

    public string SkillID => this.mSkillParam != null ? this.mSkillParam.iname : (string) null;

    public int Rank => (int) this.mRank;

    public string Name => this.mSkillParam.name;

    public ESkillType SkillType => this.mSkillParam.type;

    public ESkillTarget Target => this.mSkillParam.target;

    public ESkillTiming Timing => this.mSkillParam.timing;

    public ESkillCondition Condition => this.mSkillParam.condition;

    public int Cost => this.SkillParam.cost;

    public ELineType LineType => this.mSkillParam.line_type;

    public int EnableAttackGridHeight => this.mSkillParam.effect_height;

    public int RangeMin => this.mSkillParam.range_min;

    public int RangeMax => this.mSkillParam.range_max;

    public int Scope => this.mSkillParam.scope;

    public int HpCostRate => this.mSkillParam.hp_cost_rate;

    public ECastTypes CastType => this.mSkillParam.cast_type;

    public OInt CastSpeed => this.mCastSpeed;

    public SkillEffectTypes EffectType => this.mSkillParam.effect_type;

    public OInt EffectRate => this.mEffectRate;

    public OInt EffectValue => this.mEffectValue;

    public OInt EffectRange => this.mEffectRange;

    public OInt EffectHpMaxRate => (OInt) this.mSkillParam.effect_hprate;

    public OInt EffectGemsMaxRate => (OInt) this.mSkillParam.effect_mprate;

    public SkillParamCalcTypes EffectCalcType => this.mSkillParam.effect_calc;

    public EElement ElementType => this.mSkillParam.element_type;

    public OInt ElementValue => this.mElementValue;

    public AttackTypes AttackType => this.mSkillParam.attack_type;

    public AttackDetailTypes AttackDetailType => this.mSkillParam.attack_detail;

    public int BackAttackDefenseDownRate => this.mSkillParam.back_defrate;

    public int SideAttackDefenseDownRate => this.mSkillParam.side_defrate;

    public DamageTypes ReactionDamageType => this.mSkillParam.reaction_damage_type;

    public int DamageAbsorbRate => this.mSkillParam.absorb_damage_rate;

    public OInt ControlDamageRate => this.mControlDamageRate;

    public OInt ControlDamageValue => this.mControlDamageValue;

    public SkillParamCalcTypes ControlDamageCalcType => this.mSkillParam.control_damage_calc;

    public OInt ControlChargeTimeRate => this.mControlChargeTimeRate;

    public OInt ControlChargeTimeValue => this.mControlChargeTimeValue;

    public SkillParamCalcTypes ControlChargeTimeCalcType => this.mSkillParam.control_ct_calc;

    public ShieldTypes ShieldType => this.mSkillParam.shield_type;

    public DamageTypes ShieldDamageType => this.mSkillParam.shield_damage_type;

    public OInt ShieldTurn => this.mShieldTurn;

    public OInt ShieldValue => this.mShieldValue;

    public OInt UseRate
    {
      get => this.mUseRate;
      set => this.mUseRate = value;
    }

    public SkillLockCondition UseCondition
    {
      get => this.mUseCondition;
      set
      {
        if (value == null)
          return;
        if (this.mUseCondition == null)
          this.mUseCondition = new SkillLockCondition();
        value.CopyTo(this.mUseCondition);
      }
    }

    public bool CheckCount
    {
      get => this.mCheckCount;
      set => this.mCheckCount = value;
    }

    public int DuplicateCount => this.mSkillParam.DuplicateCount;

    public OBool IsCollabo
    {
      get => this.mIsCollabo;
      set => this.mIsCollabo = value;
    }

    public string ReplaceSkillId
    {
      get => this.mReplaceSkillId;
      set => this.mReplaceSkillId = value;
    }

    public eTeleportType TeleportType => this.mSkillParam.TeleportType;

    public ESkillTarget TeleportTarget => this.mSkillParam.TeleportTarget;

    public int TeleportHeight => this.mSkillParam.TeleportHeight;

    public bool TeleportIsMove => this.mSkillParam.TeleportIsMove;

    public eTeleportSkillPos TeleportSkillPos => this.mSkillParam.TeleportSkillPos;

    public OInt KnockBackRate => (OInt) this.mSkillParam.KnockBackRate;

    public OInt KnockBackVal => (OInt) this.mSkillParam.KnockBackVal;

    public eKnockBackDir KnockBackDir => this.mSkillParam.KnockBackDir;

    public eKnockBackDs KnockBackDs => this.mSkillParam.KnockBackDs;

    public int WeatherRate => this.mSkillParam.WeatherRate;

    public string WeatherId => this.mSkillParam.WeatherId;

    public int ElementSpcAtkRate => this.mSkillParam.ElementSpcAtkRate;

    public int MaxDamageValue => this.mSkillParam.MaxDamageValue;

    public string CutInConceptCardId => this.mSkillParam.CutInConceptCardId;

    public int JumpSpcAtkRate => this.mSkillParam.JumpSpcAtkRate;

    public AbilityData OwnerAbiliy => this.m_OwnerAbility;

    public bool IsDerivedSkill => !string.IsNullOrEmpty(this.m_BaseSkillIname);

    public SkillDeriveParam DeriveParam => this.m_SkillDeriveParam;

    public bool IsCreatedByConceptCard => this.m_ConceptCardData != null;

    public ConceptCardData ConceptCard => this.m_ConceptCardData;

    public bool IsDecreaseEffectOnSubSlot => this.m_IsDecreaseEffectOnSubSlot;

    public int DecreaseEffectRate
    {
      get => !this.IsCreatedByConceptCard ? 0 : this.m_ConceptCardData.CurrentDecreaseEffectRate;
    }

    public ProtectSkillParam ProtectSkill
    {
      get => this.mSkillParam != null ? this.mSkillParam.ProtectSkill : (ProtectSkillParam) null;
    }

    public ProtectTypes ProtectType
    {
      get
      {
        return this.mSkillParam != null && this.mSkillParam.ProtectSkill != null ? this.mSkillParam.ProtectSkill.Type : ProtectTypes.None;
      }
    }

    public DamageTypes ProtectDamageType
    {
      get
      {
        return this.mSkillParam != null && this.mSkillParam.ProtectSkill != null ? this.mSkillParam.ProtectSkill.DamageType : DamageTypes.None;
      }
    }

    public OInt ProtectRange
    {
      get
      {
        return this.mSkillParam != null ? (OInt) (this.mSkillParam.ProtectSkill == null ? 0 : this.mSkillParam.ProtectSkill.Range) : (OInt) 0;
      }
    }

    public OInt ProtectHeight
    {
      get
      {
        return this.mSkillParam != null ? (OInt) (this.mSkillParam.ProtectSkill == null ? 0 : this.mSkillParam.ProtectSkill.Height) : (OInt) 0;
      }
    }

    public int ProtectValue => this.mProtectValue;

    public void Setup(
      string iname,
      int rank,
      int rankcap = 1,
      MasterParam master = null,
      ConceptCardEffectDecreaseInfo decreaseInfo = null)
    {
      if (string.IsNullOrEmpty(iname))
      {
        this.Reset();
      }
      else
      {
        if (master == null)
          master = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
        this.mSkillParam = master.GetSkillParam(iname);
        if (decreaseInfo != null)
        {
          this.m_ConceptCardData = decreaseInfo.m_ConceptCardData;
          this.m_IsDecreaseEffectOnSubSlot = decreaseInfo.m_IsDecreaseEffect;
        }
        else
        {
          this.m_ConceptCardData = (ConceptCardData) null;
          this.m_IsDecreaseEffectOnSubSlot = false;
        }
        this.mRankCap = this.mSkillParam.lvcap != 0 ? (OInt) Math.Max(this.mSkillParam.lvcap, 1) : (OInt) Math.Max(rankcap, 1);
        this.mRank = (OInt) Math.Min(rank, (int) this.mRankCap);
        this.mTargetBuffEffect = BuffEffect.CreateBuffEffect(master.GetBuffEffectParam(this.SkillParam.target_buff_iname), (int) this.mRank, (int) this.mRankCap);
        this.mSelfBuffEffect = BuffEffect.CreateBuffEffect(master.GetBuffEffectParam(this.SkillParam.self_buff_iname), (int) this.mRank, (int) this.mRankCap);
        this.mTargetCondEffect = CondEffect.CreateCondEffect(master.GetCondEffectParam(this.SkillParam.target_cond_iname), (int) this.mRank, (int) this.mRankCap);
        this.mSelfCondEffect = CondEffect.CreateCondEffect(master.GetCondEffectParam(this.SkillParam.self_cond_iname), (int) this.mRank, (int) this.mRankCap);
        this.UpdateParam();
      }
    }

    private void Reset()
    {
      this.mSkillParam = (SkillParam) null;
      this.mRank = (OInt) 1;
    }

    public bool IsValid() => this.mSkillParam != null;

    public int GetRankCap() => (int) this.mRankCap;

    public void UpdateParam()
    {
      if (this.SkillParam == null)
        return;
      int rank = this.Rank;
      int rankCap = this.GetRankCap();
      this.mCastSpeed = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.cast_speed);
      this.mEffectRate = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.effect_rate);
      this.mEffectValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.effect_value);
      this.mEffectRange = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.effect_range);
      this.mElementValue = (OInt) 0;
      this.mControlDamageRate = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.control_damage_rate);
      this.mControlDamageValue = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.control_damage_value);
      this.mControlChargeTimeRate = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.control_ct_rate);
      this.mControlChargeTimeValue = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.control_ct_value);
      this.mShieldTurn = (OInt) this.SkillParam.CalcCurrentRankValueShort(rank, rankCap, this.SkillParam.shield_turn);
      this.mShieldValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.shield_value);
      if (this.SkillParam.ProtectSkill != null)
        this.mProtectValue = this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.ProtectSkill.Value);
      if (this.mTargetBuffEffect != null)
        this.mTargetBuffEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mTargetCondEffect != null)
        this.mTargetCondEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mSelfBuffEffect != null)
        this.mSelfBuffEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mSelfCondEffect == null)
        return;
      this.mSelfCondEffect.UpdateCurrentValues(rank, rankCap);
    }

    public bool IsNormalAttack()
    {
      return this.SkillParam != null && this.SkillParam.type == ESkillType.Attack;
    }

    public bool IsPassiveSkill()
    {
      if (this.SkillParam == null)
        return false;
      if (this.SkillType == ESkillType.Passive)
        return true;
      return this.SkillType == ESkillType.Item && this.EffectType == SkillEffectTypes.Equipment;
    }

    public bool IsItem() => this.SkillParam != null && this.SkillParam.type == ESkillType.Item;

    public bool IsReactionSkill() => this.SkillParam != null && this.SkillParam.IsReactionSkill();

    public bool IsEnableChangeRange()
    {
      return this.SkillParam != null && this.SkillParam.IsEnableChangeRange();
    }

    public bool IsEnableHeightRangeBonus()
    {
      return this.SkillParam != null && this.SkillParam.IsEnableHeightRangeBonus();
    }

    public bool IsEnableHeightParamAdjust()
    {
      return this.SkillParam != null && this.SkillParam.IsEnableHeightParamAdjust();
    }

    public bool IsEnableUnitLockTarget()
    {
      return this.SkillParam != null && this.SkillParam.IsEnableUnitLockTarget();
    }

    public bool IsDamagedSkill() => this.SkillParam != null && this.SkillParam.IsDamagedSkill();

    public bool IsHealSkill() => this.SkillParam != null && this.SkillParam.IsHealSkill();

    public bool IsPierce() => this.SkillParam != null && this.SkillParam.IsPierce();

    public bool IsJewelAttack() => this.SkillParam != null && this.SkillParam.IsJewelAttack();

    public bool IsForceHit() => this.SkillParam != null && this.SkillParam.IsForceHit();

    public bool IsSuicide() => this.SkillParam != null && this.SkillParam.IsSuicide();

    public bool IsSubActuate() => this.SkillParam != null && this.SkillParam.IsSubActuate();

    public bool IsFixedDamage() => this.SkillParam != null && this.SkillParam.IsFixedDamage();

    public bool IsForceUnitLock() => this.SkillParam != null && this.SkillParam.IsForceUnitLock();

    public bool IsAllDamageReaction()
    {
      return this.SkillParam != null && this.SkillParam.IsAllDamageReaction();
    }

    public bool IsIgnoreElement() => this.SkillParam != null && this.SkillParam.IsIgnoreElement();

    public bool IsPrevApply() => this.SkillParam != null && this.SkillParam.IsPrevApply();

    public bool IsMhmDamage() => this.SkillParam != null && this.SkillParam.IsMhmDamage();

    public bool IsCastBreak() => this.SkillParam != null && this.SkillParam.IsCastBreak();

    public bool IsCastSkill() => this.SkillParam != null && this.SkillParam.IsCastSkill();

    public bool IsCutin() => this.SkillParam != null && this.SkillParam.IsCutin();

    public bool IsMapSkill() => this.SkillParam == null || this.SkillParam.IsMapSkill();

    public bool IsBattleSkill() => this.SkillParam != null && this.SkillParam.IsBattleSkill();

    public bool IsAreaSkill() => this.SkillParam != null && this.SkillParam.IsAreaSkill();

    public bool IsAllEffect() => this.SkillParam != null && this.SkillParam.IsAllEffect();

    public bool IsLongRangeSkill() => this.SkillParam != null && this.SkillParam.IsLongRangeSkill();

    public bool IsSupportSkill() => this.SkillParam != null && this.SkillParam.IsSupportSkill();

    public bool IsTrickSkill() => this.SkillParam != null && this.SkillParam.IsTrickSkill();

    public bool IsTransformSkill() => this.SkillParam != null && this.SkillParam.IsTransformSkill();

    public bool IsDynamicTransformSkill()
    {
      return this.SkillParam != null && this.SkillParam.IsDynamicTransformSkill();
    }

    public bool IsSetBreakObjSkill()
    {
      return this.SkillParam != null && this.SkillParam.IsSetBreakObjSkill();
    }

    public bool IsChangeWeatherSkill()
    {
      return this.SkillParam != null && this.SkillParam.IsChangeWeatherSkill();
    }

    public bool IsJudgeHp(Unit unit) => this.SkillParam != null && this.SkillParam.IsJudgeHp(unit);

    public bool IsConditionSkill() => this.SkillParam != null && this.SkillParam.IsConditionSkill();

    public bool IsPhysicalAttack()
    {
      return this.SkillParam != null && this.SkillParam.attack_type == AttackTypes.PhyAttack;
    }

    public bool IsMagicalAttack()
    {
      return this.SkillParam != null && this.SkillParam.attack_type == AttackTypes.MagAttack;
    }

    public bool IsSelfTargetSelect() => this.SkillParam.IsSelfTargetSelect();

    public bool IsAdvantage() => this.SkillParam != null && this.SkillParam.IsAdvantage();

    public bool IsAiNoAutoTiming() => this.SkillParam != null && this.SkillParam.IsAiNoAutoTiming();

    public bool IsMpUseAfter() => this.SkillParam != null && this.SkillParam.IsMpUseAfter();

    public bool IsForcedTargeting
    {
      get => this.SkillParam != null && this.SkillParam.ForcedTargetingTurn > 0;
    }

    public int ForcedTargetingTurn
    {
      get => this.SkillParam != null ? this.SkillParam.ForcedTargetingTurn : 0;
    }

    public bool IsForcedTargetingSkillEffect
    {
      get => this.SkillParam != null && this.SkillParam.IsForcedTargetingSkillEffect;
    }

    public bool CheckGridLineSkill()
    {
      SkillParam skillParam = this.SkillParam;
      return skillParam != null && skillParam.line_type != ELineType.None && this.RangeMax > 1;
    }

    public bool CheckUnitSkillTarget()
    {
      switch (this.Target)
      {
        case ESkillTarget.Self:
        case ESkillTarget.SelfSide:
        case ESkillTarget.EnemySide:
        case ESkillTarget.UnitAll:
        case ESkillTarget.NotSelf:
        case ESkillTarget.SelfSideNotSelf:
          return true;
        default:
          return false;
      }
    }

    public int GetHpCost(Unit self)
    {
      int hpCost = this.SkillParam.hp_cost;
      int num = hpCost + hpCost * (int) self.CurrentStatus[BattleBonus.HpCostRate] * 100 / 10000;
      return (int) self.MaximumStatus.param.hp * num * 100 / 10000;
    }

    public BuffEffect GetBuffEffect(SkillEffectTargets target = SkillEffectTargets.Target)
    {
      if (target == SkillEffectTargets.Target)
        return this.mTargetBuffEffect;
      return target == SkillEffectTargets.Self ? this.mSelfBuffEffect : (BuffEffect) null;
    }

    public CondEffect GetCondEffect(SkillEffectTargets target = SkillEffectTargets.Target)
    {
      if (target == SkillEffectTargets.Target)
        return this.mTargetCondEffect;
      return target == SkillEffectTargets.Self ? this.mSelfCondEffect : (CondEffect) null;
    }

    public int CalcBuffEffectValue(
      ESkillTiming timing,
      ParamTypes type,
      int src,
      SkillEffectTargets target = SkillEffectTargets.Target)
    {
      return timing != this.Timing ? src : this.CalcBuffEffectValue(type, src, target);
    }

    public int CalcBuffEffectValue(ParamTypes type, int src, SkillEffectTargets target = SkillEffectTargets.Target)
    {
      BuffEffect buffEffect = this.GetBuffEffect(target);
      if (buffEffect == null)
        return src;
      BuffEffect.BuffTarget buffTarget = buffEffect[type];
      return buffTarget == null ? src : SkillParam.CalcSkillEffectValue(buffTarget.calcType, (int) buffTarget.value, src);
    }

    public int GetBuffEffectValue(ParamTypes type, SkillEffectTargets target = SkillEffectTargets.Target)
    {
      BuffEffect buffEffect = this.GetBuffEffect(target);
      if (buffEffect == null)
        return 0;
      BuffEffect.BuffTarget buffTarget = buffEffect[type];
      return buffTarget == null ? 0 : (int) buffTarget.value;
    }

    public bool BuffSkill(
      ESkillTiming timing,
      EElement element,
      BaseStatus buff,
      BaseStatus buff_negative,
      BaseStatus buff_scale,
      BaseStatus debuff,
      BaseStatus debuff_negative,
      BaseStatus debuff_scale,
      RandXorshift rand = null,
      SkillEffectTargets buff_target = SkillEffectTargets.Target,
      bool is_resume = false,
      List<BuffEffect.BuffValues> list = null,
      bool calcDecreaseEffect = false)
    {
      if (this.Timing != timing)
        return false;
      BuffEffect buffEffect = this.GetBuffEffect(buff_target);
      if (buffEffect == null)
        return false;
      if (!is_resume)
      {
        int rate = (int) buffEffect.param.rate;
        if (rate > 0 && rate < 100)
        {
          DebugUtility.Assert(rand != null, "発動確率が設定されているスキルを正規タイミングで発動させたにも関わらず乱数生成器の設定がされていない");
          if ((int) (rand.Get() % 100U) > rate)
            return false;
        }
      }
      if (list == null)
      {
        if (buff != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add, buff, calcDecreaseEffect: calcDecreaseEffect);
        if (buff_negative != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add, buff_negative, calcDecreaseEffect: calcDecreaseEffect);
        if (buff_scale != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, buff_scale, calcDecreaseEffect: calcDecreaseEffect);
        if (debuff != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add, debuff, calcDecreaseEffect: calcDecreaseEffect);
        if (debuff_negative != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add, debuff_negative, calcDecreaseEffect: calcDecreaseEffect);
        if (debuff_scale != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, debuff_scale, calcDecreaseEffect: calcDecreaseEffect);
      }
      else
      {
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Add, (BaseStatus) null, list, calcDecreaseEffect);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, (BaseStatus) null, list, calcDecreaseEffect);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Add, (BaseStatus) null, list, calcDecreaseEffect);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, (BaseStatus) null, list, calcDecreaseEffect);
      }
      return true;
    }

    private void InternalBuffSkill(
      BuffEffect effect,
      EElement element,
      BuffTypes buffType,
      bool is_check_negative_value,
      bool is_negative_value_is_buff,
      SkillParamCalcTypes calcType,
      BaseStatus status,
      List<BuffEffect.BuffValues> list = null,
      bool calcDecreaseEffect = false)
    {
      for (int index1 = 0; index1 < effect.targets.Count; ++index1)
      {
        BuffEffect.BuffTarget target = effect.targets[index1];
        if (target != null && target.buffType == buffType && (!is_check_negative_value || BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff) && target.calcType == calcType && (element == EElement.None || !BuffEffect.IsElementBuff(target.paramType) || BuffEffect.IsMatchElementBuff(element, target.paramType)))
        {
          BuffMethodTypes buffMethodType = this.GetBuffMethodType(target.buffType, calcType);
          ParamTypes paramType = target.paramType;
          int num = (int) target.value;
          if ((bool) effect.param.mIsUpBuff)
            num = 0;
          if (calcDecreaseEffect && this.IsNeedDecreaseEffect())
            num = GameUtility.CalcSubRateRoundDown((long) num, (long) this.m_ConceptCardData.CurrentDecreaseEffectRate);
          string tokkou = (string) target.tokkou;
          if (list == null)
          {
            BuffEffect.SetBuffValues(paramType, buffMethodType, ref status, num, tokkou);
          }
          else
          {
            bool flag = true;
            for (int index2 = 0; index2 < list.Count; ++index2)
            {
              BuffEffect.BuffValues buffValues = list[index2];
              if (buffValues.param_type == paramType && buffValues.method_type == buffMethodType)
              {
                buffValues.value += num;
                list[index2] = buffValues;
                flag = false;
                break;
              }
            }
            if (flag)
              list.Add(new BuffEffect.BuffValues()
              {
                param_type = paramType,
                method_type = buffMethodType,
                value = num
              });
          }
        }
      }
    }

    private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      if (this.Timing == ESkillTiming.Passive || calc != SkillParamCalcTypes.Scale)
        return BuffMethodTypes.Add;
      return buff == BuffTypes.Buff ? BuffMethodTypes.Highest : BuffMethodTypes.Lowest;
    }

    public bool IsReactionDet(AttackDetailTypes atk_detail_type)
    {
      return this.mSkillParam != null && this.mSkillParam.IsReactionDet(atk_detail_type);
    }

    public static void GetHomePassiveBuffStatus(
      SkillData skill,
      EElement element,
      ref BaseStatus status,
      ref BaseStatus scale_status,
      bool calcDecreaseEffect = false)
    {
      SkillData.GetHomePassiveBuffStatus(skill, element, ref status, ref status, ref status, ref status, ref scale_status, calcDecreaseEffect);
    }

    public static void GetHomePassiveBuffStatus(
      SkillData skill,
      EElement element,
      ref BaseStatus status,
      ref BaseStatus negative_status,
      ref BaseStatus debuff_status,
      ref BaseStatus negative_debuff_status,
      ref BaseStatus scale_status,
      bool calcDecreaseEffect = false)
    {
      if (skill == null || !skill.IsPassiveSkill() || skill.Target != ESkillTarget.Self || skill.Condition != ESkillCondition.None || !string.IsNullOrEmpty(skill.SkillParam.tokkou))
        return;
      BuffEffect buffEffect1 = skill.GetBuffEffect();
      if (buffEffect1 != null && buffEffect1.param != null && buffEffect1.param.cond == ESkillCondition.None && buffEffect1.param.mAppType == EAppType.Standard && buffEffect1.param.mEffRange == EEffRange.None && !(bool) buffEffect1.param.mIsUpBuff)
        skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, calcDecreaseEffect: calcDecreaseEffect);
      BuffEffect buffEffect2 = skill.GetBuffEffect(SkillEffectTargets.Self);
      if (buffEffect2 == null || buffEffect2.param == null || buffEffect2.param.cond != ESkillCondition.None || buffEffect2.param.mAppType != EAppType.Standard || buffEffect2.param.mEffRange != EEffRange.None || (bool) buffEffect2.param.mIsUpBuff)
        return;
      skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, buff_target: SkillEffectTargets.Self, calcDecreaseEffect: calcDecreaseEffect);
    }

    public static void GetPassiveBuffStatus(
      SkillData skill,
      BuffEffect[] add_buff_effects,
      EElement element,
      ref BaseStatus status,
      ref BaseStatus scale_status)
    {
      SkillData.GetPassiveBuffStatus(skill, add_buff_effects, element, ref status, ref status, ref status, ref status, ref scale_status);
    }

    public static void GetPassiveBuffStatus(
      SkillData skill,
      BuffEffect[] add_buff_effects,
      EElement element,
      ref BaseStatus status,
      ref BaseStatus negative_status,
      ref BaseStatus debuff_status,
      ref BaseStatus negative_debuff_status,
      ref BaseStatus scale_status)
    {
      if (skill == null || !skill.IsPassiveSkill())
        return;
      BuffEffect buffEffect1 = skill.GetBuffEffect();
      if (buffEffect1 != null && buffEffect1.param != null && buffEffect1.param.mAppType == EAppType.Standard && buffEffect1.param.mEffRange == EEffRange.None && !(bool) buffEffect1.param.mIsUpBuff)
        skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status);
      BuffEffect buffEffect2 = skill.GetBuffEffect(SkillEffectTargets.Self);
      if (buffEffect2 == null || buffEffect2.param == null || buffEffect2.param.mAppType != EAppType.Standard || buffEffect2.param.mEffRange != EEffRange.None || (bool) buffEffect2.param.mIsUpBuff)
        return;
      skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, buff_target: SkillEffectTargets.Self);
    }

    public bool IsTargetGridNoUnit => this.SkillParam != null && this.SkillParam.IsTargetGridNoUnit;

    public bool IsTargetValidGrid => this.SkillParam != null && this.SkillParam.IsTargetValidGrid;

    public bool IsSkillCountNoLimit
    {
      get => this.SkillParam != null && this.SkillParam.IsSkillCountNoLimit;
    }

    public bool IsTargetTeleport => this.SkillParam != null && this.SkillParam.IsTargetTeleport;

    public void SetOwnerAbility(AbilityData owner) => this.m_OwnerAbility = owner;

    public SkillData CreateDeriveSkill(SkillDeriveParam skillDeriveParam)
    {
      SkillData deriveSkill = new SkillData();
      deriveSkill.Setup(skillDeriveParam.DeriveSkillIname, (int) this.mRank, (int) this.mRankCap);
      deriveSkill.m_OwnerAbility = this.m_OwnerAbility;
      deriveSkill.m_BaseSkillIname = this.SkillID;
      deriveSkill.m_SkillDeriveParam = skillDeriveParam;
      return deriveSkill;
    }

    public DependStateSpcEffParam GetDependStateSpcEffParam()
    {
      if (this.SkillParam != null && !string.IsNullOrEmpty(this.SkillParam.DependStateSpcEffId) && this.mDependStateSpcEffParam == null)
        this.mDependStateSpcEffParam = MonoSingleton<GameManager>.Instance.MasterParam.GetDependStateSpcEffParam(this.SkillParam.DependStateSpcEffId);
      return this.mDependStateSpcEffParam;
    }

    public DependStateSpcEffParam GetDependStateSpcEffParamSelf()
    {
      if (this.SkillParam != null && !string.IsNullOrEmpty(this.SkillParam.DependStateSpcEffSelfId) && this.mDependStateSpcEffParamSelf == null)
        this.mDependStateSpcEffParamSelf = MonoSingleton<GameManager>.Instance.MasterParam.GetDependStateSpcEffParam(this.SkillParam.DependStateSpcEffSelfId);
      return this.mDependStateSpcEffParamSelf;
    }

    public bool IsNeedDecreaseEffect()
    {
      return this.IsCreatedByConceptCard && this.m_ConceptCardData.IsEquipedSubSlot && this.IsDecreaseEffectOnSubSlot;
    }

    public bool IsProtectSkill() => this.SkillParam != null && this.SkillParam.IsProtectSkill();

    public bool IsProtectReactionSkill()
    {
      return this.SkillParam != null && this.SkillParam.IsProtectReactionSkill();
    }

    public bool IsNeedResetDirection
    {
      get => this.SkillParam != null && this.SkillParam.IsNeedResetDirection;
    }

    public bool IsShowSkillNamePlate
    {
      get => this.SkillParam != null && this.SkillParam.IsShowSkillNamePlate;
    }

    public bool IsAllowCutIn => this.SkillParam != null && this.SkillParam.IsAllowCutIn;

    public bool IsAdditionalSkill => this.SkillParam != null && this.SkillParam.IsAdditionalSkill;

    public SkillAdditionalParam SkillAdditional
    {
      get
      {
        return this.SkillParam != null ? this.SkillParam.SkillAdditional : (SkillAdditionalParam) null;
      }
    }

    public SkillAntiShieldParam SkillAntiShield
    {
      get
      {
        return this.SkillParam != null ? this.SkillParam.SkillAntiShield : (SkillAntiShieldParam) null;
      }
    }

    public int GetAntiShieldIgnoreRate()
    {
      if (this.mAntiShieldIgnoreRate < (short) 0)
      {
        this.mAntiShieldIgnoreRate = (short) 0;
        SkillAntiShieldParam skillAntiShield = this.SkillAntiShield;
        if (skillAntiShield != null && skillAntiShield.IsIgnore)
        {
          int num = this.SkillParam.CalcCurrentRankValueShort((int) this.mRank, (int) this.mRankCap, skillAntiShield.IgnoreRate);
          if (num == 0)
            num = 100;
          if (num > 0)
            this.mAntiShieldIgnoreRate = (short) num;
        }
      }
      return (int) this.mAntiShieldIgnoreRate;
    }

    public int GetAntiShieldDestroyRate()
    {
      if (this.mAntiShieldDestroyRate < (short) 0)
      {
        this.mAntiShieldDestroyRate = (short) 0;
        SkillAntiShieldParam skillAntiShield = this.SkillAntiShield;
        if (skillAntiShield != null && skillAntiShield.IsDestroy)
        {
          int num = this.SkillParam.CalcCurrentRankValueShort((int) this.mRank, (int) this.mRankCap, skillAntiShield.DestroyRate);
          if (num == 0)
            num = 100;
          if (num > 0)
            this.mAntiShieldDestroyRate = (short) num;
        }
      }
      return (int) this.mAntiShieldDestroyRate;
    }
  }
}
