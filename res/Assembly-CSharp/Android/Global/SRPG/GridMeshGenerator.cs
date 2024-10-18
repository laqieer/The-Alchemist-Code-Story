﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GridMeshGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  internal class GridMeshGenerator
  {
    private List<Vector3> mVerts = new List<Vector3>(256);
    private List<int> mIndices = new List<int>(256);
    private const float MinNormalThreshold = 0.5f;

    public void Clear()
    {
      this.mVerts.Clear();
      this.mIndices.Clear();
    }

    public void AddMesh(Mesh mesh, Matrix4x4 matrix, Rect clipRect, bool mirror)
    {
      Vector3[] vertices = mesh.vertices;
      for (int index = vertices.Length - 1; index >= 0; --index)
        vertices[index] = matrix.MultiplyPoint(vertices[index]);
      if (mirror)
      {
        int index = mesh.triangles.Length - 3;
        while (index >= 0)
        {
          if ((double) Vector3.Dot(Vector3.Cross(vertices[mesh.triangles[index + 2]] - vertices[mesh.triangles[index]], vertices[mesh.triangles[index + 1]] - vertices[mesh.triangles[index]]).normalized, Vector3.up) > 0.5)
            this.AddTriangle(vertices[mesh.triangles[index + 2]], vertices[mesh.triangles[index + 1]], vertices[mesh.triangles[index]], clipRect);
          index -= 3;
        }
      }
      else
      {
        int index = mesh.triangles.Length - 3;
        while (index >= 0)
        {
          if ((double) Vector3.Dot(Vector3.Cross(vertices[mesh.triangles[index + 1]] - vertices[mesh.triangles[index]], vertices[mesh.triangles[index + 2]] - vertices[mesh.triangles[index]]).normalized, Vector3.up) > 0.5)
            this.AddTriangle(vertices[mesh.triangles[index]], vertices[mesh.triangles[index + 1]], vertices[mesh.triangles[index + 2]], clipRect);
          index -= 3;
        }
      }
    }

    public void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Rect rect)
    {
      Vector3 vector3_1 = Vector3.Min(Vector3.Min(v0, v1), v2);
      Vector3 vector3_2 = Vector3.Max(Vector3.Max(v0, v1), v2);
      if ((double) vector3_2.x < (double) rect.xMin || (double) rect.xMax < (double) vector3_1.x || ((double) vector3_2.z < (double) rect.yMin || (double) rect.yMax < (double) vector3_1.z))
        return;
      int num1 = this.AddVertex(v0);
      int num2 = this.AddVertex(v1);
      int num3 = this.AddVertex(v2);
      this.mIndices.Add(num1);
      this.mIndices.Add(num2);
      this.mIndices.Add(num3);
    }

    private int AddVertex(Vector3 v)
    {
      for (int index = this.mVerts.Count - 1; index >= 0; --index)
      {
        if (this.mVerts[index] == v)
          return index;
      }
      this.mVerts.Add(v);
      return this.mVerts.Count - 1;
    }

    public Mesh CreateMesh()
    {
      Vector2[] vector2Array = new Vector2[this.mVerts.Count];
      for (int index = this.mVerts.Count - 1; index >= 0; --index)
      {
        vector2Array[index].x = this.mVerts[index].x;
        vector2Array[index].y = this.mVerts[index].z;
      }
      Mesh mesh = new Mesh();
      mesh.vertices = this.mVerts.ToArray();
      mesh.triangles = this.mIndices.ToArray();
      mesh.uv = vector2Array;
      mesh.UploadMeshData(true);
      return mesh;
    }
  }
}
