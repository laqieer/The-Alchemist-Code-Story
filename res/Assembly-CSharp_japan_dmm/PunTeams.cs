// Decompiled with JetBrains decompiler
// Type: PunTeams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PunTeams : MonoBehaviour
{
  public static Dictionary<PunTeams.Team, List<PhotonPlayer>> PlayersPerTeam;
  public const string TeamPlayerProp = "team";

  public void Start()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
    foreach (object key in Enum.GetValues(typeof (PunTeams.Team)))
      PunTeams.PlayersPerTeam[(PunTeams.Team) key] = new List<PhotonPlayer>();
  }

  public void OnDisable()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
  }

  public void OnJoinedRoom() => this.UpdateTeams();

  public void OnLeftRoom() => this.Start();

  public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) => this.UpdateTeams();

  public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) => this.UpdateTeams();

  public void OnPhotonPlayerConnected(PhotonPlayer newPlayer) => this.UpdateTeams();

  public void UpdateTeams()
  {
    foreach (object key in Enum.GetValues(typeof (PunTeams.Team)))
      PunTeams.PlayersPerTeam[(PunTeams.Team) key].Clear();
    for (int index = 0; index < PhotonNetwork.playerList.Length; ++index)
    {
      PhotonPlayer player = PhotonNetwork.playerList[index];
      PunTeams.Team team = player.GetTeam();
      PunTeams.PlayersPerTeam[team].Add(player);
    }
  }

  public enum Team : byte
  {
    none,
    red,
    blue,
  }
}
