// Decompiled with JetBrains decompiler
// Type: SRPG.GachaPickupParts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaPickupParts : MonoBehaviour
  {
    [Header("切り替え画像オブジェクト")]
    [SerializeField]
    private RawImage m_Image;

    public RawImage Image => this.m_Image;

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
        return;
      this.m_Image.texture = (Texture) null;
    }
  }
}
