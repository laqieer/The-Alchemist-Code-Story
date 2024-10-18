// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusEnableTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      this.enabled = false;
      this.ActivateOutputLinks(this.PIN_SUCCESS);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError && Network.ErrCode == Network.EErrCode.VS_EnableTimeOutOfPriod)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        Network.RemoveAPI();
        this.enabled = false;
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
