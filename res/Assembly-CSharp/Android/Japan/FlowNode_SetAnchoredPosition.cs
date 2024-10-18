// Decompiled with JetBrains decompiler
// Type: FlowNode_SetAnchoredPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
    if ((Object) this.Target != (Object) null)
      this.Target.anchoredPosition = this.TargetPosition;
    this.ActivateOutputLinks(1);
  }
}
