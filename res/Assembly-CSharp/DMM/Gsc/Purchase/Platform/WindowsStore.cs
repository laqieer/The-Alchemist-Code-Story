// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.WindowsStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using Gsc.Network;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Gsc.Purchase.Platform
{
  public class WindowsStore : FlowWithPurchaseKit
  {
    public WindowsStore(PurchaseHandler handler)
      : base(handler)
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      List<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t> purchaseDataList = new List<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t>();
      foreach (PurchaseKit.PurchaseData purchaseData in response.Values)
      {
        PurchaseKit.PurchaseData purchase = purchaseData;
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>((Func<ProductInfo, bool>) (x => x.ID == purchase.ProductId)).FirstOrDefault<ProductInfo>();
        purchaseDataList.Add(new Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t(productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, purchase.Data0, purchase.ID));
      }
      WebTask<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment, Gsc.Purchase.API.Response.Fulfillment> webTask = new Gsc.Purchase.API.PAS.WindowsStore.Fulfillment(Session.DefaultSession.DeviceID, purchaseDataList).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
      webTask.OnResponse(new VoidCallbackWithError<Gsc.Purchase.API.Response.Fulfillment>(((FlowWithPurchaseKit) this).OnFulfillmentResponse));
      return (IWebTask) webTask;
    }
  }
}
