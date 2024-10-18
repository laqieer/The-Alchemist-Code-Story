// Decompiled with JetBrains decompiler
// Type: ImageArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/ImageArray")]
public class ImageArray : Image
{
  public Sprite[] Images = new Sprite[0];
  private int mImageIndex;

  public int ImageIndex
  {
    get => this.mImageIndex;
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

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    if (Object.op_Equality((Object) this.sprite, (Object) null))
      toFill.Clear();
    else
      base.OnPopulateMesh(toFill);
  }
}
