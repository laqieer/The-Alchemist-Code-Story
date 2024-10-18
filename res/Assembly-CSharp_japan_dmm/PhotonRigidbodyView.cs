// Decompiled with JetBrains decompiler
// Type: PhotonRigidbodyView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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

  private void Awake() => this.m_Body = ((Component) this).GetComponent<Rigidbody>();

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
