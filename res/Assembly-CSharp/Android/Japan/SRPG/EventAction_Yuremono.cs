// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Yuremono
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/揺れもの切替", "アクターの揺れもの状態を切り替えます。", 6702148, 11158596)]
  public class EventAction_Yuremono : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public bool EnableYuremono;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if ((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null)
      {
        foreach (Behaviour componentsInChild in byUniqueName.gameObject.GetComponentsInChildren<YuremonoInstance>())
          componentsInChild.enabled = this.EnableYuremono;
      }
      this.ActivateNext();
    }
  }
}
