// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildLeave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildLeave", 32741)]
  [FlowNode.Pin(1, "ギルド脱退", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド脱退完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ギルド脱退失敗", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGuildLeave : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_LEAVE = 1;
    private const int PIN_OUTPUT_END_GUILD_LEAVE = 101;
    private const int PIN_OUTPUT_ERROR_GUILD_LEAVE = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.LeaveGuild();
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_RaidDissolutionFaild:
          case Network.EErrCode.Guild_NotDissoluteGVGInPeriod:
          case Network.EErrCode.Guild_RaidLeaveFaild:
          case Network.EErrCode.Guild_NotLeaveGVGInPeriod:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
            break;
          case Network.EErrCode.Guild_LeaveFailed:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, new UIUtility.DialogResultEvent(this.BackHome), systemModal: true);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildLeave.Json_ResGuildLeave> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildLeave.Json_ResGuildLeave>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player_guild);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(101);
        ((Behaviour) this).enabled = false;
        MonoSingleton<GameManager>.Instance.Player.HasGuildReward = false;
        MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin = false;
        MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate = 0;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance, (UnityEngine.Object) null))
          return;
        MonoSingleton<ChatWindow>.Instance.ForceUpdateChatLog(ChatWindow.eChatType.Guild);
      }
    }

    private void LeaveGuild()
    {
      GuildData guild = MonoSingleton<GameManager>.Instance.Player.Guild;
      PlayerGuildData playerGuild = MonoSingleton<GameManager>.Instance.Player.PlayerGuild;
      if (guild == null || playerGuild == null)
        return;
      if (guild.MemberCount == 1)
        this.ExecRequest((WebAPI) new ReqGuildDissolve(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      else if (playerGuild.IsGuildMaster)
      {
        GuildMemberData guildMemberData = (GuildMemberData) null;
        for (int index = 0; index < guild.Members.Length; ++index)
        {
          if (!(guild.Members[index].Uid == MonoSingleton<GameManager>.Instance.DeviceId) && (guildMemberData == null || guildMemberData.JoinedAt > guild.Members[index].JoinedAt))
            guildMemberData = guild.Members[index];
        }
        if (guildMemberData == null)
          return;
        this.ExecRequest((WebAPI) new ReqGuildLeave(guildMemberData.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        if (playerGuild.IsGuildMaster)
          return;
        this.ExecRequest((WebAPI) new ReqGuildLeave(string.Empty, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    private void BackHome(GameObject go) => this.ActivateOutputLinks(102);

    public class Json_ResGuildLeave
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
    }
  }
}
