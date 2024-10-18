// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Animator/Play", 32741)]
[FlowNode.Pin(0, "Play", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
public class FlowNode_AnimatorPlay : FlowNode
{
  [FlowNode.ShowInInfo]
  public string StateName = "open";
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    Animator component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
    if ((Object) component != (Object) null)
      component.Play(this.StateName);
    this.ActivateOutputLinks(10);
  }
}
