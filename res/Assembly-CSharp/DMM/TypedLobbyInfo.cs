// Decompiled with JetBrains decompiler
// Type: TypedLobbyInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class TypedLobbyInfo : TypedLobby
{
  public int PlayerCount;
  public int RoomCount;

  public override string ToString()
  {
    return string.Format("TypedLobbyInfo '{0}'[{1}] rooms: {2} players: {3}", (object) this.Name, (object) this.Type, (object) this.RoomCount, (object) this.PlayerCount);
  }
}
