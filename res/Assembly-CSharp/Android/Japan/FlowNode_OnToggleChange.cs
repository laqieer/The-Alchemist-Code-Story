// Decompiled with JetBrains decompiler
// Type: FlowNode_OnToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnToggleChange", 58751)]
[FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Output, 2)]
public class FlowNode_OnToggleChange : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  private void Start()
  {
    if ((Object) this.Target != (Object) null)
      this.Target.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
    this.enabled = false;
  }

  private void OnValueChanged(bool value)
  {
    if (value)
      this.Activate(1);
    else
      this.Activate(2);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 1)
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      if (pinID != 2)
        return;
      this.ActivateOutputLinks(2);
    }
  }
}
