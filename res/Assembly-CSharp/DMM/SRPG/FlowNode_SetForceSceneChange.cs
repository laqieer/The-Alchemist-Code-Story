// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetForceSceneChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Scene/SetForceSceneChange", 32741)]
  [FlowNode.Pin(0, "True", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "False", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_SetForceSceneChange : FlowNode
  {
    private const int PIN_IN_TRUE = 0;
    private const int PIN_IN_FALSE = 1;
    private const int PIN_OUT = 100;

    public override void OnActivate(int pinID)
    {
      GlobalVars.ForceSceneChange = pinID == 0;
      this.ActivateOutputLinks(100);
    }
  }
}
