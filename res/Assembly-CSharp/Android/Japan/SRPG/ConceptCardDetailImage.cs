// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailImage : MonoBehaviour
  {
    public RawImage_Transparent Image;

    private void Start()
    {
      if ((UnityEngine.Object) ConceptCardManager.Instance == (UnityEngine.Object) null)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      ConceptCardData conceptCardData = instance.SelectedConceptCardMaterialData == null ? instance.SelectedConceptCardData : instance.SelectedConceptCardMaterialData;
      if (!((UnityEngine.Object) this.Image != (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.Image, AssetPath.ConceptCard(conceptCardData.Param));
    }
  }
}
