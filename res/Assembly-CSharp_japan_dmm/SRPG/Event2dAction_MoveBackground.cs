﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("背景/移動(2D)", "背景を指定した位置に移動させます", 5592405, 4473992)]
  public class Event2dAction_MoveBackground : EventAction
  {
    public float MoveTime;
    public Vector3 MoveFrom;
    public Vector3 MoveTo;
    public bool Async;
    private EventBackGround mBackGround;
    private Vector3 FromPosition;
    private Vector3 ToPosition;
    private readonly float MOVE_TIME = 0.5f;
    private float offset;

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBackGround, (Object) null))
        return;
      this.mBackGround = EventBackGround.Find();
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mBackGround, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        if ((double) this.MoveTime <= 0.0)
          this.MoveTime = this.MOVE_TIME;
        this.FromPosition = this.MoveFrom;
        this.ToPosition = this.MoveTo;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      ((Component) this.mBackGround).transform.localPosition = Vector3.op_Addition(this.FromPosition, Vector3.Scale(Vector3.op_Subtraction(this.ToPosition, this.FromPosition), new Vector3(this.offset, this.offset, this.offset)));
      this.offset += Time.deltaTime / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      ((Component) this.mBackGround).transform.localPosition = this.ToPosition;
      if (!this.Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
