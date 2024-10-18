// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexingExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.UtilityScripts
{
  public static class PlayerRoomIndexingExtensions
  {
    public static int GetRoomIndex(this PhotonPlayer player)
    {
      if (!((Object) PlayerRoomIndexing.instance == (Object) null))
        return PlayerRoomIndexing.instance.GetRoomIndex(player);
      Debug.LogError((object) "Missing PlayerRoomIndexing Component in Scene");
      return -1;
    }
  }
}
