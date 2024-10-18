// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityData
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
  [MessagePackObject(true)]
  public class AbilityData
  {
    public const long UNIQUE_ID_MAP_EFFECT = -1;
    private UnitData mOwner;
    private OLong mUniqueID = (OLong) 0L;
    private AbilityParam mAbilityParam;
    private OInt mExp = (OInt) 0;
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    private List<SkillData> mSkills;
    public bool IsNoneCategory;
    public bool IsHideList;
    private AbilityData m_BaseAbility;
    private List<AbilityData> m_DeriveAbility;
    private List<SkillData> m_DeriveSkills;
    private AbilityDeriveParam m_AbilityDeriveParam;
    private ConceptCardEffectDecreaseInfo m_ConceptCardEffectDecreaseInfo;

    public UnitData Owner => this.mOwner;

    public long UniqueID => (long) this.mUniqueID;

    public AbilityParam Param => this.mAbilityParam;

    public string AbilityID => this.Param != null ? this.Param.iname : (string) null;

    public string AbilityName => this.Param != null ? this.Param.name : (string) null;

    public int Rank => (int) this.mRank;

    public int Exp => (int) this.mExp;

    public EAbilityType AbilityType => this.Param != null ? this.Param.type : EAbilityType.Active;

    public EAbilitySlot SlotType => this.Param != null ? this.Param.slot : EAbilitySlot.Action;

    public LearningSkill[] LearningSkills
    {
      get => this.Param != null ? this.Param.skills : (LearningSkill[]) null;
    }

    public List<SkillData> Skills
    {
      get => AbilityData.MakeDerivedSkillList(this.mSkills, this.m_DeriveSkills);
    }

    public bool IsDerivedAbility => this.m_BaseAbility != null;

    public bool IsDeriveBaseAbility
    {
      get => this.m_DeriveAbility != null && this.m_DeriveAbility.Count > 0;
    }

    public AbilityData DeriveBaseAbility => this.m_BaseAbility;

    public AbilityDeriveParam DeriveParam => this.m_AbilityDeriveParam;

    public AbilityData DerivedAbility
    {
      get => this.IsDeriveBaseAbility ? this.m_DeriveAbility[0] : (AbilityData) null;
    }

    public bool IsCreatedByConceptCard => this.m_ConceptCardEffectDecreaseInfo != null;

    public void Setup(
      UnitData owner,
      long iid,
      string iname,
      int exp,
      int rank_cap = 0,
      ConceptCardEffectDecreaseInfo decreaseInfo = null)
    {
      if (string.IsNullOrEmpty(iname))
      {
        this.Reset();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        this.mOwner = owner;
        this.mAbilityParam = instance.GetAbilityParam(iname);
        this.m_ConceptCardEffectDecreaseInfo = decreaseInfo;
        this.mUniqueID = (OLong) iid;
        this.mExp = (OInt) exp;
        this.mRankCap = (OInt) this.mAbilityParam.GetRankCap();
        if (rank_cap > 0)
          this.mRankCap = (OInt) rank_cap;
        this.mRank = (OInt) this.CalcRank();
        this.mSkills = (List<SkillData>) null;
        AbilityParam abilityParam = this.Param;
        if (abilityParam.skills == null)
          return;
        this.mSkills = new List<SkillData>(abilityParam.skills.Length);
        this.UpdateLearningsSkill(true);
      }
    }

    private void Reset()
    {
      this.mUniqueID = (OLong) 0L;
      this.mAbilityParam = (AbilityParam) null;
      this.mExp = (OInt) 0;
      this.mRank = (OInt) 1;
      this.mSkills = (List<SkillData>) null;
    }

    public bool IsValid() => this.mAbilityParam != null;

    public int GetRankCap()
    {
      return this.Owner == null ? (int) this.mRankCap : Math.Min((int) this.mRankCap, this.Owner.Lv);
    }

    public int GetRankMaxCap() => Math.Max((int) this.mRankCap, 1);

    public int CalcRank() => Math.Min((int) this.mExp + 1, this.GetRankMaxCap());

    public int GetNextGold()
    {
      return (int) this.mRank < this.GetRankMaxCap() ? MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold((int) this.mRank) : 0;
    }

    public void GainExp(int exp = 1)
    {
      int rankCap = this.GetRankCap();
      if (rankCap < this.Rank)
        return;
      int mRank = (int) this.mRank;
      AbilityData abilityData = this;
      abilityData.mExp = (OInt) ((int) abilityData.mExp + exp);
      this.mRank = (OInt) Math.Min(this.CalcRank(), rankCap);
      this.mExp = (OInt) Math.Max((int) this.mRank - 1, 0);
      if ((int) this.mRank == mRank)
        return;
      for (int index = 0; index < this.mSkills.Count; ++index)
        this.mSkills[index].Setup(this.mSkills[index].SkillID, (int) this.mRank, this.GetRankMaxCap());
      if (this.Owner == null)
        return;
      this.Owner.UpdateAbilityRankUp();
    }

    public void UpdateLearningsSkill(bool locked, List<SkillData> sd_lists = null)
    {
      if (this.mSkills == null)
        return;
      AbilityParam abilityParam = this.Param;
      this.mSkills.Clear();
      if (abilityParam == null || abilityParam.skills.Length == 0)
        return;
      QuestClearUnlockUnitDataParam[] unlocks = (QuestClearUnlockUnitDataParam[]) null;
      if (this.Owner != null)
        unlocks = this.Owner.UnlockedSkills;
      for (int index1 = 0; index1 < abilityParam.skills.Length; ++index1)
      {
        if (!locked || (int) this.mRank >= abilityParam.skills[index1].locklv)
        {
          string skillId = abilityParam.skills[index1].iname;
          if (unlocks != null)
          {
            QuestClearUnlockUnitDataParam unlockUnitDataParam = Array.Find<QuestClearUnlockUnitDataParam>(unlocks, (Predicate<QuestClearUnlockUnitDataParam>) (p => p.old_id == skillId && p.parent_id == this.AbilityID));
            if (unlockUnitDataParam != null && !unlockUnitDataParam.add)
              skillId = unlockUnitDataParam.new_id;
          }
          SkillData skillData = (SkillData) null;
          if (sd_lists != null)
          {
            for (int index2 = 0; index2 < sd_lists.Count; ++index2)
            {
              SkillData sdList = sd_lists[index2];
              if (sdList != null && sdList.SkillID == skillId)
              {
                skillData = sdList;
                break;
              }
            }
          }
          if (skillData == null)
          {
            skillData = new SkillData();
            if (this.IsCreatedByConceptCard)
              skillData.Setup(skillId, (int) this.mRank, (int) this.mRankCap, decreaseInfo: this.m_ConceptCardEffectDecreaseInfo);
            else
              skillData.Setup(skillId, (int) this.mRank, (int) this.mRankCap);
          }
          this.mSkills.Add(skillData);
        }
      }
      if (unlocks != null)
      {
        for (int i = 0; i < unlocks.Length; ++i)
        {
          if (unlocks[i].add && !(unlocks[i].parent_id != this.AbilityID) && this.mSkills.Find((Predicate<SkillData>) (p => p.SkillID == unlocks[i].new_id)) == null)
          {
            string newId = unlocks[i].new_id;
            SkillData skillData = (SkillData) null;
            if (sd_lists != null)
            {
              for (int index = 0; index < sd_lists.Count; ++index)
              {
                SkillData sdList = sd_lists[index];
                if (sdList != null && sdList.SkillID == newId)
                {
                  skillData = sdList;
                  break;
                }
              }
            }
            if (skillData == null)
            {
              skillData = new SkillData();
              if (this.IsCreatedByConceptCard)
                skillData.Setup(newId, (int) this.mRank, (int) this.mRankCap, decreaseInfo: this.m_ConceptCardEffectDecreaseInfo);
              else
                skillData.Setup(newId, (int) this.mRank, (int) this.mRankCap);
            }
            this.mSkills.Add(skillData);
          }
        }
      }
      this.mSkills.ForEach((Action<SkillData>) (skillData => skillData.SetOwnerAbility(this)));
    }

    public void ReplaceSkills(List<SkillData> rpl_lists)
    {
      this.ReplaceSkills(this.mSkills, rpl_lists);
      this.ReplaceSkills(this.m_DeriveSkills, rpl_lists);
    }

    private void ReplaceSkills(List<SkillData> org_lists, List<SkillData> rpl_lists)
    {
      if (org_lists == null || rpl_lists == null)
        return;
      for (int idx = 0; idx < org_lists.Count; ++idx)
      {
        SkillData skillData = rpl_lists.Find((Predicate<SkillData>) (rs => rs.SkillID == org_lists[idx].SkillID));
        if (skillData != null)
          org_lists[idx] = skillData;
      }
    }

    public List<string> GetLearningSkillList2(int rank)
    {
      if (this.Param == null || this.Param.skills == null)
        return (List<string>) null;
      List<string> learningSkillList2 = new List<string>();
      for (int index1 = 0; index1 < this.Param.skills.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(this.Param.skills[index1].iname))
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.Skills.Count; ++index2)
          {
            string str = this.Skills[index2].SkillParam.iname;
            if (!string.IsNullOrEmpty(this.Skills[index2].ReplaceSkillId))
              str = this.Skills[index2].ReplaceSkillId;
            if (this.Skills[index2].IsDerivedSkill)
              str = this.Skills[index2].m_BaseSkillIname;
            if (str == this.Param.skills[index1].iname)
            {
              flag = true;
              break;
            }
          }
          if (!flag && this.Param.skills[index1].locklv <= rank)
            learningSkillList2.Add(this.Param.skills[index1].iname);
        }
      }
      return learningSkillList2;
    }

    public List<string> GetLearningSkillList(int rank)
    {
      if (this.Param == null || this.Param.skills == null)
        return (List<string>) null;
      List<string> learningSkillList = new List<string>();
      for (int index = 0; index < this.Param.skills.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.Param.skills[index].iname) && rank == this.Param.skills[index].locklv)
          learningSkillList.Add(this.Param.skills[index].iname);
      }
      return learningSkillList;
    }

    public bool CheckEnableUseAbility(UnitData self, int job_index)
    {
      return this.Param != null && this.Param.CheckEnableUseAbility(self, job_index);
    }

    public override string ToString()
    {
      return string.Format("[AbilityData: UniqueID={0}, Param={1}, AbilityID={2}, AbilityName={3}, Rank={4}, Exp={5}, AbilityType={6}, SlotType={7}, LearningSkills={8}, Skills={9}]", (object) this.UniqueID, (object) this.Param, (object) this.Param.iname, (object) this.AbilityName, (object) this.Rank, (object) this.Exp, (object) this.AbilityType, (object) this.SlotType, (object) this.LearningSkills, (object) this.Skills);
    }

    public static MixedAbilityData ToMix(AbilityData[] abilitys, string name, string iconName)
    {
      MixedAbilityData mix = new MixedAbilityData();
      mix.mSkills = new List<SkillData>();
      for (int index = 0; index < abilitys.Length; ++index)
      {
        foreach (SkillData skill in abilitys[index].Skills)
          mix.mSkills.Add(skill);
      }
      mix.mAbilityParam = new AbilityParam();
      mix.mAbilityParam.name = name;
      mix.mAbilityParam.icon = iconName;
      mix.mAbilityParam.type = EAbilityType.Active;
      mix.mAbilityParam.slot = EAbilitySlot.Action;
      mix.mAbilityParam.skills = new LearningSkill[1];
      return mix;
    }

    public SkillData FindSkillDataInSkills(string iname)
    {
      foreach (SkillData skill in this.Skills)
      {
        if (skill != null && skill.IsValid() && !(skill.SkillID != iname))
          return skill;
      }
      return (SkillData) null;
    }

    public void UpdateLearningsSkillCollabo(Json_CollaboSkill[] skills)
    {
      if (this.mSkills == null)
        return;
      this.mSkills.Clear();
      if (skills == null || skills.Length == 0)
        return;
      AbilityParam abilityParam = this.Param;
      if (abilityParam == null || abilityParam.skills.Length == 0)
        return;
      List<LearningSkill> learningSkillList = new List<LearningSkill>((IEnumerable<LearningSkill>) abilityParam.skills);
      foreach (Json_CollaboSkill skill in skills)
      {
        Json_CollaboSkill cs = skill;
        if (!string.IsNullOrEmpty(cs.iname) && learningSkillList.Find((Predicate<LearningSkill>) (tls => tls.iname == cs.iname)) != null)
        {
          SkillData skillData = new SkillData();
          skillData.Setup(cs.iname, (int) this.mRank, (int) this.mRankCap);
          skillData.IsCollabo = (OBool) true;
          this.mSkills.Add(skillData);
        }
      }
    }

    public AbilityData CreateDeriveAbility(AbilityDeriveParam deriveParam)
    {
      AbilityData deriveAbility = new AbilityData();
      deriveAbility.Setup(this.Owner, this.UniqueID, deriveParam.DeriveAbilityIname, this.Exp);
      deriveAbility.m_BaseAbility = this;
      deriveAbility.IsNoneCategory = this.IsNoneCategory;
      deriveAbility.m_AbilityDeriveParam = deriveParam;
      return deriveAbility;
    }

    private static List<SkillData> MakeDerivedSkillList(
      List<SkillData> originSkills,
      List<SkillData> deriveSkills)
    {
      if (deriveSkills == null)
        return originSkills;
      List<SkillData> skillDataList = new List<SkillData>();
      for (int i = 0; i < originSkills.Count; ++i)
      {
        List<SkillData> all = deriveSkills.FindAll((Predicate<SkillData>) (ds => ds.m_BaseSkillIname == originSkills[i].SkillParam.iname));
        if (all != null && all.Count > 0)
          skillDataList.AddRange((IEnumerable<SkillData>) all);
        else
          skillDataList.Add(originSkills[i]);
      }
      return skillDataList;
    }

    public void ResetDeriveAbility()
    {
      if (this.IsDerivedAbility)
        this.m_BaseAbility.ResetDeriveAbility();
      if (this.m_DeriveAbility != null)
        this.m_DeriveAbility.Clear();
      if (this.m_DeriveSkills == null)
        return;
      this.m_DeriveSkills.Clear();
    }

    public void AddDeriveAbility(AbilityData deriveAbility)
    {
      if (this.m_DeriveAbility == null)
        this.m_DeriveAbility = new List<AbilityData>();
      this.m_DeriveAbility.Add(deriveAbility);
    }

    public void AddDeriveSkill(SkillData skillData)
    {
      if (this.m_DeriveSkills == null)
        this.m_DeriveSkills = new List<SkillData>();
      this.m_DeriveSkills.Add(skillData);
    }

    public string[] FindDeriveSkillIDs(string baseSkillIname)
    {
      return this.m_DeriveSkills != null ? this.m_DeriveSkills.Where<SkillData>((Func<SkillData, bool>) (skillData => skillData.m_BaseSkillIname == baseSkillIname)).Select<SkillData, string>((Func<SkillData, string>) (skillData => skillData.SkillID)).ToArray<string>() : new string[0];
    }

    public SkillData[] FindDeriveSkills(string baseSkillIname)
    {
      return this.m_DeriveSkills != null ? this.m_DeriveSkills.Where<SkillData>((Func<SkillData, bool>) (skillData => skillData.m_BaseSkillIname == baseSkillIname)).ToArray<SkillData>() : new SkillData[0];
    }
  }
}
