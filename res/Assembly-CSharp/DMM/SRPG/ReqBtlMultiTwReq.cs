// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlMultiTwReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlMultiTwReq : WebAPI
  {
    public ReqBtlMultiTwReq(
      string iname,
      int partyIndex,
      int plid,
      int seat,
      string[] uid,
      Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/multi/tower/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"token\":\"");
      stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"plid\":\"");
      stringBuilder.Append(plid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"seat\":\"");
      stringBuilder.Append(seat);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"uids\":[");
      for (int index = 0; index < uid.Length; ++index)
      {
        stringBuilder.Append("\"" + uid[index] + "\"");
        if (index != uid.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
