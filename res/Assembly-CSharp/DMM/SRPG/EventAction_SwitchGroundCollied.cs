// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SwitchGroundCollied
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/地面判定切り替え", "指定のアクターの地面判定を切り替えます。", 5592405, 4473992)]
  public class EventAction_SwitchGroundCollied : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public bool GroundSnap = true;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (Object.op_Inequality((Object) byUniqueName, (Object) null))
        byUniqueName.CollideGround = this.GroundSnap;
      this.ActivateNext();
    }
  }
}
