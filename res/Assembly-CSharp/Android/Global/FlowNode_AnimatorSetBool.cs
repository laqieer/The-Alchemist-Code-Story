﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorSetBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(3, "Turned Off", FlowNode.PinTypes.Output, 12)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(2, "Turned On", FlowNode.PinTypes.Output, 11)]
[FlowNode.Pin(12, "Toggle", FlowNode.PinTypes.Input, 2)]
[FlowNode.NodeType("Animator/SetBool", 32741)]
[FlowNode.Pin(10, "On", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Off", FlowNode.PinTypes.Input, 1)]
public class FlowNode_AnimatorSetBool : FlowNode
{
  [FlowNode.ShowInInfo]
  public string PropertyName = "None";
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    Animator component = (!((Object) this.Target != (Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
    if ((Object) component != (Object) null)
    {
      bool flag1 = component.GetBool(this.PropertyName);
      switch (pinID)
      {
        case 10:
          component.SetBool(this.PropertyName, true);
          break;
        case 11:
          component.SetBool(this.PropertyName, false);
          break;
        case 12:
          component.SetBool(this.PropertyName, !component.GetBool(this.PropertyName));
          break;
      }
      if (10 <= pinID && pinID <= 12)
      {
        bool flag2 = component.GetBool(this.PropertyName);
        if (flag1 != flag2)
        {
          if (flag2)
            this.ActivateOutputLinks(2);
          else
            this.ActivateOutputLinks(3);
        }
      }
    }
    this.ActivateOutputLinks(1);
  }
}
