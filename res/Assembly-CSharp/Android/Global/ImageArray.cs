// Decompiled with JetBrains decompiler
// Type: ImageArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ImageArray")]
public class ImageArray : Image
{
  public Sprite[] Images = new Sprite[0];
  private int mImageIndex;

  public int ImageIndex
  {
    get
    {
      return this.mImageIndex;
    }
    set
    {
      if (0 <= value && value < this.Images.Length)
      {
        this.sprite = this.Images[value];
        this.mImageIndex = value;
      }
      else
        Debug.LogError((object) "範囲外のインデックスが指定されました。");
    }
  }

  protected override void OnPopulateMesh(VertexHelper toFill)
  {
    if ((Object) this.sprite == (Object) null)
      toFill.Clear();
    else
      base.OnPopulateMesh(toFill);
  }
}
