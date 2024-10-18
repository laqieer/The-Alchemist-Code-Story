// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Location
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/Location", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_Location : FlowNode
  {
    private Location m_Location;

    private void Update()
    {
      if (this.m_Location != null)
      {
        this.m_Location.Update();
        if (this.m_Location.IsBusy())
          return;
        this.m_Location.Release();
        this.m_Location = (Location) null;
      }
      else
        ((Behaviour) this).enabled = false;
    }

    private void Start()
    {
      if (this.m_Location != null)
        return;
      this.m_Location = new Location();
      this.m_Location.Initialize();
      this.m_Location.Start(new Action<Location>(this.OnSuccess), new Action<Location>(this.OnFailed));
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!Location.isGPSEnable)
      {
        this.OnFailed((Location) null);
      }
      else
      {
        this.Start();
        ((Behaviour) this).enabled = true;
      }
    }

    private void OnSuccess(Location gps)
    {
      GlobalVars.Location = gps == null ? Vector2.zero : gps.location;
      this.ActivateOutputLinks(1);
    }

    private void OnFailed(Location gps)
    {
      GlobalVars.Location = Vector2.zero;
      this.ActivateOutputLinks(2);
    }
  }
}
