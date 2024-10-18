// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DestroyObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/オブジェクト/削除", "指定のオブジェクトを削除します。", 6702148, 11158596)]
  public class EventAction_DestroyObject : EventAction
  {
    [SerializeField]
    [StringIsObjectList]
    public string TargetID;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Inequality((Object) actor, (Object) null))
        Object.Destroy((Object) actor);
      this.ActivateNext();
    }
  }
}
