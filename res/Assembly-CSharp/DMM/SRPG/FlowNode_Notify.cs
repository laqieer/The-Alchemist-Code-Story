// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Notify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Notify/Notify", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_Notify : FlowNode
  {
    public GameObject NotifyListTemplate;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        MonoSingleton<GameManager>.Instance.InitNotifyList(this.NotifyListTemplate);
      this.ActivateOutputLinks(10);
    }
  }
}
