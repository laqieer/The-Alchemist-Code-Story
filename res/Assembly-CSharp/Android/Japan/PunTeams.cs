// Decompiled with JetBrains decompiler
// Type: PunTeams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunTeams : MonoBehaviour
{
  public static Dictionary<PunTeams.Team, List<PhotonPlayer>> PlayersPerTeam;
  public const string TeamPlayerProp = "team";

  public void Start()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
    IEnumerator enumerator = Enum.GetValues(typeof (PunTeams.Team)).GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        PunTeams.PlayersPerTeam[(PunTeams.Team) current] = new List<PhotonPlayer>();
      }
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
  }

  public void OnDisable()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
  }

  public void OnJoinedRoom()
  {
    this.UpdateTeams();
  }

  public void OnLeftRoom()
  {
    this.Start();
  }

  public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
  {
    this.UpdateTeams();
  }

  public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
  {
    this.UpdateTeams();
  }

  public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
  {
    this.UpdateTeams();
  }

  public void UpdateTeams()
  {
    IEnumerator enumerator = Enum.GetValues(typeof (PunTeams.Team)).GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        PunTeams.PlayersPerTeam[(PunTeams.Team) current].Clear();
      }
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
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
