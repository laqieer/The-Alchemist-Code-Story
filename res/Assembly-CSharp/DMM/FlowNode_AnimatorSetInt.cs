// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorSetInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Animator/Set Int", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AnimatorSetInt : FlowNode
{
  [FlowNode.ShowInInfo]
  public string ParameterName = "None";
  public int Value;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  public bool UpdateAnimator;

  public override string GetCaption() => base.GetCaption() + ":" + this.ParameterName;

  public override void OnActivate(int pinID)
  {
    Animator component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.SetInteger(this.ParameterName, this.Value);
      if (this.UpdateAnimator)
        component.Update(0.0f);
    }
    this.ActivateOutputLinks(1);
  }
}
