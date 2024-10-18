// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
          ((Component) this).gameObject.SetActive(false);
          break;
      }
    }
  }
}
