// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AppVer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Version/AppVersion", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Default", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AppVer : FlowNode
  {
    [FlexibleArray]
    public string[] Versions = new string[0];

    public override FlowNode.Pin[] GetDynamicPins()
    {
      FlowNode.Pin[] dynamicPins = new FlowNode.Pin[this.Versions.Length];
      for (int index = 0; index < this.Versions.Length; ++index)
        dynamicPins[index] = new FlowNode.Pin(2 + index, this.Versions[index], FlowNode.PinTypes.Output, 2 + index);
      return dynamicPins;
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
