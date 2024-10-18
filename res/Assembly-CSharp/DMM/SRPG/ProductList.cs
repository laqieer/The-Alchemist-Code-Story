// Decompiled with JetBrains decompiler
// Type: SRPG.ProductList
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
  [AddComponentMenu("Payment/ProductList")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "選択された", FlowNode.PinTypes.Output, 0)]
  public class ProductList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public PurchaseListItem ItemTemplate;
    [Description("リスト限定アイテムとして使用するゲームオブジェクト")]
    public PurchaseListItem ItemLimitedTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("リストアイテム(VIP)として使用するゲームオブジェクト")]
    public PurchaseListItem ItemVipTemplate;
    [Description("リストアイテム(PREMIUM)として使用するゲームオブジェクト")]
    public PurchaseListItem ItemPremiumTemplate;
    [Description("リストアイテム(機能売り)として使用するゲームオブジェクト")]
    [SerializeField]
    private PurchaseListItem ItemExpansionTemplate;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private ProductList.TemplateItem[] mTemplateItems;
    private GameObject mDetailInfo;
    private List<PurchaseListItem> mParchaseListItem = new List<PurchaseListItem>();
    private List<int> coinNumList;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      Transform transform = ((Component) this).transform;
      for (int index = 0; index < transform.childCount; ++index)
      {
        Transform child = transform.GetChild(index);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) child, (UnityEngine.Object) null) && ((Component) child).gameObject.activeInHierarchy)
          ((Component) child).gameObject.SetActive(false);
      }
      this.RefreshItems(true);
    }

    public void Refresh()
    {
      this.RefreshItems(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        return;
      ListExtras component = ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.zero;
    }

    private void RefreshItems(bool is_start)
    {
      Transform transform = ((Component) this).transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) child, (UnityEngine.Object) null) && ((Component) child).gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) child).gameObject);
      }
      PaymentManager.Product[] product1 = BuyCoinManager.Instance.GetProduct();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) || product1 == null)
        return;
      PurchaseListItem purchaseListItem1 = (PurchaseListItem) null;
      PurchaseListItem purchaseListItem2 = (PurchaseListItem) null;
      this.mParchaseListItem.Clear();
      List<PurchaseListItem> purchaseListItemList = new List<PurchaseListItem>();
      for (int index = 0; index < product1.Length; ++index)
      {
        PaymentManager.Product product2 = product1[index];
        PurchaseListItem purchaseListItem3;
        if (product2.productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PremiumProduct)
        {
          purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemPremiumTemplate);
          purchaseListItem1 = purchaseListItem3;
        }
        else if (product2.productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.VipCardProduct)
        {
          purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemVipTemplate);
          purchaseListItem2 = purchaseListItem3;
        }
        else
        {
          switch (BuyCoinManager.Instance.mNowSelectTab)
          {
            case BuyCoinManager.BuyCoinShopType.COIN:
              purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemTemplate);
              break;
            case BuyCoinManager.BuyCoinShopType.EXPANSION:
              BuyCoinProductParam param = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(product2.ID);
              if (param != null && MonoSingleton<GameManager>.Instance.MasterParam.ExpansionPurchaseParams.Find((Predicate<ExpansionPurchaseParam>) (x => x.BuyCoinProduct == param.Iname)) != null)
              {
                purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemExpansionTemplate);
                break;
              }
              continue;
            case BuyCoinManager.BuyCoinShopType.FGG:
              BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(product2.ID);
              if (coinProductParam != null)
              {
                PurchaseListItem templateItem = this.FindTemplateItem(coinProductParam.TemplateName);
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) templateItem, (UnityEngine.Object) null))
                {
                  purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(templateItem);
                  break;
                }
                continue;
              }
              continue;
            default:
              purchaseListItem3 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemLimitedTemplate);
              break;
          }
          purchaseListItemList.Add(purchaseListItem3);
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) purchaseListItem3, (UnityEngine.Object) null))
          return;
        ((UnityEngine.Object) purchaseListItem3).hideFlags = (HideFlags) 52;
        purchaseListItem3.Init(product2);
        DataSource.Bind<PaymentManager.Product>(((Component) purchaseListItem3).gameObject, product2);
        if (!is_start)
        {
          ListItemEvents component = ((Component) purchaseListItem3).GetComponent<ListItemEvents>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          }
        }
        ((Component) purchaseListItem3).transform.SetParent(transform, false);
        ((Component) purchaseListItem3).gameObject.SetActive(true);
        this.mParchaseListItem.Add(purchaseListItem3);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) purchaseListItem2, (UnityEngine.Object) null))
        ((Component) purchaseListItem2).transform.SetAsFirstSibling();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) purchaseListItem1, (UnityEngine.Object) null))
        ((Component) purchaseListItem1).transform.SetAsFirstSibling();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) purchaseListItem1, (UnityEngine.Object) null) && purchaseListItem1.IsPurchased)
        ((Component) purchaseListItem1).transform.SetAsLastSibling();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) purchaseListItem2, (UnityEngine.Object) null) && purchaseListItem2.IsPurchased)
        ((Component) purchaseListItem2).transform.SetAsLastSibling();
      if (BuyCoinManager.Instance.mNowSelectTab == BuyCoinManager.BuyCoinShopType.EXPANSION)
      {
        foreach (PurchaseListItem purchaseListItem4 in this.mParchaseListItem)
        {
          if (purchaseListItem4.IsPurchased)
            ((Component) purchaseListItem4).transform.SetAsLastSibling();
        }
        foreach (ExpansionPurchaseData expansion in MonoSingleton<GameManager>.Instance.Player.Expansions)
        {
          if (expansion.param != null)
          {
            BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(expansion.param.BuyCoinProduct);
            if (coinProductParam != null && !coinProductParam.IsShopOpen())
            {
              PaymentManager.Product product = BuyCoinManager.Instance.GetProductByProductId(coinProductParam.ProductId);
              if (product != null && !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mParchaseListItem.Find((Predicate<PurchaseListItem>) (x => x.ProductID == product))))
              {
                PurchaseListItem purchaseListItem5 = UnityEngine.Object.Instantiate<PurchaseListItem>(this.ItemExpansionTemplate);
                purchaseListItemList.Add(purchaseListItem5);
                ((UnityEngine.Object) purchaseListItem5).hideFlags = (HideFlags) 52;
                purchaseListItem5.Init(product);
                DataSource.Bind<PaymentManager.Product>(((Component) purchaseListItem5).gameObject, product);
                ((Component) purchaseListItem5).transform.SetParent(transform, false);
                ((Component) purchaseListItem5).gameObject.SetActive(true);
                this.mParchaseListItem.Add(purchaseListItem5);
              }
            }
          }
        }
      }
      if (this.coinNumList == null)
      {
        PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
        this.coinNumList = new List<int>();
        for (int index = 0; index < products.Length; ++index)
        {
          if (!this.coinNumList.Contains(products[index].numPaid))
            this.coinNumList.Add(products[index].numPaid);
        }
        this.coinNumList.Sort();
      }
      for (int index = 0; index < purchaseListItemList.Count; ++index)
      {
        BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(purchaseListItemList[index].ProductID.ID);
        if (coinProductParam != null)
          purchaseListItemList[index].SetSpriteId(coinProductParam.ImageArrayIndex);
        if (!BuyCoinManager.Instance.GetProductBuyConfirm(purchaseListItemList[index].ProductID))
          ((Component) purchaseListItemList[index]).transform.SetAsLastSibling();
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (!BuyCoinManager.Instance.GetProductBuyConfirm(dataOfClass))
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
      GlobalVars.SelectedProductIname = dataOfClass.ID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<PaymentManager.Product>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }

    private PurchaseListItem FindTemplateItem(string name)
    {
      if (string.IsNullOrEmpty(name) || this.mTemplateItems == null)
        return (PurchaseListItem) null;
      foreach (ProductList.TemplateItem mTemplateItem in this.mTemplateItems)
      {
        if (mTemplateItem.name == name)
          return mTemplateItem.obj;
      }
      return (PurchaseListItem) null;
    }

    [Serializable]
    public struct TemplateItem
    {
      public string name;
      public PurchaseListItem obj;
    }
  }
}
