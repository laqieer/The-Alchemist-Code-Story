// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqRanking : WebAPI
  {
    public ReqRanking(string[] inames, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit/multiple";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"inames\":[");
      for (int index = 0; index < inames.Length; ++index)
      {
        stringBuilder.Append("\"");
        stringBuilder.Append(JsonEscape.Escape(inames[index]));
        if (index == inames.Length - 1)
          stringBuilder.Append("\"]");
        else
          stringBuilder.Append("\",");
      }
      this.body = stringBuilder.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
