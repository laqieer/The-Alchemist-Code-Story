// Decompiled with JetBrains decompiler
// Type: SRPG.TriggerCameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TriggerCameraShake : MonoBehaviour
  {
    public float Duration = 0.3f;
    public float FrequencyX = 10f;
    public float FrequencyY = 10f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;

    private void Start()
    {
      Camera main = Camera.main;
      if (Object.op_Inequality((Object) main, (Object) null))
      {
        CameraShakeEffect cameraShakeEffect = ((Component) main).gameObject.AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = this.Duration;
        cameraShakeEffect.FrequencyX = this.FrequencyX;
        cameraShakeEffect.FrequencyY = this.FrequencyY;
        cameraShakeEffect.AmplitudeX = this.AmplitudeX;
        cameraShakeEffect.AmplitudeY = this.AmplitudeY;
      }
      Object.Destroy((Object) this);
    }
  }
}
