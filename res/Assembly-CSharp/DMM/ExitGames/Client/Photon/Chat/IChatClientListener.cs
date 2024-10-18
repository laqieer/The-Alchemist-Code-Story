// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.IChatClientListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public interface IChatClientListener
  {
    void DebugReturn(DebugLevel level, string message);

    void OnDisconnected();

    void OnConnected();

    void OnChatStateChange(ChatState state);

    void OnGetMessages(string channelName, string[] senders, object[] messages);

    void OnPrivateMessage(string sender, object message, string channelName);

    void OnSubscribed(string[] channels, bool[] results);

    void OnUnsubscribed(string[] channels);

    void OnStatusUpdate(string user, int status, bool gotMessage, object message);

    void OnUserSubscribed(string channel, string user);

    void OnUserUnsubscribed(string channel, string user);
  }
}
