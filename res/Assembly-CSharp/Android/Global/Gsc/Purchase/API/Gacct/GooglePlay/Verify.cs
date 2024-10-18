// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.GooglePlay.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;

namespace Gsc.Purchase.API.Gacct.GooglePlay
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/verify";

    public Verify(string currency, float price, string signature, string receiptData)
    {
      this.Currency = currency;
      this.Price = price;
      this.Signature = signature;
      this.ReceiptData = receiptData;
    }

    public string Currency { get; set; }

    public float Price { get; set; }

    public string Signature { get; set; }

    public string ReceiptData { get; set; }

    public bool IsBundle { get; set; }

    public override string GetPath()
    {
      if (this.IsBundle)
        return SDK.Configuration.Env.BundlePurchaseApiPrefix + "/verify";
      return SDK.Configuration.Env.PurchaseApiPrefix + "/verify";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["currency"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Currency),
        ["price"] = Serializer.Instance.Add<float>(new Func<float, object>(Serializer.From<float>)).Serialize<float>(this.Price),
        ["signature"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Signature),
        ["signed_data"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.ReceiptData)
      };
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          this.SuccessTransactionId = document.Root["success_transaction_id"].ToString();
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
