// Decompiled with JetBrains decompiler
// Type: SRPG.TouchController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Event/TouchController")]
  public class TouchController : 
    MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler,
    IEventSystemHandler
  {
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
    private float mClickRadiusThreshold = 5f;
    private RectTransform mRectTransform;
    private Vector2 mDragStart;

    public RectTransform GetRectTransform() => this.mRectTransform;

    public bool IsTouching => this.mIsTouching;

    public Vector2 DragStart => this.mDragStart;

    public Vector2 WorldSpaceVelocity
    {
      get
      {
        Camera main = Camera.main;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null))
          return Vector2.zero;
        Transform transform = ((Component) main).transform;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0.0f;
        ((Vector3) ref forward).Normalize();
        right.y = 0.0f;
        ((Vector3) ref right).Normalize();
        return new Vector2((float) ((double) right.x * (double) this.Velocity.x + (double) forward.x * (double) this.Velocity.y), (float) ((double) right.z * (double) this.Velocity.x + (double) forward.z * (double) this.Velocity.y));
      }
    }

    public void IgnoreCurrentTouch()
    {
      this.mIsTouching = false;
      this.Velocity = Vector2.zero;
      this.AngularVelocity = Vector2.zero;
    }

    private void Awake() => this.mRectTransform = ((Component) this).GetComponent<RectTransform>();

    private void Start() => this.mClickRadiusThreshold = (float) (Screen.height / 18);

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
      this.mDragStart = Vector2.op_Implicit(((Component) this).transform.InverseTransformPoint(Vector2.op_Implicit(eventData.position)));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      this.mIsTouching = false;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) eventData.pointerPress, (UnityEngine.Object) ((Component) this).gameObject))
      {
        Vector2 vector2 = Vector2.op_Subtraction(this.mClickPos, eventData.position);
        if ((double) ((Vector2) ref vector2).magnitude <= (double) this.mClickRadiusThreshold && !this.mMultiTouched && this.OnClick != null)
          this.OnClick(eventData.position);
      }
      if (this.OnDragEndDelegate == null)
        return;
      this.OnDragEndDelegate();
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.mIsTouching || (double) Time.time < (double) this.mPointerDownTime + 0.10000000149011612)
        return;
      if (SRPG_TouchInputModule.IsMultiTouching)
      {
        this.AngularVelocity = eventData.delta;
        this.DragDelta = Vector2.zero;
      }
      else
      {
        Vector2 vector2 = Vector2.op_Implicit(((Component) this).transform.InverseTransformPoint(Vector2.op_Implicit(eventData.position)));
        this.Velocity = eventData.delta;
        this.DragDelta = Vector2.op_Subtraction(vector2, this.mDragStart);
        if (this.OnDragDelegate == null)
          return;
        this.OnDragDelegate();
      }
    }

    public delegate void ClickEvent(Vector2 screenPos);

    public delegate void DragEvent();
  }
}
