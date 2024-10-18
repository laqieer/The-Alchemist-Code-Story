// Decompiled with JetBrains decompiler
// Type: SRPG.QuestClearUnlockUnitDataParam
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
  public class QuestClearUnlockUnitDataParam
  {
    public string iname;
    public string uid;
    public bool add;
    public QuestClearUnlockUnitDataParam.EUnlockTypes type = QuestClearUnlockUnitDataParam.EUnlockTypes.None;
    public string new_id;
    public string old_id;
    public string parent_id;
    public int ulv;
    public string aid;
    public int alv;
    public string[] qids;
    public bool qcnd;

    public string GetUnitLevelCond()
    {
      if (this.ulv <= 0)
        return string.Empty;
      return LocalizedText.Get("sys.SKILL_UNLOCKCOND_UNITLV", (object) this.ulv);
    }

    public string GetAbilityLevelCond()
    {
      if (!string.IsNullOrEmpty(this.aid) && this.alv > 0)
      {
        AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.aid);
        if (abilityParam != null)
          return LocalizedText.Get("sys.SKILL_UNLOCKCOND_ABILITYLV", (object) abilityParam.name, (object) this.alv);
      }
      return string.Empty;
    }

    public string GetClearQuestCond()
    {
      if (this.qids == null)
        return string.Empty;
      string str = string.Empty;
      for (int index = 0; index < this.qids.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.qids[index]);
        if (quest != null)
        {
          if (!string.IsNullOrEmpty(str))
            str += ", ";
          str = str + quest.title + quest.name;
        }
      }
      return LocalizedText.Get("sys.SKILL_UNLOCKCOND_CLEARQUEST") + str;
    }

    public bool IsSameNeedClearQuestCond(QuestClearUnlockUnitDataParam target)
    {
      if (this.qids == null && target.qids == null)
        return true;
      if (this.qids == null && target.qids != null || this.qids != null && target.qids == null)
        return false;
      if (this.qids.Length == 0 && target.qids.Length == 0)
        return true;
      List<string> stringList1 = new List<string>((IEnumerable<string>) this.qids);
      List<string> stringList2 = new List<string>((IEnumerable<string>) target.qids);
      if (stringList1.Count != stringList2.Count)
        return false;
      stringList1.Sort((Comparison<string>) ((a, b) => a.CompareTo(b)));
      stringList2.Sort((Comparison<string>) ((a, b) => a.CompareTo(b)));
      for (int index = 0; index < stringList1.Count; ++index)
      {
        if (stringList1[index].CompareTo(stringList2[index]) != 0)
          return false;
      }
      return true;
    }

    public void Deserialize(JSON_QuestClearUnlockUnitDataParam json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      this.uid = json.uid;
      this.add = json.add > 0;
      this.type = (QuestClearUnlockUnitDataParam.EUnlockTypes) json.type;
      this.new_id = json.new_id;
      this.old_id = json.old_id;
      this.parent_id = json.parent_id;
      this.ulv = json.ulv;
      this.aid = json.aid;
      this.alv = json.alv;
      this.qcnd = json.qcnd > 0;
      if (json.qids == null)
        return;
      this.qids = new string[json.qids.Length];
      json.qids.CopyTo((Array) this.qids, 0);
    }

    public string GetUnlockTypeText()
    {
      string empty = string.Empty;
      switch (this.type + 1)
      {
        case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
          empty = LocalizedText.Get("sys.UNITLIST_REWRITE_JOB");
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
          empty = LocalizedText.Get("sys.UNITLIST_REWRITE_SKILL");
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
          empty = LocalizedText.Get("sys.UNITLIST_REWRITE_ABILITY");
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
          empty = LocalizedText.Get("sys.UNITLIST_REWRITE_LEADERSKILL");
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.CollaboAbility:
          empty = LocalizedText.Get("sys.UNITLIST_REWRITE_MASTERABILITY");
          break;
      }
      return empty;
    }

    public string GetRewriteName()
    {
      string rewriteName = string.Empty;
      if (!this.add && this.old_id != null)
      {
        switch (this.type + 1)
        {
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
            rewriteName = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.old_id).name;
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
          case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
            rewriteName = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(this.old_id).name;
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
            rewriteName = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id).name;
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.CollaboAbility:
            AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
            if (abilityParam != null)
            {
              rewriteName = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(abilityParam.skills[0].iname).name;
              break;
            }
            break;
        }
      }
      return rewriteName;
    }

    public string GetCondText(UnitParam unit)
    {
      if (unit == null)
        return string.Empty;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      if (this.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Ability || this.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill)
      {
        string[] jobsets = unit.jobsets;
        if (jobsets != null)
        {
          MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
          JobParam[] jobParamArray = new JobParam[jobsets.Length];
          for (int index = 0; index < jobsets.Length; ++index)
          {
            JobSetParam jobSetParam = masterParam.GetJobSetParam(jobsets[index]);
            if (jobSetParam != null)
              jobParamArray[index] = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(jobSetParam.job);
          }
          if (this.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Ability)
          {
            if (!this.add)
            {
              for (int index = 0; index < jobParamArray.Length; ++index)
              {
                if (jobParamArray[index].FindRankOfAbility(this.old_id) != -1)
                {
                  str1 = jobParamArray[index].name;
                  break;
                }
              }
            }
          }
          else if (this.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill)
          {
            for (int index1 = 0; index1 < jobParamArray.Length; ++index1)
            {
              for (int lv = 0; lv < JobParam.MAX_JOB_RANK; ++lv)
              {
                OString[] learningAbilitys = jobParamArray[index1].GetLearningAbilitys(lv);
                if (learningAbilitys != null && learningAbilitys.Length >= 1)
                {
                  for (int index2 = 0; index2 < learningAbilitys.Length; ++index2)
                  {
                    if (this.add)
                    {
                      if ((string) learningAbilitys[index2] == this.parent_id)
                      {
                        str1 = jobParamArray[index1].name;
                        break;
                      }
                    }
                    else
                    {
                      AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam((string) learningAbilitys[index2]);
                      if (abilityParam != null && Array.FindIndex<LearningSkill>(abilityParam.skills, (Predicate<LearningSkill>) (p => p.iname == (!this.add ? this.old_id : this.parent_id))) != -1)
                      {
                        str1 = jobParamArray[index1].name;
                        break;
                      }
                    }
                  }
                  if (!string.IsNullOrEmpty(str1))
                    break;
                }
              }
              if (!string.IsNullOrEmpty(str1))
                break;
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(str1))
        str1 = LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_TEXTFRAME", (object) str1);
      switch (this.type + 1)
      {
        case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
        case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
          if (this.add)
          {
            str2 = LocalizedText.Get(!string.IsNullOrEmpty(str1) ? "sys.UNITLIST_REWRITE_ABILITY" : "sys.UNITLIST_REWRITE_MASTERABILITY");
            AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.parent_id);
            str3 = abilityParam == null ? str3 : abilityParam.name;
            break;
          }
          str2 = this.type != QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill ? LocalizedText.Get(!string.IsNullOrEmpty(str1) ? "sys.UNITLIST_REWRITE_SKILL" : "sys.UNITLIST_REWRITE_MASTERABILITY") : LocalizedText.Get("sys.UNITLIST_REWRITE_LEADERSKILL");
          SkillParam skillParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(this.old_id);
          str3 = skillParam1 == null ? str3 : skillParam1.name;
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
          if (this.add)
          {
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_JOB");
            JobParam jobParam = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.parent_id);
            str3 = jobParam == null ? str3 : jobParam.name;
            break;
          }
          str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_ABILITY");
          AbilityParam abilityParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
          str3 = abilityParam1 == null ? str3 : abilityParam1.name;
          break;
        case QuestClearUnlockUnitDataParam.EUnlockTypes.CollaboAbility:
          if (!this.add)
          {
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_MASTERABILITY");
            AbilityParam abilityParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
            if (abilityParam2 != null)
            {
              SkillParam skillParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(abilityParam2.skills[0].iname);
              str3 = skillParam2 == null ? str3 : skillParam2.name;
              break;
            }
            break;
          }
          break;
      }
      if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
        return string.Empty;
      return LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_PARENT", (object) str2, (object) str1, (object) str3);
    }

    public bool IsMasterAbility()
    {
      return this.type == QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility;
    }

    public enum EUnlockTypes
    {
      None = -1, // 0xFFFFFFFF
      Job = 0,
      Skill = 1,
      Ability = 2,
      LeaderSkill = 3,
      MasterAbility = 4,
      CollaboAbility = 5,
    }
  }
}
