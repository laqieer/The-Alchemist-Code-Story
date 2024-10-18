// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SEStop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/SE停止(2D)", "SEを停止します", 5592405, 4473992)]
  public class Event2dAction_SEStop : EventAction
  {
    public float fadeOutTime = 1f;
    public string SE_ID;
    public bool Async;
    private float elapsedtime;
    private Event2dAction_SELoop seloop;

    public override void OnActivate()
    {
      this.seloop = Event2dAction_SELoop.Find(this.SE_ID);
      if ((UnityEngine.Object) this.seloop != (UnityEngine.Object) null)
      {
        Debug.Log((object) "seloop.HandleSE.StopDefaultAll");
        this.seloop.HandleSE.StopDefaultAll(this.fadeOutTime);
      }
      if (this.Async)
        this.ActivateNext(true);
      this.elapsedtime = 0.0f;
    }

    public override void Update()
    {
      this.elapsedtime += Time.deltaTime;
      if ((double) this.elapsedtime <= (double) this.fadeOutTime)
        return;
      if ((UnityEngine.Object) this.seloop != (UnityEngine.Object) null && this.seloop.enabled)
        this.seloop.enabled = false;
      if (this.Async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}
