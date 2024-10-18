// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    public class Response : Json_PlayerDataAll
    {
      public long[] sells;
    }
  }
}
