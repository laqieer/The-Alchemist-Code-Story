// Decompiled with JetBrains decompiler
// Type: OpJoinRandomRoomParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;

internal class OpJoinRandomRoomParams
{
  public Hashtable ExpectedCustomRoomProperties;
  public byte ExpectedMaxPlayers;
  public MatchmakingMode MatchingType;
  public TypedLobby TypedLobby;
  public string SqlLobbyFilter;
  public string[] ExpectedUsers;
}
