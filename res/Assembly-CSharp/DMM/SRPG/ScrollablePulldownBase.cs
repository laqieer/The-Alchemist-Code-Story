// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldownBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public abstract class ScrollablePulldownBase : 
    Selectable,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IEventSystemHandler
  {
    public ScrollablePulldownBase.SelectItemEvent OnSelectionChangeDelegate;
    [SerializeField]
    protected List<PulldownItem> Items = new List<PulldownItem>();
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
    [SerializeField]
    private bool ForceOpen;
    [SerializeField]
    private bool SizeAdjustable;
    [SerializeField]
    private int MinItemCount = 1;
    [SerializeField]
    private int MaxItemCount = 5;
    [SerializeField]
    private int ItemHeight = 20;
    [SerializeField]
    private bool IsAutoPositionItemHolder;
    private int mPrevSelectionIndex = -1;
    private int mSelectionIndex = -1;
    private bool mOpened;
    private bool mTrackTouchPosititon;

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (!Object.op_Inequality((Object) this.BackGround, (Object) null))
        return;
      this.BackGround.gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
      ((UIBehaviour) this).OnDestroy();
      if (Object.op_Inequality((Object) this.SelectionText, (Object) null) && !string.IsNullOrEmpty(this.SelectionText.text))
        this.SelectionText.text = string.Empty;
      this.mSelectionIndex = -1;
    }

    protected void ResetAllStatus() => this.mSelectionIndex = -1;

    public void ClearSelection()
    {
      this.mSelectionIndex = -1;
      this.mPrevSelectionIndex = -1;
    }

    public int Selection
    {
      get => this.mSelectionIndex;
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.Items.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.Items.Count; ++index)
          this.Items[index].OnStatusChanged(index == this.mSelectionIndex);
        if (!Object.op_Inequality((Object) this.Items[this.mSelectionIndex].Text, (Object) null))
          return;
        this.SelectionText.text = this.Items[this.mSelectionIndex].Text.text;
      }
    }

    public int PrevSelection
    {
      set => this.mPrevSelectionIndex = value;
      get => this.mPrevSelectionIndex;
    }

    private bool SelectNearestItem(PointerEventData e)
    {
      Vector2 position = e.position;
      float num1 = float.MaxValue;
      int num2 = -1;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        RectTransform transform = ((Component) this.Items[index]).transform as RectTransform;
        Vector2 vector2;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, position, (Camera) null, ref vector2);
        if (this.mTrackTouchPosititon)
        {
          float magnitude = ((Vector2) ref vector2).magnitude;
          if ((double) magnitude < (double) num1)
          {
            num2 = index;
            num1 = magnitude;
          }
        }
        else
        {
          Rect rect = transform.rect;
          if ((double) ((Rect) ref rect).xMin <= (double) vector2.x && (double) vector2.x < (double) ((Rect) ref rect).xMax && (double) ((Rect) ref rect).yMin <= (double) vector2.y && (double) vector2.y < (double) ((Rect) ref rect).yMax)
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
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE);
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
        this.ClosePulldown();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);
      if (!this.IsInteractable())
        return;
      if (this.mOpened)
        this.ClosePulldown();
      else
        this.OpenPulldown();
    }

    public void OpenPulldown()
    {
      if (this.mOpened || !this.ForceOpen && this.Items.Count <= 1)
        return;
      if (this.SizeAdjustable && Object.op_Inequality((Object) this.ScrollRect, (Object) null))
      {
        RectTransform component = ((Component) this.ScrollRect).GetComponent<RectTransform>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          int num = Mathf.Clamp(this.Items.Count, this.MinItemCount, this.MaxItemCount);
          Vector2 sizeDelta = component.sizeDelta;
          sizeDelta.y = (float) (num * this.ItemHeight);
          component.sizeDelta = sizeDelta;
        }
      }
      if (Object.op_Inequality((Object) this.BackGround, (Object) null))
      {
        this.BackGround.SetActive(true);
        if (this.IsAutoPositionItemHolder)
          this.BackGround.transform.position = ((Component) this).transform.position;
      }
      this.mOpened = true;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE);
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
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.CloseSE);
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
      if (!this.mOpened)
        return;
      Vector2 vector2 = Vector2.op_Subtraction(eventData.pressPosition, eventData.position);
      if ((double) ((Vector2) ref vector2).magnitude <= 5.0)
        return;
      this.SelectNearestItem(eventData);
      this.ClosePulldown(false);
      this.TriggerItemChange();
    }

    public PulldownItem GetItemAt(int index)
    {
      return 0 <= index && index < this.Items.Count ? this.Items[index] : (PulldownItem) null;
    }

    public int ItemCount => this.Items.Count;

    public PulldownItem GetCurrentSelection() => this.GetItemAt(this.mSelectionIndex);

    public int IndexOf(PulldownItem item) => this.Items.IndexOf(item);

    public delegate void SelectItemEvent(int value);
  }
}
