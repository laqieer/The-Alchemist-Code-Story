// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Yuremono
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Inequality((Object) byUniqueName, (Object) null))
      {
        foreach (Behaviour componentsInChild in ((Component) byUniqueName).gameObject.GetComponentsInChildren<YuremonoInstance>())
          componentsInChild.enabled = this.EnableYuremono;
      }
      this.ActivateNext();
    }
  }
}
