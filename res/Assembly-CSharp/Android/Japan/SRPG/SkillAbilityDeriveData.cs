// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class SkillAbilityDeriveData
  {
    public HashSet<SkillAbilityDeriveParam> m_AdditionalSkillAbilityDeriveParam = new HashSet<SkillAbilityDeriveParam>();
    public SkillAbilityDeriveParam m_SkillAbilityDeriveParam;
    public List<SkillDeriveData> m_SkillDeriveData;
    public List<AbilityDeriveData> m_AbilityDeriveData;

    public int MasterIndex
    {
      get
      {
        return this.m_SkillAbilityDeriveParam.m_OriginIndex;
      }
    }

    public bool CheckContainsTriggerIname(ESkillAbilityDeriveConds triggerType, string triggerIname)
    {
      return this.m_SkillAbilityDeriveParam.CheckContainsTriggerIname(triggerType, triggerIname);
    }

    public bool CheckContainsTriggerInames(SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam)
    {
      return this.m_SkillAbilityDeriveParam.CheckContainsTriggerInames(searchKeyTriggerParam);
    }

    public void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam, List<SkillAbilityDeriveParam> additionalSkillAbilityDeriveParams)
    {
      this.m_SkillDeriveData = new List<SkillDeriveData>();
      this.m_AbilityDeriveData = new List<AbilityDeriveData>();
      this.m_SkillAbilityDeriveParam = skillAbilityDeriveParam;
      IEnumerable<SkillDeriveData> collection1 = this.m_SkillAbilityDeriveParam.SkillDeriveParams.Select<SkillDeriveParam, SkillDeriveData>((Func<SkillDeriveParam, SkillDeriveData>) (param => new SkillDeriveData(param)));
      IEnumerable<AbilityDeriveData> collection2 = this.m_SkillAbilityDeriveParam.AbilityDeriveParams.Select<AbilityDeriveParam, AbilityDeriveData>((Func<AbilityDeriveParam, AbilityDeriveData>) (param => new AbilityDeriveData(param)));
      this.m_SkillDeriveData.AddRange(collection1);
      this.m_AbilityDeriveData.AddRange(collection2);
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (SkillDeriveData skillDeriveData in this.m_SkillDeriveData)
      {
        if (!dictionary.ContainsKey(skillDeriveData.Param.BaseSkillIname))
          dictionary.Add(skillDeriveData.Param.BaseSkillIname, this.MasterIndex);
      }
      foreach (AbilityDeriveData abilityDeriveData in this.m_AbilityDeriveData)
      {
        if (!dictionary.ContainsKey(abilityDeriveData.Param.BaseAbilityIname))
          dictionary.Add(abilityDeriveData.Param.BaseAbilityIname, this.MasterIndex);
      }
      foreach (SkillAbilityDeriveParam abilityDeriveParam1 in additionalSkillAbilityDeriveParams)
      {
        if (!this.m_AdditionalSkillAbilityDeriveParam.Contains(abilityDeriveParam1))
          this.m_AdditionalSkillAbilityDeriveParam.Add(abilityDeriveParam1);
        List<SkillDeriveParam> skillDeriveParams = abilityDeriveParam1.SkillDeriveParams;
        List<AbilityDeriveParam> abilityDeriveParams = abilityDeriveParam1.AbilityDeriveParams;
        foreach (SkillDeriveParam skillDeriveParam in skillDeriveParams)
        {
          int b = -1;
          if (dictionary.TryGetValue(skillDeriveParam.BaseSkillIname, out b))
          {
            b = Mathf.Min(skillDeriveParam.MasterIndex, b);
            dictionary[skillDeriveParam.BaseSkillIname] = b;
          }
          else
            dictionary.Add(skillDeriveParam.BaseSkillIname, skillDeriveParam.MasterIndex);
          this.m_SkillDeriveData.Add(new SkillDeriveData(skillDeriveParam)
          {
            IsAdd = true
          });
        }
        foreach (AbilityDeriveParam abilityDeriveParam2 in abilityDeriveParams)
        {
          int b = -1;
          if (dictionary.TryGetValue(abilityDeriveParam2.BaseAbilityIname, out b))
          {
            b = Mathf.Min(abilityDeriveParam2.MasterIndex, b);
            dictionary[abilityDeriveParam2.BaseAbilityIname] = b;
          }
          else
            dictionary.Add(abilityDeriveParam2.BaseAbilityIname, abilityDeriveParam2.MasterIndex);
          this.m_AbilityDeriveData.Add(new AbilityDeriveData(abilityDeriveParam2)
          {
            IsAdd = true
          });
        }
      }
      foreach (AbilityDeriveData abilityDeriveData in this.m_AbilityDeriveData)
      {
        int num = -1;
        if (dictionary.TryGetValue(abilityDeriveData.Param.BaseAbilityIname, out num))
          abilityDeriveData.IsDisable = abilityDeriveData.Param.MasterIndex > num;
      }
      foreach (SkillDeriveData skillDeriveData in this.m_SkillDeriveData)
      {
        int num = -1;
        if (dictionary.TryGetValue(skillDeriveData.Param.BaseSkillIname, out num))
          skillDeriveData.IsDisable = skillDeriveData.Param.MasterIndex > num;
      }
    }

    public List<SkillDeriveParam> GetAvailableSkillDeriveParams()
    {
      return this.m_SkillDeriveData.Where<SkillDeriveData>((Func<SkillDeriveData, bool>) (deriveData => !deriveData.IsDisable)).Select<SkillDeriveData, SkillDeriveParam>((Func<SkillDeriveData, SkillDeriveParam>) (deriveData => deriveData.Param)).ToList<SkillDeriveParam>();
    }

    public List<AbilityDeriveParam> GetAvailableAbilityDeriveParams()
    {
      return this.m_AbilityDeriveData.Where<AbilityDeriveData>((Func<AbilityDeriveData, bool>) (deriveData => !deriveData.IsDisable)).Select<AbilityDeriveData, AbilityDeriveParam>((Func<AbilityDeriveData, AbilityDeriveParam>) (deriveData => deriveData.Param)).ToList<AbilityDeriveParam>();
    }
  }
}
