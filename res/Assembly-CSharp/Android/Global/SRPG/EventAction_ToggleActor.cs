// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_ToggleActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/表示切替", "シーン上のアクターの表示状態を切り替えます", 5592405, 4473992)]
  public class EventAction_ToggleActor : EventAction
  {
    [StringIsActorID]
    public string ActorID;
    public EventAction_ToggleActor.ToggleTypes Type;
    private GameObject mSummonEffect;

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
      return (IEnumerator) new EventAction_ToggleActor.\u003CPreloadAssets\u003Ec__IteratorB4() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      switch (this.Type)
      {
        case EventAction_ToggleActor.ToggleTypes.Hide:
          if ((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null)
          {
            byUniqueName.SetVisible(false);
            break;
          }
          break;
        case EventAction_ToggleActor.ToggleTypes.Show:
        case EventAction_ToggleActor.ToggleTypes.Summon:
          if ((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null)
          {
            byUniqueName.SetVisible(true);
            if ((UnityEngine.Object) this.mSummonEffect != (UnityEngine.Object) null)
            {
              GameUtility.RequireComponent<OneShotParticle>((UnityEngine.Object.Instantiate((UnityEngine.Object) this.mSummonEffect, byUniqueName.transform.position, byUniqueName.transform.rotation) as GameObject).gameObject);
              break;
            }
            break;
          }
          break;
      }
      this.ActivateNext();
    }

    public override void GoToEndState()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (!((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null))
        return;
      if (this.Type == EventAction_ToggleActor.ToggleTypes.Hide)
        byUniqueName.SetVisible(false);
      else
        byUniqueName.SetVisible(true);
    }

    public enum ToggleTypes
    {
      Hide,
      Show,
      Summon,
    }
  }
}
