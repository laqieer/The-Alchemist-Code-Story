// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CompTrophyStarMissionType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/StarMission/Comp/Type", 32741)]
  [FlowNode.Pin(1, "Comp", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Equal", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Not Equal", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_CompTrophyStarMissionType : FlowNode
  {
    private const int PIN_IN_COMP = 1;
    private const int PIN_OUT_EQUAL = 101;
    private const int PIN_OUT_NOT_EQUAL = 102;
    [SerializeField]
    private TrophyStarMissionParam.eStarMissionType Type;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.Type == TrophyStarMissionParam.SelectStarMissionType)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
