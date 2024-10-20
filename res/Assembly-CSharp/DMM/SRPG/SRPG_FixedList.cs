﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_FixedList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SRPG_FixedList : UIBehaviour
  {
    protected int mPage;
    protected int mMaxPages;
    protected int mPageSize;
    private GridLayoutGroup mGrid;
    private int mCellCountX;
    private int mCellCountY;
    private bool mStarted;
    private bool mShouldRefresh;
    private bool mCalculatedCellCounts;
    protected bool mInvokeSelChange;
    protected List<GameObject> mItems = new List<GameObject>();
    protected bool mFocusSelection;
    protected object[] mData;
    protected System.Type mDataType;
    public SRPG_FixedList.SelectionChangeEvent OnSelectionChange = (SRPG_FixedList.SelectionChangeEvent) (list => { });
    public Scrollbar PageScrollBar;
    public Text PageIndex;
    public int MaxSelection = 8;
    public Text PageIndexMax;
    public Text NumSelection;
    public Button ForwardButton;
    public Button BackButton;
    public GameObject[] ExtraItems = new GameObject[0];
    protected List<object> mSelection = new List<object>(4);
    public int MaxCellCount = 64;

    protected virtual void Start()
    {
      base.Start();
      this.mStarted = true;
    }

    public bool IsRefreshing => this.mShouldRefresh;

    public virtual RectTransform ListParent => ((Component) this).GetComponent<RectTransform>();

    public virtual void SetData(object[] src, System.Type type)
    {
      this.mData = src;
      this.mDataType = type;
      this.Refresh();
    }

    private void RecalculateCellCounts()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGrid, (UnityEngine.Object) null))
        this.mGrid = ((Component) this.ListParent).GetComponent<GridLayoutGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGrid, (UnityEngine.Object) null))
      {
        this.mCellCountX = 0;
        this.mCellCountY = 0;
      }
      else
      {
        RectTransform listParent = this.ListParent;
        Rect rect1 = listParent.rect;
        float x = ((Rect) ref rect1).size.x;
        Rect rect2 = listParent.rect;
        float y = ((Rect) ref rect2).size.y;
        this.mCellCountX = Mathf.Max(1, Mathf.FloorToInt((float) (((double) x - (double) ((LayoutGroup) this.mGrid).padding.horizontal + (double) this.mGrid.spacing.x + 1.0 / 1000.0) / ((double) this.mGrid.cellSize.x + (double) this.mGrid.spacing.x))));
        this.mCellCountY = Mathf.Max(1, Mathf.FloorToInt((float) (((double) y - (double) ((LayoutGroup) this.mGrid).padding.vertical + (double) this.mGrid.spacing.y + 1.0 / 1000.0) / ((double) this.mGrid.cellSize.y + (double) this.mGrid.spacing.y))));
        this.mCalculatedCellCounts = true;
      }
    }

    public int CellCount
    {
      get
      {
        this.RecalculateCellCounts();
        return this.mCellCountX * this.mCellCountY;
      }
    }

    public int Page
    {
      get => this.mPage;
      set
      {
        if (this.mPage == value)
          return;
        this.mPage = value;
        this.Refresh();
      }
    }

    public int MaxPage => this.mMaxPages;

    public object[] Selection => this.mSelection.ToArray();

    protected object[] Data => this.mData;

    protected virtual int DataCount => this.mData != null ? this.mData.Length : 0;

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      if (this.mCalculatedCellCounts)
        return;
      this.mShouldRefresh = true;
    }

    protected virtual void Update()
    {
      if (!this.mShouldRefresh)
        return;
      this.mShouldRefresh = false;
      this.RefreshItems();
    }

    protected virtual void LateUpdate()
    {
      if (!this.mShouldRefresh)
        return;
      this.mShouldRefresh = false;
      this.RefreshItems();
    }

    public void Refresh() => this.mShouldRefresh = true;

    public void CancelRefresh() => this.mShouldRefresh = false;

    protected virtual GameObject CreateItem() => (GameObject) null;

    protected virtual GameObject CreateItem(int index) => (GameObject) null;

    protected bool HasStarted => this.mStarted;

    protected virtual void OnUpdateItem(GameObject go, int index)
    {
    }

    protected virtual void OnItemSelect(GameObject go)
    {
    }

    protected void _OnItemSelect(GameObject go)
    {
      if (this.mShouldRefresh)
        return;
      this.OnItemSelect(go);
      object dataOfClass = DataSource.FindDataOfClass(go, this.mDataType, (object) null);
      if (dataOfClass == null)
        return;
      if (this.MaxSelection > 0)
      {
        if (this.mSelection.Contains(dataOfClass))
          this.mSelection.Remove(dataOfClass);
        else if (this.mSelection.Count < this.MaxSelection)
          this.mSelection.Add(dataOfClass);
        this.UpdateSelection();
        this.TriggerSelectionChange();
      }
      else
      {
        this.mSelection.Clear();
        this.mSelection.Add(dataOfClass);
        this.UpdateSelection();
        this.TriggerSelectionChange();
      }
    }

    protected virtual void RefreshItems()
    {
      this.mPageSize = this.CellCount;
      Transform listParent = (Transform) this.ListParent;
      while (this.mItems.Count < this.mPageSize)
      {
        GameObject gameObject = this.CreateItem();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("CreateItem returned NULL");
          return;
        }
        gameObject.transform.SetParent(listParent, false);
        this.mItems.Add(gameObject);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this._OnItemSelect);
      }
      if (this.mItems.Count == 0)
        return;
      if (this.mPageSize > 0)
      {
        this.mMaxPages = (this.DataCount + this.ExtraItems.Length + this.mPageSize - 1) / this.mPageSize;
        this.mPage = Mathf.Clamp(this.mPage, 0, this.mMaxPages - 1);
      }
      if (this.mFocusSelection)
      {
        this.mFocusSelection = false;
        if (this.mSelection != null && this.mSelection.Count > 0)
        {
          int num = Array.IndexOf<object>(this.mData, this.mSelection[0]) + this.ExtraItems.Length;
          if (num >= 0)
            this.mPage = num / this.mPageSize;
        }
      }
      this.BindData();
      for (int index = 0; index < this.ExtraItems.Length; ++index)
      {
        int num = this.mPage * this.mPageSize + index;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExtraItems[index], (UnityEngine.Object) null))
          this.ExtraItems[index].SetActive(0 <= num && num < this.ExtraItems.Length);
      }
      this.UpdateSelection();
      this.UpdatePage();
      if (!this.mInvokeSelChange)
        return;
      this.mInvokeSelChange = false;
      this.TriggerSelectionChange();
    }

    public virtual void BindData()
    {
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        int index2 = this.mPage * this.mPageSize + index1 - this.ExtraItems.Length;
        if (0 <= index2 && index2 < this.mData.Length)
        {
          DataSource.Bind(this.mItems[index1], this.mDataType, this.mData[index2]);
          this.OnUpdateItem(this.mItems[index1], index2);
          this.mItems[index1].SetActive(true);
          GameParameter.UpdateAll(this.mItems[index1]);
        }
        else
          this.mItems[index1].SetActive(false);
      }
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItems[index], (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index]);
      }
      this.mItems.Clear();
      this.mSelection.Clear();
    }

    public void ClearSelection()
    {
      this.mSelection.Clear();
      this.UpdateSelection();
      this.TriggerSelectionChange();
    }

    protected virtual void TriggerSelectionChange() => this.OnSelectionChange(this);

    public virtual void UpdatePage()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageScrollBar, (UnityEngine.Object) null))
      {
        if (this.mMaxPages >= 2)
        {
          this.PageScrollBar.size = 1f / (float) this.mMaxPages;
          this.PageScrollBar.value = (float) this.mPage / ((float) this.mMaxPages - 1f);
        }
        else
        {
          this.PageScrollBar.size = 1f;
          this.PageScrollBar.value = 0.0f;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndex, (UnityEngine.Object) null))
        this.PageIndex.text = Mathf.Min(this.mPage + 1, this.mMaxPages).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndexMax, (UnityEngine.Object) null))
        this.PageIndexMax.text = this.mMaxPages.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
        ((Selectable) this.ForwardButton).interactable = this.mPage < this.mMaxPages - 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.BackButton).interactable = this.mPage > 0;
    }

    public virtual void GotoPreviousPage()
    {
      if (this.mPage <= 0)
        return;
      --this.mPage;
      this.Refresh();
    }

    public virtual void GotoNextPage()
    {
      if (this.mPage >= this.mMaxPages - 1)
        return;
      ++this.mPage;
      this.Refresh();
    }

    public void SetPageIndex(int pIndex = -1, bool isRefresh = false)
    {
      if (0 > pIndex || pIndex > this.mMaxPages - 1)
        return;
      this.mPage = pIndex;
      if (!isRefresh)
        return;
      this.Refresh();
    }

    public void RegisterNextButtonCallBack(UnityAction callBack)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ForwardButton.onClick).AddListener(new UnityAction((object) callBack, __methodptr(Invoke)));
    }

    public void RegisterPrevButtonCallBack(UnityAction callBack)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.BackButton.onClick).AddListener(new UnityAction((object) callBack, __methodptr(Invoke)));
    }

    public void UpdateSelection()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NumSelection, (UnityEngine.Object) null))
        return;
      this.NumSelection.text = this.mSelection.Count.ToString();
    }

    public void SetSelection(object[] sel, bool invoke, bool focus)
    {
      this.mFocusSelection = focus;
      this.Refresh();
      this.mSelection.Clear();
      for (int index = 0; index < sel.Length; ++index)
      {
        if (sel[index] != null && !this.mSelection.Contains(sel[index]))
          this.mSelection.Add(sel[index]);
      }
      if (!this.mStarted)
      {
        this.mInvokeSelChange = this.mInvokeSelChange || invoke;
      }
      else
      {
        this.UpdateSelection();
        if (!invoke)
          return;
        this.TriggerSelectionChange();
      }
    }

    public delegate void SelectionChangeEvent(SRPG_FixedList list);
  }
}