// Decompiled with JetBrains decompiler
// Type: FlowNode_ImageArrayChangeIndex
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("UI/ImageArrayChangeIndex", 32741)]
[FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
public class FlowNode_ImageArrayChangeIndex : FlowNode
{
  [FlowNode.ShowInInfo]
  public int Index;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    ImageArray component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<ImageArray>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.ImageIndex = this.Index;
    this.ActivateOutputLinks(10);
  }
}
