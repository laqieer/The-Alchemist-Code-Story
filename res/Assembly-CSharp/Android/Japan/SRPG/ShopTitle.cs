// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ShopTitle : MonoBehaviour
  {
    public ImageArray IamgeArray;

    private void Start()
    {
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
          this.IamgeArray.ImageIndex = 0;
          break;
        case EShopType.Tabi:
          this.IamgeArray.ImageIndex = 1;
          break;
        case EShopType.Kimagure:
          this.IamgeArray.ImageIndex = 2;
          break;
        case EShopType.Guerrilla:
          this.IamgeArray.ImageIndex = 3;
          break;
        default:
          this.gameObject.SetActive(false);
          break;
      }
    }
  }
}
