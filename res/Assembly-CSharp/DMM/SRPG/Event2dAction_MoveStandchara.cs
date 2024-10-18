// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("立ち絵/移動(2D)", "立ち絵を指定した位置に移動させます。", 5592405, 4473992)]
  public class Event2dAction_MoveStandchara : EventAction
  {
    public string CharaID;
    public float MoveTime;
    public EventStandChara.PositionTypes MoveTo;
    private EventStandChara mStandChara;
    private Vector3 FromPosition;
    private Vector3 ToPosition;
    private const float MOVE_TIME = 0.5f;
    private float offset;

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = EventStandChara.Find(this.CharaID);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mStandChara, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        if ((double) this.MoveTime <= 0.0)
          this.MoveTime = 0.5f;
        this.FromPosition = ((Component) this.mStandChara).transform.localPosition;
        RectTransform component = ((Component) this.mStandChara).GetComponent<RectTransform>();
        RectTransform eventRootTransform = this.EventRootTransform;
        Rect rect1 = eventRootTransform.rect;
        double num1 = (double) ((Rect) ref rect1).width / 2.0;
        Rect rect2 = component.rect;
        double num2 = (double) ((Rect) ref rect2).width / 2.0;
        float num3 = (float) (num1 - num2);
        Rect rect3 = eventRootTransform.rect;
        double num4 = (double) ((Rect) ref rect3).width / 2.0;
        Rect rect4 = component.rect;
        double num5 = (double) ((Rect) ref rect4).width / 2.0;
        float num6 = (float) (num4 + num5);
        if (this.MoveTo == EventStandChara.PositionTypes.Left)
          this.ToPosition = new Vector3(-num3, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Center)
          this.ToPosition = new Vector3(0.0f, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.Right)
          this.ToPosition = new Vector3(num3, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo == EventStandChara.PositionTypes.OverLeft)
          this.ToPosition = new Vector3(-num6, this.FromPosition.y, this.FromPosition.z);
        if (this.MoveTo != EventStandChara.PositionTypes.OverRight)
          return;
        this.ToPosition = new Vector3(num6, this.FromPosition.y, this.FromPosition.z);
      }
    }

    public override void Update()
    {
      ((Component) this.mStandChara).transform.localPosition = Vector3.op_Addition(this.FromPosition, Vector3.Scale(Vector3.op_Subtraction(this.ToPosition, this.FromPosition), new Vector3(this.offset, this.offset, this.offset)));
      this.offset += Time.deltaTime / this.MoveTime;
      if ((double) this.offset < 1.0)
        return;
      this.offset = 1f;
      ((Component) this.mStandChara).transform.localPosition = this.ToPosition;
      this.ActivateNext();
    }
  }
}
