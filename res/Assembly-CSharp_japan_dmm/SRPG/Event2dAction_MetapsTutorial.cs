// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MetapsTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
          MyMetaps.TrackTutorialPoint(this.Point);
        }
        catch
        {
        }
      }
      this.ActivateNext();
    }
  }
}
