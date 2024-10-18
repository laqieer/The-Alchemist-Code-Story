// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "選択クリア", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "アイコン更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "複数選択の登録", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "複数選択を戻す", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "バックアップ作成", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "フィルタ適用", FlowNode.PinTypes.Input, 10)]
  public class ConceptCardList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private int MAX_MULTI_SELECT = 10;
    private List<GameObject> mCardIcons = new List<GameObject>();
    private MultiConceptCard mSortDataList = new MultiConceptCard();
    private MultiConceptCard mSelectedMaterials = new MultiConceptCard();
    private MultiConceptCard mBackupSelectedMaterials = new MultiConceptCard();
    private const string SAVE_TOGGLE_SELECT_SAME_CARD_KEY = "TOGGLE_SAME_CARD";
    public const int PIN_CLEAR = 1;
    public const int PIN_REFRESH = 2;
    public const int PIN_REGIST_MAT = 3;
    public const int PIN_REVERT_MAT = 4;
    public const int PIN_BACKUP_MAT = 5;
    public const int PIN_FILTER = 10;
    [SerializeField]
    private ConceptCardList.ListType mListType;
    [SerializeField]
    private ConceptCardList.ListIconCalc mListIconCalc;
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private GameObject EmptyMessage;
    [SerializeField]
    private Text PageIndex;
    [SerializeField]
    private Text PageIndexMax;
    [SerializeField]
    private Button ForwardButton;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Text SortTypeText;
    [SerializeField]
    private ImageArray FilterBgImages;
    [SerializeField]
    private Text CurrSelectedNum;
    [SerializeField]
    private Text MaxSelectedNum;
    [SerializeField]
    private Text TextSellZeny;
    [SerializeField]
    private Text TextMixCost;
    [SerializeField]
    private Text TextExp;
    [SerializeField]
    private Text TextTrust;
    [SerializeField]
    private GameObject TextWarningObject;
    [SerializeField]
    private Button[] SelectedInteractableButton;
    [SerializeField]
    private Text CurrentConceptCardNum;
    [SerializeField]
    private Text MaxConceptCardNum;
    [SerializeField]
    private Toggle mIgnoreSelectSameConceptCardToggle;
    private ConceptCardManager mCCManager;
    private int mPage;
    private int mMaxPages;
    private int mPageSize;

    private ConceptCardManager CCManager
    {
      get
      {
        if ((UnityEngine.Object) this.mCCManager == (UnityEngine.Object) null)
        {
          this.mCCManager = this.GetComponentInParent<ConceptCardManager>();
          if ((UnityEngine.Object) this.mCCManager == (UnityEngine.Object) null)
            DebugUtility.LogError("Not found ConceptCardManager.");
        }
        return this.mCCManager;
      }
    }

    private List<ConceptCardData> PlayerConceptCards
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.ConceptCards;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.ClearSelected();
          break;
        case 2:
          this.RefreshIconList(false);
          this.RefreshConceptCardNum();
          break;
        case 3:
          this.RegistMultiSelect();
          break;
        case 4:
          this.IncorporateMultiSelect();
          break;
        case 5:
          this.BackupMultiSelect();
          break;
        case 10:
          this.RefreshIconList(true);
          this.RefreshSortFilterObjects();
          break;
      }
    }

    public void Init()
    {
      if ((UnityEngine.Object) this.mCardObjectTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.mCardObjectParent == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle != (UnityEngine.Object) null)
      {
        this.mIgnoreSelectSameConceptCardToggle.isOn = this.GetToggleSameSelect();
        this.CCManager.ToggleSameSelectCard = this.mIgnoreSelectSameConceptCardToggle.isOn;
      }
      this.mCardObjectTemplate.SetActive(false);
      this.RefreshIconList(false);
      this.RefreshConceptCardNum();
      this.RefreshSortFilterObjects();
    }

    public int CellCount
    {
      get
      {
        int max = 64;
        GridLayoutGroup component = this.mCardObjectParent.GetComponent<GridLayoutGroup>();
        if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        {
          DebugUtility.LogError("Not found GridLayoutGroup.");
          return 0;
        }
        float x1 = component.cellSize.x;
        float y1 = component.cellSize.y;
        float x2 = component.spacing.x;
        float y2 = component.spacing.y;
        float horizontal = (float) component.padding.horizontal;
        float vertical = (float) component.padding.vertical;
        Rect rect = ((RectTransform) this.mCardObjectParent.transform).rect;
        float num1 = rect.width - horizontal + x2;
        float num2 = rect.height - vertical + y2;
        return Mathf.Clamp(Mathf.FloorToInt(num1 / (x1 + x2)) * Mathf.FloorToInt(num2 / (y1 + y2)), 0, max);
      }
    }

    private void ClearSelected()
    {
      this.mSelectedMaterials.Clear();
      this.RefreshIconList(false);
    }

    private void RegistMultiSelect()
    {
      this.CCManager.SelectedMaterials = this.mSelectedMaterials;
    }

    private void IncorporateMultiSelect()
    {
      this.mSelectedMaterials.Clone(this.mBackupSelectedMaterials);
    }

    private void BackupMultiSelect()
    {
      this.mBackupSelectedMaterials.Clone(this.CCManager.SelectedMaterials);
    }

    private void RefreshIconList(bool filter = false)
    {
      this.InstantiateIcons();
      if (this.mCardIcons.Count == 0)
        return;
      if ((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle != (UnityEngine.Object) null)
      {
        this.mIgnoreSelectSameConceptCardToggle.isOn = this.CCManager.ToggleSameSelectCard;
        this.SaveSameConceptCardToggle();
      }
      this.mSelectedMaterials = this.CCManager.SelectedMaterials;
      MultiConceptCard iconList = this.GetIconList();
      if (this.mListType == ConceptCardList.ListType.ENHANCE)
        iconList.Remove(this.CCManager.SelectedConceptCardData);
      if (filter)
      {
        List<long> longList = new List<long>();
        foreach (OLong id in this.mSelectedMaterials.GetIDList())
        {
          long uniqueID = (long) id;
          if (!iconList.Contains(uniqueID))
            longList.Add(uniqueID);
        }
        foreach (long uniqueID in longList)
          this.mSelectedMaterials.Remove(uniqueID);
      }
      if (this.mPageSize > 0)
      {
        this.mMaxPages = (iconList.Count + this.mPageSize - 1) / this.mPageSize;
        this.mPage = Mathf.Max(0, Mathf.Min(this.mPage, this.mMaxPages - 1));
      }
      this.RefreshIcons(iconList, this.mSelectedMaterials);
      this.RefreshPage();
      this.RefreshParameter();
      if ((UnityEngine.Object) this.EmptyMessage != (UnityEngine.Object) null)
        this.EmptyMessage.SetActive(0 >= iconList.Count);
      if (this.SelectedInteractableButton == null)
        return;
      bool flag = 0 < this.mSelectedMaterials.Count;
      foreach (Button button in this.SelectedInteractableButton)
      {
        if (!((UnityEngine.Object) button == (UnityEngine.Object) null))
          button.interactable = flag;
      }
    }

    private void InstantiateIcons()
    {
      this.mPageSize = this.mListIconCalc != ConceptCardList.ListIconCalc.AUTO ? this.MAX_MULTI_SELECT : this.CellCount;
      while (this.mCardIcons.Count < this.mPageSize)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mCardObjectTemplate);
        gameObject.transform.SetParent((Transform) this.mCardObjectParent, false);
        this.mCardIcons.Add(gameObject);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        }
      }
    }

    private void RefreshConceptCardNum()
    {
      if ((UnityEngine.Object) this.CurrentConceptCardNum != (UnityEngine.Object) null)
        this.CurrentConceptCardNum.text = MonoSingleton<GameManager>.Instance.Player.ConceptCardNum.ToString();
      if (!((UnityEngine.Object) this.MaxConceptCardNum != (UnityEngine.Object) null))
        return;
      this.MaxConceptCardNum.text = MonoSingleton<GameManager>.Instance.Player.ConceptCardCap.ToString();
    }

    private MultiConceptCard CurrMaterials()
    {
      List<ConceptCardData> card_list = new List<ConceptCardData>((IEnumerable<ConceptCardData>) this.PlayerConceptCards);
      if ((UnityEngine.Object) this.CCManager != (UnityEngine.Object) null)
      {
        this.Filter(card_list);
        ConceptCardListSortWindow.Sort(this.CCManager.SortType, this.CCManager.SortOrderType, card_list);
      }
      this.mSortDataList.SetArray(card_list.ToArray());
      return this.mSortDataList;
    }

    public void Filter(List<ConceptCardData> card_list)
    {
      if ((UnityEngine.Object) this.CCManager == (UnityEngine.Object) null || card_list.Count == 0)
        return;
      if ((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle != (UnityEngine.Object) null && this.CCManager.SelectedConceptCardData != null && this.mIgnoreSelectSameConceptCardToggle.isOn)
        card_list.RemoveAll((Predicate<ConceptCardData>) (x => !x.FilterEnhance(this.CCManager.SelectedConceptCardData.Param.iname)));
      else
        card_list.RemoveAll((Predicate<ConceptCardData>) (x => !x.Filter(this.CCManager.FilterType)));
    }

    private MultiConceptCard GetIconList()
    {
      if (this.mListType == ConceptCardList.ListType.MANAGER)
        return this.mSelectedMaterials;
      return this.CurrMaterials();
    }

    private void RefreshIcons(MultiConceptCard drawicons, MultiConceptCard materials)
    {
      if (drawicons == null)
        return;
      ConceptCardManager ccManager = this.CCManager;
      bool AcceptableExp = true;
      bool AcceptableTrust = true;
      bool flag = true;
      if (this.mListType == ConceptCardList.ListType.ENHANCE && ccManager.SelectedConceptCardData != null)
      {
        int mixTotalExp;
        int mixTrustExp;
        int mixTotalAwakeLv;
        ConceptCardManager.CalcTotalExpTrust(ccManager.SelectedConceptCardData, materials, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
        AcceptableExp = mixTotalExp < ccManager.SelectedConceptCardData.GetExpToLevelMax();
        AcceptableTrust = mixTrustExp < ccManager.SelectedConceptCardData.GetTrustToLevelMax();
        if (ccManager.SelectedConceptCardData.GetReward() == null)
          AcceptableTrust = false;
        if ((int) ccManager.SelectedConceptCardData.AwakeCount >= ccManager.SelectedConceptCardData.AwakeCountCap)
          flag = false;
      }
      for (int index1 = 0; index1 < this.mCardIcons.Count; ++index1)
      {
        int index2 = this.mPage * this.mPageSize + index1;
        this.mCardIcons[index1].SetActive(true);
        ConceptCardIcon component1 = this.mCardIcons[index1].GetComponent<ConceptCardIcon>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.Setup(drawicons.GetItem(index2));
        if (0 <= index2 && index2 < drawicons.Count)
        {
          ConceptCardIconMultiSelect component2 = this.mCardIcons[index1].GetComponent<ConceptCardIconMultiSelect>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            bool CanAwake = true;
            if (this.mListType == ConceptCardList.ListType.ENHANCE && ccManager.SelectedConceptCardData != null)
              CanAwake = flag && ccManager.SelectedConceptCardData.Param.iname == component2.ConceptCard.Param.iname;
            this.RefreshSelectParam(component2, materials);
            this.RefreshEnableParam(component2, materials, AcceptableExp, AcceptableTrust, CanAwake);
          }
        }
      }
    }

    private void RefreshPage()
    {
      if ((UnityEngine.Object) this.PageIndex != (UnityEngine.Object) null)
        this.PageIndex.text = Mathf.Min(this.mPage + 1, this.mMaxPages).ToString();
      if ((UnityEngine.Object) this.PageIndexMax != (UnityEngine.Object) null)
        this.PageIndexMax.text = this.mMaxPages.ToString();
      if ((UnityEngine.Object) this.ForwardButton != (UnityEngine.Object) null)
        this.ForwardButton.interactable = this.mPage < this.mMaxPages - 1;
      if (!((UnityEngine.Object) this.PrevButton != (UnityEngine.Object) null))
        return;
      this.PrevButton.interactable = this.mPage > 0;
    }

    public void GotoPreviousPage()
    {
      if (this.mPage <= 0)
        return;
      --this.mPage;
      this.RefreshIconList(false);
    }

    public void GotoNextPage()
    {
      if (this.mPage >= this.mMaxPages - 1)
        return;
      ++this.mPage;
      this.RefreshIconList(false);
    }

    private void RefreshParameter()
    {
      this.RefreshSelected();
      this.RefreshTextSellZeny();
      this.RefreshTextMixCost();
      this.RefreshTextExpAndTrust();
      this.RefreshTextWarning();
    }

    private void RefreshSelected()
    {
      if ((UnityEngine.Object) this.CurrSelectedNum == (UnityEngine.Object) null)
        return;
      this.CurrSelectedNum.text = this.mSelectedMaterials.Count.ToString();
      if ((UnityEngine.Object) this.MaxSelectedNum == (UnityEngine.Object) null)
        return;
      this.MaxSelectedNum.text = this.MAX_MULTI_SELECT.ToString();
    }

    private void RefreshTextSellZeny()
    {
      if ((UnityEngine.Object) this.TextSellZeny == (UnityEngine.Object) null)
        return;
      int totalSellZeny = 0;
      ConceptCardManager.GalcTotalSellZeny(this.mSelectedMaterials, out totalSellZeny);
      this.TextSellZeny.text = totalSellZeny.ToString();
    }

    private void RefreshTextMixCost()
    {
      if ((UnityEngine.Object) this.TextMixCost == (UnityEngine.Object) null)
        return;
      int totalMixZeny = 0;
      ConceptCardManager.GalcTotalMixZeny(this.mSelectedMaterials, out totalMixZeny);
      this.TextMixCost.text = totalMixZeny.ToString();
    }

    private void RefreshTextExpAndTrust()
    {
      int mixTotalExp;
      int mixTrustExp;
      int mixTotalAwakeLv;
      ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, this.mSelectedMaterials, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
      if ((UnityEngine.Object) this.TextExp != (UnityEngine.Object) null)
        this.TextExp.text = mixTotalExp.ToString();
      if (!((UnityEngine.Object) this.TextTrust != (UnityEngine.Object) null))
        return;
      this.TextTrust.text = ConceptCardManager.ParseTrustFormat(this.CCManager.SelectedConceptCardData, mixTrustExp);
    }

    private void RefreshTextWarning()
    {
      if ((UnityEngine.Object) this.TextWarningObject == (UnityEngine.Object) null)
        return;
      this.TextWarningObject.SetActive(false);
      Text componentInChildren = this.TextWarningObject.GetComponentInChildren<Text>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
        return;
      componentInChildren.text = string.Empty;
      string warningTextByMaterials = ConceptCardManager.GetWarningTextByMaterials(this.mSelectedMaterials);
      if (string.IsNullOrEmpty(warningTextByMaterials))
        return;
      componentInChildren.text = warningTextByMaterials;
      this.TextWarningObject.SetActive(true);
    }

    private void RefreshEnableParam(ConceptCardIconMultiSelect drawicons, MultiConceptCard materials, bool AcceptableExp, bool AcceptableTrust, bool CanAwake)
    {
      if ((UnityEngine.Object) drawicons == (UnityEngine.Object) null)
        return;
      bool enable = true;
      if (!materials.IsSelected(drawicons.ConceptCard))
      {
        UnitData unitData = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u =>
        {
          if (u.ConceptCard != null)
            return (long) u.ConceptCard.UniqueID == (long) drawicons.ConceptCard.UniqueID;
          return false;
        }));
        if (materials.Count >= this.MAX_MULTI_SELECT || drawicons.ConceptCard.Favorite || unitData != null)
          enable = false;
        else if (this.CCManager.SelectedConceptCardData != null)
        {
          MultiConceptCard materials1 = new MultiConceptCard();
          materials1.Add(drawicons.ConceptCard);
          int mixTotalExp;
          int mixTrustExp;
          int mixTotalAwakeLv;
          ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, materials1, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
          if ((0 >= mixTotalExp || !AcceptableExp) && (0 >= mixTrustExp || !AcceptableTrust) && !CanAwake)
            enable = false;
        }
      }
      if (this.mListType == ConceptCardList.ListType.SELL && drawicons.ConceptCard.Param.not_sale)
      {
        drawicons.SetNotSellFlag(true);
        enable = false;
      }
      else
        drawicons.SetNotSellFlag(false);
      drawicons.RefreshEnableParam(enable);
    }

    private void RefreshSelectParam(ConceptCardIconMultiSelect drawicons, MultiConceptCard materials)
    {
      if ((UnityEngine.Object) drawicons == (UnityEngine.Object) null)
        return;
      bool selected = materials.IsSelected(drawicons.ConceptCard);
      drawicons.RefreshSelectParam(selected);
    }

    private void RefreshSortFilterObjects()
    {
      if ((UnityEngine.Object) this.SortTypeText != (UnityEngine.Object) null)
        this.SortTypeText.text = LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.CCManager.SortType));
      if (!((UnityEngine.Object) this.FilterBgImages != (UnityEngine.Object) null))
        return;
      this.FilterBgImages.ImageIndex = this.CCManager.FilterType != (ConceptCardListFilterWindow.Type.RARITY_1 | ConceptCardListFilterWindow.Type.RARITY_2 | ConceptCardListFilterWindow.Type.RARITY_3 | ConceptCardListFilterWindow.Type.RARITY_4 | ConceptCardListFilterWindow.Type.RARITY_5) ? 1 : 0;
    }

    private void OnItemDetail(GameObject go)
    {
      ConceptCardIcon component = go.GetComponent<ConceptCardIcon>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      ConceptCardData conceptCard = component.ConceptCard;
      if (conceptCard == null)
        return;
      this.CCManager.SelectedConceptCardData = conceptCard;
    }

    private void OnItemSelect(GameObject go)
    {
      ConceptCardIcon component = go.GetComponent<ConceptCardIcon>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      ConceptCardData conceptCard = component.ConceptCard;
      if (conceptCard == null || !this.mSelectedMaterials.IsSelected(conceptCard) && this.mSelectedMaterials.Count >= this.MAX_MULTI_SELECT)
        return;
      this.mSelectedMaterials.Flip(conceptCard);
      this.RefreshSelectParam(go.GetComponent<ConceptCardIconMultiSelect>(), this.mSelectedMaterials);
      this.RefreshIconList(false);
    }

    private bool GetToggleSameSelect()
    {
      if (!PlayerPrefsUtility.HasKey("TOGGLE_SAME_CARD"))
        return false;
      string str = PlayerPrefsUtility.GetString("TOGGLE_SAME_CARD", string.Empty);
      if (string.IsNullOrEmpty(str))
        return false;
      bool result = false;
      if (!bool.TryParse(str, out result))
        return false;
      return result;
    }

    public void OnChangeIgnoreSameCardToggle(bool isOn)
    {
      if ((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle == (UnityEngine.Object) null)
        return;
      this.CCManager.ToggleSameSelectCard = isOn;
      this.RefreshIconList(true);
      this.RefreshSortFilterObjects();
    }

    private void SaveSameConceptCardToggle()
    {
      if (string.IsNullOrEmpty("TOGGLE_SAME_CARD") || (UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle == (UnityEngine.Object) null)
        return;
      PlayerPrefsUtility.SetString("TOGGLE_SAME_CARD", this.mIgnoreSelectSameConceptCardToggle.isOn.ToString(), true);
    }

    public enum ListType
    {
      NORMAL,
      ENHANCE,
      SELL,
      MANAGER,
    }

    public enum ListIconCalc
    {
      AUTO,
      SEPCIFY,
    }
  }
}
