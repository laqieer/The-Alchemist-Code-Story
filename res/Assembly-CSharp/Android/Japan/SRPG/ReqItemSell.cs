// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class ReqItemSell : WebAPI
  {
    public ReqItemSell(Dictionary<long, int> sells, bool is_item_convert, Network.ResponseCallback response)
    {
      this.name = !is_item_convert ? "item/sell" : "item/auto/sell";
      this.body = "\"sells\":[";
      string str = string.Empty;
      foreach (KeyValuePair<long, int> sell in sells)
      {
        str += "{";
        str = str + "\"iid\":" + sell.Key.ToString() + ",";
        str = str + "\"num\":" + sell.Value.ToString();
        str += "},";
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      this.body += str;
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
