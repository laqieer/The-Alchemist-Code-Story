// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.AppStore.Fulfillment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Purchase.API.PAS.AppStore
{
  public class Fulfillment : Request<Fulfillment, Gsc.Purchase.API.Response.Fulfillment>
  {
    private const string ___path = "{0}/pas/appstore/{1}/fulfill";

    public Fulfillment(List<Fulfillment.PurchaseData_t> purchaseDataList, string receiptData)
    {
      this.PurchaseDataList = purchaseDataList;
      this.ReceiptData = receiptData;
    }

    public string DeviceId { get; set; }

    public List<Fulfillment.PurchaseData_t> PurchaseDataList { get; set; }

    public string ReceiptData { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/appstore/{1}/fulfill", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => (string) null;

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      Serializer serializer = Serializer.Instance.WithArray<Fulfillment.PurchaseData_t>();
      // ISSUE: reference to a compiler-generated field
      if (Fulfillment.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Fulfillment.\u003C\u003Ef__mg\u0024cache0 = new Func<Fulfillment.PurchaseData_t, object>(Serializer.FromObject<Fulfillment.PurchaseData_t>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<Fulfillment.PurchaseData_t, object> fMgCache0 = Fulfillment.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = serializer.Add<Fulfillment.PurchaseData_t>(fMgCache0).Serialize<List<Fulfillment.PurchaseData_t>>(this.PurchaseDataList);
      dictionary2["receipts"] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      Serializer instance = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Fulfillment.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Fulfillment.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Fulfillment.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance.Add<string>(fMgCache1).Serialize<string>(this.ReceiptData);
      dictionary3["data"] = obj2;
      parameters["device_id"] = (object) this.DeviceId;
      parameters["receipt"] = (object) dictionary1;
      parameters["platform"] = (object) "appstore";
      parameters["version"] = (object) "v2";
      return parameters;
    }

    public class PurchaseData_t : IRequestObject, IObject
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
        if (Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache0 = Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0;
        object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.Currency);
        dictionary1["currency"] = obj1;
        Dictionary<string, object> dictionary2 = payload;
        Serializer instance2 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1 = new Func<float, object>(Serializer.From<float>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<float, object> fMgCache1 = Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache1;
        object obj2 = instance2.Add<float>(fMgCache1).Serialize<float>(this.Price);
        dictionary2["price"] = obj2;
        Dictionary<string, object> dictionary3 = payload;
        Serializer instance3 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache2 = Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2;
        object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.TransactionId);
        dictionary3["transaction_id"] = obj3;
        return payload;
      }
    }
  }
}
