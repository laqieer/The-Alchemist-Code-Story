// Decompiled with JetBrains decompiler
// Type: NullGraphic2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NullGraphic2 : Graphic
{
  protected override void Start()
  {
    base.Start();
    this.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    base.OnPopulateMesh(vh);
  }
}
