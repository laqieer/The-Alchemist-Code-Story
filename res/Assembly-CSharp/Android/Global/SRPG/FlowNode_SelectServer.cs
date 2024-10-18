// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "安定版", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "開発用", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/SelectServer", 32741)]
  public class FlowNode_SelectServer : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      string host = "http://dev01-app.alcww.gumi.sg/";
      if (pinID == 1)
        Network.SetDefaultHostConfigured(host);
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }
  }
}
