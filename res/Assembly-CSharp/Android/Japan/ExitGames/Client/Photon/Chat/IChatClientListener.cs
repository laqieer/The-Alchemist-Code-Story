// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.IChatClientListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
  }
}
