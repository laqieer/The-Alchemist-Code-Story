// Decompiled with JetBrains decompiler
// Type: Image_Transparent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
