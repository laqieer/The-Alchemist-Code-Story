// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqItemShopBuypaid : WebAPI
  {
    public ReqItemShopBuypaid(string iname, int id, int buynum, Network.ResponseCallback response)
    {
      this.name = "shop/buy";
      this.body = "\"iname\":\"" + iname + "\",";
      ReqItemShopBuypaid reqItemShopBuypaid1 = this;
      reqItemShopBuypaid1.body = reqItemShopBuypaid1.body + "\"id\":" + id.ToString() + ",";
      ReqItemShopBuypaid reqItemShopBuypaid2 = this;
      reqItemShopBuypaid2.body = reqItemShopBuypaid2.body + "\"buynum\":" + buynum.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
