// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearActiveUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/ClearActiveUnitSlot", 32741)]
  [FlowNode.Pin(1, "Clear Slot", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ClearActiveUnitSlot : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Inequality((Object) PartyUnitSlot.Active, (Object) null))
        DataSource.FindDataOfClass<PartyData>(((Component) PartyUnitSlot.Active).gameObject, (PartyData) null)?.SetUnitUniqueID(PartyUnitSlot.Active.Index, 0L);
      this.ActivateOutputLinks(100);
    }
  }
}
