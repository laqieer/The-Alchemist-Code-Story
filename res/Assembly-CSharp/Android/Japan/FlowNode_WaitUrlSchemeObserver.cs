// Decompiled with JetBrains decompiler
// Type: FlowNode_WaitUrlSchemeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
