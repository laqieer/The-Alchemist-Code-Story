﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUpdateTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqUpdateTrophy", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqUpdateTrophy : FlowNode_Network
  {
    private TrophyParam mTrophyParam;
    private int mLevelOld;
    public GameObject RewardWindow;
    public string ReviewURL_Android;
    public string ReviewURL_iOS;
    public string ReviewURL_Generic;
    public string ReviewURL_Twitter;

    private void OnOverItemAmount()
    {
      UIUtility.ConfirmBox(LocalizedText.Get("sys.MAILBOX_ITEM_OVER_MSG"), (string) null, (UIUtility.DialogResultEvent) (go =>
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        TrophyParam trophy = instance.MasterParam.GetTrophy((string) GlobalVars.SelectedTrophy);
        TrophyState trophyCounter = instance.Player.GetTrophyCounter(trophy, false);
        List<TrophyState> trophyprogs = new List<TrophyState>() { trophyCounter };
        if (trophyCounter.Param.newapi)
          this.ExecRequest((WebAPI) new SGReqUpdateTrophy(trophyprogs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
        else
          this.ExecRequest((WebAPI) new ReqUpdateTrophy(trophyprogs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
        this.enabled = true;
      }), (UIUtility.DialogResultEvent) (go => this.enabled = false), (GameObject) null, false, -1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyParam trophy = instance.MasterParam.GetTrophy((string) GlobalVars.SelectedTrophy);
      this.mTrophyParam = trophy;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mLevelOld = player.Lv;
      GlobalVars.PlayerExpOld.Set(player.Exp);
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        instance.Player.DEBUG_ADD_COIN(trophy.Coin, 0, 0);
        instance.Player.DEBUG_ADD_GOLD(trophy.Gold);
        this.enabled = false;
        this.Success();
      }
      else
      {
        TrophyState trophyCounter = instance.Player.GetTrophyCounter(trophy, true);
        List<TrophyState> trophyprogs = new List<TrophyState>() { trophyCounter };
        if (trophyCounter.Param.newapi)
          this.ExecRequest((WebAPI) new SGReqUpdateTrophy(trophyprogs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
        else
          this.ExecRequest((WebAPI) new ReqUpdateTrophy(trophyprogs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
        this.enabled = true;
      }
    }

    private void Success()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        for (int index = 0; index < this.mTrophyParam.Items.Length; ++index)
          player.GainItem(this.mTrophyParam.Items[index].iname, this.mTrophyParam.Items[index].Num);
      }
      for (int index = 0; index < this.mTrophyParam.Items.Length; ++index)
        player.OnItemQuantityChange(this.mTrophyParam.Items[index].iname, this.mTrophyParam.Items[index].Num);
      player.OnCoinChange(this.mTrophyParam.Coin);
      player.OnGoldChange(this.mTrophyParam.Gold);
      if (player.Lv > this.mLevelOld)
        player.OnPlayerLevelChange(player.Lv - this.mLevelOld);
      GlobalVars.PlayerLevelChanged.Set(player.Lv != this.mLevelOld);
      GlobalVars.PlayerExpNew.Set(player.Exp);
      player.MarkTrophiesEnded(new TrophyParam[1]
      {
        this.mTrophyParam
      });
      if ((UnityEngine.Object) this.RewardWindow != (UnityEngine.Object) null)
      {
        RewardData data = new RewardData();
        data.Coin = this.mTrophyParam.Coin;
        data.Gold = this.mTrophyParam.Gold;
        data.Exp = this.mTrophyParam.Exp;
        if (data.Coin > 0)
          AnalyticsManager.TrackFreePremiumCurrencyObtain((long) data.Coin, "Milestone Reward");
        if (data.Gold > 0)
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) data.Gold, "Milestone Reward", (string) null);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < this.mTrophyParam.Items.Length; ++index)
        {
          ItemData itemData = new ItemData();
          if (itemData.Setup(0L, this.mTrophyParam.Items[index].iname, this.mTrophyParam.Items[index].Num))
          {
            data.Items.Add(itemData);
            ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemData.Param.iname);
            int num = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
            data.ItemsBeforeAmount.Add(num);
            AnalyticsManager.TrackNonPremiumCurrencyObtain(itemDataByItemId.Param.type != EItemType.Ticket ? AnalyticsManager.NonPremiumCurrencyType.Item : AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) itemDataByItemId.Num, "Milestone Reward", itemDataByItemId.ItemID);
          }
        }
        DataSource.Bind<RewardData>(this.RewardWindow, data);
      }
      GameCenterManager.SendAchievementProgress(this.mTrophyParam.iname);
      if (this.mTrophyParam != null && this.mTrophyParam.Objectives != null)
      {
        for (int index = 0; index < this.mTrophyParam.Objectives.Length; ++index)
        {
          if (this.mTrophyParam.Objectives[index].type == TrophyConditionTypes.followtwitter)
          {
            string reviewUrlTwitter = this.ReviewURL_Twitter;
            if (!string.IsNullOrEmpty(reviewUrlTwitter))
            {
              Application.OpenURL(reviewUrlTwitter);
              break;
            }
            break;
          }
        }
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        Network.RemoveAPI();
      else if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.TrophyRewarded:
          case Network.EErrCode.TrophyOutOfDate:
          case Network.EErrCode.TrophyRollBack:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.trophies))
              DebugUtility.LogError("Trophy Error : Discrepancy detected from server values! Following server values.");
            else
              DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
