// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemGuerrillaShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqItemGuerrillaShopBuypaid : WebAPI
  {
    public ReqItemGuerrillaShopBuypaid(int id, int buynum, Network.ResponseCallback response)
    {
      this.name = "shop/guerrilla/buy";
      ReqItemGuerrillaShopBuypaid guerrillaShopBuypaid1 = this;
      guerrillaShopBuypaid1.body = guerrillaShopBuypaid1.body + "\"id\":" + id.ToString() + ",";
      ReqItemGuerrillaShopBuypaid guerrillaShopBuypaid2 = this;
      guerrillaShopBuypaid2.body = guerrillaShopBuypaid2.body + "\"buynum\":" + buynum.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
