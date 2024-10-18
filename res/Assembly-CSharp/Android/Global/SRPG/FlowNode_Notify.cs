// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Notify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Notify", 32741)]
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
