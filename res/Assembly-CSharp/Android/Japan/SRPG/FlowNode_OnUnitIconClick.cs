// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Event/OnUnitIconClick", 58751)]
  [FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_OnUnitIconClick : FlowNode
  {
    public void Click()
    {
      this.ActivateOutputLinks(1);
    }
  }
}
