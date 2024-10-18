// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerDispatchFinishEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
