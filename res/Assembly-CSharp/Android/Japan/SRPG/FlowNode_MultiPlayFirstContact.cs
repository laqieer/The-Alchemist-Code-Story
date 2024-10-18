// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayFirstContact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayFirstContact", 32741)]
  [FlowNode.Pin(0, "exist", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "yes", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(2, "no", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayFirstContact : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks((UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null || SceneBattle.Instance.FirstContact <= 0 ? 2 : 1);
    }
  }
}
