// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SwitchGroundCollied
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("New/アクター/地面判定切り替え", "指定のアクターの地面判定を切り替えます。", 5592405, 4473992)]
  public class EventAction_SwitchGroundCollied : EventAction
  {
    public bool GroundSnap = true;
    [StringIsActorList]
    public string ActorID;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if ((UnityEngine.Object) byUniqueName != (UnityEngine.Object) null)
        byUniqueName.CollideGround = this.GroundSnap;
      this.ActivateNext();
    }
  }
}
