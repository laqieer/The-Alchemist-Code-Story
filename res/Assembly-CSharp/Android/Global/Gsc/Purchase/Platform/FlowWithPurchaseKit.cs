// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.FlowWithPurchaseKit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.Response;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public abstract class FlowWithPurchaseKit : IPurchaseFlowImpl, IPurchaseListener
  {
    private readonly int SKU_LIMIT = 20;
    protected readonly PurchaseHandler handler;
    private Queue<string[]> ProcessSKU;
    private List<ProductInfo> productList;

    public FlowWithPurchaseKit(PurchaseHandler handler)
    {
      this.handler = handler;
      this.ProcessSKU = new Queue<string[]>();
      this.productList = new List<ProductInfo>();
    }

    public virtual void SKUSplitter(string[] productIds)
    {
      int sourceIndex = 0;
      while (sourceIndex < productIds.Length)
      {
        int length = productIds.Length - sourceIndex;
        if (length > this.SKU_LIMIT)
          length = this.SKU_LIMIT;
        string[] strArray = new string[length];
        Array.Copy((Array) productIds, sourceIndex, (Array) strArray, 0, length);
        this.ProcessSKU.Enqueue(strArray);
        sourceIndex += this.SKU_LIMIT;
      }
    }

    public virtual void Init(string[] productIds)
    {
      GameObject gameObject = NativeRootObject.Instance.gameObject;
      DebugUtility.Log("Gsc.Purchase.Platform.FlowWithPurchaseKit: productIds.Length" + (object) productIds.Length);
      this.SKUSplitter(productIds);
      PurchaseKit.Init(this.ProcessSKU.Dequeue(), (IPurchaseListener) this, gameObject, new PurchaseKit.Logger(PurchaseHandler.Log), new IntPtr());
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
      if (this.ProcessSKU.Count > 0)
        return;
      this.SKUSplitter(productIds);
      PurchaseKit.UpdateProducts(this.ProcessSKU.Dequeue());
    }

    public virtual void Consume(string transactionId)
    {
      PurchaseKit.Consume(transactionId);
    }

    private static ResultCode GetResultCode(int resultCode)
    {
      int num = resultCode;
      switch (num)
      {
        case 0:
          return ResultCode.Succeeded;
        case 2:
          return ResultCode.Unavailabled;
        default:
          if (num == 16)
            return ResultCode.Canceled;
          if (num == 17)
            return ResultCode.AlreadyOwned;
          return num == 32 ? ResultCode.Deferred : ResultCode.Failed;
      }
    }

    public virtual void OnInitResult(int resultCode)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnInitResult: " + (object) resultCode);
      this.handler.OnInitResult(FlowWithPurchaseKit.GetResultCode(resultCode));
    }

    public virtual void OnProductResult(int resultCode, PurchaseKit.ProductResponse response)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnProductResult: " + (object) resultCode);
      if (resultCode == 0 && response != null)
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnProductResult: response.values len " + (object) response.Values.Length);
        for (int index = 0; index < response.Values.Length; ++index)
        {
          PurchaseKit.ProductData productData = response.Values[index];
          this.productList.Add(new ProductInfo(productData.ID, productData.LocalizedTitle, productData.LocalizedDescription, productData.LocalizedPrice, productData.Currency, (float) productData.Price));
          DebugUtility.Log("FlowWithPurchaseKit::OnProductResult: response[" + (object) index + "].ID: " + productData.ID);
        }
        if (this.ProcessSKU.Count > 0)
        {
          PurchaseKit.UpdateProducts(this.ProcessSKU.Dequeue());
        }
        else
        {
          this.handler.OnProductResult(ResultCode.Succeeded, this.productList.ToArray());
          this.productList.Clear();
        }
      }
      else
        this.handler.OnProductResult(FlowWithPurchaseKit.GetResultCode(resultCode), (ProductInfo[]) null);
    }

    public virtual void OnPurchaseResult(int resultCode, PurchaseKit.PurchaseResponse response)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnPurchaseResult: " + (object) resultCode);
      if (resultCode == 0 && response != null)
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnPurchaseResult: " + (object) response.Values.Length);
        this.CreateFulfillmentTask(response);
      }
      else
        this.handler.OnPurchaseResult(FlowWithPurchaseKit.GetResultCode(resultCode), (FulfillmentResult) null);
    }

    protected void OnFulfillmentResponse(Fulfillment response, IErrorResponse error)
    {
      if (error != null)
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnFulfillmentResponse: ERROR");
        this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
      }
      else
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnFulfillmentResponse: " + (object) response.Result);
        this.handler.OnPurchaseResult(ResultCode.Succeeded, response.Result);
      }
    }

    protected abstract IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response);
  }
}
