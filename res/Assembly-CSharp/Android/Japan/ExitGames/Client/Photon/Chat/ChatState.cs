// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public enum ChatState
  {
    Uninitialized,
    ConnectingToNameServer,
    ConnectedToNameServer,
    Authenticating,
    Authenticated,
    DisconnectingFromNameServer,
    ConnectingToFrontEnd,
    ConnectedToFrontEnd,
    DisconnectingFromFrontEnd,
    QueuedComingFromFrontEnd,
    Disconnecting,
    Disconnected,
  }
}
