// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RemoveSuspendData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/RemoveSuspendData", 16729156)]
  [FlowNode.Pin(0, "Remove", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_RemoveSuspendData : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      BattleCore.RemoveSuspendData();
      this.ActivateOutputLinks(1);
    }
  }
}
