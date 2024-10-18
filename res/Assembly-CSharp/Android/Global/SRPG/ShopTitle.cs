// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        default:
          this.gameObject.SetActive(false);
          break;
      }
    }
  }
}
