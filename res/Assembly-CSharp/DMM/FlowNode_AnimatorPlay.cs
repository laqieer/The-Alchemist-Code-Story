// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
    Animator component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Play(this.StateName);
    this.ActivateOutputLinks(10);
  }
}
