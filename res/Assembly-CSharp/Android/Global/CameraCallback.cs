// Decompiled with JetBrains decompiler
// Type: CameraCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[ExecuteInEditMode]
[DisallowMultipleComponent]
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
    this.OnCameraPreCull(this.GetComponent<Camera>());
  }

  private void OnPreRender()
  {
    if (this.OnCameraPreRender == null)
      return;
    this.OnCameraPreRender(this.GetComponent<Camera>());
  }

  private void OnPostRender()
  {
    if (this.OnCameraPostRender == null)
      return;
    this.OnCameraPostRender(this.GetComponent<Camera>());
  }

  public delegate void CameraEvent(Camera camera);

  public delegate void RenderImageEvent(Camera camera, RenderTexture src, RenderTexture dest);
}
