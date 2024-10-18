// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ChargeV2Limit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.API.App
{
  public class ChargeV2Limit : GenericRequest<ChargeV2Limit, Gsc.Purchase.API.Response.ChargeV2Limit>
  {
    private const string ___path = "/v2/limit";

    public ChargeV2Limit(List<float> prices, string currency)
    {
      this.Prices = prices;
      this.Currency = currency;
    }

    public List<float> Prices { get; set; }

    public string Currency { get; set; }

    public override string GetPath()
    {
      return SDK.Configuration.Env.PurchaseApiPrefix + "/v2/limit?currency=" + this.Currency + string.Join(string.Empty, this.Prices.Select<float, string>((Func<float, string>) (x => "&price=" + (object) x)).ToArray<string>());
    }

    public override string GetMethod()
    {
      return "GET";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return (Dictionary<string, object>) null;
    }
  }
}
