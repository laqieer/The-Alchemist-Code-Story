// Decompiled with JetBrains decompiler
// Type: NullGraphic2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
