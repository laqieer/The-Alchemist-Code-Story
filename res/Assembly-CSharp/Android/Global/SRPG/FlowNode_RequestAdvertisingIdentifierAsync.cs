// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestAdvertisingIdentifierAsync
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Request Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/RequestAdvertisingIdentifierAsync", 32741)]
  [FlowNode.Pin(11, "Request Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(101, "Start", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_RequestAdvertisingIdentifierAsync : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (Application.RequestAdvertisingIdentifierAsync(new Application.AdvertisingIdentifierCallback(this.AdCallback)))
        return;
      this.ActivateOutputLinks(11);
    }

    public void AdCallback(string advertisingId, bool trackingEnabled, string error)
    {
      GameManager.SetDeviceID(advertisingId);
      this.ActivateOutputLinks(10);
    }
  }
}
