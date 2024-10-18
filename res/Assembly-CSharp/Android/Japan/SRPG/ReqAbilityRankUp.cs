// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAbilityRankUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqAbilityRankUp : WebAPI
  {
    public ReqAbilityRankUp(Dictionary<long, int> abilities, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null)
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
