﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqProductBuy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqProductBuy : WebAPI
  {
    public ReqProductBuy(
      string productID,
      string receipt,
      string transactionID,
      Network.ResponseCallback response)
    {
      this.name = "product/buy";
      this.body = string.Empty;
      ReqProductBuy reqProductBuy1 = this;
      reqProductBuy1.body = reqProductBuy1.body + "\"productid\":\"" + productID + "\",";
      ReqProductBuy reqProductBuy2 = this;
      reqProductBuy2.body = reqProductBuy2.body + "\"receipt\":\"" + receipt + "\",";
      ReqProductBuy reqProductBuy3 = this;
      reqProductBuy3.body = reqProductBuy3.body + "\"transactionid\":\"" + transactionID + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
