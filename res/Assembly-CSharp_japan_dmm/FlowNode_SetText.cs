// Decompiled with JetBrains decompiler
// Type: FlowNode_SetText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("UI/SetText", 32741)]
[FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 0)]
public class FlowNode_SetText : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (UnityEngine.UI.Text), true)]
  public UnityEngine.UI.Text Target;
  public string Text;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    if (Object.op_Inequality((Object) this.Target, (Object) null))
      this.Target.text = this.Text;
    this.ActivateOutputLinks(100);
  }
}
