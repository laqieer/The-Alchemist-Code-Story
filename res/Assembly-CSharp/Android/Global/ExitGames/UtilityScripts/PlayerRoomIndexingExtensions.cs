// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexingExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
