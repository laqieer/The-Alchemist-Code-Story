// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckUnlockLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/CheckUnlockLevel", 32741)]
  [FlowNode.Pin(101, "Check", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "True", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "False", FlowNode.PinTypes.Output, 1002)]
  public class FlowNode_CheckUnlockLevel : FlowNode
  {
    private const int PIN_INPUT_START_CHECK = 101;
    private const int PIN_OUTPUT_TRUE = 1001;
    private const int PIN_OUTPUT_FALSE = 1002;
    [HideInInspector]
    public long Condition;

    private UnlockTargets condition
    {
      get => (UnlockTargets) this.Condition;
      set => this.Condition = (long) value;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Check();
    }

    private void Check()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, this.condition))
        this.ActivateOutputLinks(1002);
      else
        this.ActivateOutputLinks(1001);
    }
  }
}
