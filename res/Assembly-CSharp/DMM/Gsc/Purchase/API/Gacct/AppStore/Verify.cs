// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.AppStore.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Gsc.Purchase.API.Gacct.AppStore
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/v2/ios/verify";

    public Verify(List<Verify.PurchaseData_t> receipts, string receiptData)
    {
      this.Receipts = receipts;
      this.ReceiptData = receiptData;
    }

    public List<Verify.PurchaseData_t> Receipts { get; set; }

    public string ReceiptData { get; set; }

    public override string GetPath() => SDK.Configuration.Env.PurchaseApiPrefix + "/v2/ios/verify";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer serializer = Serializer.Instance.WithArray<Verify.PurchaseData_t>();
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache0 = new Func<Verify.PurchaseData_t, object>(Serializer.FromObject<Verify.PurchaseData_t>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<Verify.PurchaseData_t, object> fMgCache0 = Verify.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = serializer.Add<Verify.PurchaseData_t>(fMgCache0).Serialize<List<Verify.PurchaseData_t>>(this.Receipts);
      dictionary1["receipts"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Verify.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance.Add<string>(fMgCache1).Serialize<string>(this.ReceiptData);
      dictionary2["receipt_data"] = obj2;
      return parameters;
    }

    public class PurchaseData_t : IRequestObject, Gsc.Network.IObject
    {
      public PurchaseData_t(string currency, float price, string transactionId)
      {
        this.Currency = currency;
        this.Price = price;
        this.TransactionId = transactionId;
      }

      public string Currency { get; set; }

      public float Price { get; set; }

      public string TransactionId { get; set; }

      public Dictionary<string, object> GetPayload()
      {
        Dictionary<string, object> payload = new Dictionary<string, object>();
        Dictionary<string, object> dictionary1 = payload;
        Serializer instance1 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache0 = Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0;
        object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.Currency);
        dictionary1["currency"] = obj1;
        Dictionary<string, object> dictionary2 = payload;
        Serializer instance2 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1 = new Func<float, object>(Serializer.From<float>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<float, object> fMgCache1 = Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1;
        object obj2 = instance2.Add<float>(fMgCache1).Serialize<float>(this.Price);
        dictionary2["price"] = obj2;
        Dictionary<string, object> dictionary3 = payload;
        Serializer instance3 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache2 = Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2;
        object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.TransactionId);
        dictionary3["transaction_id"] = obj3;
        return payload;
      }
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          this.SuccessTransactionIds = document.Root["success_transaction_ids"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
          this.DuplicatedTransactionIds = document.Root["duplicate_transaction_ids"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
          this.CurrentPaidCoin = document.Root["current_paid_coin"].ToInt();
          this.CurrentFreeCoin = document.Root["current_free_coin"].ToInt();
          this.CurrentCommonCoin = document.Root["current_common_coin"].ToInt();
          this.AdditionalPaidCoin = document.Root["additional_paid_coin"].ToInt();
          this.AdditionalFreeCoin = document.Root["additional_free_coin"].ToInt();
        }
      }

      public string[] SuccessTransactionIds { get; private set; }

      public string[] DuplicatedTransactionIds { get; private set; }

      public int CurrentPaidCoin { get; private set; }

      public int CurrentFreeCoin { get; private set; }

      public int CurrentCommonCoin { get; private set; }

      public int AdditionalPaidCoin { get; private set; }

      public int AdditionalFreeCoin { get; private set; }
    }
  }
}
