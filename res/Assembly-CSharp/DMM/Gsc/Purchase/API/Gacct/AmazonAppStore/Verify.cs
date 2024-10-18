// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.AmazonAppStore.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Purchase.API.Gacct.AmazonAppStore
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/amazon/verify";

    public Verify(string userId, string currency, float price, string receiptId)
    {
      this.UserId = userId;
      this.Currency = currency;
      this.Price = price;
      this.ReceiptId = receiptId;
    }

    public string UserId { get; set; }

    public string Currency { get; set; }

    public float Price { get; set; }

    public string ReceiptId { get; set; }

    public override string GetPath() => SDK.Configuration.Env.PurchaseApiPrefix + "/amazon/verify";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = Verify.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.UserId);
      dictionary1["user"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Verify.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.Currency);
      dictionary2["currency"] = obj2;
      Dictionary<string, object> dictionary3 = parameters;
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache2 = new Func<float, object>(Serializer.From<float>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<float, object> fMgCache2 = Verify.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<float>(fMgCache2).Serialize<float>(this.Price);
      dictionary3["price"] = obj3;
      Dictionary<string, object> dictionary4 = parameters;
      Serializer instance4 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache3 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache3 = Verify.\u003C\u003Ef__mg\u0024cache3;
      object obj4 = instance4.Add<string>(fMgCache3).Serialize<string>(this.ReceiptId);
      dictionary4["receipt"] = obj4;
      return parameters;
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          this.SuccessTransactionId = document.Root["receipt_id"].ToString();
          this.CurrentPaidCoin = document.Root["current_paid_coin"].ToInt();
          this.CurrentFreeCoin = document.Root["current_free_coin"].ToInt();
          this.CurrentCommonCoin = document.Root["current_common_coin"].ToInt();
          this.AdditionalPaidCoin = document.Root["additional_paid_coin"].ToInt();
          this.AdditionalFreeCoin = document.Root["additional_free_coin"].ToInt();
        }
      }

      public string SuccessTransactionId { get; private set; }

      public int CurrentPaidCoin { get; private set; }

      public int CurrentFreeCoin { get; private set; }

      public int CurrentCommonCoin { get; private set; }

      public int AdditionalPaidCoin { get; private set; }

      public int AdditionalFreeCoin { get; private set; }
    }
  }
}
