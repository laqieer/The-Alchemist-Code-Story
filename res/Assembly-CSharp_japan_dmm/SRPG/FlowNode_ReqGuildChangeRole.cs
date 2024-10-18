// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildChangeRole
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildMasterChange", 32741)]
  [FlowNode.Pin(1, "ギルドメンバー役職変更", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルドメンバー役職変更完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildChangeRole : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_CHANGE_ROLE = 1;
    private const int PIN_OUTPUT_END_GUILD_CHANGE_ROLE = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.RequestChangeRole();
      ((Behaviour) this).enabled = true;
    }

    private void RequestChangeRole()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildCommand.Instance, (UnityEngine.Object) null))
        return;
      switch (GuildCommand.Instance.TargetRole)
      {
        case GuildMemberData.eRole.MASTAER:
          this.ExecRequest((WebAPI) new ReqGuildMasterChange(GuildCommand.Instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case GuildMemberData.eRole.MEMBER:
          this.ExecRequest((WebAPI) new ReqGuildGeneralChange(GuildCommand.Instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case GuildMemberData.eRole.SUB_MASTAER:
          this.ExecRequest((WebAPI) new ReqGuildSubMasterChange(GuildCommand.Instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Guild_SubMasterMax)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildChangeRole.Json_ResGuildChangeRole> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildChangeRole.Json_ResGuildChangeRole>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          GuildMemberData.eRole roleId = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId;
          if (jsonObject.body != null)
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player_guild);
          }
          if (roleId == GuildMemberData.eRole.MASTAER)
          {
            if (roleId != MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId)
              MonoSingleton<GameManager>.Instance.Player.HasGuildReward = false;
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(101);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ResGuildChangeRole
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public JSON_GuildMember[] applied_member;
    }
  }
}
