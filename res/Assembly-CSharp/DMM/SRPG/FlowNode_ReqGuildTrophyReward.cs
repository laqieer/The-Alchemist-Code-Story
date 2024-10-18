// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildTrophyReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildTrophy/Req/ReqGuildTrophyReward", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(150, "ポート未所属", FlowNode.PinTypes.Output, 150)]
  [FlowNode.Pin(151, "未達成のミッション", FlowNode.PinTypes.Output, 151)]
  [FlowNode.Pin(152, "前日より前の報酬は受け取れない", FlowNode.PinTypes.Output, 152)]
  [FlowNode.Pin(153, "受け取り済", FlowNode.PinTypes.Output, 153)]
  public class FlowNode_ReqGuildTrophyReward : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_JOINED = 150;
    protected const int PIN_OUT_NOT_ACHIEVE_TROPHY = 151;
    protected const int PIN_OUT_PAST_TROPHY = 152;
    protected const int PIN_OUT_RECEIVED_TROPHY_REWARD = 153;
    private List<TrophyParam> mTrophyParam = new List<TrophyParam>();
    public GameObject RewardWindow;
    private int mLevelOld;
    private int mUnitCountsOld;

    public ReqGuildTrophyReward.RequestParam CreateReqGetGuildTrophyReward()
    {
      this.mTrophyParam.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (GlobalVars.SelectedTrophies != null)
      {
        foreach (string selectedTrophy in GlobalVars.SelectedTrophies)
          this.mTrophyParam.Add(instance.MasterParam.GetGuildTrophy(selectedTrophy));
      }
      else
        this.mTrophyParam.Add(instance.MasterParam.GetGuildTrophy((string) GlobalVars.SelectedTrophy));
      List<ReqGuildTrophyReward.RequestTrophy> requestTrophyList = new List<ReqGuildTrophyReward.RequestTrophy>();
      foreach (TrophyParam trophyParam in this.mTrophyParam)
      {
        if (trophyParam != null)
        {
          ReqGuildTrophyReward.RequestTrophy requestTrophy = new ReqGuildTrophyReward.RequestTrophy(trophyParam.iname, TimeManager.ServerTime.ToYMD());
          requestTrophyList.Add(requestTrophy);
        }
      }
      return new ReqGuildTrophyReward.RequestParam(requestTrophyList.ToArray());
    }

    public override void OnActivate(int pinID)
    {
      ReqGuildTrophyReward.RequestParam guildTrophyReward = this.CreateReqGetGuildTrophyReward();
      if (guildTrophyReward == null)
        return;
      this.TrophySurveillanceStart();
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqGuildTrophyReward(guildTrophyReward, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildTrophyReward.Response response = (ReqGuildTrophyReward.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildTrophyReward.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildTrophyReward.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.Guild_NotJoined:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(150)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.Guild_NotAchieveTrophy:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(151)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.Guild_PastTrophy:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(152)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.Guild_ReceivedTrophyReward:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(153)), systemModal: true);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGuildTrophyReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildTrophyReward.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(response.player);
            MonoSingleton<GameManager>.Instance.Deserialize(response.items);
            MonoSingleton<GameManager>.Instance.Deserialize(response.units);
            MonoSingleton<GameManager>.Instance.Deserialize(response.artifacts, true);
            FlowNode_ReqUpdateTrophy.Deserialize(response.cards);
            this.TrophySurveillanceEnd();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedTrophies = (List<string>) null;
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    private void TrophySurveillanceStart()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mLevelOld = player.Lv;
      GlobalVars.PlayerExpOld.Set(player.Exp);
      this.mUnitCountsOld = player.Units.Count;
    }

    private void TrophySurveillanceEnd()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      foreach (TrophyParam trophyParam in this.mTrophyParam)
      {
        for (int index = 0; index < trophyParam.Items.Length; ++index)
          player.OnItemQuantityChange(trophyParam.Items[index].iname, trophyParam.Items[index].Num);
        player.OnCoinChange(trophyParam.Coin);
        player.OnGoldChange(trophyParam.Gold);
        GameCenterManager.SendAchievementProgress(trophyParam.iname);
      }
      if (player.Lv > this.mLevelOld)
      {
        player.OnPlayerLevelChange(player.Lv - this.mLevelOld);
        GlobalVars.PlayerLevelChanged.Set(true);
      }
      GlobalVars.PlayerExpNew.Set(player.Exp);
      if (player.Units.Count > this.mUnitCountsOld)
        player.OnUnitGet();
      player.GuildTrophyData.MarkTrophiesEnded(this.mTrophyParam.ToArray());
      player.GuildTrophyData.UpdateHasRewards();
    }

    private void BindRewardWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RewardWindow, (UnityEngine.Object) null))
        return;
      RewardData data = new RewardData();
      foreach (TrophyParam trophyParam in this.mTrophyParam)
      {
        TrophyParam param = trophyParam;
        data.Coin += param.Coin;
        data.Gold += param.Gold;
        data.Exp += param.Exp;
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int i = 0; i < param.Items.Length; ++i)
        {
          ItemData itemData1 = data.Items.FirstOrDefault<ItemData>((Func<ItemData, bool>) (item => item.ItemID == param.Items[i].iname));
          if (itemData1 == null)
          {
            ItemData itemData2 = new ItemData();
            if (itemData2.Setup(0L, param.Items[i].iname, param.Items[i].Num))
            {
              data.Items.Add(itemData2);
              ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemData2.Param.iname);
              int num = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
              data.ItemsBeforeAmount.Add(num);
            }
          }
          else
            itemData1.SetNum(itemData1.Num + param.Items[i].Num);
        }
      }
      DataSource.Bind<RewardData>(this.RewardWindow, data);
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGuildTrophyReward.Response body;
    }
  }
}
