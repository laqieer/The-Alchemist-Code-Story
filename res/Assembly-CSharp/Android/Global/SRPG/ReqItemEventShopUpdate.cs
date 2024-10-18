// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemEventShopUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemEventShopUpdate : WebAPI
  {
    public ReqItemEventShopUpdate(string iname, string costiname, Network.ResponseCallback response)
    {
      this.name = "shop/update";
      this.body = "\"iname\":\"" + iname + "\",";
      ReqItemEventShopUpdate itemEventShopUpdate = this;
      itemEventShopUpdate.body = itemEventShopUpdate.body + "\"costiname\":\"" + costiname + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
