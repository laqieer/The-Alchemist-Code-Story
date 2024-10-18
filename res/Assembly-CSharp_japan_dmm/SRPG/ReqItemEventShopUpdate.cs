// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemEventShopUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqItemEventShopUpdate : WebAPI
  {
    public ReqItemEventShopUpdate(
      string iname,
      string costiname,
      Network.ResponseCallback response)
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
