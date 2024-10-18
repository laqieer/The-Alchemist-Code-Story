// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardEquipList : UIBehaviour
  {
    private List<ConceptCardData> mCardDatas = new List<ConceptCardData>();
    private List<ConceptCardIcon> mCardIcons = new List<ConceptCardIcon>();
    private bool mIsInitialized;
    private bool mIsChangeRectSize;
    private bool mIsKeepCurrentPage;
    private RectTransform mRectTransform;
    private GridLayoutGroup mGrid;
    private int mFirstCardIndex;
    private int mCurrentPage;
    private int mLastPage;
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

    public int CurrentPage
    {
      get
      {
        return this.mCurrentPage;
      }
    }

    public int LastPage
    {
      get
      {
        return this.mLastPage;
      }
    }

    private RectTransform RT
    {
      get
      {
        if ((UnityEngine.Object) this.mRectTransform == (UnityEngine.Object) null)
          this.mRectTransform = (RectTransform) this.transform;
        return this.mRectTransform;
      }
    }

    private GridLayoutGroup Grid
    {
      get
      {
        if ((UnityEngine.Object) this.mGrid == (UnityEngine.Object) null)
          this.mGrid = this.GetComponent<GridLayoutGroup>();
        return this.mGrid;
      }
    }

    public ConceptCardData SelectedConceptCardData
    {
      get
      {
        return this.mSelectedConceptCardData;
      }
    }

    public bool IsIgnoreEquipedConceptCard
    {
      get
      {
        return PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, 0) != 0;
      }
      set
      {
        int num = !value ? 0 : 1;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, num, false);
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
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card =>
      {
        if ((UnityEngine.Object) card != (UnityEngine.Object) null && card.ConceptCard != null)
          return (long) card.ConceptCard.UniqueID == (long) this.mReservedSelectConceptCardData.UniqueID;
        return false;
      }));
      if ((UnityEngine.Object) selected_icon != (UnityEngine.Object) null)
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
      List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>();
      for (int i = 0; i < card_datas.Count; ++i)
      {
        UnitData unitData = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u =>
        {
          if (u.ConceptCard != null)
            return (long) u.ConceptCard.UniqueID == (long) card_datas[i].UniqueID;
          return false;
        }));
        if (!equipedConceptCard || unitData == null || unitData.UniqueID == selected_unit.UniqueID)
          conceptCardDataList.Add(card_datas[i]);
      }
      this.mIsChangeRectSize = true;
      this.mIsKeepCurrentPage = is_keep_page;
      this.mCardEmptyMessageText.gameObject.SetActive(conceptCardDataList.Count <= 0);
      this.mCardDatas.Clear();
      this.mCardDatas.Add((ConceptCardData) null);
      this.mCardDatas.AddRange((IEnumerable<ConceptCardData>) conceptCardDataList);
      this.Sort(ConceptCardListSortWindow.LoadDataType(), ConceptCardListSortWindow.LoadDataOrderType());
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
            this.mCardIcons[index2].gameObject.SetActive(false);
        }
      }
      int activeIconCount = this.GetActiveIconCount();
      if (activeIconCount >= num1)
        return;
      int num3 = num1 - activeIconCount;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (!this.mCardIcons[index].gameObject.activeSelf)
        {
          this.mCardIcons[index].gameObject.SetActive(true);
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
          this.mCardIcons[index1].gameObject.SetActive(false);
        }
        else
          this.mCardIcons[index1].Setup(this.mCardDatas[index2]);
      }
    }

    public void Sort(ConceptCardListSortWindow.Type SortType, ConceptCardListSortWindow.Type SortOrderType)
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
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
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

    public void SelectCardIcon(ConceptCardIcon selected_icon)
    {
      this.ResetSelectCardAnimation();
      if ((UnityEngine.Object) selected_icon == (UnityEngine.Object) null)
      {
        this.mSelectedConceptCardData = (ConceptCardData) null;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon((ConceptCardIcon) null);
      }
      else
      {
        this.mSelectedConceptCardData = selected_icon.ConceptCard;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon(selected_icon);
        Animator component = selected_icon.GetComponent<Animator>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.SetInteger("st", 1);
      }
    }

    private void ResetSelectCardAnimation()
    {
      int activeIconCount = this.GetActiveIconCount();
      for (int index = 0; index < activeIconCount; ++index)
      {
        Animator component = this.mCardIcons[index].GetComponent<Animator>();
        if (!((UnityEngine.Object) component == (UnityEngine.Object) null))
          component.SetInteger("st", 0);
      }
    }

    private void SelectedCardIconActive()
    {
      if (this.mSelectedConceptCardData == null)
        return;
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card =>
      {
        if ((UnityEngine.Object) card != (UnityEngine.Object) null && card.ConceptCard != null)
          return (long) card.ConceptCard.UniqueID == (long) this.mSelectedConceptCardData.UniqueID;
        return false;
      }));
      if (!((UnityEngine.Object) selected_icon != (UnityEngine.Object) null))
        return;
      this.SelectCardIcon(selected_icon);
    }

    private int GetCardPage(ConceptCardData target)
    {
      if (target == null)
        return -1;
      int index1 = this.mCardDatas.FindIndex((Predicate<ConceptCardData>) (data =>
      {
        if (data != null)
          return (long) data.UniqueID == (long) target.UniqueID;
        return false;
      }));
      if (index1 <= -1)
        return -1;
      int num = -1;
      int mCurrentPage = this.mCurrentPage;
      this.mCurrentPage = 0;
      for (int index2 = 0; index2 <= this.LastPage; ++index2)
      {
        if (index1 < this.mFirstCardIndex + this.CellCount)
        {
          num = Mathf.Min(this.mCurrentPage, this.LastPage);
          break;
        }
        ++this.mCurrentPage;
        this.RefreshFirstIconIndex();
      }
      this.mCurrentPage = mCurrentPage;
      this.RefreshFirstIconIndex();
      return num;
    }

    public void OpenSelectIconExistPage(ConceptCardData card)
    {
      if (card == null)
        return;
      this.mReservedSelectConceptCardData = card;
    }

    private int GetActiveIconCount()
    {
      int num = 0;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (this.mCardIcons[index].gameObject.activeSelf)
          ++num;
      }
      return num;
    }

    private int CellCount
    {
      get
      {
        int max = 64;
        if ((UnityEngine.Object) this.Grid == (UnityEngine.Object) null)
        {
          DebugUtility.LogError("ERROR!! : Dont Setup GridLayoutGroup");
          return 0;
        }
        float x1 = this.Grid.cellSize.x;
        float y1 = this.Grid.cellSize.y;
        float x2 = this.Grid.spacing.x;
        float y2 = this.Grid.spacing.y;
        float horizontal = (float) this.Grid.padding.horizontal;
        float vertical = (float) this.Grid.padding.vertical;
        float num1 = this.RT.rect.width - horizontal + x2;
        float num2 = this.RT.rect.height - vertical + y2;
        return Mathf.Clamp(Mathf.FloorToInt(num1 / (x1 + x2)) * Mathf.FloorToInt(num2 / (y1 + y2)), 0, max);
      }
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.mIsChangeRectSize = true;
    }
  }
}
