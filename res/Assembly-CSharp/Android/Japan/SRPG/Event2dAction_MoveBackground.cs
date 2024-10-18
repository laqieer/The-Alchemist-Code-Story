// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("背景/移動(2D)", "背景を指定した位置に移動させます", 5592405, 4473992)]
  public class Event2dAction_MoveBackground : EventAction
  {
    private readonly float MOVE_TIME = 0.5f;
    public float MoveTime;
    public Vector3 MoveFrom;
    public Vector3 MoveTo;
    public bool Async;
    private EventBackGround mBackGround;
    private Vector3 FromPosition;
    private Vector3 ToPosition;
    private float offset;

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mBackGround == (UnityEngine.Object) null))
        return;
      this.mBackGround = EventBackGround.Find();
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mBackGround == (UnityEngine.Object) null)
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
      this.mBackGround.transform.localPosition = this.FromPosition + Vector3.Scale(this.ToPosition - this.FromPosition, new Vector3(this.offset, this.offset, this.offset));
      this.offset += Time.deltaTime / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      this.mBackGround.transform.localPosition = this.ToPosition;
      if (!this.Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
