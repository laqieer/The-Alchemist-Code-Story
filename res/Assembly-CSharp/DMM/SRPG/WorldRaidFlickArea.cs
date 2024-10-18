// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidFlickArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "フリック完了", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "左フリック", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "右フリック", FlowNode.PinTypes.Output, 11)]
  public class WorldRaidFlickArea : 
    TouchController,
    IFlowInterface,
    IPointerClickHandler,
    IEventSystemHandler
  {
    private const int PIN_INPUT_FLICK_FINISH = 1;
    private const int PIN_OUT_LEFT_FLICK = 10;
    private const int PIN_OUT_RIGHT_FLICK = 11;
    [SerializeField]
    private RectTransform MoveObj;
    [SerializeField]
    private float FlickJugdeNum;
    private bool mFlickFlag;
    private bool mFlickAnimFlag;
    private Vector3 mStartPos;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.FlickEnd();
    }

    private void Start()
    {
      this.mFlickFlag = false;
      this.mFlickAnimFlag = false;
      if (!Object.op_Inequality((Object) this.MoveObj, (Object) null))
        return;
      this.mStartPos = ((Transform) this.MoveObj).localPosition;
    }

    private void Update()
    {
      if (!this.mFlickFlag)
      {
        if ((double) this.DragDelta.x >= (double) this.FlickJugdeNum)
        {
          this.mFlickFlag = true;
          this.mFlickAnimFlag = true;
          this.FlickRight();
        }
        else if ((double) this.DragDelta.x <= -(double) this.FlickJugdeNum)
        {
          this.mFlickFlag = true;
          this.mFlickAnimFlag = true;
          this.FlickLeft();
        }
        if (!Object.op_Inequality((Object) this.MoveObj, (Object) null))
          return;
        if (this.IsTouching && (double) this.DragDelta.x != 0.0)
        {
          ((Transform) this.MoveObj).localPosition = new Vector3(this.DragDelta.x / 2f + this.mStartPos.x, this.mStartPos.y, this.mStartPos.z);
        }
        else
        {
          if (this.IsTouching || this.mFlickAnimFlag)
            return;
          ((Transform) this.MoveObj).localPosition = this.mStartPos;
        }
      }
      else
      {
        if (this.IsTouching)
          return;
        this.DragDelta = Vector2.zero;
        this.mFlickFlag = false;
      }
    }

    private void FlickLeft()
    {
      this.DragDelta = Vector2.zero;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void FlickRight()
    {
      this.DragDelta = Vector2.zero;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void FlickEnd()
    {
      if (!this.IsTouching)
        this.DragDelta = Vector2.zero;
      ((Transform) this.MoveObj).localPosition = this.mStartPos;
      this.mFlickAnimFlag = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
  }
}
