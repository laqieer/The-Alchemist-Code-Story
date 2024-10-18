// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqLimitedShopList : WebAPI
  {
    public ReqLimitedShopList(
      Network.ResponseCallback response,
      ReqLimitedShopList.eShopType shop_type = ReqLimitedShopList.eShopType.Limited)
    {
      this.name = "shop/limited/shoplist";
      if (shop_type == ReqLimitedShopList.eShopType.Port)
        this.body = WebAPI.GetRequestString("\"is_port_shop\":1");
      else
        this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }

    public enum eShopType
    {
      Limited,
      Port,
    }
  }
}
