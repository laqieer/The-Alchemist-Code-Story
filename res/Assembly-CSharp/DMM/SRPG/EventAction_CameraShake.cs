// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_CameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("カメラ/シェイク", "画面を揺らします。", 5592405, 4473992)]
  public class EventAction_CameraShake : EventAction
  {
    public float Duration = 0.3f;
    public float FrequencyX = 12.51327f;
    public float FrequencyY = 20.4651f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;
    public bool Async;

    public override void OnActivate()
    {
      if ((double) this.Duration <= 0.0)
      {
        this.ActivateNext();
      }
      else
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
        if (!this.Async)
          return;
        this.ActivateNext();
      }
    }

    public override void Update()
    {
      this.Duration -= Time.deltaTime;
      if ((double) this.Duration > 0.0)
        return;
      this.ActivateNext();
    }
  }
}
