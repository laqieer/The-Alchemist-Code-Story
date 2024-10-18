// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactConvert
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqArtifactConvert : WebAPI
  {
    public ReqArtifactConvert(long[] artifact_iids, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iids\":[");
      string str = string.Empty;
      for (int index = 0; index < artifact_iids.Length; ++index)
        str = str + artifact_iids[index].ToString() + ",";
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "unit/job/artifact/convert";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class Response : Json_PlayerDataAll
    {
      public long[] iids;
    }
  }
}
