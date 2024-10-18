// Decompiled with JetBrains decompiler
// Type: InRoomRoundTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InRoomRoundTimer : MonoBehaviour
{
  public int SecondsPerTurn = 5;
  public double StartTime;
  public Rect TextPos = new Rect(0.0f, 80f, 150f, 300f);
  private bool startRoundWhenTimeIsSynced;
  private const string StartTimeKey = "st";

  private void StartRoundNow()
  {
    if (PhotonNetwork.time < 9.9999997473787516E-05)
    {
      this.startRoundWhenTimeIsSynced = true;
    }
    else
    {
      this.startRoundWhenTimeIsSynced = false;
      PhotonNetwork.room.SetCustomProperties(new Hashtable()
      {
        [(object) "st"] = (object) PhotonNetwork.time
      });
    }
  }

  public void OnJoinedRoom()
  {
    if (PhotonNetwork.isMasterClient)
      this.StartRoundNow();
    else
      Debug.Log((object) ("StartTime already set: " + (object) ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "st")));
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "st"))
      return;
    this.StartTime = (double) propertiesThatChanged[(object) "st"];
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "st"))
      return;
    Debug.Log((object) "The new master starts a new round, cause we didn't start yet.");
    this.StartRoundNow();
  }

  private void Update()
  {
    if (!this.startRoundWhenTimeIsSynced)
      return;
    this.StartRoundNow();
  }

  public void OnGUI()
  {
    double num1 = PhotonNetwork.time - this.StartTime;
    double num2 = (double) this.SecondsPerTurn - num1 % (double) this.SecondsPerTurn;
    int num3 = (int) (num1 / (double) this.SecondsPerTurn);
    GUILayout.BeginArea(this.TextPos);
    GUILayout.Label(string.Format("elapsed: {0:0.000}", (object) num1), new GUILayoutOption[0]);
    GUILayout.Label(string.Format("remaining: {0:0.000}", (object) num2), new GUILayoutOption[0]);
    GUILayout.Label(string.Format("turn: {0:0}", (object) num3), new GUILayoutOption[0]);
    if (GUILayout.Button("new round", new GUILayoutOption[0]))
      this.StartRoundNow();
    GUILayout.EndArea();
  }
}
