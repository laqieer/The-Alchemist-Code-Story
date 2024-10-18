﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlMultiTwReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlMultiTwReq : WebAPI
  {
    public ReqBtlMultiTwReq(string iname, int partyIndex, int plid, int seat, string[] uid, Network.ResponseCallback response)
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
