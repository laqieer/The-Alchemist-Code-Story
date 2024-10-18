// Decompiled with JetBrains decompiler
// Type: Photon.MonoBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
