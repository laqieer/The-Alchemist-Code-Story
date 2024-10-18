// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerBackHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Event/TriggerBackHandler", 16087213)]
  [FlowNode.Pin(1, "Trigger", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Triggered", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TriggerBackHandler : FlowNode
  {
    public bool Force;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      BackHandler.Invoke();
      this.ActivateOutputLinks(2);
    }
  }
}
