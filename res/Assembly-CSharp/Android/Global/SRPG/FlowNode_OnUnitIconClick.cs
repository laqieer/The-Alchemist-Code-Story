// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Event/OnUnitIconClick", 58751)]
  [FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_OnUnitIconClick : FlowNode
  {
    public override void OnActivate(int pinID)
    {
    }

    public void Click()
    {
      this.ActivateOutputLinks(1);
    }
  }
}
