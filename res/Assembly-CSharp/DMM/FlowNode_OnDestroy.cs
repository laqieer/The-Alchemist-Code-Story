// Decompiled with JetBrains decompiler
// Type: FlowNode_OnDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("Event/OnDestroy", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnDestroy : FlowNodePersistent
{
  protected override void OnDestroy()
  {
    this.ActivateOutputLinks(1);
    base.OnDestroy();
  }
}
