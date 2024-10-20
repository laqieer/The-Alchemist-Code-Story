﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class AbilityData
  {
    private OLong mUniqueID = (OLong) 0L;
    private OInt mExp = (OInt) 0;
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    public const long UNIQUE_ID_MAP_EFFECT = -1;
    private UnitData mOwner;
    private AbilityParam mAbilityParam;
    private List<SkillData> mSkills;
    public bool IsNoneCategory;
    public bool IsHideList;

    public UnitData Owner
    {
      get
      {
        return this.mOwner;
      }
    }

    public long UniqueID
    {
      get
      {
        return (long) this.mUniqueID;
      }
    }

    public AbilityParam Param
    {
      get
      {
        return this.mAbilityParam;
      }
    }

    public string AbilityID
    {
      get
      {
        if (this.Param != null)
          return this.Param.iname;
        return (string) null;
      }
    }

    public string AbilityName
    {
      get
      {
        if (this.Param != null)
          return this.Param.name;
        return (string) null;
      }
    }

    public int Rank
    {
      get
      {
        return (int) this.mRank;
      }
    }

    public int Exp
    {
      get
      {
        return (int) this.mExp;
      }
    }

    public EAbilityType AbilityType
    {
      get
      {
        if (this.Param != null)
          return this.Param.type;
        return EAbilityType.Active;
      }
    }

    public EAbilitySlot SlotType
    {
      get
      {
        if (this.Param != null)
          return this.Param.slot;
        return EAbilitySlot.Action;
      }
    }

    public LearningSkill[] LearningSkills
    {
      get
      {
        if (this.Param != null)
          return this.Param.skills;
        return (LearningSkill[]) null;
      }
    }

    public List<SkillData> Skills
    {
      get
      {
        return this.mSkills;
      }
    }

    public void Setup(UnitData owner, long iid, string iname, int exp)
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
        this.mUniqueID = (OLong) iid;
        this.mExp = (OInt) exp;
        this.mRankCap = (OInt) this.mAbilityParam.GetRankCap();
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

    public bool IsValid()
    {
      return this.mAbilityParam != null;
    }

    public int GetRankCap()
    {
      if (this.Owner == null)
        return (int) this.mRankCap;
      return Math.Min((int) this.mRankCap, this.Owner.Lv);
    }

    public int GetRankMaxCap()
    {
      return Math.Max((int) this.mRankCap, 1);
    }

    public int CalcRank()
    {
      return Math.Min((int) this.mExp + 1, this.GetRankMaxCap());
    }

    public int GetNextGold()
    {
      if ((int) this.mRank < this.GetRankMaxCap())
        return MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold((int) this.mRank);
      return 0;
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
        this.mSkills[index].Setup(this.mSkills[index].SkillID, (int) this.mRank, this.GetRankMaxCap(), (MasterParam) null);
      if (this.Owner == null)
        return;
      this.Owner.UpdateAbilityRankUp();
    }

    public void UpdateLearningsSkill(bool locked)
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
          SkillData skillData = new SkillData();
          string skillId = abilityParam.skills[index1].iname;
          if (unlocks != null)
          {
            int index2 = Array.FindIndex<QuestClearUnlockUnitDataParam>(unlocks, (Predicate<QuestClearUnlockUnitDataParam>) (p => p.old_id == skillId));
            if (index2 != -1 && !unlocks[index2].add && unlocks[index2].parent_id == this.AbilityID)
              skillId = unlocks[index2].new_id;
          }
          skillData.Setup(skillId, (int) this.mRank, (int) this.mRankCap, (MasterParam) null);
          this.mSkills.Add(skillData);
        }
      }
      if (unlocks == null)
        return;
      for (int i = 0; i < unlocks.Length; ++i)
      {
        if (unlocks[i].add && !(unlocks[i].parent_id != this.AbilityID) && this.mSkills.Find((Predicate<SkillData>) (p => p.SkillID == unlocks[i].new_id)) == null)
        {
          SkillData skillData = new SkillData();
          skillData.Setup(unlocks[i].new_id, (int) this.mRank, (int) this.mRankCap, (MasterParam) null);
          this.mSkills.Add(skillData);
        }
      }
    }

    public List<string> GetLearningSkillList2(int rank)
    {
      if (this.Param == null || this.Param.skills == null)
        return (List<string>) null;
      List<string> stringList = new List<string>();
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
            if (str == this.Param.skills[index1].iname)
            {
              flag = true;
              break;
            }
          }
          if (!flag && this.Param.skills[index1].locklv <= rank)
            stringList.Add(this.Param.skills[index1].iname);
        }
      }
      return stringList;
    }

    public List<string> GetLearningSkillList(int rank)
    {
      if (this.Param == null || this.Param.skills == null)
        return (List<string>) null;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Param.skills.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.Param.skills[index].iname) && rank == this.Param.skills[index].locklv)
          stringList.Add(this.Param.skills[index].iname);
      }
      return stringList;
    }

    public bool CheckEnableUseAbility(UnitData self, int job_index)
    {
      if (this.Param != null)
        return this.Param.CheckEnableUseAbility(self, job_index);
      return false;
    }

    public override string ToString()
    {
      return string.Format("[AbilityData: UniqueID={0}, Param={1}, AbilityID={2}, AbilityName={3}, Rank={4}, Exp={5}, AbilityType={6}, SlotType={7}, LearningSkills={8}, Skills={9}]", (object) this.UniqueID, (object) this.Param, (object) this.Param.iname, (object) this.AbilityName, (object) this.Rank, (object) this.Exp, (object) this.AbilityType, (object) this.SlotType, (object) this.LearningSkills, (object) this.Skills);
    }

    public static MixedAbilityData ToMix(AbilityData[] abilitys, string name, string iconName)
    {
      MixedAbilityData mixedAbilityData = new MixedAbilityData();
      mixedAbilityData.mSkills = new List<SkillData>();
      for (int index = 0; index < abilitys.Length; ++index)
      {
        using (List<SkillData>.Enumerator enumerator = abilitys[index].mSkills.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SkillData current = enumerator.Current;
            mixedAbilityData.mSkills.Add(current);
          }
        }
      }
      mixedAbilityData.mAbilityParam = new AbilityParam();
      mixedAbilityData.mAbilityParam.name = name;
      mixedAbilityData.mAbilityParam.icon = iconName;
      mixedAbilityData.mAbilityParam.type = EAbilityType.Active;
      mixedAbilityData.mAbilityParam.slot = EAbilitySlot.Action;
      mixedAbilityData.mAbilityParam.skills = new LearningSkill[1];
      return mixedAbilityData;
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
          skillData.Setup(cs.iname, (int) this.mRank, (int) this.mRankCap, (MasterParam) null);
          skillData.IsCollabo = (OBool) true;
          this.mSkills.Add(skillData);
        }
      }
    }
  }
}