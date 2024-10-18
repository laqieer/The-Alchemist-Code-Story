// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeactivateInventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/DeactivateInventorySlot", 32741)]
  [FlowNode.Pin(1, "Deactivate", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_DeactivateInventorySlot : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      InventorySlot.Active = (InventorySlot) null;
      MonoSingleton<GameManager>.Instance.Player.SaveInventory();
      this.ActivateOutputLinks(100);
    }
  }
}
