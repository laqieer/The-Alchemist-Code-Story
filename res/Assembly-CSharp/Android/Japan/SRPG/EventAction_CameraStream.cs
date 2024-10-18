// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_CameraStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/カメラ/カメラストリーム", "カメラ制御用のアニメーションを設定します。", 5592405, 4473992)]
  internal class EventAction_CameraStream : EventAction
  {
    public float Near = 0.01f;
    public float Far = 1000f;
    public AnimationClip m_CameraAnime;
    public bool m_Async;
    public bool ScaleToFov;

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.m_CameraAnime != (UnityEngine.Object) null)
      {
        Animation animation = UnityEngine.Camera.main.gameObject.GetComponent<Animation>();
        if ((UnityEngine.Object) animation == (UnityEngine.Object) null)
          animation = UnityEngine.Camera.main.gameObject.AddComponent<Animation>();
        if ((UnityEngine.Object) animation != (UnityEngine.Object) null)
        {
          animation.AddClip(this.m_CameraAnime, this.m_CameraAnime.ToString());
          animation.Play(this.m_CameraAnime.ToString());
        }
        UnityEngine.Camera.main.nearClipPlane = this.Near;
        UnityEngine.Camera.main.farClipPlane = this.Far;
        if (!this.m_Async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      if (!((UnityEngine.Object) UnityEngine.Camera.main != (UnityEngine.Object) null))
        return;
      Vector3 localScale = UnityEngine.Camera.main.transform.localScale;
      if (this.ScaleToFov)
        UnityEngine.Camera.main.fieldOfView = localScale.x;
      Animation component = UnityEngine.Camera.main.gameObject.GetComponent<Animation>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || component.isPlaying)
        return;
      component.Stop();
      if (!this.m_Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
