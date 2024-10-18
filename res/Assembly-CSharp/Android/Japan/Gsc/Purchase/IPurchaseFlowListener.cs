// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
