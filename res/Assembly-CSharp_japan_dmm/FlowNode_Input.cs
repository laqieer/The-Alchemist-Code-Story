// Decompiled with JetBrains decompiler
// Type: FlowNode_Input
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("Event/Input", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_Input : FlowNode
{
  public string PinName;

  public override string GetCaption() => base.GetCaption() + ":" + this.PinName;

  public override void OnActivate(int pinID) => this.ActivateOutputLinks(1);
}
