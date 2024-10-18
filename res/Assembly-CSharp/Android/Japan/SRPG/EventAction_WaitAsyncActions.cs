// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitAsyncActions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
