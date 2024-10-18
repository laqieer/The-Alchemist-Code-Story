// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.AmazonAppStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.Gacct.AmazonAppStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public class AmazonAppStore : FlowWithPurchaseKit
  {
    private bool done;

    public AmazonAppStore(PurchaseHandler handler)
      : base(handler)
    {
    }

    [DebuggerHidden]
    public IEnumerator InitProducts(string[] productIds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Gsc.Purchase.Platform.AmazonAppStore.\u003CInitProducts\u003Ec__IteratorA()
      {
        productIds = productIds,
        \u003C\u0024\u003EproductIds = productIds,
        \u003C\u003Ef__this = this
      };
    }

    public override void Init(string[] productIds)
    {
      this.done = false;
      DebugUtility.Log("HERE");
      new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call("runOnUiThread", new object[1]
      {
        (object) (AndroidJavaRunnable) (() =>
        {
          UnityEngine.Debug.Log((object) "HERE2");
          PurchaseKit.SetAndroidService("jp.co.gu3.purchasekit.services.amazon.AmazonAppStoreService");
          this.done = true;
        })
      });
      RootObject.Instance.StartCoroutine(this.InitProducts(productIds));
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      int count = 0;
      bool hasError = false;
      IWebTask webTask1 = (IWebTask) null;
      FulfillmentResult.OrderInfo[] succeededTransactions = new FulfillmentResult.OrderInfo[response.Values.Length];
      for (int index = 0; index < succeededTransactions.Length; ++index)
      {
        PurchaseKit.PurchaseData purchaseData = response.Values[index];
        succeededTransactions[index] = new FulfillmentResult.OrderInfo(0, 0, purchaseData.ProductId, purchaseData.ID);
      }
      foreach (PurchaseKit.PurchaseData purchaseData in response.Values)
      {
        PurchaseKit.PurchaseData purchase = purchaseData;
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>((Func<ProductInfo, bool>) (x => x.ID == purchase.ProductId)).FirstOrDefault<ProductInfo>();
        WebTask<Verify, Verify.Response> webTask2 = new Verify(response.Meta.Data0, productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, purchase.ID)
        {
          IsBundle = purchase.ProductId.Contains("bundle")
        }.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
        webTask1 = (IWebTask) webTask2;
        webTask2.OnResponse((VoidCallbackWithError<Verify.Response>) ((r, e) =>
        {
          if (e != null)
            hasError = true;
          if (++count < response.Values.Length)
            return;
          if (hasError)
            this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
          else
            this.handler.OnPurchaseResult(ResultCode.Succeeded, new FulfillmentResult(r.CurrentFreeCoin, r.CurrentPaidCoin, r.CurrentCommonCoin, ((IEnumerable<FulfillmentResult.OrderInfo>) succeededTransactions).ToArray<FulfillmentResult.OrderInfo>(), new FulfillmentResult.OrderInfo[0]));
        }));
      }
      return webTask1;
    }
  }
}
