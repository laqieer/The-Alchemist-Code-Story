// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DestroyObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/削除", "指定のオブジェクトを削除します。", 6702148, 11158596)]
  public class EventAction_DestroyObject : EventAction
  {
    [StringIsObjectList]
    [SerializeField]
    public string TargetID;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) actor);
      this.ActivateNext();
    }
  }
}
