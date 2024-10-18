// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_ToggleActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/表示切替", "シーン上のアクターの表示状態を切り替えます", 5592405, 4473992)]
  public class EventAction_ToggleActor2 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public EventAction_ToggleActor2.ToggleTypes Type;
    private GameObject mSummonEffect;

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_ToggleActor2.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      switch (this.Type)
      {
        case EventAction_ToggleActor2.ToggleTypes.Hide:
          if (Object.op_Inequality((Object) byUniqueName, (Object) null))
          {
            byUniqueName.SetVisible(false);
            break;
          }
          break;
        case EventAction_ToggleActor2.ToggleTypes.Show:
        case EventAction_ToggleActor2.ToggleTypes.Summon:
          if (Object.op_Inequality((Object) byUniqueName, (Object) null))
          {
            byUniqueName.SetVisible(true);
            if (Object.op_Inequality((Object) this.mSummonEffect, (Object) null))
            {
              GameUtility.RequireComponent<OneShotParticle>(Object.Instantiate<GameObject>(this.mSummonEffect, ((Component) byUniqueName).transform.position, ((Component) byUniqueName).transform.rotation).gameObject);
              break;
            }
            break;
          }
          break;
      }
      this.ActivateNext();
    }

    public enum ToggleTypes
    {
      Hide,
      Show,
      Summon,
    }
  }
}
