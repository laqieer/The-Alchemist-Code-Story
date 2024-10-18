// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerDispatchFinishEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("AVProVideo/MediaPlayerDispatchFinishEvent")]
  public class FlowNode_MediaPlayerDispatchFinishEvent : FlowNode
  {
    public FlowNode_MediaPlayerDispatchFinishEvent.OnEnd onEnd;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.onEnd != null)
        this.onEnd();
      this.ActivateOutputLinks(10);
    }

    public delegate void OnEnd();
  }
}
