// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinManager
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
  [FlowNode.Pin(1, "InitTabCoin", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "InitTabExpansion", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "InitTabLimited", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "InitTabCollabo", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "InitTabFgG", FlowNode.PinTypes.Input, 5)]
  public class BuyCoinManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT_TAB_COIN = 1;
    public const int PIN_INPUT_INIT_TAB_EXPANSION = 2;
    public const int PIN_INPUT_INIT_TAB_LIMITED = 3;
    public const int PIN_INPUT_INIT_TAB_COLLABO = 4;
    public const int PIN_INPUT_INIT_TAB_FGG = 5;
    [SerializeField]
    private GameObject mTabCoin;
    [SerializeField]
    private GameObject mTabLimited;
    [SerializeField]
    private GameObject mTabCollabo;
    [SerializeField]
    private GameObject mTabExpansion;
    [SerializeField]
    private GameObject mTabFgG;
    [SerializeField]
    private GameObject mPeriod;
    [SerializeField]
    private Text mPeriodText;
    private static BuyCoinManager mInstance;
    public BuyCoinManager.BuyCoinShopType mNowSelectTab;

    public static BuyCoinManager Instance => BuyCoinManager.mInstance;

    private void Awake() => BuyCoinManager.mInstance = this;

    private void OnDestroy() => BuyCoinManager.mInstance = (BuyCoinManager) null;

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          this.SelectedToggleTab(BuyCoinManager.BuyCoinShopType.COIN);
          break;
        case 2:
          this.Init();
          this.SelectedToggleTab(BuyCoinManager.BuyCoinShopType.EXPANSION);
          break;
        case 3:
          this.Init();
          this.SelectedToggleTab(BuyCoinManager.BuyCoinShopType.LIMITED);
          break;
        case 4:
          this.Init();
          this.SelectedToggleTab(BuyCoinManager.BuyCoinShopType.COLLABO);
          break;
        case 5:
          this.Init();
          this.SelectedToggleTab(BuyCoinManager.BuyCoinShopType.FGG);
          break;
      }
    }

    private void Init()
    {
      GameUtility.SetGameObjectActive(this.mPeriod, false);
      GameUtility.SetGameObjectActive(this.mTabCoin, true);
      GameUtility.SetGameObjectActive(this.mTabLimited, false);
      GameUtility.SetGameObjectActive(this.mTabCollabo, false);
      GameUtility.SetGameObjectActive(this.mTabExpansion, false);
      GameUtility.SetGameObjectActive(this.mTabFgG, false);
      List<BuyCoinShopParam> buyCoinShopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinShopParam();
      if (buyCoinShopParam == null)
        return;
      for (int index = 0; index < buyCoinShopParam.Count; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.MasterParam.IsBuyCoinShopOpen(buyCoinShopParam[index]) && (FlowNode_IsFgGBuy.IsFgGBuy() || buyCoinShopParam[index].ShopType != BuyCoinManager.BuyCoinShopType.FGG))
        {
          GameObject tabObject = this.GetTabObject(buyCoinShopParam[index].ShopType);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tabObject, (UnityEngine.Object) null))
          {
            Text componentInChildren = tabObject.GetComponentInChildren<Text>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) && !string.IsNullOrEmpty(buyCoinShopParam[index].DisplayName))
              componentInChildren.text = LocalizedText.Get(buyCoinShopParam[index].DisplayName);
            tabObject.SetActive(true);
            DataSource.Bind<BuyCoinShopParam>(tabObject, buyCoinShopParam[index]);
          }
        }
      }
    }

    private GameObject GetTabObject(BuyCoinManager.BuyCoinShopType type)
    {
      switch (type)
      {
        case BuyCoinManager.BuyCoinShopType.COIN:
          return this.mTabCoin;
        case BuyCoinManager.BuyCoinShopType.LIMITED:
          return this.mTabLimited;
        case BuyCoinManager.BuyCoinShopType.COLLABO:
          return this.mTabCollabo;
        case BuyCoinManager.BuyCoinShopType.EXPANSION:
          return this.mTabExpansion;
        case BuyCoinManager.BuyCoinShopType.FGG:
          return this.mTabFgG;
        default:
          return (GameObject) null;
      }
    }

    private void SelectedToggleTab(BuyCoinManager.BuyCoinShopType type)
    {
      this.mNowSelectTab = type;
      GameObject tabObject = this.GetTabObject(type);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tabObject, (UnityEngine.Object) null))
        return;
      Toggle component = tabObject.GetComponent<Toggle>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.isOn = true;
    }

    public void OnClickTab(GameObject go)
    {
      BuyCoinShopParam dataOfClass = DataSource.FindDataOfClass<BuyCoinShopParam>(go, (BuyCoinShopParam) null);
      if (dataOfClass == null)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriod, (UnityEngine.Object) null))
          return;
        this.mPeriod.SetActive(false);
      }
      else
        this.RefreshShopList(dataOfClass);
    }

    private void RefreshShopList(BuyCoinShopParam _param)
    {
      this.mNowSelectTab = _param.ShopType;
      if (!_param.AlwaysOpen)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriod, (UnityEngine.Object) null))
        {
          this.mPeriod.SetActive(true);
          DateTime dateTime1 = TimeManager.FromUnixTime(_param.BeginAt);
          DateTime dateTime2 = TimeManager.FromUnixTime(_param.EndAt);
          string str1 = LocalizedText.Get("sys.BUYCOIN_PERIODTIME", (object) dateTime1.Year, (object) dateTime1.Month, (object) dateTime1.Day, (object) dateTime1.Hour, (object) dateTime1.Minute);
          string str2 = LocalizedText.Get("sys.BUYCOIN_PERIODTIME", (object) dateTime2.Year, (object) dateTime2.Month, (object) dateTime2.Day, (object) dateTime2.Hour, (object) dateTime2.Minute);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriodText, (UnityEngine.Object) null))
            this.mPeriodText.text = LocalizedText.Get("sys.BUYCOIN_PERIOD", (object) str1, (object) str2);
        }
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriod, (UnityEngine.Object) null))
        this.mPeriod.SetActive(false);
      this.GetProduct();
      GlobalEvent.Invoke("CHANGE_TAB", (object) this);
    }

    public PaymentManager.Product[] GetProduct()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
      List<PaymentManager.Product> productList = new List<PaymentManager.Product>();
      if (products == null)
        return products;
      BuyCoinShopParam buyCoinShopParam = masterParam.GetBuyCoinShopParam(this.mNowSelectTab);
      if (buyCoinShopParam != null)
      {
        for (int index = 0; index < products.Length; ++index)
        {
          BuyCoinProductParam coinProductParam = masterParam.GetBuyCoinProductParam(products[index].ID);
          if (coinProductParam != null && coinProductParam.ShopId == buyCoinShopParam.ShopId)
            productList.Add(products[index]);
        }
      }
      return productList.ToArray();
    }

    public PaymentManager.Product GetProductByProductId(string _id)
    {
      PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
      PaymentManager.Product productByProductId = (PaymentManager.Product) null;
      if (products != null)
      {
        foreach (PaymentManager.Product product in products)
        {
          if (product.productID == _id)
          {
            productByProductId = product;
            break;
          }
        }
      }
      return productByProductId;
    }

    public bool GetProductBuyConfirm(PaymentManager.Product product)
    {
      return product != null && (product.ID == null || product.remainNum != 0);
    }

    public enum BuyCoinShopType
    {
      COIN,
      LIMITED,
      COLLABO,
      EXPANSION,
      FGG,
      ALL,
    }

    public enum PremiumRewadType
    {
      Item,
      Gold,
      Coin,
      Artifact,
      ConceptCard,
      Unit,
    }

    public enum PremiumRestrictionType
    {
      None,
      AllBuy,
      DayBuy,
    }
  }
}
