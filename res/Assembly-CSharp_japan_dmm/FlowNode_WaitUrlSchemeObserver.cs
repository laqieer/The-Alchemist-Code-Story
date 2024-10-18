// Decompiled with JetBrains decompiler
// Type: FlowNode_WaitUrlSchemeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Event/WaitUrlSchemeObserver", 58751)]
[FlowNode.Pin(102, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(0, "Finished", FlowNode.PinTypes.Output, 0)]
public class FlowNode_WaitUrlSchemeObserver : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 102)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver start");
    ((Behaviour) this).enabled = true;
  }

  private void Update()
  {
    if (FlowNode_OnUrlSchemeLaunch.IsExecuting)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver done");
    ((Behaviour) this).enabled = false;
    this.ActivateOutputLinks(0);
  }
}
