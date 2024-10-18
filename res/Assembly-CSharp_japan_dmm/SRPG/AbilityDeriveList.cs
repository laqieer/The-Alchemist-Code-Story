// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDeriveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AbilityDeriveList : MonoBehaviour
  {
    [HeaderBar("▼派生元アビリティのバナーが属するリストの親")]
    [SerializeField]
    private GameObject m_BaseAbilityRoot;
    [SerializeField]
    private Button m_DisableBaseAbility;
    [HeaderBar("▼派生先アビリティ関連のオブジェクトの親")]
    [SerializeField]
    private GameObject m_DeriveObjectRoot;
    [SerializeField]
    private RectTransform m_DeriveAbilityListRoot;
    [SerializeField]
    private GameObject m_DeriveAbilityTemplate;
    [SerializeField]
    [BitMask]
    private AbilityDeriveList.ShowFlags m_ShowFlags;
    [HeaderBar("▼アビリティ詳細を開く為のコールバック")]
    public AbilityDeriveList.OnListItemEvent OnDetailOpen;
    [HeaderBar("▼アビリティが選択された時のコールバック")]
    public AbilityDeriveList.OnListItemEvent OnSelectEvent;
    [HeaderBar("▼アビリティのランクアップボタンのコールバック")]
    public AbilityDeriveList.OnListItemEvent OnRankUpEvent;
    public AbilityDeriveList.OnListItemEvent OnRankUpBtnPressEvent;
    public AbilityDeriveList.OnListItemEvent OnRankUpBtnUpEvent;
    private List<GameObject> m_DeriveAbilitys;

    public bool HasDerive => this.m_DeriveAbilitys != null && this.m_DeriveAbilitys.Count > 0;

    public bool IsShowBaseAbility
    {
      get => this.ShowFlags_IsOn(AbilityDeriveList.ShowFlags.ShowBaseAbility);
      set => this.ShowFlags_Set(AbilityDeriveList.ShowFlags.ShowBaseAbility, value);
    }

    public bool IsDisableBaseAbilityInteractable
    {
      get => this.ShowFlags_IsOn(AbilityDeriveList.ShowFlags.DisableBaseAbilityInteractable);
    }

    private void Start() => GameUtility.SetGameObjectActive(this.m_DeriveAbilityTemplate, false);

    public void SetupWithAbilityParam(
      AbilityParam baseAbility,
      List<AbilityDeriveParam> abilityDeriveParams)
    {
      if (abilityDeriveParams != null && abilityDeriveParams.Count > 0)
      {
        foreach (AbilityDeriveParam abilityDeriveParam in abilityDeriveParams)
          this.CreateDeriveAbilityItem(abilityDeriveParam);
      }
      else
        this.IsShowBaseAbility = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BaseAbilityRoot, (UnityEngine.Object) null))
      {
        this.RegisterHoldEvent(this.m_BaseAbilityRoot);
        DataSource.Bind<AbilityParam>(this.m_BaseAbilityRoot, baseAbility);
      }
      this.UpdateUIActive();
    }

    public void SetupWithAbilityData(
      AbilityData baseAbility,
      List<AbilityData> derivedAbilitys,
      UnitData unit_data = null)
    {
      if (derivedAbilitys != null && derivedAbilitys.Count > 0)
      {
        foreach (AbilityData derivedAbility in derivedAbilitys)
          this.CreateDeriveAbilityItem(derivedAbility);
      }
      else
        this.IsShowBaseAbility = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BaseAbilityRoot, (UnityEngine.Object) null))
      {
        this.RegisterHoldEvent(this.m_BaseAbilityRoot);
        DataSource.Bind<AbilityData>(this.m_BaseAbilityRoot, baseAbility);
        if (unit_data != null)
        {
          string key = unit_data.SearchAbilityReplacementSkill(baseAbility.Param.iname);
          if (!string.IsNullOrEmpty(key))
            DataSource.Bind<AbilityParam>(this.m_BaseAbilityRoot, MonoSingleton<GameManager>.Instance.GetAbilityParam(key));
        }
      }
      this.UpdateUIActive();
    }

    public void SetupWithConceptCard(
      ConceptCardSkillDatailData conceptCardSkillDatailData,
      UnitData unitData,
      ConceptCardDetailAbility.ClickDetail onClickDetail,
      bool enableDecreaseEffectObject,
      int decreaseEffectRate)
    {
      bool flag = conceptCardSkillDatailData.effect != null && conceptCardSkillDatailData.effect.Ability != null;
      if (unitData != null && conceptCardSkillDatailData.type != ConceptCardDetailAbility.ShowType.LockSkill && flag)
      {
        List<AbilityData> abilityDataList = new List<AbilityData>();
        AbilityData ability = conceptCardSkillDatailData.effect.Ability;
        foreach (AbilityData searchDerivedAbility in unitData.SearchDerivedAbilitys(ability))
        {
          foreach (ConceptCardSkillDatailData conceptCardSkillDatailData1 in ConceptCardUtility.CreateConceptCardSkillDatailData(searchDerivedAbility))
          {
            if (conceptCardSkillDatailData1.type == ConceptCardDetailAbility.ShowType.Ability)
              AbilityDeriveList.SetupConceptCard(this.CreateDeriveAbilityItem(conceptCardSkillDatailData1.effect.Ability), conceptCardSkillDatailData1, onClickDetail, enableDecreaseEffectObject, decreaseEffectRate);
          }
        }
      }
      if (!this.HasDerive)
        this.ShowFlags_IsOn(AbilityDeriveList.ShowFlags.ShowBaseAbility);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BaseAbilityRoot, (UnityEngine.Object) null))
      {
        if (flag)
        {
          AbilityData ability = conceptCardSkillDatailData.effect.Ability;
          AbilityDeriveList.SetupConceptCard(this.m_BaseAbilityRoot, conceptCardSkillDatailData, onClickDetail, enableDecreaseEffectObject, decreaseEffectRate);
          DataSource.Bind<AbilityData>(this.m_BaseAbilityRoot, ability);
        }
        else
        {
          SkillData skillData = conceptCardSkillDatailData.skill_data;
          AbilityDeriveList.SetupConceptCard(this.m_BaseAbilityRoot, conceptCardSkillDatailData, onClickDetail, enableDecreaseEffectObject, decreaseEffectRate);
          DataSource.Bind<SkillData>(this.m_BaseAbilityRoot, skillData);
        }
      }
      this.UpdateUIActive();
    }

    private static void SetupConceptCard(
      GameObject obj,
      ConceptCardSkillDatailData conceptCardSkillDatailData,
      ConceptCardDetailAbility.ClickDetail onClickDetail,
      bool isSubSlot,
      int decreaseEffectRate)
    {
      ConceptCardDetailAbility component = obj.GetComponent<ConceptCardDetailAbility>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetCardSkill(conceptCardSkillDatailData, isSubSlot, decreaseEffectRate);
      component.SetAbilityDetailParent(onClickDetail);
    }

    private GameObject CreateDeriveAbilityItem(AbilityDeriveParam abilityDeriveParam)
    {
      GameObject deriveAbilityItem = (GameObject) null;
      if (this.m_DeriveAbilitys == null)
        this.m_DeriveAbilitys = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DeriveAbilityTemplate, (UnityEngine.Object) null))
      {
        deriveAbilityItem = UnityEngine.Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
        DataSource.Bind<SkillAbilityDeriveParam>(deriveAbilityItem, abilityDeriveParam.m_SkillAbilityDeriveParam);
        DataSource.Bind<AbilityParam>(deriveAbilityItem, abilityDeriveParam.m_DeriveParam);
        DataSource.Bind<AbilityDeriveParam>(deriveAbilityItem, abilityDeriveParam);
        deriveAbilityItem.SetActive(true);
        deriveAbilityItem.transform.SetParent((Transform) this.m_DeriveAbilityListRoot, false);
        this.m_DeriveAbilitys.Add(deriveAbilityItem);
        this.RegisterHoldEvent(deriveAbilityItem);
      }
      return deriveAbilityItem;
    }

    private GameObject CreateDeriveAbilityItem(AbilityData derivedAbility)
    {
      GameObject deriveAbilityItem = (GameObject) null;
      if (this.m_DeriveAbilitys == null)
        this.m_DeriveAbilitys = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DeriveAbilityTemplate, (UnityEngine.Object) null))
      {
        deriveAbilityItem = UnityEngine.Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
        if (derivedAbility.DeriveParam != null)
          DataSource.Bind<SkillAbilityDeriveParam>(deriveAbilityItem, derivedAbility.DeriveParam.m_SkillAbilityDeriveParam);
        DataSource.Bind<AbilityData>(deriveAbilityItem, derivedAbility);
        DataSource.Bind<AbilityDeriveParam>(deriveAbilityItem, derivedAbility.DeriveParam);
        deriveAbilityItem.SetActive(true);
        deriveAbilityItem.transform.SetParent((Transform) this.m_DeriveAbilityListRoot, false);
        this.m_DeriveAbilitys.Add(deriveAbilityItem);
        this.RegisterHoldEvent(deriveAbilityItem);
      }
      return deriveAbilityItem;
    }

    private GameObject CreateDeriveConceptCardSkillItem(SkillData derivedSkill)
    {
      GameObject conceptCardSkillItem = (GameObject) null;
      if (this.m_DeriveAbilitys == null)
        this.m_DeriveAbilitys = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DeriveAbilityTemplate, (UnityEngine.Object) null))
      {
        conceptCardSkillItem = UnityEngine.Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
        if (derivedSkill.DeriveParam != null)
          DataSource.Bind<SkillAbilityDeriveParam>(conceptCardSkillItem, derivedSkill.DeriveParam.m_SkillAbilityDeriveParam);
        DataSource.Bind<SkillData>(conceptCardSkillItem, derivedSkill);
        DataSource.Bind<SkillDeriveParam>(conceptCardSkillItem, derivedSkill.DeriveParam);
        conceptCardSkillItem.SetActive(true);
        conceptCardSkillItem.transform.SetParent((Transform) this.m_DeriveAbilityListRoot, false);
        this.m_DeriveAbilitys.Add(conceptCardSkillItem);
      }
      return conceptCardSkillItem;
    }

    private void RegisterHoldEvent(GameObject obj)
    {
      UnitAbilityListItemEvents componentInChildren = obj.GetComponentInChildren<UnitAbilityListItemEvents>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      UnitAbilityListItemEvents abilityListItemEvents1 = componentInChildren;
      abilityListItemEvents1.OnOpenDetail = abilityListItemEvents1.OnOpenDetail - new ListItemEvents.ListItemEvent(this.OpenAbilityDetail);
      UnitAbilityListItemEvents abilityListItemEvents2 = componentInChildren;
      abilityListItemEvents2.OnOpenDetail = abilityListItemEvents2.OnOpenDetail + new ListItemEvents.ListItemEvent(this.OpenAbilityDetail);
      UnitAbilityListItemEvents abilityListItemEvents3 = componentInChildren;
      abilityListItemEvents3.OnSelect = abilityListItemEvents3.OnSelect - new ListItemEvents.ListItemEvent(this.OnSelect);
      UnitAbilityListItemEvents abilityListItemEvents4 = componentInChildren;
      abilityListItemEvents4.OnSelect = abilityListItemEvents4.OnSelect + new ListItemEvents.ListItemEvent(this.OnSelect);
      componentInChildren.OnRankUp -= new ListItemEvents.ListItemEvent(this.OnRankUp);
      componentInChildren.OnRankUp += new ListItemEvents.ListItemEvent(this.OnRankUp);
      componentInChildren.OnRankUpBtnPress -= new ListItemEvents.ListItemEvent(this.OnRankUpBtnPress);
      componentInChildren.OnRankUpBtnPress += new ListItemEvents.ListItemEvent(this.OnRankUpBtnPress);
      componentInChildren.OnRankUpBtnUp -= new ListItemEvents.ListItemEvent(this.OnRankUpBtnUp);
      componentInChildren.OnRankUpBtnUp += new ListItemEvents.ListItemEvent(this.OnRankUpBtnUp);
    }

    public void SwitchBaseAbilityEnable(bool enable)
    {
      this.IsShowBaseAbility = enable;
      this.UpdateUIActive();
    }

    private void UpdateUIActive()
    {
      if (this.IsShowBaseAbility)
      {
        GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, true);
        if (this.HasDerive)
        {
          GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, true);
          int num = this.m_DeriveAbilitys.Count - 1;
          for (int index = 0; index < this.m_DeriveAbilitys.Count; ++index)
          {
            DeriveListItem component = this.m_DeriveAbilitys[index].GetComponent<DeriveListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetLineActive(true, index != num);
          }
        }
        else
          GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, false);
        if (!this.IsDisableBaseAbilityInteractable || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DisableBaseAbility, (UnityEngine.Object) null))
          return;
        ((Selectable) this.m_DisableBaseAbility).interactable = !this.HasDerive;
      }
      else
      {
        if (this.HasDerive)
        {
          GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, false);
          for (int index = 0; index < this.m_DeriveAbilitys.Count; ++index)
          {
            DeriveListItem component = this.m_DeriveAbilitys[index].GetComponent<DeriveListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetLineActive(false, false);
          }
        }
        else
          GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, true);
        if (!this.IsDisableBaseAbilityInteractable || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DisableBaseAbility, (UnityEngine.Object) null))
          return;
        ((Selectable) this.m_DisableBaseAbility).interactable = !this.HasDerive;
      }
    }

    public void AddDetailOpenEventListener(UnityAction<GameObject> callback)
    {
      if (this.OnDetailOpen == null)
        this.OnDetailOpen = new AbilityDeriveList.OnListItemEvent();
      this.OnDetailOpen.RemoveListener(callback);
      this.OnDetailOpen.AddListener(callback);
    }

    public void AddSelectEventListener(UnityAction<GameObject> callback)
    {
      if (this.OnSelectEvent == null)
        this.OnSelectEvent = new AbilityDeriveList.OnListItemEvent();
      this.OnSelectEvent.RemoveListener(callback);
      this.OnSelectEvent.AddListener(callback);
    }

    public void AddRankUpEventListener(UnityAction<GameObject> callback)
    {
      if (this.OnRankUpEvent == null)
        this.OnRankUpEvent = new AbilityDeriveList.OnListItemEvent();
      this.OnRankUpEvent.RemoveListener(callback);
      this.OnRankUpEvent.AddListener(callback);
    }

    public void AddRankUpBtnPressEventListener(UnityAction<GameObject> callback)
    {
      if (this.OnRankUpBtnPressEvent == null)
        this.OnRankUpBtnPressEvent = new AbilityDeriveList.OnListItemEvent();
      this.OnRankUpBtnPressEvent.RemoveListener(callback);
      this.OnRankUpBtnPressEvent.AddListener(callback);
    }

    public void AddRankUpBtnUpEventListener(UnityAction<GameObject> callback)
    {
      if (this.OnRankUpBtnUpEvent == null)
        this.OnRankUpBtnUpEvent = new AbilityDeriveList.OnListItemEvent();
      this.OnRankUpBtnUpEvent.RemoveListener(callback);
      this.OnRankUpBtnUpEvent.AddListener(callback);
    }

    public void RemoveOnDetailOpen(UnityAction<GameObject> callback)
    {
      if (this.OnDetailOpen == null)
        return;
      this.OnDetailOpen.RemoveListener(callback);
    }

    public void OpenAbilityDetail(GameObject obj)
    {
      AbilityParam dataOfClass = DataSource.FindDataOfClass<AbilityParam>(obj, (AbilityParam) null);
      if (dataOfClass != null)
        AbilityDetailWindow.SetBindAbility(dataOfClass);
      if (this.OnDetailOpen == null)
        return;
      this.OnDetailOpen.Invoke(obj);
    }

    public void OnSelect(GameObject obj)
    {
      if (this.OnSelectEvent == null)
        return;
      this.OnSelectEvent.Invoke(obj);
    }

    public void OnRankUp(GameObject obj)
    {
      if (this.OnRankUpEvent == null)
        return;
      this.OnRankUpEvent.Invoke(obj);
    }

    public void OnRankUpBtnPress(GameObject obj)
    {
      if (this.OnRankUpBtnPressEvent == null)
        return;
      this.OnRankUpBtnPressEvent.Invoke(obj);
    }

    public void OnRankUpBtnUp(GameObject obj)
    {
      if (this.OnRankUpBtnUpEvent == null)
        return;
      this.OnRankUpBtnUpEvent.Invoke(obj);
    }

    private bool ShowFlags_IsOn(AbilityDeriveList.ShowFlags flags)
    {
      return (this.m_ShowFlags & flags) != (AbilityDeriveList.ShowFlags) 0;
    }

    private bool ShowFlags_IsOff(AbilityDeriveList.ShowFlags flags)
    {
      return (this.m_ShowFlags & flags) == (AbilityDeriveList.ShowFlags) 0;
    }

    private void ShowFlags_SetOn(AbilityDeriveList.ShowFlags flags) => this.m_ShowFlags |= flags;

    private void ShowFlags_SetOff(AbilityDeriveList.ShowFlags flags) => this.m_ShowFlags &= ~flags;

    private void ShowFlags_Set(AbilityDeriveList.ShowFlags flags, bool value)
    {
      if (value)
        this.ShowFlags_SetOn(flags);
      else
        this.ShowFlags_SetOff(flags);
    }

    [Serializable]
    public class OnListItemEvent : UnityEvent<GameObject>
    {
    }

    [Flags]
    private enum ShowFlags
    {
      ShowBaseAbility = 1,
      DisableBaseAbilityInteractable = 2,
    }
  }
}
