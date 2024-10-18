// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetPurchaseType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "Gems", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Bundles", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SetPurchaseType", 32741)]
  public class FlowNode_SetPurchaseType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Bundles;
          break;
        case 101:
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Gems;
          break;
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
