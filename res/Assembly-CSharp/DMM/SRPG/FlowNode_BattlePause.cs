// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Battle/Pause", 32741)]
  [FlowNode.Pin(0, "一時停止", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "再開", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_BattlePause : FlowNode
  {
    private bool IsPauseAllowed => GameUtility.GetCurrentScene() != GameUtility.EScene.BATTLE_MULTI;

    public override void OnActivate(int pinID)
    {
      if (!this.IsPauseAllowed || Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
      {
        DebugUtility.Log("=== BattlePause => OutputLinks(100) ===");
        this.ActivateOutputLinks(100);
      }
      else if (pinID == 0)
      {
        SceneBattle.Instance.Pause(true);
        this.ActivateOutputLinks(100);
      }
      else
      {
        if (pinID != 1)
          return;
        SceneBattle.Instance.Pause(false);
        this.ActivateOutputLinks(100);
      }
    }
  }
}
