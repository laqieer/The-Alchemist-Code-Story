// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkReachability
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      switch (Application.internetReachability)
      {
        case NetworkReachability.NotReachable:
          this.ActivateOutputLinks(4);
          break;
        case NetworkReachability.ReachableViaCarrierDataNetwork:
          this.ActivateOutputLinks(3);
          break;
        case NetworkReachability.ReachableViaLocalAreaNetwork:
          this.ActivateOutputLinks(2);
          break;
      }
    }
  }
}
