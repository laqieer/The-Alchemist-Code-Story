// Decompiled with JetBrains decompiler
// Type: SRPG.SkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class SkillParam
  {
    public const int FORCED_TARGETING_ETERNAL = 99;
    public static readonly int MAX_PARAMTYPES = Enum.GetNames(typeof (ParamTypes)).Length;
    public string iname;
    public string name;
    public string expr;
    public string motion;
    public string effect;
    public string defend_effect;
    public string weapon;
    public string tokkou;
    public int tk_rate;
    public ESkillType type;
    public ESkillTiming timing;
    public ESkillCondition condition;
    public int lvcap;
    public int cost;
    public int count;
    public int rate;
    public int back_defrate;
    public int side_defrate;
    public int ignore_defense_rate;
    public ELineType line_type;
    public ESelectType select_range;
    public int range_min;
    public int range_max;
    public ESelectType select_scope;
    public int scope;
    public int effect_height;
    private BitArray mFlags = new BitArray(34);
    public int hp_cost_rate;
    public int hp_cost;
    public int random_hit_rate;
    public ECastTypes cast_type;
    public SkillRankUpValueShort cast_speed;
    public ESkillTarget target;
    public SkillEffectTypes effect_type;
    public SkillRankUpValueShort effect_rate;
    public SkillRankUpValue effect_value;
    public SkillRankUpValueShort effect_range;
    public SkillParamCalcTypes effect_calc;
    public int effect_hprate;
    public int effect_mprate;
    public int effect_dead_rate;
    public int effect_lvrate;
    public int absorb_damage_rate;
    public EElement element_type;
    public AttackTypes attack_type;
    public AttackDetailTypes attack_detail;
    public DamageTypes reaction_damage_type;
    public List<AttackDetailTypes> reaction_det_lists;
    public SkillRankUpValueShort control_damage_rate;
    public SkillRankUpValueShort control_damage_value;
    public SkillParamCalcTypes control_damage_calc;
    public SkillRankUpValueShort control_ct_rate;
    public SkillRankUpValueShort control_ct_value;
    public SkillParamCalcTypes control_ct_calc;
    public string target_buff_iname;
    public string target_cond_iname;
    public string self_buff_iname;
    public string self_cond_iname;
    public ShieldTypes shield_type;
    public DamageTypes shield_damage_type;
    public SkillRankUpValueShort shield_turn;
    public SkillRankUpValue shield_value;
    public string job;
    public int ComboNum;
    public int ComboDamageRate;
    public JewelDamageTypes JewelDamageType;
    public int JewelDamageValue;
    public int DuplicateCount;
    public string SceneName;
    public string SceneNameBigUnit;
    public string CollaboMainId;
    public int CollaboHeight;
    public int KnockBackRate;
    public int KnockBackVal;
    public eKnockBackDir KnockBackDir;
    public eKnockBackDs KnockBackDs;
    public eDamageDispType DamageDispType;
    public eTeleportType TeleportType;
    public ESkillTarget TeleportTarget;
    public int TeleportHeight;
    public bool TeleportIsMove;
    public List<string> ReplaceTargetIdLists;
    public List<string> ReplaceChangeIdLists;
    public List<string> AbilityReplaceTargetIdLists;
    public List<string> AbilityReplaceChangeIdLists;
    public string CollaboVoiceId;
    public int CollaboVoicePlayDelayFrame;
    public string ReplacedTargetId;
    public string TrickId;
    public eTrickSetType TrickSetType;
    public string BreakObjId;
    public string MapEffectDesc;
    public int WeatherRate;
    public string WeatherId;
    public int ElementSpcAtkRate;
    public int MaxDamageValue;
    public string CutInConceptCardId;
    public int JudgeHpVal;
    public SkillParamCalcTypes JudgeHpCalc;
    public string AcFromAbilId;
    public string AcToAbilId;
    public int AcTurn;
    public int EffectHitTargetNumRate;
    public eAbsorbAndGive AbsorbAndGive;
    public eSkillTargetEx TargetEx;
    public int JumpSpcAtkRate;
    public eTeleportSkillPos TeleportSkillPos;
    public string DynamicTransformUnitId;
    public string SkillMotionId;
    public string DependStateSpcEffId;
    public string DependStateSpcEffSelfId;
    public int ForcedTargetingTurn;
    public string ProtectSkillId;
    private ProtectSkillParam mProtectSkill;
    public string SkillAdditionalId;
    private SkillAdditionalParam mSkillAdditional;
    public string SkillAntiShieldId;
    private SkillAntiShieldParam mSkillAntiShield;

    public ProtectSkillParam ProtectSkill
    {
      get
      {
        if (this.mProtectSkill == null)
        {
          if (string.IsNullOrEmpty(this.ProtectSkillId))
            return (ProtectSkillParam) null;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam != null)
          {
            this.mProtectSkill = MonoSingleton<GameManager>.Instance.MasterParam.GetProtectSkillParam(this.ProtectSkillId);
            if (this.mProtectSkill == null)
              DebugUtility.LogError("SkillParam:[" + this.iname + "]で指定されているProtectSkillのiname[" + this.ProtectSkillId + "]が存在してない");
          }
        }
        return this.mProtectSkill;
      }
    }

    public SkillAdditionalParam SkillAdditional
    {
      get
      {
        if (this.mSkillAdditional == null)
        {
          if (string.IsNullOrEmpty(this.SkillAdditionalId))
            return (SkillAdditionalParam) null;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.MasterParam != null)
            this.mSkillAdditional = instance.MasterParam.GetSkillAdditionalParam(this.SkillAdditionalId);
        }
        return this.mSkillAdditional;
      }
    }

    public SkillAntiShieldParam SkillAntiShield
    {
      get
      {
        if (this.mSkillAntiShield == null)
        {
          if (string.IsNullOrEmpty(this.SkillAntiShieldId))
            return (SkillAntiShieldParam) null;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.MasterParam != null)
            this.mSkillAntiShield = instance.MasterParam.GetSkillAntiShieldParam(this.SkillAntiShieldId);
        }
        return this.mSkillAntiShield;
      }
    }

    public bool Deserialize(JSON_SkillParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.motion = json.motnm;
      this.effect = json.effnm;
      this.defend_effect = json.effdef;
      this.weapon = json.weapon;
      this.tokkou = json.tktag;
      this.tk_rate = json.tkrate;
      this.type = (ESkillType) json.type;
      this.timing = (ESkillTiming) json.timing;
      this.condition = (ESkillCondition) json.cond;
      this.target = (ESkillTarget) json.target;
      this.line_type = (ELineType) json.line;
      this.lvcap = json.cap;
      this.cost = json.cost;
      this.count = json.count;
      this.rate = json.rate;
      this.select_range = (ESelectType) json.sran;
      this.range_min = json.rangemin;
      this.range_max = json.range;
      this.select_scope = (ESelectType) json.ssco;
      this.scope = json.scope;
      this.effect_height = json.eff_h;
      this.back_defrate = json.bdb;
      this.side_defrate = json.sdb;
      this.ignore_defense_rate = json.idr;
      this.job = json.job;
      this.SceneName = json.scn;
      this.SceneNameBigUnit = json.scn_bu;
      this.ComboNum = json.combo_num;
      this.ComboDamageRate = 100 - Math.Abs(json.combo_rate);
      this.JewelDamageType = (JewelDamageTypes) json.jdtype;
      this.JewelDamageValue = json.jdv;
      this.DuplicateCount = json.dupli;
      this.CollaboMainId = json.cs_main_id;
      this.CollaboHeight = json.cs_height;
      this.KnockBackRate = json.kb_rate;
      this.KnockBackVal = json.kb_val;
      this.KnockBackDir = (eKnockBackDir) json.kb_dir;
      this.KnockBackDs = (eKnockBackDs) json.kb_ds;
      this.DamageDispType = (eDamageDispType) json.dmg_dt;
      this.ReplaceTargetIdLists = (List<string>) null;
      if (json.rp_tgt_ids != null)
      {
        this.ReplaceTargetIdLists = new List<string>();
        foreach (string rpTgtId in json.rp_tgt_ids)
          this.ReplaceTargetIdLists.Add(rpTgtId);
      }
      this.ReplaceChangeIdLists = (List<string>) null;
      if (json.rp_chg_ids != null && this.ReplaceTargetIdLists != null)
      {
        this.ReplaceChangeIdLists = new List<string>();
        foreach (string rpChgId in json.rp_chg_ids)
          this.ReplaceChangeIdLists.Add(rpChgId);
      }
      if (this.ReplaceTargetIdLists != null && this.ReplaceChangeIdLists != null && this.ReplaceTargetIdLists.Count != this.ReplaceChangeIdLists.Count)
      {
        this.ReplaceTargetIdLists.Clear();
        this.ReplaceChangeIdLists.Clear();
      }
      this.AbilityReplaceTargetIdLists = (List<string>) null;
      if (json.ab_rp_tgt_ids != null)
      {
        this.AbilityReplaceTargetIdLists = new List<string>();
        foreach (string abRpTgtId in json.ab_rp_tgt_ids)
          this.AbilityReplaceTargetIdLists.Add(abRpTgtId);
      }
      this.AbilityReplaceChangeIdLists = (List<string>) null;
      if (json.ab_rp_chg_ids != null && this.AbilityReplaceTargetIdLists != null)
      {
        this.AbilityReplaceChangeIdLists = new List<string>();
        foreach (string abRpChgId in json.ab_rp_chg_ids)
          this.AbilityReplaceChangeIdLists.Add(abRpChgId);
      }
      if (this.AbilityReplaceTargetIdLists != null && this.AbilityReplaceChangeIdLists != null && this.AbilityReplaceTargetIdLists.Count != this.AbilityReplaceChangeIdLists.Count)
      {
        this.AbilityReplaceTargetIdLists.Clear();
        this.AbilityReplaceChangeIdLists.Clear();
      }
      this.CollaboVoiceId = json.cs_voice;
      this.CollaboVoicePlayDelayFrame = json.cs_vp_df;
      this.TeleportType = (eTeleportType) json.tl_type;
      this.TeleportTarget = (ESkillTarget) json.tl_target;
      this.TeleportHeight = json.tl_height;
      this.TeleportIsMove = json.tl_is_mov != 0;
      this.TeleportSkillPos = (eTeleportSkillPos) json.tsk_pos;
      this.TrickId = json.tr_id;
      this.TrickSetType = (eTrickSetType) json.tr_set;
      this.BreakObjId = json.bo_id;
      this.MapEffectDesc = json.me_desc;
      this.WeatherRate = json.wth_rate;
      this.WeatherId = json.wth_id;
      this.ElementSpcAtkRate = json.elem_tk;
      this.MaxDamageValue = json.max_dmg;
      this.CutInConceptCardId = json.ci_cc_id;
      this.JudgeHpVal = json.jhp_val;
      this.JudgeHpCalc = (SkillParamCalcTypes) json.jhp_calc;
      this.AcFromAbilId = json.ac_fr_ab_id;
      this.AcToAbilId = json.ac_to_ab_id;
      this.AcTurn = json.ac_turn;
      this.EffectHitTargetNumRate = json.eff_htnrate;
      this.AbsorbAndGive = (eAbsorbAndGive) json.aag;
      this.TargetEx = (eSkillTargetEx) json.target_ex;
      this.JumpSpcAtkRate = json.jmp_tk;
      this.DynamicTransformUnitId = json.dtu_id;
      this.SkillMotionId = json.sm_id;
      this.DependStateSpcEffId = json.dsse_id;
      this.DependStateSpcEffSelfId = json.dsse_self_id;
      this.ForcedTargetingTurn = json.ft_turn;
      this.mFlags.SetAll(false);
      this.mFlags.Set(4, json.cutin != 0);
      this.mFlags.Set(5, json.isbtl != 0);
      this.mFlags.Set(1, json.chran != 0);
      this.mFlags.Set(3, json.sonoba != 0);
      this.mFlags.Set(2, json.pierce != 0);
      this.mFlags.Set(6, json.hbonus != 0);
      this.mFlags.Set(7, json.ehpa != 0);
      this.mFlags.Set(8, json.utgt != 0);
      this.mFlags.Set(9, json.ctbreak != 0);
      this.mFlags.Set(10, json.mpatk != 0);
      this.mFlags.Set(11, json.fhit != 0);
      this.mFlags.Set(12, json.suicide != 0);
      this.mFlags.Set(13, json.sub_actuate != 0);
      this.mFlags.Set(14, json.is_fixed != 0);
      this.mFlags.Set(15, json.f_ulock != 0);
      this.mFlags.Set(16, json.ad_react != 0);
      this.mFlags.Set(18, json.ig_elem != 0);
      this.mFlags.Set(19, json.is_pre_apply != 0);
      this.mFlags.Set(20, json.jhp_over != 0);
      this.mFlags.Set(21, json.is_mhm_dmg != 0);
      this.mFlags.Set(22, json.ac_is_self != 0);
      this.mFlags.Set(23, json.ac_is_reset != 0);
      this.mFlags.Set(24, json.is_htndiv != 0);
      this.mFlags.Set(25, json.is_no_ccc != 0);
      this.mFlags.Set(26, json.jmpbreak != 0);
      this.mFlags.Set(27, json.is_ob_react != 0);
      this.mFlags.Set(28, json.is_no_ujb != 0);
      this.mFlags.Set(29, json.is_ai_noautotiming != 0);
      this.mFlags.Set(30, json.is_cri != 0);
      this.mFlags.Set(31, json.jdabs != 0);
      this.mFlags.Set(32, json.is_mp_use_after != 0);
      this.mFlags.Set(33, json.protect_ignore != 0);
      this.hp_cost = json.hp_cost;
      this.hp_cost_rate = Math.Min(Math.Max(json.hp_cost_rate, 0), 100);
      this.random_hit_rate = json.rhit;
      this.effect_type = (SkillEffectTypes) json.eff_type;
      this.effect_calc = (SkillParamCalcTypes) json.eff_calc;
      this.effect_rate = new SkillRankUpValueShort();
      this.effect_rate.ini = json.eff_rate_ini;
      this.effect_rate.max = json.eff_rate_max;
      this.effect_value = new SkillRankUpValue();
      this.effect_value.ini = json.eff_val_ini;
      this.effect_value.max = json.eff_val_max;
      this.effect_range = new SkillRankUpValueShort();
      this.effect_range.ini = json.eff_range_ini;
      this.effect_range.max = json.eff_range_max;
      this.effect_hprate = json.eff_hprate;
      this.effect_mprate = json.eff_mprate;
      this.effect_dead_rate = json.eff_durate;
      this.effect_lvrate = json.eff_lvrate;
      this.attack_type = (AttackTypes) json.atk_type;
      this.attack_detail = (AttackDetailTypes) json.atk_det;
      this.element_type = (EElement) json.elem;
      this.cast_type = (ECastTypes) json.ct_type;
      this.cast_speed = (SkillRankUpValueShort) null;
      if (this.type == ESkillType.Skill && (json.ct_spd_ini != (short) 0 || json.ct_spd_max != (short) 0))
      {
        this.cast_speed = new SkillRankUpValueShort();
        this.cast_speed.ini = json.ct_spd_ini;
        this.cast_speed.max = json.ct_spd_max;
      }
      this.absorb_damage_rate = json.abs_d_rate;
      this.reaction_damage_type = (DamageTypes) json.react_d_type;
      this.reaction_det_lists = (List<AttackDetailTypes>) null;
      if (json.react_dets != null)
      {
        this.reaction_det_lists = new List<AttackDetailTypes>();
        foreach (AttackDetailTypes reactDet in json.react_dets)
          this.reaction_det_lists.Add(reactDet);
      }
      this.control_ct_rate = (SkillRankUpValueShort) null;
      this.control_ct_value = (SkillRankUpValueShort) null;
      if (this.control_ct_calc == SkillParamCalcTypes.Fixed || json.ct_val_ini != (short) 0 || json.ct_val_max != (short) 0)
      {
        this.control_ct_rate = new SkillRankUpValueShort();
        this.control_ct_rate.ini = json.ct_rate_ini;
        this.control_ct_rate.max = json.ct_rate_max;
        this.control_ct_value = new SkillRankUpValueShort();
        this.control_ct_value.ini = json.ct_val_ini;
        this.control_ct_value.max = json.ct_val_max;
        this.control_ct_calc = (SkillParamCalcTypes) json.ct_calc;
      }
      this.target_buff_iname = json.t_buff;
      this.target_cond_iname = json.t_cond;
      this.self_buff_iname = json.s_buff;
      this.self_cond_iname = json.s_cond;
      this.shield_type = (ShieldTypes) json.shield_type;
      this.shield_damage_type = (DamageTypes) json.shield_d_type;
      this.shield_turn = (SkillRankUpValueShort) null;
      this.shield_value = (SkillRankUpValue) null;
      if (this.shield_type != ShieldTypes.None && this.shield_damage_type != DamageTypes.None)
      {
        this.shield_turn = new SkillRankUpValueShort();
        this.shield_turn.ini = json.shield_turn_ini;
        this.shield_turn.max = json.shield_turn_max;
        this.shield_value = new SkillRankUpValue();
        this.shield_value.ini = json.shield_ini;
        this.shield_value.max = json.shield_max;
        this.mFlags.Set(17, json.shield_reset != 0);
      }
      if (this.reaction_damage_type != DamageTypes.None || this.shield_damage_type != DamageTypes.None)
      {
        this.control_damage_rate = new SkillRankUpValueShort();
        this.control_damage_rate.ini = json.ctrl_d_rate_ini;
        this.control_damage_rate.max = json.ctrl_d_rate_max;
        this.control_damage_value = new SkillRankUpValueShort();
        this.control_damage_value.ini = json.ctrl_d_ini;
        this.control_damage_value.max = json.ctrl_d_max;
        this.control_damage_calc = (SkillParamCalcTypes) json.ctrl_d_calc;
      }
      this.ProtectSkillId = json.protect_iname;
      this.SkillAdditionalId = json.sa_iname;
      this.SkillAntiShieldId = json.sas_iname;
      SkillEffectTypes effectType = this.effect_type;
      switch (effectType)
      {
        case SkillEffectTypes.Teleport:
        case SkillEffectTypes.Changing:
        case SkillEffectTypes.Throw:
          this.scope = 0;
          this.select_scope = ESelectType.Cross;
          break;
        case SkillEffectTypes.RateDamage:
          if (this.attack_type == AttackTypes.None)
          {
            this.attack_type = AttackTypes.PhyAttack;
            break;
          }
          break;
        default:
          if (effectType == SkillEffectTypes.Attack || effectType == SkillEffectTypes.ReflectDamage || effectType == SkillEffectTypes.RateDamageCurrent)
            goto case SkillEffectTypes.RateDamage;
          else
            break;
      }
      if (this.select_range == ESelectType.Laser)
      {
        this.select_scope = ESelectType.Laser;
        this.scope = Math.Max(this.scope, 1);
      }
      else
      {
        switch (this.select_range)
        {
          case ESelectType.LaserSpread:
            this.select_scope = ESelectType.LaserSpread;
            break;
          case ESelectType.LaserWide:
            this.select_scope = ESelectType.LaserWide;
            break;
          case ESelectType.LaserTwin:
            this.select_scope = ESelectType.LaserTwin;
            break;
          case ESelectType.LaserTriple:
            this.select_scope = ESelectType.LaserTriple;
            break;
        }
        switch (this.select_scope)
        {
          case ESelectType.LaserSpread:
          case ESelectType.LaserWide:
          case ESelectType.LaserTwin:
          case ESelectType.LaserTriple:
            this.scope = 1;
            break;
        }
      }
      if (this.TeleportType != eTeleportType.None)
      {
        if (!this.IsTargetGridNoUnit && this.TeleportType != eTeleportType.BeforeSkill)
          this.target = ESkillTarget.GridNoUnit;
        if (this.IsTargetTeleport)
        {
          if (this.IsCastSkill())
            this.cast_speed = (SkillRankUpValueShort) null;
          if (this.scope != 0)
            this.scope = 0;
        }
      }
      if (this.IsTargetValidGrid && !this.IsTrickSkill())
        this.target = ESkillTarget.GridNoUnit;
      if (this.timing == ESkillTiming.Auto && this.effect_type == SkillEffectTypes.Attack)
        this.effect_type = SkillEffectTypes.Buff;
      return true;
    }

    public static void UpdateReplaceSkill(List<SkillParam> sp_lists)
    {
      foreach (SkillParam spList in sp_lists)
      {
        SkillParam sp = spList;
        if (sp.ReplaceChangeIdLists != null && sp.ReplaceChangeIdLists.Count > 0 && sp.ReplaceTargetIdLists != null && sp.ReplaceTargetIdLists.Count > 0 && sp.ReplaceChangeIdLists.Count == sp.ReplaceTargetIdLists.Count)
        {
          for (int idx = 0; idx < sp.ReplaceChangeIdLists.Count; ++idx)
          {
            SkillParam skillParam1 = sp_lists.Find((Predicate<SkillParam>) (skill => skill.iname == sp.ReplaceChangeIdLists[idx]));
            SkillParam skillParam2 = sp_lists.Find((Predicate<SkillParam>) (skill => skill.iname == sp.ReplaceTargetIdLists[idx]));
            if (skillParam1 != null && skillParam2 != null && string.IsNullOrEmpty(skillParam1.ReplacedTargetId))
              skillParam1.ReplacedTargetId = skillParam2.iname;
          }
        }
      }
    }

    public bool IsCritical => this.mFlags.Get(30);

    public bool IsJewelAbsorb => this.mFlags.Get(31);

    public bool IsDamagedSkill()
    {
      return (this.effect_type == SkillEffectTypes.Attack || this.effect_type == SkillEffectTypes.ReflectDamage || this.effect_type == SkillEffectTypes.RateDamage || this.effect_type == SkillEffectTypes.RateDamageCurrent) && this.attack_type != AttackTypes.None;
    }

    public bool IsHealSkill()
    {
      return this.effect_type == SkillEffectTypes.Heal || this.effect_type == SkillEffectTypes.RateHeal;
    }

    public bool IsReactionSkill() => this.type == ESkillType.Reaction;

    public bool IsEnableChangeRange() => this.mFlags.Get(1);

    public bool IsEnableHeightRangeBonus()
    {
      return !SkillParam.IsTypeLaser(this.select_range) && !SkillParam.IsTypeLaser(this.select_scope) && this.mFlags.Get(6);
    }

    public bool IsEnableHeightParamAdjust() => this.mFlags.Get(7);

    public bool IsPierce() => this.mFlags.Get(2);

    public bool IsJewelAttack() => this.mFlags.Get(10);

    public bool IsForceHit() => this.mFlags.Get(11);

    public bool IsSuicide() => this.mFlags.Get(12);

    public bool IsSubActuate() => this.mFlags.Get(13);

    public bool IsFixedDamage() => this.mFlags.Get(14);

    public bool IsForceUnitLock() => this.mFlags.Get(15);

    public bool IsAllDamageReaction() => this.mFlags.Get(16);

    public bool IsShieldReset() => this.mFlags.Get(17);

    public bool IsIgnoreElement() => this.mFlags.Get(18);

    public bool IsPrevApply() => this.mFlags.Get(19);

    public bool IsMhmDamage() => this.mFlags.Get(21);

    public bool IsAcSelf() => this.mFlags.Get(22);

    public bool IsAcReset() => this.mFlags.Get(23);

    public bool IsHitTargetNumDiv() => this.mFlags.Get(24);

    public bool IsNoChargeCalcCT() => this.mFlags.Get(25);

    public bool IsJumpBreak() => this.mFlags.Get(26);

    public bool IsObstReaction() => this.mFlags.Get(27);

    public bool IsNoUsedJewelBuff() => this.mFlags.Get(28);

    public bool IsAiNoAutoTiming() => this.mFlags.Get(29);

    public bool IsMpUseAfter() => this.mFlags.Get(32);

    public bool IsEnableUnitLockTarget() => this.mFlags.Get(8);

    public bool IsCastBreak() => this.mFlags.Get(9);

    public bool IsCastSkill() => this.cast_speed != null;

    public bool IsCutin() => this.mFlags.Get(4);

    public bool IsMapSkill() => !this.IsBattleSkill();

    public bool IsBattleSkill() => this.mFlags.Get(5);

    public bool IsAreaSkill() => this.scope > 0 || this.select_scope == ESelectType.All;

    public bool IsAllEffect() => this.select_scope == ESelectType.All;

    public bool IsLongRangeSkill()
    {
      if (this.range_max > 1)
        return true;
      if (this.range_max == 0)
        return false;
      return this.select_range == ESelectType.Diamond || this.select_range == ESelectType.All;
    }

    public bool IsSupportSkill()
    {
      return this.effect_type == SkillEffectTypes.Buff || this.effect_type == SkillEffectTypes.Debuff;
    }

    public bool IsConditionSkill()
    {
      return this.effect_type == SkillEffectTypes.CureCondition || this.effect_type == SkillEffectTypes.FailCondition || this.effect_type == SkillEffectTypes.DisableCondition;
    }

    public bool IsTrickSkill() => this.effect_type == SkillEffectTypes.SetTrick;

    public bool IsTransformSkill()
    {
      switch (this.effect_type)
      {
        case SkillEffectTypes.TransformUnit:
        case SkillEffectTypes.TransformUnitTakeOverHP:
        case SkillEffectTypes.DynamicTransformUnit:
          return true;
        default:
          return false;
      }
    }

    public bool IsDynamicTransformSkill()
    {
      return this.effect_type == SkillEffectTypes.DynamicTransformUnit;
    }

    public bool IsSetBreakObjSkill() => this.effect_type == SkillEffectTypes.SetBreakObj;

    public bool IsChangeWeatherSkill() => this.effect_type == SkillEffectTypes.ChangeWeather;

    public bool IsJudgeHp(Unit unit)
    {
      if (unit == null || this.condition != ESkillCondition.JudgeHP)
        return false;
      int hp1 = (int) unit.CurrentStatus.param.hp;
      int hp2 = (int) unit.MaximumStatus.param.hp;
      int num = this.JudgeHpCalc == SkillParamCalcTypes.Scale ? hp2 * this.JudgeHpVal / 100 : this.JudgeHpVal;
      return this.mFlags.Get(20) ? hp1 >= num : hp1 <= num;
    }

    public bool IsSelfTargetSelect() => this.range_min <= 0 && this.mFlags.Get(3);

    public bool IsAdvantage()
    {
      switch (this.effect_type)
      {
        case SkillEffectTypes.Attack:
        case SkillEffectTypes.Debuff:
        case SkillEffectTypes.RateDamage:
        case SkillEffectTypes.RateDamageCurrent:
          return false;
        case SkillEffectTypes.FailCondition:
          return this.target == ESkillTarget.Self || this.target == ESkillTarget.SelfSide || this.target == ESkillTarget.SelfSideNotSelf;
        case SkillEffectTypes.SetTrick:
          if (!string.IsNullOrEmpty(this.TrickId))
          {
            TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(this.TrickId);
            if (trickParam != null && trickParam.DamageType == eTrickDamageType.DAMAGE)
              return false;
            break;
          }
          break;
      }
      return true;
    }

    public int CalcCurrentRankValue(int rank, int rankcap, SkillRankUpValue param)
    {
      return param != null ? this.CalcCurrentRankValue(rank, rankcap, param.ini, param.max) : 0;
    }

    public int CalcCurrentRankValueShort(int rank, int rankcap, SkillRankUpValueShort param)
    {
      return param != null ? this.CalcCurrentRankValue(rank, rankcap, (int) param.ini, (int) param.max) : 0;
    }

    public int CalcCurrentRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = (max - ini) * 100 / num1;
      return ini + num3 * num2 / 100;
    }

    public static int CalcSkillEffectValue(SkillParamCalcTypes calctype, int skillval, int target)
    {
      switch (calctype)
      {
        case SkillParamCalcTypes.Add:
          return target + skillval;
        case SkillParamCalcTypes.Scale:
          return target + (int) ((long) target * (long) skillval / 100L);
        default:
          return skillval;
      }
    }

    public bool IsTargetGridNoUnit => this.target == ESkillTarget.GridNoUnit;

    public bool IsTargetValidGrid => this.target == ESkillTarget.ValidGrid;

    public static bool IsTypeLaser(ESelectType type)
    {
      return new List<ESelectType>((IEnumerable<ESelectType>) new ESelectType[5]
      {
        ESelectType.Laser,
        ESelectType.LaserSpread,
        ESelectType.LaserWide,
        ESelectType.LaserTwin,
        ESelectType.LaserTriple
      }).Contains(type);
    }

    public bool IsSkillCountNoLimit => this.count == 0;

    public bool IsProtectSkill() => this.ProtectSkill != null;

    public bool IsProtectReactionSkill()
    {
      return (this.effect_type == SkillEffectTypes.Attack || this.effect_type == SkillEffectTypes.RateDamage || this.effect_type == SkillEffectTypes.RateDamageCurrent) && !this.mFlags.Get(33);
    }

    public bool IsReactionDet(AttackDetailTypes atk_detail_type)
    {
      return this.reaction_det_lists == null || this.reaction_det_lists.Count == 0 || this.reaction_det_lists.Contains(atk_detail_type);
    }

    public bool IsTargetTeleport
    {
      get => this.TeleportType == eTeleportType.BeforeSkill && !this.IsTargetGridNoUnit;
    }

    public static bool IsAagTypeGive(eAbsorbAndGive aag)
    {
      switch (aag)
      {
        case eAbsorbAndGive.Give:
        case eAbsorbAndGive.GiveDiv:
        case eAbsorbAndGive.Same:
        case eAbsorbAndGive.SameDiv:
          return true;
        default:
          return false;
      }
    }

    public static bool IsAagTypeSame(eAbsorbAndGive aag)
    {
      return aag == eAbsorbAndGive.Same || aag == eAbsorbAndGive.SameDiv;
    }

    public static bool IsAagTypeDiv(eAbsorbAndGive aag)
    {
      return aag == eAbsorbAndGive.GiveDiv || aag == eAbsorbAndGive.SameDiv;
    }

    public bool IsForcedTargetingSkillEffect
    {
      get
      {
        switch (this.effect_type)
        {
          case SkillEffectTypes.None:
          case SkillEffectTypes.Attack:
          case SkillEffectTypes.Heal:
          case SkillEffectTypes.Buff:
          case SkillEffectTypes.Debuff:
          case SkillEffectTypes.Shield:
          case SkillEffectTypes.FailCondition:
          case SkillEffectTypes.CureCondition:
          case SkillEffectTypes.DisableCondition:
          case SkillEffectTypes.GemsGift:
          case SkillEffectTypes.GemsIncDec:
          case SkillEffectTypes.Teleport:
          case SkillEffectTypes.Changing:
          case SkillEffectTypes.RateHeal:
          case SkillEffectTypes.RateDamage:
          case SkillEffectTypes.RateDamageCurrent:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsNeedResetDirection
    {
      get
      {
        switch (this.type)
        {
          case ESkillType.Reaction:
          case ESkillType.Additional:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsShowSkillNamePlate
    {
      get
      {
        switch (this.type)
        {
          case ESkillType.Skill:
          case ESkillType.Reaction:
          case ESkillType.Additional:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsAllowCutIn
    {
      get
      {
        switch (this.type)
        {
          case ESkillType.Skill:
          case ESkillType.Additional:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsAdditionalSkill => this.type == ESkillType.Additional;
  }
}
