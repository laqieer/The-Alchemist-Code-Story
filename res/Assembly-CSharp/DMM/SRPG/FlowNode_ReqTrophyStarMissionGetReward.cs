// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTrophyStarMissionGetReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/StarMission/Req/GetReward", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqTrophyStarMissionGetReward : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      PlayerData.TrophyStarMission.StarMission starMission = (PlayerData.TrophyStarMission.StarMission) null;
      int daily_ymd = 0;
      int get_index = -1;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.Player != null && instance.Player.TrophyStarMissionInfo != null)
      {
        if (TrophyStarMissionParam.SelectStarMissionType == TrophyStarMissionParam.eStarMissionType.DAILY)
        {
          starMission = instance.Player.TrophyStarMissionInfo.Daily;
          get_index = TrophyStarMissionParam.SelectDailyTreasureIndex;
        }
        else
        {
          starMission = instance.Player.TrophyStarMissionInfo.Weekly;
          get_index = TrophyStarMissionParam.SelectWeeklyTreasureIndex;
        }
        if (instance.Player.TrophyStarMissionInfo.Daily != null)
          daily_ymd = instance.Player.TrophyStarMissionInfo.Daily.YyMmDd;
      }
      if (starMission != null && daily_ymd != 0 && get_index >= 0)
        this.ExecRequest((WebAPI) new ReqTrophyStarMissionGetReward(starMission.TsmParam.Iname, get_index, starMission.YyMmDd, daily_ymd, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      else
        this.OnFailed();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqTrophyStarMissionGetReward.Response response = (ReqTrophyStarMissionGetReward.Response) null;
      bool flag1 = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag1)
      {
        FlowNode_ReqTrophyStarMissionGetReward.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqTrophyStarMissionGetReward.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.TrophyStarMission_AlreadyReceived:
          case SRPG.Network.EErrCode.TrophyStarMission_NotAchieved:
          case SRPG.Network.EErrCode.TrophyStarMission_Future:
          case SRPG.Network.EErrCode.TrophyStarMission_OutOfPeriod:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag1)
        {
          WebAPI.JSON_BodyResponse<ReqTrophyStarMissionGetReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTrophyStarMissionGetReward.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          response = jsonObject.body;
        }
        if (response == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.Player == null)
              throw new Exception("ReqTrophyStarMissionGetReward: GameManager Fatal error!");
            if (!TrophyStarMissionParam.EntryTrophyStarMission(response.star_mission))
              throw new Exception("ReqTrophyStarMissionGetReward: illegal StarMission information!");
            if (response.player != null)
              instance.Deserialize(response.player);
            if (response.items != null)
              instance.Deserialize(response.items);
            if (response.units != null)
              instance.Deserialize(response.units);
            if (response.cards != null && response.cards.Length != 0)
            {
              bool flag2 = false;
              for (int index = 0; index < response.cards.Length; ++index)
              {
                ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard card = response.cards[index];
                if (card == null)
                  throw new Exception("ReqTrophyStarMissionGetReward: illegal response cards! card is null");
                if (card.is_mail == 0)
                  flag2 = true;
                if (!string.IsNullOrEmpty(card.get_unit))
                  FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(card.iname) ?? throw new Exception(string.Format("ReqTrophyStarMissionGetReward: illegal response card iname! iname={0}", (object) card.iname)));
              }
              if (flag2)
                instance.Player.OnDirtyConceptCardData();
            }
            if (response.artifacts != null)
              instance.Deserialize(response.artifacts, true);
            instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
            instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqTrophyStarMissionGetReward.Response body;
    }
  }
}
