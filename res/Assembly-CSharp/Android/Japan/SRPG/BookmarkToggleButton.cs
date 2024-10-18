// Decompiled with JetBrains decompiler
// Type: SRPG.BookmarkToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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

    public void EnableShadow(bool enabled)
    {
      this.Shadow.SetActive(enabled);
    }
  }
}
