// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeactivateInventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
