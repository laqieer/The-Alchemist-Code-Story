// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayJingle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
[FlowNode.NodeType("Sound/PlayJingle", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlayJingle : FlowNode
{
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MonoSingleton<MySound>.Instance.PlayJingle(this.cueID);
    this.ActivateOutputLinks(1);
  }
}
