// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Setup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("初期化", "2Dデモの初期化を行います", 5592405, 4473992)]
  public class Event2dAction_Setup : EventAction
  {
    public override void OnActivate()
    {
      this.ActiveCanvas.gameObject.AddComponent<UIZSort>();
      GameUtility.FadeIn(1f);
    }

    public override void Update()
    {
      if (GameUtility.IsScreenFading)
        return;
      this.ActivateNext();
    }
  }
}
