// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerLocalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/TriggerLocalEvent", 32741)]
[FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
public class FlowNode_TriggerLocalEvent : FlowNode
{
  [StringIsLocalEventID]
  public string EventName;

  public override string[] GetInfoLines()
  {
    if (string.IsNullOrEmpty(this.EventName))
      return base.GetInfoLines();
    return new string[1]{ "Event is " + this.EventName };
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 100 || string.IsNullOrEmpty(this.EventName))
      return;
    FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.EventName);
    this.ActivateOutputLinks(1);
  }

  public static void TriggerLocalEvent(Component caller, string EventName)
  {
    FlowNode_LocalEvent[] componentsInChildren = caller.GetComponentsInChildren<FlowNode_LocalEvent>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].gameObject.activeInHierarchy && componentsInChildren[index].EventName == EventName)
        componentsInChildren[index].Activate(-1);
    }
  }
}
