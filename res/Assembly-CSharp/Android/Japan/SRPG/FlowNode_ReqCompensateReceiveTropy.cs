// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCompensateReceiveTropy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Trophy/CompensateReceiveTrophy", 32741)]
  [FlowNode.Pin(1, "Begin", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "End", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqCompensateReceiveTropy : FlowNode_Network
  {
    private const int PIN_INPUT_BEGIN_API = 1;
    private const int PIN_OUTPUT_END_API = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqCompensateReceiveTrophy(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqCompensateReceiveTrophy.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqCompensateReceiveTrophy.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(jsonObject.body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.OverwiteTrophyProgress(jsonObject.body.bingoprogs);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        Network.RemoveAPI();
        this.enabled = false;
        this.ActivateOutputLinks(101);
      }
    }
  }
}
