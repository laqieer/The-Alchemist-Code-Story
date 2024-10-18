// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
