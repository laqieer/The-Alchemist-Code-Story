// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_RaidCheckNextArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Raid/CheckNextArea", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Has Next Area", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "Last Area", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 9)]
  public class Flownode_RaidCheckNextArea : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      RaidManager instance = RaidManager.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
      {
        this.ActivateOutputLinks(900);
      }
      else
      {
        RaidAreaParam raidArea = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidArea(instance.CurrentRaidAreaId);
        if (raidArea == null)
          this.ActivateOutputLinks(900);
        else if (raidArea.Order < MonoSingleton<GameManager>.Instance.MasterParam.GetRaidAreaCount(instance.RaidPeriodId))
          this.ActivateOutputLinks(101);
        else
          this.ActivateOutputLinks(102);
      }
    }
  }
}
