// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      this.enabled = true;
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
      this.enabled = false;
    }
  }
}
