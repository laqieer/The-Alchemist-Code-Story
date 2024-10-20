﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SELoop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/SE再生(2D)", "SEを再生します", 5592405, 4473992)]
  public class Event2dAction_SELoop : EventAction
  {
    private static List<Event2dAction_SELoop> instances = new List<Event2dAction_SELoop>();
    public string SE;
    public bool Loop;
    [HideInInspector]
    public float Interval = 1f;
    [HideInInspector]
    public int Count = 1;
    [HideInInspector]
    public bool async;
    [HideInInspector]
    public string SE_ID;
    private static readonly float MinInterval = 0.1f;
    private static readonly int MinCount = 0;
    private float mTimer;
    private int mCounter;
    private MySound.CueSheetHandle mHandleSE;
    private static readonly string SECueSheetName = nameof (SE);

    public MySound.CueSheetHandle HandleSE => this.mHandleSE;

    public static Event2dAction_SELoop Find(string id)
    {
      for (int index = 0; index < Event2dAction_SELoop.instances.Count; ++index)
      {
        if (Event2dAction_SELoop.instances[index].SE_ID == id)
          return Event2dAction_SELoop.instances[index];
      }
      return (Event2dAction_SELoop) null;
    }

    protected override void OnDestroy() => Event2dAction_SELoop.instances.Remove(this);

    public override void PreStart()
    {
      this.mHandleSE = MySound.CueSheetHandle.Create(Event2dAction_SELoop.SECueSheetName, MySound.EType.SE);
      if (this.mHandleSE == null)
        return;
      this.mHandleSE.CreateDefaultOneShotSource();
      Event2dAction_SELoop.instances.Add(this);
    }

    public override void OnActivate()
    {
      if (this.Loop)
      {
        this.mTimer = 0.0f;
        this.mCounter = this.Count;
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
      else
      {
        if (this.mHandleSE != null)
          this.mHandleSE.PlayDefaultOneShot(this.SE, false);
        this.ActivateNext();
      }
    }

    public override void Update()
    {
      this.mTimer -= Time.deltaTime;
      if ((double) this.mTimer > 0.0)
        return;
      if (this.mHandleSE != null)
        this.mHandleSE.PlayDefaultOneShot(this.SE, false);
      this.mTimer = this.Interval;
      if (this.mCounter == 0 || --this.mCounter > 0)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}