// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/HomeApi", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(20, "Success", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "Failed", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_HomeApi : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        bool isGuildInvite = GuildManager.IsNeedGuildInviteRequest(TimeManager.Now());
        bool isReqCoinBonus = CoinBuyUseBonusWindow.IsDirtyBonusProgress();
        bool isAutoRepeatQuest = !MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsInitialized;
        bool isReqPartyOverWrite = !UnitOverWriteUtility.IsInitalized;
        bool isMultiPush = false;
        if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
          isMultiPush = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        MultiInvitationBadge.isValid = false;
        bool isGuildRoleReward = false;
        if ((bool) GlobalVars.IsHomeAPIAddGuildRoleBonus)
          isGuildRoleReward = MonoSingleton<GameManager>.Instance.Player.IsGuildAssign && (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster || MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster);
        GlobalVars.IsHomeAPIAddGuildRoleBonus.Set(false);
        bool isGuildGvGRate = false;
        if ((bool) GlobalVars.IsHomeAPI)
        {
          GlobalVars.IsHomeAPI.Set(false);
          isGuildGvGRate = true;
        }
        bool isAppearingWorldRaid = WorldRaidManager.IsWithinChallenge();
        this.ExecRequest((WebAPI) new HomeApi(isMultiPush, isGuildInvite, isReqCoinBonus, isAutoRepeatQuest, isReqPartyOverWrite, isGuildRoleReward, isGuildGvGRate, isAppearingWorldRaid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this._Success();
    }

    private void _Success()
    {
      this.ActivateOutputLinks(20);
      ((Behaviour) this).enabled = false;
    }

    private void _Failed()
    {
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(20);
      ((Behaviour) this).enabled = false;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this._Failed();
      }
      else
      {
        DebugMenu.Log("API", "homeapi:{" + www.text + "}");
        WebAPI.JSON_BodyResponse<FlowNode_HomeApi.JSON_HomeApiResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_HomeApi.JSON_HomeApiResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body != null && jsonObject.body.player != null)
        {
          MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = jsonObject.body.player.areamail_enabled != 0;
          MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = jsonObject.body.player.present_granted != 0;
          MultiInvitationReceiveWindow.SetBadge(jsonObject.body.player.multi_inv != 0);
          MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = jsonObject.body.player.charge_bonus;
          MonoSingleton<GameManager>.Instance.Player.HasArenaReward = jsonObject.body.player.colo_reward != 0;
          if (jsonObject.body.player.applied_member_num >= 0)
          {
            if (GuildManager.NotifyEntryRequestCount < jsonObject.body.player.applied_member_num)
              NotifyList.Push(string.Format(LocalizedText.Get("sys.NOTIFY_GUILD_ENTRY_REQUEST"), (object) jsonObject.body.player.applied_member_num));
            GuildManager.NotifyEntryRequestCount = jsonObject.body.player.applied_member_num;
          }
          DrawCardParam.DrawCardEnabled = jsonObject.body.player.drawcard_enabled != 0;
          MonoSingleton<GameManager>.Instance.Player.HasGvGReward = jsonObject.body.player.gvg_reward != 0;
          if (jsonObject.body.player.guild_role_reward_status >= 0)
            MonoSingleton<GameManager>.Instance.Player.HasGuildReward = jsonObject.body.player.guild_role_reward_status == 1;
          if (jsonObject.body.gvg_rate >= 0)
          {
            MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate = jsonObject.body.gvg_rate;
            MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin = jsonObject.body.gvg_league_disp == 1;
          }
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = false;
          MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = false;
          MultiInvitationReceiveWindow.SetBadge(false);
          MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = 0;
          MonoSingleton<GameManager>.Instance.Player.HasArenaReward = false;
          MonoSingleton<GameManager>.Instance.Player.HasGvGReward = false;
          MonoSingleton<GameManager>.Instance.Player.HasGuildReward = false;
          MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin = false;
          MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate = 0;
        }
        if (jsonObject.body != null && jsonObject.body.pubinfo != null)
          LoginNewsInfo.SetPubInfo(jsonObject.body.pubinfo);
        if (jsonObject.body != null && jsonObject.body.bonus_stats != null)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.bonus_stats);
        if (jsonObject.body != null && jsonObject.body.bonus_rewards != null)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.bonus_rewards);
        CoinBuyUseBonusWindow.SyncCoin();
        if (jsonObject.body != null && !MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsInitialized)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.auto_repeat);
        if (jsonObject.body != null && jsonObject.body.party_decks != null)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.party_decks);
        if (jsonObject.body != null && Object.op_Inequality((Object) HomeWindow.mInstance, (Object) null))
          HomeWindow.mInstance.SetWorldRaidLastBossStatus((HomeWindow.WorldRaidLastBossStatus) jsonObject.body.worldraid_last_boss_status);
        this._Success();
      }
    }

    public class JSON_HomeApiResponse
    {
      public FlowNode_HomeApi.JSON_HomeApiResponse.Player player;
      public LoginNewsInfo.JSON_PubInfo pubinfo;
      public JSON_PlayerCoinBuyUseBonusState[] bonus_stats;
      public JSON_PlayerCoinBuyUseBonusRewardState[] bonus_rewards;
      public Json_AutoRepeatQuestData auto_repeat;
      public JSON_PartyOverWrite[] party_decks;
      public int gvg_rate = -1;
      public int gvg_league_disp;
      public int worldraid_last_boss_status;

      public class Player
      {
        public int areamail_enabled;
        public int present_granted;
        public int multi_inv;
        public int charge_bonus;
        public int colo_reward;
        public int applied_member_num = -1;
        public int drawcard_enabled;
        public int gvg_reward;
        public int guild_role_reward_status = -1;
      }
    }
  }
}
