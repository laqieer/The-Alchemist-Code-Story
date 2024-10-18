// Decompiled with JetBrains decompiler
// Type: SmoothSyncMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
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
    using (List<Component>.Enumerator enumerator = this.photonView.ObservedComponents.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        if ((Object) enumerator.Current == (Object) this)
        {
          flag = true;
          break;
        }
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
