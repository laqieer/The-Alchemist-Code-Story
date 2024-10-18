// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipWindow
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
  [FlowNode.Pin(10, "次のページ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "前のページ", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "念装アイコン選択", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "OKボタン", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(50, "外すボタン", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "真理念装データ取得完了", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(70, "ソート", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(75, "フィルター", FlowNode.PinTypes.Input, 75)]
  [FlowNode.Pin(80, "念装詳細設定", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(1000, "装備変更", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "真理念装データ取得開始", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "ウィンドウ閉じる", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1030, "念装詳細設定完了", FlowNode.PinTypes.Output, 1030)]
  public class ConceptCardEquipWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_PAGE_NEXT = 10;
    public const int PIN_PAGE_BACK = 20;
    public const int PIN_CARD_SELECT_ICON = 30;
    public const int PIN_CLICK_OK = 40;
    public const int PIN_CLICK_RELEASE = 50;
    public const int PIN_END_REQUEST_CARD_DATAS = 60;
    public const int PIN_SORT_DATAS = 70;
    public const int PIN_FILTER_DATAS = 75;
    public const int PIN_SET_DETAIL_DATA = 80;
    public const int PIN_CHANGE_EQUIP = 1000;
    public const int PIN_START_REQUEST_CARD_DATAS = 1010;
    public const int PIN_CLOSE_WINDOW = 1020;
    public const int PIN_SET_DETAIL_DATA_END = 1030;
    private static ConceptCardEquipWindow _instance;
    [SerializeField]
    private Toggle mIgnoreEquipedConceptCardToggle;
    [SerializeField]
    private ConceptCardIcon mIcon;
    [SerializeField]
    private Text mName;
    [SerializeField]
    private Text mLvText;
    [SerializeField]
    private Text mLvMaxText;
    [SerializeField]
    private Text mExpText;
    [SerializeField]
    private Slider mExpSlider;
    [SerializeField]
    private StatusList mStatus;
    [SerializeField]
    private Text SortText;
    [SerializeField]
    private GameObject mParamViewObject;
    [SerializeField]
    private GameObject mEmptyParamViewObject;
    [SerializeField]
    private GameObject mTrustObject;
    [SerializeField]
    private Text mTrustText;
    [SerializeField]
    private ImageArray FilterBgImages;
    [SerializeField]
    private ConceptCardEquipScrollList mConceptCardEquipScrollList;
    [SerializeField]
    private Button ButtonAutoEquip;
    [SerializeField]
    private GameObject DecreaseEffectObject;
    [SerializeField]
    private Text DecreaseEffectText;
    [SerializeField]
    private GameObject SubslotBadge;
    private BaseStatus mBaseAdd = new BaseStatus();
    private BaseStatus mBaseMul = new BaseStatus();
    private BaseStatus mUnitAdd = new BaseStatus();
    private BaseStatus mUnitMul = new BaseStatus();
    private bool IsInitalized;
    private FlowNode_ReqConceptCardSet mFlownodeReqConceptCardSet;
    private ConceptCardData mSelectedCardData;
    private UnitData mSelectedUnit;
    private int mSelectedCardIndex;
    private List<string> mOtherSlotCardInames;
    public ConceptCardListSortWindow.Type SortType;
    public ConceptCardListSortWindow.Type SortOrderType;
    public Action OnCancelAction;
    public Action OnChangeAction;

    public static ConceptCardEquipWindow Instance => ConceptCardEquipWindow._instance;

    public UnitData SelectedUnit => this.mSelectedUnit;

    public int SelectedCardIndex => this.mSelectedCardIndex;

    private void Awake() => ConceptCardEquipWindow._instance = this;

    private void Start()
    {
      this.SortType = ConceptCardListSortWindow.LoadDataType();
      this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
      this.SetSortName();
    }

    private void OnDestroy() => ConceptCardEquipWindow._instance = (ConceptCardEquipWindow) null;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.PageNext();
          break;
        case 20:
          this.PageBack();
          break;
        case 30:
          this.SelectCardIcon();
          break;
        case 40:
          this.ClickOKButton();
          break;
        case 50:
          this.ClickReleaseButton();
          break;
        case 60:
          this.EndRequestConceptCardDatas();
          break;
        case 70:
          this.mConceptCardEquipScrollList.Sort(this.SortType, this.SortOrderType);
          this.mConceptCardEquipScrollList.Refresh();
          this.SetSortName();
          this.RefreshSortFilterObjects();
          break;
        case 75:
          this.mConceptCardEquipScrollList.Filter();
          this.mConceptCardEquipScrollList.Refresh();
          this.RefreshSortFilterObjects();
          break;
        case 80:
          GlobalVars.SelectedConceptCardData.Set(this.mSelectedCardData);
          ConceptCardEquipDetail.SetSelectedUnitData(this.mSelectedUnit, this.mSelectedCardIndex);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1030);
          break;
      }
    }

    private void Update()
    {
      if (!this.IsInitalized)
        return;
      this.RefreshSortFilterObjects();
    }

    public void Init(UnitData unit, int selectedCardIndex)
    {
      this.mSelectedUnit = unit;
      this.mSelectedCardIndex = selectedCardIndex;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConceptCardEquipScrollList, (UnityEngine.Object) null))
        this.mConceptCardEquipScrollList.SelectedUnitData(this.mSelectedUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConceptCardEquipScrollList, (UnityEngine.Object) null))
        this.mIgnoreEquipedConceptCardToggle.isOn = this.mConceptCardEquipScrollList.IsIgnoreEquipedConceptCard;
      this.mOtherSlotCardInames = new List<string>();
      for (int index = 0; index < unit.ConceptCards.Length; ++index)
      {
        if (index != selectedCardIndex && unit.ConceptCards[index] != null)
          this.mOtherSlotCardInames.Add(unit.ConceptCards[index].Param.iname);
      }
      GameUtility.SetGameObjectActive(this.SubslotBadge, !ConceptCardData.IsMainSlot(selectedCardIndex));
      this.mFlownodeReqConceptCardSet = ((Component) this).GetComponent<FlowNode_ReqConceptCardSet>();
      this.mSelectedCardData = this.mSelectedUnit.GetConceptCardByIndex(selectedCardIndex);
      this.SetParam(this.mSelectedCardData, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
      this.SetActiveParamViewObject(this.mSelectedCardData == null);
      this.StartRequestConceptCardDatas();
      this.RefreshSortFilterObjects();
    }

    public void PageNext()
    {
    }

    public void PageBack()
    {
    }

    private void RefreshPageText()
    {
    }

    private void RefreshSortFilterObjects()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FilterBgImages, (UnityEngine.Object) null))
        return;
      this.FilterBgImages.ImageIndex = !FilterConceptCardPrefs.Load().IsDisableFilterAll() ? 1 : 0;
    }

    public void RefreshSelectedCard(ConceptCardIcon selecIcon)
    {
      this.SetActiveParamViewObject(UnityEngine.Object.op_Equality((UnityEngine.Object) selecIcon, (UnityEngine.Object) null));
      this.mSelectedCardData = !UnityEngine.Object.op_Inequality((UnityEngine.Object) selecIcon, (UnityEngine.Object) null) ? (ConceptCardData) null : selecIcon.ConceptCard;
      this.mConceptCardEquipScrollList.SetSelectedCardData(this.mSelectedCardData);
      if (this.mSelectedCardData == null || this.mSelectedUnit == null)
        return;
      this.SetParam(this.mSelectedCardData, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
    }

    private void SelectCardIcon()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      this.RefreshSelectedCard(currentValue.GetComponent<ConceptCardIcon>("_self"));
    }

    private void SetParam(ConceptCardData card_data, UnitData unit, int job_index)
    {
      if (card_data == null || unit == null || unit.Jobs.Length <= job_index)
        return;
      JobData job = unit.Jobs[job_index];
      this.mName.text = card_data.Param.name;
      this.mLvText.text = card_data.Lv.ToString();
      this.mLvMaxText.text = card_data.CurrentLvCap.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTrustObject, (UnityEngine.Object) null))
        this.mTrustObject.SetActive(card_data.GetReward() != null);
      ConceptCardManager.SubstituteTrustFormat(card_data, this.mTrustText, (int) card_data.Trust);
      int num1 = 0;
      float num2 = 1f;
      if ((int) card_data.Lv < (int) card_data.CurrentLvCap)
      {
        int conceptCardLevelExp1 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) card_data.Rarity, (int) card_data.Lv);
        int conceptCardLevelExp2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) card_data.Rarity, (int) card_data.Lv + 1);
        num2 = (float) ((int) card_data.Exp - conceptCardLevelExp1) / (float) (conceptCardLevelExp2 - conceptCardLevelExp1);
        num1 = conceptCardLevelExp2 - (int) card_data.Exp;
      }
      this.mExpText.text = num1.ToString();
      this.mExpSlider.value = num2;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIcon, (UnityEngine.Object) null))
        this.mIcon.Setup(card_data);
      this.mBaseAdd.Clear();
      this.mBaseMul.Clear();
      this.mUnitAdd.Clear();
      this.mUnitMul.Clear();
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      List<ConceptCardEquipEffect> enableEquipEffects = card_data.GetEnableEquipEffects(unit, job);
      bool flag = false;
      for (int index = 0; index < enableEquipEffects.Count; ++index)
      {
        enableEquipEffects[index].GetStatus(ref fixed_status, ref scale_status);
        this.mBaseAdd.Add(fixed_status);
        this.mBaseMul.Add(scale_status);
        flag |= enableEquipEffects[index].IsDecreaseEffectOnSub;
      }
      this.mStatus.SetValues(this.mBaseAdd, this.mBaseMul, this.mUnitAdd, this.mUnitMul);
      bool active = flag && !ConceptCardData.IsMainSlot(this.mSelectedCardIndex) && card_data.CurrentDecreaseEffectRate > 0;
      GameUtility.SetGameObjectActive(this.DecreaseEffectObject, active);
      if (!active)
        return;
      ConceptCardUtility.SetDecreaseEffectRateText(this.DecreaseEffectText, card_data.CurrentDecreaseEffectRate);
    }

    private void SetActiveParamViewObject(bool is_empty)
    {
      this.mParamViewObject.SetActive(!is_empty);
      this.mEmptyParamViewObject.SetActive(is_empty);
    }

    private void ClickOKButton()
    {
      ConceptCardData conceptCardByIndex = this.mSelectedUnit.GetConceptCardByIndex(this.mSelectedCardIndex);
      bool flag = false;
      if (conceptCardByIndex != null && this.mSelectedCardData != null)
        flag = (long) conceptCardByIndex.UniqueID == (long) this.mSelectedCardData.UniqueID;
      if (this.mSelectedCardData == null || flag)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
        if (this.OnCancelAction == null)
          return;
        this.OnCancelAction();
      }
      else
      {
        UnitData owner = this.mSelectedCardData.GetOwner();
        if (owner != null && owner.UniqueID != this.mSelectedUnit.UniqueID)
          UIUtility.ConfirmBox(LocalizedText.Get("sys.CONCEPT_CARD_EQUIP_CONFIRM", (object) owner.UnitParam.name), new UIUtility.DialogResultEvent(this.AcceptEquipConceptCard), new UIUtility.DialogResultEvent(this.CancelEquipConceptCard));
        else
          this.AcceptEquipConceptCard(((Component) this).gameObject);
      }
    }

    private void AcceptEquipConceptCard(GameObject obj)
    {
      ConceptCardData[] conceptCardDataArray = ConceptCardUtility.SetConceptCardData(this.mSelectedUnit.ConceptCards, this.mSelectedCardIndex, this.mSelectedCardData);
      long[] equip_card_iids = new long[conceptCardDataArray.Length];
      for (int index = 0; index < conceptCardDataArray.Length; ++index)
        equip_card_iids[index] = conceptCardDataArray[index] != null ? (long) conceptCardDataArray[index].UniqueID : 0L;
      this.mFlownodeReqConceptCardSet.SetEquipParam(equip_card_iids, this.mSelectedUnit.UniqueID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      if (this.OnChangeAction == null)
        return;
      this.OnChangeAction();
    }

    private void CancelEquipConceptCard(GameObject obj)
    {
      if (this.OnCancelAction == null)
        return;
      this.OnCancelAction();
    }

    private void ClickReleaseButton()
    {
      ConceptCardData conceptCardByIndex = this.mSelectedUnit.GetConceptCardByIndex(this.mSelectedCardIndex);
      if (conceptCardByIndex == null || (long) conceptCardByIndex.UniqueID <= 0L)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
      }
      else
      {
        ConceptCardData[] conceptCardDataArray = ConceptCardUtility.SetConceptCardData(this.mSelectedUnit.ConceptCards, this.mSelectedCardIndex, (ConceptCardData) null);
        long[] equip_card_iids = new long[conceptCardDataArray.Length];
        for (int index = 0; index < conceptCardDataArray.Length; ++index)
          equip_card_iids[index] = conceptCardDataArray[index] != null ? (long) conceptCardDataArray[index].UniqueID : 0L;
        this.mFlownodeReqConceptCardSet.SetEquipParam(equip_card_iids, this.mSelectedUnit.UniqueID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
        if (this.OnChangeAction == null)
          return;
        this.OnChangeAction();
      }
    }

    private void StartRequestConceptCardDatas()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void EndRequestConceptCardDatas()
    {
      this.IsInitalized = true;
      this.mConceptCardEquipScrollList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, this.mSelectedCardIndex, exclude_card_iname: this.mOtherSlotCardInames);
      ((Component) this.mConceptCardEquipScrollList).gameObject.SetActive(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonAutoEquip, (UnityEngine.Object) null))
        return;
      List<ConceptCardData> ignore_cards = new List<ConceptCardData>();
      for (int index = 0; index < this.mSelectedUnit.ConceptCards.Length; ++index)
      {
        ConceptCardData conceptCardByIndex;
        if (index != this.mSelectedCardIndex && (conceptCardByIndex = this.mSelectedUnit.GetConceptCardByIndex(index)) != null)
          ignore_cards.Add(conceptCardByIndex);
      }
      List<ConceptCardData> conceptCards = MonoSingleton<GameManager>.Instance.Player.ConceptCards;
      if (conceptCards != null && conceptCards.Count > 0)
        ((Selectable) this.ButtonAutoEquip).interactable = conceptCards.FindIndex((Predicate<ConceptCardData>) (card =>
        {
          if (ignore_cards.FindIndex((Predicate<ConceptCardData>) (c => c.Param.iname == card.Param.iname)) >= 0)
            return false;
          UnitData owner = card.GetOwner();
          return owner == null || owner.UniqueID == this.mSelectedUnit.UniqueID;
        })) >= 0;
      else
        ((Selectable) this.ButtonAutoEquip).interactable = false;
    }

    public void SetSortName()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortText, (UnityEngine.Object) null))
        return;
      this.SortText.text = LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.SortType));
    }

    public void SetSelectedCardIcon(ConceptCardIcon card_icon)
    {
      this.mSelectedCardData = !UnityEngine.Object.op_Inequality((UnityEngine.Object) card_icon, (UnityEngine.Object) null) ? (ConceptCardData) null : card_icon.ConceptCard;
      this.mConceptCardEquipScrollList.SetSelectedCardData(this.mSelectedCardData);
    }

    public void OnChangeIgnoreEquipedCardToggle(bool isOn)
    {
      if (!this.IsInitalized)
        return;
      this.mConceptCardEquipScrollList.IsIgnoreEquipedConceptCard = isOn;
      this.mConceptCardEquipScrollList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, this.mSelectedCardIndex, true, this.mOtherSlotCardInames);
    }
  }
}
