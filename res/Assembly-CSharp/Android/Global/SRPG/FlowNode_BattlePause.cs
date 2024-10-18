// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "一時停止", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "再開", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("Battle/Pause", 32741)]
  public class FlowNode_BattlePause : FlowNode
  {
    private bool IsPauseAllowed
    {
      get
      {
        return GameUtility.GetCurrentScene() != GameUtility.EScene.BATTLE_MULTI;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (!this.IsPauseAllowed || (UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null)
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
