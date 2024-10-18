// Decompiled with JetBrains decompiler
// Type: SRPG.BookmarkToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class BookmarkToggleButton : MonoBehaviour
  {
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
