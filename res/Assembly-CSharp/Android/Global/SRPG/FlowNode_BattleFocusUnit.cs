// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleFocusUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "フォーカス", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Battle/FocusUnit", 32741)]
  public class FlowNode_BattleFocusUnit : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      SceneBattle.Instance.ResetMoveCamera();
    }
  }
}
