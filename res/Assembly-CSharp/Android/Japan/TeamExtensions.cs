// Decompiled with JetBrains decompiler
// Type: TeamExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public static class TeamExtensions
{
  public static PunTeams.Team GetTeam(this PhotonPlayer player)
  {
    object obj;
    if (player.CustomProperties.TryGetValue((object) "team", out obj))
      return (PunTeams.Team) obj;
    return PunTeams.Team.none;
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
      hashtable.Add((object) nameof (team), (object) team);
      Hashtable propertiesToSet = hashtable;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      photonPlayer.SetCustomProperties(propertiesToSet, (Hashtable) local, num != 0);
    }
  }
}
