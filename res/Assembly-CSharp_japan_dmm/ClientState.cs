// Decompiled with JetBrains decompiler
// Type: ClientState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
