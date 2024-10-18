// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetSleep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(101, "Off", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "On", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SetSleep", 32741)]
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
