// Decompiled with JetBrains decompiler
// Type: SRPG.GridMeshGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  internal class GridMeshGenerator
  {
    private const float MinNormalThreshold = 0.5f;
    private List<Vector3> mVerts = new List<Vector3>(256);
    private List<int> mIndices = new List<int>(256);

    public void Clear()
    {
      this.mVerts.Clear();
      this.mIndices.Clear();
    }

    [DebuggerHidden]
    public IEnumerator AddMeshAsync(Mesh mesh, Matrix4x4 matrix, Rect clipRect, bool mirror)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GridMeshGenerator.\u003CAddMeshAsync\u003Ec__Iterator0()
      {
        mesh = mesh,
        matrix = matrix,
        mirror = mirror,
        clipRect = clipRect,
        \u0024this = this
      };
    }

    public void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Rect rect)
    {
      Vector3 vector3_1 = Vector3.Min(Vector3.Min(v0, v1), v2);
      Vector3 vector3_2 = Vector3.Max(Vector3.Max(v0, v1), v2);
      if ((double) vector3_2.x < (double) ((Rect) ref rect).xMin || (double) ((Rect) ref rect).xMax < (double) vector3_1.x || (double) vector3_2.z < (double) ((Rect) ref rect).yMin || (double) ((Rect) ref rect).yMax < (double) vector3_1.z)
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
        if (Vector3.op_Equality(this.mVerts[index], v))
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
