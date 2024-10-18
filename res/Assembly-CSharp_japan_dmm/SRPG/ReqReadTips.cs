// Decompiled with JetBrains decompiler
// Type: SRPG.ReqReadTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqReadTips : WebAPI
  {
    public ReqReadTips(
      string tips,
      string trophyprog,
      string bingoprog,
      Network.ResponseCallback response)
    {
      this.name = "tips/end";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"iname\":");
      stringBuilder.Append("\"" + tips + "\"");
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
