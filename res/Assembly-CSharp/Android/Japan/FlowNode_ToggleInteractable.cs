// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInteractable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[FlowNode.NodeType("Toggle/Interactable", 32741)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_ToggleInteractable : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    Selectable selectable = !((Object) this.Target != (Object) null) ? (Selectable) null : this.Target.GetComponent<Selectable>();
    switch (pinID)
    {
      case 10:
        if ((Object) selectable != (Object) null)
        {
          selectable.interactable = true;
          break;
        }
        break;
      case 11:
        if ((Object) selectable != (Object) null)
        {
          selectable.interactable = false;
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
