// Decompiled with JetBrains decompiler
// Type: SRPG.ReqShopLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqShopLineup : WebAPI
  {
    public ReqShopLineup(string shop_name, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "shop/itemlist";
      stringBuilder.Append("\"shopName\":\"");
      stringBuilder.Append(shop_name);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
