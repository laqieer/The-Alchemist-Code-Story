// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemLimitedShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
