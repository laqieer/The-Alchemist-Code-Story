// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildLeave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

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
      this.enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_RaidDissolutionFaild:
          case Network.EErrCode.Guild_RaidLeaveFaild:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            this.enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
            break;
          case Network.EErrCode.Guild_LeaveFailed:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            this.enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, new UIUtility.DialogResultEvent(this.BackHome), (GameObject) null, true, -1);
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
        this.enabled = false;
        if (!((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance != (UnityEngine.Object) null))
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

    private void BackHome(GameObject go)
    {
      this.ActivateOutputLinks(102);
    }

    public class Json_ResGuildLeave
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
    }
  }
}
