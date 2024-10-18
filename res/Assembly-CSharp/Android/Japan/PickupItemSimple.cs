// Decompiled with JetBrains decompiler
// Type: PickupItemSimple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class PickupItemSimple : Photon.MonoBehaviour
{
  public float SecondsBeforeRespawn = 2f;
  public bool PickupOnCollide;
  public bool SentPickup;

  public void OnTriggerEnter(Collider other)
  {
    PhotonView component = other.GetComponent<PhotonView>();
    if (!this.PickupOnCollide || !((Object) component != (Object) null) || !component.isMine)
      return;
    this.Pickup();
  }

  public void Pickup()
  {
    if (this.SentPickup)
      return;
    this.SentPickup = true;
    this.photonView.RPC("PunPickupSimple", PhotonTargets.AllViaServer);
  }

  [PunRPC]
  public void PunPickupSimple(PhotonMessageInfo msgInfo)
  {
    if (!this.SentPickup || !msgInfo.sender.IsLocal || !this.gameObject.GetActive())
      ;
    this.SentPickup = false;
    if (!this.gameObject.GetActive())
    {
      Debug.Log((object) ("Ignored PU RPC, cause item is inactive. " + (object) this.gameObject));
    }
    else
    {
      float time = this.SecondsBeforeRespawn - (float) (PhotonNetwork.time - msgInfo.timestamp);
      if ((double) time <= 0.0)
        return;
      this.gameObject.SetActive(false);
      this.Invoke("RespawnAfter", time);
    }
  }

  public void RespawnAfter()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.gameObject.SetActive(true);
  }
}
