// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactEnforce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      using (Dictionary<string, int>.Enumerator enumerator = usedItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, int> current = enumerator.Current;
          str += "{";
          str = str + "\"iname\":\"" + current.Key + "\",";
          str = str + "\"num\":" + (object) current.Value;
          str += "},";
        }
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
