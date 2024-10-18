// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsRuneBoxFull
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Rune/IsRuneBoxFull", 32741)]
  [FlowNode.Pin(10, "ルーンのBoxが満タンか？", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "true", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "false", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_IsRuneBoxFull : FlowNode
  {
    private const int PIN_INPUT_CHECK = 10;
    private const int PIN_OUTPUT_TRUE = 100;
    private const int PIN_OUTPUT_FALSE = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.IsRuneStorageFull)
        UIUtility.SystemMessage(string.Empty, LocalizedText.Get("sys.RUNE_STORAGE_OVER_TEXT"), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(100)));
      else
        this.ActivateOutputLinks(110);
    }
  }
}
