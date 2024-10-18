// Decompiled with JetBrains decompiler
// Type: InRoomTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class InRoomTime : MonoBehaviour
{
  private int roomStartTimestamp;
  private const string StartTimeKey = "#rt";

  public double RoomTime
  {
    get
    {
      return (double) (uint) this.RoomTimestamp / 1000.0;
    }
  }

  public int RoomTimestamp
  {
    get
    {
      if (PhotonNetwork.inRoom)
        return PhotonNetwork.ServerTimestamp - this.roomStartTimestamp;
      return 0;
    }
  }

  public bool IsRoomTimeSet
  {
    get
    {
      if (PhotonNetwork.inRoom)
        return PhotonNetwork.room.CustomProperties.ContainsKey((object) "#rt");
      return false;
    }
  }

  [DebuggerHidden]
  internal IEnumerator SetRoomStartTimestamp()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new InRoomTime.\u003CSetRoomStartTimestamp\u003Ec__Iterator0() { \u0024this = this };
  }

  public void OnJoinedRoom()
  {
    this.StartCoroutine("SetRoomStartTimestamp");
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    this.StartCoroutine("SetRoomStartTimestamp");
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!propertiesThatChanged.ContainsKey((object) "#rt"))
      return;
    this.roomStartTimestamp = (int) propertiesThatChanged[(object) "#rt"];
  }
}
