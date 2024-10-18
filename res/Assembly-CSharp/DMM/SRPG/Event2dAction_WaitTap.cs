// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_WaitTap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/待機", "待機します。", 5592405, 4473992)]
  public class Event2dAction_WaitTap : EventAction
  {
    public bool tapWaiting;
    [HideInInspector]
    public float WaitSeconds = 1f;
    private float mTimer;
    private bool waitFrame;

    public override void OnActivate()
    {
      this.waitFrame = false;
      if (this.tapWaiting)
        return;
      this.mTimer = this.WaitSeconds;
    }

    public override void Update()
    {
      if (!this.waitFrame)
      {
        this.waitFrame = true;
      }
      else
      {
        if (this.tapWaiting)
          return;
        this.mTimer -= Time.deltaTime;
        if ((double) this.mTimer > 0.0)
          return;
        this.ActivateNext();
      }
    }

    public override bool Forward()
    {
      if (!this.waitFrame || !this.tapWaiting)
        return false;
      this.ActivateNext();
      return true;
    }
  }
}
