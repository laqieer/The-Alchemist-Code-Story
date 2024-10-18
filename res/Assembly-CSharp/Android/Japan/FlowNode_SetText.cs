// Decompiled with JetBrains decompiler
// Type: FlowNode_SetText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
    if ((Object) this.Target != (Object) null)
      this.Target.text = this.Text;
    this.ActivateOutputLinks(100);
  }
}
