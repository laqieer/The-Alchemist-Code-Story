﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqOrdealPartyUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
        this.enabled = true;
        this.ExecRequest((WebAPI) new ReqOrdealPartyUpdate(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.OrdealParties));
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.RemoveAPI();
        Network.ResetError();
        this.enabled = false;
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
