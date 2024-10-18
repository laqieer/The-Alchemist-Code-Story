// Decompiled with JetBrains decompiler
// Type: FlowNode_WaitUrlSchemeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(0, "Finished", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("Event/WaitUrlSchemeObserver", 58751)]
[FlowNode.Pin(102, "Start", FlowNode.PinTypes.Input, 0)]
public class FlowNode_WaitUrlSchemeObserver : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 102)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver start");
    this.enabled = true;
  }

  private void Update()
  {
    if (FlowNode_OnUrlSchemeLaunch.IsExecuting)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver done");
    this.enabled = false;
    this.ActivateOutputLinks(0);
  }
}
