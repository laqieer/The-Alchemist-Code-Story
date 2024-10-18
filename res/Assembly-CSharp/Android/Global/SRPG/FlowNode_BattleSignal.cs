// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleSignal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "Resume", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Stop", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("Battle/Signal", 4513092)]
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
