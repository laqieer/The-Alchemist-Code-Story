﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleGameObjectGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Toggle/GameObjectGroup", 32741)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "Enabled", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(3, "Disabled", FlowNode.PinTypes.Output, 4)]
public class FlowNode_ToggleGameObjectGroup : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject[] Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if (this.Target != null)
        {
          for (int index = 0; index < this.Target.Length; ++index)
          {
            if ((Object) this.Target[index] != (Object) null)
              this.Target[index].SetActive(true);
          }
        }
        this.ActivateOutputLinks(2);
        break;
      case 11:
        if (this.Target != null)
        {
          for (int index = 0; index < this.Target.Length; ++index)
          {
            if ((Object) this.Target[index] != (Object) null)
              this.Target[index].SetActive(false);
          }
        }
        this.ActivateOutputLinks(3);
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
