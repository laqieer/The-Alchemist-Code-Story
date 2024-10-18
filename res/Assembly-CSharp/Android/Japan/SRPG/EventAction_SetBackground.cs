// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
