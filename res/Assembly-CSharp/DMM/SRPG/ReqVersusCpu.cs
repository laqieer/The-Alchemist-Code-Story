// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusCpu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqVersusCpu : WebAPI
  {
    public ReqVersusCpu(string iname, int deck_id, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "vs/com/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(JsonEscape.Escape(iname));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"deck_id\":");
      stringBuilder.Append(deck_id);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
