// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqLimitedShopList : WebAPI
  {
    public ReqLimitedShopList(Network.ResponseCallback response)
    {
      this.name = "shop/limited/shoplist";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
