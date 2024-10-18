// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollAutoFit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "有効化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "無効化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "有効化完了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "無効化完了", FlowNode.PinTypes.Output, 3)]
  public class ScrollAutoFit : SRPG_ScrollRect, IFlowInterface
  {
    [SerializeField]
    [HideInInspector]
    public bool UseAutoFit;
    [SerializeField]
    [HideInInspector]
    public float FitTime = 0.2f;
    [SerializeField]
    [HideInInspector]
    public float ItemScale;
    [SerializeField]
    [HideInInspector]
    public bool HorizontalMode;
    [SerializeField]
    public float Offset;
    [SerializeField]
    public bool UseMoveRange;
    private ScrollAutoFit.State mState;
    private float mStartPos;
    private float mEndPos;
    private float mScrollAnimTime;
    private bool isDragging;
    private RectTransform rectTransform;
    private int mStartIdx;
    private Vector2 mStartDragPos;
    private bool mForceScroll;
    public ScrollAutoFit.ScrollStopEvent OnScrollStop = new ScrollAutoFit.ScrollStopEvent();
    public ScrollAutoFit.ScrollBeginEvent OnScrollBegin = new ScrollAutoFit.ScrollBeginEvent();
    [SerializeField]
    public ScrollAutoFit.ContentChangedEvent OnContentChanged = new ScrollAutoFit.ContentChangedEvent();

    public ScrollAutoFit.State CurrentState => this.mState;

    public Rect rect => this.rectTransform.rect;

    protected override void Awake()
    {
      base.Awake();
      this.rectTransform = ((Component) this).GetComponent<RectTransform>();
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.onValueChanged).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnContentChangedListner)));
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
      this.horizontalNormalizedPosition = 0.0f;
      this.verticalNormalizedPosition = 0.0f;
      this.velocity = Vector2.zero;
      this.StopMovement();
    }

    protected virtual void OnDestroy()
    {
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.onValueChanged).RemoveListener(new UnityAction<Vector2>((object) this, __methodptr(OnContentChangedListner)));
      ((UIBehaviour) this).OnDestroy();
    }

    private void OnContentChangedListner(Vector2 value)
    {
      if (this.HorizontalMode)
        this.OnContentChanged.Invoke(value.x);
      else
        this.OnContentChanged.Invoke(value.y);
    }

    public int DragStartIdx
    {
      get => this.mStartIdx;
      set => this.mStartIdx = value;
    }

    public Vector2 DragStartPos
    {
      get => this.mStartDragPos;
      set => this.mStartDragPos = value;
    }

    public bool IsDrag => this.isDragging;

    public bool IsMove
    {
      get
      {
        if (this.isDragging || this.mState != ScrollAutoFit.State.Wait)
          return true;
        Vector2 velocity = this.velocity;
        return (double) ((Vector2) ref velocity).magnitude > 0.10000000149011612;
      }
    }

    private void Update()
    {
      if (!this.UseAutoFit)
        return;
      switch (this.mState)
      {
        case ScrollAutoFit.State.Wait:
          this.UpdateWait();
          break;
        case ScrollAutoFit.State.Dragging:
          this.MoveContentRange();
          break;
        case ScrollAutoFit.State.DragEnd:
          if (!this.isDragging)
          {
            if (this.HorizontalMode)
            {
              if ((double) Mathf.Abs(this.velocity.x) < (double) this.ItemScale)
              {
                if (this.UseMoveRange)
                {
                  this.SetScrollToHorizontal(this.GetNearIconPos());
                  break;
                }
                this.SetScrollToHorizontal((float) this.GetCurrent() * this.ItemScale + this.Offset);
                break;
              }
              if (this.UseMoveRange && this.CheckSetScrollPos())
              {
                this.MoveContentRange();
                this.mScrollAnimTime = -1f;
                this.mState = ScrollAutoFit.State.Wait;
                this.OnScrollStop.Invoke();
                this.StopMovement();
                double nearIconPos = (double) this.GetNearIconPos();
                break;
              }
              break;
            }
            if ((double) Mathf.Abs(this.velocity.y) < (double) this.ItemScale)
            {
              this.SetScrollTo((float) Mathf.RoundToInt(this.content.anchoredPosition.y / this.ItemScale) * this.ItemScale + this.Offset);
              break;
            }
            break;
          }
          break;
        case ScrollAutoFit.State.Scrolling:
          if ((double) this.mScrollAnimTime >= 0.0)
          {
            this.mScrollAnimTime += Time.deltaTime;
            float num = (double) this.FitTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.FitTime) * 3.1415927410125732 * 0.5));
            Vector2 anchoredPosition = this.content.anchoredPosition;
            if (this.HorizontalMode)
              anchoredPosition.x = Mathf.Lerp(this.mStartPos, this.mEndPos, num);
            else
              anchoredPosition.y = Mathf.Lerp(this.mStartPos, this.mEndPos, num);
            this.content.anchoredPosition = anchoredPosition;
            if ((double) num >= 1.0)
            {
              this.mScrollAnimTime = -1f;
              this.mState = ScrollAutoFit.State.Wait;
              this.OnScrollStop.Invoke();
              this.StopMovement();
              break;
            }
            break;
          }
          break;
      }
      this.mForceScroll = false;
    }

    private void UpdateWait()
    {
      if (!this.IsScrollNow || this.isDragging || this.mForceScroll)
        return;
      this.OnScrollBegin.Invoke();
      if ((double) this.ItemScale == 0.0 || !this.UseAutoFit)
        return;
      this.mState = ScrollAutoFit.State.DragEnd;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      this.mStartIdx = this.GetCurrent();
      base.OnBeginDrag(eventData);
      this.isDragging = true;
      this.mState = ScrollAutoFit.State.Dragging;
      this.OnScrollBegin.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
      base.OnEndDrag(eventData);
      this.isDragging = false;
      if (this.mForceScroll || (double) this.ItemScale == 0.0 || !this.UseAutoFit)
        return;
      this.mState = ScrollAutoFit.State.DragEnd;
    }

    public void SetScrollTo(float pos)
    {
      this.mForceScroll = true;
      this.mStartPos = this.content.anchoredPosition.y;
      this.mEndPos = pos;
      this.mScrollAnimTime = 0.0f;
      this.mState = ScrollAutoFit.State.Scrolling;
    }

    public void SetScrollTo(int index)
    {
      this.SetScrollTo((float) -((double) index * (double) this.ItemScale + (double) this.Offset));
    }

    public void SetScrollToHorizontal(float pos)
    {
      this.mForceScroll = true;
      this.mStartPos = this.content.anchoredPosition.x;
      this.mEndPos = pos;
      this.mScrollAnimTime = 0.0f;
      this.mState = ScrollAutoFit.State.Scrolling;
    }

    public void SetScrollToHorizontal(int index)
    {
      this.SetScrollToHorizontal((float) -((double) index * (double) this.ItemScale + (double) this.Offset));
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.vertical = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      }
      if (pinID != 1)
        return;
      this.vertical = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    public int GetCurrent()
    {
      return this.HorizontalMode ? Mathf.RoundToInt((this.content.anchoredPosition.x - this.Offset) / this.ItemScale) : Mathf.RoundToInt(this.content.anchoredPosition.y / this.ItemScale);
    }

    public void Step() => this.Update();

    private void MoveContentRange()
    {
      if (!this.UseMoveRange || UnityEngine.Object.op_Equality((UnityEngine.Object) this.content, (UnityEngine.Object) null))
        return;
      ScrollContentsInfo component = ((Component) this.content).GetComponent<ScrollContentsInfo>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      this.content.anchoredPosition = component.SetRangePos(this.content.anchoredPosition);
    }

    private bool CheckSetScrollPos()
    {
      if (!this.UseMoveRange || UnityEngine.Object.op_Equality((UnityEngine.Object) this.content, (UnityEngine.Object) null))
        return false;
      ScrollContentsInfo component = ((Component) this.content).GetComponent<ScrollContentsInfo>();
      return !UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) && component.CheckRangePos(this.content.anchoredPosition.x);
    }

    private float GetNearIconPos()
    {
      float nearIconPos = (float) this.GetCurrent() * this.ItemScale + this.Offset;
      if (!this.UseMoveRange || UnityEngine.Object.op_Equality((UnityEngine.Object) this.content, (UnityEngine.Object) null))
        return nearIconPos;
      ScrollContentsInfo component = ((Component) this.content).GetComponent<ScrollContentsInfo>();
      return UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) ? nearIconPos : component.GetNearIconPos(this.content.anchoredPosition.x);
    }

    public enum State
    {
      Wait,
      Dragging,
      DragEnd,
      Scrolling,
    }

    [SerializeField]
    public class ScrollStopEvent : UnityEvent
    {
    }

    [SerializeField]
    public class ScrollBeginEvent : UnityEvent
    {
    }

    [Serializable]
    public class ContentChangedEvent : UnityEvent<float>
    {
    }
  }
}
