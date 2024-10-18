// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqDecideTutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Gacha/ReqDecideTutorialGacha")]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqDecideTutorialGacha : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OU_SUCCESS = 10;
    private const int PIN_OU_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      this.enabled = true;
      this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(11);
    }
  }
}
