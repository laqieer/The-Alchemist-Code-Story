// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetRunMode2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      return (IEnumerator) new EventAction_SetRunMode2.\u003CPreloadAssets\u003Ec__IteratorAB() { \u003C\u003Ef__this = this };
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
