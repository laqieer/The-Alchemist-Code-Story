// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
          if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            break;
          SceneBattle.Instance.PlayBGM();
          break;
        case 1:
          if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            break;
          SceneBattle.Instance.StopBGM();
          break;
      }
    }
  }
}
