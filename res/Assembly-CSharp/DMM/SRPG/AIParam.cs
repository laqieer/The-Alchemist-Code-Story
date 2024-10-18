﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AIParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class AIParam
  {
    public string iname;
    public RoleTypes role;
    public ParamTypes param;
    public ParamPriorities param_prio;
    public OLong flags = (OLong) 0L;
    public OInt escape_border = (OInt) 0;
    public OInt heal_border = (OInt) 0;
    public OInt gems_border = (OInt) 0;
    public OInt buff_border = (OInt) 0;
    public OInt cond_border = (OInt) 0;
    public OInt safe_border = (OInt) 0;
    public OInt gosa_border = (OInt) 0;
    public OInt DisableSupportActionHpBorder;
    public OInt DisableSupportActionMemberBorder;
    public SkillCategory[] SkillCategoryPriorities;
    public ParamTypes[] BuffPriorities;
    public EUnitCondition[] ConditionPriorities;

    public bool Deserialize(JSON_AIParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.role = (RoleTypes) json.role;
      this.param = (ParamTypes) json.prm;
      this.param_prio = (ParamPriorities) json.prmprio;
      if (json.best != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 1L);
      }
      if (json.sneak != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 2L);
      }
      if (json.notmov != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 4L);
      }
      if (json.notact != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 8L);
      }
      if (json.notskl != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 16L);
      }
      if (json.notavo != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 32L);
      }
      if (json.csff != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 64L);
      }
      if (json.notmpd != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 128L);
      }
      if (json.buff_self != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 256L);
      }
      if (json.notprio != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 512L);
      }
      if (json.use_old_sort != 0)
      {
        AIParam aiParam = this;
        aiParam.flags = (OLong) ((long) aiParam.flags | 1024L);
      }
      this.escape_border = (OInt) json.sos;
      this.heal_border = (OInt) json.heal;
      this.gems_border = (OInt) json.gems;
      this.buff_border = (OInt) json.buff_border;
      this.cond_border = (OInt) json.cond_border;
      this.safe_border = (OInt) json.safe_border;
      this.gosa_border = (OInt) json.gosa_border;
      this.DisableSupportActionHpBorder = (OInt) json.notsup_hp;
      this.DisableSupportActionMemberBorder = (OInt) json.notsup_num;
      this.SkillCategoryPriorities = (SkillCategory[]) null;
      this.BuffPriorities = (ParamTypes[]) null;
      this.ConditionPriorities = (EUnitCondition[]) null;
      if (json.skil_prio != null)
      {
        this.SkillCategoryPriorities = new SkillCategory[json.skil_prio.Length];
        for (int index = 0; index < json.skil_prio.Length; ++index)
          this.SkillCategoryPriorities[index] = (SkillCategory) json.skil_prio[index];
      }
      if (json.buff_prio != null)
      {
        this.BuffPriorities = new ParamTypes[json.buff_prio.Length];
        for (int index = 0; index < json.buff_prio.Length; ++index)
          this.BuffPriorities[index] = (ParamTypes) json.buff_prio[index];
      }
      if (json.cond_prio != null)
      {
        this.ConditionPriorities = new EUnitCondition[json.cond_prio.Length];
        for (int index = 0; index < json.cond_prio.Length; ++index)
          this.ConditionPriorities[index] = (EUnitCondition) (1L << json.cond_prio[index]);
      }
      return true;
    }

    public bool CheckFlag(AIFlags flag) => ((long) this.flags & (long) flag) != 0L;
  }
}
