// Decompiled with JetBrains decompiler
// Type: SRPG.CameraLayers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CameraLayers : MonoBehaviour
  {
    public UnityEngine.Camera Overlay;

    public static void Setup(UnityEngine.Camera parent)
    {
      if ((UnityEngine.Object) parent == (UnityEngine.Object) null || (UnityEngine.Object) parent.GetComponent<CameraLayers>() != (UnityEngine.Object) null)
        return;
      parent.gameObject.AddComponent<CameraLayers>();
    }

    private void Start()
    {
      this.Overlay = UnityEngine.Object.Instantiate<UnityEngine.Camera>(GameSettings.Instance.Cameras.OverlayCamera, Vector3.zero, Quaternion.identity);
      this.Overlay.transform.SetParent(this.transform, false);
    }

    private void LateUpdate()
    {
      UnityEngine.Camera component = this.GetComponent<UnityEngine.Camera>();
      foreach (UnityEngine.Camera componentsInChild in this.GetComponentsInChildren<UnityEngine.Camera>())
        componentsInChild.fieldOfView = component.fieldOfView;
    }
  }
}
