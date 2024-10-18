// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AbilityDetailWindow : MonoBehaviour
  {
    private static UnitData BindObject;
    public Transform SkillLayoutParent;
    public GameObject SkillTemplate;
    public GameObject SkillLockedTemplate;
    [StringIsGameObjectID]
    public string UnlockCondTextId;
    public GameObject SkillUnlockCondWindow;
    public static bool IsEnableSkillChange;

    public static void SetBindObject(UnitData udata)
    {
      AbilityDetailWindow.BindObject = udata;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.SkillTemplate != (UnityEngine.Object) null)
        this.SkillTemplate.SetActive(false);
      if ((UnityEngine.Object) this.SkillLockedTemplate != (UnityEngine.Object) null)
        this.SkillLockedTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.SkillUnlockCondWindow != (UnityEngine.Object) null))
        return;
      this.SkillUnlockCondWindow.SetActive(false);
    }

    private void OnEnable()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      UnitData data1 = (UnitData) null;
      if ((bool) ((UnityEngine.Object) UnitEnhanceV3.Instance))
        data1 = UnitEnhanceV3.Instance.CurrentUnit;
      if (data1 == null)
        data1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (data1 == null && AbilityDetailWindow.BindObject != null)
        data1 = AbilityDetailWindow.BindObject;
      if (data1 == null)
      {
        DebugUtility.LogError("Not found bind unit data.");
      }
      else
      {
        AbilityData abilityData = data1.GetAbilityData((long) GlobalVars.SelectedAbilityUniqueID);
        AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam((string) GlobalVars.SelectedAbilityID);
        QuestClearUnlockUnitDataParam[] unlockedSkills = data1.UnlockedSkills;
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>();
        QuestClearUnlockUnitDataParam[] allUnlockUnitDatas = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnlockUnitDatas();
        if (allUnlockUnitDatas != null)
        {
          for (int index = 0; index < allUnlockUnitDatas.Length; ++index)
          {
            QuestClearUnlockUnitDataParam param = allUnlockUnitDatas[index];
            if (param.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill && param.uid == data1.UnitID && param.parent_id == abilityParam.iname && (unlockedSkills == null || Array.FindIndex<QuestClearUnlockUnitDataParam>(unlockedSkills, (Predicate<QuestClearUnlockUnitDataParam>) (p => p.iname == param.iname)) == -1))
              unlockUnitDataParamList.Add(param);
          }
        }
        RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) data1.UnitParam.raremax);
        int num = Math.Min((int) rarityParam.UnitLvCap + (int) rarityParam.UnitAwakeLvCap, abilityParam.GetRankCap());
        DataSource.Bind<UnitData>(this.gameObject, data1);
        DataSource.Bind<AbilityData>(this.gameObject, abilityData);
        AbilityParam data2 = abilityParam;
        if (AbilityDetailWindow.IsEnableSkillChange)
        {
          string key = data1.SearchAbilityReplacementSkill(abilityParam.iname);
          if (!string.IsNullOrEmpty(key))
            data2 = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
        }
        DataSource.Bind<AbilityParam>(this.gameObject, data2);
        if ((UnityEngine.Object) this.SkillTemplate != (UnityEngine.Object) null)
        {
          List<SkillParam> skillParamList = new List<SkillParam>();
          if (abilityData != null && abilityData.LearningSkills != null)
          {
            for (int index1 = 0; index1 < abilityData.LearningSkills.Length; ++index1)
            {
              if (abilityData.LearningSkills[index1].locklv <= num)
              {
                string str1 = abilityData.LearningSkills[index1].iname;
                if (unlockedSkills != null)
                {
                  for (int index2 = 0; index2 < unlockedSkills.Length; ++index2)
                  {
                    if (unlockedSkills[index2].old_id == str1)
                    {
                      str1 = unlockedSkills[index2].new_id;
                      break;
                    }
                  }
                }
                if (AbilityDetailWindow.IsEnableSkillChange)
                {
                  string str2 = data1.SearchReplacementSkill(str1);
                  if (!string.IsNullOrEmpty(str2))
                    str1 = str2;
                }
                skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(str1));
              }
            }
            if (unlockedSkills != null)
            {
              for (int index = 0; index < unlockedSkills.Length; ++index)
              {
                if (unlockedSkills[index].add && unlockedSkills[index].parent_id == abilityData.AbilityID)
                  skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(unlockedSkills[index].new_id));
              }
            }
          }
          else
          {
            for (int index = 0; index < abilityParam.skills.Length; ++index)
            {
              if (abilityParam.skills[index].locklv <= num)
                skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(abilityParam.skills[index].iname));
            }
          }
          if (abilityData == data1.MasterAbility && data1.CollaboAbility != null)
          {
            using (List<SkillData>.Enumerator enumerator = data1.CollaboAbility.Skills.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SkillData current = enumerator.Current;
                skillParamList.Add(current.SkillParam);
              }
            }
          }
          for (int index = 0; index < skillParamList.Count; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkillTemplate);
            DataSource.Bind<SkillParam>(gameObject, skillParamList[index]);
            gameObject.transform.SetParent(this.SkillLayoutParent, false);
            gameObject.SetActive(true);
          }
          for (int index = 0; index < unlockUnitDataParamList.Count; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkillLockedTemplate);
            DataSource.Bind<SkillParam>(gameObject, MonoSingleton<GameManager>.Instance.GetSkillParam(unlockUnitDataParamList[index].new_id));
            DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject, unlockUnitDataParamList[index]);
            gameObject.transform.SetParent(this.SkillLayoutParent, false);
            gameObject.SetActive(true);
          }
        }
        GameParameter.UpdateAll(this.gameObject);
      }
    }

    public void OnOpenSkillUnlockCondWindow(GameObject button)
    {
      if (!((UnityEngine.Object) this.SkillUnlockCondWindow != (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.SkillUnlockCondWindow);
      QuestClearUnlockUnitDataParam dataOfClass = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam>(button, (QuestClearUnlockUnitDataParam) null);
      DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject1, dataOfClass);
      gameObject1.SetActive(true);
      gameObject1.transform.SetParent(this.transform, false);
      Text gameObject2 = GameObjectID.FindGameObject<Text>(this.UnlockCondTextId);
      if (!((UnityEngine.Object) gameObject2 != (UnityEngine.Object) null))
        return;
      gameObject2.text = dataOfClass.GetUnitLevelCond();
      string abilityLevelCond = dataOfClass.GetAbilityLevelCond();
      if (!string.IsNullOrEmpty(abilityLevelCond))
      {
        Text text = gameObject2;
        text.text = text.text + (string.IsNullOrEmpty(gameObject2.text) ? string.Empty : "\n") + abilityLevelCond;
      }
      string clearQuestCond = dataOfClass.GetClearQuestCond();
      if (string.IsNullOrEmpty(clearQuestCond))
        return;
      Text text1 = gameObject2;
      text1.text = text1.text + (string.IsNullOrEmpty(gameObject2.text) ? string.Empty : "\n") + clearQuestCond;
    }
  }
}
