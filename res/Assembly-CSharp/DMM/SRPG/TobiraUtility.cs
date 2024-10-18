// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace SRPG
{
  public class TobiraUtility
  {
    public static int GetUnlockTobiraNum(Json_Tobira[] json)
    {
      return json == null ? 0 : ((IEnumerable<Json_Tobira>) json).Count<Json_Tobira>((Func<Json_Tobira, bool>) (json_tobira => json_tobira.category != 0 && json_tobira.lv >= 1));
    }

    public static int GetTobiraUnlockLevel(string unit_iname)
    {
      int tobiraUnlockLevel = 0;
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTobiraConditionsForUnit(unit_iname, TobiraParam.Category.START);
      if (conditionsForUnit == null)
        return tobiraUnlockLevel;
      foreach (TobiraConditionParam tobiraConditionParam in conditionsForUnit)
      {
        if (tobiraConditionParam.CondType == TobiraConditionParam.ConditionType.Unit)
        {
          if (string.IsNullOrEmpty(tobiraConditionParam.CondUnit.UnitIname))
          {
            tobiraUnlockLevel = tobiraConditionParam.CondUnit.Level;
            break;
          }
          if (unit_iname == tobiraConditionParam.CondUnit.UnitIname)
          {
            tobiraUnlockLevel = tobiraConditionParam.CondUnit.Level;
            break;
          }
        }
      }
      return tobiraUnlockLevel;
    }

    public static List<string> GetOverwrittenAbilitys(UnitData unitData)
    {
      List<string> overwrittenAbilitys = new List<string>();
      if (unitData == null || unitData.UnitParam == null)
        return overwrittenAbilitys;
      List<TobiraParam> list = ((IEnumerable<TobiraParam>) MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unitData.UnitParam.iname)).ToList<TobiraParam>();
      List<TobiraLearnAbilityParam> learnAbilityParamList1 = new List<TobiraLearnAbilityParam>();
      List<AbilityData> learnAbilitys = unitData.TobiraMasterAbilitys;
      if (list != null)
      {
        List<TobiraLearnAbilityParam> learnAbilityParamList2 = new List<TobiraLearnAbilityParam>();
        for (int index = 0; index < list.Count; ++index)
        {
          if (list[index] != null)
          {
            foreach (TobiraLearnAbilityParam learnAbilityParam in list[index].LeanAbilityParam)
            {
              learnAbilityParamList2.Add(learnAbilityParam);
              learnAbilityParamList1.Add(learnAbilityParam);
            }
          }
        }
        learnAbilityParamList2.RemoveAll((Predicate<TobiraLearnAbilityParam>) (lta =>
        {
          for (int index = 0; index < learnAbilitys.Count; ++index)
          {
            if (lta.AbilityIname == learnAbilitys[index].AbilityID && lta.AbilityAddType == TobiraLearnAbilityParam.AddType.MasterOverwrite)
              return false;
          }
          return true;
        }));
        TobiraLearnAbilityParam learnAbilityParam1;
        for (int index = 0; index < learnAbilityParamList2.Count; ++index)
        {
          for (string overWriteAbilityName = learnAbilityParamList2[index].AbilityOverwrite; !string.IsNullOrEmpty(overWriteAbilityName); overWriteAbilityName = learnAbilityParam1 == null ? string.Empty : learnAbilityParam1.AbilityOverwrite)
          {
            overwrittenAbilitys.Add(overWriteAbilityName);
            learnAbilityParam1 = learnAbilityParamList1.Find((Predicate<TobiraLearnAbilityParam>) (abi => abi.AbilityIname == overWriteAbilityName));
          }
        }
      }
      return overwrittenAbilitys;
    }

    public static List<ConditionsResult> TobiraConditionsCheck(
      UnitData unitData,
      TobiraConditionParam[] conds)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      List<ConditionsResult> conditionsResultList = new List<ConditionsResult>();
      for (int index = 0; index < conds.Length; ++index)
      {
        if (conds[index].CondType == TobiraConditionParam.ConditionType.Unit)
        {
          TobiraCondsUnitParam condUnit = conds[index].CondUnit;
          UnitData unitData1 = unitData;
          UnitParam unitParam = (UnitParam) null;
          if (!condUnit.HasFlag(TobiraCondsUnitParam.ConditionsDetail.IsSelf))
          {
            unitData1 = instanceDirect.Player.FindUnitDataByUnitID(condUnit.UnitIname);
            if (unitData1 == null)
              unitParam = instanceDirect.GetUnitParam(condUnit.UnitIname);
          }
          if (condUnit.HasFlag(TobiraCondsUnitParam.ConditionsDetail.IsUnitLv))
            conditionsResultList.Add((ConditionsResult) new ConditionsResult_UnitLv(unitData1, unitParam, condUnit.Level));
          if (condUnit.HasFlag(TobiraCondsUnitParam.ConditionsDetail.IsTobiraLv))
            conditionsResultList.Add((ConditionsResult) new ConditionsResult_TobiraLv(unitData1, unitParam, condUnit.TobiraCategory, condUnit.TobiraLv));
          if (condUnit.HasFlag(TobiraCondsUnitParam.ConditionsDetail.IsAwake))
            conditionsResultList.Add((ConditionsResult) new ConditionsResult_AwakeLv(unitData1, unitParam, condUnit.AwakeLevel));
          if (condUnit.HasFlag(TobiraCondsUnitParam.ConditionsDetail.IsJobLv))
            conditionsResultList.Add((ConditionsResult) new ConditionsResult_JobLv(unitData1, unitParam, condUnit.Jobs));
        }
        else if (conds[index].CondType == TobiraConditionParam.ConditionType.Quest)
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_QuestClear(instanceDirect.FindQuest(conds[index].CondIname)));
      }
      if (conditionsResultList.Count < 1)
        conditionsResultList.Add((ConditionsResult) new ConditionsResult_TobiraNoConditions());
      return conditionsResultList;
    }

    public static List<ConditionsResult> TobiraRecipeCheck(
      UnitData unitData,
      TobiraParam.Category category,
      int targetLevel)
    {
      List<ConditionsResult> conditionsResultList = new List<ConditionsResult>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      TobiraRecipeParam tobiraRecipe = instanceDirect.MasterParam.GetTobiraRecipe(unitData.UnitID, category, targetLevel);
      if (tobiraRecipe.Cost > 0)
        conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasGold(tobiraRecipe.Cost));
      if (tobiraRecipe.UnitPieceNum > 0)
      {
        if (!string.IsNullOrEmpty(unitData.UnitParam.subPiece))
        {
          string[] inames = new string[2]
          {
            unitData.UnitParam.piece,
            unitData.UnitParam.subPiece
          };
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(inames, tobiraRecipe.UnitPieceNum));
        }
        else
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(unitData.UnitParam.piece, tobiraRecipe.UnitPieceNum));
      }
      if (tobiraRecipe.ElementNum > 0)
      {
        int elementNum = tobiraRecipe.ElementNum;
        ItemParam elementPieceParam = unitData.GetElementPieceParam();
        if (elementPieceParam == null)
          DebugUtility.LogWarning("対応する属性欠片が見つかりませんでした。");
        else
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(elementPieceParam.iname, elementNum));
      }
      if (tobiraRecipe.UnlockElementNum > 0)
      {
        int unlockElementNum = tobiraRecipe.UnlockElementNum;
        ItemParam itemParam = instanceDirect.GetItemParam(unitData.GetUnlockTobiraElementID());
        if (itemParam == null)
          DebugUtility.LogWarning("属性に対応する扉用素材が見つかりませんでした。");
        else
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(itemParam.iname, unlockElementNum));
      }
      if (tobiraRecipe.UnlockBirthNum > 0)
      {
        int unlockBirthNum = tobiraRecipe.UnlockBirthNum;
        ItemParam itemParam = instanceDirect.GetItemParam(unitData.GetUnlockTobiraBirthID());
        if (itemParam == null)
          DebugUtility.LogWarning("出自に対応する扉用素材が見つかりませんでした。");
        else
          conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(itemParam.iname, unlockBirthNum));
      }
      foreach (TobiraRecipeMaterialParam material in tobiraRecipe.Materials)
      {
        if (material != null && !string.IsNullOrEmpty(material.Iname))
        {
          int num = material.Num;
          ItemParam itemParam = instanceDirect.GetItemParam(material.Iname);
          if (itemParam == null)
            DebugUtility.LogWarning("アイテムが見つかりませんでした。");
          else
            conditionsResultList.Add((ConditionsResult) new ConditionsResult_HasItem(itemParam.iname, num));
        }
      }
      return conditionsResultList;
    }

    public static bool IsClearAllToboraConditions(
      UnitData unit_data,
      TobiraParam.Category tobira_category)
    {
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unit_data.UnitID, tobira_category);
      if (conditionsForUnit == null)
        return true;
      bool flag = true;
      List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraConditionsCheck(unit_data, conditionsForUnit);
      for (int index = 0; index < conditionsResultList.Count; ++index)
      {
        if (!conditionsResultList[index].isClear)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static bool IsClearAllToboraRecipe(
      UnitData unit_data,
      TobiraParam.Category tobira_category,
      int tobiraLv)
    {
      bool flag = true;
      List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraRecipeCheck(unit_data, tobira_category, tobiraLv);
      for (int index = 0; index < conditionsResultList.Count; ++index)
      {
        if (!conditionsResultList[index].isClear)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static string ToJsonString(List<TobiraData> list)
    {
      if (list == null || list.Count <= 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder(512);
      stringBuilder.Append("\"doors\":[");
      for (int index = 0; index < list.Count; ++index)
      {
        if (index != 0)
          stringBuilder.Append(",");
        stringBuilder.Append(list[index].ToJsonString());
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static void CalcTobiraParameter(
      string unit_iname,
      TobiraParam.Category category,
      int lv,
      ref BaseStatus add,
      ref BaseStatus scale)
    {
      TobiraData tobiraData = new TobiraData();
      if (!tobiraData.Setup(unit_iname, category, lv))
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unit_iname);
      SkillData.GetHomePassiveBuffStatus(tobiraData.ParameterBuffSkill, unitParam == null ? EElement.None : unitParam.element, ref add, ref scale);
    }

    public static List<SkillData> GetParameterBuffSkills(List<TobiraData> list)
    {
      List<SkillData> parameterBuffSkills = new List<SkillData>();
      if (list == null)
        return parameterBuffSkills;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index] != null && list[index].ParameterBuffSkill != null && list[index].ParameterBuffSkill.SkillParam != null)
          parameterBuffSkills.Add(list[index].ParameterBuffSkill);
      }
      return parameterBuffSkills;
    }

    public static int GetAdditionalUnitLevelCapWithTobira(List<TobiraData> list)
    {
      int levelCapWithTobira = 0;
      if (list == null)
        return levelCapWithTobira;
      int tobiraUnitLvCapBonus = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index] != null && list[index].Param != null && list[index].IsUnlocked && list[index].Param.TobiraCategory != TobiraParam.Category.START)
          levelCapWithTobira += tobiraUnitLvCapBonus;
      }
      return levelCapWithTobira;
    }

    public static int GetAdditionalUnitLevelCapWithUnlockNum(int unlockNum)
    {
      int capWithUnlockNum = 0;
      if (unlockNum == 0)
        return capWithUnlockNum;
      int tobiraUnitLvCapBonus = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus;
      for (int index = 0; index < unlockNum; ++index)
        capWithUnlockNum += tobiraUnitLvCapBonus;
      return capWithUnlockNum;
    }

    public static string GetOverwriteLeaderSkill(List<TobiraData> list)
    {
      if (list.Count <= 0)
        return string.Empty;
      TobiraData tobiraData1 = (TobiraData) null;
      foreach (TobiraData tobiraData2 in list)
      {
        if (tobiraData2.IsLearnedLeaderSkill)
          tobiraData1 = tobiraData1 == null ? tobiraData2 : (tobiraData1.Param.Priority >= tobiraData2.Param.Priority ? tobiraData1 : tobiraData2);
      }
      return tobiraData1 != null ? tobiraData1.LearnedLeaderSkillIname : string.Empty;
    }

    public static SkillData CreateParameterBuffSkill(TobiraParam tobiraParam, int currentLv)
    {
      if (tobiraParam == null)
        return (SkillData) null;
      int tobiraLvCap = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
      SkillData parameterBuffSkill = new SkillData();
      parameterBuffSkill.Setup(tobiraParam.SkillIname, currentLv, tobiraLvCap);
      return parameterBuffSkill;
    }

    public static void TryCraeteLeaderSkill(
      TobiraParam tobiraParam,
      int currentLv,
      ref SkillData skillData,
      bool isJust = false)
    {
      if (isJust)
      {
        if (tobiraParam.OverwriteLeaderSkillLevel != currentLv)
          return;
        skillData = new SkillData();
        skillData.Setup(tobiraParam.OverwriteLeaderSkillIname, 1);
      }
      else
      {
        if (tobiraParam.OverwriteLeaderSkillLevel > currentLv)
          return;
        skillData = new SkillData();
        skillData.Setup(tobiraParam.OverwriteLeaderSkillIname, 1);
      }
    }

    public static void TryCraeteAbilityData(
      UnitData unitData,
      TobiraParam tobiraParam,
      int currentLv,
      List<AbilityData> newAbilitys,
      List<AbilityData> oldAbilitys,
      bool isJust = false)
    {
      List<TobiraLearnAbilityParam> learnAbilityParamList = new List<TobiraLearnAbilityParam>();
      TobiraLearnAbilityParam learnAbilityParam1 = Array.Find<TobiraLearnAbilityParam>(tobiraParam.LeanAbilityParam, (Predicate<TobiraLearnAbilityParam>) (learnAbil => learnAbil.AbilityAddType == TobiraLearnAbilityParam.AddType.JobOverwrite));
      List<JobData> baseJobs = unitData.GetBaseJobs(false);
      if (isJust)
      {
        foreach (TobiraLearnAbilityParam learnAbilityParam2 in tobiraParam.LeanAbilityParam)
        {
          TobiraLearnAbilityParam learn_ability_param = learnAbilityParam2;
          if (learn_ability_param.Level == currentLv)
          {
            if (learn_ability_param.AbilityAddType == TobiraLearnAbilityParam.AddType.JobOverwrite)
            {
              if (baseJobs.FindIndex((Predicate<JobData>) (job_data => job_data.JobID == learn_ability_param.TargetJob)) >= 0)
                learnAbilityParam1 = learn_ability_param;
            }
            else
              learnAbilityParamList.Add(learn_ability_param);
          }
        }
        if (learnAbilityParam1 != null)
          learnAbilityParamList.Add(learnAbilityParam1);
      }
      else
      {
        foreach (TobiraLearnAbilityParam learnAbilityParam3 in tobiraParam.LeanAbilityParam)
        {
          TobiraLearnAbilityParam learn_ability_param = learnAbilityParam3;
          if (learn_ability_param.Level <= currentLv)
          {
            if (learn_ability_param.AbilityAddType == TobiraLearnAbilityParam.AddType.JobOverwrite)
            {
              if (baseJobs.FindIndex((Predicate<JobData>) (job_data => job_data.JobID == learn_ability_param.TargetJob)) >= 0)
                learnAbilityParam1 = learn_ability_param;
            }
            else
              learnAbilityParamList.Add(learn_ability_param);
          }
        }
        if (learnAbilityParam1 != null)
          learnAbilityParamList.Add(learnAbilityParam1);
      }
      newAbilitys.Clear();
      oldAbilitys.Clear();
      newAbilitys.Capacity = learnAbilityParamList.Count;
      oldAbilitys.Capacity = learnAbilityParamList.Count;
      for (int index = 0; index < learnAbilityParamList.Count; ++index)
      {
        AbilityData abilityData1 = new AbilityData();
        AbilityData abilityData2 = (AbilityData) null;
        abilityData1.Setup((UnitData) null, 0L, learnAbilityParamList[index].AbilityIname, 1);
        newAbilitys.Add(abilityData1);
        if (!string.IsNullOrEmpty(learnAbilityParamList[index].AbilityOverwrite) && (learnAbilityParamList[index].AbilityAddType == TobiraLearnAbilityParam.AddType.JobOverwrite || learnAbilityParamList[index].AbilityAddType == TobiraLearnAbilityParam.AddType.MasterOverwrite))
        {
          abilityData2 = new AbilityData();
          abilityData2.Setup((UnitData) null, 0L, learnAbilityParamList[index].AbilityOverwrite, 0);
        }
        oldAbilitys.Add(abilityData2);
      }
    }

    public static List<TobiraData> CreateDummyData(UnitData unit)
    {
      TobiraParam[] tobiraListForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit.UnitParam.iname);
      List<TobiraData> dummyData = new List<TobiraData>();
      for (int index = 0; index < tobiraListForUnit.Length; ++index)
      {
        if (tobiraListForUnit[index] != null)
        {
          TobiraData tobiraData = new TobiraData();
          tobiraData.Setup(tobiraListForUnit[index].UnitIname, tobiraListForUnit[index].TobiraCategory, index % 5 + 1);
          dummyData.Add(tobiraData);
        }
      }
      return dummyData;
    }

    public static void CreateDummyData(Json_Unit[] units)
    {
      if (units == null)
        return;
      for (int index1 = 0; index1 < units.Length; ++index1)
      {
        Json_Unit unit = units[index1];
        TobiraParam[] tobiraListForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit.iname);
        List<Json_Tobira> jsonTobiraList = new List<Json_Tobira>();
        for (int index2 = 0; index2 < tobiraListForUnit.Length; ++index2)
        {
          TobiraParam tobiraParam = tobiraListForUnit[index2];
          if (tobiraParam != null)
            jsonTobiraList.Add(new Json_Tobira()
            {
              category = (int) tobiraParam.TobiraCategory,
              lv = index2 % 5 + 1
            });
        }
        if (jsonTobiraList.Count > 0)
          unit.doors = jsonTobiraList.ToArray();
      }
    }
  }
}
