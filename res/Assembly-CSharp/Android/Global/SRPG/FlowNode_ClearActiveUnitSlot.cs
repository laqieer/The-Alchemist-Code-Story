// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearActiveUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Clear Slot", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("SRPG/ClearActiveUnitSlot", 32741)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ClearActiveUnitSlot : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if ((UnityEngine.Object) PartyUnitSlot.Active != (UnityEngine.Object) null)
        DataSource.FindDataOfClass<PartyData>(PartyUnitSlot.Active.gameObject, (PartyData) null)?.SetUnitUniqueID(PartyUnitSlot.Active.Index, 0L);
      this.ActivateOutputLinks(100);
    }
  }
}
