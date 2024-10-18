// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Event/OnUnitIconClick", 58751)]
  [FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_OnUnitIconClick : FlowNode
  {
    public void Click() => this.ActivateOutputLinks(1);
  }
}
