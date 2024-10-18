// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/ToggleChange", 58751)]
[FlowNode.Pin(1, "enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(2, "disable", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ToggleChange : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  public override void OnActivate(int pinID)
  {
    if (!((Object) this.Target != (Object) null))
      return;
    if (pinID == 1)
    {
      this.Target.isOn = true;
    }
    else
    {
      if (pinID != 2)
        return;
      this.Target.isOn = false;
    }
  }
}
