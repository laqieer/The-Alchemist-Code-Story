// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitGauge : MonoBehaviour
  {
    public string ModeInt = "mode";
    public GradientGauge MainGauge;
    public UnitBuffDisplay BuffDisplay;
    private int mMode;
    public GameObject WeakTemplate;
    public GameObject ResistTemplate;
    public GameObject ElementIcon;
    public Image ElementIconImage;
    public GameObject ElementIconWeakOverlay;
    public GameObject ElementIconResistOverlay;
    private Unit mCurrentUnit;
    private GameObject ResistWeakPopup;

    public int Mode
    {
      get
      {
        return this.mMode;
      }
      set
      {
        this.mMode = value;
        if (this.mMode == 0)
          this.activateHpGauge(false);
        else
          this.activateHpGauge(true);
      }
    }

    private void SetElementIconImage(EElement element)
    {
      if (!((UnityEngine.Object) this.ElementIcon != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ElementIconImage != (UnityEngine.Object) null))
        return;
      this.ElementIconImage.sprite = GameSettings.Instance.Elements_IconSmall[(int) element];
      this.ResetElementIconOverlay();
    }

    private void ResetElementIconOverlay()
    {
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, false);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, false);
    }

    private void ToggleElementIconOverlay(bool weakActive)
    {
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, weakActive);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, !weakActive);
    }

    private void ActivateElementIconInternal(bool resetOverlay)
    {
      bool active = true;
      if (this.mCurrentUnit != null && this.mCurrentUnit.IsBreakObj && this.mCurrentUnit.Element == EElement.None)
        active = false;
      GameUtility.SetGameObjectActive(this.ElementIcon.gameObject, active);
      if (!resetOverlay)
        return;
      this.ResetElementIconOverlay();
    }

    private void DeactivateElementIconInternal()
    {
      GameUtility.SetGameObjectActive(this.ElementIcon.gameObject, false);
      this.ResetElementIconOverlay();
    }

    private void activateHpGauge(bool is_active)
    {
      if (this.mCurrentUnit == null || !this.mCurrentUnit.IsBreakObj)
        return;
      if ((bool) ((UnityEngine.Object) this.MainGauge))
        this.MainGauge.gameObject.SetActive(is_active);
      Image component = this.GetComponent<Image>();
      if ((bool) ((UnityEngine.Object) component))
        component.enabled = is_active;
      if (!(bool) ((UnityEngine.Object) this.ElementIcon) || this.mCurrentUnit.Element == EElement.None)
        return;
      Image[] componentsInChildren = this.ElementIcon.GetComponentsInChildren<Image>();
      if (componentsInChildren == null)
        return;
      foreach (Behaviour behaviour in componentsInChildren)
        behaviour.enabled = is_active;
    }

    protected void Start()
    {
      GameUtility.SetGameObjectActive(this.WeakTemplate, false);
      GameUtility.SetGameObjectActive(this.ResistTemplate, false);
      GameUtility.SetGameObjectActive(this.ElementIcon, false);
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, false);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, false);
    }

    public void SetOwner(Unit owner)
    {
      this.mCurrentUnit = owner;
      this.SetElementIconImage(this.mCurrentUnit.Element);
      this.activateHpGauge(false);
    }

    public Unit GetOwner()
    {
      return this.mCurrentUnit;
    }

    public void ActivateElementIcon(bool resetOverlay)
    {
      this.ActivateElementIconInternal(resetOverlay);
    }

    public void DeactivateElementIcon()
    {
      this.DeactivateElementIconInternal();
    }

    private int CalcElementRate(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (skill != null && skill.IsIgnoreElement())
      {
        skillElement = EElement.None;
        attackerElement = EElement.None;
      }
      EElement weakElement = UnitParam.GetWeakElement(this.mCurrentUnit.Element);
      EElement resistElement = UnitParam.GetResistElement(this.mCurrentUnit.Element);
      int num = 0;
      if (attackerElement != EElement.None)
        num += weakElement != attackerElement ? (resistElement != attackerElement ? 0 : -1) : 1;
      if (skillElement != EElement.None)
        num += weakElement != skillElement ? (resistElement != skillElement ? 0 : -1) : 1;
      return num;
    }

    public void OnAttack(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if ((UnityEngine.Object) this.ResistWeakPopup != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.ResistWeakPopup);
        this.ResistWeakPopup = (GameObject) null;
      }
      if (this.mCurrentUnit != null && this.mCurrentUnit.IsBreakObj)
        return;
      int num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
      if (num > 0)
      {
        if (!((UnityEngine.Object) this.WeakTemplate != (UnityEngine.Object) null))
          return;
        this.ResistWeakPopup = UnityEngine.Object.Instantiate<GameObject>(this.WeakTemplate);
        this.ResistWeakPopup.transform.SetParent(this.transform, false);
        this.ResistWeakPopup.SetActive(true);
      }
      else
      {
        if (num >= 0 || !((UnityEngine.Object) this.ResistTemplate != (UnityEngine.Object) null))
          return;
        this.ResistWeakPopup = UnityEngine.Object.Instantiate<GameObject>(this.ResistTemplate);
        this.ResistWeakPopup.transform.SetParent(this.transform, false);
        this.ResistWeakPopup.SetActive(true);
      }
    }

    public void Focus(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (this.mCurrentUnit == null || !this.mCurrentUnit.IsBreakObj)
      {
        int num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
        if (num > 0)
        {
          this.ToggleElementIconOverlay(true);
          return;
        }
        if (num < 0)
        {
          this.ToggleElementIconOverlay(false);
          return;
        }
      }
      this.ResetElementIconOverlay();
    }

    private void Update()
    {
      Animator component = this.GetComponent<Animator>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.ModeInt))
        component.SetInteger(this.ModeInt, this.mMode);
      this.ActivateElementIcon(false);
      if (this.MainGauge.IsAnimating || !((UnityEngine.Object) this.ResistWeakPopup != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.ResistWeakPopup);
      this.ResistWeakPopup = (GameObject) null;
    }

    public enum GaugeMode
    {
      Normal,
      Attack,
      Target,
      Change,
    }
  }
}
