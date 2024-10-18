// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RaidPeriod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Raid/RaidPeriod", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "RAID開催中", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "RAID期間外", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_RaidPeriod : FlowNode
  {
    private const int INPUT_START = 10;
    private const int OUTPUT_RAIDOPEN = 1000;
    private const int OUTPUT_RAIDCLOSE = 1001;
    [HeaderBar("レイド開始時間を伸ばす(hour)")]
    [SerializeField]
    private int beginAddHour;
    [HeaderBar("レイド終了時間を伸ばす(hour)")]
    [SerializeField]
    private int endAddHour;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(this.beginAddHour, this.endAddHour) != null)
        this.ActivateOutputLinks(1000);
      else
        this.ActivateOutputLinks(1001);
    }
  }
}
