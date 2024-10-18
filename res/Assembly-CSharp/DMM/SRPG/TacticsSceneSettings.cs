// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsSceneSettings
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
  [ExecuteInEditMode]
  public class TacticsSceneSettings : SceneRoot
  {
    public static bool AutoDeactivateScene;
    private static List<TacticsSceneSettings> mScenes = new List<TacticsSceneSettings>();
    [HideInInspector]
    public bool OverrideCameraSettings;
    [HideInInspector]
    public float CameraYawMin = 45f;
    [HideInInspector]
    public float CameraYawMax = 135f;
    public Texture2D ScreenFilter;
    public Texture2D BackgroundImage;
    private Mesh mGridMesh;
    private List<GridLayer> mGridLayers = new List<GridLayer>(6);

    public static TacticsSceneSettings[] Scenes => TacticsSceneSettings.mScenes.ToArray();

    public static int SceneCount => TacticsSceneSettings.mScenes.Count;

    public static void SetScenesActive(bool active)
    {
      for (int index = 0; index < TacticsSceneSettings.mScenes.Count; ++index)
        ((Component) TacticsSceneSettings.mScenes[index]).gameObject.SetActive(active);
    }

    public static TacticsSceneSettings LastScene
    {
      get
      {
        return TacticsSceneSettings.mScenes.Count > 0 ? TacticsSceneSettings.mScenes[TacticsSceneSettings.mScenes.Count - 1] : (TacticsSceneSettings) null;
      }
    }

    public static TacticsSceneSettings Instance
    {
      get
      {
        return TacticsSceneSettings.mScenes.Count > 0 ? TacticsSceneSettings.mScenes[0] : (TacticsSceneSettings) null;
      }
    }

    public static void PopFirstScene()
    {
      if (TacticsSceneSettings.mScenes.Count <= 0)
        return;
      Object.DestroyImmediate((Object) ((Component) TacticsSceneSettings.mScenes[0]).gameObject);
    }

    private void Start()
    {
      if (!Application.isPlaying)
        return;
      Light[] componentsInChildren = ((Component) this).GetComponentsInChildren<Light>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        componentsInChildren[index].shadows = (LightShadows) 0;
    }

    protected override void Awake()
    {
      base.Awake();
      TacticsSceneSettings.mScenes.Add(this);
      if (TacticsSceneSettings.AutoDeactivateScene)
      {
        ((Component) this).gameObject.SetActive(false);
        TacticsSceneSettings.AutoDeactivateScene = false;
      }
      TacticsSceneCamera.ClearInstance();
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mGridMesh, (Object) null))
      {
        Object.DestroyImmediate((Object) this.mGridMesh);
        this.mGridMesh = (Mesh) null;
      }
      TacticsSceneSettings.mScenes.Remove(this);
    }

    [DebuggerHidden]
    public IEnumerator GenerateGridMeshAsync(int x, int y)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsSceneSettings.\u003CGenerateGridMeshAsync\u003Ec__Iterator0()
      {
        x = x,
        y = y,
        \u0024this = this
      };
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
      ((Component) gridLayer).transform.SetParent(((Component) this).transform, false);
      ((Component) gridLayer).GetComponent<MeshFilter>().mesh = this.mGridMesh;
      gridLayer.LayerID = layerID;
      ((Component) gridLayer).gameObject.SetActive(false);
      Transform transform = ((Component) gridLayer).transform;
      transform.position = Vector3.op_Addition(transform.position, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.up, (float) layerID), 0.01f));
      if (!string.IsNullOrEmpty(mat_path))
        gridLayer.ChangeMaterial(mat_path);
      this.mGridLayers.Add(gridLayer);
      return gridLayer;
    }

    public void HideGridLayer(int layerID)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, false);
      if (!Object.op_Inequality((Object) gridLayer, (Object) null))
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
      GridLayer gridLayer = this.FindGridLayer(layerID, true);
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

    private bool RaycastDeformedMesh(
      Mesh mesh,
      Vector3 start,
      Vector3 direction,
      Matrix4x4 world,
      float cameraZ,
      out float hitDistance,
      out Vector3 hitPos,
      out Vector3 hitPosReal)
    {
      hitDistance = float.MaxValue;
      hitPos = Vector3.zero;
      hitPosReal = Vector3.zero;
      Vector3[] vertices = mesh.vertices;
      int[] triangles = mesh.triangles;
      for (int index = 0; index < triangles.Length; index += 3)
      {
        Vector3 vector3_1 = vertices[triangles[index]];
        Vector3 vector3_2 = vertices[triangles[index + 1]];
        Vector3 vector3_3 = vertices[triangles[index + 2]];
        Vector3 pos1 = ((Matrix4x4) ref world).MultiplyPoint(vector3_1);
        Vector3 pos2 = ((Matrix4x4) ref world).MultiplyPoint(vector3_2);
        Vector3 pos3 = ((Matrix4x4) ref world).MultiplyPoint(vector3_3);
        Vector3 vector3_4 = GameUtility.DeformPosition(pos1, cameraZ);
        Vector3 vector3_5 = GameUtility.DeformPosition(pos2, cameraZ);
        Vector3 vector3_6 = GameUtility.DeformPosition(pos3, cameraZ);
        Vector3 vector3_7 = Vector3.op_Subtraction(vector3_5, vector3_4);
        Vector3 normalized1 = ((Vector3) ref vector3_7).normalized;
        Vector3 vector3_8 = Vector3.op_Subtraction(vector3_6, vector3_4);
        Vector3 vector3_9 = Vector3.Cross(((Vector3) ref vector3_8).normalized, normalized1);
        Vector3 normalized2 = ((Vector3) ref vector3_9).normalized;
        float num1 = Vector3.Dot(normalized2, direction);
        if ((double) num1 > 0.0)
        {
          float num2 = Vector3.Dot(Vector3.op_Subtraction(vector3_4, start), normalized2) / num1;
          if ((double) num2 < (double) hitDistance)
          {
            Vector3 vector3_10 = Vector3.op_Addition(start, Vector3.op_Multiply(direction, num2));
            Vector3 vector3_11 = Vector3.op_Subtraction(vector3_4, vector3_10);
            Vector3 normalized3 = ((Vector3) ref vector3_11).normalized;
            Vector3 vector3_12 = Vector3.op_Subtraction(vector3_5, vector3_10);
            Vector3 normalized4 = ((Vector3) ref vector3_12).normalized;
            Vector3 vector3_13 = Vector3.op_Subtraction(vector3_6, vector3_10);
            Vector3 normalized5 = ((Vector3) ref vector3_13).normalized;
            if ((double) (Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized3, normalized4), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized4, normalized5), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized5, normalized3), -1f, 1f))) > 6.2731852531433105)
            {
              hitDistance = num2;
              hitPos = vector3_10;
              Vector3 vector3_14 = Vector3.op_Subtraction(vector3_10, vector3_4);
              Vector3 vector3_15 = Vector3.op_Subtraction(vector3_6, vector3_5);
              Vector3 vector3_16 = Vector3.op_Subtraction(vector3_4, vector3_5);
              float num3 = Vector3.Dot(vector3_14, vector3_14);
              float num4 = Vector3.Dot(vector3_14, vector3_15);
              float num5 = Vector3.Dot(vector3_15, vector3_15);
              float num6 = (float) ((double) num3 * (double) num5 - (double) num4 * (double) num4);
              float num7 = Vector3.Dot(vector3_14, vector3_16);
              float num8 = Vector3.Dot(vector3_15, vector3_16);
              float num9 = (float) ((double) num4 * (double) num8 - (double) num7 * (double) num5) / num6;
              Vector3 vector3_17 = Vector3.op_Addition(vector3_4, Vector3.op_Multiply(vector3_14, num9));
              Vector3 vector3_18 = Vector3.op_Subtraction(vector3_17, vector3_4);
              float num10 = Vector3.Dot(Vector3.op_Subtraction(vector3_17, vector3_5), vector3_15) / Vector3.Dot(vector3_15, vector3_15);
              float num11 = Vector3.Dot(Vector3.op_Subtraction(vector3_10, vector3_4), vector3_18) / Vector3.Dot(vector3_18, vector3_18);
              hitPosReal = Vector3.Lerp(pos1, Vector3.Lerp(pos2, pos3, num10), num11);
            }
          }
        }
      }
      return (double) hitDistance < 3.4028234663852886E+38;
    }

    public bool RaycastDeformed(Ray ray, out RaycastHit result)
    {
      MeshFilter[] componentsInChildren = ((Component) this).GetComponentsInChildren<MeshFilter>(false);
      result = new RaycastHit();
      ((RaycastHit) ref result).distance = float.MaxValue;
      string[] strArray = new string[1]{ "BG" };
      Vector3 position = ((Component) Camera.main).transform.position;
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Mesh sharedMesh = componentsInChildren[index].sharedMesh;
        if (!Object.op_Equality((Object) sharedMesh, (Object) null))
        {
          GameObject gameObject = ((Component) componentsInChildren[index]).gameObject;
          if ((1 << gameObject.layer & LayerMask.GetMask(strArray)) != 0)
          {
            Transform transform = gameObject.transform;
            float hitDistance;
            Vector3 hitPosReal;
            if (this.RaycastDeformedMesh(sharedMesh, ((Ray) ref ray).origin, ((Ray) ref ray).direction, transform.localToWorldMatrix, position.z, out hitDistance, out Vector3 _, out hitPosReal) && (double) hitDistance < (double) ((RaycastHit) ref result).distance)
            {
              ((RaycastHit) ref result).point = hitPosReal;
              ((RaycastHit) ref result).distance = hitDistance;
            }
          }
        }
      }
      return (double) ((RaycastHit) ref result).distance < 3.4028234663852886E+38;
    }
  }
}
