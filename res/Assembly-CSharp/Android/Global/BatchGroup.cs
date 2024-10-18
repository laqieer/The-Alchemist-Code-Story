// Decompiled with JetBrains decompiler
// Type: BatchGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Rendering/Batch Group")]
[ExecuteInEditMode]
public class BatchGroup : MonoBehaviour
{
  private List<BatchGroup.BatchInfo> mBatches = new List<BatchGroup.BatchInfo>(4);
  private Matrix4x4 mWorldToLocal;

  private void Start()
  {
    this.UpdateBatch();
  }

  private void OnDestroy()
  {
    this.ClearBatches();
  }

  private void ClearBatches()
  {
    for (int index = 0; index < this.mBatches.Count; ++index)
      Object.DestroyImmediate((Object) this.mBatches[index].Mesh);
    this.mBatches.Clear();
  }

  public void RequestUpdateBatch()
  {
  }

  public void UpdateBatch()
  {
    BatchChunk[] componentsInChildren = this.GetComponentsInChildren<BatchChunk>(true);
    List<Material> materialList = new List<Material>(4);
    this.mWorldToLocal = this.transform.worldToLocalMatrix;
    this.ClearBatches();
    foreach (BatchChunk batchChunk1 in componentsInChildren)
    {
      if (!((Object) batchChunk1.Material == (Object) null) && !((Object) batchChunk1.GetComponentInParent<BatchGroup>() != (Object) this) && !materialList.Contains(batchChunk1.Material))
      {
        materialList.Add(batchChunk1.Material);
        int length1 = 0;
        int length2 = 0;
        foreach (BatchChunk batchChunk2 in componentsInChildren)
        {
          if (!((Object) batchChunk2.Material != (Object) batchChunk1.Material) && !((Object) batchChunk2.GetComponentInParent<BatchGroup>() != (Object) this))
          {
            length1 += batchChunk2.VertexCount;
            length2 += batchChunk2.IndexCount;
          }
        }
        if (length1 > 0 && length2 > 0)
        {
          Vector3[] vertices = new Vector3[length1];
          Vector3[] normals = new Vector3[length1];
          Vector2[] uv = new Vector2[length1];
          Color32[] colors = new Color32[length1];
          int[] indices = new int[length2];
          Vector3[] centers = new Vector3[length1];
          int baseVertex = 0;
          int baseIndex = 0;
          foreach (BatchChunk batchChunk2 in componentsInChildren)
          {
            if (!((Object) batchChunk2.Material != (Object) batchChunk1.Material))
            {
              batchChunk2.FillTriangles(baseVertex, vertices, normals, uv, colors, centers, baseIndex, indices);
              baseVertex += batchChunk2.VertexCount;
              baseIndex += batchChunk2.IndexCount;
            }
          }
          Vector2[] vector2Array = new Vector2[length1];
          for (int index = 0; index < length1; ++index)
            vector2Array[index].y = centers[index].y;
          Mesh mesh = new Mesh();
          mesh.hideFlags = HideFlags.DontSave;
          mesh.vertices = vertices;
          mesh.colors32 = colors;
          mesh.normals = normals;
          mesh.uv = uv;
          mesh.uv2 = vector2Array;
          mesh.triangles = indices;
          this.mBatches.Add(new BatchGroup.BatchInfo()
          {
            Material = batchChunk1.Material,
            Mesh = mesh
          });
        }
      }
    }
  }

  private void Update()
  {
    for (int index = 0; index < this.mBatches.Count; ++index)
      Graphics.DrawMesh(this.mBatches[index].Mesh, this.transform.localToWorldMatrix * this.mWorldToLocal, this.mBatches[index].Material, this.gameObject.layer);
  }

  private void OnRenderObject()
  {
  }

  private struct BatchInfo
  {
    public Mesh Mesh;
    public Material Material;
  }
}
