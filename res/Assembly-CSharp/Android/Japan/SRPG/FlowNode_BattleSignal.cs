// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleSignal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/Signal", 4513092)]
  [FlowNode.Pin(0, "Resume", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Stop", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Out", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_BattleSignal : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
            SceneBattle.Instance.UISignal = false;
          this.ActivateOutputLinks(2);
          break;
        case 1:
          if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
            SceneBattle.Instance.UISignal = true;
          this.ActivateOutputLinks(2);
          break;
      }
    }
  }
}
