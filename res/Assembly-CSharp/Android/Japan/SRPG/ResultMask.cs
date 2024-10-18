// Decompiled with JetBrains decompiler
// Type: SRPG.ResultMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ResultMask : MonoBehaviour
  {
    public RawImage ImgBg;

    public void SetBg(Texture2D tex)
    {
      if (!(bool) ((UnityEngine.Object) this.ImgBg) || (UnityEngine.Object) tex == (UnityEngine.Object) null)
        return;
      this.ImgBg.texture = (Texture) tex;
      this.ImgBg.gameObject.SetActive(true);
    }
  }
}
