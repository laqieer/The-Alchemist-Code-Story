// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitAsyncActions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("同期", "非同期処理が完了するのを待ちます", 5592405, 4473992)]
  public class EventAction_WaitAsyncActions : EventAction
  {
    public override void OnActivate()
    {
    }

    public override void Update()
    {
      for (int index = 0; index < this.Sequence.Actions.Length && !Object.op_Equality((Object) this.Sequence.Actions[index], (Object) this); ++index)
      {
        if (this.Sequence.Actions[index].enabled)
          return;
      }
      this.ActivateNext();
    }
  }
}
