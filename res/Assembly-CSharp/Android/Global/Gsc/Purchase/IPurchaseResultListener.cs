// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseResultListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public interface IPurchaseResultListener
  {
    void OnPurchaseSucceeded(FulfillmentResult result);

    void OnPurchaseFailed();

    void OnPurchaseCanceled();

    void OnPurchaseAlreadyOwned();

    void OnPurchaseDeferred();

    void OnOverCreditLimited();

    void OnInsufficientBalances();

    void OnFinished(bool isSuccess);
  }
}
