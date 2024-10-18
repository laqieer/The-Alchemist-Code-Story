// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldownBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public abstract class ScrollablePulldownBase : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
  {
    [SerializeField]
    protected List<PulldownItem> Items = new List<PulldownItem>();
    private int mPrevSelectionIndex = -1;
    private int mSelectionIndex = -1;
    public ScrollablePulldownBase.SelectItemEvent OnSelectionChangeDelegate;
    [SerializeField]
    protected RectTransform ItemHolder;
    [SerializeField]
    protected ScrollRect ScrollRect;
    [SerializeField]
    private Text SelectionText;
    [SerializeField]
    private GameObject BackGround;
    [SerializeField]
    private string OpenSE;
    [SerializeField]
    private string CloseSE;
    [SerializeField]
    private string SelectSE;
    private bool mOpened;
    private bool mTrackTouchPosititon;

    protected override void Start()
    {
      base.Start();
      if (!((UnityEngine.Object) this.BackGround != (UnityEngine.Object) null))
        return;
      this.BackGround.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if ((UnityEngine.Object) this.SelectionText != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.SelectionText.text))
        this.SelectionText.text = string.Empty;
      this.mSelectionIndex = -1;
    }

    protected void ResetAllStatus()
    {
      this.mSelectionIndex = -1;
    }

    public int Selection
    {
      get
      {
        return this.mSelectionIndex;
      }
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.Items.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.Items.Count; ++index)
          this.Items[index].OnStatusChanged(index == this.mSelectionIndex);
        if (!((UnityEngine.Object) this.Items[this.mSelectionIndex].Text != (UnityEngine.Object) null))
          return;
        this.SelectionText.text = this.Items[this.mSelectionIndex].Text.text;
      }
    }

    public int PrevSelection
    {
      set
      {
        this.mPrevSelectionIndex = value;
      }
    }

    private bool SelectNearestItem(PointerEventData e)
    {
      Vector2 position = e.position;
      float num1 = float.MaxValue;
      int num2 = -1;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        RectTransform transform = this.Items[index].transform as RectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, position, (UnityEngine.Camera) null, out localPoint);
        if (this.mTrackTouchPosititon)
        {
          float magnitude = localPoint.magnitude;
          if ((double) magnitude < (double) num1)
          {
            num2 = index;
            num1 = magnitude;
          }
        }
        else
        {
          Rect rect = transform.rect;
          if ((double) rect.xMin <= (double) localPoint.x && (double) localPoint.x < (double) rect.xMax && ((double) rect.yMin <= (double) localPoint.y && (double) localPoint.y < (double) rect.yMax))
            num2 = index;
        }
      }
      if (num2 < 0)
        return false;
      if (num2 != this.Selection)
        this.mTrackTouchPosititon = true;
      this.Selection = num2;
      return true;
    }

    protected void TriggerItemChange()
    {
      if (!string.IsNullOrEmpty(this.SelectSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE, 0.0f);
      if (this.mPrevSelectionIndex == this.mSelectionIndex)
        return;
      this.mPrevSelectionIndex = this.mSelectionIndex;
      int num = this.Items[this.mSelectionIndex].Value;
      if (this.OnSelectionChangeDelegate == null)
        return;
      this.OnSelectionChangeDelegate(num);
    }

    private void OnPulldownMenuTouch(BaseEventData eventData)
    {
      if (this.SelectNearestItem(eventData as PointerEventData))
      {
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }
      else
        this.ClosePulldown(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);
      if (!this.IsInteractable())
        return;
      if (this.mOpened)
        this.ClosePulldown(true);
      else
        this.OpenPulldown();
    }

    public void OpenPulldown()
    {
      if (this.mOpened || this.Items.Count <= 1)
        return;
      this.BackGround.SetActive(true);
      this.mOpened = true;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0.0f);
    }

    public void ClosePulldown(bool se = true)
    {
      if (!this.mOpened)
        return;
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
      this.BackGround.SetActive(false);
      this.mOpened = false;
      if (!se || string.IsNullOrEmpty(this.CloseSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.CloseSE, 0.0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.mOpened)
        return;
      this.SelectNearestItem(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (!this.mOpened || (double) (eventData.pressPosition - eventData.position).magnitude <= 5.0)
        return;
      this.SelectNearestItem(eventData);
      this.ClosePulldown(false);
      this.TriggerItemChange();
    }

    public PulldownItem GetItemAt(int index)
    {
      if (0 <= index && index < this.Items.Count)
        return this.Items[index];
      return (PulldownItem) null;
    }

    public int ItemCount
    {
      get
      {
        return this.Items.Count;
      }
    }

    public PulldownItem GetCurrentSelection()
    {
      return this.GetItemAt(this.mSelectionIndex);
    }

    public delegate void SelectItemEvent(int value);
  }
}
