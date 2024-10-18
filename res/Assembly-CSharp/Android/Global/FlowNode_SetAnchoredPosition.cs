// Decompiled with JetBrains decompiler
// Type: FlowNode_SetAnchoredPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("UI/SetAnchoredPosition", 32741)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
public class FlowNode_SetAnchoredPosition : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (RectTransform), true)]
  public RectTransform Target;
  public Vector2 TargetPosition;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    if ((Object) this.Target != (Object) null)
      this.Target.anchoredPosition = this.TargetPosition;
    this.ActivateOutputLinks(1);
  }
}
