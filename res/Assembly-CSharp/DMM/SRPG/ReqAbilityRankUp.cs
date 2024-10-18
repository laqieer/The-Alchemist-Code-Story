// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAbilityRankUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqAbilityRankUp : WebAPI
  {
    public ReqAbilityRankUp(
      Dictionary<long, int> abilities,
      Network.ResponseCallback response,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "unit/job/abil/lvup";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"aps\":[");
      string str = string.Empty;
      foreach (KeyValuePair<long, int> ability in abilities)
      {
        str += "{";
        str = str + "\"iid\":" + (object) ability.Key + ",";
        str = str + "\"ap\":" + (object) ability.Value;
        str += "},";
      }
      if (str.Length > 0)
        stringBuilder.Append(str.Substring(0, str.Length - 1));
      stringBuilder.Append("]");
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
