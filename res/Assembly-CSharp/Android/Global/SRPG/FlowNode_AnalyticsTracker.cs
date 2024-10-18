// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AnalyticsTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Analytics/AnalyticsTracker", 32741)]
  public class FlowNode_AnalyticsTracker : FlowNode
  {
    public AnalyticsManager.TrackingType trackingType;

    public override void OnActivate(int pinID)
    {
      Debug.Log((object) ("FlownodeAnalyticsTracker => " + this.name + "-->" + (object) this.trackingType));
      AnalyticsManager.TrackTutorialEventGeneric(this.trackingType);
      this.ActivateOutputLinks(1);
    }
  }
}
