// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollAutoFit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    public float FitTime = 0.2f;
    public ScrollAutoFit.ScrollStopEvent OnScrollStop = new ScrollAutoFit.ScrollStopEvent();
    public ScrollAutoFit.ScrollBeginEvent OnScrollBegin = new ScrollAutoFit.ScrollBeginEvent();
    [SerializeField]
    [HideInInspector]
    public bool UseAutoFit;
    [HideInInspector]
    [SerializeField]
    public float ItemScale;
    [HideInInspector]
    [SerializeField]
    public bool HorizontalMode;
    [SerializeField]
    public float Offset;
    private ScrollAutoFit.State mState;
    private float mStartPos;
    private float mEndPos;
    private float mScrollAnimTime;
    private bool isDragging;
    private RectTransform rectTransform;
    private int mStartIdx;
    private Vector2 mStartDragPos;
    private bool mForceScroll;

    public Rect rect
    {
      get
      {
        return this.rectTransform.rect;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      this.rectTransform = this.GetComponent<RectTransform>();
    }

    public int DragStartIdx
    {
      get
      {
        return this.mStartIdx;
      }
      set
      {
        this.mStartIdx = value;
      }
    }

    public Vector2 DragStartPos
    {
      get
      {
        return this.mStartDragPos;
      }
      set
      {
        this.mStartDragPos = value;
      }
    }

    public bool IsDrag
    {
      get
      {
        return this.isDragging;
      }
    }

    public bool IsMove
    {
      get
      {
        if (!this.isDragging && this.mState == ScrollAutoFit.State.Wait)
          return (double) this.velocity.magnitude > 0.100000001490116;
        return true;
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
        case ScrollAutoFit.State.DragEnd:
          if (!this.isDragging)
          {
            if (this.HorizontalMode)
            {
              if ((double) Mathf.Abs(this.velocity.x) < (double) this.ItemScale)
              {
                this.SetScrollToHorizontal((float) this.GetCurrent() * this.ItemScale + this.Offset);
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
            float t = (double) this.FitTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.FitTime) * 3.14159274101257 * 0.5));
            Vector2 anchoredPosition = this.content.anchoredPosition;
            if (this.HorizontalMode)
              anchoredPosition.x = Mathf.Lerp(this.mStartPos, this.mEndPos, t);
            else
              anchoredPosition.y = Mathf.Lerp(this.mStartPos, this.mEndPos, t);
            this.content.anchoredPosition = anchoredPosition;
            if ((double) t >= 1.0)
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
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      this.mStartIdx = this.GetCurrent();
      base.OnBeginDrag(eventData);
      this.isDragging = true;
      this.mState = ScrollAutoFit.State.Dragging;
      this.OnScrollBegin.Invoke();
    }

    public override void OnEndDrag(PointerEventData eventData)
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

    public void SetScrollToHorizontal(float pos)
    {
      this.mForceScroll = true;
      this.mStartPos = this.content.anchoredPosition.x;
      this.mEndPos = pos;
      this.mScrollAnimTime = 0.0f;
      this.mState = ScrollAutoFit.State.Scrolling;
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
      if (this.HorizontalMode)
        return Mathf.RoundToInt((this.content.anchoredPosition.x - this.Offset) / this.ItemScale);
      return Mathf.RoundToInt(this.content.anchoredPosition.y / this.ItemScale);
    }

    private enum State
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
  }
}
