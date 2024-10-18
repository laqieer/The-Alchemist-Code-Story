// Decompiled with JetBrains decompiler
// Type: InRoomRoundTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public class InRoomRoundTimer : MonoBehaviour
{
  public int SecondsPerTurn = 5;
  public Rect TextPos = new Rect(0.0f, 80f, 150f, 300f);
  public double StartTime;
  private bool startRoundWhenTimeIsSynced;
  private const string StartTimeKey = "st";

  private void StartRoundNow()
  {
    if (PhotonNetwork.time < 9.99999974737875E-05)
    {
      this.startRoundWhenTimeIsSynced = true;
    }
    else
    {
      this.startRoundWhenTimeIsSynced = false;
      PhotonNetwork.room.SetCustomProperties(new Hashtable()
      {
        [(object) "st"] = (object) PhotonNetwork.time
      }, (Hashtable) null, false);
    }
  }

  public void OnJoinedRoom()
  {
    if (PhotonNetwork.isMasterClient)
      this.StartRoundNow();
    else
      Debug.Log((object) ("StartTime already set: " + (object) PhotonNetwork.room.CustomProperties.ContainsKey((object) "st")));
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!propertiesThatChanged.ContainsKey((object) "st"))
      return;
    this.StartTime = (double) propertiesThatChanged[(object) "st"];
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    if (PhotonNetwork.room.CustomProperties.ContainsKey((object) "st"))
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
    GUILayout.Label(string.Format("elapsed: {0:0.000}", (object) num1));
    GUILayout.Label(string.Format("remaining: {0:0.000}", (object) num2));
    GUILayout.Label(string.Format("turn: {0:0}", (object) num3));
    if (GUILayout.Button("new round"))
      this.StartRoundNow();
    GUILayout.EndArea();
  }
}
