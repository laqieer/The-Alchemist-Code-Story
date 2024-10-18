// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPollFromName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
    if (!((Object) gameObject != (Object) null))
      return;
    this.m_Animator = gameObject.GetComponent<Animator>();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 10)
      return;
    this.enabled = true;
  }

  private void Update()
  {
    if ((Object) this.m_Animator == (Object) null || !this.m_Animator.gameObject.GetActive())
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }
    else if ((Object) this.m_Animator.runtimeAnimatorController == (Object) null || this.m_Animator.runtimeAnimatorController.animationClips == null || this.m_Animator.runtimeAnimatorController.animationClips.Length == 0)
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }
    else
    {
      if (!this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName(this.m_StateName))
        return;
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }
  }
}
