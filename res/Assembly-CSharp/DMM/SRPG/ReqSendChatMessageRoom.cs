// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSendChatMessageRoom : WebAPI
  {
    public ReqSendChatMessageRoom(
      string room_token,
      string message,
      string[] uids,
      Network.ResponseCallback response)
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
