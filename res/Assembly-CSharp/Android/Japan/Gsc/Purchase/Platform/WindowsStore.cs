// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.WindowsStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth;
using Gsc.Network;
using System;
using System.Collections.Generic;
using System.Linq;

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
