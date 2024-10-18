// Decompiled with JetBrains decompiler
// Type: SRPG.ReqReadTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqReadTips : WebAPI
  {
    public ReqReadTips(string tips, string trophyprog, string bingoprog, Network.ResponseCallback response)
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
