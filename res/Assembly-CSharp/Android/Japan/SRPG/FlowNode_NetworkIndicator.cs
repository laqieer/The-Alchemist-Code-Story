﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.NodeType("Network/NetworkIndicator", 32741)]
  [FlowNode.Pin(0, "Destroy", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(5, "Create", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "OnDestroy", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(20, "OnCreate", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_NetworkIndicator : FlowNode
  {
    private FlowNode_NetworkIndicator.NetworkIndicator mRef = new FlowNode_NetworkIndicator.NetworkIndicator();

    public static bool NeedDisplay()
    {
      return FlowNode_NetworkIndicator.NetworkIndicator.NeedDisplay();
    }

    protected override void OnDestroy()
    {
      this.mRef.Disable();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        this.mRef.Disable();
        this.ActivateOutputLinks(10);
      }
      else
      {
        if (pinID != 5)
          return;
        this.mRef.Enable();
        this.ActivateOutputLinks(20);
      }
    }

    private class NetworkIndicator
    {
      private static List<FlowNode_NetworkIndicator.NetworkIndicator> mInstances = new List<FlowNode_NetworkIndicator.NetworkIndicator>();
      private bool mActive;

      public static bool NeedDisplay()
      {
        return FlowNode_NetworkIndicator.NetworkIndicator.mInstances.Count > 0;
      }

      ~NetworkIndicator()
      {
        this.Disable();
      }

      public void Enable()
      {
        if (this.mActive)
          return;
        this.mActive = true;
        FlowNode_NetworkIndicator.NetworkIndicator.mInstances.Add(this);
      }

      public void Disable()
      {
        if (!this.mActive)
          return;
        this.mActive = false;
        FlowNode_NetworkIndicator.NetworkIndicator.mInstances.Remove(this);
      }
    }
  }
}
