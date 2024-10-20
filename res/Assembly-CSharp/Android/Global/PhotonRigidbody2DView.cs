﻿// Decompiled with JetBrains decompiler
// Type: PhotonRigidbody2DView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Photon Networking/Photon Rigidbody 2D View")]
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (PhotonView))]
public class PhotonRigidbody2DView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  private bool m_SynchronizeVelocity = true;
  [SerializeField]
  private bool m_SynchronizeAngularVelocity = true;
  private Rigidbody2D m_Body;

  private void Awake()
  {
    this.m_Body = this.GetComponent<Rigidbody2D>();
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
        this.m_Body.velocity = (Vector2) stream.ReceiveNext();
      if (!this.m_SynchronizeAngularVelocity)
        return;
      this.m_Body.angularVelocity = (float) stream.ReceiveNext();
    }
  }
}