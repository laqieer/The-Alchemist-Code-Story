// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeactivateInventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
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
