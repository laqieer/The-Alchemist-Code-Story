﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsSceneCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TacticsSceneCamera : MonoBehaviour
  {
    public TacticsSceneCamera.MoveRange m_MoveRange;
    public TacticsSceneCamera.AllRange m_AllRange;
    private TacticsSceneCamera.AllRangeObj m_AllRangeObj;
    private static TacticsSceneCamera m_Instance;

    public TacticsSceneCamera.MoveRange moveRange => this.m_MoveRange;

    public TacticsSceneCamera.AllRange allRange => this.m_AllRange;

    public bool isAllRange
    {
      set => this.m_AllRange.enable = value;
      get => this.m_AllRange != null && this.m_AllRange.enable;
    }

    public static TacticsSceneCamera instance => TacticsSceneCamera.m_Instance;

    public static void ClearInstance() => TacticsSceneCamera.m_Instance = (TacticsSceneCamera) null;

    private void Start()
    {
      if (!this.isAllRange || this.m_AllRange.groups == null)
        return;
      this.m_AllRangeObj = new TacticsSceneCamera.AllRangeObj();
      this.m_AllRangeObj.Create(this.m_AllRange);
      this.UpdateAllRangeCamera();
    }

    private void Update()
    {
      if (this.m_AllRangeObj == null)
        return;
      this.CalcAllRangeCamera();
      this.UpdateAllRangeCamera();
    }

    private void UpdateAllRangeCamera()
    {
      for (int index1 = 0; index1 < this.m_AllRangeObj.groups.Length; ++index1)
      {
        TacticsSceneCamera.AllRangeObj.GroupObj group = this.m_AllRangeObj.groups[index1];
        for (int index2 = 0; index2 < group.renders.Count; ++index2)
        {
          TacticsSceneCamera.RenderSet render = group.renders[index2];
          bool flag = (double) group.alpha > 0.0;
          for (int index3 = 0; index3 < render.meshRenderers.Count; ++index3)
            ((Renderer) render.meshRenderers[index3]).enabled = flag;
          for (int index4 = 0; index4 < render.skinRenderers.Count; ++index4)
            ((Renderer) render.meshRenderers[index4]).enabled = flag;
        }
      }
    }

    private void CalcAllRangeCamera()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      float num1 = 360f - SceneBattle.Instance.GetCameraAngle();
      if ((double) num1 < 0.0)
        num1 += 360f;
      else if ((double) num1 >= 360.0)
        num1 -= 360f;
      for (int index = 0; index < this.m_AllRangeObj.groups.Length; ++index)
      {
        TacticsSceneCamera.AllRangeObj.GroupObj group = this.m_AllRangeObj.groups[index];
        float min = group.data.min;
        float max = group.data.max;
        float num2 = group.data.prange;
        bool flag1 = false;
        if ((double) num2 == 0.0)
          num2 = 1f;
        bool flag2 = (double) min < (double) num1 && (double) min + (double) num2 > (double) num1 || (double) max > (double) num1 && (double) max - (double) num2 < (double) num1;
        if (group.state == 0)
        {
          if ((double) num1 > (double) min && (double) num1 < (double) max)
          {
            if (flag1)
              group.alpha -= group.alpha * 0.2f;
            else
              group.alpha = 0.0f;
            if ((double) group.alpha < 0.0099999997764825821)
            {
              group.alpha = 0.0f;
              if (!flag2)
                group.state = 1;
            }
          }
          else
          {
            if (flag1)
              group.alpha += (float) ((1.0 - (double) group.alpha) * 0.20000000298023224);
            else
              group.alpha = 1f;
            if ((double) group.alpha > 0.99000000953674316)
              group.alpha = 1f;
          }
        }
        else if (group.state == 1)
        {
          float num3 = min + num2;
          float num4 = max - num2;
          if ((double) num1 >= (double) num3 && (double) num1 <= (double) num4)
          {
            if (flag1)
              group.alpha -= group.alpha * 0.2f;
            else
              group.alpha = 0.0f;
            if ((double) group.alpha < 0.0099999997764825821)
              group.alpha = 0.0f;
          }
          else
          {
            if (flag1)
              group.alpha += (float) ((1.0 - (double) group.alpha) * 0.20000000298023224);
            else
              group.alpha = 1f;
            if ((double) group.alpha > 0.99000000953674316)
            {
              group.alpha = 1f;
              if (!flag2)
                group.state = 0;
            }
          }
        }
      }
    }

    public void CreateMoveRange(TacticsSceneSettings setting)
    {
      if (this.m_MoveRange != null)
        return;
      this.m_MoveRange = new TacticsSceneCamera.MoveRange();
      if (!setting.OverrideCameraSettings)
        return;
      this.m_MoveRange.isOverride = setting.OverrideCameraSettings;
      this.m_MoveRange.min = 360f - setting.CameraYawMax;
      this.m_MoveRange.max = 360f - setting.CameraYawMin;
      this.m_MoveRange.start = this.m_MoveRange.min;
    }

    public void CreateAllRange()
    {
      if (this.m_AllRange != null)
        return;
      this.m_AllRange = new TacticsSceneCamera.AllRange();
    }

    private void OnEnable()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      TacticsSceneCamera.m_Instance = this;
      SceneBattle.Instance.SetNewCamera(TacticsSceneCamera.instance);
    }

    private void OnDisable() => TacticsSceneCamera.m_Instance = (TacticsSceneCamera) null;

    public static TacticsSceneCamera.RenderSet[] GetRenderSets(GameObject[] gobjs, string[] shaders)
    {
      List<TacticsSceneCamera.RenderSet> renderSetList = new List<TacticsSceneCamera.RenderSet>();
      for (int index = 0; index < gobjs.Length; ++index)
      {
        TacticsSceneCamera.RenderSet renderSet = TacticsSceneCamera.GetRenderSet(gobjs[index], shaders);
        if (renderSet != null)
          renderSetList.Add(renderSet);
      }
      return renderSetList.ToArray();
    }

    public static TacticsSceneCamera.RenderSet GetRenderSet(GameObject gobj, string[] shaders)
    {
      TacticsSceneCamera.RenderSet renderSet = new TacticsSceneCamera.RenderSet();
      MeshRenderer[] componentsInChildren1 = gobj.GetComponentsInChildren<MeshRenderer>();
      for (int index1 = 0; index1 < componentsInChildren1.Length; ++index1)
      {
        if (!Application.isPlaying)
        {
          Material[] sharedMaterials = ((Renderer) componentsInChildren1[index1]).sharedMaterials;
          if (sharedMaterials != null && sharedMaterials.Length > 0)
          {
            for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
            {
              if (TacticsSceneCamera.CheckMaterialShader(sharedMaterials[index2], shaders))
              {
                renderSet.meshRenderers.Add(componentsInChildren1[index1]);
                renderSet.materials.Add(sharedMaterials[index2]);
              }
            }
          }
          else
          {
            Material[] materials = ((Renderer) componentsInChildren1[index1]).materials;
            for (int index3 = 0; index3 < materials.Length; ++index3)
            {
              if (TacticsSceneCamera.CheckMaterialShader(materials[index3], shaders))
              {
                renderSet.meshRenderers.Add(componentsInChildren1[index1]);
                renderSet.materials.Add(materials[index3]);
              }
            }
          }
        }
        else
        {
          Material[] materials = ((Renderer) componentsInChildren1[index1]).materials;
          for (int index4 = 0; index4 < materials.Length; ++index4)
          {
            if (TacticsSceneCamera.CheckMaterialShader(materials[index4], shaders))
            {
              renderSet.meshRenderers.Add(componentsInChildren1[index1]);
              renderSet.materials.Add(materials[index4]);
            }
          }
        }
      }
      SkinnedMeshRenderer[] componentsInChildren2 = gobj.GetComponentsInChildren<SkinnedMeshRenderer>();
      for (int index5 = 0; index5 < componentsInChildren2.Length; ++index5)
      {
        Material[] materials = ((Renderer) componentsInChildren2[index5]).materials;
        for (int index6 = 0; index6 < materials.Length; ++index6)
        {
          if (TacticsSceneCamera.CheckMaterialShader(materials[index6], shaders))
          {
            renderSet.skinRenderers.Add(componentsInChildren2[index5]);
            renderSet.materials.Add(materials[index6]);
          }
        }
      }
      return renderSet.materials.Count == 0 ? (TacticsSceneCamera.RenderSet) null : renderSet;
    }

    private static bool CheckMaterialShader(Material material, string[] shaders)
    {
      if (shaders == null)
        return true;
      for (int index = 0; index < shaders.Length; ++index)
      {
        if (((UnityEngine.Object) material.shader).name == shaders[index])
          return true;
      }
      return false;
    }

    [Serializable]
    public class MoveRange
    {
      public bool isOverride;
      public float min;
      public float max;
      public float start;
    }

    [Serializable]
    public class AllRange
    {
      public bool enable;
      public TacticsSceneCamera.AllRange.Group[] groups;

      [Serializable]
      public class Group
      {
        public string name;
        public float min;
        public float max;
        public float prange;
        public bool fade;
        public GameObject[] gobjs;
      }
    }

    public class AllRangeObj
    {
      public TacticsSceneCamera.AllRange data;
      public TacticsSceneCamera.AllRangeObj.GroupObj[] groups;

      public class GroupObj
      {
        public TacticsSceneCamera.AllRange.Group data;
        public int state;
        public float alpha;
        public List<TacticsSceneCamera.RenderSet> renders = new List<TacticsSceneCamera.RenderSet>();
      }
    }

    public class RenderSet
    {
      public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
      public List<SkinnedMeshRenderer> skinRenderers = new List<SkinnedMeshRenderer>();
      public List<Material> materials = new List<Material>();
    }
  }
}
