﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitDisable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("強制終了/禁止(2D)", "強制終了を禁止します", 5592405, 4473992)]
  public class Event2dAction_QuitDisable : EventAction
  {
    public override void OnActivate()
    {
      EventQuit eventQuit = EventQuit.Find();
      if ((UnityEngine.Object) null == (UnityEngine.Object) eventQuit)
      {
        this.ActivateNext();
      }
      else
      {
        eventQuit.gameObject.SetActive(false);
        this.ActivateNext();
      }
    }
  }
}
