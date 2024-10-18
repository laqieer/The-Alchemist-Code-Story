// Decompiled with JetBrains decompiler
// Type: NullGraphic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NullGraphic : Graphic
{
  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
  }
}
