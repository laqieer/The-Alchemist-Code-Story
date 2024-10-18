// Decompiled with JetBrains decompiler
// Type: EnterRoomParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;

#nullable disable
internal class EnterRoomParams
{
  public string RoomName;
  public RoomOptions RoomOptions;
  public TypedLobby Lobby;
  public Hashtable PlayerProperties;
  public bool OnGameServer = true;
  public bool CreateIfNotExists;
  public bool RejoinOnly;
  public string[] ExpectedUsers;
}
