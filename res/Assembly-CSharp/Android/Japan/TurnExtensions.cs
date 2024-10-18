// Decompiled with JetBrains decompiler
// Type: TurnExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;

public static class TurnExtensions
{
  public static readonly string TurnPropKey = "Turn";
  public static readonly string TurnStartPropKey = "TStart";
  public static readonly string FinishedTurnPropKey = "FToA";

  public static void SetTurn(this Room room, int turn, bool setStartTime = false)
  {
    if (room == null || room.CustomProperties == null)
      return;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet[(object) TurnExtensions.TurnPropKey] = (object) turn;
    if (setStartTime)
      propertiesToSet[(object) TurnExtensions.TurnStartPropKey] = (object) PhotonNetwork.ServerTimestamp;
    room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
  }

  public static int GetTurn(this RoomInfo room)
  {
    if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey((object) TurnExtensions.TurnPropKey))
      return 0;
    return (int) room.CustomProperties[(object) TurnExtensions.TurnPropKey];
  }

  public static int GetTurnStart(this RoomInfo room)
  {
    if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey((object) TurnExtensions.TurnStartPropKey))
      return 0;
    return (int) room.CustomProperties[(object) TurnExtensions.TurnStartPropKey];
  }

  public static int GetFinishedTurn(this PhotonPlayer player)
  {
    Room room = PhotonNetwork.room;
    if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey((object) TurnExtensions.TurnPropKey))
      return 0;
    string str = TurnExtensions.FinishedTurnPropKey + (object) player.ID;
    return (int) room.CustomProperties[(object) str];
  }

  public static void SetFinishedTurn(this PhotonPlayer player, int turn)
  {
    Room room = PhotonNetwork.room;
    if (room == null || room.CustomProperties == null)
      return;
    string str = TurnExtensions.FinishedTurnPropKey + (object) player.ID;
    room.SetCustomProperties(new Hashtable()
    {
      [(object) str] = (object) turn
    }, (Hashtable) null, false);
  }
}
