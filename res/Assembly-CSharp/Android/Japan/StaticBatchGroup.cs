// Decompiled with JetBrains decompiler
// Type: StaticBatchGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StaticBatchGroup : MonoBehaviour
{
  private static List<StaticBatchGroup> mInstances = new List<StaticBatchGroup>();
  private const int DEFAULT_INDEX_BUFFER_SIZE = 512;
  private MeshRenderer mMeshRenderer;
  private MeshFilter mMeshFilter;

  private void Awake()
  {
    StaticBatchGroup.mInstances.Add(this);
    this.mMeshRenderer = this.GetComponent<MeshRenderer>();
    this.mMeshFilter = this.GetComponent<MeshFilter>();
  }

  private void OnDestroy()
  {
    StaticBatchGroup.mInstances.Remove(this);
  }

  private static float GetScaleSign(Transform tr)
  {
    Vector3 lossyScale = tr.lossyScale;
    return Mathf.Sign(lossyScale.x) * Mathf.Sign(lossyScale.y) * Mathf.Sign(lossyScale.z);
  }

  private static Mesh MergeMeshes(List<StaticBatchGroup.Section>[] groups, int numSubMeshes, List<Vector3> verts, List<Vector3> normals, List<Color32> colors, List<Vector2> uvs, List<Vector2> uvs1, List<Vector2> uvs2, List<int>[] newIndices, List<int> indexMap)
  {
    Mesh mesh = new Mesh();
    mesh.hideFlags = HideFlags.DontSave;
    mesh.subMeshCount = numSubMeshes;
    if (newIndices.Length < numSubMeshes)
    {
      int length = newIndices.Length;
      Array.Resize<List<int>>(ref newIndices, numSubMeshes + 2);
      for (int index = length - 1; index < newIndices.Length; ++index)
        newIndices[index] = new List<int>(512);
    }
    verts.Clear();
    uvs.Clear();
    uvs2.Clear();
    int num1 = 0;
    Transform transform1 = groups[0][0].MeshFilter.transform;
    Matrix4x4 worldToLocalMatrix = transform1.worldToLocalMatrix;
    float scaleSign1 = StaticBatchGroup.GetScaleSign(transform1);
    for (int index1 = 0; index1 < numSubMeshes; ++index1)
    {
      List<StaticBatchGroup.Section> group = groups[index1];
      newIndices[index1].Clear();
      for (int index2 = 0; index2 < group.Count; ++index2)
      {
        Transform transform2 = group[index2].MeshFilter.transform;
        float scaleSign2 = StaticBatchGroup.GetScaleSign(transform2);
        Matrix4x4 matrix4x4 = worldToLocalMatrix * transform2.localToWorldMatrix;
        MeshRenderer meshRenderer = group[index2].MeshRenderer;
        Mesh sharedMesh = group[index2].MeshFilter.sharedMesh;
        int[] triangles = sharedMesh.GetTriangles(group[index2].SubMesh);
        if (triangles.Length > 0)
        {
          indexMap.Clear();
          Vector3[] vertices = sharedMesh.vertices;
          Vector2[] uv = sharedMesh.uv;
          Vector2[] uv2 = sharedMesh.uv2;
          LightmapLayout component = meshRenderer.GetComponent<LightmapLayout>();
          if ((Object) component != (Object) null)
          {
            for (int index3 = uv.Length - 1; index3 >= 0; --index3)
            {
              uv2[index3].x = uv2[index3].x * component.Position.x + component.Position.z;
              uv2[index3].y = uv2[index3].y * component.Position.y + component.Position.w;
            }
          }
          int num2;
          int num3;
          int num4;
          if ((double) scaleSign1 * (double) scaleSign2 < 0.0)
          {
            num2 = triangles.Length - 1;
            num3 = -1;
            num4 = -1;
          }
          else
          {
            num2 = 0;
            num3 = triangles.Length;
            num4 = 1;
          }
          int index4 = num2;
          while (index4 != num3)
          {
            int index3 = triangles[index4];
            int num5 = indexMap.IndexOf(index3);
            if (num5 >= 0)
            {
              newIndices[index1].Add(num5 + num1);
            }
            else
            {
              verts.Add(matrix4x4.MultiplyPoint(vertices[index3]));
              if (uv != null)
                uvs.Add(uv[index3]);
              else
                uvs.Add(Vector2.zero);
              if (uv2 != null)
                uvs2.Add(uv2[index3]);
              else
                uvs2.Add(Vector2.zero);
              newIndices[index1].Add(indexMap.Count + num1);
              indexMap.Add(index3);
            }
            index4 += num4;
          }
          num1 += indexMap.Count;
        }
      }
    }
    mesh.vertices = verts.ToArray();
    mesh.uv = uvs.ToArray();
    mesh.uv2 = uvs2.ToArray();
    for (int submesh = 0; submesh < numSubMeshes; ++submesh)
      mesh.SetTriangles(newIndices[submesh].ToArray(), submesh);
    mesh.UploadMeshData(true);
    return mesh;
  }

  private static Mesh[] GenerateBatch()
  {
    List<StaticBatchGroup.Section> sectionList = new List<StaticBatchGroup.Section>(64);
    List<Mesh> meshList = new List<Mesh>();
    for (int index1 = 0; index1 < StaticBatchGroup.mInstances.Count; ++index1)
    {
      MeshRenderer mMeshRenderer = StaticBatchGroup.mInstances[index1].mMeshRenderer;
      if (!((Object) mMeshRenderer == (Object) null) && mMeshRenderer.sharedMaterials.Length > 0)
      {
        MeshFilter mMeshFilter = StaticBatchGroup.mInstances[index1].mMeshFilter;
        if (!((Object) mMeshFilter == (Object) null) && !((Object) mMeshFilter.sharedMesh == (Object) null))
        {
          for (int index2 = 0; index2 < mMeshRenderer.sharedMaterials.Length; ++index2)
          {
            Material sharedMaterial = mMeshRenderer.sharedMaterials[index2];
            if (!((Object) sharedMaterial == (Object) null))
              sectionList.Add(new StaticBatchGroup.Section()
              {
                Material = sharedMaterial,
                SubMesh = index2,
                MeshFilter = mMeshFilter,
                MeshRenderer = mMeshRenderer
              });
          }
        }
      }
    }
    for (int index = 0; index < StaticBatchGroup.mInstances.Count; ++index)
      Object.Destroy((Object) StaticBatchGroup.mInstances[index]);
    List<StaticBatchGroup.Section>[] array = (List<StaticBatchGroup.Section>[]) null;
    List<Vector3> verts = new List<Vector3>();
    List<Vector3> normals = (List<Vector3>) null;
    List<Color32> colors = (List<Color32>) null;
    List<Vector2> uvs1 = (List<Vector2>) null;
    List<int> indexMap = new List<int>(1024);
    List<int>[] newIndices = new List<int>[8];
    for (int index = 0; index < newIndices.Length; ++index)
      newIndices[index] = new List<int>(512);
    List<Vector2> uvs = new List<Vector2>();
    List<Vector2> uvs2 = new List<Vector2>();
    for (int index1 = 0; index1 < sectionList.Count; ++index1)
    {
      if (!sectionList[index1].Merged)
      {
        Material[] sharedMaterials = sectionList[index1].MeshRenderer.sharedMaterials;
        if (array == null)
          array = new List<StaticBatchGroup.Section>[sharedMaterials.Length * 2];
        else if (array.Length < sharedMaterials.Length)
          Array.Resize<List<StaticBatchGroup.Section>>(ref array, sharedMaterials.Length + 2);
        for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
        {
          if (!((Object) sharedMaterials[index2] == (Object) null))
          {
            if (array[index2] != null)
              array[index2].Clear();
            else
              array[index2] = new List<StaticBatchGroup.Section>(8);
            for (int index3 = index1; index3 < sectionList.Count; ++index3)
            {
              if (!((Object) sharedMaterials[index2] != (Object) sectionList[index3].Material) && !sectionList[index3].Merged)
              {
                sectionList[index3].Merged = true;
                sectionList[index3].MeshRenderer.enabled = false;
                array[index2].Add(sectionList[index3]);
              }
            }
          }
        }
        int length = sharedMaterials.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (array[index2].Count <= 0)
          {
            array[index2] = array[length - 1];
            --length;
            --index2;
          }
        }
        if (length <= 0)
        {
          sectionList[index1].MeshRenderer.enabled = false;
        }
        else
        {
          Mesh mesh = StaticBatchGroup.MergeMeshes(array, length, verts, normals, colors, uvs, uvs1, uvs2, newIndices, indexMap);
          sectionList[index1].MeshFilter.sharedMesh = mesh;
          meshList.Add(mesh);
          Material[] materialArray = new Material[length];
          for (int index2 = 0; index2 < length; ++index2)
            materialArray[index2] = array[index2][0].Material;
          LightmapLayout component = sectionList[index1].MeshRenderer.GetComponent<LightmapLayout>();
          component.Position = new Vector4(1f, 1f, 0.0f, 0.0f);
          component.ApplyLayout();
          sectionList[index1].MeshRenderer.lightmapScaleOffset = new Vector4(1f, 1f, 0.0f, 0.0f);
          sectionList[index1].MeshRenderer.sharedMaterials = materialArray;
          sectionList[index1].MeshRenderer.enabled = true;
        }
      }
    }
    return meshList.ToArray();
  }

  private void Start()
  {
    if ((Object) this != (Object) StaticBatchGroup.mInstances[StaticBatchGroup.mInstances.Count - 1])
      return;
    Mesh[] meshes = StaticBatchGroup.GenerateBatch();
    if (meshes == null)
      return;
    this.gameObject.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go =>
    {
      for (int index = 0; index < meshes.Length; ++index)
        Object.DestroyImmediate((Object) meshes[index]);
    });
  }

  private class Section
  {
    public Material Material;
    public MeshRenderer MeshRenderer;
    public MeshFilter MeshFilter;
    public int SubMesh;
    public bool Merged;
  }
}
