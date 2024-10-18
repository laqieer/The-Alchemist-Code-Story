// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MilestoneTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Analytics/MilestoneTrigger", 32741)]
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Finished", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MilestoneTrigger : FlowNode
  {
    private static Dictionary<FlowNode_MilestoneTrigger.MilestoneType, string> _milestoneAndNetworkIDDict = new Dictionary<FlowNode_MilestoneTrigger.MilestoneType, string>();
    [SerializeField]
    private FlowNode_MilestoneTrigger.MilestoneType Milestone;

    public override void OnActivate(int pinID)
    {
      DebugUtility.LogWarning("Attempt Milestone:" + (object) this.Milestone);
      if (this.Milestone == FlowNode_MilestoneTrigger.MilestoneType.freegemsofferwall)
      {
        if (AnalyticsManager.HasOfferwallToShow())
          AnalyticsManager.AttemptToShowPlacement(this.Milestone.ToString(), new Action(this.OnPlacementShown), Network.SessionID);
      }
      else if (!FlowNode_MilestoneTrigger._milestoneAndNetworkIDDict.ContainsKey(this.Milestone) || FlowNode_MilestoneTrigger._milestoneAndNetworkIDDict[this.Milestone] != Network.SessionID)
      {
        AnalyticsManager.AttemptToShowPlacement(this.Milestone.ToString(), new Action(this.OnPlacementShown), Network.SessionID);
        FlowNode_MilestoneTrigger._milestoneAndNetworkIDDict[this.Milestone] = Network.SessionID;
      }
      this.ActivateOutputLinks(1);
    }

    private void OnPlacementShown()
    {
      this.ActivateOutputLinks(2);
    }

    [SerializeField]
    private enum MilestoneType
    {
      homescreen,
      title,
      freegemsofferwall,
    }
  }
}
