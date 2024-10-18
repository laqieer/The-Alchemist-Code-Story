﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPoll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
        this.mAnimator = !((Object) this.Target != (Object) null) ? this.GetComponent<Animator>() : this.Target.GetComponent<Animator>();
        this.enabled = true;
        this.Update();
        break;
      case 11:
        this.enabled = false;
        break;
    }
  }

  private void Update()
  {
    if ((Object) this.mAnimator == (Object) null || !this.mAnimator.gameObject.GetActive())
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }
    else if ((Object) this.mAnimator.runtimeAnimatorController == (Object) null || this.mAnimator.runtimeAnimatorController.animationClips == null || this.mAnimator.runtimeAnimatorController.animationClips.Length == 0)
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
    else
    {
      if (!this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.StateName))
        return;
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
