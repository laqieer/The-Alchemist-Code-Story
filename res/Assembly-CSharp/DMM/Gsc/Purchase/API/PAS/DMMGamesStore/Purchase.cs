// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.Purchase
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
namespace Gsc.Purchase.API.PAS.DMMGamesStore
{
  public class Purchase : Request<Gsc.Purchase.API.PAS.DMMGamesStore.Purchase, Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.Response>
  {
    private const string ___path = "{0}/pas/dmmgamesstore/{1}/purchase";

    public Purchase(int viewerId, string onetimeToken, string productId)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
      this.ProductID = productId;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public string ProductID { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/dmmgamesstore/{1}/purchase", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => "{0}/pas/dmmgamesstore/{1}/purchase";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary1["dmm_viewer_id"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.OnetimeToken);
      dictionary2["dmm_onetime_token"] = obj2;
      Dictionary<string, object> dictionary3 = parameters;
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.ProductID);
      dictionary3["product_id"] = obj3;
      return parameters;
    }

    public class Response : GenericResponse<Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.PaymentId = document.Root["dmm_payment_id"].ToString();
      }

      public string PaymentId { get; private set; }
    }
  }
}
