// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
          EventStandChara.Instances[index].Close();
      }
      else
      {
        EventStandChara eventStandChara = EventStandChara.Find(this.CharaID);
        if (Object.op_Inequality((Object) eventStandChara, (Object) null))
          eventStandChara.Close();
      }
      this.ActivateNext();
    }

    public override void Update()
    {
    }
  }
}
