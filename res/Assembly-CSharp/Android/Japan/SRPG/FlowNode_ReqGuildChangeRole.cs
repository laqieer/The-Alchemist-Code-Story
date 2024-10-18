// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildChangeRole
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

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
      this.enabled = true;
    }

    private void RequestChangeRole()
    {
      if ((UnityEngine.Object) GuildCommand.Instance == (UnityEngine.Object) null)
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
          if (jsonObject.body != null)
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player_guild);
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(101);
        this.enabled = false;
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
