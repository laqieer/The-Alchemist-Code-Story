// Decompiled with JetBrains decompiler
// Type: AnimatedToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Animator))]
public class AnimatedToggle : Toggle
{
  public string BoolName = "on";
  [CustomEnum(typeof (SystemSound.ECue), -1)]
  public int ClickSound = -1;
  public string DisabledBool;
  private Animator mAnimator;
  public AnimatedToggle.ClickEvent OnClick;
  [BitMask]
  public CriticalSections CSMask;

  protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
  {
  }

  protected override void Awake()
  {
    this.mAnimator = this.GetComponent<Animator>();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    this.Update();
  }

  private void Update()
  {
    this.mAnimator.SetBool(this.BoolName, this.isOn);
    if (string.IsNullOrEmpty(this.DisabledBool))
      return;
    this.mAnimator.SetBool(this.DisabledBool, !this.interactable);
  }

  public override void OnPointerClick(PointerEventData eventData)
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
    if (!this.IsInteractable() || this.ClickSound < 0)
      return;
    SystemSound.Play((SystemSound.ECue) this.ClickSound);
  }

  private bool IsCriticalSectionActive()
  {
    return (this.CSMask & CriticalSection.GetActive()) != (CriticalSections) 0;
  }

  public delegate void ClickEvent(AnimatedToggle toggle);
}
