// Decompiled with JetBrains decompiler
// Type: SRPG.TouchController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [AddComponentMenu("Event/TouchController")]
  public class TouchController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
  {
    private float mClickRadiusThreshold = 5f;
    private const float DragStartThreshold = 0.1f;
    public TouchController.ClickEvent OnClick;
    public TouchController.DragEvent OnDragDelegate;
    public TouchController.DragEvent OnDragEndDelegate;
    [NonSerialized]
    public Vector2 Velocity;
    [NonSerialized]
    public Vector2 AngularVelocity;
    private Vector2 mClickPos;
    private bool mMultiTouched;
    private bool mIsTouching;
    private float mPointerDownTime;
    [NonSerialized]
    public Vector2 DragDelta;
    private RectTransform mRectTransform;
    private Vector2 mDragStart;

    public RectTransform GetRectTransform()
    {
      return this.mRectTransform;
    }

    public bool IsTouching
    {
      get
      {
        return this.mIsTouching;
      }
    }

    public Vector2 DragStart
    {
      get
      {
        return this.mDragStart;
      }
    }

    public Vector2 WorldSpaceVelocity
    {
      get
      {
        UnityEngine.Camera main = UnityEngine.Camera.main;
        if (!((UnityEngine.Object) main != (UnityEngine.Object) null))
          return Vector2.zero;
        Transform transform = main.transform;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0.0f;
        forward.Normalize();
        right.y = 0.0f;
        right.Normalize();
        return new Vector2((float) ((double) right.x * (double) this.Velocity.x + (double) forward.x * (double) this.Velocity.y), (float) ((double) right.z * (double) this.Velocity.x + (double) forward.z * (double) this.Velocity.y));
      }
    }

    public void IgnoreCurrentTouch()
    {
      this.mIsTouching = false;
      this.Velocity = Vector2.zero;
      this.AngularVelocity = Vector2.zero;
    }

    private void Awake()
    {
      this.mRectTransform = this.GetComponent<RectTransform>();
    }

    private void Start()
    {
      this.mClickRadiusThreshold = (float) (Screen.height / 18);
    }

    private void Update()
    {
      if (!SRPG_TouchInputModule.IsMultiTouching)
        return;
      this.mMultiTouched = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      this.mClickPos = eventData.position;
      this.mMultiTouched = false;
      this.mIsTouching = true;
      this.DragDelta = Vector2.zero;
      this.mPointerDownTime = Time.time;
      this.AngularVelocity = Vector2.zero;
      this.Velocity = Vector2.zero;
      this.mDragStart = (Vector2) this.transform.InverseTransformPoint((Vector3) eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      this.mIsTouching = false;
      if ((UnityEngine.Object) eventData.pointerPress == (UnityEngine.Object) this.gameObject && ((double) (this.mClickPos - eventData.position).magnitude <= (double) this.mClickRadiusThreshold && !this.mMultiTouched && this.OnClick != null))
        this.OnClick(eventData.position);
      if (this.OnDragEndDelegate == null)
        return;
      this.OnDragEndDelegate();
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.mIsTouching || (double) Time.time < (double) this.mPointerDownTime + 0.100000001490116)
        return;
      if (SRPG_TouchInputModule.IsMultiTouching)
      {
        this.AngularVelocity = eventData.delta;
        this.DragDelta = Vector2.zero;
      }
      else
      {
        Vector2 vector2 = (Vector2) this.transform.InverseTransformPoint((Vector3) eventData.position);
        this.Velocity = eventData.delta;
        this.DragDelta = vector2 - this.mDragStart;
        if (this.OnDragDelegate == null)
          return;
        this.OnDragDelegate();
      }
    }

    public delegate void ClickEvent(Vector2 screenPos);

    public delegate void DragEvent();
  }
}
