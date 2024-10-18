// Decompiled with JetBrains decompiler
// Type: PickupItemSimple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class PickupItemSimple : MonoBehaviour
{
  public float SecondsBeforeRespawn = 2f;
  public bool PickupOnCollide;
  public bool SentPickup;

  public void OnTriggerEnter(Collider other)
  {
    PhotonView component = ((Component) other).GetComponent<PhotonView>();
    if (!this.PickupOnCollide || !Object.op_Inequality((Object) component, (Object) null) || !component.isMine)
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
    if (!this.SentPickup || !msgInfo.sender.IsLocal || !((Component) this).gameObject.GetActive())
      ;
    this.SentPickup = false;
    if (!((Component) this).gameObject.GetActive())
    {
      Debug.Log((object) ("Ignored PU RPC, cause item is inactive. " + (object) ((Component) this).gameObject));
    }
    else
    {
      float num = this.SecondsBeforeRespawn - (float) (PhotonNetwork.time - msgInfo.timestamp);
      if ((double) num <= 0.0)
        return;
      ((Component) this).gameObject.SetActive(false);
      this.Invoke("RespawnAfter", num);
    }
  }

  public void RespawnAfter()
  {
    if (!Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
      return;
    ((Component) this).gameObject.SetActive(true);
  }
}
