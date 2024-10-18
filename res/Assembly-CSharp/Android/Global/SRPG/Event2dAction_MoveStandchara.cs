// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵/移動(2D)", "立ち絵を指定した位置に移動させます。", 5592405, 4473992)]
  public class Event2dAction_MoveStandchara : EventAction
  {
    private const float MOVE_TIME = 0.5f;
    public string CharaID;
    public float MoveTime;
    public EventStandChara.PositionTypes MoveTo;
    private EventStandChara mStandChara;
    private Vector3 FromPosition;
    private Vector3 ToPosition;
    private float offset;

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mStandChara == (UnityEngine.Object) null)
      {
        this.ActivateNext();
      }
      else
      {
        if ((double) this.MoveTime <= 0.0)
          this.MoveTime = 0.5f;
        this.FromPosition = this.mStandChara.transform.localPosition;
        RectTransform component = this.mStandChara.GetComponent<RectTransform>();
        RectTransform transform = this.ActiveCanvas.transform as RectTransform;
        float x1 = (float) ((double) transform.rect.width / 2.0 - (double) component.rect.width / 2.0);
        float x2 = (float) ((double) transform.rect.width / 2.0 + (double) component.rect.width / 2.0);
        if (this.MoveTo == EventStandChara.PositionTypes.Left)
          this.ToPosition = new Vector3(-x1, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Center)
          this.ToPosition = new Vector3(0.0f, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Right)
          this.ToPosition = new Vector3(x1, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.OverLeft)
          this.ToPosition = new Vector3(-x2, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo != EventStandChara.PositionTypes.OverRight)
          return;
        this.ToPosition = new Vector3(x2, this.FromPosition.y, this.FromPosition.z);
      }
    }

    public override void Update()
    {
      this.mStandChara.transform.localPosition = this.FromPosition + Vector3.Scale(this.ToPosition - this.FromPosition, new Vector3(this.offset, this.offset, this.offset));
      this.offset += Time.deltaTime / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      this.mStandChara.transform.localPosition = this.ToPosition;
      this.ActivateNext();
    }
  }
}
