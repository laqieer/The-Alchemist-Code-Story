// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCompensateReceiveTropy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
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
      ((Behaviour) this).enabled = true;
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
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.bingoprogs);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        Network.RemoveAPI();
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(101);
      }
    }
  }
}
