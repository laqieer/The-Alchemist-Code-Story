// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/背景イメージを変更", "カメラの背景イメージを変更します", 5592405, 4473992)]
  public class EventAction_SetBackground : EventAction
  {
    [HideInInspector]
    public Texture2D BackgroundImage;

    public override void OnActivate()
    {
      RenderPipeline renderPipeline = GameUtility.RequireComponent<RenderPipeline>(UnityEngine.Camera.main.gameObject);
      if ((UnityEngine.Object) this.BackgroundImage != (UnityEngine.Object) null)
        renderPipeline.BackgroundImage = (Texture) this.BackgroundImage;
      else if ((UnityEngine.Object) TacticsSceneSettings.Instance != (UnityEngine.Object) null)
        renderPipeline.BackgroundImage = (Texture) TacticsSceneSettings.Instance.BackgroundImage;
      this.ActivateNext();
    }
  }
}
