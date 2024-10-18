// Decompiled with JetBrains decompiler
// Type: OnAwakeUsePhotonView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class OnAwakeUsePhotonView : Photon.MonoBehaviour
{
  private void Awake()
  {
    if (!this.photonView.isMine)
      return;
    this.photonView.RPC("OnAwakeRPC", PhotonTargets.All);
  }

  private void Start()
  {
    if (!this.photonView.isMine)
      return;
    this.photonView.RPC("OnAwakeRPC", PhotonTargets.All, new object[1]
    {
      (object) (byte) 1
    });
  }

  [PunRPC]
  public void OnAwakeRPC()
  {
    Debug.Log((object) ("RPC: 'OnAwakeRPC' PhotonView: " + (object) this.photonView));
  }

  [PunRPC]
  public void OnAwakeRPC(byte myParameter)
  {
    Debug.Log((object) ("RPC: 'OnAwakeRPC' Parameter: " + (object) myParameter + " PhotonView: " + (object) this.photonView));
  }
}
