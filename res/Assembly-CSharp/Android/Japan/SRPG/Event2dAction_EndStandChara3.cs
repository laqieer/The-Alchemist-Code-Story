// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandChara3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/立ち絵消去2(2D)", "表示されている立ち絵を消します", 5592405, 4473992)]
  public class Event2dAction_EndStandChara3 : EventAction
  {
    public float FadeTime = 1f;
    public string CharaID;
    public bool Async;
    private float mTimer;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.CharaID))
      {
        for (int index = EventStandCharaController2.Instances.Count - 1; index >= 0; --index)
          EventStandCharaController2.Instances[index].Close(0.3f);
      }
      else
      {
        EventStandCharaController2 instances = EventStandCharaController2.FindInstances(this.CharaID);
        if ((UnityEngine.Object) instances != (UnityEngine.Object) null)
          instances.Close(this.FadeTime);
      }
      this.mTimer = this.FadeTime;
      if (!this.Async)
        return;
      this.ActivateNext(true);
    }

    public override void Update()
    {
      this.mTimer -= Time.deltaTime;
      if ((double) this.mTimer > 0.0)
        return;
      if (!this.Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
