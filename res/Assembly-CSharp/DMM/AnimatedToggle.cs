// Decompiled with JetBrains decompiler
// Type: AnimatedToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Animator))]
public class AnimatedToggle : Toggle
{
  public string BoolName = "on";
  public string DisabledBool;
  private Animator mAnimator;
  public AnimatedToggle.ClickEvent OnClick;
  [CustomEnum(typeof (SystemSound.ECue), -1)]
  public int ClickSound = -1;
  [BitMask]
  public CriticalSections CSMask;

  protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
  {
  }

  protected virtual void Awake() => this.mAnimator = ((Component) this).GetComponent<Animator>();

  protected virtual void OnEnable()
  {
    base.OnEnable();
    this.Update();
  }

  private void Update()
  {
    this.mAnimator.SetBool(this.BoolName, this.isOn);
    if (string.IsNullOrEmpty(this.DisabledBool))
      return;
    this.mAnimator.SetBool(this.DisabledBool, !((Selectable) this).interactable);
  }

  public virtual void OnPointerClick(PointerEventData eventData)
  {
    if (this.IsCriticalSectionActive())
      return;
    base.OnPointerClick(eventData);
    if (this.OnClick != null)
      this.OnClick(this);
    this.PlaySound();
  }

  private void PlaySound()
  {
    if (!((Selectable) this).IsInteractable() || this.ClickSound < 0)
      return;
    SystemSound.Play((SystemSound.ECue) this.ClickSound);
  }

  private bool IsCriticalSectionActive()
  {
    return (this.CSMask & CriticalSection.GetActive()) != (CriticalSections) 0;
  }

  public delegate void ClickEvent(AnimatedToggle toggle);
}
