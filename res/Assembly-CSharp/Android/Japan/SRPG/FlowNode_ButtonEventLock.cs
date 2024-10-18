// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ButtonEventLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
