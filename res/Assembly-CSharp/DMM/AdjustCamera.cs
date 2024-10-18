// Decompiled with JetBrains decompiler
// Type: AdjustCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class AdjustCamera : MonoBehaviour
{
  [SerializeField]
  private bool m_IsIgnoreAutoAdjust;
  private Camera m_TargetCamera;
  private static Dictionary<Camera, AdjustCamera> s_ComponentCache = new Dictionary<Camera, AdjustCamera>();

  [RuntimeInitializeOnLoadMethod]
  private static void InitializeOnLoad()
  {
    Camera.CameraCallback onPreCull = Camera.onPreCull;
    // ISSUE: reference to a compiler-generated field
    if (AdjustCamera.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdjustCamera.\u003C\u003Ef__mg\u0024cache0 = new Camera.CameraCallback(AdjustCamera.onPreCull);
    }
    // ISSUE: reference to a compiler-generated field
    Camera.CameraCallback fMgCache0 = AdjustCamera.\u003C\u003Ef__mg\u0024cache0;
    Camera.onPreCull = onPreCull + fMgCache0;
  }

  private void Start()
  {
    this.m_TargetCamera = ((Component) this).GetComponent<Camera>();
    if (this.m_IsIgnoreAutoAdjust)
      return;
    AdjustCamera.SetViewportRect(this.m_TargetCamera);
  }

  private static void onPreCull(Camera camera)
  {
    AdjustCamera adjustCamera1 = (AdjustCamera) null;
    if (AdjustCamera.s_ComponentCache.TryGetValue(camera, out adjustCamera1))
    {
      if (!Object.op_Equality((Object) adjustCamera1, (Object) null))
        return;
      AdjustCamera adjustCamera2 = ((Component) camera).gameObject.GetComponent<AdjustCamera>();
      if (Object.op_Equality((Object) adjustCamera2, (Object) null))
        adjustCamera2 = ((Component) camera).gameObject.AddComponent<AdjustCamera>();
      AdjustCamera.s_ComponentCache[camera] = adjustCamera2;
    }
    else
    {
      AdjustCamera adjustCamera3 = ((Component) camera).GetComponent<AdjustCamera>();
      if (Object.op_Equality((Object) adjustCamera3, (Object) null))
        adjustCamera3 = ((Component) camera).gameObject.AddComponent<AdjustCamera>();
      AdjustCamera.s_ComponentCache.Add(camera, adjustCamera3);
      Camera[] array = AdjustCamera.s_ComponentCache.Keys.ToArray<Camera>();
      for (int index = 0; index < array.Length; ++index)
      {
        if (Object.op_Equality((Object) array[index], (Object) null))
          AdjustCamera.s_ComponentCache.Remove(array[index]);
      }
    }
  }

  private static void SetViewportRect(Camera camera)
  {
    if (Object.op_Equality((Object) camera, (Object) null))
      return;
    camera.rect = SetCanvasBounds.GetCameraViewport();
  }
}
