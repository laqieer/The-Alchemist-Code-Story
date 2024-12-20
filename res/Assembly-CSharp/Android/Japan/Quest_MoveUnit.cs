﻿// Decompiled with JetBrains decompiler
// Type: Quest_MoveUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("UI/Drafts/Quest_MoveUnit")]
public class Quest_MoveUnit : UIDraft
{
  private Vector3 mVelocity = Vector3.zero;
  [UIDraft.AutoGenerated]
  public RectTransform VirtualStick;
  [UIDraft.AutoGenerated]
  public RectTransform VirtualStickBG;
  [UIDraft.AutoGenerated]
  public RectTransform VirtualStickFG;
  [UIDraft.AutoGenerated]
  public RectTransform TouchArea;
  [UIDraft.AutoGenerated]
  public Button Btn_OK;
  [UIDraft.AutoGenerated]
  public Button Btn_Cancel;
  public Quest_MoveUnit.ButtonClickEvent OnClickOK;
  public Quest_MoveUnit.ButtonClickEvent OnClickCancel;
  [NonSerialized]
  public Transform CameraTransform;
  private bool mTouched;
  private Vector3 mTouchStart;
  private Vector3 mTouchPos;

  public Vector2 Velocity
  {
    get
    {
      if (!((Object) this.CameraTransform != (Object) null))
        return (Vector2) this.mVelocity;
      Vector3 forward = this.CameraTransform.forward;
      Vector3 right = this.CameraTransform.right;
      forward.y = 0.0f;
      forward.Normalize();
      right.y = 0.0f;
      right.Normalize();
      return new Vector2((float) ((double) right.x * (double) this.mVelocity.x + (double) forward.x * (double) this.mVelocity.y), (float) ((double) right.z * (double) this.mVelocity.x + (double) forward.z * (double) this.mVelocity.y));
    }
  }

  private void OnEnable()
  {
    this.VirtualStick.gameObject.SetActive(false);
  }

  private void Start()
  {
    UIEventListener uiEventListener = UIEventListener.Get((Component) this.TouchArea);
    uiEventListener.onPointerUp = (UIEventListener.PointerEvent) (eventData =>
    {
      UIUtility.ToggleWindowState((Component) this.VirtualStick, false);
      this.mTouched = false;
      this.mVelocity = Vector3.zero;
    });
    uiEventListener.onPointerDown = (UIEventListener.PointerEvent) (eventData =>
    {
      GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
      UIUtility.ToggleWindowState((Component) this.VirtualStick, true);
      Vector3 vector3 = gameObject.transform.InverseTransformPoint((Vector3) eventData.position);
      ((RectTransform) this.VirtualStick.transform).anchoredPosition = new Vector2(vector3.x, vector3.y);
      this.mTouchStart = vector3;
      this.mTouchPos = vector3;
      this.mTouched = true;
      this.mVelocity = Vector3.zero;
    });
    uiEventListener.onDrag = (UIEventListener.PointerEvent) (eventData => this.mTouchPos = eventData.pointerPress.transform.InverseTransformPoint((Vector3) eventData.position));
    UIUtility.AddEventListener(this.Btn_OK.gameObject, (UnityEvent) this.Btn_OK.onClick, (UIUtility.EventListener) (go =>
    {
      if (this.OnClickOK == null)
        return;
      this.OnClickOK();
    }));
    UIUtility.AddEventListener(this.Btn_Cancel.gameObject, (UnityEvent) this.Btn_Cancel.onClick, (UIUtility.EventListener) (go =>
    {
      if (this.OnClickCancel == null)
        return;
      this.OnClickCancel();
    }));
  }

  private void Update()
  {
    if (!this.mTouched)
      return;
    Vector3 vector3 = this.mTouchPos - this.mTouchStart;
    RectTransform transform = (RectTransform) this.VirtualStickFG.transform;
    float num = (float) (((double) ((RectTransform) this.VirtualStickBG.transform).sizeDelta.x - (double) transform.sizeDelta.x) * 0.5);
    if ((double) vector3.magnitude >= (double) num)
      vector3 = vector3.normalized * num;
    transform.anchoredPosition = (Vector2) vector3;
    this.mVelocity = vector3 * (1f / num);
  }

  public void ShowButtons(bool show)
  {
    this.Btn_OK.gameObject.SetActive(show);
    this.Btn_Cancel.gameObject.SetActive(show);
  }

  public delegate void ButtonClickEvent();
}
