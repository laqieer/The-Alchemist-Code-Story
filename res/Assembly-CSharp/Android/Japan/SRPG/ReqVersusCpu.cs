// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusCpu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

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
