﻿// Decompiled with JetBrains decompiler
// Type: PhotonRigidbodyView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
[RequireComponent(typeof (Rigidbody))]
[AddComponentMenu("Photon Networking/Photon Rigidbody View")]
public class PhotonRigidbodyView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  private bool m_SynchronizeVelocity = true;
  [SerializeField]
  private bool m_SynchronizeAngularVelocity = true;
  private Rigidbody m_Body;

  private void Awake()
  {
    this.m_Body = this.GetComponent<Rigidbody>();
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      if (this.m_SynchronizeVelocity)
        stream.SendNext((object) this.m_Body.velocity);
      if (!this.m_SynchronizeAngularVelocity)
        return;
      stream.SendNext((object) this.m_Body.angularVelocity);
    }
    else
    {
      if (this.m_SynchronizeVelocity)
        this.m_Body.velocity = (Vector3) stream.ReceiveNext();
      if (!this.m_SynchronizeAngularVelocity)
        return;
      this.m_Body.angularVelocity = (Vector3) stream.ReceiveNext();
    }
  }
}
