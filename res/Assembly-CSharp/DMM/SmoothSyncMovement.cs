// Decompiled with JetBrains decompiler
// Type: SmoothSyncMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class SmoothSyncMovement : MonoBehaviour, IPunObservable
{
  public float SmoothingDelay = 5f;
  private Vector3 correctPlayerPos = Vector3.zero;
  private Quaternion correctPlayerRot = Quaternion.identity;

  public void Awake()
  {
    bool flag = false;
    foreach (Object observedComponent in this.photonView.ObservedComponents)
    {
      if (Object.op_Equality(observedComponent, (Object) this))
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
      stream.SendNext((object) ((Component) this).transform.position);
      stream.SendNext((object) ((Component) this).transform.rotation);
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
    ((Component) this).transform.position = Vector3.Lerp(((Component) this).transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
    ((Component) this).transform.rotation = Quaternion.Lerp(((Component) this).transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
  }
}
