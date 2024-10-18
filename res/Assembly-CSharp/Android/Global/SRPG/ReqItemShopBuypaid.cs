// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
