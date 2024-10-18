// Decompiled with JetBrains decompiler
// Type: FlowNode_SetAnchoredPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("UI/SetAnchoredPosition", 32741)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
public class FlowNode_SetAnchoredPosition : FlowNode
{
  [FlowNode.DropTarget(typeof (RectTransform), true)]
  [FlowNode.ShowInInfo]
  public RectTransform Target;
  public Vector2 TargetPosition;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    if (Object.op_Inequality((Object) this.Target, (Object) null))
      this.Target.anchoredPosition = this.TargetPosition;
    this.ActivateOutputLinks(1);
  }
}
