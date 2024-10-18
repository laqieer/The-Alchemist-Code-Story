// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerLevelUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Battle/プレイヤーレベルアップ", 32741)]
  [FlowNode.Pin(0, "実行", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "レベルアップした", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "レベルアップしなかった", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_PlayerLevelUp : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      int num = PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpOld);
      if (PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpNew) == num)
        this.ActivateOutputLinks(2);
      else
        this.ActivateOutputLinks(1);
    }
  }
}
