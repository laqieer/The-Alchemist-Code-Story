﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqChatMessageRoom : WebAPI
  {
    public ReqChatMessageRoom(long start_id, string room_token, int limit, long exclude_id, bool isMultiPush, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/room/message";
      stringBuilder.Append("\"start_id\":" + start_id.ToString() + ",");
      if (!string.IsNullOrEmpty(room_token))
        stringBuilder.Append("\"room_token\":\"" + room_token + "\",");
      stringBuilder.Append("\"limit\":" + limit.ToString() + ",");
      stringBuilder.Append("\"exclude_id\":" + exclude_id.ToString());
      if (isMultiPush)
        stringBuilder.Append(",\"is_multi_push\":1");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
