﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class ArtifactParam
  {
    public string[] equip_effects = new string[RarityParam.MAX_RARITY];
    public string[] attack_effects = new string[RarityParam.MAX_RARITY];
    private const string ARTIFACT_EXPR_PREFIX = "_EXPR";
    private const string ARTIFACT_FLAVOR_PREFIX = "_FLAVOR";
    public const int ARTIFACT_RARITY_MAX = 4;
    public string iname;
    public string name;
    public string spec;
    public string asset;
    public string voice;
    public string icon;
    public ArtifactTypes type;
    public int rareini;
    public int raremax;
    public string kakera;
    public bool is_create;
    public int maxnum;
    public string skill;
    public int kcoin;
    public int tcoin;
    public int acoin;
    public int mcoin;
    public int pcoin;
    public int buy;
    public int sell;
    public int enhance_cost;
    public string[] skills;
    public string[] abil_inames;
    public string[] abil_conds;
    public int[] abil_levels;
    public int[] abil_rareties;
    public int[] abil_shows;
    public string tag;
    public int condition_lv;
    public string[] condition_units;
    public string[] condition_jobs;
    public string condition_birth;
    public ESex condition_sex;
    public EElement condition_element;
    public OInt condition_raremin;
    public OInt condition_raremax;
    public int CachedKakeraCount;
    public string CondSkillMotion;
    public int skin_hide;
    public string insp_skill;
    public int insp_lv_bonus;
    private SkillMotionParam m_SkillMotionParam;

    public string Expr
    {
      get
      {
        return this.GetText("external_artifact", this.iname + "_EXPR");
      }
    }

    public string Flavor
    {
      get
      {
        return this.GetText("external_artifact", this.iname + "_FLAVOR");
      }
    }

    public bool HasAbility
    {
      get
      {
        if (this.abil_inames != null)
          return this.abil_inames.Length > 0;
        return false;
      }
    }

    public bool Deserialize(JSON_ArtifactParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.spec = json.spec;
      this.asset = json.asset;
      this.voice = json.voice;
      this.icon = json.icon;
      this.tag = json.tag;
      this.type = (ArtifactTypes) json.type;
      this.rareini = json.rini;
      this.raremax = json.rmax;
      this.kakera = json.kakera;
      this.maxnum = json.maxnum;
      this.is_create = json.notsmn == 0;
      this.skills = (string[]) null;
      if (json.skills != null)
      {
        this.skills = new string[json.skills.Length];
        for (int index = 0; index < json.skills.Length; ++index)
          this.skills[index] = json.skills[index];
      }
      Array.Clear((Array) this.equip_effects, 0, this.equip_effects.Length);
      this.equip_effects[0] = json.equip1;
      this.equip_effects[1] = json.equip2;
      this.equip_effects[2] = json.equip3;
      this.equip_effects[3] = json.equip4;
      this.equip_effects[4] = json.equip5;
      Array.Clear((Array) this.attack_effects, 0, this.attack_effects.Length);
      this.attack_effects[0] = json.attack1;
      this.attack_effects[1] = json.attack2;
      this.attack_effects[2] = json.attack3;
      this.attack_effects[3] = json.attack4;
      this.attack_effects[4] = json.attack5;
      this.abil_inames = (string[]) null;
      this.abil_levels = (int[]) null;
      this.abil_rareties = (int[]) null;
      this.abil_shows = (int[]) null;
      this.abil_conds = (string[]) null;
      if (json.abils != null && json.ablvs != null && (json.abrares != null && json.abshows != null) && (json.abconds != null && json.abils.Length == json.ablvs.Length && (json.abils.Length == json.abrares.Length && json.abils.Length == json.abshows.Length)) && json.abils.Length == json.abconds.Length)
      {
        this.abil_inames = new string[json.abils.Length];
        this.abil_levels = new int[json.ablvs.Length];
        this.abil_rareties = new int[json.abrares.Length];
        this.abil_shows = new int[json.abshows.Length];
        this.abil_conds = new string[json.abconds.Length];
        for (int index = 0; index < json.ablvs.Length; ++index)
        {
          this.abil_inames[index] = json.abils[index];
          this.abil_levels[index] = json.ablvs[index];
          this.abil_rareties[index] = json.abrares[index];
          this.abil_shows[index] = json.abshows[index];
          this.abil_conds[index] = json.abconds[index];
        }
      }
      this.kcoin = json.kc;
      this.tcoin = json.tc;
      this.acoin = json.ac;
      this.mcoin = json.mc;
      this.pcoin = json.pp;
      this.buy = json.buy;
      this.sell = json.sell;
      this.enhance_cost = json.ecost;
      this.condition_lv = json.eqlv;
      this.condition_sex = (ESex) json.sex;
      this.condition_birth = json.birth;
      this.condition_element = (EElement) json.elem;
      this.condition_units = (string[]) null;
      this.condition_jobs = (string[]) null;
      this.condition_raremin = (OInt) json.eqrmin;
      this.condition_raremax = (OInt) json.eqrmax;
      if (json.units != null && json.units.Length > 0)
      {
        this.condition_units = new string[json.units.Length];
        for (int index = 0; index < json.units.Length; ++index)
          this.condition_units[index] = json.units[index];
      }
      if (json.jobs != null && json.jobs.Length > 0)
      {
        this.condition_jobs = new string[json.jobs.Length];
        for (int index = 0; index < json.jobs.Length; ++index)
          this.condition_jobs[index] = json.jobs[index];
      }
      this.CondSkillMotion = json.cond_sm;
      this.skin_hide = json.skin_hide;
      this.insp_skill = json.insp_skill;
      this.insp_lv_bonus = json.insp_lv_bonus;
      return true;
    }

    public bool CheckEnableEquip(UnitData self, int jobIndex = -1)
    {
      if (!string.IsNullOrEmpty(this.CondSkillMotion))
      {
        if (this.m_SkillMotionParam == null)
          this.m_SkillMotionParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillMotionParam(this.CondSkillMotion);
        if (this.m_SkillMotionParam == null)
          DebugUtility.LogError("「cond_sm」に不正なIDが指定されています。 => " + this.CondSkillMotion);
        else if (this.m_SkillMotionParam.CheckPassCondition(self))
          return true;
      }
      if (this.condition_units != null && Array.IndexOf<string>(this.condition_units, self.UnitParam.iname) < 0)
        return false;
      if (this.condition_jobs != null)
      {
        if (self.CurrentJob == null)
          return false;
        if (jobIndex < 0)
        {
          JobParam jobParam1 = self.CurrentJob.Param;
          if (Array.IndexOf<string>(this.condition_jobs, jobParam1.iname) < 0)
          {
            if (string.IsNullOrEmpty(jobParam1.origin))
              return false;
            JobParam jobParam2 = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(jobParam1.origin);
            if (jobParam2 == null || Array.IndexOf<string>(this.condition_jobs, jobParam2.iname) < 0)
              return false;
          }
        }
        else
        {
          if (jobIndex >= self.Jobs.Length)
            return false;
          JobParam jobParam1 = self.Jobs[jobIndex].Param;
          if (Array.IndexOf<string>(this.condition_jobs, jobParam1.iname) < 0)
          {
            if (string.IsNullOrEmpty(jobParam1.origin))
              return false;
            JobParam jobParam2 = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(jobParam1.origin);
            if (jobParam2 == null || Array.IndexOf<string>(this.condition_jobs, jobParam2.iname) < 0)
              return false;
          }
        }
      }
      if (!string.IsNullOrEmpty(this.condition_birth) && (string) self.UnitParam.birth != this.condition_birth || this.condition_sex != ESex.Unknown && self.UnitParam.sex != this.condition_sex || this.condition_element != EElement.None && self.Element != this.condition_element || (int) this.condition_raremax != 0 && ((int) this.condition_raremin > self.Rarity || (int) this.condition_raremax < self.Rarity || (int) this.condition_raremax < (int) this.condition_raremin))
        return false;
      if (this.type == ArtifactTypes.Arms && self.Jobs != null)
      {
        JobParam jobParam = jobIndex >= 0 ? (jobIndex >= self.Jobs.Length ? (JobParam) null : self.Jobs[jobIndex].Param) : self.CurrentJob.Param;
        if (!string.IsNullOrEmpty(jobParam.artifact) && this.tag != MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(jobParam.artifact).tag)
          return false;
      }
      return true;
    }

    public int GetBuyNum(ESaleType type)
    {
      switch (type)
      {
        case ESaleType.Gold:
          return this.buy;
        case ESaleType.Coin:
          return this.kcoin;
        case ESaleType.TourCoin:
          return this.tcoin;
        case ESaleType.ArenaCoin:
          return this.acoin;
        case ESaleType.PiecePoint:
          return this.pcoin;
        case ESaleType.MultiCoin:
          return this.mcoin;
        case ESaleType.EventCoin:
          return 0;
        case ESaleType.Coin_P:
          return this.kcoin;
        default:
          return 0;
      }
    }

    public string GetText(string table, string key)
    {
      string str = LocalizedText.Get(table + "." + key);
      if (str.Equals(key))
        return string.Empty;
      return str;
    }

    public static bool IsDetachableArtifact(ArtifactParam param)
    {
      return !string.IsNullOrEmpty(param.icon) && !param.icon.Contains("_SKIN_") && !param.icon.Contains("_SK_");
    }
  }
}
