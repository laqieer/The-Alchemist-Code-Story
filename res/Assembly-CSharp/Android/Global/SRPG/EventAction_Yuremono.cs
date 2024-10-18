// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Yuremono
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
