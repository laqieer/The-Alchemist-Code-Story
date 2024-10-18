// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCoinBuyUseBonusReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/CoinBuyUse/ReqCoinBuyUseReward")]
  [FlowNode.Pin(100, "報酬受取開始", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1000, "報酬受取完了", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "データ不足エラー", FlowNode.PinTypes.Output, 1010)]
  public class FlowNode_ReqCoinBuyUseBonusReward : FlowNode_Network
  {
    private const int PIN_INPUT_START = 100;
    private const int PIN_OUTPUT_END = 1000;
    private const int PIN_OUTPUT_NODATA = 1010;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!this.SendRequest())
        this.ActivateOutputLinks(1010);
      else
        ((Behaviour) this).enabled = true;
    }

    private bool SendRequest()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) CoinBuyUseBonusWindow.Instance, (UnityEngine.Object) null))
        return false;
      CoinBuyUseBonusParam targetBonusParam = CoinBuyUseBonusWindow.Instance.TargetBonusParam;
      CoinBuyUseBonusContentParam targetContentParam = CoinBuyUseBonusWindow.Instance.TargetContentParam;
      if (targetBonusParam == null || targetContentParam == null)
        return false;
      this.ExecRequest((WebAPI) new ReqCoinBuyUseBonusReward(targetBonusParam.Iname, targetContentParam.Num, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      return true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.CoinBuyUse_OutOfPeriod)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqCoinBuyUseBonusReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqCoinBuyUseBonusReward.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.bonus_rewards);
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
