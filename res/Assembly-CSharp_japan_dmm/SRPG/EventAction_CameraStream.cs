// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_CameraStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/カメラ/カメラストリーム", "カメラ制御用のアニメーションを設定します。", 5592405, 4473992)]
  internal class EventAction_CameraStream : EventAction
  {
    public AnimationClip m_CameraAnime;
    public bool m_Async;
    public float Near = 0.01f;
    public float Far = 1000f;
    public bool ScaleToFov;

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.m_CameraAnime, (Object) null))
      {
        Animation animation = ((Component) Camera.main).gameObject.GetComponent<Animation>();
        if (Object.op_Equality((Object) animation, (Object) null))
          animation = ((Component) Camera.main).gameObject.AddComponent<Animation>();
        if (Object.op_Inequality((Object) animation, (Object) null))
        {
          animation.AddClip(this.m_CameraAnime, this.m_CameraAnime.ToString());
          animation.Play(this.m_CameraAnime.ToString());
        }
        Camera.main.nearClipPlane = this.Near;
        Camera.main.farClipPlane = this.Far;
        if (!this.m_Async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      if (!Object.op_Inequality((Object) Camera.main, (Object) null))
        return;
      Vector3 localScale = ((Component) Camera.main).transform.localScale;
      if (this.ScaleToFov)
        Camera.main.fieldOfView = localScale.x;
      Animation component = ((Component) Camera.main).gameObject.GetComponent<Animation>();
      if (!Object.op_Inequality((Object) component, (Object) null) || component.isPlaying)
        return;
      component.Stop();
      if (!this.m_Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
