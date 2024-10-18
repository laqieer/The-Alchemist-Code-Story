// Decompiled with JetBrains decompiler
// Type: FlowNode_DirectionCut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Event/DirectionCut", 32741)]
[FlowNode.Pin(0, "On", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Off", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 2)]
[FlowNode.ShowInInspector]
public class FlowNode_DirectionCut : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 0:
        GameUtility.Config_DirectionCut.Value = true;
        break;
      case 1:
        GameUtility.Config_DirectionCut.Value = false;
        break;
    }
    this.ActivateOutputLinks(10);
  }
}
