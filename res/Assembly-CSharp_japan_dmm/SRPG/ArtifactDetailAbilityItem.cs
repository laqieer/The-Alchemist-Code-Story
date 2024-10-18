﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailAbilityItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtifactDetailAbilityItem : 
    MonoBehaviour,
    IPointerDownHandler,
    IHoldGesture,
    IEventSystemHandler
  {
    [SerializeField]
    private GameObject mUnitIcon;
    [SerializeField]
    private GameObject mJobIcon;
    [SerializeField]
    private GameObject mDisableMask;
    [SerializeField]
    private GameObject mLockImage;
    [SerializeField]
    private Text mConditionsText;
    [SerializeField]
    private GameObject mLineVertical;
    [SerializeField]
    private GameObject mLineHorizontal;
    private AbilityParam mAbilityParam;
    private bool mIsEnable;
    private bool mHasDeriveAbility;

    public bool IsEnable => this.mIsEnable;

    public bool HasDeriveAbility => this.mHasDeriveAbility;

    public void Setup(
      AbilityParam ability_param,
      bool is_derive_ability,
      bool is_enable,
      bool is_locked,
      bool has_derive_ability)
    {
      this.mAbilityParam = ability_param;
      this.mIsEnable = is_enable;
      this.mHasDeriveAbility = has_derive_ability;
      if (is_locked)
        this.mLockImage.SetActive(is_locked);
      else
        this.mDisableMask.SetActive(!this.mIsEnable);
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null))
        this.mUnitIcon.SetActive(DataSource.FindDataOfClass<UnitParam>(((Component) this).gameObject, (UnitParam) null) != null);
      if (Object.op_Inequality((Object) this.mJobIcon, (Object) null))
        this.mJobIcon.SetActive(DataSource.FindDataOfClass<JobParam>(((Component) this).gameObject, (JobParam) null) != null);
      if (!Object.op_Inequality((Object) this.mConditionsText, (Object) null))
        return;
      AbilityConditions abilityConditions = new AbilityConditions();
      abilityConditions.Setup(ability_param, MonoSingleton<GameManager>.Instance.MasterParam);
      this.mConditionsText.text = abilityConditions.MakeConditionsText();
    }

    public void SetActive(bool is_active)
    {
      if (this.mAbilityParam == null)
        return;
      ((Component) this).gameObject.SetActive(is_active);
    }

    public void SetActiveLine(bool is_active)
    {
      if (Object.op_Inequality((Object) this.mLineVertical, (Object) null))
        this.mLineVertical.SetActive(is_active);
      if (!Object.op_Inequality((Object) this.mLineHorizontal, (Object) null))
        return;
      this.mLineHorizontal.SetActive(is_active);
    }

    public void DestoryLastLine()
    {
      if (Object.op_Equality((Object) this.mLineVertical, (Object) null))
        return;
      Object.Destroy((Object) this.mLineVertical);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      ArtifactDetailWindow componentInParent = ((Component) this).GetComponentInParent<ArtifactDetailWindow>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      AbilityDetailWindow.SetBindAbility(this.mAbilityParam);
      componentInParent.OpenAbilityDetail();
    }

    public void OnPointerHoldEnd()
    {
    }
  }
}
