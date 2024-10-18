// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlComRaid : WebAPI
  {
    public ReqBtlComRaid(string iname, int ticket, Network.ResponseCallback response, int partyIndex)
    {
      this.name = "btl/com/raid2";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"req_at\":");
      stringBuilder.Append(Network.GetServerTime());
      stringBuilder.Append(",");
      stringBuilder.Append("\"ticket\":");
      stringBuilder.Append(ticket.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
