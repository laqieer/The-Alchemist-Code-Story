// Decompiled with JetBrains decompiler
// Type: ClientState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public enum ClientState
{
  Uninitialized,
  PeerCreated,
  Queued,
  Authenticated,
  JoinedLobby,
  DisconnectingFromMasterserver,
  ConnectingToGameserver,
  ConnectedToGameserver,
  Joining,
  Joined,
  Leaving,
  DisconnectingFromGameserver,
  ConnectingToMasterserver,
  QueuedComingFromGameserver,
  Disconnecting,
  Disconnected,
  ConnectedToMaster,
  ConnectingToNameServer,
  ConnectedToNameServer,
  DisconnectingFromNameServer,
  Authenticating,
}
