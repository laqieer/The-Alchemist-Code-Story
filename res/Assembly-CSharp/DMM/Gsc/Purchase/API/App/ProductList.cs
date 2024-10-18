// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ProductList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Purchase.API.App
{
  public class ProductList : GenericRequest<ProductList, Gsc.Purchase.API.Response.ProductList>
  {
    private const string ___path = "/api{0}/{1}/products";

    public override string GetPath()
    {
      return string.Format("/api{0}/{1}/products", (object) SDK.Configuration.Env.PurchaseApiPrefix, (object) Device.Platform);
    }

    public override string GetMethod() => "GET";

    protected override Dictionary<string, object> GetParameters()
    {
      return (Dictionary<string, object>) null;
    }
  }
}
