// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemLimitedShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqItemLimitedShopBuypaid : WebAPI
  {
    public ReqItemLimitedShopBuypaid(string shopname, int id, int buynum, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "shop/limited/buy";
      stringBuilder.Append("\"shopName\":\"" + shopname + "\",");
      stringBuilder.Append("\"id\":" + id.ToString() + ",");
      stringBuilder.Append("\"buynum\":" + buynum.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
