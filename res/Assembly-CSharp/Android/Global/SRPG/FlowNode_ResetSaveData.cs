// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("SRPG/セーブデータリセット", 32741)]
  public class FlowNode_ResetSaveData : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<GameManager>.Instance.Player.InitPlayerPrefs();
      MonoSingleton<GameManager>.Instance.Player.LoadPlayerPrefs();
      this.ActivateOutputLinks(1);
    }
  }
}
