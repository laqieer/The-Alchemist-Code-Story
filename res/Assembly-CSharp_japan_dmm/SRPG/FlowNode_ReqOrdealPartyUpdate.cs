// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqOrdealPartyUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Party/ReqOrdealPartyUpdate", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1000, "Failed", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqOrdealPartyUpdate : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.ExecRequest((WebAPI) new ReqOrdealPartyUpdate(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.OrdealParties));
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(1000);
      }
      else
      {
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
