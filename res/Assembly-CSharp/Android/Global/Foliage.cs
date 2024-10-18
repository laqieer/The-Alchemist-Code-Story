// Decompiled with JetBrains decompiler
// Type: Foliage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Batch/Foliage")]
public class Foliage : BatchChunk
{
  public Color32 TopColor;
  public Color32 BottomColor;

  public override void FillTriangles(int baseVertex, Vector3[] vertices, Vector3[] normals, Vector2[] uv, Color32[] colors, Vector3[] centers, int baseIndex, int[] indices)
  {
    Vector3 position = this.transform.position;
    Matrix4x4 localToWorldMatrix = this.transform.localToWorldMatrix;
    float b1 = float.MaxValue;
    float b2 = float.MinValue;
    for (int index = 0; index < this.Mesh.vertexCount; ++index)
    {
      vertices[baseVertex + index] = localToWorldMatrix.MultiplyPoint(this.Mesh.vertices[index]);
      b1 = Mathf.Min(vertices[baseVertex + index].y, b1);
      b2 = Mathf.Max(vertices[baseVertex + index].y, b2);
    }
    if (normals != null)
    {
      for (int index = 0; index < this.Mesh.vertexCount; ++index)
        normals[baseVertex + index] = localToWorldMatrix.MultiplyVector(this.Mesh.normals[index]);
    }
    if (uv != null)
    {
      for (int index = 0; index < this.Mesh.vertexCount; ++index)
        uv[baseVertex + index] = this.Mesh.uv[index];
    }
    if (centers != null)
    {
      for (int index = 0; index < this.Mesh.vertexCount; ++index)
        centers[baseVertex + index] = position;
    }
    float num = b2 - b1;
    if (colors != null)
    {
      if (this.Mesh.colors32.Length == this.Mesh.vertexCount)
      {
        for (int index = 0; index < this.Mesh.vertexCount; ++index)
        {
          Color32 color32_1 = this.Mesh.colors32[index];
          Color32 color32_2 = Color32.Lerp(this.BottomColor, this.TopColor, (vertices[baseVertex + index].y - b1) / num);
          color32_1.r = (byte) ((int) color32_1.r * (int) color32_2.r / (int) byte.MaxValue);
          color32_1.g = (byte) ((int) color32_1.g * (int) color32_2.g / (int) byte.MaxValue);
          color32_1.b = (byte) ((int) color32_1.b * (int) color32_2.b / (int) byte.MaxValue);
          color32_1.a = (byte) ((int) color32_1.a * (int) color32_2.a / (int) byte.MaxValue);
          colors[baseVertex + index] = color32_1;
        }
      }
      else
      {
        for (int index = 0; index < this.Mesh.vertexCount; ++index)
          colors[baseVertex + index] = Color32.Lerp(this.BottomColor, this.TopColor, (vertices[baseVertex + index].y - b1) / num);
      }
    }
    for (int index = 0; index < this.Mesh.triangles.Length; ++index)
      indices[baseIndex + index] = this.Mesh.triangles[index] + baseVertex;
  }
}
