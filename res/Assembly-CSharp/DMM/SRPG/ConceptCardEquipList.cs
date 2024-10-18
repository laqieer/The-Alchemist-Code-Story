// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardEquipList : UIBehaviour
  {
    private bool mIsInitialized;
    private bool mIsChangeRectSize;
    private bool mIsKeepCurrentPage;
    private RectTransform mRectTransform;
    private GridLayoutGroup mGrid;
    private List<ConceptCardData> mCardDatas = new List<ConceptCardData>();
    private List<ConceptCardData> mCardDatasDefault = new List<ConceptCardData>();
    private List<ConceptCardIcon> mCardIcons = new List<ConceptCardIcon>();
    private int mFirstCardIndex;
    private int mCurrentPage;
    private int mLastPage;
    private UnitData mSelectedUnit;
    private ConceptCardData mSelectedConceptCardData;
    private ConceptCardData mReservedSelectConceptCardData;
    [SerializeField]
    private GameObject mNoEquipButtonObject;
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private Text mCardEmptyMessageText;

    public int CurrentPage => this.mCurrentPage;

    public int LastPage => this.mLastPage;

    private RectTransform RT
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRectTransform, (UnityEngine.Object) null))
          this.mRectTransform = (RectTransform) ((Component) this).transform;
        return this.mRectTransform;
      }
    }

    private GridLayoutGroup Grid
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGrid, (UnityEngine.Object) null))
          this.mGrid = ((Component) this).GetComponent<GridLayoutGroup>();
        return this.mGrid;
      }
    }

    public ConceptCardData SelectedConceptCardData => this.mSelectedConceptCardData;

    public bool IsIgnoreEquipedConceptCard
    {
      get => PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED) != 0;
      set
      {
        int num = !value ? 0 : 1;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, num);
      }
    }

    private void LateUpdate()
    {
      if (!this.mIsInitialized || !this.mIsChangeRectSize)
        return;
      this.mLastPage = this.CalcLastPage();
      int num = -1;
      if (this.mIsKeepCurrentPage)
      {
        this.mCurrentPage = Mathf.Min(this.mCurrentPage, this.LastPage);
      }
      else
      {
        num = this.GetCardPage(this.mReservedSelectConceptCardData);
        if (num >= 0)
          this.mCurrentPage = num;
      }
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.mIsChangeRectSize = false;
      if (num < 0)
        return;
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card => UnityEngine.Object.op_Inequality((UnityEngine.Object) card, (UnityEngine.Object) null) && card.ConceptCard != null && (long) card.ConceptCard.UniqueID == (long) this.mReservedSelectConceptCardData.UniqueID));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
        this.SelectCardIcon(selected_icon);
      this.mReservedSelectConceptCardData = (ConceptCardData) null;
    }

    public void HideTemplateObject()
    {
      this.mCardObjectTemplate.SetActive(false);
      this.mNoEquipButtonObject.SetActive(false);
    }

    public void Init(List<ConceptCardData> card_datas, UnitData selected_unit, bool is_keep_page = false)
    {
      bool equipedConceptCard = this.IsIgnoreEquipedConceptCard;
      List<ConceptCardData> collection = new List<ConceptCardData>();
      for (int i = 0; i < card_datas.Count; ++i)
      {
        UnitData unitData = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u => u.IsEquipConceptCard((long) card_datas[i].UniqueID)));
        if (!equipedConceptCard || unitData == null)
          collection.Add(card_datas[i]);
      }
      this.mIsChangeRectSize = true;
      this.mIsKeepCurrentPage = is_keep_page;
      ((Component) this.mCardEmptyMessageText).gameObject.SetActive(collection.Count <= 0);
      this.mCardDatas.Clear();
      this.mCardDatas.Add((ConceptCardData) null);
      this.mCardDatas.AddRange((IEnumerable<ConceptCardData>) collection);
      this.mCardDatasDefault.Clear();
      this.mCardDatasDefault.AddRange((IEnumerable<ConceptCardData>) this.mCardDatas);
      this.Sort(ConceptCardListSortWindow.LoadDataType(), ConceptCardListSortWindow.LoadDataOrderType());
      this.Filter();
      this.mCurrentPage = !is_keep_page ? 0 : this.mCurrentPage;
      this.mIsInitialized = true;
    }

    public void PageNext()
    {
      if (this.mCurrentPage >= this.mLastPage)
        return;
      ++this.mCurrentPage;
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    public void PageBack()
    {
      if (this.mCurrentPage <= 0)
        return;
      --this.mCurrentPage;
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    public void PageDefault()
    {
      this.mCurrentPage = 0;
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    private void CreateIcon()
    {
      int num1 = this.CellCount;
      while (this.mCardIcons.Count < num1)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mCardObjectTemplate);
        gameObject.transform.SetParent((Transform) this.mCardObjectParent, false);
        gameObject.SetActive(true);
        this.mCardIcons.Add(gameObject.GetComponent<ConceptCardIcon>());
      }
      if (this.mCurrentPage == 0)
        num1 = Mathf.Max(0, num1 - 1);
      if (this.mCardIcons.Count > num1)
      {
        int num2 = this.mCardIcons.Count - num1;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          int index2 = this.mCardIcons.Count - 1 - index1;
          if (this.mCardIcons.Count > index2 && index2 >= 0)
            ((Component) this.mCardIcons[index2]).gameObject.SetActive(false);
        }
      }
      int activeIconCount = this.GetActiveIconCount();
      if (activeIconCount >= num1)
        return;
      int num3 = num1 - activeIconCount;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (!((Component) this.mCardIcons[index]).gameObject.activeSelf)
        {
          ((Component) this.mCardIcons[index]).gameObject.SetActive(true);
          --num3;
          if (num3 <= 0)
            break;
        }
      }
    }

    private void RefreshIcon()
    {
      int activeIconCount = this.GetActiveIconCount();
      for (int index1 = 0; index1 < activeIconCount; ++index1)
      {
        int index2 = this.mFirstCardIndex + index1;
        if (index2 >= this.mCardDatas.Count)
        {
          this.mCardIcons[index1].ResetIcon();
          ((Component) this.mCardIcons[index1]).gameObject.SetActive(false);
        }
        else
        {
          this.mCardIcons[index1].Setup(this.mCardDatas[index2]);
          if (this.mSelectedUnit.UnitParam.IsNoRecommended())
            this.mCardIcons[index1].SetRecommendFlag(false);
          else if (MonoSingleton<GameManager>.Instance.MasterParam.FindUnitConceptCardConditions(this.mSelectedUnit, this.mCardDatas[index2]) > 0)
            this.mCardIcons[index1].SetRecommendFlag(true);
          else
            this.mCardIcons[index1].SetRecommendFlag(false);
        }
      }
    }

    public void Sort(
      ConceptCardListSortWindow.Type SortType,
      ConceptCardListSortWindow.Type SortOrderType)
    {
      if (this.mSelectedUnit.UnitParam.IsNoRecommended())
      {
        bool flag = false;
        for (int index = 0; index < this.mCardDatas.Count; ++index)
        {
          if (this.mCardDatas[index] == null)
            flag = true;
        }
        ConceptCardListSortWindow.Sort(SortType, SortOrderType, this.mCardDatas);
        if (flag)
          this.mCardDatas.Insert(0, (ConceptCardData) null);
      }
      else
      {
        List<ConceptCardData> conceptCardDataList1 = new List<ConceptCardData>();
        List<ConceptCardData> collection = new List<ConceptCardData>();
        List<ConceptCardData> conceptCardDataList2 = new List<ConceptCardData>();
        List<int> intList = new List<int>();
        int num = 0;
        ConceptCardListSortWindow.Sort(SortType, SortOrderType, this.mCardDatas);
        for (int index = 0; index < this.mCardDatas.Count; ++index)
        {
          if (this.mCardDatas[index] != null)
          {
            int conceptCardConditions = MonoSingleton<GameManager>.Instance.MasterParam.FindUnitConceptCardConditions(this.mSelectedUnit, this.mCardDatas[index]);
            if (conceptCardConditions > 0)
            {
              conceptCardDataList1.Add(this.mCardDatas[index]);
              intList.Add(conceptCardConditions);
              if (num < conceptCardConditions)
                num = conceptCardConditions;
            }
            else
              collection.Add(this.mCardDatas[index]);
          }
        }
        for (int index1 = 0; index1 <= num; ++index1)
        {
          for (int index2 = 0; index2 < intList.Count; ++index2)
          {
            if (intList[index2] == index1)
              conceptCardDataList2.Add(conceptCardDataList1[index2]);
          }
        }
        conceptCardDataList2.AddRange((IEnumerable<ConceptCardData>) collection);
        this.mCardDatas = conceptCardDataList2;
        this.mCardDatas.Insert(0, (ConceptCardData) null);
      }
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    public void Filter()
    {
      this.mCardDatas.Clear();
      this.mCardDatas.AddRange((IEnumerable<ConceptCardData>) this.mCardDatasDefault);
      this.Sort(ConceptCardListSortWindow.LoadDataType(), ConceptCardListSortWindow.LoadDataOrderType());
      FilterConceptCardPrefs filter = FilterConceptCardPrefs.Load();
      for (int index = this.mCardDatas.Count - 1; index >= 0; --index)
      {
        if (this.mCardDatas[index] != null && !this.mCardDatas[index].Filter(filter))
          this.mCardDatas.Remove(this.mCardDatas[index]);
      }
      this.PageDefault();
      this.mIsChangeRectSize = true;
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
      this.RefreshSelectedCard();
    }

    private int CalcLastPage()
    {
      int cellCount = this.CellCount;
      if (cellCount == 0)
        return 0;
      int num = this.mCardDatas.Count % cellCount != 0 ? 1 : 0;
      return Mathf.Max(this.mCardDatas.Count / cellCount + num - 1, 0);
    }

    private void RefreshFirstIconIndex()
    {
      this.mFirstCardIndex = this.mCurrentPage * this.CellCount;
      if (this.mCurrentPage > 0)
        return;
      this.mFirstCardIndex = 1;
    }

    private void RefreshNoEquipButtonObject()
    {
      this.mNoEquipButtonObject.SetActive(this.mCurrentPage == 0);
    }

    public void SelectedUnitData(UnitData unit) => this.mSelectedUnit = unit;

    public void SelectCardIcon(ConceptCardIcon selected_icon)
    {
      this.ResetSelectCardAnimation();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
      {
        this.mSelectedConceptCardData = (ConceptCardData) null;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon((ConceptCardIcon) null);
      }
      else
      {
        this.mSelectedConceptCardData = selected_icon.ConceptCard;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon(selected_icon);
        Animator component = ((Component) selected_icon).GetComponent<Animator>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.SetInteger("st", 1);
      }
    }

    private void ResetSelectCardAnimation()
    {
      int activeIconCount = this.GetActiveIconCount();
      for (int index = 0; index < activeIconCount; ++index)
      {
        Animator component = ((Component) this.mCardIcons[index]).GetComponent<Animator>();
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetInteger("st", 0);
      }
    }

    private void RefreshSelectedCard()
    {
      if (this.mSelectedConceptCardData == null)
        return;
      for (int index = this.mCardDatas.Count - 1; index >= 0; --index)
      {
        if (this.mCardDatas[index] != null && (long) this.mCardDatas[index].UniqueID == (long) this.mSelectedConceptCardData.UniqueID)
          return;
      }
      ConceptCardEquipWindow.Instance.RefreshSelectedCard((ConceptCardIcon) null);
    }

    private void SelectedCardIconActive()
    {
      if (this.mSelectedConceptCardData == null)
        return;
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card => UnityEngine.Object.op_Inequality((UnityEngine.Object) card, (UnityEngine.Object) null) && card.ConceptCard != null && (long) card.ConceptCard.UniqueID == (long) this.mSelectedConceptCardData.UniqueID));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
        return;
      this.SelectCardIcon(selected_icon);
    }

    private int GetCardPage(ConceptCardData target)
    {
      if (target == null)
        return -1;
      int index1 = this.mCardDatas.FindIndex((Predicate<ConceptCardData>) (data => data != null && (long) data.UniqueID == (long) target.UniqueID));
      if (index1 <= -1)
        return -1;
      int cardPage = -1;
      int mCurrentPage = this.mCurrentPage;
      this.mCurrentPage = 0;
      for (int index2 = 0; index2 <= this.LastPage; ++index2)
      {
        if (index1 < this.mFirstCardIndex + this.CellCount)
        {
          cardPage = Mathf.Min(this.mCurrentPage, this.LastPage);
          break;
        }
        ++this.mCurrentPage;
        this.RefreshFirstIconIndex();
      }
      this.mCurrentPage = mCurrentPage;
      this.RefreshFirstIconIndex();
      return cardPage;
    }

    public void OpenSelectIconExistPage(ConceptCardData card)
    {
      if (card == null)
        return;
      this.mReservedSelectConceptCardData = card;
    }

    private int GetActiveIconCount()
    {
      int activeIconCount = 0;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (((Component) this.mCardIcons[index]).gameObject.activeSelf)
          ++activeIconCount;
      }
      return activeIconCount;
    }

    private int CellCount
    {
      get
      {
        int num1 = 64;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Grid, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("ERROR!! : Dont Setup GridLayoutGroup");
          return 0;
        }
        float x1 = this.Grid.cellSize.x;
        float y1 = this.Grid.cellSize.y;
        float x2 = this.Grid.spacing.x;
        float y2 = this.Grid.spacing.y;
        float horizontal = (float) ((LayoutGroup) this.Grid).padding.horizontal;
        float vertical = (float) ((LayoutGroup) this.Grid).padding.vertical;
        Rect rect1 = this.RT.rect;
        float num2 = ((Rect) ref rect1).width - horizontal + x2;
        Rect rect2 = this.RT.rect;
        float num3 = ((Rect) ref rect2).height - vertical + y2;
        return Mathf.Clamp(Mathf.FloorToInt(num2 / (x1 + x2)) * Mathf.FloorToInt(num3 / (y1 + y2)), 0, num1);
      }
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.mIsChangeRectSize = true;
    }
  }
}
