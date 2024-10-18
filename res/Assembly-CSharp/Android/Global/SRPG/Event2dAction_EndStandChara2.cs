// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandChara2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵2/立ち絵消去(2D)", "表示されている立ち絵を消します", 5592405, 4473992)]
  public class Event2dAction_EndStandChara2 : EventAction
  {
    private const float WAIT_SECONDS = 1f;
    public string CharaID;
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
          instances.Close(0.3f);
      }
      this.mTimer = 1f;
    }

    public override void Update()
    {
      this.mTimer -= Time.deltaTime;
      if ((double) this.mTimer > 0.0)
        return;
      this.ActivateNext();
    }
  }
}
