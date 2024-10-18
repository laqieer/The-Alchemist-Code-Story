// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerGlobalEventExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.Pin(101, "Back", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Event/TriggerGlobalEventExt", 32741)]
  public class FlowNode_TriggerGlobalEventExt : FlowNode_TriggerGlobalEvent
  {
    [StringIsGlobalEventID]
    public string CurrEventName;
    public bool SceneChange;

    public override void OnActivate(int pinID)
    {
      if (pinID == 100 && !string.IsNullOrEmpty(this.EventName))
      {
        GlobalVars.PreEventName = this.CurrEventName;
        this.SceneInvoke(this.EventName);
      }
      if (pinID != 101)
        return;
      if (!string.IsNullOrEmpty(GlobalVars.PreEventName))
        this.SceneInvoke(GlobalVars.PreEventName);
      else if (!string.IsNullOrEmpty(this.EventName))
        this.SceneInvoke(this.EventName);
      GlobalVars.PreEventName = this.CurrEventName;
    }

    private void SceneInvoke(string event_name)
    {
      GlobalVars.ForceSceneChange = this.SceneChange;
      GlobalEvent.Invoke(event_name, (object) this);
      this.ActivateOutputLinks(1);
    }
  }
}
