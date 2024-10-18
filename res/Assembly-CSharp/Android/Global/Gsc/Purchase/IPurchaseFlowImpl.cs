// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public interface IPurchaseFlowImpl
  {
    void Init(string[] productIds);

    void UpdateProducts(string[] productIds);

    void Resume();

    bool Confirmed();

    bool Purchase(ProductInfo product);

    void Consume(string transactionId);
  }
}
