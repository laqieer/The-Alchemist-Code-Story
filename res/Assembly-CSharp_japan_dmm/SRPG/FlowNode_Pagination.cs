// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Pagination
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/Pagination", 32741)]
  [FlowNode.Pin(1, "Next", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Prev", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_Pagination : FlowNode
  {
    [SerializeField]
    [FlowNode.DropTarget(typeof (IPagination), false)]
    private Component target;

    public override void OnActivate(int pinID)
    {
      if (this.target is IPagination target)
      {
        if (pinID == 1)
          target.NextPage();
        else
          target.PrevPage();
      }
      else
        DebugUtility.LogWarning("FlowNode_Pagination Target is not have IPagination");
      this.ActivateOutputLinks(10);
    }
  }
}
