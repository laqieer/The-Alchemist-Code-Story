// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorSkip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Animator/Skip", 32741)]
[FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_AnimatorSkip : FlowNode
{
  [FlowNode.ShowInInfo]
  public string StateName = string.Empty;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  private int m_PrevStateHash;

  public override void OnActivate(int pinID)
  {
    Animator component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      if (!string.IsNullOrEmpty(this.StateName))
      {
        if (((AnimatorStateInfo) ref animatorStateInfo).IsName(this.StateName) && this.m_PrevStateHash != ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash)
        {
          component.Play(((AnimatorStateInfo) ref animatorStateInfo).fullPathHash, 0, 1f);
          this.m_PrevStateHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
        }
      }
      else if (this.m_PrevStateHash != ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash)
      {
        component.Play(((AnimatorStateInfo) ref animatorStateInfo).fullPathHash, 0, 1f);
        this.m_PrevStateHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
      }
    }
    this.ActivateOutputLinks(1);
  }
}
