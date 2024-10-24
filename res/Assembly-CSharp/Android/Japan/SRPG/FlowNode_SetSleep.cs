﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetSleep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Common/SetSleep", 32741)]
  [FlowNode.Pin(100, "On", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Off", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SetSleep : FlowNode
  {
    private int On
    {
      get
      {
        return 100;
      }
    }

    private int Off
    {
      get
      {
        return 101;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == this.On)
        GameUtility.SetDefaultSleepSetting();
      else if (pinID == this.Off)
        GameUtility.SetNeverSleep();
      this.ActivateOutputLinks(1);
    }
  }
}
