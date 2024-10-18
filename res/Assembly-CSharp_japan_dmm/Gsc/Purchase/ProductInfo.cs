// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.ProductInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Purchase
{
  public class ProductInfo
  {
    public readonly string ID;
    public readonly string LocalizedTitle;
    public readonly string LocalizedDescription;
    public readonly string LocalizedPrice;
    public readonly string CurrencyCode;
    public readonly float Price;

    public ProductInfo(
      string id,
      string title,
      string description,
      string price,
      string currencyCode,
      float priceValue)
    {
      this.ID = id;
      this.LocalizedTitle = title;
      this.LocalizedDescription = description;
      this.LocalizedPrice = price;
      this.CurrencyCode = currencyCode;
      this.Price = priceValue;
    }

    public bool enabled => PurchaseFlow.IsEnable(this);
  }
}
