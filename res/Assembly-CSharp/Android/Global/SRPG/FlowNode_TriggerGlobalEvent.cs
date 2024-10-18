﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerGlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Event/TriggerGlobalEvent", 32741)]
  [AddComponentMenu("")]
  public class FlowNode_TriggerGlobalEvent : FlowNode
  {
    [StringIsGlobalEventID]
    public string EventName;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[1]{ "Event is [" + this.EventName + "]" };
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
