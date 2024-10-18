// Decompiled with JetBrains decompiler
// Type: InRoomTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class InRoomTime : MonoBehaviour
{
  private int roomStartTimestamp;
  private const string StartTimeKey = "#rt";

  public double RoomTime => (double) (uint) this.RoomTimestamp / 1000.0;

  public int RoomTimestamp
  {
    get => PhotonNetwork.inRoom ? PhotonNetwork.ServerTimestamp - this.roomStartTimestamp : 0;
  }

  public bool IsRoomTimeSet
  {
    get
    {
      return PhotonNetwork.inRoom && ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "#rt");
    }
  }

  [DebuggerHidden]
  internal IEnumerator SetRoomStartTimestamp()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new InRoomTime.\u003CSetRoomStartTimestamp\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  public void OnJoinedRoom() => this.StartCoroutine("SetRoomStartTimestamp");

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    this.StartCoroutine("SetRoomStartTimestamp");
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "#rt"))
      return;
    this.roomStartTimestamp = (int) propertiesThatChanged[(object) "#rt"];
  }
}
