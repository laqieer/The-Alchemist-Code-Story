// Decompiled with JetBrains decompiler
// Type: SRPG.BookmarkToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BookmarkToggleButton : MonoBehaviour
  {
    [SerializeField]
    public Text Text;
    [SerializeField]
    private GameObject OnImage;
    [SerializeField]
    private GameObject OffImage;
    [SerializeField]
    private GameObject Shadow;

    public void Activate(bool doActivate)
    {
      this.OnImage.SetActive(doActivate);
      this.OffImage.SetActive(!doActivate);
    }

    public void EnableShadow(bool enabled) => this.Shadow.SetActive(enabled);
  }
}
