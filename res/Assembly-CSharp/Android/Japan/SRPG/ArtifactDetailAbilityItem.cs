// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailAbilityItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactDetailAbilityItem : MonoBehaviour, IPointerDownHandler, IHoldGesture, IEventSystemHandler
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

    public bool IsEnable
    {
      get
      {
        return this.mIsEnable;
      }
    }

    public bool HasDeriveAbility
    {
      get
      {
        return this.mHasDeriveAbility;
      }
    }

    public void Setup(AbilityParam ability_param, bool is_derive_ability, bool is_enable, bool is_locked, bool has_derive_ability)
    {
      this.mAbilityParam = ability_param;
      this.mIsEnable = is_enable;
      this.mHasDeriveAbility = has_derive_ability;
      if (is_locked)
        this.mLockImage.SetActive(is_locked);
      else
        this.mDisableMask.SetActive(!this.mIsEnable);
      if ((UnityEngine.Object) this.mUnitIcon != (UnityEngine.Object) null)
        this.mUnitIcon.SetActive(DataSource.FindDataOfClass<UnitParam>(this.gameObject, (UnitParam) null) != null);
      if ((UnityEngine.Object) this.mJobIcon != (UnityEngine.Object) null)
        this.mJobIcon.SetActive(DataSource.FindDataOfClass<JobParam>(this.gameObject, (JobParam) null) != null);
      if (!((UnityEngine.Object) this.mConditionsText != (UnityEngine.Object) null))
        return;
      AbilityConditions abilityConditions = new AbilityConditions();
      abilityConditions.Setup(ability_param, MonoSingleton<GameManager>.Instance.MasterParam);
      this.mConditionsText.text = abilityConditions.MakeConditionsText();
    }

    public void SetActive(bool is_active)
    {
      if (this.mAbilityParam == null)
        return;
      this.gameObject.SetActive(is_active);
    }

    public void SetActiveLine(bool is_active)
    {
      if ((UnityEngine.Object) this.mLineVertical != (UnityEngine.Object) null)
        this.mLineVertical.SetActive(is_active);
      if (!((UnityEngine.Object) this.mLineHorizontal != (UnityEngine.Object) null))
        return;
      this.mLineHorizontal.SetActive(is_active);
    }

    public void DestoryLastLine()
    {
      if ((UnityEngine.Object) this.mLineVertical == (UnityEngine.Object) null)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mLineVertical);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      ArtifactDetailWindow componentInParent = this.GetComponentInParent<ArtifactDetailWindow>();
      if (!((UnityEngine.Object) componentInParent != (UnityEngine.Object) null))
        return;
      AbilityDetailWindow.SetBindAbility(this.mAbilityParam);
      componentInParent.OpenAbilityDetail();
    }

    public void OnPointerHoldEnd()
    {
    }
  }
}
