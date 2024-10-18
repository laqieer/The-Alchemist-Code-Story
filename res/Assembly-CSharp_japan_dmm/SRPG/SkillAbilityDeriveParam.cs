// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  [MessagePackObject(false)]
  public class SkillAbilityDeriveParam
  {
    [Key(1)]
    public string iname;
    [Key(2)]
    public SkillAbilityDeriveTriggerParam[] deriveTriggers;
    [Key(3)]
    public string[] base_abils;
    [Key(4)]
    public string[] derive_abils;
    [Key(5)]
    public string[] base_skills;
    [Key(6)]
    public string[] derive_skills;
    [Key(0)]
    public int m_OriginIndex;
    private List<SkillDeriveParam> m_SkillDeriveParams;
    private List<AbilityDeriveParam> m_AbilityDeriveParams;

    [SerializationConstructor]
    public SkillAbilityDeriveParam(int index) => this.m_OriginIndex = index;

    [Key(7)]
    public List<SkillDeriveParam> SkillDeriveParams => this.m_SkillDeriveParams;

    [Key(8)]
    public List<AbilityDeriveParam> AbilityDeriveParams => this.m_AbilityDeriveParams;

    public string GetTriggerArtifactIname(int index)
    {
      if (this.deriveTriggers != null && index < this.deriveTriggers.Length)
      {
        SkillAbilityDeriveTriggerParam deriveTrigger = this.deriveTriggers[index];
        if (deriveTrigger.m_TriggerType == ESkillAbilityDeriveConds.EquipArtifact)
          return deriveTrigger.m_TriggerIname;
      }
      return string.Empty;
    }

    public ArtifactParam GetTriggerArtifactParam(int index)
    {
      string triggerArtifactIname = this.GetTriggerArtifactIname(index);
      return string.IsNullOrEmpty(triggerArtifactIname) ? (ArtifactParam) null : MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(triggerArtifactIname);
    }

    private void Deserialize(JSON_SkillAbilityDeriveParam json)
    {
      this.iname = json.iname;
      ESkillAbilityDeriveConds[] array1 = ((IEnumerable<ESkillAbilityDeriveConds>) new ESkillAbilityDeriveConds[3]
      {
        (ESkillAbilityDeriveConds) json.trig_type_1,
        (ESkillAbilityDeriveConds) json.trig_type_2,
        (ESkillAbilityDeriveConds) json.trig_type_3
      }).Where<ESkillAbilityDeriveConds>((Func<ESkillAbilityDeriveConds, bool>) (trig_type => trig_type != ESkillAbilityDeriveConds.Unknown)).ToArray<ESkillAbilityDeriveConds>();
      string[] array2 = ((IEnumerable<string>) new string[3]
      {
        json.trig_iname_1,
        json.trig_iname_2,
        json.trig_iname_3
      }).Where<string>((Func<string, bool>) (trig_iname => !string.IsNullOrEmpty(trig_iname))).ToArray<string>();
      this.deriveTriggers = new SkillAbilityDeriveTriggerParam[array2.Length];
      for (int index = 0; index < array2.Length; ++index)
        this.deriveTriggers[index] = new SkillAbilityDeriveTriggerParam(array1[index], array2[index]);
      if (json.base_abils != null)
      {
        this.base_abils = new string[json.base_abils.Length];
        for (int index = 0; index < this.base_abils.Length; ++index)
          this.base_abils[index] = json.base_abils[index];
      }
      if (json.derive_abils != null)
      {
        this.derive_abils = new string[json.derive_abils.Length];
        for (int index = 0; index < this.derive_abils.Length; ++index)
          this.derive_abils[index] = json.derive_abils[index];
      }
      if (json.base_skills != null)
      {
        this.base_skills = new string[json.base_skills.Length];
        for (int index = 0; index < this.base_skills.Length; ++index)
          this.base_skills[index] = json.base_skills[index];
      }
      if (json.base_skills == null)
        return;
      this.derive_skills = new string[json.derive_skills.Length];
      for (int index = 0; index < this.derive_skills.Length; ++index)
        this.derive_skills[index] = json.derive_skills[index];
    }

    public void Deserialize(JSON_SkillAbilityDeriveParam json, MasterParam masterParam)
    {
      this.Deserialize(json);
      this.FindSkillAbilityDeriveParams(masterParam);
    }

    private void FindSkillAbilityDeriveParams(MasterParam masterParam)
    {
      if (masterParam == null)
        return;
      this.m_SkillDeriveParams = this.GetSkillDeriveParams(masterParam);
      this.m_AbilityDeriveParams = this.GetAbilityDeriveParams(masterParam);
    }

    public bool CheckContainsTriggerIname(
      ESkillAbilityDeriveConds conditionsType,
      string triggerIname)
    {
      if (string.IsNullOrEmpty(triggerIname))
        return false;
      int hashCode = triggerIname.GetHashCode();
      for (int index = 0; index < this.deriveTriggers.Length; ++index)
      {
        if (this.deriveTriggers[index].m_TriggerType == conditionsType && !string.IsNullOrEmpty(this.deriveTriggers[index].m_TriggerIname) && this.deriveTriggers[index].m_TriggerIname.GetHashCode() == hashCode)
          return true;
      }
      return false;
    }

    public bool CheckContainsTriggerInames(
      SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam)
    {
      if (searchKeyTriggerParam == null || searchKeyTriggerParam.Length != this.deriveTriggers.Length)
        return false;
      bool[] source = new bool[searchKeyTriggerParam.Length];
      for (int index1 = 0; index1 < this.deriveTriggers.Length; ++index1)
      {
        if (this.deriveTriggers[index1].m_TriggerType == searchKeyTriggerParam[index1].m_TriggerType && !string.IsNullOrEmpty(this.deriveTriggers[index1].m_TriggerIname))
        {
          for (int index2 = 0; index2 < searchKeyTriggerParam.Length; ++index2)
          {
            if (!source[index2] && !string.IsNullOrEmpty(searchKeyTriggerParam[index2].m_TriggerIname) && this.deriveTriggers[index1].m_TriggerIname.GetHashCode() == searchKeyTriggerParam[index2].m_TriggerIname.GetHashCode())
            {
              source[index2] = true;
              break;
            }
          }
        }
      }
      return ((IEnumerable<bool>) source).Count<bool>((Func<bool, bool>) (value => value)) >= searchKeyTriggerParam.Length;
    }

    private List<SkillDeriveParam> GetSkillDeriveParams(MasterParam masterParam)
    {
      List<SkillDeriveParam> skillDeriveParams = new List<SkillDeriveParam>();
      if (this.base_skills == null || this.derive_skills == null)
        return skillDeriveParams;
      for (int index = 0; index < this.base_skills.Length; ++index)
      {
        string baseSkill = this.base_skills[index];
        string deriveSkill = this.derive_skills[index];
        if ((!string.IsNullOrEmpty(baseSkill) || !string.IsNullOrEmpty(deriveSkill)) && !string.IsNullOrEmpty(baseSkill) && !string.IsNullOrEmpty(deriveSkill))
        {
          SkillDeriveParam skillDeriveParam = new SkillDeriveParam();
          skillDeriveParam.m_BaseParam = masterParam.GetSkillParam(baseSkill);
          skillDeriveParam.m_DeriveParam = masterParam.GetSkillParam(deriveSkill);
          skillDeriveParam.m_SkillAbilityDeriveParam = this;
          skillDeriveParams.Add(skillDeriveParam);
        }
      }
      return skillDeriveParams;
    }

    private List<AbilityDeriveParam> GetAbilityDeriveParams(MasterParam masterParam)
    {
      List<AbilityDeriveParam> abilityDeriveParams = new List<AbilityDeriveParam>();
      if (this.base_abils == null || this.derive_abils == null)
        return abilityDeriveParams;
      for (int index = 0; index < this.base_abils.Length; ++index)
      {
        string baseAbil = this.base_abils[index];
        string deriveAbil = this.derive_abils[index];
        if ((!string.IsNullOrEmpty(baseAbil) || !string.IsNullOrEmpty(deriveAbil)) && !string.IsNullOrEmpty(baseAbil) && !string.IsNullOrEmpty(deriveAbil))
        {
          AbilityDeriveParam abilityDeriveParam = new AbilityDeriveParam();
          abilityDeriveParam.m_BaseParam = masterParam.GetAbilityParam(baseAbil);
          abilityDeriveParam.m_DeriveParam = masterParam.GetAbilityParam(deriveAbil);
          abilityDeriveParam.m_SkillAbilityDeriveParam = this;
          abilityDeriveParams.Add(abilityDeriveParam);
        }
      }
      return abilityDeriveParams;
    }
  }
}
