// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GalleryItem : MonoBehaviour
  {
    [SerializeField]
    private Button Button;
    [SerializeField]
    private GameObject UnknownImage;

    public void SetAvailable(bool isAvailable)
    {
      if ((UnityEngine.Object) this.Button != (UnityEngine.Object) null)
        this.Button.interactable = isAvailable;
      if (!((UnityEngine.Object) this.UnknownImage != (UnityEngine.Object) null))
        return;
      this.UnknownImage.SetActive(!isAvailable);
    }
  }
}
