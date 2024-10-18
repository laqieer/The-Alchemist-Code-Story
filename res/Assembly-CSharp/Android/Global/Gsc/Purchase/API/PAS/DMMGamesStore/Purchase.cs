// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.Purchase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      return new Dictionary<string, object>()
      {
        ["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerID),
        ["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken),
        ["product_id"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.ProductID)
      };
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
