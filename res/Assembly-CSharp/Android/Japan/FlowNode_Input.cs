// Decompiled with JetBrains decompiler
// Type: FlowNode_Input
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Event/Input", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_Input : FlowNode
{
  public string PinName;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.PinName;
  }

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
