// Decompiled with JetBrains decompiler
// Type: SRPG.ResultMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ResultMask : MonoBehaviour
  {
    public RawImage ImgBg;

    public void SetBg(Texture2D tex)
    {
      if (!Object.op_Implicit((Object) this.ImgBg) || Object.op_Equality((Object) tex, (Object) null))
        return;
      this.ImgBg.texture = (Texture) tex;
      ((Component) this.ImgBg).gameObject.SetActive(true);
    }
  }
}
