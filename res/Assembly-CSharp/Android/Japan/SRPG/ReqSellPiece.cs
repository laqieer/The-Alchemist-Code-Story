// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSellPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqSellPiece : WebAPI
  {
    public ReqSellPiece(Dictionary<long, int> sells, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"sells\":[");
      string str = string.Empty;
      foreach (KeyValuePair<long, int> sell in sells)
      {
        str += "{";
        str = str + "\"iid\":" + (object) sell.Key + ",";
        str = str + "\"num\":" + (object) sell.Value;
        str += "},";
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "shop/piece/sell";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
