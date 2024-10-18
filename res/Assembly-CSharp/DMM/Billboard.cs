// Decompiled with JetBrains decompiler
// Type: Billboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
    Transform transform1 = ((Component) this).transform;
    Transform transform2 = ((Component) camera).transform;
    transform1.rotation = Quaternion.LookRotation(transform2.forward, transform2.up);
  }
}
