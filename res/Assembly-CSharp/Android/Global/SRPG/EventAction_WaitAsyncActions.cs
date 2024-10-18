// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitAsyncActions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      for (int index = 0; index < this.Sequence.Actions.Length && !((UnityEngine.Object) this.Sequence.Actions[index] == (UnityEngine.Object) this); ++index)
      {
        if (this.Sequence.Actions[index].enabled)
          return;
      }
      this.ActivateNext();
    }
  }
}
