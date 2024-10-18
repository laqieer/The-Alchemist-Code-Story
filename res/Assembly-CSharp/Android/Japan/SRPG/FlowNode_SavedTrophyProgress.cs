// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SavedTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
