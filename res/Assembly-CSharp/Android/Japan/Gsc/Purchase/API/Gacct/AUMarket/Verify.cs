// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.AUMarket.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.API.Gacct.AUMarket
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/aumarket/verify";

    public Verify(string signature, string receiptData, List<Verify.PurchaseData_t> purchaseDataList)
    {
      this.Signature = signature;
      this.ReceiptData = receiptData;
      this.PurchaseDataList = purchaseDataList;
    }

    public List<Verify.PurchaseData_t> PurchaseDataList { get; set; }

    public string Signature { get; set; }

    public string ReceiptData { get; set; }

    public override string GetPath()
    {
      return SDK.Configuration.Env.PurchaseApiPrefix + "/aumarket/verify";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "receipts";
      Serializer serializer = Serializer.Instance.WithArray<Verify.PurchaseData_t>();
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache0 = new Func<Verify.PurchaseData_t, object>(Serializer.FromObject<Verify.PurchaseData_t>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<Verify.PurchaseData_t, object> fMgCache0 = Verify.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = serializer.Add<Verify.PurchaseData_t>(fMgCache0).Serialize<List<Verify.PurchaseData_t>>(this.PurchaseDataList);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "signature";
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Verify.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance1.Add<string>(fMgCache1).Serialize<string>(this.Signature);
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "receipt";
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Verify.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Verify.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = Verify.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance2.Add<string>(fMgCache2).Serialize<string>(this.ReceiptData);
      dictionary4[index3] = obj3;
      return dictionary1;
    }

    public class PurchaseData_t : IRequestObject, Gsc.Network.IObject
    {
      public PurchaseData_t(string currency, float price, string payInfoNo)
      {
        this.Currency = currency;
        this.Price = price;
        this.PayInfoNo = payInfoNo;
      }

      public string Currency { get; set; }

      public float Price { get; set; }

      public string PayInfoNo { get; set; }

      public Dictionary<string, object> GetPayload()
      {
        Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
        Dictionary<string, object> dictionary2 = dictionary1;
        string index1 = "currency";
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
        dictionary2[index1] = obj1;
        Dictionary<string, object> dictionary3 = dictionary1;
        string index2 = "price";
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
        dictionary3[index2] = obj2;
        Dictionary<string, object> dictionary4 = dictionary1;
        string index3 = "pay_info_no";
        Serializer instance3 = Serializer.Instance;
        // ISSUE: reference to a compiler-generated field
        if (Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
        }
        // ISSUE: reference to a compiler-generated field
        Func<string, object> fMgCache2 = Verify.PurchaseData_t.\u003C\u003Ef__mg\u0024cache2;
        object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.PayInfoNo);
        dictionary4[index3] = obj3;
        return dictionary1;
      }
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          this.SuccessTransactionIds = document.Root["success_pay_info_no"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
          this.DuplicatedTransactionIds = document.Root["duplicate_pay_info_nos"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
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
