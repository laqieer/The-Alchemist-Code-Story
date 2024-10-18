// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DestroyActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/削除", "指定のアクターを削除します。", 6702148, 11158596)]
  public class EventAction_DestroyActor : EventAction
  {
    [StringIsActorList]
    public string ActorID;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
      }
      this.ActivateNext();
    }
  }
}
