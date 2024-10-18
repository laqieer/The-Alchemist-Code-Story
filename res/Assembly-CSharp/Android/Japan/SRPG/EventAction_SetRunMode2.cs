// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetRunMode2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/走りアニメーション変更", "ユニットの走りアニメーションを変更します。", 5592405, 4473992)]
  public class EventAction_SetRunMode2 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public string AnimationName;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_SetRunMode2.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
    }

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.SetRunAnimation(this.AnimationName);
      }
      this.ActivateNext();
    }
  }
}
