// Decompiled with JetBrains decompiler
// Type: FlowNode_IsAnimStateInfoName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Animator/IsName", 32741)]
[FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(10, "Output(Yes)", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(11, "Output(No)", FlowNode.PinTypes.Output, 3)]
public class FlowNode_IsAnimStateInfoName : FlowNode
{
  [FlowNode.ShowInInfo]
  public string[] StateName = new string[0];
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    if (this.StateName == null || this.StateName.Length <= 0)
    {
      this.ActivateOutputLinks(11);
    }
    else
    {
      Animator component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
        for (int index = 0; index < this.StateName.Length; ++index)
        {
          if (((AnimatorStateInfo) ref animatorStateInfo).IsName(this.StateName[index]))
          {
            this.ActivateOutputLinks(10);
            return;
          }
        }
      }
      this.ActivateOutputLinks(11);
    }
  }
}
