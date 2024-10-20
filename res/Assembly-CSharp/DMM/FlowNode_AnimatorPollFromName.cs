﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPollFromName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Animator/PollFromName", 32741)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
public class FlowNode_AnimatorPollFromName : FlowNode
{
  public string m_AnimatorName = string.Empty;
  public string m_StateName = string.Empty;
  private Animator m_Animator;

  protected override void Awake()
  {
    base.Awake();
    GameObject gameObject = GameObject.Find(this.m_AnimatorName);
    if (!Object.op_Inequality((Object) gameObject, (Object) null))
      return;
    this.m_Animator = gameObject.GetComponent<Animator>();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 10)
      return;
    ((Behaviour) this).enabled = true;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_Animator, (Object) null) || !((Component) this.m_Animator).gameObject.GetActive())
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }
    else if (Object.op_Equality((Object) this.m_Animator.runtimeAnimatorController, (Object) null) || this.m_Animator.runtimeAnimatorController.animationClips == null || this.m_Animator.runtimeAnimatorController.animationClips.Length == 0)
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }
    else
    {
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).IsName(this.m_StateName))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }
  }
}