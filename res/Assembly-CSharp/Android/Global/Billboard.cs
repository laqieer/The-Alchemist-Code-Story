// Decompiled with JetBrains decompiler
// Type: Billboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Billboard")]
public class Billboard : MonoBehaviour
{
  private void OnEnable()
  {
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  private void OnDisable()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  private void PreCull(Camera camera)
  {
    Transform transform1 = this.transform;
    Transform transform2 = camera.transform;
    transform1.rotation = Quaternion.LookRotation(transform2.forward, transform2.up);
  }
}
