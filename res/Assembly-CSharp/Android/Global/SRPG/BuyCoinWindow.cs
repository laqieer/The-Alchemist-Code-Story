// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1001, "Gems", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1000, "Bundles", FlowNode.PinTypes.Output, 1000)]
  public class BuyCoinWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_BUNDLES = 1000;
    public const int PIN_GEMS = 1001;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.SelectedPurchaseType == GlobalVars.PurchaseType.Bundles)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }
  }
}
