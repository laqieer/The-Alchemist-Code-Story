// Decompiled with JetBrains decompiler
// Type: FlowNode_DirectionCut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.ShowInInspector]
[FlowNode.NodeType("Event/DirectionCut", 32741)]
[FlowNode.Pin(0, "SkillDisplayOn", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "SkillDisplayOff", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "ZoomEffectOn", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(3, "ZoomEffectOff", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
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
      case 2:
        GameUtility.Config_ZoomEffect.Value = true;
        break;
      case 3:
        GameUtility.Config_ZoomEffect.Value = false;
        break;
    }
    this.ActivateOutputLinks(10);
  }
}
