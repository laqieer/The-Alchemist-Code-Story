// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetScrollPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("UI/SetScrollPosition")]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetScrollPosition : FlowNode
  {
    public ScrollRect ScrollRect;
    public Vector2 NormalizedPosition;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        this.ScrollRect.normalizedPosition = this.NormalizedPosition;
      this.ActivateOutputLinks(1);
    }
  }
}
