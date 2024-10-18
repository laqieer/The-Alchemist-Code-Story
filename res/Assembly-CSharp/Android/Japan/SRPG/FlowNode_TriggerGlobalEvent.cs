// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerGlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/TriggerGlobalEvent", 32741)]
  [FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TriggerGlobalEvent : FlowNode
  {
    [StringIsGlobalEventID]
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
      GlobalEvent.Invoke(this.EventName, (object) this);
      this.ActivateOutputLinks(1);
    }
  }
}
