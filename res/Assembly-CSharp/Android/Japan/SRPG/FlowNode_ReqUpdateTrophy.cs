// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUpdateTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Trophy/ReqUpdateTrophy", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqUpdateTrophy : FlowNode_Network
  {
    private List<TrophyParam> mTrophyParam = new List<TrophyParam>();
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
        this.ExecRequest((WebAPI) new ReqUpdateTrophy(new List<TrophyState>()
        {
          instance.Player.GetTrophyCounter(trophy, false)
        }, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
        this.enabled = true;
      }), (UIUtility.DialogResultEvent) (go => this.enabled = false), (GameObject) null, false, -1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.enabled)
        return;
      this.mTrophyParam.Clear();
      bool flag = GlobalVars.SelectedTrophies != null;
      GameManager gm = MonoSingleton<GameManager>.Instance;
      if (flag)
      {
        foreach (string selectedTrophy in GlobalVars.SelectedTrophies)
          this.mTrophyParam.Add(gm.MasterParam.GetTrophy(selectedTrophy));
      }
      else
        this.mTrophyParam.Add(gm.MasterParam.GetTrophy((string) GlobalVars.SelectedTrophy));
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mLevelOld = player.Lv;
      GlobalVars.PlayerExpOld.Set(player.Exp);
      this.ExecRequest((WebAPI) new ReqUpdateTrophy(this.mTrophyParam.Select<TrophyParam, TrophyState>((Func<TrophyParam, TrophyState>) (param => gm.Player.GetTrophyCounter(param, true))).ToList<TrophyState>(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
      this.enabled = true;
    }

    private void Success()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      foreach (TrophyParam trophyParam in this.mTrophyParam)
      {
        for (int index = 0; index < trophyParam.Items.Length; ++index)
          player.OnItemQuantityChange(trophyParam.Items[index].iname, trophyParam.Items[index].Num);
        player.OnCoinChange(trophyParam.Coin);
        player.OnGoldChange(trophyParam.Gold);
      }
      if (player.Lv > this.mLevelOld)
        player.OnPlayerLevelChange(player.Lv - this.mLevelOld);
      GlobalVars.PlayerLevelChanged.Set(player.Lv != this.mLevelOld);
      GlobalVars.PlayerExpNew.Set(player.Exp);
      player.MarkTrophiesEnded(this.mTrophyParam.ToArray());
      if ((UnityEngine.Object) this.RewardWindow != (UnityEngine.Object) null)
      {
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
        DataSource.Bind<RewardData>(this.RewardWindow, data, false);
      }
      foreach (TrophyParam trophyParam in this.mTrophyParam)
        GameCenterManager.SendAchievementProgress(trophyParam.iname);
      foreach (TrophyParam trophyParam in this.mTrophyParam)
      {
        if (this.mTrophyParam != null && trophyParam.Objectives != null)
        {
          for (int index = 0; index < trophyParam.Objectives.Length; ++index)
          {
            if (trophyParam.Objectives[index].type == TrophyConditionTypes.followtwitter)
            {
              string reviewUrlTwitter = this.ReviewURL_Twitter;
              if (!string.IsNullOrEmpty(reviewUrlTwitter))
              {
                Application.OpenURL(reviewUrlTwitter);
                goto label_39;
              }
              else
                goto label_39;
            }
          }
        }
      }
label_39:
      GlobalVars.SelectedTrophies = (List<string>) null;
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        this.OnFailed();
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
            this.Deserialize(jsonObject.body.concept_cards);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }

    private void Deserialize(Json_TrophyConceptCards json)
    {
      if (json == null)
        return;
      if (json.mail != null && json.mail.Length > 0)
      {
        RewardData rewardData = GlobalVars.LastReward.Get();
        if (rewardData != null)
          rewardData.IsOverLimit = true;
        foreach (Json_TrophyConceptCard trophyConceptCard in json.mail)
        {
          if (!string.IsNullOrEmpty(trophyConceptCard.unit))
          {
            if (rewardData != null)
            {
              ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(trophyConceptCard.unit);
              rewardData.AddReward(itemParam, 1);
            }
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(trophyConceptCard.iname));
          }
        }
      }
      if (json.direct == null)
        return;
      RewardData rewardData1 = GlobalVars.LastReward.Get();
      foreach (Json_TrophyConceptCard trophyConceptCard in json.direct)
      {
        MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
        if (!string.IsNullOrEmpty(trophyConceptCard.unit))
        {
          if (rewardData1 != null)
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(trophyConceptCard.unit);
            rewardData1.AddReward(itemParam, 1);
          }
          FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(trophyConceptCard.iname));
        }
      }
    }
  }
}
