// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.AndApp.Fulfillment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Purchase.API.PAS.AndApp
{
  public class Fulfillment : Request<Fulfillment, Gsc.Purchase.API.Response.Fulfillment>
  {
    private const string ___path = "{0}/pas/andapp/{1}/fulfill";

    public Fulfillment(string deviceId, List<Fulfillment.PurchaseData_t> purchaseDataList)
    {
      this.DeviceId = deviceId;
      this.PurchaseDataList = purchaseDataList;
    }

    public string DeviceId { get; set; }

    public List<Fulfillment.PurchaseData_t> PurchaseDataList { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/andapp/{1}/fulfill", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => (string) null;

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      parameters["device_id"] = (object) this.DeviceId;
      parameters["platform"] = (object) "andapp";
      parameters["version"] = (object) "v1";
      Dictionary<string, object> dictionary = parameters;
      Serializer serializer = Serializer.Instance.WithArray<Fulfillment.PurchaseData_t>();
      // ISSUE: reference to a compiler-generated field
      if (Fulfillment.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Fulfillment.\u003C\u003Ef__mg\u0024cache0 = new Func<Fulfillment.PurchaseData_t, object>(Serializer.FromObject<Fulfillment.PurchaseData_t>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<Fulfillment.PurchaseData_t, object> fMgCache0 = Fulfillment.\u003C\u003Ef__mg\u0024cache0;
      object obj = serializer.Add<Fulfillment.PurchaseData_t>(fMgCache0).Serialize<List<Fulfillment.PurchaseData_t>>(this.PurchaseDataList);
      dictionary["receipts"] = obj;
      return parameters;
    }

    public class PurchaseData_t : IRequestObject, IObject
    {
      public PurchaseData_t(string receiptData) => this.ReceiptData = receiptData;

      public string ReceiptData { get; set; }

      public Dictionary<string, object> GetPayload()
      {
        Dictionary<string, object> payload = new Dictionary<string, object>();
        Dictionary<string, object> dictionary = payload;
        Serializer instance = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache0 = Fulfillment.PurchaseData_t.\u003C\u003Ef__mg\u0024cache0;
        object obj = instance.Add<string>(fMgCache0).Serialize<string>(this.ReceiptData);
        dictionary["transaction_token"] = obj;
        return payload;
      }
    }
  }
}
