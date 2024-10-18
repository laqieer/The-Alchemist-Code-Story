// Decompiled with JetBrains decompiler
// Type: SkyLayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Sky Layer")]
[RequireComponent(typeof (Renderer))]
public class SkyLayer : MonoBehaviour
{
  public float SkyDistance = 10f;
  public Vector3 SkyOffset = new Vector3(0.0f, 0.0f, 0.0f);
  private bool mVisible;
  private Vector3 mOldPosition;
  private Camera mCamera;

  private void OnEnable()
  {
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
  }

  private void OnDisable()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
  }

  private void OnCameraPreCull(Camera camera)
  {
    this.OnWillRenderObject();
  }

  private void OnWillRenderObject()
  {
    Transform transform1 = Camera.current.transform;
    Transform transform2 = this.transform;
    this.mVisible = true;
    this.mOldPosition = transform2.position;
    transform2.position = Vector3.forward * (transform1.position.z + this.SkyDistance) + this.SkyOffset;
  }

  private void OnRenderObject()
  {
    if (!this.mVisible)
      return;
    this.transform.position = this.mOldPosition;
    this.mVisible = false;
  }
}
