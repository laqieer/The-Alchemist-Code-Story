// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitDisable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("強制終了/禁止(2D)", "強制終了を禁止します", 5592405, 4473992)]
  public class Event2dAction_QuitDisable : EventAction
  {
    public override void OnActivate()
    {
      EventQuit eventQuit = EventQuit.Find();
      if (Object.op_Equality((Object) null, (Object) eventQuit))
      {
        this.ActivateNext();
      }
      else
      {
        ((Component) eventQuit).gameObject.SetActive(false);
        EventScript.ActiveButtons(false);
        this.ActivateNext();
      }
    }
  }
}
