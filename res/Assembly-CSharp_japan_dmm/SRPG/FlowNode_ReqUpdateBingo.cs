// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUpdateBingo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqBingo/ReqUpdateBingo", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "開催期間外の報酬受け取り", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqUpdateBingo : FlowNode_Network
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
        List<TrophyState> trophyprogs = new List<TrophyState>()
        {
          instance.Player.TrophyData.GetTrophyCounter(trophy)
        };
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqUpdateBingo(trophyprogs, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true, this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }), (UIUtility.DialogResultEvent) (go => ((Behaviour) this).enabled = false));
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyParam trophy = instance.MasterParam.GetTrophy((string) GlobalVars.SelectedTrophy);
      this.mTrophyParam = trophy;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mLevelOld = player.Lv;
      GlobalVars.PlayerExpOld.Set(player.Exp);
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Offline)
      {
        instance.Player.DEBUG_ADD_COIN(trophy.Coin, 0, 0);
        instance.Player.DEBUG_ADD_GOLD(trophy.Gold);
        ((Behaviour) this).enabled = false;
        this.Success();
      }
      else
      {
        List<TrophyState> trophyprogs = new List<TrophyState>()
        {
          instance.Player.TrophyData.GetTrophyCounter(trophy)
        };
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqUpdateBingo(trophyprogs, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true, this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.Lv > this.mLevelOld)
        player.OnPlayerLevelChange(player.Lv - this.mLevelOld);
      GlobalVars.PlayerLevelChanged.Set(player.Lv != this.mLevelOld);
      GlobalVars.PlayerExpNew.Set(player.Exp);
      player.TrophyData.MarkTrophiesEnded(new TrophyParam[1]
      {
        this.mTrophyParam
      });
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardWindow, (UnityEngine.Object) null))
      {
        RewardData data = new RewardData();
        data.Coin = this.mTrophyParam.Coin;
        data.Gold = this.mTrophyParam.Gold;
        data.Exp = this.mTrophyParam.Exp;
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
          }
        }
        DataSource.Bind<RewardData>(this.RewardWindow, data);
      }
      GameCenterManager.SendAchievementProgress(this.mTrophyParam.iname);
      if (this.mTrophyParam != null && this.mTrophyParam.Objectives != null)
      {
        for (int index = 0; index < this.mTrophyParam.Objectives.Length; ++index)
        {
          if (this.mTrophyParam.Objectives[index].type == TrophyConditionTypes.review)
          {
            string reviewUrlGeneric = this.ReviewURL_Generic;
            if (!string.IsNullOrEmpty(reviewUrlGeneric))
            {
              Application.OpenURL(reviewUrlGeneric);
              break;
            }
            break;
          }
        }
      }
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
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      Json_PlayerDataAll body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          switch (SRPG.Network.ErrCode)
          {
            case SRPG.Network.EErrCode.TrophyRewarded:
            case SRPG.Network.EErrCode.TrophyOutOfDate:
            case SRPG.Network.EErrCode.TrophyRollBack:
              this.OnBack();
              return;
            case SRPG.Network.EErrCode.BingoOutofDateReceive:
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.CHALLENGEMISSION_ERROR_OUT_OF_DATE_RECEIVE"), new UIUtility.DialogResultEvent(this.OnErrorDialogClosed));
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              return;
            default:
              this.OnRetry();
              return;
          }
        }
        else
        {
          WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
          if (jsonObject == null || jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          body = jsonObject.body;
        }
      }
      else
      {
        FlowNode_ReqUpdateBingo.MP_UpdateBingoResponse updateBingoResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUpdateBingo.MP_UpdateBingoResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) updateBingoResponse.stat;
        string statMsg = updateBingoResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          switch (SRPG.Network.ErrCode)
          {
            case SRPG.Network.EErrCode.TrophyRewarded:
            case SRPG.Network.EErrCode.TrophyOutOfDate:
            case SRPG.Network.EErrCode.TrophyRollBack:
              this.OnBack();
              return;
            case SRPG.Network.EErrCode.BingoOutofDateReceive:
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.CHALLENGEMISSION_ERROR_OUT_OF_DATE_RECEIVE"), new UIUtility.DialogResultEvent(this.OnErrorDialogClosed));
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              return;
            default:
              this.OnRetry();
              return;
          }
        }
        else
          body = updateBingoResponse.body;
      }
      DebugUtility.Assert(body != null, "res == null");
      if (body == null)
      {
        this.OnRetry();
      }
      else
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(body.player);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }

    private void OnErrorDialogClosed(GameObject dialog) => this.ActivateOutputLinks(100);

    [MessagePackObject(true)]
    public class MP_UpdateBingoResponse : WebAPI.JSON_BaseResponse
    {
      public Json_PlayerDataAll body;
    }
  }
}
