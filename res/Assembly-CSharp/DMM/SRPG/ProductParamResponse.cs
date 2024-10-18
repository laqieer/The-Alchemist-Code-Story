// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParamResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ProductParamResponse
  {
    public List<ProductParam> products = new List<ProductParam>();

    public bool Deserialize(JSON_ProductParamResponse json)
    {
      if (json == null || json.products == null)
        return true;
      this.products.Clear();
      for (int index1 = 0; index1 < json.products.Length; ++index1)
      {
        ProductParam productParam = new ProductParam();
        JSON_ProductBuyCoinParam jsonBuycoin = (JSON_ProductBuyCoinParam) null;
        if (json.buycoins != null)
        {
          for (int index2 = 0; index2 < json.buycoins.Length; ++index2)
          {
            if (json.products[index1].product_id == json.buycoins[index2].product_id)
            {
              jsonBuycoin = json.buycoins[index2];
              break;
            }
          }
        }
        if (!productParam.Deserialize(json.products[index1], jsonBuycoin))
          return false;
        this.products.Add(productParam);
      }
      return true;
    }
  }
}
