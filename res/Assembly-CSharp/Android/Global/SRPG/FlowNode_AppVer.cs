// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AppVer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/App Version", 32741)]
  [FlowNode.Pin(1, "Default", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_AppVer : FlowNode
  {
    [FlexibleArray]
    public string[] Versions = new string[0];

    public override FlowNode.Pin[] GetDynamicPins()
    {
      FlowNode.Pin[] pinArray = new FlowNode.Pin[this.Versions.Length];
      for (int index = 0; index < this.Versions.Length; ++index)
        pinArray[index] = new FlowNode.Pin(2 + index, this.Versions[index], FlowNode.PinTypes.Output, 2 + index);
      return pinArray;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      for (int index = 0; index < this.Versions.Length; ++index)
      {
        if (Application.version == this.Versions[index])
        {
          this.ActivateOutputLinks(2 + index);
          return;
        }
      }
      this.ActivateOutputLinks(1);
    }
  }
}
