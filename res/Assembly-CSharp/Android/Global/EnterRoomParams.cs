// Decompiled with JetBrains decompiler
// Type: EnterRoomParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;

internal class EnterRoomParams
{
  public bool OnGameServer = true;
  public string RoomName;
  public RoomOptions RoomOptions;
  public TypedLobby Lobby;
  public Hashtable PlayerProperties;
  public bool CreateIfNotExists;
  public bool RejoinOnly;
  public string[] ExpectedUsers;
}
