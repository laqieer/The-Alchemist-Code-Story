// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ProductParam
  {
    private string mId;
    private string mProductId;
    private string mPlatform;
    private string mName;
    private string mDescription;
    private int mAdditionalPaidCoin;
    private int mAdditionalFreeCoin;
    private int mRemainNum;
    private ProductParam.ProductSaleInfo mSale;
    private const int REMAIN_DEFAULT = -1;

    public string Id => this.mId;

    public string ProductId => this.mProductId;

    public string Platform => this.mPlatform;

    public string Name
    {
      get
      {
        return this.mSale != null && !string.IsNullOrEmpty(this.mSale.Name) ? this.mSale.Name : this.mName;
      }
    }

    public string Description
    {
      get
      {
        return this.mSale != null && !string.IsNullOrEmpty(this.mSale.Description) ? this.mSale.Description : this.mDescription;
      }
    }

    public int AdditionalPaidCoin => this.mAdditionalPaidCoin;

    public int AdditionalFreeCoin
    {
      get
      {
        return this.mSale != null && this.mSale.AdditionalFreeCoin > 0 ? this.mSale.AdditionalFreeCoin : this.mAdditionalFreeCoin;
      }
    }

    public int RemainNum => this.mRemainNum;

    public bool Deserialize(JSON_ProductParam jsonProduct, JSON_ProductBuyCoinParam jsonBuycoin)
    {
      if (jsonProduct == null)
        return false;
      this.mProductId = jsonProduct.product_id;
      this.mPlatform = jsonProduct.platform;
      this.mName = jsonProduct.name;
      this.mDescription = jsonProduct.description;
      this.mAdditionalPaidCoin = jsonProduct.additional_paid_coin;
      this.mAdditionalFreeCoin = jsonProduct.additional_free_coin;
      if (jsonProduct.sale != null)
      {
        this.mSale = new ProductParam.ProductSaleInfo();
        this.mSale.Name = !string.IsNullOrEmpty(jsonProduct.sale.name) ? jsonProduct.sale.name : string.Empty;
        this.mSale.Description = !string.IsNullOrEmpty(jsonProduct.sale.description) ? jsonProduct.sale.description : string.Empty;
        this.mSale.AdditionalFreeCoin = jsonProduct.sale.additional_free_coin > 0 ? jsonProduct.sale.additional_free_coin : 0;
      }
      this.mRemainNum = -1;
      if (jsonBuycoin != null)
      {
        this.mId = jsonBuycoin.id;
        this.mRemainNum = jsonBuycoin.remain_num;
      }
      return true;
    }

    public class ProductSaleInfo
    {
      public string Name;
      public string Description;
      public int AdditionalFreeCoin;
    }
  }
}
