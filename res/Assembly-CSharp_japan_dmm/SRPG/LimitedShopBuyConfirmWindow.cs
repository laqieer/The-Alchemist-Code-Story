// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Slider Plus", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Slider Minus", FlowNode.PinTypes.Input, 11)]
  public class LimitedShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private const int PINID_REFRESH = 1;
    private const int PINID_SLIDER_PLUS = 10;
    private const int PINID_SLIDER_MINUS = 11;
    public GameObject limited_item;
    public GameObject no_limited_item;
    public GameObject Sold;
    public Text SoldNum;
    public Text TextDesc;
    [SerializeField]
    private GameObject HasCount;
    [HeaderBar("▼アイコン表示用オブジェクト")]
    public GameObject ItemIconRoot;
    public GameObject ConceptCardIconRoot;
    [HeaderBar("▼右側の表示領域")]
    public GameObject LayoutRight;
    public GameObject EnableEquipUnitWindow;
    public RectTransform UnitLayoutParent;
    public GameObject UnitTemplate;
    public GameObject ConceptCardDetail;
    [HeaderBar("▼まとめ買い用")]
    public GameObject AmountSliderHolder;
    public Slider AmountSlider;
    public Text AmountSliderNum;
    public Button IncrementButton;
    public Button DecrementButton;
    public Text LimitedItemPriceText;
    [HeaderBar("▼所持 幻晶石/ゼニー等")]
    public GameObject HasJem;
    public GameObject HasCoin;
    public GameObject HasZenny;
    private List<GameObject> mUnits;
    private bool mEnabledSlider;
    private LimitedShopItem mShopitem;

    private void Awake()
    {
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitTemplate, (UnityEngine.Object) null) && this.UnitTemplate.activeInHierarchy)
        this.UnitTemplate.SetActive(false);
      this.mUnits = new List<GameObject>(MonoSingleton<GameManager>.Instance.Player.Units.Count);
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          break;
        case 10:
          this.IncrementSliderValue();
          break;
        case 11:
          this.DecrementSliderValue();
          break;
      }
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnitTemplate, (UnityEngine.Object) null))
        return;
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].gameObject.SetActive(false);
      this.mShopitem = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData().items.FirstOrDefault<LimitedShopItem>((Func<LimitedShopItem, bool>) (item => item.id == GlobalVars.ShopBuyIndex));
      bool flag1 = !this.mShopitem.IsNotLimited || this.mShopitem.saleType == ESaleType.Coin_P;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.limited_item, (UnityEngine.Object) null))
        this.limited_item.SetActive(flag1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.no_limited_item, (UnityEngine.Object) null))
        this.no_limited_item.SetActive(!flag1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Sold, (UnityEngine.Object) null))
        this.Sold.SetActive(!this.mShopitem.IsNotLimited);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SoldNum, (UnityEngine.Object) null))
        this.SoldNum.text = this.mShopitem.remaining_num.ToString();
      this.mEnabledSlider = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AmountSliderHolder, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AmountSlider, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AmountSliderNum, (UnityEngine.Object) null))
      {
        if (!this.mShopitem.IsNotLimited && this.mShopitem.remaining_num > 1)
        {
          this.mEnabledSlider = true;
          GameParameter component = ((Component) this.LimitedItemPriceText).GetComponent<GameParameter>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).enabled = false;
          this.AmountSliderHolder.SetActive(true);
          int remainingCurrency = ShopData.GetRemainingCurrency((ShopItem) this.mShopitem);
          int buyPrice = ShopData.GetBuyPrice((ShopItem) this.mShopitem);
          int num1 = 1;
          if (buyPrice > 0)
          {
            while (buyPrice * num1 <= remainingCurrency)
            {
              ++num1;
              if (this.mShopitem.remaining_num + 1 < num1)
                break;
            }
          }
          int num2 = Math.Max(Math.Min(num1 - 1, this.mShopitem.remaining_num), 1);
          this.AmountSlider.minValue = 1f;
          this.AmountSlider.maxValue = (float) num2;
          this.SetSliderValue(1f);
          // ISSUE: method pointer
          ((UnityEvent<float>) this.AmountSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSliderValueChanged)));
        }
        else
        {
          this.mEnabledSlider = false;
          this.AmountSliderHolder.SetActive(false);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HasJem, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HasCoin, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HasZenny, (UnityEngine.Object) null))
      {
        switch (this.mShopitem.saleType)
        {
          case ESaleType.Gold:
            this.HasJem.SetActive(false);
            this.HasCoin.SetActive(false);
            this.HasZenny.SetActive(true);
            break;
          case ESaleType.Coin:
          case ESaleType.Coin_P:
            this.HasJem.SetActive(true);
            this.HasCoin.SetActive(false);
            this.HasZenny.SetActive(false);
            break;
          default:
            this.HasJem.SetActive(false);
            this.HasCoin.SetActive(true);
            this.HasZenny.SetActive(false);
            break;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnableEquipUnitWindow, (UnityEngine.Object) null))
      {
        this.EnableEquipUnitWindow.gameObject.SetActive(false);
        int index1 = 0;
        for (int index2 = 0; index2 < units.Count; ++index2)
        {
          UnitData data = units[index2];
          bool flag2 = false;
          for (int index3 = 0; index3 < data.Jobs.Length; ++index3)
          {
            JobData job = data.Jobs[index3];
            if (job.IsActivated)
            {
              int equipSlotByItemId = job.FindEquipSlotByItemID(this.mShopitem.iname);
              if (equipSlotByItemId != -1 && job.CheckEnableEquipSlot(equipSlotByItemId))
              {
                flag2 = true;
                break;
              }
            }
          }
          if (flag2)
          {
            if (index1 >= this.mUnits.Count)
            {
              this.UnitTemplate.SetActive(true);
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitTemplate);
              gameObject.transform.SetParent((Transform) this.UnitLayoutParent, false);
              this.mUnits.Add(gameObject);
              this.UnitTemplate.SetActive(false);
            }
            GameObject gameObject1 = this.mUnits[index1].gameObject;
            DataSource.Bind<UnitData>(gameObject1, data);
            gameObject1.SetActive(true);
            this.EnableEquipUnitWindow.gameObject.SetActive(true);
            GameUtility.SetGameObjectActive(this.LayoutRight, true);
            ++index1;
          }
        }
      }
      DataSource.Bind<LimitedShopItem>(((Component) this).gameObject, this.mShopitem);
      if (this.mShopitem.IsArtifact)
        DataSource.Bind<ArtifactParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname));
      else if (this.mShopitem.IsConceptCard)
      {
        GameUtility.SetGameObjectActive(this.ItemIconRoot, false);
        GameUtility.SetGameObjectActive(this.ConceptCardIconRoot, true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardDetail, (UnityEngine.Object) null))
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(this.mShopitem.iname);
          GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
          GameUtility.SetGameObjectActive(this.ConceptCardDetail, true);
          GameUtility.SetGameObjectActive(this.LayoutRight, true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextDesc, (UnityEngine.Object) null))
            this.TextDesc.text = cardDataForDisplay.Param.expr;
        }
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.iname);
        if (itemDataByItemId != null)
        {
          DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextDesc, (UnityEngine.Object) null))
            this.TextDesc.text = itemDataByItemId.Param.Expr;
          if (itemDataByItemId.Param.type == EItemType.Rune)
            GameUtility.SetGameObjectActive(this.HasCount, false);
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.iname);
          if (itemParam != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextDesc, (UnityEngine.Object) null))
          {
            DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextDesc, (UnityEngine.Object) null))
              this.TextDesc.text = itemParam.Expr;
            if (itemParam.type == EItemType.Rune)
              GameUtility.SetGameObjectActive(this.HasCount, false);
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void IncrementSliderValue() => this.SetSliderValue(this.AmountSlider.value + 1f);

    private void DecrementSliderValue() => this.SetSliderValue(this.AmountSlider.value - 1f);

    private void SetSliderValue(float newValue)
    {
      this.AmountSlider.value = newValue;
      this.AmountSliderNum.text = ((int) this.AmountSlider.value).ToString();
      if ((double) this.AmountSlider.value <= (double) this.AmountSlider.minValue)
        ((Selectable) this.DecrementButton).interactable = false;
      else
        ((Selectable) this.DecrementButton).interactable = true;
      if ((double) this.AmountSlider.value >= (double) this.AmountSlider.maxValue)
        ((Selectable) this.IncrementButton).interactable = false;
      else
        ((Selectable) this.IncrementButton).interactable = true;
      if ((double) this.AmountSlider.maxValue == 1.0 && (double) this.AmountSlider.minValue == 1.0)
        ((Selectable) this.AmountSlider).interactable = false;
      else
        ((Selectable) this.AmountSlider).interactable = true;
      int num = ShopData.GetBuyPrice((ShopItem) this.mShopitem) * (int) this.AmountSlider.value;
      this.LimitedItemPriceText.text = this.mShopitem.saleType != ESaleType.Gold ? num.ToString() : CurrencyBitmapText.CreateFormatedText(num.ToString());
    }

    private void OnSliderValueChanged(float newValue) => this.SetSliderValue(newValue);

    public void UpdateBuyAmount()
    {
      if (this.mEnabledSlider)
        GlobalVars.ShopBuyAmount = (int) this.AmountSlider.value;
      else
        GlobalVars.ShopBuyAmount = 1;
    }
  }
}
