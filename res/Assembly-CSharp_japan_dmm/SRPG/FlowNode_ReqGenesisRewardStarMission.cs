// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGenesisRewardStarMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/Req/RewardStarMission", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "OutOfPeriod", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGenesisRewardStarMission : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_OUT_OF_PERIOD = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqGenesisRewardStarMission(GenesisChapterManager.Instance.CurrentChapterParam.AreaId, GenesisChapterManager.Instance.StageDifficulty, GenesisChapterManager.Instance.SelectedStarRewardIndex, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Genesis_OutOfPeriod)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.ActivateOutputLinks(102);
          ((Behaviour) this).enabled = false;
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqGenesisRewardStarMission.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGenesisRewardStarMission.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (jsonObject.body.genesis_stars != null)
              GenesisManager.SetStarMissionInfo(jsonObject.body.genesis_stars);
            if (jsonObject.body.reward != null && UnityEngine.Object.op_Implicit((UnityEngine.Object) GenesisChapterManager.Instance))
              GenesisChapterManager.Instance.SetStarMissionReward(jsonObject.body.reward);
            if (jsonObject.body.player != null)
              instance.Deserialize(jsonObject.body.player);
            if (jsonObject.body.items != null)
              instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.units != null)
              instance.Deserialize(jsonObject.body.units);
            if (jsonObject.body.cards != null)
              instance.Player.OnDirtyConceptCardData();
            if (jsonObject.body.artifacts != null)
              instance.Deserialize(jsonObject.body.artifacts, true);
            instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.trophyprogs);
            instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.bingoprogs);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
