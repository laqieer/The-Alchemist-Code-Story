// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorSetInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Animator/Set Int", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AnimatorSetInt : FlowNode
{
  [FlowNode.ShowInInfo]
  public string ParameterName = "None";
  public int Value;
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject Target;
  public bool UpdateAnimator;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.ParameterName;
  }

  public override void OnActivate(int pinID)
  {
    Animator component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
    if ((Object) component != (Object) null)
    {
      component.SetInteger(this.ParameterName, this.Value);
      if (this.UpdateAnimator)
        component.Update(0.0f);
    }
    this.ActivateOutputLinks(1);
  }
}
