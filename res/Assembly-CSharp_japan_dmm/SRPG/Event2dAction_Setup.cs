// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Setup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("初期化", "2Dデモの初期化を行います", 5592405, 4473992)]
  public class Event2dAction_Setup : EventAction
  {
    public override void OnActivate()
    {
      ((Component) this.EventRootTransform).gameObject.AddComponent<UIZSort>();
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
