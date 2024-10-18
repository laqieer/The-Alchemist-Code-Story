// Decompiled with JetBrains decompiler
// Type: EnterRoomParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
