// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ProductList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;
using System;
using System.Linq;

namespace Gsc.Purchase.API.Response
{
  public class ProductList : GenericResponse<ProductList>
  {
    public ProductList(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.Products = document.Root["products"].GetArray().Select<IValue, ProductList.ProductData_t>((Func<IValue, ProductList.ProductData_t>) (x => new ProductList.ProductData_t(x))).ToArray<ProductList.ProductData_t>();
    }

    public ProductList.ProductData_t[] Products { get; private set; }

    public class ProductData_t : IResponseObject, Gsc.Network.IObject
    {
      public ProductData_t(IValue node)
      {
        Gsc.DOM.IObject @object = node.GetObject();
        this.ProductId = @object["product_id"].ToString();
        this.Price = @object["price"].ToFloat();
        this.Name = @object["name"].ToString();
        this.Currency = @object["currency"].ToString();
        this.LocalizedPrice = @object["localized_price"].ToString();
        this.Description = @object["description"].ToString();
      }

      public string ProductId { get; private set; }

      public float Price { get; private set; }

      public string Name { get; private set; }

      public string Currency { get; private set; }

      public string LocalizedPrice { get; private set; }

      public string Description { get; private set; }
    }
  }
}
