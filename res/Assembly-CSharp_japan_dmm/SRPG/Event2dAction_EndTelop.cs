// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [EventActionInfo("会話/テロップ閉じる(2D)", "表示されているテロップを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndTelop : EventAction
  {
    public override void OnActivate()
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
        EventTelopBubble.Instances[index].Close();
      this.ActivateNext();
    }
  }
}
