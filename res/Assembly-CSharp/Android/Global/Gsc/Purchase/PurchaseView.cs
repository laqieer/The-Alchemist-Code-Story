// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace Gsc.Purchase
{
  public class PurchaseView : MonoBehaviour, IPurchaseFlowListener, IPurchaseResultListener
  {
    private void Start()
    {
      PurchaseFlow.LaunchFlow<PurchaseView>(this);
    }

    public void InputBirthday(PurchaseFlow flow)
    {
      MonoSingleton<PaymentManager>.Instance.OnNeedBirthday();
    }

    public void OnInvalidBirthday(PurchaseFlow flow)
    {
      MonoSingleton<PurchaseManager>.Instance.ResponseBirthday(PaymentManager.ERegisterBirthdayResult.ERROR);
    }

    public void Confirm(PurchaseFlow flow, ProductInfo product)
    {
      MonoSingleton<PurchaseManager>.Instance.Confirm(flow, product);
    }

    public void OnProducts(PurchaseFlow flow, ProductInfo[] products)
    {
      MonoSingleton<PurchaseManager>.Instance.OnProducts(flow, products);
    }

    public void OnPurchaseSucceeded(FulfillmentResult result)
    {
    }

    public void OnPurchaseFailed()
    {
      MonoSingleton<PaymentManager>.Instance.OnPurchaseFailed();
    }

    public void OnPurchaseCanceled()
    {
      MonoSingleton<PaymentManager>.Instance.OnPurchaseCanceled(string.Empty);
    }

    public void OnPurchaseAlreadyOwned()
    {
      MonoSingleton<PaymentManager>.Instance.OnPurchaseAlreadyOwned(string.Empty);
    }

    public void OnOverCreditLimited()
    {
      MonoSingleton<PaymentManager>.Instance.OnOverCreditLimited();
    }

    public void OnInsufficientBalances()
    {
      MonoSingleton<PaymentManager>.Instance.OnInsufficientBalances();
    }

    public void OnPurchaseDeferred()
    {
      MonoSingleton<PaymentManager>.Instance.OnPurchaseDeferred();
    }

    public void OnFinished(bool isSuccess)
    {
      MonoSingleton<PaymentManager>.Instance.OnPurchaseFinished(isSuccess);
    }
  }
}
