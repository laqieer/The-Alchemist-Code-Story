// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorSetNormalizedTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Animator/Set Normalized Time", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AnimatorSetNormalizedTime : FlowNode
{
  [FlowNode.ShowInInfo]
  public float NormalizedTime;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  public bool UpdateAnimator;

  public override void OnActivate(int pinID)
  {
    Animator component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
    if ((Object) component != (Object) null)
    {
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      component.Play(animatorStateInfo.fullPathHash, 0, this.NormalizedTime);
      if (this.UpdateAnimator)
        component.Update(0.0f);
    }
    this.ActivateOutputLinks(1);
  }
}
