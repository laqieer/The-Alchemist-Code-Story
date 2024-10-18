// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/PlayBGM", 32741)]
  [FlowNode.Pin(0, "再生", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "停止", FlowNode.PinTypes.Input, 1)]
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
