// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildMemberKick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildMemberKick", 32741)]
  [FlowNode.Pin(1, "ギルドメンバー追放", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルドメンバー追放完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildMemberKick : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_MEMBER_KICK = 1;
    private const int PIN_OUTPUT_END_GUILD_MEMBER_KICK = 101;

    public override void OnActivate(int pinID)
    {
      GuildConfirm instance = GuildConfirm.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildMemberKick(instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_RaidKickFaild:
          case Network.EErrCode.Guild_NotBanishGVGInPeriod:
            Network.RemoveAPI();
            Network.IsIndicator = true;
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildMemberKick.Json_ResGuildMemberKick> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildMemberKick.Json_ResGuildMemberKick>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.guild);
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

    public class Json_ResGuildMemberKick
    {
      public JSON_Guild guild;
    }
  }
}
