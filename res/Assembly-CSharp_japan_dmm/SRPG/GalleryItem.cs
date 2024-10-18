// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Inequality((Object) this.Button, (Object) null))
        ((Selectable) this.Button).interactable = isAvailable;
      if (!Object.op_Inequality((Object) this.UnknownImage, (Object) null))
        return;
      this.UnknownImage.SetActive(!isAvailable);
    }
  }
}
