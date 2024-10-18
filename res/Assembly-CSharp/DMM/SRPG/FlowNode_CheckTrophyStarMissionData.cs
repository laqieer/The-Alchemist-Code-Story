// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTrophyStarMissionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/StarMission/Check/Data", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "Exist", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "NotExist", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_CheckTrophyStarMissionData : FlowNode
  {
    private const int PIN_IN_CHECK = 1;
    private const int PIN_OUT_EXIST = 111;
    private const int PIN_OUT_NOT_EXIST = 112;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Implicit((Object) instance) && instance.Player != null && instance.Player.TrophyStarMissionInfo != null)
        this.ActivateOutputLinks(111);
      else
        this.ActivateOutputLinks(112);
    }
  }
}
