// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendInviteChatMessageWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqSendInviteChatMessageWorld : WebAPI
  {
    public ReqSendInviteChatMessageWorld(
      int channel,
      string message,
      Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/send/guild/invite";
      stringBuilder.Append("\"channel\":" + channel.ToString() + ",");
      stringBuilder.Append("\"message\":\"" + message + "\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
