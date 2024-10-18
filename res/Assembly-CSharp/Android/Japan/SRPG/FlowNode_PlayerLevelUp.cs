// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerLevelUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
