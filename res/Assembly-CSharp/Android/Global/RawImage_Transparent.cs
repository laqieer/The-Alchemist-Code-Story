// Decompiled with JetBrains decompiler
// Type: RawImage_Transparent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/RawImage (透明)")]
public class RawImage_Transparent : RawImage
{
  public string Preview;

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    if ((Object) this.texture != (Object) null)
      base.OnPopulateMesh(vh);
    else
      vh.Clear();
  }
}
