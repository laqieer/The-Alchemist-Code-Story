// Decompiled with JetBrains decompiler
// Type: OnClickDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class OnClickDestroy : Photon.MonoBehaviour
{
  public bool DestroyByRpc;

  public void OnClick()
  {
    if (!this.DestroyByRpc)
      PhotonNetwork.Destroy(this.gameObject);
    else
      this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
  }

  [PunRPC]
  [DebuggerHidden]
  public IEnumerator DestroyRpc()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new OnClickDestroy.\u003CDestroyRpc\u003Ec__Iterator2A()
    {
      \u003C\u003Ef__this = this
    };
  }
}
