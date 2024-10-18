// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SEStop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/SE停止(2D)", "SEを停止します", 5592405, 4473992)]
  public class Event2dAction_SEStop : EventAction
  {
    public string SE_ID;
    public bool Async;
    public float fadeOutTime = 1f;
    private float elapsedtime;
    private Event2dAction_SELoop seloop;

    public override void OnActivate()
    {
      this.seloop = Event2dAction_SELoop.Find(this.SE_ID);
      if (Object.op_Inequality((Object) this.seloop, (Object) null))
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
      if (Object.op_Inequality((Object) this.seloop, (Object) null) && this.seloop.enabled)
        this.seloop.enabled = false;
      if (this.Async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}
