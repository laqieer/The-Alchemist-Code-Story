// Decompiled with JetBrains decompiler
// Type: Photon.MonoBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Photon
{
  public class MonoBehaviour : UnityEngine.MonoBehaviour
  {
    private PhotonView pvCache;

    public PhotonView photonView
    {
      get
      {
        if ((Object) this.pvCache == (Object) null)
          this.pvCache = PhotonView.Get((Component) this);
        return this.pvCache;
      }
    }
  }
}
