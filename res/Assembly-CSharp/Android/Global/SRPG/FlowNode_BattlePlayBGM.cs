// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "停止", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("Battle/PlayBGM", 32741)]
  [FlowNode.Pin(0, "再生", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BattlePlayBGM : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
            break;
          SceneBattle.Instance.PlayBGM();
          break;
        case 1:
          if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
            break;
          SceneBattle.Instance.StopBGM();
          break;
      }
    }
  }
}
