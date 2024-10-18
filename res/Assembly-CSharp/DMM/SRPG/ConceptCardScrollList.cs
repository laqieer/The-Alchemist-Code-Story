// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardScrollList
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
  [FlowNode.Pin(1, "選択クリア", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "アイコン更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "複数選択の登録", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "複数選択を戻す", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "バックアップ作成", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "フィルタ適用", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(6, "アイコン選択", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "複数選択を反映", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(11, "一括選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(51, "選択した", FlowNode.PinTypes.Output, 51)]
  [FlowNode.Pin(61, "複数選択を反映した", FlowNode.PinTypes.Output, 61)]
  public class ConceptCardScrollList : MonoBehaviour, IFlowInterface
  {
    private const string SAVE_TOGGLE_SELECT_SAME_CARD_KEY = "TOGGLE_SAME_CARD";
    private const string SAVE_TOGGLE_SELECT_FAVARITE_CARD_KEY = "TOGGLE_FAVARITE_CARD";
    public const int PIN_CLEAR = 1;
    public const int PIN_REFRESH = 2;
    public const int PIN_REGIST_MAT = 3;
    public const int PIN_REVERT_MAT = 4;
    public const int PIN_BACKUP_MAT = 5;
    public const int PIN_SELECT_ICON = 6;
    public const int PIN_SELECT_APPLY = 7;
    public const int PIN_FILTER = 10;
    public const int PIN_SELECT_MAX = 11;
    public const int PIN_OUTPUT = 51;
    public const int PIN_OUTPUT_SELECT_APPLY = 61;
    [SerializeField]
    private int MAX_MULTI_SELECT = 10;
    [SerializeField]
    private GameObject EmptyMessage;
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
    private Text TextSellCoin;
    [SerializeField]
    private Text TextMixCost;
    [SerializeField]
    private Text TextExp;
    [SerializeField]
    private Text TextTrust;
    [SerializeField]
    private Button[] SelectedInteractableButton;
    [SerializeField]
    private Button SelectMaxButton;
    [SerializeField]
    private Text CurrentConceptCardNum;
    [SerializeField]
    private Text MaxConceptCardNum;
    [SerializeField]
    private Toggle mIgnoreSelectSameConceptCardToggle;
    [SerializeField]
    private Toggle mIgnoreFavariteConceptCardToggle;
    [SerializeField]
    private ContentController mContentController;
    [SerializeField]
    private bool FavoriteCardSelectable = true;
    [SerializeField]
    private bool HasOwnerCardSelectable = true;
    private List<ConceptCardIconParam> mParams = new List<ConceptCardIconParam>();
    public ConceptCardIconParam mSelectedConceptCardIconParam;
    private MultiConceptCard mSelectedConceptCards = new MultiConceptCard();
    private MultiConceptCard mBackupSelectedMaterials = new MultiConceptCard();
    private Vector2 mAnchorPosition = Vector2.zero;
    private ConceptCardManager mCCManager;

    private ConceptCardManager CCManager
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCCManager, (UnityEngine.Object) null))
        {
          this.mCCManager = ((Component) this).GetComponentInParent<ConceptCardManager>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCCManager, (UnityEngine.Object) null))
            DebugUtility.LogError("Not found ConceptCardManager.");
        }
        return this.mCCManager;
      }
    }

    private void OnItemDetail(GameObject go)
    {
    }

    private void OnItemSelect(GameObject go)
    {
    }

    public List<ConceptCardData> Filter(List<ConceptCardData> card_list)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CCManager, (UnityEngine.Object) null) || card_list.Count == 0)
        return new List<ConceptCardData>();
      List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>((IEnumerable<ConceptCardData>) card_list);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle, (UnityEngine.Object) null) && this.CCManager.SelectedConceptCardData != null)
      {
        if (this.mIgnoreSelectSameConceptCardToggle.isOn)
          conceptCardDataList.RemoveAll((Predicate<ConceptCardData>) (x => !x.FilterEnhance(this.CCManager.SelectedConceptCardData.Param.iname)));
        if (conceptCardDataList.Contains(this.CCManager.SelectedConceptCardData))
          conceptCardDataList.Remove(this.CCManager.SelectedConceptCardData);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIgnoreFavariteConceptCardToggle, (UnityEngine.Object) null) && this.mIgnoreFavariteConceptCardToggle.isOn)
        conceptCardDataList.RemoveAll((Predicate<ConceptCardData>) (x => x.Favorite));
      conceptCardDataList.RemoveAll((Predicate<ConceptCardData>) (x => !x.Filter(this.CCManager.FilterPrefs)));
      return conceptCardDataList;
    }

    public void Init(List<ConceptCardData> card_datas)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle, (UnityEngine.Object) null))
      {
        this.mIgnoreSelectSameConceptCardToggle.isOn = this.GetToggleSameSelect();
        this.CCManager.ToggleSameSelectCard = this.mIgnoreSelectSameConceptCardToggle.isOn;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIgnoreFavariteConceptCardToggle, (UnityEngine.Object) null))
        this.mIgnoreFavariteConceptCardToggle.isOn = this.GetToggleFavarite();
      this.mSelectedConceptCards.Clear();
      this.Refresh(card_datas, true);
      this.RefreshParameter();
    }

    private void RefreshSortFilterObjects(bool reset_position)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortTypeText, (UnityEngine.Object) null))
        this.SortTypeText.text = LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.CCManager.SortType));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FilterBgImages, (UnityEngine.Object) null))
        this.FilterBgImages.ImageIndex = !FilterConceptCardPrefs.Load().IsDisableFilterAll() ? 1 : 0;
      if (!reset_position)
        return;
      this.mAnchorPosition = Vector2.zero;
    }

    public void Refresh(List<ConceptCardData> card_datas, bool reset_position = false)
    {
      this.mAnchorPosition = this.mContentController.GetAnchorePos();
      this.mContentController.Release();
      ContentSource source = new ContentSource();
      this.mParams.Clear();
      this.RefreshSortFilterObjects(reset_position);
      card_datas = this.Filter(card_datas);
      ConceptCardListSortWindow.Sort(this.CCManager.SortType, this.CCManager.SortOrderType, card_datas);
      if (this.mSelectedConceptCards.Count > 0)
      {
        List<long> uniqueIds = this.mSelectedConceptCards.GetUniqueIDs();
        List<long> longList = new List<long>();
        foreach (long num in uniqueIds)
        {
          long selected_id = num;
          if (card_datas.FindIndex((Predicate<ConceptCardData>) (item => (long) item.UniqueID == selected_id)) < 0)
            longList.Add(selected_id);
        }
        foreach (long uniqueID in longList)
          this.mSelectedConceptCards.Remove(uniqueID);
      }
      bool flag = true;
      if (card_datas != null)
      {
        for (int index = 0; index < card_datas.Count; ++index)
        {
          ConceptCardData cardData = card_datas[index];
          if (cardData != null)
            flag = false;
          ConceptCardIconParam conceptCardIconParam = new ConceptCardIconParam();
          conceptCardIconParam.ConceptCard = cardData;
          conceptCardIconParam.Enable = true;
          conceptCardIconParam.Select = false;
          conceptCardIconParam.Initialize(source);
          this.mParams.Add(conceptCardIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mParams.ToArray());
      this.mContentController.Initialize(source, this.mAnchorPosition);
      this.mContentController.ForceUpdate();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null))
        this.EmptyMessage.SetActive(flag);
      this.RefreshSelectList();
      this.RefreshConceptCardNum();
    }

    private void RefreshConceptCardNum()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentConceptCardNum, (UnityEngine.Object) null))
        this.CurrentConceptCardNum.text = MonoSingleton<GameManager>.Instance.Player.ConceptCardNum.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxConceptCardNum, (UnityEngine.Object) null))
        return;
      this.MaxConceptCardNum.text = MonoSingleton<GameManager>.Instance.Player.ConceptCardCap.ToString();
    }

    private void RefreshSelectNum()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxSelectedNum, (UnityEngine.Object) null))
        this.MaxSelectedNum.text = this.MAX_MULTI_SELECT.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrSelectedNum, (UnityEngine.Object) null))
        return;
      this.CurrSelectedNum.text = this.mSelectedConceptCards.Count.ToString();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mSelectedConceptCards.Clear();
          this.RefreshSelectList();
          this.RefreshParameter();
          break;
        case 2:
          this.Refresh(MonoSingleton<GameManager>.Instance.Player.ConceptCards);
          this.RefreshSelectList();
          this.RefreshParameter();
          break;
        case 3:
          if (this.mSelectedConceptCardIconParam == null)
            break;
          if (this.mSelectedConceptCards.Contains((long) this.mSelectedConceptCardIconParam.ConceptCard.UniqueID))
          {
            this.mSelectedConceptCards.Remove(this.mSelectedConceptCardIconParam.ConceptCard);
          }
          else
          {
            if (this.mSelectedConceptCards.Count >= this.MAX_MULTI_SELECT)
              break;
            if (this.CCManager.IsSellList(((Component) this).gameObject))
            {
              int totalSellZeny = 0;
              ConceptCardManager.GalcTotalSellZeny(this.mSelectedConceptCards, out totalSellZeny);
              if (int.MaxValue - (MonoSingleton<GameManager>.Instance.Player.Gold + totalSellZeny) < this.mSelectedConceptCardIconParam.ConceptCard.Param.sell)
              {
                UIUtility.SystemMessage(LocalizedText.Get("sys.SELECT_ERROR_ZENNY_LIMIT_OVER"), (UIUtility.DialogResultEvent) null);
                break;
              }
            }
            this.mSelectedConceptCards.Add(this.mSelectedConceptCardIconParam.ConceptCard);
          }
          this.RefreshSelectList();
          this.RefreshParameter();
          this.mSelectedConceptCardIconParam = (ConceptCardIconParam) null;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
          break;
        case 4:
          this.IncorporateMultiSelect();
          break;
        case 5:
          this.BackupMultiSelect();
          break;
        case 6:
          if (this.mSelectedConceptCardIconParam == null)
            break;
          this.CCManager.SelectedConceptCardData = this.mSelectedConceptCardIconParam.ConceptCard;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
          break;
        case 7:
          if (this.mSelectedConceptCards.Count <= 0)
            break;
          this.CCManager.SelectedMaterials = this.mSelectedConceptCards;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 61);
          break;
        case 10:
          this.Refresh(MonoSingleton<GameManager>.Instance.Player.ConceptCards, true);
          this.RefreshSelectList();
          this.RefreshParameter();
          break;
        case 11:
          this.SetAllSelect();
          break;
      }
    }

    public void RefreshSelectList()
    {
      int count = this.mSelectedConceptCards.Count;
      bool flag1 = false;
      if (this.CCManager.SelectedConceptCardData != null && this.CCManager.IsEnhanceList(((Component) this).gameObject))
      {
        bool flag2 = true;
        int mixTotalExp;
        int mixTrustExp;
        ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, this.mSelectedConceptCards, out mixTotalExp, out mixTrustExp, out int _);
        bool flag3 = mixTotalExp < this.CCManager.SelectedConceptCardData.GetExpToLevelMax();
        bool flag4 = mixTrustExp < this.CCManager.SelectedConceptCardData.GetTrustToLevelMax();
        if (this.CCManager.SelectedConceptCardData.GetReward() == null)
          flag4 = false;
        if ((int) this.CCManager.SelectedConceptCardData.AwakeCount >= this.CCManager.SelectedConceptCardData.AwakeCountCap)
          flag2 = false;
        flag1 = !flag3 && !flag4 && !flag2;
      }
      foreach (ConceptCardIconParam mParam in this.mParams)
      {
        if (!this.mSelectedConceptCards.Contains((long) mParam.ConceptCard.UniqueID))
        {
          mParam.Enable = !flag1 && count < this.MAX_MULTI_SELECT;
          if (mParam.Enable && (!this.FavoriteCardSelectable || !this.HasOwnerCardSelectable))
            mParam.Enable = !this.FavoriteCardSelectable && !mParam.ConceptCard.Favorite && !this.HasOwnerCardSelectable && mParam.ConceptCard.GetOwner(true) == null;
          mParam.Select = false;
        }
        else
          mParam.Select = true;
        mParam.Refresh();
      }
    }

    private void IncorporateMultiSelect()
    {
      this.mSelectedConceptCards.Clone(this.mBackupSelectedMaterials);
    }

    private void BackupMultiSelect()
    {
      this.mBackupSelectedMaterials.Clone(this.CCManager.SelectedMaterials);
    }

    private void RefreshSelected()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CurrSelectedNum, (UnityEngine.Object) null))
        return;
      this.CurrSelectedNum.text = this.mSelectedConceptCards.Count.ToString();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MaxSelectedNum, (UnityEngine.Object) null))
        return;
      this.MaxSelectedNum.text = this.MAX_MULTI_SELECT.ToString();
    }

    private void RefreshTextSellZeny()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TextSellZeny, (UnityEngine.Object) null))
        return;
      int totalSellZeny = 0;
      ConceptCardManager.GalcTotalSellZeny(this.mSelectedConceptCards, out totalSellZeny);
      this.TextSellZeny.text = totalSellZeny.ToString();
    }

    private void RefreshTextSellCoin()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TextSellCoin, (UnityEngine.Object) null))
        return;
      int totalSellCoin = 0;
      ConceptCardManager.CalcTotalSellCoin(this.mSelectedConceptCards, out totalSellCoin);
      this.TextSellCoin.text = totalSellCoin.ToString();
    }

    private void RefreshTextMixCost()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TextMixCost, (UnityEngine.Object) null))
        return;
      int totalMixZeny = 0;
      ConceptCardManager.GalcTotalMixZeny(this.mSelectedConceptCards, out totalMixZeny);
      this.TextMixCost.text = totalMixZeny.ToString();
    }

    private void RefreshTextExpAndTrust()
    {
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, this.mSelectedConceptCards, out mixTotalExp, out mixTrustExp, out int _);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextExp, (UnityEngine.Object) null))
        this.TextExp.text = mixTotalExp.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextTrust, (UnityEngine.Object) null))
        return;
      this.TextTrust.text = ConceptCardManager.ParseTrustFormat(this.CCManager.SelectedConceptCardData, mixTrustExp);
    }

    private void RefreshParameter()
    {
      this.RefreshSelected();
      this.RefreshTextSellZeny();
      this.RefreshTextSellCoin();
      this.RefreshTextMixCost();
      this.RefreshTextExpAndTrust();
      this.RefreshActiveButtons();
    }

    private void RefreshActiveButtons()
    {
      if (this.SelectedInteractableButton != null)
      {
        bool flag = 0 < this.mSelectedConceptCards.Count;
        foreach (Button button in this.SelectedInteractableButton)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null))
            ((Selectable) button).interactable = flag;
        }
      }
      this.SetInteractableSelectMaxButton();
    }

    private bool GetToggleSameSelect()
    {
      if (!PlayerPrefsUtility.HasKey("TOGGLE_SAME_CARD"))
        return false;
      string str = PlayerPrefsUtility.GetString("TOGGLE_SAME_CARD", string.Empty);
      if (string.IsNullOrEmpty(str))
        return false;
      bool result = false;
      return bool.TryParse(str, out result) && result;
    }

    public void OnChangeIgnoreSameCardToggle(bool isOn)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle, (UnityEngine.Object) null))
        return;
      this.CCManager.ToggleSameSelectCard = isOn;
      this.SaveSameConceptCardToggle();
      this.Refresh(MonoSingleton<GameManager>.Instance.Player.ConceptCards);
    }

    private void SaveSameConceptCardToggle()
    {
      if (string.IsNullOrEmpty("TOGGLE_SAME_CARD") || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mIgnoreSelectSameConceptCardToggle, (UnityEngine.Object) null))
        return;
      PlayerPrefsUtility.SetString("TOGGLE_SAME_CARD", this.mIgnoreSelectSameConceptCardToggle.isOn.ToString(), true);
    }

    private bool GetToggleFavarite()
    {
      if (!PlayerPrefsUtility.HasKey("TOGGLE_FAVARITE_CARD"))
        return false;
      string str = PlayerPrefsUtility.GetString("TOGGLE_FAVARITE_CARD", string.Empty);
      if (string.IsNullOrEmpty(str))
        return false;
      bool result = false;
      return bool.TryParse(str, out result) && result;
    }

    public void OnChangeIgnoreFavariteCardToggle()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mIgnoreFavariteConceptCardToggle, (UnityEngine.Object) null))
        return;
      this.SaveFavariteConceptCardToggle();
      this.Refresh(MonoSingleton<GameManager>.Instance.Player.ConceptCards);
    }

    private void SaveFavariteConceptCardToggle()
    {
      if (string.IsNullOrEmpty("TOGGLE_FAVARITE_CARD") || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mIgnoreFavariteConceptCardToggle, (UnityEngine.Object) null))
        return;
      PlayerPrefsUtility.SetString("TOGGLE_FAVARITE_CARD", this.mIgnoreFavariteConceptCardToggle.isOn.ToString(), true);
    }

    public void ButtonEventOnClickNode(ConceptCardIconNode node)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null))
        return;
      this.mSelectedConceptCardIconParam = node.GetParam() as ConceptCardIconParam;
    }

    private void SetAllSelect()
    {
      if (this.CCManager.IsSellList(((Component) this).gameObject))
      {
        int totalSellZeny = 0;
        ConceptCardManager.GalcTotalSellZeny(this.mSelectedConceptCards, out totalSellZeny);
        int num = int.MaxValue - (MonoSingleton<GameManager>.Instance.Player.Gold + totalSellZeny);
        foreach (ConceptCardIconParam mParam in this.mParams)
        {
          if (!this.mSelectedConceptCards.Contains((long) mParam.ConceptCard.UniqueID) && !mParam.ConceptCard.Favorite && mParam.ConceptCard.GetOwner(true) == null && num - mParam.ConceptCard.Param.sell >= 0)
          {
            num -= mParam.ConceptCard.Param.sell;
            this.mSelectedConceptCards.Add(mParam.ConceptCard);
            if (this.mSelectedConceptCards.Count >= this.MAX_MULTI_SELECT)
              break;
          }
        }
      }
      this.RefreshSelectList();
      this.RefreshParameter();
      this.SetInteractableSelectMaxButton();
    }

    private void SetInteractableSelectMaxButton()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectMaxButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.SelectMaxButton).interactable = false;
      if (!this.CCManager.IsSellList(((Component) this).gameObject))
        return;
      int totalSellZeny = 0;
      ConceptCardManager.GalcTotalSellZeny(this.mSelectedConceptCards, out totalSellZeny);
      int num = int.MaxValue - (MonoSingleton<GameManager>.Instance.Player.Gold + totalSellZeny);
      if (this.mSelectedConceptCards.Count >= this.MAX_MULTI_SELECT)
        return;
      foreach (ConceptCardIconParam mParam in this.mParams)
      {
        if (!this.mSelectedConceptCards.Contains((long) mParam.ConceptCard.UniqueID))
        {
          if (!mParam.ConceptCard.Favorite && mParam.ConceptCard.GetOwner(true) == null && num - mParam.ConceptCard.Param.sell >= 0)
          {
            ((Selectable) this.SelectMaxButton).interactable = true;
            break;
          }
        }
        else if (this.mSelectedConceptCards.Count >= this.MAX_MULTI_SELECT)
          break;
      }
    }
  }
}
