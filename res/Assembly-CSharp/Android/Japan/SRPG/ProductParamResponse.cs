// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParamResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

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
      for (int index = 0; index < json.products.Length; ++index)
      {
        ProductParam productParam = new ProductParam();
        if (!productParam.Deserialize(json.products[index]))
          return false;
        this.products.Add(productParam);
      }
      return true;
    }
  }
}
