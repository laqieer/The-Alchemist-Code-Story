// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsSceneSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  public class TacticsSceneSettings : SceneRoot
  {
    private static List<TacticsSceneSettings> mScenes = new List<TacticsSceneSettings>();
    [HideInInspector]
    public float CameraYawMin = 45f;
    [HideInInspector]
    public float CameraYawMax = 135f;
    private List<GridLayer> mGridLayers = new List<GridLayer>(6);
    public static bool AutoDeactivateScene;
    [HideInInspector]
    public bool OverrideCameraSettings;
    public Texture2D ScreenFilter;
    public Texture2D BackgroundImage;
    private Mesh mGridMesh;

    public static TacticsSceneSettings[] Scenes
    {
      get
      {
        return TacticsSceneSettings.mScenes.ToArray();
      }
    }

    public static int SceneCount
    {
      get
      {
        return TacticsSceneSettings.mScenes.Count;
      }
    }

    public static void SetScenesActive(bool active)
    {
      for (int index = 0; index < TacticsSceneSettings.mScenes.Count; ++index)
        TacticsSceneSettings.mScenes[index].gameObject.SetActive(active);
    }

    public static TacticsSceneSettings LastScene
    {
      get
      {
        if (TacticsSceneSettings.mScenes.Count > 0)
          return TacticsSceneSettings.mScenes[TacticsSceneSettings.mScenes.Count - 1];
        return (TacticsSceneSettings) null;
      }
    }

    public static TacticsSceneSettings Instance
    {
      get
      {
        if (TacticsSceneSettings.mScenes.Count > 0)
          return TacticsSceneSettings.mScenes[0];
        return (TacticsSceneSettings) null;
      }
    }

    public static void PopFirstScene()
    {
      if (TacticsSceneSettings.mScenes.Count <= 0)
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) TacticsSceneSettings.mScenes[0].gameObject);
    }

    private void Start()
    {
      if (!Application.isPlaying)
        return;
      Light[] componentsInChildren = this.GetComponentsInChildren<Light>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        componentsInChildren[index].shadows = LightShadows.None;
    }

    protected override void Awake()
    {
      base.Awake();
      TacticsSceneSettings.mScenes.Add(this);
      if (TacticsSceneSettings.AutoDeactivateScene)
      {
        this.gameObject.SetActive(false);
        TacticsSceneSettings.AutoDeactivateScene = false;
      }
      TacticsSceneCamera.ClearInstance();
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this.mGridMesh != (UnityEngine.Object) null)
      {
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mGridMesh);
        this.mGridMesh = (Mesh) null;
      }
      TacticsSceneSettings.mScenes.Remove(this);
    }

    public void GenerateGridMesh(int x, int y)
    {
      GridMeshGenerator gridMeshGenerator = new GridMeshGenerator();
      Rect clipRect = new Rect(0.0f, 0.0f, (float) x, (float) y);
      Matrix4x4 worldToLocalMatrix = this.transform.worldToLocalMatrix;
      MeshFilter[] componentsInChildren = this.GetComponentsInChildren<MeshFilter>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (componentsInChildren[index].gameObject.activeSelf && componentsInChildren[index].gameObject.layer == GameUtility.LayerBG && !((UnityEngine.Object) componentsInChildren[index].sharedMesh == (UnityEngine.Object) null))
        {
          Transform transform = componentsInChildren[index].transform;
          Vector3 lossyScale = transform.lossyScale;
          bool mirror = (double) Mathf.Sign(lossyScale.x) * (double) Mathf.Sign(lossyScale.y) * (double) Mathf.Sign(lossyScale.z) < 0.0;
          gridMeshGenerator.AddMesh(componentsInChildren[index].sharedMesh, worldToLocalMatrix * transform.localToWorldMatrix, clipRect, mirror);
        }
      }
      this.mGridMesh = gridMeshGenerator.CreateMesh();
    }

    private GridLayer FindGridLayer(int layerID, bool autoCreate, string mat_path = null)
    {
      for (int index = this.mGridLayers.Count - 1; index >= 0; --index)
      {
        if (this.mGridLayers[index].LayerID == layerID)
          return this.mGridLayers[index];
      }
      if (!autoCreate)
        return (GridLayer) null;
      GridLayer gridLayer = new GameObject("Grid").AddComponent<GridLayer>();
      gridLayer.transform.SetParent(this.transform, false);
      gridLayer.GetComponent<MeshFilter>().mesh = this.mGridMesh;
      gridLayer.LayerID = layerID;
      gridLayer.gameObject.SetActive(false);
      gridLayer.transform.position += Vector3.up * (float) layerID * 0.01f;
      if (!string.IsNullOrEmpty(mat_path))
        gridLayer.ChangeMaterial(mat_path);
      this.mGridLayers.Add(gridLayer);
      return gridLayer;
    }

    public void HideGridLayer(int layerID)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, false, (string) null);
      if (!((UnityEngine.Object) gridLayer != (UnityEngine.Object) null))
        return;
      gridLayer.Hide();
    }

    public void HideGridLayers()
    {
      for (int index = this.mGridLayers.Count - 1; index >= 0; --index)
        this.mGridLayers[index].Hide();
    }

    public void ShowGridLayer(int layerID, GridMap<Color32> grid, bool mask)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, true, (string) null);
      gridLayer.UpdateGrid(grid);
      gridLayer.SetMask(mask);
      gridLayer.Show();
    }

    public void ShowGridLayer(int layerID, GridMap<bool> grid, Color32 color, bool mask = false)
    {
      if (grid == null)
        return;
      GridMap<Color32> grid1 = new GridMap<Color32>(grid.w, grid.h);
      for (int x = 0; x < grid.w; ++x)
      {
        for (int y = 0; y < grid.h; ++y)
        {
          if (grid.get(x, y))
            grid1.set(x, y, color);
        }
      }
      this.ShowGridLayer(layerID, grid1, mask);
    }

    public void ShowGridLayer(int layerID, GridMap<Color32> grid, string mat_path)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, true, mat_path);
      gridLayer.UpdateGrid(grid);
      gridLayer.SetMask(true);
      gridLayer.Show();
    }

    private bool RaycastDeformedMesh(Mesh mesh, Vector3 start, Vector3 direction, Matrix4x4 world, float cameraZ, out float hitDistance, out Vector3 hitPos, out Vector3 hitPosReal)
    {
      hitDistance = float.MaxValue;
      hitPos = Vector3.zero;
      hitPosReal = Vector3.zero;
      Vector3[] vertices = mesh.vertices;
      int[] triangles = mesh.triangles;
      int index = 0;
      while (index < triangles.Length)
      {
        Vector3 v1 = vertices[triangles[index]];
        Vector3 v2 = vertices[triangles[index + 1]];
        Vector3 v3 = vertices[triangles[index + 2]];
        Vector3 vector3_1 = world.MultiplyPoint(v1);
        Vector3 vector3_2 = world.MultiplyPoint(v2);
        Vector3 vector3_3 = world.MultiplyPoint(v3);
        Vector3 vector3_4 = GameUtility.DeformPosition(vector3_1, cameraZ);
        Vector3 vector3_5 = GameUtility.DeformPosition(vector3_2, cameraZ);
        Vector3 vector3_6 = GameUtility.DeformPosition(vector3_3, cameraZ);
        Vector3 normalized1 = (vector3_5 - vector3_4).normalized;
        Vector3 normalized2 = Vector3.Cross((vector3_6 - vector3_4).normalized, normalized1).normalized;
        float num1 = Vector3.Dot(normalized2, direction);
        if ((double) num1 > 0.0)
        {
          float num2 = Vector3.Dot(vector3_4 - start, normalized2) / num1;
          if ((double) num2 < (double) hitDistance)
          {
            Vector3 vector3_7 = start + direction * num2;
            Vector3 normalized3 = (vector3_4 - vector3_7).normalized;
            Vector3 normalized4 = (vector3_5 - vector3_7).normalized;
            Vector3 normalized5 = (vector3_6 - vector3_7).normalized;
            if ((double) (Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized3, normalized4), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized4, normalized5), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized5, normalized3), -1f, 1f))) > 6.27318525314331)
            {
              hitDistance = num2;
              hitPos = vector3_7;
              Vector3 vector3_8 = vector3_7 - vector3_4;
              Vector3 vector3_9 = vector3_6 - vector3_5;
              Vector3 rhs = vector3_4 - vector3_5;
              float num3 = Vector3.Dot(vector3_8, vector3_8);
              float num4 = Vector3.Dot(vector3_8, vector3_9);
              float num5 = Vector3.Dot(vector3_9, vector3_9);
              float num6 = (float) ((double) num3 * (double) num5 - (double) num4 * (double) num4);
              float num7 = Vector3.Dot(vector3_8, rhs);
              float num8 = Vector3.Dot(vector3_9, rhs);
              float num9 = (float) ((double) num4 * (double) num8 - (double) num7 * (double) num5) / num6;
              Vector3 vector3_10 = vector3_4 + vector3_8 * num9;
              Vector3 vector3_11 = vector3_10 - vector3_4;
              float t1 = Vector3.Dot(vector3_10 - vector3_5, vector3_9) / Vector3.Dot(vector3_9, vector3_9);
              float t2 = Vector3.Dot(vector3_7 - vector3_4, vector3_11) / Vector3.Dot(vector3_11, vector3_11);
              hitPosReal = Vector3.Lerp(vector3_1, Vector3.Lerp(vector3_2, vector3_3, t1), t2);
            }
          }
        }
        index += 3;
      }
      return (double) hitDistance < 3.40282346638529E+38;
    }

    public bool RaycastDeformed(Ray ray, out RaycastHit result)
    {
      MeshFilter[] componentsInChildren = this.GetComponentsInChildren<MeshFilter>(false);
      result = new RaycastHit();
      result.distance = float.MaxValue;
      string[] strArray = new string[1]{ "BG" };
      Vector3 position = UnityEngine.Camera.main.transform.position;
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Mesh sharedMesh = componentsInChildren[index].sharedMesh;
        if (!((UnityEngine.Object) sharedMesh == (UnityEngine.Object) null))
        {
          GameObject gameObject = componentsInChildren[index].gameObject;
          if ((1 << gameObject.layer & LayerMask.GetMask(strArray)) != 0)
          {
            Transform transform = gameObject.transform;
            float hitDistance;
            Vector3 hitPos;
            Vector3 hitPosReal;
            if (this.RaycastDeformedMesh(sharedMesh, ray.origin, ray.direction, transform.localToWorldMatrix, position.z, out hitDistance, out hitPos, out hitPosReal) && (double) hitDistance < (double) result.distance)
            {
              result.point = hitPosReal;
              result.distance = hitDistance;
            }
          }
        }
      }
      return (double) result.distance < 3.40282346638529E+38;
    }
  }
}
