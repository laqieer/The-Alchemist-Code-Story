// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSellPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
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
