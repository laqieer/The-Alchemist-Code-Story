﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopText : MonoBehaviour
  {
    public string Normal;
    public string Tabi;
    public string Kimagure;
    public string Monozuki;
    public string Tour;
    public string Arena;
    public string Multi;
    public string AwakePiece;
    public string Artifact;
    public string Limited;
    private string mTextID;

    private void LateUpdate()
    {
      if (this.mTextID != null)
        return;
      Text component = this.GetComponent<Text>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
          this.mTextID = this.Normal;
          break;
        case EShopType.Tabi:
          this.mTextID = this.Tabi;
          break;
        case EShopType.Kimagure:
          this.mTextID = this.Kimagure;
          break;
        case EShopType.Monozuki:
          this.mTextID = this.Monozuki;
          break;
        case EShopType.Tour:
          this.mTextID = this.Tour;
          break;
        case EShopType.Arena:
          this.mTextID = this.Arena;
          break;
        case EShopType.Multi:
          this.mTextID = this.Multi;
          break;
        case EShopType.AwakePiece:
          this.mTextID = this.AwakePiece;
          break;
        case EShopType.Artifact:
          this.mTextID = this.Artifact;
          break;
        case EShopType.Limited:
          this.mTextID = this.Limited;
          break;
      }
      if (string.IsNullOrEmpty(this.mTextID))
        this.mTextID = string.Empty;
      else
        component.text = LocalizedText.Get(this.mTextID);
    }
  }
}
