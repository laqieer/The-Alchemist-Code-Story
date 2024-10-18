﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestClearUnlockPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class QuestClearUnlockPopup : MonoBehaviour
  {
    public Button CloseButton;
    public GameObject ItemWindow;
    public string ItemCloseFlag;
    public string ItemEndAnimation;
    public string ItemOpendAnimation;
    public GameObject SkillNewItem;
    public GameObject SkillUpdateItem;
    public GameObject AbilityNewItem;
    public GameObject AbilityUpdateItem;
    public GameObject LeaderSkillNewItem;
    public GameObject LeaderSkillUpdateItem;
    public GameObject MasterAbilityNewItem;
    public GameObject MasterAbilityUpdateItem;
    private QuestClearUnlockUnitDataParam[] mUnlocks;
    private List<GameObject> mListItems = new List<GameObject>();
    private UnitData mUnit;
    private Animator mWindowAnimator;
    private bool mInitalize;

    private void Start()
    {
      this.mUnlocks = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam[]>(((Component) this).gameObject, (QuestClearUnlockUnitDataParam[]) null);
      if (this.mUnlocks == null)
        return;
      this.mUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (this.mUnit == null)
        return;
      if (Object.op_Inequality((Object) this.SkillNewItem, (Object) null))
        this.SkillNewItem.SetActive(false);
      if (Object.op_Inequality((Object) this.SkillUpdateItem, (Object) null))
        this.SkillUpdateItem.SetActive(false);
      if (Object.op_Inequality((Object) this.AbilityNewItem, (Object) null))
        this.AbilityNewItem.SetActive(false);
      if (Object.op_Inequality((Object) this.AbilityUpdateItem, (Object) null))
        this.AbilityUpdateItem.SetActive(false);
      if (Object.op_Inequality((Object) this.LeaderSkillNewItem, (Object) null))
        this.LeaderSkillNewItem.SetActive(false);
      if (Object.op_Inequality((Object) this.LeaderSkillUpdateItem, (Object) null))
        this.LeaderSkillUpdateItem.SetActive(false);
      if (Object.op_Inequality((Object) this.MasterAbilityNewItem, (Object) null))
        this.MasterAbilityNewItem.SetActive(false);
      ((Component) this.CloseButton).gameObject.SetActive(false);
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index = 0; index < this.mUnlocks.Length; ++index)
      {
        GameObject gameObject = (GameObject) null;
        AbilityParam data1 = (AbilityParam) null;
        SkillParam data2 = (SkillParam) null;
        switch (this.mUnlocks[index].type + 1)
        {
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
            gameObject = !this.mUnlocks[index].add ? this.CreateInstance(this.SkillUpdateItem) : this.CreateInstance(this.SkillNewItem);
            data1 = masterParam.GetAbilityParam(this.mUnlocks[index].parent_id);
            data2 = masterParam.GetSkillParam(this.mUnlocks[index].new_id);
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
            gameObject = this.CreateInstance(!this.mUnlocks[index].add ? this.AbilityUpdateItem : this.AbilityNewItem);
            data1 = masterParam.GetAbilityParam(this.mUnlocks[index].new_id);
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
            gameObject = this.CreateInstance(!this.mUnlocks[index].add ? this.LeaderSkillUpdateItem : this.LeaderSkillNewItem);
            data2 = masterParam.GetSkillParam(this.mUnlocks[index].new_id);
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.CollaboAbility:
            gameObject = this.CreateInstance(!this.mUnlocks[index].add ? this.MasterAbilityUpdateItem : this.MasterAbilityNewItem);
            data1 = masterParam.GetAbilityParam(this.mUnlocks[index].new_id);
            if (data1 != null && data1.skills != null)
            {
              data2 = masterParam.GetSkillParam(data1.skills[0].iname);
              break;
            }
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability | QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
            gameObject = this.CreateInstance(!this.mUnlocks[index].add ? this.MasterAbilityUpdateItem : this.MasterAbilityNewItem);
            data2 = masterParam.GetSkillParam(this.mUnlocks[index].new_id);
            break;
        }
        if (Object.op_Inequality((Object) this.ItemWindow, (Object) null) && Object.op_Inequality((Object) gameObject, (Object) null))
        {
          if (data1 != null)
            DataSource.Bind<AbilityParam>(gameObject, data1);
          if (data2 != null)
            DataSource.Bind<SkillParam>(gameObject, data2);
          gameObject.transform.SetParent(this.ItemWindow.transform, false);
          gameObject.SetActive(false);
          this.mListItems.Add(gameObject);
        }
      }
      if (this.mListItems.Count <= 0)
        return;
      if (Object.op_Inequality((Object) this.ItemWindow, (Object) null))
        this.mWindowAnimator = this.ItemWindow.GetComponent<Animator>();
      this.mListItems[0].SetActive(true);
    }

    private void Update()
    {
      if (!this.mInitalize)
      {
        this.mInitalize = true;
      }
      else
      {
        if (this.mListItems.Count <= 0)
          return;
        if (this.mListItems.Count == 1)
        {
          if (((Component) this.CloseButton).gameObject.activeInHierarchy || !GameUtility.CompareAnimatorStateName((Component) this.mWindowAnimator, this.ItemOpendAnimation) || this.mWindowAnimator.IsInTransition(0))
            return;
          ((Component) this.CloseButton).gameObject.SetActive(true);
        }
        else if (!((Component) this.CloseButton).gameObject.activeInHierarchy && this.mListItems.Count <= 1 && GameUtility.CompareAnimatorStateName((Component) this.mWindowAnimator, this.ItemOpendAnimation) && !this.mWindowAnimator.IsInTransition(0))
        {
          ((Component) this.CloseButton).gameObject.SetActive(true);
        }
        else
        {
          if (!GameUtility.CompareAnimatorStateName((Component) this.mWindowAnimator, this.ItemEndAnimation) || this.mWindowAnimator.IsInTransition(0))
            return;
          this.mListItems[0].SetActive(false);
          this.mListItems.RemoveAt(0);
          this.mListItems[0].SetActive(true);
          GameUtility.SetAnimatorBool((Component) this.mWindowAnimator, this.ItemCloseFlag, false);
        }
      }
    }

    public void OnClick()
    {
      if (this.mListItems.Count <= 1 || this.mWindowAnimator.GetBool(this.ItemCloseFlag))
        return;
      GameUtility.SetAnimatorBool((Component) this.mWindowAnimator, this.ItemCloseFlag, true);
    }

    private GameObject CreateInstance(GameObject template)
    {
      return Object.op_Inequality((Object) template, (Object) null) ? Object.Instantiate<GameObject>(template) : (GameObject) null;
    }
  }
}
