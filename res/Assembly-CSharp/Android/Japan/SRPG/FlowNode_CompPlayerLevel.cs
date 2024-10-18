// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CompPlayerLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
