// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_WaitTap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/待機", "待機します。", 5592405, 4473992)]
  public class Event2dAction_WaitTap : EventAction
  {
    [HideInInspector]
    public float WaitSeconds = 1f;
    public bool tapWaiting;
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
