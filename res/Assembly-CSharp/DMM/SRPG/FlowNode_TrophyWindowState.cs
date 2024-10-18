// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TrophyWindowState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/TrophyWindowState", 32741)]
  [FlowNode.Pin(1, "Action", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Set", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Compare Match", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Compare Not Match", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Reset", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_TrophyWindowState : FlowNode
  {
    private const int PIN_IN_ACTION = 1;
    private const int PIN_OUT_SET = 10;
    private const int PIN_OUT_COMPARE_MATCH = 11;
    private const int PIN_OUT_COMPARE_NOT_MATCH = 12;
    private const int PIN_OUT_RESET = 13;
    [SerializeField]
    private FlowNode_TrophyWindowState.Type ActionType;
    [SerializeField]
    private FlowNode_TrophyWindowState.TrophyType SelectTrophyType;
    private static FlowNode_TrophyWindowState.TrophyType mTrophyType = FlowNode_TrophyWindowState.TrophyType.DAILY;

    public override void OnActivate(int pinID)
    {
      switch (this.ActionType)
      {
        case FlowNode_TrophyWindowState.Type.SET:
          FlowNode_TrophyWindowState.mTrophyType = this.SelectTrophyType;
          this.ActivateOutputLinks(10);
          break;
        case FlowNode_TrophyWindowState.Type.COMPARE:
          this.ActivateOutputLinks(FlowNode_TrophyWindowState.mTrophyType != this.SelectTrophyType ? 12 : 11);
          break;
        case FlowNode_TrophyWindowState.Type.RESET:
          FlowNode_TrophyWindowState.mTrophyType = FlowNode_TrophyWindowState.TrophyType.NONE;
          this.ActivateOutputLinks(13);
          break;
      }
    }

    private enum Type : byte
    {
      NONE,
      SET,
      COMPARE,
      RESET,
    }

    private enum TrophyType : byte
    {
      NONE,
      DAILY,
      RECORD,
      GUILD_DAILY,
    }
  }
}
