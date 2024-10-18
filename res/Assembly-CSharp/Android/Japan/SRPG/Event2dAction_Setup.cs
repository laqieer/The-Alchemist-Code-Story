// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Setup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
