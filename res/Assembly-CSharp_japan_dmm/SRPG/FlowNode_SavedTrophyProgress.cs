// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SavedTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/SavedTrophyProgress", 32741)]
  [FlowNode.Pin(0, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "LoadTrophyProgress", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "UpdateByGameManager", FlowNode.PinTypes.Input, 2)]
  public class FlowNode_SavedTrophyProgress : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          MonoSingleton<GameManager>.Instance.LoadUpdateTrophyList();
          break;
        case 2:
          MonoSingleton<GameManager>.Instance.update_trophy_lock.LockClear();
          break;
      }
      this.ActivateOutputLinks(0);
    }
  }
}
