// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.PurchaseUpdate
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
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "dmm_viewer_id";
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (PurchaseUpdate.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseUpdate.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = PurchaseUpdate.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "dmm_onetime_token";
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (PurchaseUpdate.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseUpdate.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = PurchaseUpdate.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.OnetimeToken);
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "dmm_payment_ids";
      Serializer serializer = Serializer.Instance.WithArray<string>();
      // ISSUE: reference to a compiler-generated field
      if (PurchaseUpdate.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseUpdate.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = PurchaseUpdate.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = serializer.Add<string>(fMgCache2).Serialize<List<string>>(this.PaymentIds);
      dictionary4[index3] = obj3;
      return dictionary1;
    }

    public class Response : GenericResponse<PurchaseUpdate.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.Results = document.Root["statuses"].GetArray().Select<IValue, PurchaseUpdate.Response.Status_t>((Func<IValue, PurchaseUpdate.Response.Status_t>) (x => new PurchaseUpdate.Response.Status_t(x))).ToArray<PurchaseUpdate.Response.Status_t>();
      }

      public PurchaseUpdate.Response.Status_t[] Results { get; private set; }

      public class Status_t : IResponseObject, Gsc.Network.IObject
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
