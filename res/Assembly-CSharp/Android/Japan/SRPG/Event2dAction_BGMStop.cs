﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_BGMStop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/BGM停止(2D)", "BGMを停止します", 5592405, 4473992)]
  public class Event2dAction_BGMStop : EventAction
  {
    public float fadeOutTime = 1f;
    public bool Async;
    private float elapsedtime;
    private Event2dAction_BGMStop.STOP_STATE state;

    public override void OnActivate()
    {
      this.elapsedtime = 0.0f;
      this.state = Event2dAction_BGMStop.STOP_STATE.PREPARE;
    }

    public override void Update()
    {
      if (this.state == Event2dAction_BGMStop.STOP_STATE.PREPARE)
      {
        if (MonoSingleton<MySound>.Instance.StopBGMFadeOut(this.fadeOutTime))
        {
          if (this.Async)
          {
            this.ActivateNext();
            this.state = Event2dAction_BGMStop.STOP_STATE.STOPPED;
          }
          else
          {
            this.state = Event2dAction_BGMStop.STOP_STATE.STOPPING;
            this.elapsedtime = 0.0f;
          }
        }
        if (!(bool) ((UnityEngine.Object) SceneBattle.Instance))
          return;
        SceneBattle.Instance.EventPlayBgmID = (string) null;
      }
      else
      {
        if (this.state != Event2dAction_BGMStop.STOP_STATE.STOPPING)
          return;
        this.elapsedtime += Time.deltaTime;
        if ((double) this.elapsedtime <= (double) this.fadeOutTime)
          return;
        this.state = Event2dAction_BGMStop.STOP_STATE.STOPPED;
        this.ActivateNext();
      }
    }

    private enum STOP_STATE
    {
      PREPARE,
      STOPPING,
      STOPPED,
    }
  }
}
