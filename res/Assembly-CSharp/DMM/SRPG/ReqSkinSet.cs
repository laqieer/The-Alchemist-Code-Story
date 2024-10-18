// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSkinSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSkinSet : WebAPI
  {
    public ReqSkinSet(Dictionary<long, string> sets, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"sets\":[");
      string str = string.Empty;
      foreach (KeyValuePair<long, string> set in sets)
      {
        str += "{";
        str = str + "\"iid\":" + set.Key.ToString() + ",";
        str = str + "\"iname\":\"" + set.Value + "\"";
        str += "},";
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "unit/skin/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
