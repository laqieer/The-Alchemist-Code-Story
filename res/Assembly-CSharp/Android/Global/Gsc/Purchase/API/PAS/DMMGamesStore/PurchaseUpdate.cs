// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.PurchaseUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.API.PAS.DMMGamesStore
{
  public class PurchaseUpdate : Request<PurchaseUpdate, PurchaseUpdate.Response>
  {
    private const string ___path = "{0}/pas/dmmgamesstore/{1}/purchase/update";

    public PurchaseUpdate(int viewerId, string onetimeToken, List<string> paymentIds)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
      this.PaymentIds = paymentIds;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public List<string> PaymentIds { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/dmmgamesstore/{1}/purchase/update", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/pas/dmmgamesstore/{1}/purchase/update";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerID),
        ["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken),
        ["dmm_payment_ids"] = Serializer.Instance.WithArray<string>().Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<List<string>>(this.PaymentIds)
      };
    }

    public class Response : GenericResponse<PurchaseUpdate.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.Results = document.Root["statuses"].GetArray().Select<IValue, PurchaseUpdate.Response.Status_t>((Func<IValue, PurchaseUpdate.Response.Status_t>) (x => new PurchaseUpdate.Response.Status_t(x))).ToArray<PurchaseUpdate.Response.Status_t>();
      }

      public PurchaseUpdate.Response.Status_t[] Results { get; private set; }

      public class Status_t : Gsc.Network.IObject, IResponseObject
      {
        public Status_t(IValue node)
        {
          Gsc.DOM.IObject @object = node.GetObject();
          this.PaymentId = @object["dmm_payment_id"].ToString();
          this.Status = @object["status"].ToString();
        }

        public string PaymentId { get; private set; }

        public string Status { get; private set; }
      }
    }
  }
}
