// Decompiled with JetBrains decompiler
// Type: SRPG.TrickData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TrickData
  {
    private TrickParam mTrickParam;
    private BuffEffect mBuffEffect;
    private CondEffect mCondEffect;
    private OBool mValid = (OBool) false;
    private Unit mCreateUnit;
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    private OInt mGridX;
    private OInt mGridY;
    private OInt mRestActionCount;
    private OInt mCreateClock;
    private string mTag;
    private static List<TrickData> mTrickDataLists = new List<TrickData>();
    private static Dictionary<TrickData, GameObject> mTrickMarkerLists = new Dictionary<TrickData, GameObject>();
    private EUnitDirection[] reverseDirection = new EUnitDirection[5]
    {
      EUnitDirection.NegativeX,
      EUnitDirection.NegativeY,
      EUnitDirection.PositiveX,
      EUnitDirection.PositiveY,
      EUnitDirection.PositiveX
    };

    public TrickParam TrickParam => this.mTrickParam;

    public BuffEffect BuffEffect => this.mBuffEffect;

    public CondEffect CondEffect => this.mCondEffect;

    public OBool Valid => this.mValid;

    public Unit CreateUnit => this.mCreateUnit;

    public OInt Rank => this.mRank;

    public OInt RankCap => this.mRankCap;

    public OInt GridX => this.mGridX;

    public OInt GridY => this.mGridY;

    public OInt RestActionCount => this.mRestActionCount;

    public OInt CreateClock => this.mCreateClock;

    public string Tag => this.mTag;

    private void setup(
      string iname,
      int grid_x,
      int grid_y,
      string tag,
      Unit creator,
      int create_clock,
      int rank,
      int rankcap)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect))
        return;
      this.mTrickParam = instanceDirect.MasterParam.GetTrickParam(iname);
      if (this.mTrickParam == null)
        return;
      this.mRankCap = (OInt) Math.Max(rankcap, 1);
      this.mRank = (OInt) Math.Min(rank, (int) this.mRankCap);
      this.mBuffEffect = BuffEffect.CreateBuffEffect(instanceDirect.MasterParam.GetBuffEffectParam(this.mTrickParam.BuffId), (int) this.mRank, (int) this.mRankCap);
      this.mCondEffect = CondEffect.CreateCondEffect(instanceDirect.MasterParam.GetCondEffectParam(this.mTrickParam.CondId), (int) this.mRank, (int) this.mRankCap);
      this.mCreateUnit = creator;
      this.mGridX = (OInt) grid_x;
      this.mGridY = (OInt) grid_y;
      this.mTag = tag;
      this.mRestActionCount = this.mTrickParam.ActionCount;
      this.mCreateClock = (OInt) create_clock;
      this.mValid = (OBool) true;
    }

    private bool actionEffectTurnStart(List<Unit> target_lists, RandXorshift rand)
    {
      if (target_lists == null)
        return false;
      bool flag = false;
      foreach (Unit targetList in target_lists)
        flag |= this.actionBuff(targetList, EffectCheckTimings.MomentStart, rand);
      return flag;
    }

    private bool actionBuff(Unit target, EffectCheckTimings chk_timing, RandXorshift rand = null)
    {
      if (this.mBuffEffect == null || target == null || !this.mBuffEffect.CheckEnableBuffTarget(target))
        return false;
      if (rand != null)
      {
        int rate = (int) this.mBuffEffect.param.rate;
        if (0 < rate && rate < 100 && (int) (rand.Get() % 100U) > rate)
          return true;
      }
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      this.mBuffEffect.CalcBuffStatus(ref status1, target.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
      this.mBuffEffect.CalcBuffStatus(ref status2, target.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
      this.mBuffEffect.CalcBuffStatus(ref status3, target.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
      this.mBuffEffect.CalcBuffStatus(ref status4, target.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
      this.mBuffEffect.CalcBuffStatus(ref status5, target.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
      this.mBuffEffect.CalcBuffStatus(ref status6, target.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, false))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Buff, false, SkillParamCalcTypes.Add, status1, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, true))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Buff, true, SkillParamCalcTypes.Add, status2, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Buff, false, SkillParamCalcTypes.Scale, status3, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, false))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, status4, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, true))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, status5, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      if (this.mBuffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale))
      {
        BuffAttachment buffAttachment = this.createBuffAttachment(target, BuffTypes.Debuff, false, SkillParamCalcTypes.Scale, status6, chk_timing);
        target.SetBuffAttachment(buffAttachment);
      }
      return true;
    }

    private BuffAttachment createBuffAttachment(
      Unit target,
      BuffTypes buff_type,
      bool is_negative_value_is_buff,
      SkillParamCalcTypes calc_type,
      BaseStatus status,
      EffectCheckTimings chk_timing)
    {
      if (this.mBuffEffect == null)
        return (BuffAttachment) null;
      BuffAttachment buffAttachment = new BuffAttachment(this.mBuffEffect);
      buffAttachment.user = this.mCreateUnit;
      buffAttachment.skill = (SkillData) null;
      buffAttachment.skilltarget = SkillEffectTargets.Self;
      buffAttachment.IsPassive = (OBool) false;
      buffAttachment.CheckTarget = (Unit) null;
      buffAttachment.DuplicateCount = 0;
      buffAttachment.CheckTiming = chk_timing;
      buffAttachment.turn = (OInt) 1;
      buffAttachment.BuffType = buff_type;
      buffAttachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
      buffAttachment.CalcType = calc_type;
      buffAttachment.UseCondition = this.mBuffEffect.param.cond;
      status.CopyTo(buffAttachment.status);
      return buffAttachment;
    }

    private bool actionEffectTurnEnd(
      List<Unit> target_lists,
      RandXorshift rand,
      LogMapTrick log_mt)
    {
      if (target_lists == null)
        return false;
      bool flag = false;
      foreach (Unit targetList in target_lists)
      {
        LogMapTrick.TargetInfo log_mt_ti = (LogMapTrick.TargetInfo) null;
        if (log_mt != null)
        {
          log_mt_ti = new LogMapTrick.TargetInfo();
          log_mt_ti.Target = targetList;
        }
        switch (this.mTrickParam.DamageType)
        {
          case eTrickDamageType.DAMAGE:
            flag |= this.actionDamage(targetList, log_mt_ti);
            break;
          case eTrickDamageType.HEAL:
            flag |= this.actionHeal(targetList, log_mt_ti);
            break;
        }
        flag |= this.actionCond(targetList, rand, log_mt_ti);
        flag |= this.actionKnockBack(targetList, rand, log_mt_ti);
        log_mt?.TargetInfoLists.Add(log_mt_ti);
      }
      return flag;
    }

    public int calcHeal(Unit target)
    {
      int hp = (int) target.MaximumStatus.param.hp;
      return Math.Min(this.mTrickParam.CalcType == SkillParamCalcTypes.Scale ? hp * (int) this.mTrickParam.DamageVal / 100 : (int) this.mTrickParam.DamageVal, hp - (int) target.CurrentStatus.param.hp);
    }

    private bool actionHeal(Unit target, LogMapTrick.TargetInfo log_mt_ti)
    {
      int num = 0;
      if (!target.IsUnitCondition(EUnitCondition.DisableHeal) && !target.IsUseBossLongHP())
      {
        int val = this.calcHeal(target);
        num = target.CalcParamRecover(val);
      }
      if (num < 0)
        return false;
      target.Heal(num);
      if (log_mt_ti != null)
      {
        log_mt_ti.IsEffective = true;
        log_mt_ti.Heal = num;
      }
      return true;
    }

    public int calcDamage(Unit target)
    {
      int hp = (int) target.MaximumStatus.param.hp;
      int num1 = this.mTrickParam.CalcType == SkillParamCalcTypes.Scale ? hp * (int) this.mTrickParam.DamageVal / 100 : (int) this.mTrickParam.DamageVal;
      if (num1 <= 0)
        return 0;
      int num2 = 0 + this.getRateDamageElement(target) + this.getRateDamageAttackDetail(target) + this.getRateDamageUnitDefense(target);
      return Math.Max(num1 - num1 * num2 / 100, 1);
    }

    private bool actionDamage(Unit target, LogMapTrick.TargetInfo log_mt_ti)
    {
      int num = this.calcDamage(target);
      if (num <= 0)
        return false;
      target.Damage(num, true);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance) && SceneBattle.Instance.Battle != null)
        SceneBattle.Instance.Battle.GimmickEventHpLower(target);
      if (log_mt_ti != null)
      {
        log_mt_ti.IsEffective = true;
        log_mt_ti.Damage = num;
      }
      return true;
    }

    private int getRateDamageElement(Unit target)
    {
      int rateDamageElement = 0;
      if (this.mTrickParam.Elem != EElement.None)
      {
        ElementParam elementResist = target.CurrentStatus.element_resist;
        rateDamageElement += (int) elementResist[this.mTrickParam.Elem];
      }
      return rateDamageElement;
    }

    private int getRateDamageAttackDetail(Unit target)
    {
      int damageAttackDetail = 0;
      switch (this.mTrickParam.AttackDetail)
      {
        case AttackDetailTypes.None:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_NoDiv];
          break;
        case AttackDetailTypes.Slash:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Slash];
          break;
        case AttackDetailTypes.Stab:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Pierce];
          break;
        case AttackDetailTypes.Blow:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Blow];
          break;
        case AttackDetailTypes.Shot:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Shot];
          break;
        case AttackDetailTypes.Magic:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Magic];
          break;
        case AttackDetailTypes.Jump:
          damageAttackDetail += (int) target.CurrentStatus[BattleBonus.Resist_Jump];
          break;
      }
      return damageAttackDetail;
    }

    private int getRateDamageUnitDefense(Unit target)
    {
      int damageUnitDefense = 0;
      switch (target.Element)
      {
        case EElement.Fire:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseFire];
          break;
        case EElement.Water:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseWater];
          break;
        case EElement.Wind:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseWind];
          break;
        case EElement.Thunder:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseThunder];
          break;
        case EElement.Shine:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseShine];
          break;
        case EElement.Dark:
          damageUnitDefense += (int) target.CurrentStatus[BattleBonus.UnitDefenseDark];
          break;
      }
      return damageUnitDefense;
    }

    private bool actionCond(Unit target, RandXorshift rand, LogMapTrick.TargetInfo log_mt_ti)
    {
      CondEffect mCondEffect = this.mCondEffect;
      if (rand == null || mCondEffect == null || mCondEffect.param == null || mCondEffect.param.conditions == null)
        return false;
      ConditionEffectTypes conditionEffectTypes = ConditionEffectTypes.None;
      if (!mCondEffect.CheckEnableCondTarget(target))
        return true;
      if (mCondEffect.param.type != ConditionEffectTypes.None && mCondEffect.param.conditions != null)
      {
        int rate = (int) mCondEffect.rate;
        if (0 < rate && rate < 100 && (int) (rand.Get() % 100U) > rate)
          return true;
        conditionEffectTypes = mCondEffect.param.type;
      }
      switch (conditionEffectTypes)
      {
        case ConditionEffectTypes.CureCondition:
          for (int index = 0; index < mCondEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = mCondEffect.param.conditions[index];
            this.cureCond(target, condition, log_mt_ti);
          }
          break;
        case ConditionEffectTypes.FailCondition:
          if ((int) mCondEffect.value != 0)
          {
            EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
            for (int index = 0; index < mCondEffect.param.conditions.Length; ++index)
            {
              EUnitCondition condition = mCondEffect.param.conditions[index];
              if (!target.IsDisableUnitCondition(condition) && this.checkFailCond(target, (int) mCondEffect.value, (int) enchantResist[condition], condition, rand))
                this.failCond(target, mCondEffect, conditionEffectTypes, condition, log_mt_ti);
            }
            break;
          }
          break;
        case ConditionEffectTypes.ForcedFailCondition:
          for (int index = 0; index < mCondEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = mCondEffect.param.conditions[index];
            this.failCond(target, mCondEffect, conditionEffectTypes, condition, log_mt_ti);
          }
          break;
        case ConditionEffectTypes.RandomFailCondition:
          if ((int) mCondEffect.value != 0)
          {
            EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
            int index = (int) ((long) rand.Get() % (long) mCondEffect.param.conditions.Length);
            EUnitCondition condition = mCondEffect.param.conditions[index];
            if (!target.IsDisableUnitCondition(condition) && this.checkFailCond(target, (int) mCondEffect.value, (int) enchantResist[condition], condition, rand))
            {
              this.failCond(target, mCondEffect, conditionEffectTypes, condition, log_mt_ti);
              break;
            }
            break;
          }
          break;
        case ConditionEffectTypes.DisableCondition:
          for (int index = 0; index < mCondEffect.param.conditions.Length; ++index)
          {
            CondAttachment condAttachment = this.createCondAttachment(target, mCondEffect, conditionEffectTypes, mCondEffect.param.conditions[index]);
            target.SetCondAttachment(condAttachment);
          }
          break;
      }
      return true;
    }

    private bool checkFailCond(
      Unit target,
      int val,
      int resist,
      EUnitCondition condition,
      RandXorshift rand)
    {
      if (rand == null || val <= 0)
        return false;
      int num1 = val - resist;
      if (num1 <= 0)
        return false;
      int num2 = (int) (rand.Get() % 100U);
      return num1 > num2;
    }

    private void cureCond(Unit target, EUnitCondition condition, LogMapTrick.TargetInfo log_mt_ti)
    {
      bool flag = target.IsUnitCondition(condition);
      target.CureCondEffects(condition);
      if (log_mt_ti == null || !flag || target.IsUnitCondition(condition))
        return;
      log_mt_ti.IsEffective = true;
      log_mt_ti.CureCondition |= condition;
    }

    private void failCond(
      Unit target,
      CondEffect effect,
      ConditionEffectTypes effect_type,
      EUnitCondition condition,
      LogMapTrick.TargetInfo log_mt_ti)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
      {
        BattleCore battle = instance.Battle;
        if (battle != null)
        {
          LogFailCondition logFailCondition = battle.Log<LogFailCondition>();
          logFailCondition.self = target;
          logFailCondition.source = (Unit) null;
          logFailCondition.condition = condition;
          TacticsUnitController unitController = instance.FindUnitController(target);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
            unitController.LockUpdateBadStatus(condition);
        }
      }
      CondAttachment condAttachment = this.createCondAttachment(target, effect, effect_type, condition);
      target.SetCondAttachment(condAttachment);
      if (log_mt_ti == null || !target.IsUnitCondition(condition))
        return;
      log_mt_ti.IsEffective = true;
      log_mt_ti.FailCondition |= condition;
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
      condAttachment.IsPassive = (OBool) false;
      condAttachment.UseCondition = ESkillCondition.None;
      condAttachment.CondType = type;
      condAttachment.Condition = condition;
      condAttachment.turn = effect.turn;
      condAttachment.CheckTiming = effect.param.chk_timing;
      condAttachment.CheckTarget = target;
      if (condAttachment.IsFailCondition())
        condAttachment.IsCurse = effect.IsCurse;
      condAttachment.SetupLinkageBuff();
      return condAttachment;
    }

    private bool actionKnockBack(Unit target, RandXorshift rand, LogMapTrick.TargetInfo log_mt_ti)
    {
      if (rand == null || (int) this.mTrickParam.KnockBackRate == 0 || (int) this.mTrickParam.KnockBackVal == 0)
        return false;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return false;
      if (!this.checkKnockBack(target, rand))
        return true;
      EUnitDirection dir = this.reverseDirection[(int) target.Direction];
      Grid gridKnockBack = battle.GetGridKnockBack(target, dir, (int) this.mTrickParam.KnockBackVal);
      if (gridKnockBack == null)
        return true;
      if (log_mt_ti != null)
      {
        log_mt_ti.IsEffective = true;
        log_mt_ti.KnockBackGrid = gridKnockBack;
      }
      target.x = gridKnockBack.x;
      target.y = gridKnockBack.y;
      return true;
    }

    private bool checkKnockBack(Unit target, RandXorshift rand)
    {
      if (target == null || rand == null || target.IsDisableUnitCondition(EUnitCondition.DisableKnockback))
        return false;
      int num = (int) this.mTrickParam.KnockBackRate - (int) target.CurrentStatus.enchant_resist[EnchantTypes.Knockback];
      return num > 0 && (num >= 100 || (int) (rand.Get() % 100U) < num);
    }

    private bool checkTarget(Unit target, bool is_eff = false)
    {
      if (target == null || this.mTrickParam.IgnoreMovTypeList.Contains(target.MovType))
        return false;
      bool flag = false;
      ESkillTarget eskillTarget = this.mTrickParam.Target;
      if (is_eff)
        eskillTarget = this.mTrickParam.EffTarget;
      switch (eskillTarget)
      {
        case ESkillTarget.Self:
          if (this.mCreateUnit != null)
          {
            flag = target == this.mCreateUnit;
            break;
          }
          break;
        case ESkillTarget.SelfSide:
        case ESkillTarget.SelfSideNotSelf:
          EUnitSide eunitSide1 = EUnitSide.Enemy;
          if (this.mCreateUnit != null)
            eunitSide1 = this.mCreateUnit.Side;
          flag = target.Side == eunitSide1;
          if (eskillTarget == ESkillTarget.SelfSideNotSelf && flag && this.mCreateUnit != null)
          {
            flag = target != this.mCreateUnit;
            break;
          }
          break;
        case ESkillTarget.EnemySide:
          EUnitSide eunitSide2 = EUnitSide.Enemy;
          if (this.mCreateUnit != null)
            eunitSide2 = this.mCreateUnit.Side;
          flag = target.Side != eunitSide2;
          break;
        case ESkillTarget.UnitAll:
          flag = true;
          break;
        case ESkillTarget.NotSelf:
          if (this.mCreateUnit != null)
          {
            flag = target != this.mCreateUnit;
            break;
          }
          break;
      }
      return flag;
    }

    private bool checkTiming(EffectCheckTimings chk_timing)
    {
      if (this.mBuffEffect == null)
        return false;
      bool flag = false;
      foreach (BuffEffectParam.Buff buff in this.mBuffEffect.param.buffs)
      {
        switch (buff.type)
        {
          case ParamTypes.Mov:
          case ParamTypes.Jmp:
            flag = true;
            break;
        }
      }
      if (chk_timing == EffectCheckTimings.Moment)
        return !flag;
      return chk_timing == EffectCheckTimings.MomentStart && flag;
    }

    private void decActionCount()
    {
      if ((int) this.mTrickParam.ActionCount == 0)
        return;
      --this.mRestActionCount;
      if ((int) this.mRestActionCount > 0)
        return;
      TrickData.RemoveEffect(this);
    }

    public bool IsVisualized()
    {
      return this.mTrickParam.VisualType != eTrickVisualType.NONE && (this.mTrickParam.VisualType != eTrickVisualType.PLAYER || MonoSingleton<GameManager>.Instance.AudienceMode || this.mCreateUnit != null && this.mCreateUnit.Side == EUnitSide.Player);
    }

    public static void ClearEffect() => TrickData.mTrickDataLists.Clear();

    public static List<TrickData> GetEffectAll() => TrickData.mTrickDataLists;

    public static TrickData SearchEffect(int grid_x, int grid_y, bool is_reinforcement = false)
    {
      TrickData trickData = TrickData.mTrickDataLists.Find((Predicate<TrickData>) (tdl => (bool) tdl.mValid && (int) tdl.mGridX == grid_x && (int) tdl.mGridY == grid_y));
      if (trickData != null && is_reinforcement && !trickData.mTrickParam.IsReinforcement)
        trickData = (TrickData) null;
      return trickData;
    }

    public static bool IsOnEffect(Unit unit, bool is_reinforcement)
    {
      if (unit == null)
        return false;
      if (unit.IsNormalSize)
        return null != TrickData.SearchEffect(unit.x, unit.y, is_reinforcement);
      for (int minColX = unit.MinColX; minColX < unit.MaxColX; ++minColX)
      {
        for (int minColY = unit.MinColY; minColY < unit.MaxColY; ++minColY)
        {
          if (TrickData.SearchEffect(unit.x + minColX, unit.y + minColY, is_reinforcement) != null)
            return true;
        }
      }
      return false;
    }

    public static TrickData EntryEffect(
      string iname,
      int grid_x,
      int grid_y,
      string tag,
      Unit creator = null,
      int create_clock = 0,
      int rank = 1,
      int rankcap = 1)
    {
      if (string.IsNullOrEmpty(iname))
        return (TrickData) null;
      TrickData trickData = new TrickData();
      trickData.setup(iname, grid_x, grid_y, tag, creator, create_clock, rank, rankcap);
      if (trickData.mTrickParam == null)
        return (TrickData) null;
      TrickData trick_data = TrickData.SearchEffect(grid_x, grid_y);
      if (trick_data != null)
      {
        if (trick_data.mTrickParam.IsNoOverWrite)
          return (TrickData) null;
        TrickData.RemoveEffect(trick_data);
      }
      TrickData.mTrickDataLists.Add(trickData);
      return trickData;
    }

    public static TrickData SuspendEffect(
      string iname,
      int grid_x,
      int grid_y,
      string tag,
      Unit creator,
      int create_clock,
      int rank,
      int rankcap,
      int rest_count)
    {
      TrickData trickData = TrickData.EntryEffect(iname, grid_x, grid_y, tag, creator, create_clock, rank, rankcap);
      if (trickData != null)
        trickData.mRestActionCount = (OInt) rest_count;
      return trickData;
    }

    public static bool RemoveEffect(TrickData trick_data)
    {
      if (trick_data == null || !TrickData.mTrickDataLists.Contains(trick_data))
        return false;
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        instance.Battle?.GimmickEventTrickKillCount(trick_data);
      TrickData.mTrickDataLists.Remove(trick_data);
      return true;
    }

    public static void RemoveEffect(int grid_x, int grid_y)
    {
      List<TrickData> all = TrickData.mTrickDataLists.FindAll((Predicate<TrickData>) (tdl => (int) tdl.mGridX == grid_x && (int) tdl.mGridY == grid_y));
      if (all == null || all.Count == 0)
        return;
      foreach (TrickData trick_data in all)
      {
        SceneBattle instance = SceneBattle.Instance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
          instance.Battle?.GimmickEventTrickKillCount(trick_data);
      }
      List<TrickData> trickDataList = new List<TrickData>(TrickData.mTrickDataLists.Count);
      foreach (TrickData mTrickDataList in TrickData.mTrickDataLists)
      {
        if (!all.Contains(mTrickDataList))
          trickDataList.Add(mTrickDataList);
      }
      TrickData.mTrickDataLists = trickDataList;
    }

    public static bool CheckClock(int now_clock)
    {
      if (TrickData.mTrickDataLists.Count == 0)
        return false;
      List<TrickData> trickDataList = new List<TrickData>(TrickData.mTrickDataLists.Count);
      foreach (TrickData mTrickDataList in TrickData.mTrickDataLists)
      {
        if ((int) mTrickDataList.mTrickParam.ValidClock == 0 || (int) mTrickDataList.mTrickParam.ValidClock + (int) mTrickDataList.mCreateClock >= now_clock)
          trickDataList.Add(mTrickDataList);
      }
      if (TrickData.mTrickDataLists.Count == trickDataList.Count)
        return false;
      TrickData.mTrickDataLists = trickDataList;
      return true;
    }

    public static void ActionEffect(
      eTrickActionTiming action_timing,
      Unit target,
      int grid_x,
      int grid_y,
      RandXorshift rand,
      LogMapTrick log_mt = null)
    {
      if (target == null)
        return;
      TrickData trick_data = TrickData.SearchEffect(grid_x, grid_y);
      if (trick_data == null || !trick_data.checkTarget(target) || target.IsJump)
        return;
      if (log_mt != null)
      {
        log_mt.TrickData = trick_data;
        log_mt.TargetInfoLists.Clear();
      }
      List<Unit> target_lists = new List<Unit>();
      if (trick_data.checkTarget(target, true))
        target_lists.Add(target);
      TrickData.addTargetAreaEff(grid_x, grid_y, trick_data, target_lists);
      if (action_timing != eTrickActionTiming.TURN_START)
      {
        if (action_timing != eTrickActionTiming.TURN_END)
          return;
        trick_data.actionEffectTurnEnd(target_lists, rand, log_mt);
        trick_data.decActionCount();
      }
      else
        trick_data.actionEffectTurnStart(target_lists, rand);
    }

    private static void addTargetAreaEff(
      int grid_x,
      int grid_y,
      TrickData trick_data,
      List<Unit> target_lists)
    {
      if (trick_data == null || target_lists == null)
        return;
      TrickParam mTrickParam = trick_data.mTrickParam;
      if (!mTrickParam.IsAreaEff)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return;
      GridMap<bool> scopeGridMap = battle.CreateScopeGridMap(grid_x, grid_y, mTrickParam.EffShape, (int) mTrickParam.EffScope, (int) mTrickParam.EffHeight);
      if (scopeGridMap == null)
        return;
      for (int x = 0; x < scopeGridMap.w; ++x)
      {
        for (int y = 0; y < scopeGridMap.h; ++y)
        {
          if (scopeGridMap.get(x, y))
          {
            Unit unitAtGrid = battle.FindUnitAtGrid(x, y);
            if (unitAtGrid != null && !target_lists.Contains(unitAtGrid) && trick_data.checkTarget(unitAtGrid, true))
              target_lists.Add(unitAtGrid);
          }
        }
      }
    }

    public static bool MomentBuff(
      Unit target,
      int grid_x,
      int grid_y,
      EffectCheckTimings chk_timing = EffectCheckTimings.Moment)
    {
      if (target == null)
        return false;
      TrickData trickData = TrickData.SearchEffect(grid_x, grid_y);
      return trickData != null && trickData.checkTarget(target) && trickData.checkTiming(chk_timing) && trickData.actionBuff(target, chk_timing);
    }

    public static void UpdateMarker()
    {
      Dictionary<TrickData, GameObject> dictionary = new Dictionary<TrickData, GameObject>();
      foreach (KeyValuePair<TrickData, GameObject> mTrickMarkerList in TrickData.mTrickMarkerLists)
      {
        TrickData key = mTrickMarkerList.Key;
        if (!TrickData.mTrickDataLists.Contains(key))
        {
          GameObject gameObject = mTrickMarkerList.Value;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
            UnityEngine.Object.Destroy((UnityEngine.Object) gameObject.gameObject);
        }
        else
          dictionary.Add(key, mTrickMarkerList.Value);
      }
      if (TrickData.mTrickMarkerLists.Count != dictionary.Count)
        TrickData.mTrickMarkerLists = dictionary;
      TrickData.AddMarker();
    }

    public static void AddMarker()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance.CurrentScene) || instance.Battle == null)
        return;
      foreach (TrickData mTrickDataList in TrickData.mTrickDataLists)
      {
        if ((bool) mTrickDataList.mValid && !TrickData.mTrickMarkerLists.ContainsKey(mTrickDataList))
        {
          Dictionary<TrickData, GameObject> dictionary = new Dictionary<TrickData, GameObject>();
          foreach (KeyValuePair<TrickData, GameObject> mTrickMarkerList in TrickData.mTrickMarkerLists)
          {
            TrickData key = mTrickMarkerList.Key;
            if ((int) key.mGridX == (int) mTrickDataList.mGridX && (int) key.mGridY == (int) mTrickDataList.mGridY)
            {
              GameObject gameObject = mTrickMarkerList.Value;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
                UnityEngine.Object.Destroy((UnityEngine.Object) gameObject.gameObject);
            }
            else
              dictionary.Add(key, mTrickMarkerList.Value);
          }
          if (TrickData.mTrickMarkerLists.Count != dictionary.Count)
            TrickData.mTrickMarkerLists = dictionary;
          TrickData.entryMarker(instance, mTrickDataList);
        }
      }
    }

    private static void entryMarker(SceneBattle sb, TrickData td)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) sb) || td == null || !td.IsVisualized())
        return;
      GameObject gameObject1 = sb.TrickMarker;
      string markerName = td.mTrickParam.MarkerName;
      if (!string.IsNullOrEmpty(markerName) && sb.TrickMarkerDics.ContainsKey(markerName))
        gameObject1 = sb.TrickMarkerDics[markerName];
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject1))
        return;
      Vector3 vector3 = sb.CalcGridCenter((int) td.mGridX, (int) td.mGridY);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1, vector3, Quaternion.identity);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject2))
        return;
      gameObject2.transform.SetParent(sb.CurrentScene.transform, false);
      TrickData.mTrickMarkerLists.Add(td, gameObject2);
    }

    public static bool CheckRemoveMarker(TrickData trick_data)
    {
      if (trick_data == null || TrickData.mTrickDataLists.Contains(trick_data) || !TrickData.mTrickMarkerLists.ContainsKey(trick_data))
        return false;
      GameObject mTrickMarkerList = TrickData.mTrickMarkerLists[trick_data];
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) mTrickMarkerList))
        UnityEngine.Object.Destroy((UnityEngine.Object) mTrickMarkerList.gameObject);
      TrickData.mTrickMarkerLists.Remove(trick_data);
      return true;
    }

    public static void UpdateMarker(
      Transform parent,
      Dictionary<string, GameObject> trickObj,
      GameObject baseObj)
    {
      Dictionary<TrickData, GameObject> dictionary = new Dictionary<TrickData, GameObject>();
      foreach (KeyValuePair<TrickData, GameObject> mTrickMarkerList in TrickData.mTrickMarkerLists)
      {
        TrickData key = mTrickMarkerList.Key;
        if (!TrickData.mTrickDataLists.Contains(key))
        {
          GameObject gameObject = mTrickMarkerList.Value;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
            UnityEngine.Object.Destroy((UnityEngine.Object) gameObject.gameObject);
        }
        else
          dictionary.Add(key, mTrickMarkerList.Value);
      }
      if (TrickData.mTrickMarkerLists.Count != dictionary.Count)
        TrickData.mTrickMarkerLists = dictionary;
      TrickData.AddMarker(parent, trickObj, baseObj);
    }

    public static void AddMarker(
      Transform parent,
      Dictionary<string, GameObject> trickObj,
      GameObject baseObj)
    {
      foreach (TrickData mTrickDataList in TrickData.mTrickDataLists)
      {
        if ((bool) mTrickDataList.mValid && !TrickData.mTrickMarkerLists.ContainsKey(mTrickDataList))
        {
          Dictionary<TrickData, GameObject> dictionary = new Dictionary<TrickData, GameObject>();
          foreach (KeyValuePair<TrickData, GameObject> mTrickMarkerList in TrickData.mTrickMarkerLists)
          {
            TrickData key = mTrickMarkerList.Key;
            if ((int) key.mGridX == (int) mTrickDataList.mGridX && (int) key.mGridY == (int) mTrickDataList.mGridY)
            {
              GameObject gameObject = mTrickMarkerList.Value;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
                UnityEngine.Object.Destroy((UnityEngine.Object) gameObject.gameObject);
            }
            else
              dictionary.Add(key, mTrickMarkerList.Value);
          }
          if (TrickData.mTrickMarkerLists.Count != dictionary.Count)
            TrickData.mTrickMarkerLists = dictionary;
          TrickData.entryMarker(parent, mTrickDataList, trickObj, baseObj);
        }
      }
    }

    private static void entryMarker(
      Transform parent,
      TrickData td,
      Dictionary<string, GameObject> dic,
      GameObject baseObj)
    {
      if (!td.IsVisualized())
        return;
      GameObject gameObject1 = baseObj;
      string markerName = td.mTrickParam.MarkerName;
      if (!string.IsNullOrEmpty(markerName) && dic.ContainsKey(markerName))
        gameObject1 = dic[markerName];
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject1))
        return;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector((float) (int) td.mGridX + 0.5f, GameUtility.CalcHeight((float) (int) td.mGridX + 0.5f, (float) (int) td.mGridY + 0.5f), (float) (int) td.mGridY + 0.5f);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1, vector3, Quaternion.identity);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject2))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
        gameObject2.transform.SetParent(parent, false);
      TrickData.mTrickMarkerLists.Add(td, gameObject2);
    }
  }
}
