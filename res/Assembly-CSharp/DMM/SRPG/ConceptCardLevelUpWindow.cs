// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLevelUpWindow
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
  [FlowNode.Pin(0, "一括強化ボタンを押した", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "強化素材画面のタブを押した", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "錬金素材画面のタブを押した", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(5, "限界突破画面のタブを押した", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(3, "選択クリアボタンを押した", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "MAXボタンを押した", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(10, "一括強化のための選択素材の設定完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(31, "一括強化、限界突破の素材の選択完了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(11, "強化素材画面のタブを押した", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "錬金素材画面のタブを押した", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(21, "限界突破画面のタブを押した", FlowNode.PinTypes.Output, 21)]
  public class ConceptCardLevelUpWindow : ConceptCardDetailBase, IFlowInterface
  {
    public const int PIN_INPUT_PUSH_ENHANCE_BUTTON = 0;
    public const int PIN_INPUT_PUSH_ENHANCE_TOGGLE_BUTTON = 1;
    public const int PIN_INPUT_PUSH_TRUST_TOGGLE_BUTTON = 2;
    public const int PIN_INPUT_PUSH_CLEAR_BUTTON = 3;
    public const int PIN_INPUT_PUSH_MAX_BUTTON = 4;
    public const int PIN_INPUT_PUSH_LIMIT_TOGGLE_BUTTON = 5;
    public const int PIN_OUTPUT_PUSH_ENHANCE_BUTTON = 10;
    public const int PIN_OUTPUT_PUSH_ENHANCE_TAB = 11;
    public const int PIN_OUTPUT_PUSH_TRUST_TAB = 12;
    public const int PIN_OUTPUT_PUSH_LIMIT_TAB = 21;
    public const int PIN_OUTPUT_PUSH_LIMITUP_BUTTON = 31;
    [SerializeField]
    private ScrollRect CardScrollRect;
    [SerializeField]
    private GameObject SelectedCardIcon;
    [SerializeField]
    private RectTransform TrustEnhanceListParent;
    [SerializeField]
    private RectTransform LimitUpListParent;
    [SerializeField]
    private GameObject TrustEnhanceListItemTemplate;
    [SerializeField]
    private GameObject LimitUpItemTemplate;
    [SerializeField]
    private Text CurrentLevel;
    [SerializeField]
    private Text FinishedLevel;
    [SerializeField]
    private Text MaxLevel;
    [SerializeField]
    private Text NextExp;
    [SerializeField]
    private Slider CardLvSlider;
    [SerializeField]
    private Text GetAllExp;
    [SerializeField]
    private Button DecideBtn;
    [SerializeField]
    private Button MaxBtn;
    [SerializeField]
    private SliderAnimator AddLevelGauge;
    [SerializeField]
    private GameObject MainLevelup;
    [SerializeField]
    private GameObject MainTrust;
    [SerializeField]
    private GameObject MainLimit;
    [SerializeField]
    private Toggle TabEnhanceToggle;
    [SerializeField]
    private Toggle TabTrustToggle;
    [SerializeField]
    private Toggle TabLimitToggle;
    [SerializeField]
    private RawImage TrustMasterRewardIcon;
    [SerializeField]
    private Image TrustMasterRewardFrame;
    [SerializeField]
    private GameObject TrustMasterRewardItemIconObject;
    [SerializeField]
    private ConceptCardIcon TrustMasterRewardCardIcon;
    [SerializeField]
    private Text TrustValueTxt;
    [SerializeField]
    private Text TrustPredictValueTxt;
    [SerializeField]
    private GameObject ItemNoneText;
    [SerializeField]
    private GameObject mAwakeCountAfterIconsParent;
    private Dictionary<string, int> mSelectExpMaterials = new Dictionary<string, int>();
    private List<ConceptCardMaterialData> mCacheMaxCardExpMaterialList = new List<ConceptCardMaterialData>();
    private List<ConceptCardLevelUpListItem> mCCExpListItem = new List<ConceptCardLevelUpListItem>();
    private Dictionary<string, int> mSelectTrustMaterials = new Dictionary<string, int>();
    private List<ConceptCardMaterialData> mCacheMaxCardTrustMaterialList = new List<ConceptCardMaterialData>();
    private List<ConceptCardLevelUpListItem> mCCTrustListItem = new List<ConceptCardLevelUpListItem>();
    private List<ConceptCardLimitUpListItem> mCCLimitUpListItem = new List<ConceptCardLimitUpListItem>();
    private int mLv;
    private int mExp;
    private int mTrust;
    private int mAwakeCap;
    private int mCurrentAwakeCount;
    private ConceptCardLevelUpWindow.TabState mTabState;

    private List<ConceptCardMaterialData> ConceptCardExpMaterials
    {
      get => MonoSingleton<GameManager>.Instance.Player.ConceptCardExpMaterials;
    }

    private List<ConceptCardMaterialData> ConceptCardTrustMaterials
    {
      get => MonoSingleton<GameManager>.Instance.Player.ConceptCardTrustMaterials;
    }

    private void Start()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      this.mConceptCardData = instance.SelectedConceptCardData;
      if (this.mConceptCardData == null)
        return;
      this.mExp = (int) this.mConceptCardData.Exp;
      this.mLv = (int) this.mConceptCardData.Lv;
      this.mTrust = (int) this.mConceptCardData.Trust;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedCardIcon, (UnityEngine.Object) null))
        return;
      ConceptCardIcon component = this.SelectedCardIcon.GetComponent<ConceptCardIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TrustEnhanceListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TrustEnhanceListItemTemplate.GetComponent<ConceptCardLevelUpListItem>(), (UnityEngine.Object) null))
        return;
      component.Setup(this.mConceptCardData);
      this.InitSelectedCardData();
      this.InitListItem();
      this.SetTabEnhance();
      this.InitSetTab();
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.SetTabEnhance();
      else if (this.mTabState == ConceptCardLevelUpWindow.TabState.Trust)
        this.SetTabTrust();
      else
        this.SetTabLimitUp();
      this.InitTrust();
      this.InitWindowButton();
      instance.BulkSelectedMaterialList.Clear();
      instance.SelectedAwakeMaterialList.Clear();
    }

    private void InitSelectedCardData()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
        this.CurrentLevel.text = this.mConceptCardData.Lv.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
        this.FinishedLevel.text = this.CurrentLevel.text;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxLevel, (UnityEngine.Object) null))
        this.MaxLevel.text = "/" + this.mConceptCardData.CurrentLvCap.ToString();
      int nextExp;
      int expTbl;
      ConceptCardUtility.GetExpParameter((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp, (int) this.mConceptCardData.CurrentLvCap, out int _, out nextExp, out expTbl);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
        this.NextExp.text = nextExp.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardLvSlider, (UnityEngine.Object) null))
        this.CardLvSlider.value = (float) (1.0 - (double) nextExp / (double) expTbl);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        return;
      this.GetAllExp.text = "0";
    }

    private void InitListItem()
    {
      List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, string.Empty).Split('|'));
      this.TrustEnhanceListItemTemplate.SetActive(false);
      List<ConceptCardMaterialData> cardMaterialDataList = new List<ConceptCardMaterialData>();
      cardMaterialDataList.AddRange((IEnumerable<ConceptCardMaterialData>) this.ConceptCardExpMaterials);
      if (this.mConceptCardData.GetReward() != null)
        cardMaterialDataList.AddRange((IEnumerable<ConceptCardMaterialData>) this.ConceptCardTrustMaterials);
      foreach (ConceptCardMaterialData material_data in cardMaterialDataList)
      {
        if ((int) material_data.Num != 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TrustEnhanceListItemTemplate);
          gameObject.SetActive(true);
          gameObject.transform.SetParent((Transform) this.TrustEnhanceListParent, false);
          gameObject.transform.SetSiblingIndex(0);
          ConceptCardLevelUpListItem component = gameObject.GetComponent<ConceptCardLevelUpListItem>();
          component.OnSelect = new ConceptCardLevelUpListItem.SelectExpItem(this.RefreshParamSelectItems);
          component.ChangeUseMax = new ConceptCardLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
          component.OnCheck = new ConceptCardLevelUpListItem.CheckSliderValue(this.OnCheck);
          if (stringList != null && stringList.Count > 0)
            component.SetUseMax(stringList.IndexOf(material_data.Param.iname) != -1);
          component.AddConceptCardData(material_data);
          if (material_data.Param.type == eCardType.Enhance_exp)
          {
            this.mCCExpListItem.Add(component);
          }
          else
          {
            component.SetExpObject(false);
            this.mCCTrustListItem.Add(component);
          }
          if (component.IsUseMax())
          {
            if (material_data.Param.type == eCardType.Enhance_exp)
              this.mCacheMaxCardExpMaterialList.Add(material_data);
            else
              this.mCacheMaxCardTrustMaterialList.Add(material_data);
          }
        }
      }
      if (this.mCacheMaxCardExpMaterialList != null && this.mCacheMaxCardExpMaterialList.Count > 0)
        this.mCacheMaxCardExpMaterialList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_exp - a.Param.en_exp));
      if (this.mCacheMaxCardTrustMaterialList != null && this.mCacheMaxCardTrustMaterialList.Count > 0)
        this.mCacheMaxCardTrustMaterialList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_trust - a.Param.en_trust));
      this.mCurrentAwakeCount = (int) this.mConceptCardData.AwakeCount;
      this.mAwakeCap = (int) MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam((int) this.mConceptCardData.Rarity).ConceptCardAwakeCountMax;
      this.LimitUpItemTemplate.SetActive(false);
      if (this.mConceptCardData.Param.limit_up_items != null && this.mConceptCardData.Param.limit_up_items.Count > 0 && this.mConceptCardData.Param.IsEnableAwake)
      {
        Transform parent = this.LimitUpItemTemplate.transform.parent;
        foreach (ConceptLimitUpItemParam limitUpItem in this.mConceptCardData.Param.limit_up_items)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(limitUpItem.iname);
          if (itemParam != null)
          {
            ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
            if (itemDataByItemParam != null && itemDataByItemParam.Num > 0)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LimitUpItemTemplate, parent);
              gameObject.SetActive(true);
              ConceptCardLimitUpListItem component = gameObject.GetComponent<ConceptCardLimitUpListItem>();
              component.Init(this.mCurrentAwakeCount, this.mAwakeCap, limitUpItem.num, itemDataByItemParam, new ConceptCardLimitUpListItem.CheckSliderValue(this.OnCheckLimitUp), new Action(this.RefreshLimitUp));
              this.mCCLimitUpListItem.Add(component);
            }
          }
        }
      }
      DataSource.Bind<ConceptCardData>(this.MainLimit, this.mConceptCardData);
    }

    private void InitSetTab() => this.mTabState = ConceptCardLevelUpWindow.LoadTabState();

    private void InitTrust()
    {
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null)
      {
        this.TrustValueTxt.text = "-";
        this.TrustPredictValueTxt.text = "-";
        ((Component) this.TrustMasterRewardFrame).gameObject.SetActive(false);
      }
      else
      {
        bool is_on = reward.reward_type == eRewardType.ConceptCard;
        this.SwitchObject(is_on, ((Component) this.TrustMasterRewardCardIcon).gameObject, this.TrustMasterRewardItemIconObject);
        if (is_on)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
          if (cardDataForDisplay != null)
            this.TrustMasterRewardCardIcon.Setup(cardDataForDisplay);
        }
        else
          this.LoadImage(reward.GetIconPath(), this.TrustMasterRewardIcon);
        this.SetSprite(this.TrustMasterRewardFrame, reward.GetFrameSprite());
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustValueTxt, (int) this.mConceptCardData.Trust);
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustPredictValueTxt, (int) this.mConceptCardData.Trust);
      }
    }

    private void InitWindowButton()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardScrollRect, (UnityEngine.Object) null))
        this.CardScrollRect.verticalNormalizedPosition = 1f;
      int num1 = 0;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        ((Selectable) this.MaxBtn).interactable = this.mCacheMaxCardExpMaterialList != null && this.mCacheMaxCardExpMaterialList.Count > 0;
        ((Selectable) this.DecideBtn).interactable = false;
        if (this.mSelectExpMaterials == null)
          return;
        foreach (KeyValuePair<string, int> selectExpMaterial in this.mSelectExpMaterials)
          num1 += selectExpMaterial.Value;
        if (num1 <= 0)
          return;
        ((Selectable) this.DecideBtn).interactable = true;
      }
      else if (this.mTabState == ConceptCardLevelUpWindow.TabState.Trust)
      {
        ((Selectable) this.MaxBtn).interactable = this.mCacheMaxCardTrustMaterialList != null && this.mCacheMaxCardTrustMaterialList.Count > 0;
        ((Selectable) this.DecideBtn).interactable = false;
        if (this.mSelectTrustMaterials == null)
          return;
        foreach (KeyValuePair<string, int> selectTrustMaterial in this.mSelectTrustMaterials)
          num1 += selectTrustMaterial.Value;
        if (num1 <= 0)
          return;
        ((Selectable) this.DecideBtn).interactable = true;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.LimitUp)
          return;
        int num2 = 0;
        if (this.mCCLimitUpListItem != null)
        {
          foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
            num2 += cardLimitUpListItem.GetUpCount();
        }
        ((Selectable) this.DecideBtn).interactable = num2 > 0;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.SetSelectMaterials();
          if (this.mTabState == ConceptCardLevelUpWindow.TabState.LimitUp)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
          this.SetTabEnhance();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 2:
          this.SetTabTrust();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          break;
        case 3:
          this.OnClear();
          break;
        case 4:
          this.OnMax();
          break;
        case 5:
          this.SetTabLimitUp();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
          break;
      }
    }

    private void OnClear()
    {
      Dictionary<string, int> dictionary;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
        cardLevelUpListItemList = this.mCCExpListItem;
      }
      else if (this.mTabState == ConceptCardLevelUpWindow.TabState.Trust)
      {
        dictionary = this.mSelectTrustMaterials;
        cardLevelUpListItemList = this.mCCTrustListItem;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.LimitUp || this.mCCLimitUpListItem == null || this.mCCLimitUpListItem.Count <= 0)
          return;
        using (List<ConceptCardLimitUpListItem>.Enumerator enumerator = this.mCCLimitUpListItem.GetEnumerator())
        {
          while (enumerator.MoveNext())
            enumerator.Current.OnReset();
          return;
        }
      }
      if (dictionary.Count <= 0)
        return;
      for (int index = 0; index < cardLevelUpListItemList.Count; ++index)
      {
        ConceptCardLevelUpListItem component = ((Component) cardLevelUpListItemList[index]).GetComponent<ConceptCardLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      dictionary.Clear();
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.RefreshFinishedExpStatus();
      else
        this.RefreshFinishedTrustStatus();
    }

    private void OnMax()
    {
      List<ConceptCardMaterialData> cardMaterialDataList;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        cardMaterialDataList = this.mCacheMaxCardExpMaterialList;
        cardLevelUpListItemList = this.mCCExpListItem;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        cardMaterialDataList = this.mCacheMaxCardTrustMaterialList;
        cardLevelUpListItemList = this.mCCTrustListItem;
      }
      if (cardMaterialDataList == null || cardMaterialDataList.Count < 0)
        return;
      for (int index = 0; index < cardLevelUpListItemList.Count; ++index)
      {
        ConceptCardLevelUpListItem component = ((Component) cardLevelUpListItemList[index]).GetComponent<ConceptCardLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.CalcCanCardLevelUpMax();
      else
        this.CalcCanCardTrustUpMax();
    }

    private int OnCheck(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num == 0)
        return -1;
      Dictionary<string, int> dictionary;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return -1;
        dictionary = this.mSelectTrustMaterials;
      }
      if (dictionary.ContainsKey(iname) && dictionary[iname] > num)
        return -1;
      ConceptCardParam conceptCardParam1 = this.Master.GetConceptCardParam(iname);
      if (conceptCardParam1 == null || MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) == 0)
        return -1;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        long expToLevelMax = (long) this.mConceptCardData.GetExpToLevelMax();
        long num1 = 0;
        foreach (string key in dictionary.Keys)
        {
          if (!(key == iname))
          {
            ConceptCardParam conceptCardParam2 = this.Master.GetConceptCardParam(key);
            if (conceptCardParam2 != null)
              num1 += (long) (conceptCardParam2.en_exp * dictionary[key]);
          }
        }
        long num2 = expToLevelMax - num1;
        long num3 = (long) (conceptCardParam1.en_exp * num);
        if (num2 < num3)
          return Mathf.CeilToInt((float) num2 / (float) conceptCardParam1.en_exp);
      }
      else
      {
        int num4 = this.GetNextTrustExp((int) this.mConceptCardData.Trust) - (int) this.mConceptCardData.Trust;
        int num5 = 0;
        foreach (string key in dictionary.Keys)
        {
          if (!(key == iname))
          {
            ConceptCardParam conceptCardParam3 = this.Master.GetConceptCardParam(key);
            if (conceptCardParam3 != null)
              num5 += conceptCardParam3.en_trust * dictionary[key];
          }
        }
        int num6 = num4 - num5;
        long num7 = (long) (conceptCardParam1.en_trust * num);
        if ((long) num6 < num7)
          return Mathf.CeilToInt((float) num6 / (float) conceptCardParam1.en_trust);
      }
      return num;
    }

    private int OnCheckLimitUp(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num <= 0)
        return 0;
      int num1 = this.mAwakeCap - this.mCurrentAwakeCount;
      if (num1 <= 0)
        return 0;
      int num2 = 0;
      foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
      {
        ItemData itemData = cardLimitUpListItem.GetItemData();
        if (itemData != null && itemData.ItemID != iname)
          num2 += cardLimitUpListItem.GetUpCount();
      }
      return num2 + num > num1 ? num1 - num2 : num;
    }

    private void RefreshParamSelectItems(string iname, int value)
    {
      if (string.IsNullOrEmpty(iname) || MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) == 0)
        return;
      Dictionary<string, int> dictionary;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        dictionary = this.mSelectTrustMaterials;
      }
      if (!dictionary.ContainsKey(iname))
        dictionary.Add(iname, value);
      else
        dictionary[iname] = value;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.RefreshFinishedExpStatus();
      else
        this.RefreshFinishedTrustStatus();
    }

    private void RefreshFinishedExpStatus()
    {
      if (this.mSelectExpMaterials == null || this.mSelectExpMaterials.Count <= 0)
        return;
      int num1 = 0;
      foreach (string key in this.mSelectExpMaterials.Keys)
      {
        ConceptCardParam conceptCardParam = this.Master.GetConceptCardParam(key);
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(key);
        if (conceptCardParam != null)
        {
          int selectExpMaterial = this.mSelectExpMaterials[key];
          if (selectExpMaterial != 0 && selectExpMaterial <= conceptCardMaterialNum)
          {
            int num2 = conceptCardParam.en_exp * selectExpMaterial;
            num1 += num2;
          }
        }
      }
      int conceptCardLevelExp1 = this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.CurrentLvCap);
      this.mExp = Math.Min((int) this.mConceptCardData.Exp + num1, conceptCardLevelExp1);
      this.mLv = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp + num1, (int) this.mConceptCardData.CurrentLvCap);
      foreach (ConceptCardLevelUpListItem cardLevelUpListItem in this.mCCExpListItem)
        cardLevelUpListItem.SetInputLock(this.mExp < conceptCardLevelExp1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
      {
        this.FinishedLevel.text = this.mLv.ToString();
        if (this.mLv >= (int) this.mConceptCardData.CurrentLvCap)
          ((Graphic) this.FinishedLevel).color = Color.red;
        else if (this.mLv > (int) this.mConceptCardData.Lv)
          ((Graphic) this.FinishedLevel).color = Color.green;
        else
          ((Graphic) this.FinishedLevel).color = Color.white;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (this.mExp == (int) this.mConceptCardData.Exp || num1 == 0)
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        else
          this.AddLevelGauge.AnimateValue(Mathf.Min(1f, Mathf.Clamp01((float) (this.mExp - this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Lv)) / (float) this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Lv))), 0.0f);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
      {
        int num3 = 0;
        if (this.mExp < conceptCardLevelExp1)
        {
          int conceptCardLevelExp2 = this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, this.mLv);
          int conceptCardNextExp = this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, this.mLv);
          if (this.mExp >= conceptCardLevelExp2)
            conceptCardNextExp = this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, Math.Min((int) this.mConceptCardData.CurrentLvCap, this.mLv + 1));
          int num4 = this.mExp - conceptCardLevelExp2;
          num3 = Math.Max(0, conceptCardNextExp <= num4 ? 0 : conceptCardNextExp - num4);
        }
        this.NextExp.text = num3.ToString();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        this.GetAllExp.text = num1.ToString();
      ((Selectable) this.DecideBtn).interactable = num1 > 0;
    }

    private void CalcCanCardLevelUpMax()
    {
      if (this.mCacheMaxCardExpMaterialList == null)
        return;
      long num1 = 0;
      foreach (ConceptCardMaterialData maxCardExpMaterial in this.mCacheMaxCardExpMaterialList)
      {
        int num2 = maxCardExpMaterial.Param.en_exp * MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial.Param.iname);
        num1 += (long) num2;
      }
      if (num1 < 0L)
        num1 = long.MaxValue;
      long num3 = (long) Mathf.Min((float) this.mConceptCardData.GetExpToLevelMax(), (float) num1);
      this.mSelectExpMaterials.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheMaxCardExpMaterialList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ConceptCardMaterialData maxCardExpMaterial1 = this.mCacheMaxCardExpMaterialList[index2];
        int conceptCardMaterialNum1 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial1.Param.iname);
        if (maxCardExpMaterial1 != null || conceptCardMaterialNum1 > 0)
        {
          ConceptCardMaterialData maxCardExpMaterial2 = this.mCacheMaxCardExpMaterialList[index1];
          int conceptCardMaterialNum2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial2.Param.iname);
          if (index2 == index1 || maxCardExpMaterial2 != null || conceptCardMaterialNum2 > 0)
          {
            if ((long) maxCardExpMaterial1.Param.en_exp > num3)
            {
              index1 = index2;
            }
            else
            {
              int num4 = (int) (num3 / (long) maxCardExpMaterial1.Param.en_exp);
              int num5 = Mathf.Min(conceptCardMaterialNum1, num4);
              int num6 = maxCardExpMaterial1.Param.en_exp * num5;
              int enExp = maxCardExpMaterial2.Param.en_exp;
              if ((long) Mathf.Abs((float) (num3 - (long) num6)) > (long) Mathf.Abs((float) (num3 - (long) enExp)))
              {
                if (this.mSelectExpMaterials.ContainsKey(maxCardExpMaterial2.Param.iname))
                {
                  if (conceptCardMaterialNum2 - this.mSelectExpMaterials[maxCardExpMaterial2.Param.iname] > 0)
                  {
                    Dictionary<string, int> selectExpMaterials;
                    string iname;
                    (selectExpMaterials = this.mSelectExpMaterials)[iname = maxCardExpMaterial2.Param.iname] = selectExpMaterials[iname] + 1;
                    num3 = 0L;
                    break;
                  }
                }
                else
                {
                  this.mSelectExpMaterials.Add(maxCardExpMaterial2.Param.iname, 1);
                  num3 = 0L;
                  break;
                }
              }
              num3 -= (long) num6;
              this.mSelectExpMaterials.Add(maxCardExpMaterial1.Param.iname, num5);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ConceptCardMaterialData maxCardExpMaterial = this.mCacheMaxCardExpMaterialList[index1];
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial.Param.iname);
        if (maxCardExpMaterial != null && conceptCardMaterialNum > 0)
        {
          if (this.mSelectExpMaterials.ContainsKey(maxCardExpMaterial.Param.iname))
          {
            if (conceptCardMaterialNum - this.mSelectExpMaterials[maxCardExpMaterial.Param.iname] > 0)
            {
              Dictionary<string, int> selectExpMaterials;
              string iname;
              (selectExpMaterials = this.mSelectExpMaterials)[iname = maxCardExpMaterial.Param.iname] = selectExpMaterials[iname] + 1;
            }
          }
          else
            this.mSelectExpMaterials.Add(maxCardExpMaterial.Param.iname, 1);
        }
      }
      if (this.mSelectExpMaterials.Count > 0)
      {
        for (int index3 = 0; index3 < this.mCCExpListItem.Count; ++index3)
        {
          if (this.mSelectExpMaterials.ContainsKey(this.mCCExpListItem[index3].GetConceptCardIName()))
            this.mCCExpListItem[index3].SetUseParamItemSliderValue(this.mSelectExpMaterials[this.mCCExpListItem[index3].GetConceptCardIName()]);
        }
      }
      this.RefreshFinishedExpStatus();
    }

    private void RefreshFinishedTrustStatus()
    {
      if (this.mSelectTrustMaterials == null || this.mSelectTrustMaterials.Count <= 0)
        return;
      int num1 = 0;
      foreach (string key in this.mSelectTrustMaterials.Keys)
      {
        ConceptCardParam conceptCardParam = this.Master.GetConceptCardParam(key);
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(key);
        if (conceptCardParam != null)
        {
          int selectTrustMaterial = this.mSelectTrustMaterials[key];
          if (selectTrustMaterial != 0 && selectTrustMaterial <= conceptCardMaterialNum)
          {
            int num2 = conceptCardParam.en_trust * selectTrustMaterial;
            num1 += num2;
          }
        }
      }
      int conceptCardTrustMax = this.Master.GetConceptCardTrustMax(this.mConceptCardData);
      int nextTrustExp = this.GetNextTrustExp((int) this.mConceptCardData.Trust);
      this.mTrust = Math.Min((int) this.mConceptCardData.Trust + num1, conceptCardTrustMax);
      foreach (ConceptCardLevelUpListItem cardLevelUpListItem in this.mCCTrustListItem)
        cardLevelUpListItem.SetInputLock(this.mTrust < nextTrustExp);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TrustPredictValueTxt, (UnityEngine.Object) null))
      {
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustPredictValueTxt, this.mTrust);
        if (this.mTrust >= nextTrustExp)
          ((Graphic) this.TrustPredictValueTxt).color = Color.red;
        else if (this.mTrust > (int) this.mConceptCardData.Trust)
          ((Graphic) this.TrustPredictValueTxt).color = Color.green;
        else
          ((Graphic) this.TrustPredictValueTxt).color = Color.white;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (this.mTrust == (int) this.mConceptCardData.Trust || num1 == 0)
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        else
          this.AddLevelGauge.AnimateValue(Mathf.Min(1f, Mathf.Clamp01((float) (int) this.mConceptCardData.Trust / (float) (this.Master.GetConceptCardTrustMax(this.mConceptCardData) - (int) this.mConceptCardData.Trust))), 0.0f);
      }
      ((Selectable) this.DecideBtn).interactable = num1 > 0;
    }

    private int GetNextTrustExp(int trust)
    {
      int nextTrustExp = trust / (int) this.Master.FixParam.CardTrustMax * (int) this.Master.FixParam.CardTrustMax + (int) this.Master.FixParam.CardTrustMax;
      if (trust >= ((int) this.mConceptCardData.AwakeCount + 1) * (int) this.Master.FixParam.CardTrustMax)
        nextTrustExp = ((int) this.mConceptCardData.AwakeCount + 1) * (int) this.Master.FixParam.CardTrustMax;
      return nextTrustExp;
    }

    private void CalcCanCardTrustUpMax()
    {
      if (this.mCacheMaxCardTrustMaterialList == null)
        return;
      long num1 = 0;
      foreach (ConceptCardMaterialData cardTrustMaterial in this.mCacheMaxCardTrustMaterialList)
      {
        int num2 = cardTrustMaterial.Param.en_trust * MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial.Param.iname);
        num1 += (long) num2;
      }
      long num3 = (long) Mathf.Min((float) (this.GetNextTrustExp(this.mTrust) - this.mTrust), (float) num1);
      this.mSelectTrustMaterials.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheMaxCardTrustMaterialList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ConceptCardMaterialData cardTrustMaterial1 = this.mCacheMaxCardTrustMaterialList[index2];
        int conceptCardMaterialNum1 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial1.Param.iname);
        if (cardTrustMaterial1 != null || conceptCardMaterialNum1 > 0)
        {
          ConceptCardMaterialData cardTrustMaterial2 = this.mCacheMaxCardTrustMaterialList[index1];
          int conceptCardMaterialNum2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial2.Param.iname);
          if (index2 == index1 || cardTrustMaterial2 != null || conceptCardMaterialNum2 > 0)
          {
            if ((long) cardTrustMaterial1.Param.en_trust > num3)
            {
              index1 = index2;
            }
            else
            {
              int num4 = (int) (num3 / (long) cardTrustMaterial1.Param.en_trust);
              int num5 = Mathf.Min(conceptCardMaterialNum1, num4);
              int num6 = cardTrustMaterial1.Param.en_trust * num5;
              int enTrust = cardTrustMaterial2.Param.en_trust;
              if ((long) Mathf.Abs((float) (num3 - (long) num6)) > (long) Mathf.Abs((float) (num3 - (long) enTrust)))
              {
                if (this.mSelectTrustMaterials.ContainsKey(cardTrustMaterial2.Param.iname))
                {
                  if (conceptCardMaterialNum2 - this.mSelectTrustMaterials[cardTrustMaterial2.Param.iname] > 0)
                  {
                    Dictionary<string, int> selectTrustMaterials;
                    string iname;
                    (selectTrustMaterials = this.mSelectTrustMaterials)[iname = cardTrustMaterial2.Param.iname] = selectTrustMaterials[iname] + 1;
                    num3 = 0L;
                    break;
                  }
                }
                else
                {
                  this.mSelectTrustMaterials.Add(cardTrustMaterial2.Param.iname, 1);
                  num3 = 0L;
                  break;
                }
              }
              num3 -= (long) num6;
              this.mSelectTrustMaterials.Add(cardTrustMaterial1.Param.iname, num5);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ConceptCardMaterialData cardTrustMaterial = this.mCacheMaxCardTrustMaterialList[index1];
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial.Param.iname);
        if (cardTrustMaterial != null && conceptCardMaterialNum > 0)
        {
          if (this.mSelectTrustMaterials.ContainsKey(cardTrustMaterial.Param.iname))
          {
            if (conceptCardMaterialNum - this.mSelectTrustMaterials[cardTrustMaterial.Param.iname] > 0)
            {
              Dictionary<string, int> selectTrustMaterials;
              string iname;
              (selectTrustMaterials = this.mSelectTrustMaterials)[iname = cardTrustMaterial.Param.iname] = selectTrustMaterials[iname] + 1;
            }
          }
          else
            this.mSelectTrustMaterials.Add(cardTrustMaterial.Param.iname, 1);
        }
      }
      if (this.mSelectTrustMaterials.Count > 0)
      {
        for (int index3 = 0; index3 < this.mCCTrustListItem.Count; ++index3)
        {
          if (this.mSelectTrustMaterials.ContainsKey(this.mCCTrustListItem[index3].GetConceptCardIName()))
            this.mCCTrustListItem[index3].SetUseParamItemSliderValue(this.mSelectTrustMaterials[this.mCCTrustListItem[index3].GetConceptCardIName()]);
        }
      }
      this.RefreshFinishedTrustStatus();
    }

    private void RefreshUseMaxItems(string iname, bool is_on)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      List<ConceptCardMaterialData> cardMaterialDataList;
      ConceptCardMaterialData cardMaterialData;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        cardMaterialDataList = this.mCacheMaxCardExpMaterialList;
        cardMaterialData = this.ConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname));
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        cardMaterialDataList = this.mCacheMaxCardTrustMaterialList;
        cardMaterialData = this.ConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname));
      }
      if (cardMaterialData == null)
        return;
      if (!is_on)
      {
        if (cardMaterialDataList.FindIndex((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)) != -1)
          cardMaterialDataList.RemoveAt(cardMaterialDataList.FindIndex((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)));
      }
      else if (cardMaterialDataList.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)) == null)
        cardMaterialDataList.Add(cardMaterialData);
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        cardMaterialDataList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_exp - a.Param.en_exp));
      else
        cardMaterialDataList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_trust - a.Param.en_trust));
      this.SaveSelectUseMax();
      ((Selectable) this.MaxBtn).interactable = cardMaterialDataList != null && cardMaterialDataList.Count > 0;
    }

    private void SavePage()
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, ((int) this.mTabState).ToString(), true);
    }

    private void SaveSelectUseMax()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mCacheMaxCardExpMaterialList.Count; ++index)
        stringList.Add(this.mCacheMaxCardExpMaterialList[index].Param.iname);
      for (int index = 0; index < this.mCacheMaxCardTrustMaterialList.Count; ++index)
        stringList.Add(this.mCacheMaxCardTrustMaterialList[index].Param.iname);
      string str = stringList.Count <= 0 ? string.Empty : string.Join("|", stringList.ToArray());
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, str, true);
    }

    private static ConceptCardLevelUpWindow.TabState LoadTabState()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS))
        return ConceptCardLevelUpWindow.TabState.Enhance;
      string s = PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, string.Empty);
      if (string.IsNullOrEmpty(s))
        return ConceptCardLevelUpWindow.TabState.Enhance;
      int result = 0;
      return !int.TryParse(s, out result) ? ConceptCardLevelUpWindow.TabState.Enhance : (ConceptCardLevelUpWindow.TabState) result;
    }

    private void SetSelectMaterials()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<SelecteConceptCardMaterial> conceptCardMaterialList = new List<SelecteConceptCardMaterial>();
      Dictionary<string, int> dictionary1;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      List<ConceptCardMaterialData> cardMaterialDataList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary1 = this.mSelectExpMaterials;
        cardLevelUpListItemList = this.mCCExpListItem;
        cardMaterialDataList = this.ConceptCardExpMaterials;
      }
      else if (this.mTabState == ConceptCardLevelUpWindow.TabState.Trust)
      {
        dictionary1 = this.mSelectTrustMaterials;
        cardLevelUpListItemList = this.mCCTrustListItem;
        cardMaterialDataList = this.ConceptCardTrustMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.LimitUp || this.mCCLimitUpListItem == null || this.mCCLimitUpListItem.Count <= 0)
          return;
        Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
        foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
        {
          int useNum = cardLimitUpListItem.GetUseNum();
          if (useNum > 0)
          {
            ItemData itemData = cardLimitUpListItem.GetItemData();
            if (itemData != null)
            {
              if (!dictionary2.ContainsKey(itemData.Param.iname))
              {
                dictionary2.Add(itemData.Param.iname, useNum);
              }
              else
              {
                Dictionary<string, int> dictionary3;
                string iname;
                (dictionary3 = dictionary2)[iname = itemData.Param.iname] = dictionary3[iname] + useNum;
              }
            }
          }
        }
        instance.SelectedAwakeMaterialList = dictionary2;
        return;
      }
      foreach (string key1 in dictionary1.Keys)
      {
        string key = key1;
        if (this.Master.GetConceptCardParam(key) != null)
        {
          int num1 = dictionary1[key];
          if (num1 <= MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(key) && num1 > 0)
          {
            int num2 = 0;
            foreach (ConceptCardMaterialData cardMaterialData in cardMaterialDataList)
            {
              if (cardMaterialData.Param.iname == key)
              {
                ConceptCardLevelUpListItem cardLevelUpListItem = cardLevelUpListItemList.Find((Predicate<ConceptCardLevelUpListItem>) (ccd => ccd.GetConceptCardIName() == key));
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) cardLevelUpListItem, (UnityEngine.Object) null))
                {
                  conceptCardMaterialList.Add(new SelecteConceptCardMaterial()
                  {
                    mUniqueID = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialUniqueID(key),
                    mSelectedData = cardLevelUpListItem.GetConceptCardData(),
                    mSelectNum = num1
                  });
                  ++num2;
                  if (num2 == num1)
                    break;
                }
              }
            }
          }
        }
      }
      instance.BulkSelectedMaterialList = conceptCardMaterialList;
    }

    private void SetTabEnhance()
    {
      this.mTabState = ConceptCardLevelUpWindow.TabState.Enhance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLevelup, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainTrust, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLimit, (UnityEngine.Object) null))
        return;
      this.MainLevelup.SetActive(true);
      this.MainTrust.SetActive(false);
      this.MainLimit.SetActive(false);
      this.ItemNoneText.SetActive(this.ConceptCardExpMaterials == null || this.ConceptCardExpMaterials.Count <= 0);
      ((Component) this.TrustEnhanceListParent).gameObject.SetActive(true);
      ((Component) this.LimitUpListParent).gameObject.SetActive(false);
      foreach (Component component in this.mCCExpListItem)
        component.gameObject.SetActive(true);
      foreach (Component component in this.mCCTrustListItem)
        component.gameObject.SetActive(false);
      this.TabEnhanceToggle.isOn = true;
      this.TabTrustToggle.isOn = false;
      this.TabLimitToggle.isOn = false;
      this.CardScrollRect.content = this.TrustEnhanceListParent;
      this.InitWindowButton();
      this.SavePage();
    }

    private void SetTabTrust()
    {
      this.mTabState = ConceptCardLevelUpWindow.TabState.Trust;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLevelup, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainTrust, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLimit, (UnityEngine.Object) null))
        return;
      this.MainLevelup.SetActive(false);
      this.MainTrust.SetActive(true);
      this.MainLimit.SetActive(false);
      this.ItemNoneText.SetActive(this.mCCTrustListItem == null || this.mCCTrustListItem.Count <= 0);
      ((Component) this.TrustEnhanceListParent).gameObject.SetActive(true);
      ((Component) this.LimitUpListParent).gameObject.SetActive(false);
      foreach (Component component in this.mCCExpListItem)
        component.gameObject.SetActive(false);
      foreach (Component component in this.mCCTrustListItem)
        component.gameObject.SetActive(true);
      this.TabEnhanceToggle.isOn = false;
      this.TabTrustToggle.isOn = true;
      this.TabLimitToggle.isOn = false;
      this.CardScrollRect.content = this.TrustEnhanceListParent;
      this.InitWindowButton();
      this.SavePage();
    }

    private void SetTabLimitUp()
    {
      this.mTabState = ConceptCardLevelUpWindow.TabState.LimitUp;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        instance.BulkSelectedMaterialList.Clear();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLevelup, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainTrust, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLimit, (UnityEngine.Object) null))
        return;
      this.MainLevelup.SetActive(false);
      this.MainTrust.SetActive(false);
      this.MainLimit.SetActive(true);
      if (this.mConceptCardData.Param.limit_up_items == null || this.mConceptCardData.Param.limit_up_items.Count <= 0 || !this.mConceptCardData.Param.IsEnableAwake)
      {
        this.ItemNoneText.SetActive(true);
      }
      else
      {
        bool flag = true;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        foreach (ConceptLimitUpItemParam limitUpItem in this.mConceptCardData.Param.limit_up_items)
        {
          if (player.GetItemAmount(limitUpItem.iname) > 0)
          {
            flag = false;
            break;
          }
        }
        this.ItemNoneText.SetActive(flag);
      }
      ((Component) this.TrustEnhanceListParent).gameObject.SetActive(false);
      ((Component) this.LimitUpListParent).gameObject.SetActive(true);
      foreach (Component component in this.mCCExpListItem)
        component.gameObject.SetActive(false);
      foreach (Component component in this.mCCTrustListItem)
        component.gameObject.SetActive(false);
      this.RefreshLimitUp();
      this.TabEnhanceToggle.isOn = false;
      this.TabTrustToggle.isOn = false;
      this.TabLimitToggle.isOn = true;
      this.CardScrollRect.content = this.LimitUpListParent;
      this.InitWindowButton();
      this.SavePage();
    }

    private void RefreshLimitUp()
    {
      int add_awake_count = 0;
      foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
        add_awake_count += cardLimitUpListItem.GetUpCount();
      this.RefreshAwakeIcons(add_awake_count);
      if (add_awake_count >= this.mAwakeCap - this.mCurrentAwakeCount)
      {
        foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
          cardLimitUpListItem.SetInputLock(true);
      }
      else
      {
        foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
          cardLimitUpListItem.SetInputLock(false);
      }
    }

    public void AddLimitUpEnable(bool enable)
    {
      foreach (ConceptCardLimitUpListItem cardLimitUpListItem in this.mCCLimitUpListItem)
        cardLimitUpListItem.SetInputLock(enable);
    }

    private void RefreshAwakeIcons(int add_awake_count)
    {
      if (add_awake_count + this.mCurrentAwakeCount > this.mAwakeCap)
        add_awake_count = this.mAwakeCap - this.mCurrentAwakeCount;
      int num = 0;
      foreach (Transform transform1 in this.mAwakeCountAfterIconsParent.transform)
      {
        foreach (Component component in transform1)
          component.gameObject.SetActive(false);
        string str = "off";
        if (num < this.mCurrentAwakeCount)
          str = "on";
        else if (num < this.mCurrentAwakeCount + add_awake_count)
          str = "up";
        Transform transform2 = transform1.Find(str);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
        {
          ((Component) transform2).gameObject.SetActive(true);
          ++num;
        }
      }
      ((Selectable) this.DecideBtn).interactable = add_awake_count > 0;
    }

    private enum TabState
    {
      Enhance,
      Trust,
      LimitUp,
    }
  }
}
