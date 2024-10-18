// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardTrustMaster : MonoBehaviour
  {
    [SerializeField]
    private RawImage mCardImage;
    [SerializeField]
    private RawImage mCardImageAdd;

    public void SetData(ConceptCardData data)
    {
      string path = AssetPath.ConceptCard(data.Param);
      if (Object.op_Inequality((Object) this.mCardImage, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImage, path);
      if (!Object.op_Inequality((Object) this.mCardImageAdd, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImageAdd, path);
    }
  }
}
