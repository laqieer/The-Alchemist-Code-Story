// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [EventActionInfo("SE再生(2D)", "SEを再生します", 5592405, 4473992)]
  public class Event2dAction_SE : EventAction
  {
    public string SE;

    public override void OnActivate()
    {
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SE, 0.0f);
      this.ActivateNext();
    }
  }
}
