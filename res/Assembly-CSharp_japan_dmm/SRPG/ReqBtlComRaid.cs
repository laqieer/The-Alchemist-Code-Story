﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlComRaid : WebAPI
  {
    public ReqBtlComRaid(
      string iname,
      int ticket,
      Network.ResponseCallback response,
      int partyIndex)
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
