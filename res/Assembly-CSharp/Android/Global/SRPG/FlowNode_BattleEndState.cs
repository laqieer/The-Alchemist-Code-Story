// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleEndState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/EndState", 32741)]
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Accept", FlowNode.PinTypes.Input, 0)]
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
