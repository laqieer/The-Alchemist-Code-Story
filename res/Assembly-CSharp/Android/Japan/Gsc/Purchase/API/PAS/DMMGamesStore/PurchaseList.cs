// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.PurchaseList
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

    public override string GetPath()
    {
      return "{0}/pas/dmmgamesstore/{1}/purchase/list";
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
      if (PurchaseList.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseList.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = PurchaseList.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "dmm_onetime_token";
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
      dictionary3[index2] = obj2;
      return dictionary1;
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
