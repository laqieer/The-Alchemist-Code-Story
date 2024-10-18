// Decompiled with JetBrains decompiler
// Type: SRPG.ReqEventShopItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqEventShopItemList : WebAPI
  {
    public ReqEventShopItemList(string shop_name, Network.ResponseCallback response)
    {
      this.name = "shop/event";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"shopName\":\"");
      stringBuilder.Append(shop_name);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
