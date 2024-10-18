// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardEquipScrollList : MonoBehaviour
  {
    private List<ConceptCardData> mCardDatas = new List<ConceptCardData>();
    private List<ConceptCardData> mCardDatasDefault = new List<ConceptCardData>();
    private UnitData mSelectedUnit;
    private ConceptCardData mSelectedConceptCardData;
    [SerializeField]
    private Text mCardEmptyMessageText;
    [SerializeField]
    private ContentController mContentController;
    private List<ConceptCardIconParam> mIconParams = new List<ConceptCardIconParam>();
    private List<string> mExcludeEquipTypeIname;
    private int mSelectedCardSlotIndex;
    private bool mIsEnableDecreaseEffect;

    public void Init(
      List<ConceptCardData> card_datas,
      UnitData selected_unit,
      int selectedCardIndex,
      bool is_keep_select = false,
      List<string> exclude_card_iname = null)
    {
      bool equipedConceptCard = this.IsIgnoreEquipedConceptCard;
      List<ConceptCardData> collection = new List<ConceptCardData>();
      for (int index = 0; index < card_datas.Count; ++index)
      {
        UnitData owner = card_datas[index].GetOwner();
        if (!equipedConceptCard || owner == null)
          collection.Add(card_datas[index]);
      }
      if (!is_keep_select && selected_unit != null)
        this.mSelectedConceptCardData = selected_unit.GetConceptCardByIndex(selectedCardIndex);
      this.mExcludeEquipTypeIname = exclude_card_iname;
      this.mSelectedCardSlotIndex = selectedCardIndex;
      this.mCardDatas.Clear();
      this.mCardDatas.Add((ConceptCardData) null);
      this.mCardDatas.AddRange((IEnumerable<ConceptCardData>) collection);
      this.mCardDatasDefault.Clear();
      this.mCardDatasDefault.AddRange((IEnumerable<ConceptCardData>) this.mCardDatas);
      this.Sort(ConceptCardListSortWindow.LoadDataType(), ConceptCardListSortWindow.LoadDataOrderType());
      this.Filter();
      this.Refresh();
    }

    public void Refresh(List<ConceptCardData> card_datas)
    {
      this.mContentController.Release();
      ContentSource source = new ContentSource();
      this.mIconParams.Clear();
      bool flag = true;
      if (card_datas != null)
      {
        for (int index1 = 0; index1 < card_datas.Count; ++index1)
        {
          ConceptCardData cardData = card_datas[index1];
          ConceptCardIconParam conceptCardIconParam = new ConceptCardIconParam();
          if (cardData != null)
            flag = false;
          conceptCardIconParam.ConceptCard = cardData;
          conceptCardIconParam.Enable = this.mExcludeEquipTypeIname == null || cardData == null || !this.mExcludeEquipTypeIname.Contains(cardData.Param.iname);
          int num = 0;
          if (cardData != null && cardData.Param != null && cardData.Param.effects != null)
          {
            for (int index2 = 0; index2 < cardData.Param.effects.Length; ++index2)
            {
              if (cardData.Param.effects[index2].is_decrease_eff)
              {
                num = cardData.CurrentDecreaseEffectRate;
                break;
              }
            }
          }
          conceptCardIconParam.DecreaseEffectRate = num;
          conceptCardIconParam.IsEnableDecreaseEffect = num > 0 && !ConceptCardData.IsMainSlot(this.mSelectedCardSlotIndex);
          conceptCardIconParam.Select = this.mSelectedConceptCardData != null && cardData != null && (long) this.mSelectedConceptCardData.UniqueID == (long) cardData.UniqueID;
          conceptCardIconParam.IsEmpty = cardData == null;
          conceptCardIconParam.IsRecommend = !this.mSelectedUnit.UnitParam.IsNoRecommended() && MonoSingleton<GameManager>.Instance.MasterParam.FindUnitConceptCardConditions(this.mSelectedUnit, cardData) > 0;
          conceptCardIconParam.Initialize(source);
          this.mIconParams.Add(conceptCardIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mIconParams.ToArray());
      this.mContentController.Initialize(source, Vector2.zero);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCardEmptyMessageText, (UnityEngine.Object) null))
        return;
      ((Component) this.mCardEmptyMessageText).gameObject.SetActive(flag);
    }

    public void Refresh()
    {
      if (this.mCardDatas == null)
        return;
      this.Refresh(this.mCardDatas);
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
          {
            flag = true;
            break;
          }
        }
        ConceptCardListSortWindow.Sort(SortType, SortOrderType, this.mCardDatas);
        if (!flag)
          return;
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
    }

    public void SelectedUnitData(UnitData unit) => this.mSelectedUnit = unit;

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
    }

    public bool IsIgnoreEquipedConceptCard
    {
      get => PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED) != 0;
      set
      {
        int num = !value ? 0 : 1;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, num);
      }
    }

    private void SelectedCardIconActive()
    {
      if (this.mSelectedConceptCardData == null)
        return;
      ConceptCardIconParam conceptCardIconParam = this.mIconParams.Find((Predicate<ConceptCardIconParam>) (item => item != null && item.ConceptCard != null && (long) item.ConceptCard.UniqueID == (long) this.mSelectedConceptCardData.UniqueID));
      if (conceptCardIconParam == null)
        return;
      conceptCardIconParam.Enable = true;
    }

    private void ResetSelectCardAnimation()
    {
      for (int index = 0; index < this.mIconParams.Count; ++index)
        this.mIconParams[index].Enable = false;
    }

    public void SetSelectedCardData(ConceptCardData card_data)
    {
      if (this.mIconParams == null)
        return;
      foreach (ConceptCardIconParam mIconParam in this.mIconParams)
      {
        mIconParam.Select = mIconParam.ConceptCard == card_data;
        mIconParam.Refresh();
      }
    }
  }
}
