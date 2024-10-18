// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.PurchaseList
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
namespace Gsc.Purchase.API.PAS.DMMGamesStore
{
  public class PurchaseList : Request<PurchaseList, PurchaseList.Response>
  {
    private const string ___path = "{0}/pas/dmmgamesstore/{1}/purchase/list";

    public PurchaseList(int viewerId, string onetimeToken)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/dmmgamesstore/{1}/purchase/list", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => "{0}/pas/dmmgamesstore/{1}/purchase/list";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (PurchaseList.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseList.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = PurchaseList.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary1["dmm_viewer_id"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (PurchaseList.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseList.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = PurchaseList.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.OnetimeToken);
      dictionary2["dmm_onetime_token"] = obj2;
      return parameters;
    }

    public class Response : GenericResponse<PurchaseList.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.Infos = document.Root["purchase_infos"].GetArray().Select<IValue, PurchaseList.Response.PurchaseInfo_t>((Func<IValue, PurchaseList.Response.PurchaseInfo_t>) (x => new PurchaseList.Response.PurchaseInfo_t(x))).ToArray<PurchaseList.Response.PurchaseInfo_t>();
      }

      public PurchaseList.Response.PurchaseInfo_t[] Infos { get; private set; }

      public class PurchaseInfo_t : IResponseObject, Gsc.Network.IObject
      {
        public PurchaseInfo_t(IValue node)
        {
          Gsc.DOM.IObject @object = node.GetObject();
          this.PaymentId = @object["dmm_payment_id"].ToString();
          this.ProductId = @object["product_id"].ToString();
        }

        public string PaymentId { get; private set; }

        public string ProductId { get; private set; }
      }
    }
  }
}
