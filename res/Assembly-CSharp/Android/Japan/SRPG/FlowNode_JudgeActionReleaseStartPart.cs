﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_JudgeActionReleaseStartPart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
