// Decompiled with JetBrains decompiler
// Type: NullGraphic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
