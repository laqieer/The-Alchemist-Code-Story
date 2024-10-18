// Decompiled with JetBrains decompiler
// Type: SRPG.Pulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class Pulldown : 
    Selectable,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IEventSystemHandler
  {
    public Pulldown.SetupPulldownItemEvent OnSetupPulldownItem;
    public Pulldown.UpdateSelectionEvent OnUpdateSelection;
    public RectTransform PulldownMenu;
    public Text SelectionText;
    public GameObject PulldownItemTemplate;
    public Text PulldownText;
    public Graphic PulldownGraphic;
    public string OpenSE;
    public string CloseSE;
    public string SelectSE;
    public Pulldown.SelectItemEvent OnSelectionChangeDelegate;
    public UnityAction<int> OnSelectionChange;
    private int mPrevSelectionIndex = -1;
    private int mSelectionIndex = -1;
    private bool mOpened;
    private bool mAutoClose;
    private bool mTrackTouchPosititon;
    private List<PulldownItem> mItems = new List<PulldownItem>();
    private bool mPulldownItemInitialized;
    private bool mPollMouseUp;

    public int Selection
    {
      get => this.mSelectionIndex;
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.mItems.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          if (Object.op_Inequality((Object) this.mItems[index].Overray, (Object) null))
            ((Component) this.mItems[index].Overray).gameObject.SetActive(index == this.mSelectionIndex);
        }
        if (Object.op_Inequality((Object) this.mItems[this.mSelectionIndex].Text, (Object) null))
          this.SelectionText.text = this.mItems[this.mSelectionIndex].Text.text;
        if (this.OnUpdateSelection != null)
          this.OnUpdateSelection();
        else
          this.UpdateSelection();
      }
    }

    protected virtual void Awake()
    {
      base.Awake();
      if (!Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null) || !Object.op_Inequality((Object) this.PulldownText, (Object) null))
        return;
      this.PulldownItemTemplate = ((Component) this.PulldownText).gameObject;
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (Object.op_Inequality((Object) this.PulldownItemTemplate, (Object) null))
        this.PulldownItemTemplate.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.PulldownMenu, (Object) null))
        ((Component) this.PulldownMenu).gameObject.SetActive(false);
      EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
      // ISSUE: method pointer
      ((UnityEvent<BaseEventData>) triggerEvent).AddListener(new UnityAction<BaseEventData>((object) this, __methodptr(OnPulldownMenuTouch)));
      EventTrigger.Entry entry = new EventTrigger.Entry();
      entry.eventID = (EventTriggerType) 2;
      entry.callback = triggerEvent;
      EventTrigger eventTrigger = ((Component) this.PulldownMenu).gameObject.RequireComponent<EventTrigger>();
      eventTrigger.triggers = new List<EventTrigger.Entry>();
      eventTrigger.triggers.Add(entry);
    }

    protected virtual void OnDestroy()
    {
      ((UIBehaviour) this).OnDestroy();
      this.ClearItems();
    }

    private void OnPulldownMenuTouch(BaseEventData eventData)
    {
      this.SelectNearestItem(eventData as PointerEventData);
      this.ClosePulldown(false);
      this.TriggerItemChange();
    }

    protected virtual PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      return itemObject.AddComponent<PulldownItem>();
    }

    public virtual PulldownItem AddItem(string label, int value)
    {
      if (Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null))
        return (PulldownItem) null;
      if (!this.mPulldownItemInitialized)
      {
        this.mPulldownItemInitialized = true;
        PulldownItem pulldownItem = this.OnSetupPulldownItem == null ? this.SetupPulldownItem(this.PulldownItemTemplate) : this.OnSetupPulldownItem(this.PulldownItemTemplate);
        pulldownItem.Text = this.PulldownText;
        pulldownItem.Graphic = this.PulldownGraphic;
      }
      GameObject gameObject = Object.Instantiate<GameObject>(this.PulldownItemTemplate);
      PulldownItem component = gameObject.GetComponent<PulldownItem>();
      if (Object.op_Inequality((Object) component.Text, (Object) null))
        component.Text.text = label;
      component.Value = value;
      this.mItems.Add(component);
      gameObject.transform.SetParent((Transform) this.PulldownMenu, false);
      gameObject.SetActive(true);
      return component;
    }

    private void TriggerItemChange()
    {
      if (!string.IsNullOrEmpty(this.SelectSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE);
      if (this.mPrevSelectionIndex == this.mSelectionIndex)
        return;
      this.mPrevSelectionIndex = this.mSelectionIndex;
      int num = this.mItems[this.mSelectionIndex].Value;
      if (this.OnSelectionChange != null)
        this.OnSelectionChange.Invoke(num);
      if (this.OnSelectionChangeDelegate == null)
        return;
      this.OnSelectionChangeDelegate(num);
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).gameObject);
      this.mItems.Clear();
      if (Object.op_Inequality((Object) this.SelectionText, (Object) null) && !string.IsNullOrEmpty(this.SelectionText.text))
        this.SelectionText.text = string.Empty;
      this.mSelectionIndex = -1;
    }

    private void SelectNearestItem(PointerEventData e)
    {
      Vector2 position = e.position;
      float num1 = float.MaxValue;
      int num2 = -1;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        RectTransform transform = ((Component) this.mItems[index]).transform as RectTransform;
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
        return;
      if (num2 != this.Selection)
        this.mTrackTouchPosititon = true;
      this.Selection = num2;
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

    protected virtual void OpenPulldown()
    {
      if (this.mOpened || this.mItems.Count <= 1)
        return;
      ((Component) this.PulldownMenu).gameObject.SetActive(true);
      this.mAutoClose = false;
      this.mOpened = true;
      this.mPollMouseUp = false;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE);
    }

    protected virtual void ClosePulldown(bool se = true)
    {
      if (!this.mOpened)
        return;
      ((Component) this.PulldownMenu).gameObject.SetActive(false);
      this.mAutoClose = false;
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

    public virtual void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);
      this.mAutoClose = true;
    }

    private void Update()
    {
      if (!this.mAutoClose || !Input.GetMouseButtonUp(0))
        return;
      if (!this.mPollMouseUp)
      {
        this.mPollMouseUp = true;
      }
      else
      {
        this.mAutoClose = false;
        this.ClosePulldown();
      }
    }

    protected virtual void UpdateSelection()
    {
    }

    public PulldownItem GetItemAt(int index)
    {
      return 0 <= index && index < this.mItems.Count ? this.mItems[index] : (PulldownItem) null;
    }

    public int ItemCount => this.mItems.Count;

    public PulldownItem GetCurrentSelection() => this.GetItemAt(this.mSelectionIndex);

    public void CloseSelf() => this.ClosePulldown();

    public delegate PulldownItem SetupPulldownItemEvent(GameObject go);

    public delegate void UpdateSelectionEvent();

    public delegate void SelectItemEvent(int value);
  }
}
