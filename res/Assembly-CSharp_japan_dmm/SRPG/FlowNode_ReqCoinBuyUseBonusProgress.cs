// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCoinBuyUseBonusProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/CoinBuyUse/ReqCoinBuyUseProgress")]
  [FlowNode.Pin(100, "進捗取得開始", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1000, "進捗取得完了", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ReqCoinBuyUseBonusProgress : FlowNode_Network
  {
    private const int PIN_INPUT_START = 100;
    private const int PIN_OUTPUT_END = 1000;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetEnableCoinBuyUseBonusParams() == null)
      {
        this.ActivateOutputLinks(1000);
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqCoinBuyUseBonusProgress(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
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
        WebAPI.JSON_BodyResponse<ReqCoinBuyUseBonusProgress.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqCoinBuyUseBonusProgress.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body != null)
          {
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.bonus_stats);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) CoinBuyUseBonusIcon.Instance, (UnityEngine.Object) null))
              CoinBuyUseBonusIcon.Instance.RefreshBadge();
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(1000);
        ((Behaviour) this).enabled = false;
      }
    }
  }
}
