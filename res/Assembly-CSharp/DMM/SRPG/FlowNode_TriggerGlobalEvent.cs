// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerGlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
