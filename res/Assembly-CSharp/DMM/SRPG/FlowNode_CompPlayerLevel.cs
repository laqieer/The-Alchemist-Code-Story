// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CompPlayerLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/CompPlayerLevel", 32741)]
  [FlowNode.Pin(1, "Comp(指定したレベルに対して)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "Upper", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "Equal", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(113, "Lower", FlowNode.PinTypes.Output, 113)]
  public class FlowNode_CompPlayerLevel : FlowNode
  {
    [SerializeField]
    private int mCompPlayerLevel;
    private const int PIN_IN_COMP = 1;
    private const int PIN_OUT_UPPER = 111;
    private const int PIN_OUT_EQUAL = 112;
    private const int PIN_OUT_LOWER = 113;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      int lv = MonoSingleton<GameManager>.Instance.Player.Lv;
      if (lv > this.mCompPlayerLevel)
        this.ActivateOutputLinks(111);
      else if (lv < this.mCompPlayerLevel)
        this.ActivateOutputLinks(113);
      else
        this.ActivateOutputLinks(112);
    }
  }
}
