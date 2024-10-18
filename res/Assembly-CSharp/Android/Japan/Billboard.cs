// Decompiled with JetBrains decompiler
// Type: Billboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
