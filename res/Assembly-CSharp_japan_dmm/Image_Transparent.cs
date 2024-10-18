// Decompiled with JetBrains decompiler
// Type: Image_Transparent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/Image (透明)")]
public class Image_Transparent : Image
{
  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    if (Object.op_Inequality((Object) this.sprite, (Object) null))
      base.OnPopulateMesh(toFill);
    else
      toFill.Clear();
  }
}
