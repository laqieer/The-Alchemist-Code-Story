// Decompiled with JetBrains decompiler
// Type: SkyLayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Renderer))]
[AddComponentMenu("Rendering/Sky Layer")]
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

  private void OnCameraPreCull(Camera camera) => this.OnWillRenderObject();

  private void OnWillRenderObject()
  {
    Transform transform1 = ((Component) Camera.current).transform;
    Transform transform2 = ((Component) this).transform;
    this.mVisible = true;
    this.mOldPosition = transform2.position;
    transform2.position = Vector3.op_Addition(Vector3.op_Multiply(Vector3.forward, transform1.position.z + this.SkyDistance), this.SkyOffset);
  }

  private void OnRenderObject()
  {
    if (!this.mVisible)
      return;
    ((Component) this).transform.position = this.mOldPosition;
    this.mVisible = false;
  }
}
