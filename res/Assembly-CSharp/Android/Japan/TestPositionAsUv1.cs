// Decompiled with JetBrains decompiler
// Type: TestPositionAsUv1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class TestPositionAsUv1 : PositionAsUV1
{
  protected TestPositionAsUv1()
  {
  }

  public override void ModifyMesh(VertexHelper vh)
  {
    UIVertex vertex = new UIVertex();
    for (int i = 0; i < vh.currentVertCount; ++i)
    {
      vh.PopulateUIVertex(ref vertex, i);
      vertex.uv1 = new Vector2(vertex.position.x, vertex.position.y);
      vh.SetUIVertex(vertex, i);
    }
  }

  public override void ModifyMesh(Mesh mesh)
  {
    base.ModifyMesh(mesh);
  }
}
