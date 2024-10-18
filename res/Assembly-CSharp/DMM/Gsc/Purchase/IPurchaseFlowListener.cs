// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
