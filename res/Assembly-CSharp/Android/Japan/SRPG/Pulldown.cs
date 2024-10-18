// Decompiled with JetBrains decompiler
// Type: SRPG.Pulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class Pulldown : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
  {
    private int mPrevSelectionIndex = -1;
    private int mSelectionIndex = -1;
    private List<PulldownItem> mItems = new List<PulldownItem>();
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
    private bool mOpened;
    private bool mAutoClose;
    private bool mTrackTouchPosititon;
    private bool mPulldownItemInitialized;
    private bool mPollMouseUp;

    public int Selection
    {
      get
      {
        return this.mSelectionIndex;
      }
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.mItems.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          if ((UnityEngine.Object) this.mItems[index].Overray != (UnityEngine.Object) null)
            this.mItems[index].Overray.gameObject.SetActive(index == this.mSelectionIndex);
        }
        if ((UnityEngine.Object) this.mItems[this.mSelectionIndex].Text != (UnityEngine.Object) null)
          this.SelectionText.text = this.mItems[this.mSelectionIndex].Text.text;
        if (this.OnUpdateSelection != null)
          this.OnUpdateSelection();
        else
          this.UpdateSelection();
      }
    }

    protected override void Awake()
    {
      base.Awake();
      if (!((UnityEngine.Object) this.PulldownItemTemplate == (UnityEngine.Object) null) || !((UnityEngine.Object) this.PulldownText != (UnityEngine.Object) null))
        return;
      this.PulldownItemTemplate = this.PulldownText.gameObject;
    }

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.PulldownItemTemplate != (UnityEngine.Object) null)
        this.PulldownItemTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.PulldownMenu != (UnityEngine.Object) null)
        this.PulldownMenu.gameObject.SetActive(false);
      UnityEngine.EventSystems.EventTrigger.TriggerEvent triggerEvent = new UnityEngine.EventSystems.EventTrigger.TriggerEvent();
      triggerEvent.AddListener(new UnityAction<BaseEventData>(this.OnPulldownMenuTouch));
      UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry();
      entry.eventID = EventTriggerType.PointerDown;
      entry.callback = triggerEvent;
      UnityEngine.EventSystems.EventTrigger eventTrigger = this.PulldownMenu.gameObject.RequireComponent<UnityEngine.EventSystems.EventTrigger>();
      eventTrigger.triggers = new List<UnityEngine.EventSystems.EventTrigger.Entry>();
      eventTrigger.triggers.Add(entry);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
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
      if ((UnityEngine.Object) this.PulldownItemTemplate == (UnityEngine.Object) null)
        return (PulldownItem) null;
      if (!this.mPulldownItemInitialized)
      {
        this.mPulldownItemInitialized = true;
        PulldownItem pulldownItem = this.OnSetupPulldownItem == null ? this.SetupPulldownItem(this.PulldownItemTemplate) : this.OnSetupPulldownItem(this.PulldownItemTemplate);
        pulldownItem.Text = this.PulldownText;
        pulldownItem.Graphic = this.PulldownGraphic;
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PulldownItemTemplate);
      PulldownItem component = gameObject.GetComponent<PulldownItem>();
      if ((UnityEngine.Object) component.Text != (UnityEngine.Object) null)
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
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE, 0.0f);
      if (this.mPrevSelectionIndex == this.mSelectionIndex)
        return;
      this.mPrevSelectionIndex = this.mSelectionIndex;
      int num = this.mItems[this.mSelectionIndex].Value;
      if (this.OnSelectionChange != null)
        this.OnSelectionChange(num);
      if (this.OnSelectionChangeDelegate == null)
        return;
      this.OnSelectionChangeDelegate(num);
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject);
      this.mItems.Clear();
      if ((UnityEngine.Object) this.SelectionText != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.SelectionText.text))
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
        RectTransform transform = this.mItems[index].transform as RectTransform;
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
        return;
      if (num2 != this.Selection)
        this.mTrackTouchPosititon = true;
      this.Selection = num2;
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

    private void OpenPulldown()
    {
      if (this.mOpened || this.mItems.Count <= 1)
        return;
      this.PulldownMenu.gameObject.SetActive(true);
      this.mAutoClose = false;
      this.mOpened = true;
      this.mPollMouseUp = false;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0.0f);
    }

    private void ClosePulldown(bool se = true)
    {
      if (!this.mOpened)
        return;
      this.PulldownMenu.gameObject.SetActive(false);
      this.mAutoClose = false;
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

    public override void OnPointerUp(PointerEventData eventData)
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
        this.ClosePulldown(true);
      }
    }

    protected virtual void UpdateSelection()
    {
    }

    public PulldownItem GetItemAt(int index)
    {
      if (0 <= index && index < this.mItems.Count)
        return this.mItems[index];
      return (PulldownItem) null;
    }

    public int ItemCount
    {
      get
      {
        return this.mItems.Count;
      }
    }

    public PulldownItem GetCurrentSelection()
    {
      return this.GetItemAt(this.mSelectionIndex);
    }

    public delegate PulldownItem SetupPulldownItemEvent(GameObject go);

    public delegate void UpdateSelectionEvent();

    public delegate void SelectItemEvent(int value);
  }
}
