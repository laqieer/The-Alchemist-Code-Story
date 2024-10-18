// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetAutoRepeatQuestData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/Reset", 32741)]
  [FlowNode.Pin(0, "自動周回データリセット", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Finish", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ResetAutoRepeatQuestData : FlowNode
  {
    private const int PIN_IN_RESET = 0;
    private const int PIN_OT_RESET = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null)
        MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.Reset();
      this.ActivateOutputLinks(10);
    }
  }
}
