// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_JudgeActionReleaseStartPart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/ストーリーパート解放演出を見せるか判断", 32741)]
  [FlowNode.Pin(0, "イン", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "解放演出を見せる", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "解放演出を見ない", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_JudgeActionReleaseStartPart : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart())
        this.ActivateOutputLinks(100);
      else
        this.ActivateOutputLinks(101);
    }
  }
}
