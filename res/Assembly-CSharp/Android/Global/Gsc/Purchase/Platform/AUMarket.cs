// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.AUMarket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Purchase.API.Gacct.AUMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public class AUMarket : FlowWithPurchaseKit
  {
    public const int RESULT_AUMARKET_OVER_CREDIT_LIMIT = 49;

    public AUMarket(PurchaseHandler handler)
      : base(handler)
    {
    }

    public override void Init(string[] productIds)
    {
      new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call("runOnUiThread", new object[1]
      {
        (object) (AndroidJavaRunnable) (() =>
        {
          PurchaseKit.SetAndroidService("jp.co.gu3.purchasekit.services.aumarket.AUMarketService");
          this.UpdateProducts(productIds);
        })
      });
    }

    public override void UpdateProducts(string[] productIds)
    {
      new Gsc.Purchase.API.App.ProductList().ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel).OnResponse((VoidCallbackWithError<Gsc.Purchase.API.Response.ProductList>) ((r, e) =>
      {
        if (e != null)
        {
          if (!this.handler.initialized)
            this.handler.OnInitResult(ResultCode.Failed);
          else
            this.handler.OnProductResult(ResultCode.Failed, (ProductInfo[]) null);
        }
        else
        {
          Gsc.Purchase.API.Response.ProductList.ProductData_t[] array = ((IEnumerable<Gsc.Purchase.API.Response.ProductList.ProductData_t>) r.Products).Where<Gsc.Purchase.API.Response.ProductList.ProductData_t>((Func<Gsc.Purchase.API.Response.ProductList.ProductData_t, bool>) (x =>
          {
            if (x.Currency == "JPY")
              return Array.IndexOf<string>(productIds, x.ProductId) >= 0;
            return false;
          })).ToArray<Gsc.Purchase.API.Response.ProductList.ProductData_t>();
          ProductInfo[] products = new ProductInfo[array.Length];
          for (int index = 0; index < array.Length; ++index)
          {
            Gsc.Purchase.API.Response.ProductList.ProductData_t productDataT = array[index];
            products[index] = new ProductInfo(productDataT.ProductId, productDataT.Name, productDataT.Description, productDataT.LocalizedPrice, productDataT.Currency, productDataT.Price);
          }
          this.handler.OnProductResult(ResultCode.Succeeded, products);
          if (this.handler.initialized)
            return;
          base.Init((string[]) null);
        }
      }));
    }

    public override void Resume()
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      List<Verify.PurchaseData_t> purchaseDataList = new List<Verify.PurchaseData_t>(response.Values.Length);
      foreach (PurchaseKit.PurchaseData purchaseData in response.Values)
      {
        PurchaseKit.PurchaseData purchase = purchaseData;
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>((Func<ProductInfo, bool>) (x => x.ID == purchase.ProductId)).FirstOrDefault<ProductInfo>();
        purchaseDataList.Add(new Verify.PurchaseData_t(productInfo.CurrencyCode, productInfo.Price, purchase.ID));
      }
      WebTask<Verify, Verify.Response> webTask = new Verify(response.Meta.Data1, response.Meta.Data0, purchaseDataList).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
      webTask.OnResponse((VoidCallbackWithError<Verify.Response>) ((r, e) =>
      {
        if (e != null)
        {
          this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
        }
        else
        {
          FulfillmentResult.OrderInfo[] succeededTransactions = new FulfillmentResult.OrderInfo[r.SuccessTransactionIds.Length];
          FulfillmentResult.OrderInfo[] duplicatedTransactions = new FulfillmentResult.OrderInfo[r.DuplicatedTransactionIds.Length];
          for (int index = 0; index < r.SuccessTransactionIds.Length; ++index)
          {
            string tx = r.SuccessTransactionIds[index];
            PurchaseKit.PurchaseData purchaseData = ((IEnumerable<PurchaseKit.PurchaseData>) response.Values).Where<PurchaseKit.PurchaseData>((Func<PurchaseKit.PurchaseData, bool>) (x => x.ID == tx)).FirstOrDefault<PurchaseKit.PurchaseData>();
            succeededTransactions[index] = new FulfillmentResult.OrderInfo(0, 0, purchaseData == null ? (string) null : purchaseData.ProductId, tx);
          }
          for (int index = 0; index < r.DuplicatedTransactionIds.Length; ++index)
          {
            string tx = r.DuplicatedTransactionIds[index];
            PurchaseKit.PurchaseData purchaseData = ((IEnumerable<PurchaseKit.PurchaseData>) response.Values).Where<PurchaseKit.PurchaseData>((Func<PurchaseKit.PurchaseData, bool>) (x => x.ID == tx)).FirstOrDefault<PurchaseKit.PurchaseData>();
            duplicatedTransactions[index] = new FulfillmentResult.OrderInfo(0, 0, purchaseData == null ? (string) null : purchaseData.ProductId, tx);
          }
          this.handler.OnPurchaseResult(ResultCode.Succeeded, new FulfillmentResult(r.CurrentFreeCoin, r.CurrentPaidCoin, r.CurrentCommonCoin, succeededTransactions, duplicatedTransactions));
        }
      }));
      return (IWebTask) webTask;
    }
  }
}
