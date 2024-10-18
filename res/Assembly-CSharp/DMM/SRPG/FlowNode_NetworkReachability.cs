// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkReachability
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/NetworkReachability", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Wifi", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(3, "Carrier", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(4, "Not Connected", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_NetworkReachability : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_WIFI = 2;
    private const int PIN_ID_CARRIER = 3;
    private const int PIN_ID_NOT_CONNECTED = 4;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      NetworkReachability internetReachability = Application.internetReachability;
      if (internetReachability != 2)
      {
        if (internetReachability != 1)
        {
          if (internetReachability != null)
            return;
          this.ActivateOutputLinks(4);
        }
        else
          this.ActivateOutputLinks(3);
      }
      else
        this.ActivateOutputLinks(2);
    }
  }
}
