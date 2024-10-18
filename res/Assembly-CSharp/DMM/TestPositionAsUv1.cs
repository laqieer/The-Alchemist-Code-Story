// Decompiled with JetBrains decompiler
// Type: TestPositionAsUv1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class TestPositionAsUv1 : PositionAsUV1
{
  protected TestPositionAsUv1()
  {
  }

  public virtual void ModifyMesh(VertexHelper vh)
  {
    UIVertex uiVertex = new UIVertex();
    for (int index = 0; index < vh.currentVertCount; ++index)
    {
      vh.PopulateUIVertex(ref uiVertex, index);
      uiVertex.uv1 = new Vector2(uiVertex.position.x, uiVertex.position.y);
      vh.SetUIVertex(uiVertex, index);
    }
  }

  public virtual void ModifyMesh(Mesh mesh) => ((BaseMeshEffect) this).ModifyMesh(mesh);
}
