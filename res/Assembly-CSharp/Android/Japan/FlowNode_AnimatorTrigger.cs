// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Animator/Trigger", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AnimatorTrigger : FlowNode
{
  [FlowNode.ShowInInfo]
  public string TriggerName = "None";
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  public bool UpdateAnimator;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.TriggerName;
  }

  public override void OnActivate(int pinID)
  {
    Animator component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
    if ((Object) component != (Object) null)
    {
      component.SetTrigger(this.TriggerName);
      if (this.UpdateAnimator)
        component.Update(0.0f);
    }
    this.ActivateOutputLinks(1);
  }
}
