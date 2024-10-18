// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerDispatchFinishEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AVProVideo/MediaPlayerDispatchFinishEvent")]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
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
