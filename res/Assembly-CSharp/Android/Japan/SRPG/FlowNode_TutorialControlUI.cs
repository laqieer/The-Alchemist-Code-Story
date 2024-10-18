// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialControlUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Tutorial/ControlUI", 32741)]
  [FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TutorialControlUI : FlowNode
  {
    private const int PIN_IN_ENABLE = 1;
    private const int PIN_IN_DISABLE = 2;
    private const int PIN_OUT_OUTPUT = 10;
    [SerializeField]
    [BitMask]
    private SceneBattle.eMaskBattleUI ControlType;

    public override void OnActivate(int pinID)
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((bool) ((UnityEngine.Object) instance))
      {
        switch (pinID)
        {
          case 1:
            instance.EnableControlBattleUI(this.ControlType, true);
            break;
          case 2:
            instance.EnableControlBattleUI(this.ControlType, false);
            break;
        }
      }
      this.ActivateOutputLinks(10);
    }
  }
}
