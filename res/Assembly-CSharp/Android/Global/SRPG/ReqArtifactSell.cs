// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqArtifactSell : WebAPI
  {
    public ReqArtifactSell(long[] artifact_iids, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"sells\":[");
      string str = string.Empty;
      for (int index = 0; index < artifact_iids.Length; ++index)
        str = str + artifact_iids[index].ToString() + ",";
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "unit/job/artifact/sell";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
