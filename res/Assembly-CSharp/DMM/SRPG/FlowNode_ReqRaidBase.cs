// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Begin", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "End", FlowNode.PinTypes.Output, 101)]
  public abstract class FlowNode_ReqRaidBase : FlowNode_Network
  {
    protected const int PIN_INPUT_BEGIN_API = 1;
    protected const int PIN_OUTPUT_END_API = 101;
    protected const int PIN_OUTPUT_TIME_OVER = 201;
    protected const int PIN_OUTPUT_ALREADY_BEAT = 202;
    protected const int PIN_OUTPUT_RESCUE_DAMAGE_ZERO = 203;
    protected const int PIN_OUTPUT_RESCUE_PLAYER_LEVEL_SHORT = 204;

    public abstract WebAPI GenerateWebAPI();

    public abstract bool Success(WWWResult www);

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      WebAPI webApi = this.GenerateWebAPI();
      if (webApi != null)
        this.ExecRequest(webApi);
      else
        this.OnSuccess(new WWWResult());
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Raid_OutOfPeriod:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, new UIUtility.DialogResultEvent(this.BackHome), systemModal: true);
            return;
          case Network.EErrCode.Raid_OutOfOenTime:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, new UIUtility.DialogResultEvent(this.BackRestore), systemModal: true);
            return;
          case Network.EErrCode.Raid_AlreadyBeat:
            if (!(this is FlowNode_ReqRaidInfo))
            {
              this.OnRetry();
              return;
            }
            break;
          case Network.EErrCode.Raid_CanNotRescuePlLvShort:
            if (!(this is FlowNode_ReqRaidRescue))
            {
              this.OnRetry();
              return;
            }
            break;
          default:
            this.OnRetry();
            return;
        }
      }
      if (this.Success(www))
        this.ActivateOutputLinks(101);
      Network.RemoveAPI();
      ((Behaviour) this).enabled = false;
    }

    private void BackHome(GameObject go)
    {
      CriticalSection.Leave();
      GlobalEvent.Invoke("RAIDBACK_HOME", (object) null);
    }

    private void BackRestore(GameObject go)
    {
      CriticalSection.Leave();
      GlobalEvent.Invoke("RAIDBACK_RESTORE", (object) null);
    }
  }
}
