﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComRecord
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;

namespace SRPG
{
  public class ReqBtlComRecord : WebAPI
  {
    public ReqBtlComRecord(string questIname, int page, int id, Network.ResponseCallback response)
    {
      this.name = "btl/com/record";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"iname\":\"" + questIname + "\"");
      if (page > 1)
      {
        stringBuilder.Append(",\"id\":" + (object) id);
        stringBuilder.Append(",\"page\":" + (object) page);
      }
      this.body = ReqBtlComRecord.CreateRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    private static string CreateRequestString(string body)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{\"ticket\":" + (object) Network.TicketID + ",");
      stringBuilder.Append("\"access_token\":\"" + Network.SessionID + "\",");
      stringBuilder.Append("\"device_id\":\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
      if (!string.IsNullOrEmpty(body))
        stringBuilder.Append(",\"param\":{" + body + "}");
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
