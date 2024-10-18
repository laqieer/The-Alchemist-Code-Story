// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexingExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace ExitGames.UtilityScripts
{
  public static class PlayerRoomIndexingExtensions
  {
    public static int GetRoomIndex(this PhotonPlayer player)
    {
      if (!Object.op_Equality((Object) PlayerRoomIndexing.instance, (Object) null))
        return PlayerRoomIndexing.instance.GetRoomIndex(player);
      Debug.LogError((object) "Missing PlayerRoomIndexing Component in Scene");
      return -1;
    }
  }
}
