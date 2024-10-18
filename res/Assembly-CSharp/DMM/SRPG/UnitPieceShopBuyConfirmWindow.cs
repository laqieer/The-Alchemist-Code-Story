// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopBuyConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Slider Plus", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Slider Minus", FlowNode.PinTypes.Input, 11)]
  public class UnitPieceShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private const int PINID_REFRESH = 1;
    private const int PINID_SLIDER_PLUS = 10;
    private const int PINID_SLIDER_MINUS = 11;
    [SerializeField]
    private Text m_SoldText;
    [SerializeField]
    private Text m_DescriptionText;
    [SerializeField]
    private GameObject HasCount;
    [HeaderBar("▼アイコン表示用オブジェクト")]
    [SerializeField]
    private GameObject m_ItemIconRoot;
    [HeaderBar("▼まとめ買い用")]
    [SerializeField]
    private GameObject m_AmountSliderHolder;
    [SerializeField]
    private Slider m_AmountSlider;
    [SerializeField]
    private Text m_AmountNum;
    [SerializeField]
    private Button m_IncrementButton;
    [SerializeField]
    private Button m_DecrementButton;
    [SerializeField]
    private Text m_ItemPriceText;
    [HeaderBar("▼所持コイン")]
    [SerializeField]
    private GameObject HasCoin;
    private bool mEnabledSlider;
    private UnitPieceShopItem mShopitem;

    private void Start() => this.Refresh();

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
      this.mShopitem = GlobalVars.BuyUnitPieceShopItem;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SoldText, (UnityEngine.Object) null))
        return;
      string format;
      if (this.mShopitem.HasNextStep)
      {
        format = LocalizedText.Get("sys.SHOP_UNITPIECE_NEXT_INCREASE");
        ((Graphic) this.m_SoldText).color = Color.yellow;
      }
      else
      {
        format = LocalizedText.Get("sys.SHOP_UNITPIECE_NEXT_SOLDOUT");
        ((Graphic) this.m_SoldText).color = Color.red;
      }
      this.m_SoldText.text = string.Format(format, (object) this.mShopitem.RemainCount);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_AmountSliderHolder, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_AmountSlider, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_AmountNum, (UnityEngine.Object) null))
        return;
      this.mEnabledSlider = false;
      UnitPieceShopParam currentParam = UnitPieceShopParam.GetCurrentParam();
      if (currentParam == null)
        return;
      string costIname = currentParam.CostIname;
      if (this.mShopitem.RemainCount > 1)
      {
        this.mEnabledSlider = true;
        this.m_AmountSliderHolder.SetActive(true);
        int num1 = MonoSingleton<GameManager>.Instance.Player.EventCoinNum(costIname);
        int val1 = 0;
        if (this.mShopitem.CostNum > 0)
        {
          for (int index = 1; index <= this.mShopitem.RemainCount && this.mShopitem.CostNum * index <= num1; ++index)
            ++val1;
        }
        int num2 = Math.Max(val1, 1);
        this.m_AmountSlider.minValue = 1f;
        this.m_AmountSlider.maxValue = (float) num2;
        this.SetSliderValue(1);
        // ISSUE: method pointer
        ((UnityEvent<float>) this.m_AmountSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSliderValueChanged)));
      }
      else
      {
        this.mEnabledSlider = false;
        this.m_AmountSliderHolder.SetActive(false);
        this.SetSliderValue(1);
      }
      DataSource.Bind<EventCoinData>(this.HasCoin, MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(costIname))));
      DataSource.Bind<UnitPieceShopItem>(((Component) this).gameObject, this.mShopitem);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_DescriptionText, (UnityEngine.Object) null))
        return;
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.IName);
      if (itemDataByItemId != null)
        DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.IName);
      if (itemParam == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      this.m_DescriptionText.text = itemParam.Expr;
      if (itemParam.type == EItemType.Rune)
        GameUtility.SetGameObjectActive(this.HasCount, false);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void IncrementSliderValue() => this.SetSliderValue((int) this.m_AmountSlider.value + 1);

    private void DecrementSliderValue() => this.SetSliderValue((int) this.m_AmountSlider.value - 1);

    private void SetSliderValue(int newValue)
    {
      this.m_AmountSlider.value = (float) newValue;
      this.m_AmountNum.text = newValue.ToString();
      ((Selectable) this.m_IncrementButton).interactable = (double) this.m_AmountSlider.value < (double) this.m_AmountSlider.maxValue;
      ((Selectable) this.m_DecrementButton).interactable = (double) this.m_AmountSlider.value > (double) this.m_AmountSlider.minValue;
      ((Selectable) this.m_AmountSlider).interactable = (double) this.m_AmountSlider.maxValue > (double) this.m_AmountSlider.minValue;
      this.m_ItemPriceText.text = (this.mShopitem.CostNum * newValue).ToString();
    }

    private void OnSliderValueChanged(float newValue) => this.SetSliderValue((int) newValue);

    public void UpdateBuyAmount()
    {
      if (this.mEnabledSlider)
        GlobalVars.ShopBuyAmount = (int) this.m_AmountSlider.value;
      else
        GlobalVars.ShopBuyAmount = 1;
    }
  }
}
