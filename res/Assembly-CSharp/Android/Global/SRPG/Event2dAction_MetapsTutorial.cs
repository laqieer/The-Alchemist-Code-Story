// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MetapsTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("MetapsTutorial用", "チュートリアルのトラッキング埋め込み用", 5592405, 4473992)]
  public class Event2dAction_MetapsTutorial : EventAction
  {
    public string Point = string.Empty;

    public override void OnActivate()
    {
      if (!string.IsNullOrEmpty(this.Point))
      {
        try
        {
        }
        catch
        {
        }
      }
      this.ActivateNext();
    }
  }
}
