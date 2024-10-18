// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.FlowWithPurchaseKit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.Response;
using System;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public abstract class FlowWithPurchaseKit : IPurchaseFlowImpl, IPurchaseListener
  {
    protected readonly PurchaseHandler handler;

    public FlowWithPurchaseKit(PurchaseHandler handler)
    {
      this.handler = handler;
    }

    public virtual void Init(string[] productIds)
    {
      GameObject gameObject = NativeRootObject.Instance.gameObject;
      string[] productIds1 = productIds;
      GameObject serviceNode = gameObject;
      // ISSUE: reference to a compiler-generated field
      if (FlowWithPurchaseKit.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        FlowWithPurchaseKit.\u003C\u003Ef__mg\u0024cache0 = new PurchaseKit.Logger(PurchaseHandler.Log);
      }
      // ISSUE: reference to a compiler-generated field
      PurchaseKit.Logger fMgCache0 = FlowWithPurchaseKit.\u003C\u003Ef__mg\u0024cache0;
      IntPtr nativeLogger = new IntPtr();
      PurchaseKit.Init(productIds1, (IPurchaseListener) this, serviceNode, fMgCache0, nativeLogger);
    }

    public virtual void Resume()
    {
      PurchaseKit.Resume();
    }

    public virtual bool Purchase(ProductInfo product)
    {
      return PurchaseKit.Purchase(product.ID);
    }

    public virtual bool Confirmed()
    {
      return false;
    }

    public virtual void UpdateProducts(string[] productIds)
    {
      PurchaseKit.UpdateProducts(productIds);
    }

    public virtual void Consume(string transactionId)
    {
      PurchaseKit.Consume(transactionId);
    }

    private static ResultCode GetResultCode(int resultCode)
    {
      switch (resultCode)
      {
        case 0:
          return ResultCode.Succeeded;
        case 2:
          return ResultCode.Unavailabled;
        default:
          if (resultCode == 16)
            return ResultCode.Canceled;
          if (resultCode == 17)
            return ResultCode.AlreadyOwned;
          return resultCode == 32 ? ResultCode.Deferred : ResultCode.Failed;
      }
    }

    public virtual void OnInitResult(int resultCode)
    {
      this.handler.OnInitResult(FlowWithPurchaseKit.GetResultCode(resultCode));
    }

    public virtual void OnProductResult(int resultCode, PurchaseKit.ProductResponse response)
    {
      if (resultCode == 0 && response != null)
      {
        ProductInfo[] products = new ProductInfo[response.Values.Length];
        for (int index = 0; index < response.Values.Length; ++index)
        {
          PurchaseKit.ProductData productData = response.Values[index];
          products[index] = new ProductInfo(productData.ID, productData.LocalizedTitle, productData.LocalizedDescription, productData.LocalizedPrice, productData.Currency, (float) productData.Price);
        }
        this.handler.OnProductResult(ResultCode.Succeeded, products);
      }
      else
        this.handler.OnProductResult(FlowWithPurchaseKit.GetResultCode(resultCode), (ProductInfo[]) null);
    }

    public virtual void OnPurchaseResult(int resultCode, PurchaseKit.PurchaseResponse response)
    {
      if (resultCode == 0 && response != null)
        this.CreateFulfillmentTask(response);
      else
        this.handler.OnPurchaseResult(FlowWithPurchaseKit.GetResultCode(resultCode), (FulfillmentResult) null);
    }

    protected void OnFulfillmentResponse(Fulfillment response, IErrorResponse error)
    {
      if (error != null)
        this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
      else
        this.handler.OnPurchaseResult(ResultCode.Succeeded, response.Result);
    }

    protected abstract IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response);
  }
}
