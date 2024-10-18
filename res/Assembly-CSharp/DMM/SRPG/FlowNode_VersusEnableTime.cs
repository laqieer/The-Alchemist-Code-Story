// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusEnableTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("VS/CheckEnableTime", 32741)]
  [FlowNode.Pin(100, "Check", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "SUCCESS", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "FAILURE", FlowNode.PinTypes.Output, 300)]
  public class FlowNode_VersusEnableTime : FlowNode_Network
  {
    private readonly int PIN_CHECK = 100;
    private readonly int PIN_SUCCESS = 200;
    private readonly int PIN_FAILURE = 300;

    public override void OnActivate(int pinID)
    {
      if (pinID != this.PIN_CHECK)
        return;
      this.ExecRequest((WebAPI) new ReqVersusFreematchStatus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      Network.RemoveAPI();
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(this.PIN_SUCCESS);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError && Network.ErrCode == Network.EErrCode.VS_EnableTimeOutOfPriod)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        Network.RemoveAPI();
        ((Behaviour) this).enabled = false;
        if (instance.IsConnectedInRoom() && instance.GetRoomPlayerList().Count > 1)
        {
          Network.ResetError();
          this.ActivateOutputLinks(this.PIN_SUCCESS);
        }
        else
          this.ActivateOutputLinks(this.PIN_FAILURE);
      }
      else
      {
        GlobalVars.VersusFreeMatchTime = TimeManager.FromDateTime(TimeManager.ServerTime);
        this.Success();
      }
    }
  }
}
