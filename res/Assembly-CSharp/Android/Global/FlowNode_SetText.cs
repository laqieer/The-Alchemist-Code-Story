// Decompiled with JetBrains decompiler
// Type: FlowNode_SetText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("UI/SetText", 32741)]
[FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
public class FlowNode_SetText : FlowNode
{
  [FlowNode.DropTarget(typeof (UnityEngine.UI.Text), true)]
  [FlowNode.ShowInInfo]
  public UnityEngine.UI.Text Target;
  public string Text;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    if ((Object) this.Target != (Object) null)
      this.Target.text = this.Text;
    this.ActivateOutputLinks(100);
  }
}
