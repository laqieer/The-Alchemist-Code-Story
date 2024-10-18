// Decompiled with JetBrains decompiler
// Type: CameraCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public class CameraCallback : MonoBehaviour
{
  public CameraCallback.CameraEvent OnCameraPreCull;
  public CameraCallback.CameraEvent OnCameraPreRender;
  public CameraCallback.CameraEvent OnCameraPostRender;
  public CameraCallback.RenderImageEvent OnCameraRenderImage;

  private void OnPreCull()
  {
    if (this.OnCameraPreCull == null)
      return;
    this.OnCameraPreCull(((Component) this).GetComponent<Camera>());
  }

  private void OnPreRender()
  {
    if (this.OnCameraPreRender == null)
      return;
    this.OnCameraPreRender(((Component) this).GetComponent<Camera>());
  }

  private void OnPostRender()
  {
    if (this.OnCameraPostRender == null)
      return;
    this.OnCameraPostRender(((Component) this).GetComponent<Camera>());
  }

  public delegate void CameraEvent(Camera camera);

  public delegate void RenderImageEvent(Camera camera, RenderTexture src, RenderTexture dest);
}
