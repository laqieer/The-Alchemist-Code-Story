// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqSendChatMessageRoom : WebAPI
  {
    public ReqSendChatMessageRoom(string room_token, string message, string[] uids, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/room/send";
      stringBuilder.Append("\"room_token\":\"" + room_token + "\",");
      stringBuilder.Append("\"message\":\"" + message + "\",");
      stringBuilder.Append("\"uids\":[");
      for (int index = 0; index < uids.Length; ++index)
      {
        stringBuilder.Append("\"" + uids[index] + "\"");
        if (index != uids.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
