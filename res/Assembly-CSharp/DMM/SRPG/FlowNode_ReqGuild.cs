// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuild", 32741)]
  [FlowNode.Pin(1, "ギルド情報取得", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド情報取得完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuild : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD = 1;
    private const int PIN_OUTPUT_END_GUILD = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuild(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        Network.RemoveAPI();
      else if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuild.Json_ResGuild> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuild.Json_ResGuild>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body != null)
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player_guild);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
            {
              GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
              GuildManager.Instance.SetGuildRaidPeriod(jsonObject.body.appears_guildraid_period_id);
              GvGManager.GvGGroupId = jsonObject.body.gvg_group_id;
              GuildManager.Instance.SetGuildAttendStatus(jsonObject.body.attend_status);
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateJoinGuild();
            if (jsonObject.body.guild != null)
              MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate = jsonObject.body.guild.gvg_rate;
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

    public class Json_ResGuild
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public JSON_GuildMember[] applied_member;
      public int appears_guildraid_period_id;
      public int gvg_group_id;
      public int attend_status;
    }
  }
}
