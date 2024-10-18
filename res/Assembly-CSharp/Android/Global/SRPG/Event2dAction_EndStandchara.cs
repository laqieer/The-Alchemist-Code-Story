// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("立ち絵/立ち絵消去(2D)", "表示されている立ち絵を消します", 5592405, 4473992)]
  public class Event2dAction_EndStandchara : EventAction
  {
    public string CharaID;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.CharaID))
      {
        for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
          EventStandChara.Instances[index].Close(0.5f);
      }
      else
      {
        EventStandChara eventStandChara = EventStandChara.Find(this.CharaID);
        if ((UnityEngine.Object) eventStandChara != (UnityEngine.Object) null)
          eventStandChara.Close(0.5f);
      }
      this.ActivateNext();
    }

    public override void Update()
    {
    }
  }
}
