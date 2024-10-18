// Decompiled with JetBrains decompiler
// Type: Image_Transparent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Image (透明)")]
public class Image_Transparent : Image
{
  protected override void OnPopulateMesh(VertexHelper toFill)
  {
    if ((Object) this.sprite != (Object) null)
      base.OnPopulateMesh(toFill);
    else
      toFill.Clear();
  }
}
