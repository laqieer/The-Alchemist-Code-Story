// Decompiled with JetBrains decompiler
// Type: SmoothSyncMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class SmoothSyncMovement : Photon.MonoBehaviour, IPunObservable
{
  public float SmoothingDelay = 5f;
  private Vector3 correctPlayerPos = Vector3.zero;
  private Quaternion correctPlayerRot = Quaternion.identity;

  public void Awake()
  {
    bool flag = false;
    foreach (Object observedComponent in this.photonView.ObservedComponents)
    {
      if (observedComponent == (Object) this)
      {
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    Debug.LogWarning((object) (this.ToString() + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used."));
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) this.transform.position);
      stream.SendNext((object) this.transform.rotation);
    }
    else
    {
      this.correctPlayerPos = (Vector3) stream.ReceiveNext();
      this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
    }
  }

  public void Update()
  {
    if (this.photonView.isMine)
      return;
    this.transform.position = Vector3.Lerp(this.transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
  }
}
