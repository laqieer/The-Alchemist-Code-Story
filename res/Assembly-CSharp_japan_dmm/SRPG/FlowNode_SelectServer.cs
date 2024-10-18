// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }
  }
}
