// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public interface IPurchaseFlowListener : IPurchaseResultListener
  {
    void InputBirthday(PurchaseFlow flow);

    void Confirm(PurchaseFlow flow, ProductInfo product);

    void OnInvalidBirthday(PurchaseFlow flow);

    void OnProducts(PurchaseFlow flow, ProductInfo[] products);
  }
}
