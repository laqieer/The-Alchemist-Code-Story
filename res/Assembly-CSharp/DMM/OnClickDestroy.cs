// Decompiled with JetBrains decompiler
// Type: OnClickDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class OnClickDestroy : MonoBehaviour
{
  public bool DestroyByRpc;

  public void OnClick()
  {
    if (!this.DestroyByRpc)
      PhotonNetwork.Destroy(((Component) this).gameObject);
    else
      this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
  }

  [DebuggerHidden]
  [PunRPC]
  public IEnumerator DestroyRpc()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new OnClickDestroy.\u003CDestroyRpc\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }
}
