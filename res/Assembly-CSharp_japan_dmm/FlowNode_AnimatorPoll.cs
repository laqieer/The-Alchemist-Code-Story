// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPoll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Animator/Poll", 32741)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Cancel", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "NoAnim", FlowNode.PinTypes.Output, 3)]
public class FlowNode_AnimatorPoll : FlowNode
{
  [FlowNode.ShowInInfo]
  public string StateName = string.Empty;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  private Animator mAnimator;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        this.mAnimator = !Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).GetComponent<Animator>() : this.Target.GetComponent<Animator>();
        ((Behaviour) this).enabled = true;
        this.Update();
        break;
      case 11:
        ((Behaviour) this).enabled = false;
        break;
    }
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.mAnimator, (Object) null) || !((Component) this.mAnimator).gameObject.GetActive())
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }
    else if (Object.op_Equality((Object) this.mAnimator.runtimeAnimatorController, (Object) null) || this.mAnimator.runtimeAnimatorController.animationClips == null || this.mAnimator.runtimeAnimatorController.animationClips.Length == 0)
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }
    else
    {
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).IsName(this.StateName))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
