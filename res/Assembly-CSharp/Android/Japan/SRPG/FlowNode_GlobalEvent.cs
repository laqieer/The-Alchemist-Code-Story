// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

    private void OnGlobalEvent(object obj)
    {
      this.ActivateOutputLinks(1);
    }
  }
}
