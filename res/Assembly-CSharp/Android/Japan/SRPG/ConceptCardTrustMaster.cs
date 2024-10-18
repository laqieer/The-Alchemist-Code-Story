// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.mCardImage != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImage, path);
      if (!((UnityEngine.Object) this.mCardImageAdd != (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImageAdd, path);
    }
  }
}
