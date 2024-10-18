// Decompiled with JetBrains decompiler
// Type: BatchChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
[ExecuteInEditMode]
public abstract class BatchChunk : MonoBehaviour
{
  public Mesh Mesh;
  public Material Material;

  private void Awake()
  {
    if (!Application.isPlaying)
      return;
    ((Behaviour) this).enabled = false;
  }

  public abstract void FillTriangles(
    int baseVertex,
    Vector3[] vertices,
    Vector3[] normals,
    Vector2[] uv,
    Color32[] colors,
    Vector3[] centers,
    int baseIndex,
    int[] indices);

  public virtual int VertexCount
  {
    get => Object.op_Inequality((Object) this.Mesh, (Object) null) ? this.Mesh.vertexCount : 0;
  }

  public virtual int IndexCount
  {
    get => Object.op_Inequality((Object) this.Mesh, (Object) null) ? this.Mesh.triangles.Length : 0;
  }
}
