﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/GlobalEvent", 58751)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_GlobalEvent : FlowNodePersistent
  {
    [StringIsGlobalEventID]
    public string EventName;
    private string mRegisteredEventName;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[1]{ "Event is " + this.EventName };
    }

    protected override void Awake()
    {
      base.Awake();
      if (string.IsNullOrEmpty(this.EventName))
        return;
      GlobalEvent.AddListener(this.EventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
      this.mRegisteredEventName = this.EventName;
    }

    protected override void OnDestroy()
    {
      if (string.IsNullOrEmpty(this.mRegisteredEventName))
        return;
      GlobalEvent.RemoveListener(this.mRegisteredEventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
    }

    private void OnGlobalEvent(object obj) => this.ActivateOutputLinks(1);
  }
}
