// Decompiled with JetBrains decompiler
// Type: OnClickDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

  [DebuggerHidden]
  [PunRPC]
  public IEnumerator DestroyRpc()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new OnClickDestroy.\u003CDestroyRpc\u003Ec__Iterator0() { \u0024this = this };
  }
}
