// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetGoToBeginnerQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/SetGoToBeginnerQuest", 32741)]
  [FlowNode.Pin(0, "SetTrue", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "SetFalse", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Result", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_SetGoToBeginnerQuest : FlowNode
  {
    public string Name;
    private const int IN_SET_TRUE = 0;
    private const int IN_SET_FALSE = 1;
    private const int OUT_RESULT = 100;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GlobalVars.RestoreBeginnerQuest = true;
          break;
        case 1:
          GlobalVars.RestoreBeginnerQuest = false;
          break;
      }
      this.ActivateOutputLinks(100);
    }
  }
}
