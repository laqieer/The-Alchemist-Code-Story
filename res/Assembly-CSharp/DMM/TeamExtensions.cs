// Decompiled with JetBrains decompiler
// Type: TeamExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class TeamExtensions
{
  public static PunTeams.Team GetTeam(this PhotonPlayer player)
  {
    object obj;
    return ((Dictionary<object, object>) player.CustomProperties).TryGetValue((object) "team", out obj) ? (PunTeams.Team) obj : PunTeams.Team.none;
  }

  public static void SetTeam(this PhotonPlayer player, PunTeams.Team team)
  {
    if (!PhotonNetwork.connectedAndReady)
    {
      Debug.LogWarning((object) ("JoinTeam was called in state: " + (object) PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady."));
    }
    else
    {
      if (player.GetTeam() == team)
        return;
      PhotonPlayer photonPlayer = player;
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) nameof (team), (object) (byte) team);
      Hashtable propertiesToSet = hashtable;
      photonPlayer.SetCustomProperties(propertiesToSet);
    }
  }
}
