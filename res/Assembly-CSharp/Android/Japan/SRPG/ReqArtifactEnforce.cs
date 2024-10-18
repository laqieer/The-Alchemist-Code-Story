// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactEnforce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqArtifactEnforce : WebAPI
  {
    public ReqArtifactEnforce(long iid, Dictionary<string, int> usedItems, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":");
      stringBuilder.Append(iid);
      stringBuilder.Append(",\"mats\":[");
      string str = string.Empty;
      foreach (KeyValuePair<string, int> usedItem in usedItems)
      {
        str += "{";
        str = str + "\"iname\":\"" + usedItem.Key + "\",";
        str = str + "\"num\":" + (object) usedItem.Value;
        str += "},";
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "unit/job/artifact/enforce";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
