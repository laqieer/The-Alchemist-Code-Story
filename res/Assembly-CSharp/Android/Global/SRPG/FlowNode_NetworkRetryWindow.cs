// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/NetworkRetryWindow", 32741)]
  [FlowNode.Pin(0, "Create", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_NetworkRetryWindow : FlowNode
  {
    public GameObject Window;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !((UnityEngine.Object) this.Window != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Instantiate<GameObject>(this.Window);
    }
  }
}
