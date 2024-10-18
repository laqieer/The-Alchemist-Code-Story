// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("会話/閉じる(2D)", "表示されている吹き出しを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndDialog : EventAction
  {
    [StringIsActorID]
    public string ActorID;
    private EventDialogBubbleCustom mBubble;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.ActorID))
      {
        for (int index = EventDialogBubbleCustom.Instances.Count - 1; index >= 0; --index)
          EventDialogBubbleCustom.Instances[index].Close();
      }
      else
      {
        this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
        if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
          this.mBubble.Close();
      }
      this.ActivateNext();
    }
  }
}
