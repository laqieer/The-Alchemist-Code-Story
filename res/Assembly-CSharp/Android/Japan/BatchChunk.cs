// Decompiled with JetBrains decompiler
// Type: BatchChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
    this.enabled = false;
  }

  public abstract void FillTriangles(int baseVertex, Vector3[] vertices, Vector3[] normals, Vector2[] uv, Color32[] colors, Vector3[] centers, int baseIndex, int[] indices);

  public virtual int VertexCount
  {
    get
    {
      if ((Object) this.Mesh != (Object) null)
        return this.Mesh.vertexCount;
      return 0;
    }
  }

  public virtual int IndexCount
  {
    get
    {
      if ((Object) this.Mesh != (Object) null)
        return this.Mesh.triangles.Length;
      return 0;
    }
  }
}
