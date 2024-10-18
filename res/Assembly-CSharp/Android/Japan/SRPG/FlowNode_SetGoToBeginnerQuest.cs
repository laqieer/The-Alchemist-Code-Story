// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetGoToBeginnerQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
