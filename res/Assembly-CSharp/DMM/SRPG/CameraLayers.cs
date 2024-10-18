// Decompiled with JetBrains decompiler
// Type: SRPG.CameraLayers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CameraLayers : MonoBehaviour
  {
    public Camera Overlay;

    public static void Setup(Camera parent)
    {
      if (Object.op_Equality((Object) parent, (Object) null) || Object.op_Inequality((Object) ((Component) parent).GetComponent<CameraLayers>(), (Object) null))
        return;
      ((Component) parent).gameObject.AddComponent<CameraLayers>();
    }

    private void Start()
    {
      this.Overlay = Object.Instantiate<Camera>(GameSettings.Instance.Cameras.OverlayCamera, Vector3.zero, Quaternion.identity);
      ((Component) this.Overlay).transform.SetParent(((Component) this).transform, false);
    }

    private void LateUpdate()
    {
      Camera component = ((Component) this).GetComponent<Camera>();
      foreach (Camera componentsInChild in ((Component) this).GetComponentsInChildren<Camera>())
        componentsInChild.fieldOfView = component.fieldOfView;
    }
  }
}
