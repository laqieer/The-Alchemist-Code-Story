// Decompiled with JetBrains decompiler
// Type: SRPG.TriggerCameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
      UnityEngine.Camera main = UnityEngine.Camera.main;
      if ((UnityEngine.Object) main != (UnityEngine.Object) null)
      {
        CameraShakeEffect cameraShakeEffect = main.gameObject.AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = this.Duration;
        cameraShakeEffect.FrequencyX = this.FrequencyX;
        cameraShakeEffect.FrequencyY = this.FrequencyY;
        cameraShakeEffect.AmplitudeX = this.AmplitudeX;
        cameraShakeEffect.AmplitudeY = this.AmplitudeY;
      }
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }
  }
}
