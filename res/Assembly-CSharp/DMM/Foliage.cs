// Decompiled with JetBrains decompiler
// Type: Foliage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("Rendering/Batch/Foliage")]
public class Foliage : BatchChunk
{
  public Color32 TopColor;
  public Color32 BottomColor;

  public override void FillTriangles(
    int baseVertex,
    Vector3[] vertices,
    Vector3[] normals,
    Vector2[] uv,
    Color32[] colors,
    Vector3[] centers,
    int baseIndex,
    int[] indices)
  {
    Vector3 position = ((Component) this).transform.position;
    Matrix4x4 localToWorldMatrix = ((Component) this).transform.localToWorldMatrix;
    float num1 = float.MaxValue;
    float num2 = float.MinValue;
    for (int index = 0; index < this.Mesh.vertexCount; ++index)
    {
      vertices[baseVertex + index] = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint(this.Mesh.vertices[index]);
      num1 = Mathf.Min(vertices[baseVertex + index].y, num1);
      num2 = Mathf.Max(vertices[baseVertex + index].y, num2);
    }
    if (normals != null)
    {
      for (int index = 0; index < this.Mesh.vertexCount; ++index)
        normals[baseVertex + index] = ((Matrix4x4) ref localToWorldMatrix).MultiplyVector(this.Mesh.normals[index]);
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
    float num3 = num2 - num1;
    if (colors != null)
    {
      if (this.Mesh.colors32.Length == this.Mesh.vertexCount)
      {
        for (int index = 0; index < this.Mesh.vertexCount; ++index)
        {
          Color32 color32_1 = this.Mesh.colors32[index];
          Color32 color32_2 = Color32.Lerp(this.BottomColor, this.TopColor, (vertices[baseVertex + index].y - num1) / num3);
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
          colors[baseVertex + index] = Color32.Lerp(this.BottomColor, this.TopColor, (vertices[baseVertex + index].y - num1) / num3);
      }
    }
    for (int index = 0; index < this.Mesh.triangles.Length; ++index)
      indices[baseIndex + index] = this.Mesh.triangles[index] + baseVertex;
  }
}
