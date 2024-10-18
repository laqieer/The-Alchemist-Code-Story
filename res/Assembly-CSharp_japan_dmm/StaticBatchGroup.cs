// Decompiled with JetBrains decompiler
// Type: StaticBatchGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
    this.mMeshRenderer = ((Component) this).GetComponent<MeshRenderer>();
    this.mMeshFilter = ((Component) this).GetComponent<MeshFilter>();
  }

  private void OnDestroy() => StaticBatchGroup.mInstances.Remove(this);

  private static float GetScaleSign(Transform tr)
  {
    Vector3 lossyScale = tr.lossyScale;
    return Mathf.Sign(lossyScale.x) * Mathf.Sign(lossyScale.y) * Mathf.Sign(lossyScale.z);
  }

  private static Mesh MergeMeshes(
    List<StaticBatchGroup.Section>[] groups,
    int numSubMeshes,
    List<Vector3> verts,
    List<Vector3> normals,
    List<Color32> colors,
    List<Vector2> uvs,
    List<Vector2> uvs1,
    List<Vector2> uvs2,
    List<int>[] newIndices,
    List<int> indexMap)
  {
    Mesh mesh = new Mesh();
    ((Object) mesh).hideFlags = (HideFlags) 52;
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
    Transform transform1 = ((Component) groups[0][0].MeshFilter).transform;
    Matrix4x4 worldToLocalMatrix = transform1.worldToLocalMatrix;
    float scaleSign1 = StaticBatchGroup.GetScaleSign(transform1);
    for (int index1 = 0; index1 < numSubMeshes; ++index1)
    {
      List<StaticBatchGroup.Section> group = groups[index1];
      newIndices[index1].Clear();
      for (int index2 = 0; index2 < group.Count; ++index2)
      {
        Transform transform2 = ((Component) group[index2].MeshFilter).transform;
        float scaleSign2 = StaticBatchGroup.GetScaleSign(transform2);
        Matrix4x4 matrix4x4 = Matrix4x4.op_Multiply(worldToLocalMatrix, transform2.localToWorldMatrix);
        MeshRenderer meshRenderer = group[index2].MeshRenderer;
        Mesh sharedMesh = group[index2].MeshFilter.sharedMesh;
        int[] triangles = sharedMesh.GetTriangles(group[index2].SubMesh);
        if (triangles.Length > 0)
        {
          indexMap.Clear();
          Vector3[] vertices = sharedMesh.vertices;
          Vector2[] uv = sharedMesh.uv;
          Vector2[] uv2 = sharedMesh.uv2;
          LightmapLayout component = ((Component) meshRenderer).GetComponent<LightmapLayout>();
          if (Object.op_Inequality((Object) component, (Object) null))
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
          for (int index4 = num2; index4 != num3; index4 += num4)
          {
            int index5 = triangles[index4];
            int num5 = indexMap.IndexOf(index5);
            if (num5 >= 0)
            {
              newIndices[index1].Add(num5 + num1);
            }
            else
            {
              verts.Add(((Matrix4x4) ref matrix4x4).MultiplyPoint(vertices[index5]));
              if (uv != null)
                uvs.Add(uv[index5]);
              else
                uvs.Add(Vector2.zero);
              if (uv2 != null)
                uvs2.Add(uv2[index5]);
              else
                uvs2.Add(Vector2.zero);
              newIndices[index1].Add(indexMap.Count + num1);
              indexMap.Add(index5);
            }
          }
          num1 += indexMap.Count;
        }
      }
    }
    mesh.vertices = verts.ToArray();
    mesh.uv = uvs.ToArray();
    mesh.uv2 = uvs2.ToArray();
    for (int index = 0; index < numSubMeshes; ++index)
      mesh.SetTriangles(newIndices[index].ToArray(), index);
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
      if (!Object.op_Equality((Object) mMeshRenderer, (Object) null) && ((Renderer) mMeshRenderer).sharedMaterials.Length > 0)
      {
        MeshFilter mMeshFilter = StaticBatchGroup.mInstances[index1].mMeshFilter;
        if (!Object.op_Equality((Object) mMeshFilter, (Object) null) && !Object.op_Equality((Object) mMeshFilter.sharedMesh, (Object) null))
        {
          for (int index2 = 0; index2 < ((Renderer) mMeshRenderer).sharedMaterials.Length; ++index2)
          {
            Material sharedMaterial = ((Renderer) mMeshRenderer).sharedMaterials[index2];
            if (!Object.op_Equality((Object) sharedMaterial, (Object) null))
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
    for (int index3 = 0; index3 < sectionList.Count; ++index3)
    {
      if (!sectionList[index3].Merged)
      {
        Material[] sharedMaterials = ((Renderer) sectionList[index3].MeshRenderer).sharedMaterials;
        if (array == null)
          array = new List<StaticBatchGroup.Section>[sharedMaterials.Length * 2];
        else if (array.Length < sharedMaterials.Length)
          Array.Resize<List<StaticBatchGroup.Section>>(ref array, sharedMaterials.Length + 2);
        for (int index4 = 0; index4 < sharedMaterials.Length; ++index4)
        {
          if (!Object.op_Equality((Object) sharedMaterials[index4], (Object) null))
          {
            if (array[index4] != null)
              array[index4].Clear();
            else
              array[index4] = new List<StaticBatchGroup.Section>(8);
            for (int index5 = index3; index5 < sectionList.Count; ++index5)
            {
              if (!Object.op_Inequality((Object) sharedMaterials[index4], (Object) sectionList[index5].Material) && !sectionList[index5].Merged)
              {
                sectionList[index5].Merged = true;
                ((Renderer) sectionList[index5].MeshRenderer).enabled = false;
                array[index4].Add(sectionList[index5]);
              }
            }
          }
        }
        int length = sharedMaterials.Length;
        for (int index6 = 0; index6 < length; ++index6)
        {
          if (array[index6].Count <= 0)
          {
            array[index6] = array[length - 1];
            --length;
            --index6;
          }
        }
        if (length <= 0)
        {
          ((Renderer) sectionList[index3].MeshRenderer).enabled = false;
        }
        else
        {
          Mesh mesh = StaticBatchGroup.MergeMeshes(array, length, verts, normals, colors, uvs, uvs1, uvs2, newIndices, indexMap);
          sectionList[index3].MeshFilter.sharedMesh = mesh;
          meshList.Add(mesh);
          Material[] materialArray = new Material[length];
          for (int index7 = 0; index7 < length; ++index7)
            materialArray[index7] = array[index7][0].Material;
          LightmapLayout component = ((Component) sectionList[index3].MeshRenderer).GetComponent<LightmapLayout>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.Position = new Vector4(1f, 1f, 0.0f, 0.0f);
            component.ApplyLayout();
          }
          else
            DebugUtility.LogError("NotFound LightmapLayout => GameObject Name is " + ((Object) ((Component) sectionList[index3].MeshRenderer).gameObject).name);
          ((Renderer) sectionList[index3].MeshRenderer).lightmapScaleOffset = new Vector4(1f, 1f, 0.0f, 0.0f);
          ((Renderer) sectionList[index3].MeshRenderer).sharedMaterials = materialArray;
          ((Renderer) sectionList[index3].MeshRenderer).enabled = true;
        }
      }
    }
    return meshList.ToArray();
  }

  private void Start()
  {
    if (Object.op_Inequality((Object) this, (Object) StaticBatchGroup.mInstances[StaticBatchGroup.mInstances.Count - 1]))
      return;
    Mesh[] meshes = StaticBatchGroup.GenerateBatch();
    if (meshes == null)
      return;
    ((Component) this).gameObject.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go =>
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
