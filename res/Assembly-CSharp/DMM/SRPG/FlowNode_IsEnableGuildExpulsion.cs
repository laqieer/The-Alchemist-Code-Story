// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsEnableGuildExpulsion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/IsEnableGuildExpulsion", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "ギルド除名可能", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "ギルド除名不可", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_IsEnableGuildExpulsion : FlowNode
  {
    private const int INPUT_START = 10;
    private const int OUTPUT_EXPULSION = 1000;
    private const int OUTPUT_NOTEXPULSION = 1001;
    [HeaderBar("レイド開始時間を伸ばす(hour)")]
    [SerializeField]
    private int beginAddHour;
    [HeaderBar("レイド終了時間を伸ばす(hour)")]
    [SerializeField]
    private int endAddHour;
    [HeaderBar("レイドのチェックを行う")]
    [SerializeField]
    private bool IsRaid = true;
    [HeaderBar("ギルドレイドのチェックを行う")]
    [SerializeField]
    private bool IsGuildRaid = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (this.IsRaid && MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(this.beginAddHour, this.endAddHour) != null)
        this.ActivateOutputLinks(1001);
      else if (this.IsGuildRaid && MonoSingleton<GameManager>.Instance.GetNowScheduleGuildRaidPeriodParam() != null)
        this.ActivateOutputLinks(1001);
      else
        this.ActivateOutputLinks(1000);
    }
  }
}
