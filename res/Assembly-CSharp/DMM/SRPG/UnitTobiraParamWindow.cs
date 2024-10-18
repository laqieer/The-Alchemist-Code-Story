// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraParamWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitTobiraParamWindow : MonoBehaviour
  {
    [SerializeField]
    private Text TobiraTitleText;
    [SerializeField]
    private ImageArray TobiraIcons;
    [SerializeField]
    private GameObject TobiraParamIconEnable;
    [SerializeField]
    private UnitTobiraParamLevel[] TobiraParamIconLevels;
    [SerializeField]
    private GameObject TobiraParamIconDisable;
    [SerializeField]
    private StatusList Status;
    [SerializeField]
    private Text AdditionalLevelCap;
    [HeaderBar("▼扉LvをMaxにした時の恩恵")]
    [SerializeField]
    private Text TobiraMasterEffectText;
    [SerializeField]
    private Text TobiraMasterEffectDescText;
    [HeaderBar("▼扉が「未開放」時に表示するもの")]
    [SerializeField]
    private GameObject TobiraOpenButton;
    [SerializeField]
    private Text TobiraOpenButtonText;
    [SerializeField]
    private GameObject mLockedLayout;
    [SerializeField]
    private GameObject mTobiraConditionsLayout;
    [SerializeField]
    private RectTransform mTobiraConditionsContent;
    [SerializeField]
    private UnitTobiraConditionsItem mTobiraConditionsTemplate;
    [HeaderBar("▼扉が「開放」時に表示するもの")]
    [SerializeField]
    private GameObject TobiraLevelUpButton;
    [SerializeField]
    private Text TobiraLevelUpButtonText;
    [SerializeField]
    private GameObject mUnlockedLayout;
    [SerializeField]
    private GameObject mTobiraMasterBonusLayout;
    [HeaderBar("▼扉Lvが「Max」時に表示するもの")]
    [SerializeField]
    private GameObject mTobiraMasterEbaleLayout;
    private List<GameObject> mTobiraParameters = new List<GameObject>();
    private List<UnitTobiraConditionsItem> mTobiraConditions = new List<UnitTobiraConditionsItem>();

    public void Refresh(UnitData unitData, TobiraData tobiraData, TobiraParam tobiraParam)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TobiraTitleText, (UnityEngine.Object) null))
        return;
      this.TobiraTitleText.text = TobiraParam.GetCategoryName(tobiraParam.TobiraCategory);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AdditionalLevelCap, (UnityEngine.Object) null))
        this.AdditionalLevelCap.text = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus.ToString();
      if (tobiraData == null)
      {
        this.TobiraParamIconEnable.SetActive(false);
        this.TobiraParamIconDisable.SetActive(true);
        Array.ForEach<UnitTobiraParamLevel>(this.TobiraParamIconLevels, (Action<UnitTobiraParamLevel>) (paramLevel => paramLevel.Refresh(0)));
        this.LockView(unitData, tobiraParam);
        this.mTobiraParameters.ForEach((Action<GameObject>) (paramGO => UnityEngine.Object.Destroy((UnityEngine.Object) paramGO)));
        this.mTobiraParameters.Clear();
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(unitData.UnitID, tobiraParam.TobiraCategory, 1, ref add, ref scale);
        this.Status.SetValues(add, scale, true);
        TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unitData.UnitID, tobiraParam.TobiraCategory);
        this.CreateConditionsItems(unitData, conditionsForUnit);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TobiraIcons, (UnityEngine.Object) null))
          this.TobiraIcons.ImageIndex = (int) tobiraData.Param.TobiraCategory;
        this.TobiraParamIconEnable.SetActive(tobiraData.IsUnlocked);
        this.TobiraParamIconDisable.SetActive(!tobiraData.IsUnlocked);
        int level = tobiraData.ViewLv;
        Array.ForEach<UnitTobiraParamLevel>(this.TobiraParamIconLevels, (Action<UnitTobiraParamLevel>) (paramLevel => paramLevel.Refresh(level)));
        if (tobiraData.IsUnlocked)
          this.UnlockView(unitData, tobiraData);
        else
          this.LockView(unitData, tobiraParam);
        this.mTobiraParameters.ForEach((Action<GameObject>) (paramGO => UnityEngine.Object.Destroy((UnityEngine.Object) paramGO)));
        this.mTobiraParameters.Clear();
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(unitData.UnitID, tobiraData.Param.TobiraCategory, tobiraData.Lv, ref add, ref scale);
        this.Status.SetValues(add, scale);
        this.SetActiveConditionsItems(false);
      }
      this.SetMasterBonusText(tobiraParam);
    }

    private void SetActiveConditionsItems(bool active)
    {
      for (int index = 0; index < this.mTobiraConditions.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTobiraConditions[index], (UnityEngine.Object) null))
          ((Component) this.mTobiraConditions[index]).gameObject.SetActive(active);
      }
    }

    private void CreateConditionsItems(UnitData unitData, TobiraConditionParam[] condition_params)
    {
      List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraConditionsCheck(unitData, condition_params);
      int num = Mathf.Max(this.mTobiraConditions.Count, conditionsResultList.Count);
      for (int index = 0; index < num; ++index)
      {
        if (this.mTobiraConditions.Count <= index)
        {
          UnitTobiraConditionsItem component = UnityEngine.Object.Instantiate<GameObject>(((Component) this.mTobiraConditionsTemplate).gameObject).GetComponent<UnitTobiraConditionsItem>();
          ((Component) component).transform.SetParent(((Component) this.mTobiraConditionsContent).transform, false);
          this.mTobiraConditions.Add(component);
        }
        UnitTobiraConditionsItem mTobiraCondition = this.mTobiraConditions[index];
        if (conditionsResultList.Count <= index)
        {
          ((Component) mTobiraCondition).gameObject.SetActive(false);
        }
        else
        {
          mTobiraCondition.Setup(conditionsResultList[index]);
          ((Component) mTobiraCondition).gameObject.SetActive(true);
        }
      }
    }

    private void LockView(UnitData unitData, TobiraParam tobiraParam)
    {
      string categoryName = TobiraParam.GetCategoryName(tobiraParam.TobiraCategory);
      this.TobiraOpenButtonText.text = string.Format(LocalizedText.Get("sys.TOBIRA_INVENTORY_OPEN_TITLE"), (object) categoryName);
      this.mLockedLayout.SetActive(true);
      this.mTobiraConditionsLayout.SetActive(true);
      this.mUnlockedLayout.SetActive(false);
      this.mTobiraMasterBonusLayout.SetActive(false);
      this.mTobiraMasterEbaleLayout.SetActive(false);
      bool isEnable = TobiraUtility.IsClearAllToboraConditions(unitData, tobiraParam.TobiraCategory);
      ((Selectable) this.TobiraOpenButton.GetComponent<Button>()).interactable = isEnable;
      UnitTobiraParamWindow.SetHilightAnimationEnable(this.TobiraOpenButton, isEnable);
    }

    private void UnlockView(UnitData unitData, TobiraData tobiraData)
    {
      string categoryName = TobiraParam.GetCategoryName(tobiraData.Param.TobiraCategory);
      this.TobiraLevelUpButtonText.text = string.Format(LocalizedText.Get("sys.TOBIRA_INVENTORY_BTN_LV_UP"), (object) categoryName);
      this.TobiraLevelUpButton.SetActive(true);
      this.mLockedLayout.SetActive(!tobiraData.IsUnlocked);
      this.mTobiraConditionsLayout.SetActive(!tobiraData.IsUnlocked);
      this.mUnlockedLayout.SetActive(tobiraData.IsUnlocked);
      this.mTobiraMasterBonusLayout.SetActive(tobiraData.IsUnlocked);
      if (tobiraData.IsMaxLv)
      {
        this.TobiraLevelUpButton.SetActive(false);
        this.mTobiraMasterEbaleLayout.SetActive(true);
      }
      else
      {
        this.TobiraLevelUpButton.SetActive(true);
        this.mTobiraMasterEbaleLayout.SetActive(false);
        bool isEnable = TobiraUtility.IsClearAllToboraRecipe(unitData, tobiraData.Param.TobiraCategory, tobiraData.Lv);
        ((Selectable) this.TobiraOpenButton.GetComponent<Button>()).interactable = isEnable;
        UnitTobiraParamWindow.SetHilightAnimationEnable(this.TobiraLevelUpButton, isEnable);
      }
    }

    private static void SetHilightAnimationEnable(GameObject obj, bool isEnable)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
        return;
      Animator component = obj.GetComponent<Animator>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetBool("hilit", isEnable);
    }

    private void SetMasterBonusText(TobiraParam tobiraParam)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TobiraMasterEffectText, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TobiraMasterEffectDescText, (UnityEngine.Object) null))
        return;
      int tobiraLvCap = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
      this.TobiraMasterEffectText.text = string.Empty;
      this.TobiraMasterEffectDescText.text = string.Empty;
      UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(tobiraParam.UnitIname);
      if (tobiraParam.IsUnlockConceptCardSlot2)
      {
        this.TobiraMasterEffectText.text = LocalizedText.Get("sys.TOBIRA_MASTER_CONCEPT_CARD_SLOT2_UNLOCK_BONUS_TEXT");
        this.TobiraMasterEffectDescText.text = LocalizedText.Get("sys.TOBIRA_MASTER_CONCEPT_CARD_SLOT2_UNLOCK_BONUS_TEXT_DESC");
      }
      List<AbilityData> newAbilitys = new List<AbilityData>();
      List<AbilityData> oldAbilitys = new List<AbilityData>();
      TobiraUtility.TryCraeteAbilityData(unitDataByUnitId, tobiraParam, tobiraLvCap, newAbilitys, oldAbilitys, true);
      if (newAbilitys.Count > 0)
      {
        AbilityParam abilityParam = oldAbilitys[0] == null ? (AbilityParam) null : oldAbilitys[0].Param;
        if (abilityParam != null)
        {
          this.TobiraMasterEffectText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_OVERRIDE_NEW_ABILITY_TEXT"), (object) abilityParam.name, (object) newAbilitys[0].Param.name);
          this.TobiraMasterEffectDescText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), (object) newAbilitys[0].Param.expr);
        }
        else
        {
          this.TobiraMasterEffectText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_NEW_ABILITY_TEXT"), (object) newAbilitys[0].Param.name);
          this.TobiraMasterEffectDescText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), (object) newAbilitys[0].Param.expr);
        }
      }
      SkillData skillData = (SkillData) null;
      TobiraUtility.TryCraeteLeaderSkill(tobiraParam, tobiraLvCap, ref skillData, true);
      if (skillData == null)
        return;
      this.TobiraMasterEffectText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_NEW_LEADER_SKILL_TEXT"), (object) skillData.SkillParam.name);
      this.TobiraMasterEffectDescText.text = string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), (object) skillData.SkillParam.expr);
    }
  }
}
