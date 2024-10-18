// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.GooglePlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Purchase.API.Gacct.GooglePlay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.Platform
{
  public class GooglePlay : FlowWithPurchaseKit
  {
    public GooglePlay(PurchaseHandler handler)
      : base(handler)
    {
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
        WebTask<Verify, Verify.Response> webTask2 = new Verify(productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, purchase.Data1, purchase.Data0)
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
