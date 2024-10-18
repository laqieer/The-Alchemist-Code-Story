// Decompiled with JetBrains decompiler
// Type: PickupItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class PickupItem : Photon.MonoBehaviour, IPunObservable
{
  public static HashSet<PickupItem> DisabledPickupItems = new HashSet<PickupItem>();
  public float SecondsBeforeRespawn = 2f;
  public bool PickupOnTrigger;
  public bool PickupIsMine;
  public UnityEngine.MonoBehaviour OnPickedUpCall;
  public bool SentPickup;
  public double TimeOfRespawn;

  public int ViewID
  {
    get
    {
      return this.photonView.viewID;
    }
  }

  public void OnTriggerEnter(Collider other)
  {
    PhotonView component = other.GetComponent<PhotonView>();
    if (!this.PickupOnTrigger || !((Object) component != (Object) null) || !component.isMine)
      return;
    this.Pickup();
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting && (double) this.SecondsBeforeRespawn <= 0.0)
      stream.SendNext((object) this.gameObject.transform.position);
    else
      this.gameObject.transform.position = (Vector3) stream.ReceiveNext();
  }

  public void Pickup()
  {
    if (this.SentPickup)
      return;
    this.SentPickup = true;
    this.photonView.RPC("PunPickup", PhotonTargets.AllViaServer);
  }

  public void Drop()
  {
    if (!this.PickupIsMine)
      return;
    this.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer);
  }

  public void Drop(Vector3 newPosition)
  {
    if (!this.PickupIsMine)
      return;
    this.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, new object[1]
    {
      (object) newPosition
    });
  }

  [PunRPC]
  public void PunPickup(PhotonMessageInfo msgInfo)
  {
    if (msgInfo.sender.IsLocal)
      this.SentPickup = false;
    if (!this.gameObject.GetActive())
    {
      Debug.Log((object) ("Ignored PU RPC, cause item is inactive. " + (object) this.gameObject + " SecondsBeforeRespawn: " + (object) this.SecondsBeforeRespawn + " TimeOfRespawn: " + (object) this.TimeOfRespawn + " respawn in future: " + (object) (this.TimeOfRespawn > PhotonNetwork.time)));
    }
    else
    {
      this.PickupIsMine = msgInfo.sender.IsLocal;
      if ((Object) this.OnPickedUpCall != (Object) null)
        this.OnPickedUpCall.SendMessage("OnPickedUp", (object) this);
      if ((double) this.SecondsBeforeRespawn <= 0.0)
      {
        this.PickedUp(0.0f);
      }
      else
      {
        double num = (double) this.SecondsBeforeRespawn - (PhotonNetwork.time - msgInfo.timestamp);
        if (num <= 0.0)
          return;
        this.PickedUp((float) num);
      }
    }
  }

  internal void PickedUp(float timeUntilRespawn)
  {
    this.gameObject.SetActive(false);
    PickupItem.DisabledPickupItems.Add(this);
    this.TimeOfRespawn = 0.0;
    if ((double) timeUntilRespawn <= 0.0)
      return;
    this.TimeOfRespawn = PhotonNetwork.time + (double) timeUntilRespawn;
    this.Invoke("PunRespawn", timeUntilRespawn);
  }

  [PunRPC]
  internal void PunRespawn(Vector3 pos)
  {
    Debug.Log((object) "PunRespawn with Position.");
    this.PunRespawn();
    this.gameObject.transform.position = pos;
  }

  [PunRPC]
  internal void PunRespawn()
  {
    PickupItem.DisabledPickupItems.Remove(this);
    this.TimeOfRespawn = 0.0;
    this.PickupIsMine = false;
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.gameObject.SetActive(true);
  }
}
