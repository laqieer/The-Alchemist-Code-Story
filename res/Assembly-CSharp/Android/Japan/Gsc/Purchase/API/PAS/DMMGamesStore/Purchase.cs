// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.Purchase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;

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

    public override string GetPath()
    {
      return "{0}/pas/dmmgamesstore/{1}/purchase";
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
      if (Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = Gsc.Purchase.API.PAS.DMMGamesStore.Purchase.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "dmm_onetime_token";
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
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "product_id";
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
      dictionary4[index3] = obj3;
      return dictionary1;
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
