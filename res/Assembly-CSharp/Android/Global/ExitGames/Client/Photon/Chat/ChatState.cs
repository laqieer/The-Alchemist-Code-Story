// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
