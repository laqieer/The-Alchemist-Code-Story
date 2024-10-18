// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleFocusUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Battle/FocusUnit", 32741)]
  [FlowNode.Pin(0, "フォーカス", FlowNode.PinTypes.Input, 0)]
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
