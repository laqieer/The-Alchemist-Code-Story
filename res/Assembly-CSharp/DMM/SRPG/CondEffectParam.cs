﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CondEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class CondEffectParam
  {
    public string iname;
    public string job;
    public string buki;
    public string birth;
    public ESex sex;
    public EElement elem;
    public ConditionEffectTypes type;
    public ESkillCondition cond;
    public OInt value_ini;
    public OInt value_max;
    public OInt rate_ini;
    public OInt rate_max;
    public OInt turn_ini;
    public OInt turn_max;
    public EffectCheckTargets chk_target;
    public EffectCheckTimings chk_timing;
    public EUnitCondition[] conditions;
    public string[] BuffIds;
    public string[] tags;
    public string un_group;
    public string[] custom_targets;
    public OInt v_poison_rate;
    public OInt v_poison_fix;
    public OInt v_paralyse_rate;
    public OInt v_blink_hit;
    public OInt v_blink_avo;
    public OInt v_death_count;
    public OInt v_berserk_atk;
    public OInt v_berserk_def;
    public OInt v_fast;
    public OInt v_slow;
    public OInt v_donmov;
    public OInt v_auto_hp_heal;
    public OInt v_auto_hp_heal_fix;
    public OInt v_auto_mp_heal;
    public OInt v_auto_mp_heal_fix;
    public OInt curse;
    private BitArray mFlags = new BitArray(2);

    public bool IsLinkBuffDupli => this.mFlags.Get(0);

    public bool IsLinkBuffResist => this.mFlags.Get(1);

    public bool Deserialize(JSON_CondEffectParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.job = json.job;
      this.buki = json.buki;
      this.birth = json.birth;
      this.sex = (ESex) json.sex;
      this.elem = (EElement) json.elem;
      this.cond = (ESkillCondition) json.cond;
      this.type = (ConditionEffectTypes) json.type;
      this.chk_target = (EffectCheckTargets) json.chktgt;
      this.chk_timing = (EffectCheckTimings) json.timing;
      this.value_ini = (OInt) json.vini;
      this.value_max = (OInt) json.vmax;
      this.rate_ini = (OInt) json.rini;
      this.rate_max = (OInt) json.rmax;
      this.turn_ini = (OInt) json.tini;
      this.turn_max = (OInt) json.tmax;
      this.curse = (OInt) json.curse;
      this.conditions = (EUnitCondition[]) null;
      if (json.conds != null)
      {
        this.conditions = new EUnitCondition[json.conds.Length];
        for (int index = 0; index < json.conds.Length; ++index)
        {
          if (json.conds[index] >= 0)
            this.conditions[index] = (EUnitCondition) (1L << json.conds[index]);
        }
      }
      this.BuffIds = (string[]) null;
      if (json.buffs != null)
      {
        this.BuffIds = new string[json.buffs.Length];
        for (int index = 0; index < json.buffs.Length; ++index)
          this.BuffIds[index] = json.buffs[index];
      }
      this.mFlags.Set(0, json.is_lb_dupli != 0);
      this.mFlags.Set(1, json.is_lb_resist != 0);
      if (!string.IsNullOrEmpty(json.tag))
      {
        this.tags = json.tag.Split(',');
        if (this.tags != null && this.tags.Length == 0)
          this.tags = (string[]) null;
      }
      this.un_group = json.un_group;
      if (json.custom_targets != null)
      {
        this.custom_targets = new string[json.custom_targets.Length];
        for (int index = 0; index < json.custom_targets.Length; ++index)
          this.custom_targets[index] = json.custom_targets[index];
      }
      this.v_poison_rate = (OInt) json.v_poi;
      this.v_poison_fix = (OInt) json.v_poifix;
      this.v_paralyse_rate = (OInt) json.v_par;
      this.v_blink_hit = (OInt) json.v_blihit;
      this.v_blink_avo = (OInt) json.v_bliavo;
      this.v_death_count = (OInt) json.v_dea;
      this.v_berserk_atk = (OInt) json.v_beratk;
      this.v_berserk_def = (OInt) json.v_berdef;
      this.v_fast = (OInt) json.v_fast;
      this.v_slow = (OInt) json.v_slow;
      this.v_donmov = (OInt) json.v_don;
      this.v_auto_hp_heal = (OInt) json.v_ahp;
      this.v_auto_mp_heal = (OInt) json.v_amp;
      this.v_auto_hp_heal_fix = (OInt) json.v_ahpfix;
      this.v_auto_mp_heal_fix = (OInt) json.v_ampfix;
      return true;
    }

    public string GetLinkageBuffId(EUnitCondition cond)
    {
      if (this.conditions == null || this.BuffIds == null)
        return (string) null;
      for (int index = 0; index < this.conditions.Length; ++index)
      {
        if (this.conditions[index] == cond)
          return index < this.BuffIds.Length ? this.BuffIds[index] : (string) null;
      }
      return (string) null;
    }

    private enum eFlags
    {
      LINK_BUFF_DUPLI,
      LINK_BUFF_RESIST,
      MAX,
    }
  }
}
