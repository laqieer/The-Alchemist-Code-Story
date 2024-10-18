// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetArenaReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/ResetArenaReward", 32741)]
  [FlowNode.Pin(0, "実行", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10000, "完了", FlowNode.PinTypes.Output, 10000)]
  public class FlowNode_ResetArenaReward : FlowNode
  {
    private const int PIN_IN_EXEC = 0;
    private const int PIN_OUT_EXEC = 10000;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        GlobalVars.ArenaAward = (Json_ArenaAward) null;
      this.ActivateOutputLinks(10000);
    }
  }
}
