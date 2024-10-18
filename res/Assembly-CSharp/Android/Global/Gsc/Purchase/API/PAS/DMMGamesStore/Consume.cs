// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.Consume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;

namespace Gsc.Purchase.API.PAS.DMMGamesStore
{
  public class Consume : Request<Consume, Consume.Response>
  {
    private const string ___path = "{0}/pas/dmmgamesstore/{1}/purchase/consume";

    public Consume(int viewerId, string onetimeToken, string paymentId)
    {
      this.ViewerId = viewerId;
      this.OnetimeToken = onetimeToken;
      this.PaymentId = paymentId;
    }

    public int ViewerId { get; set; }

    public string OnetimeToken { get; set; }

    public string PaymentId { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/dmmgamesstore/{1}/purchase/consume", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/pas/dmmgamesstore/{1}/purchase/consume";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerId),
        ["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken),
        ["dmm_payment_id"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.PaymentId)
      };
    }

    public class Response : GenericResponse<Consume.Response>
    {
      public Response(WebInternalResponse response)
      {
      }
    }
  }
}
