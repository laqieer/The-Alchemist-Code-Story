// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemGuerrillaShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
