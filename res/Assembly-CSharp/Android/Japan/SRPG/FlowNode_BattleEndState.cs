// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleEndState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/EndState", 32741)]
  [FlowNode.Pin(0, "Accept", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_BattleEndState : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        SceneBattle.Instance.GotoNextState();
      }
      else
      {
        if (pinID != 1)
          return;
        SceneBattle.Instance.GotoPreviousState();
      }
    }
  }
}
