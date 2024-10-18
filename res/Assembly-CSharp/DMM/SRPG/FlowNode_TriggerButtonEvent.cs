// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/TriggerButtonEvent", 16087213)]
  [FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TriggerButtonEvent : FlowNode
  {
    public bool Force;
    public string EventName = string.Empty;
    public SerializeValue Value = new SerializeValue();

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 || string.IsNullOrEmpty(this.EventName))
        return;
      if (this.Force)
        ButtonEvent.ForceInvoke(this.EventName, (object) this.Value);
      else
        ButtonEvent.Invoke(this.EventName, (object) this.Value);
      this.ActivateOutputLinks(1);
    }
  }
}
