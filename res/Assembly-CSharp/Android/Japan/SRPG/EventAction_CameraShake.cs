// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_CameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
