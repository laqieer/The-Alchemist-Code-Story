﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Auth;
using SRPG;
using System.Collections.Generic;
using UnityEngine;

namespace Gsc.Purchase
{
  public class PurchaseManager : MonoSingleton<PurchaseManager>
  {
    public PurchaseManager.OnProductsSchedule onProductsSchedule { get; set; }

    private List<string> GetProductList(List<ProductParam> ProductMasters)
    {
      List<string> stringList = new List<string>();
      foreach (ProductParam productMaster in ProductMasters)
      {
        if (!(productMaster.mPlatform != Device.Platform))
          stringList.Add(productMaster.ProductId);
      }
      if (stringList.Count == 0)
      {
        foreach (ProductParam productMaster in ProductMasters)
        {
          if (!(productMaster.mPlatform != "windows"))
            stringList.Add(productMaster.ProductId);
        }
      }
      return stringList;
    }

    public void Init(List<ProductParam> ProductMasters)
    {
      PurchaseFlow.Init(this.GetProductList(ProductMasters).ToArray(), (IPurchaseGlobalListener) new PurchaseManager.PurchaseGlobalListener());
    }

    public void InputBirthday(int year, int month, int day)
    {
      this.onProductsSchedule = PurchaseManager.OnProductsSchedule.REQUEST_API;
      PurchaseFlow.Instance.InputBirthday(year, month, day);
    }

    public void ResponseBirthday(PaymentManager.ERegisterBirthdayResult result)
    {
      MonoSingleton<PaymentManager>.Instance.OnAgeResponse(result);
    }

    public void Purchase(string productId)
    {
      PurchaseFlow.Instance.Purchase(productId);
    }

    public void UpdateProduct(List<ProductParam> ProductMasters)
    {
      PurchaseFlow.UpdateProducts(this.GetProductList(ProductMasters).ToArray());
    }

    public void Resume()
    {
      PurchaseFlow.Resume();
    }

    public void Confirm(PurchaseFlow flow, ProductInfo product)
    {
      UIUtility.ConfirmBox(LocalizedText.Get("sys.MSG_PURCHASE_CONFIRM", (object) product.LocalizedTitle, (object) product.LocalizedPrice), (UIUtility.DialogResultEvent) (obj =>
      {
        flow.Confirmed(true);
        MonoSingleton<PaymentManager>.Instance.OnPurchaseProcessing();
      }), (UIUtility.DialogResultEvent) (obj => flow.Confirmed(false)), (GameObject) null, true, -1, (string) null, (string) null);
    }

    public void OnProducts(PurchaseFlow flow, ProductInfo[] products)
    {
      if (this.onProductsSchedule == PurchaseManager.OnProductsSchedule.REQUEST_API)
        this.ResponseBirthday(PaymentManager.ERegisterBirthdayResult.SUCCESS);
      this.onProductsSchedule = PurchaseManager.OnProductsSchedule.NONE;
      MonoSingleton<PaymentManager>.Instance.OnUpdateProductDetails(PurchaseFlow.ProductList);
    }

    public enum OnProductsSchedule
    {
      NONE,
      REQUEST_API,
    }

    public class PurchaseGlobalListener : IPurchaseGlobalListener, IPurchaseResultListener
    {
      public void OnInitialized()
      {
        MonoSingleton<PaymentManager>.Instance.OnUpdateProductDetails(PurchaseFlow.ProductList);
      }

      private void OnPurchaseSucceeded(FulfillmentResult.OrderInfo order)
      {
        MonoSingleton<PaymentManager>.Instance.OnPurchaseSucceeded(order);
      }

      public void OnPurchaseSucceeded(FulfillmentResult result)
      {
        foreach (FulfillmentResult.OrderInfo succeededTransaction in result.SucceededTransactions)
          this.OnPurchaseSucceeded(succeededTransaction);
        foreach (FulfillmentResult.OrderInfo duplicatedTransaction in result.DuplicatedTransactions)
          this.OnPurchaseSucceeded(duplicatedTransaction);
        MonoSingleton<GameManager>.Instance.Player.SetCoinPurchaseResult(result);
        GlobalEvent.Invoke("REFRESH_COIN_STATUS", (object) this);
        GlobalVars.AfterCoin = MonoSingleton<GameManager>.Instance.Player.Coin;
        PaymentManager.Product product = MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID);
        if (product == null)
          return;
        GlobalVars.BeforeCoin = MonoSingleton<GameManager>.Instance.Player.Coin - (product.numFree + product.numPaid);
      }

      public void OnPurchaseFailed()
      {
      }

      public void OnPurchaseCanceled()
      {
      }

      public void OnPurchaseAlreadyOwned()
      {
      }

      public void OnPurchaseDeferred()
      {
      }

      public void OnOverCreditLimited()
      {
      }

      public void OnInsufficientBalances()
      {
      }

      public void OnFinished(bool isSuccess)
      {
      }
    }
  }
}
