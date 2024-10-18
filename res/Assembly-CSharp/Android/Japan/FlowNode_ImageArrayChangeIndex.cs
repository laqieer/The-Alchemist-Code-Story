// Decompiled with JetBrains decompiler
// Type: FlowNode_ImageArrayChangeIndex
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
    ImageArray component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<ImageArray>();
    if ((Object) component != (Object) null)
      component.ImageIndex = this.Index;
    this.ActivateOutputLinks(10);
  }
}
