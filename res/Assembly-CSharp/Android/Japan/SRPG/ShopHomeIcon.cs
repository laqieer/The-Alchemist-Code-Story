// Decompiled with JetBrains decompiler
// Type: SRPG.ShopHomeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class ShopHomeIcon : MonoBehaviour
  {
    public GameObject ShopIcon;
    public GameObject GuerrillaIcon;

    private void Start()
    {
      bool flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
      if ((UnityEngine.Object) this.ShopIcon != (UnityEngine.Object) null)
        this.ShopIcon.SetActive(!flag);
      if (!((UnityEngine.Object) this.GuerrillaIcon != (UnityEngine.Object) null))
        return;
      this.GuerrillaIcon.SetActive(flag);
    }
  }
}
