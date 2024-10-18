// Decompiled with JetBrains decompiler
// Type: TurnExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

#nullable disable
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
    room.SetCustomProperties(propertiesToSet);
  }

  public static int GetTurn(this RoomInfo room)
  {
    return room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnPropKey) ? 0 : (int) room.CustomProperties[(object) TurnExtensions.TurnPropKey];
  }

  public static int GetTurnStart(this RoomInfo room)
  {
    return room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnStartPropKey) ? 0 : (int) room.CustomProperties[(object) TurnExtensions.TurnStartPropKey];
  }

  public static int GetFinishedTurn(this PhotonPlayer player)
  {
    Room room = PhotonNetwork.room;
    if (room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnPropKey))
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
    });
  }
}
