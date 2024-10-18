// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetCameraBG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/背景イメージを変更(2D)", "カメラの背景イメージを変更します", 5592405, 4473992)]
  public class Event2dAction_SetCameraBG : EventAction
  {
    [HideInInspector]
    public Texture2D BackgroundImage;

    public override void OnActivate()
    {
      this.ActivateNext();
    }
  }
}
