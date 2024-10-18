// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Platform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Platform/Switch", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "iOS", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Android", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "DMM", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "EDITOR", FlowNode.PinTypes.Output, 104)]
  public class FlowNode_Platform : FlowNode
  {
    private const int PIN_PLATFORM_IOS = 101;
    private const int PIN_PLATFORM_ANDROID = 102;
    private const int PIN_PLATFORM_DMM = 103;
    private const int PIN_PLATFORM_EDITOR = 104;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks(103);
    }
  }
}
