// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ButtonEventLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/ButtonEventLock", 13156327)]
  [FlowNode.Pin(1, "Lock", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "UnLock", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Reset", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "AllReset", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ButtonEventLock : FlowNode
  {
    public string LockKey;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          ButtonEvent.Lock(this.LockKey);
          break;
        case 2:
          ButtonEvent.UnLock(this.LockKey);
          break;
        case 3:
          ButtonEvent.ResetLock(this.LockKey);
          break;
        case 4:
          ButtonEvent.Reset();
          break;
      }
      this.ActivateOutputLinks(10);
    }
  }
}
