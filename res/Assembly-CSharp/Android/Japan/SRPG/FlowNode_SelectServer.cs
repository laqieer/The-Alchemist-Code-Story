// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Network/SelectServer", 32741)]
  [FlowNode.Pin(0, "安定版", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "開発用", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SelectServer : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      string host = "http://localhost:5000/";
      if (pinID == 1)
        Network.SetDefaultHostConfigured(host);
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }
  }
}
