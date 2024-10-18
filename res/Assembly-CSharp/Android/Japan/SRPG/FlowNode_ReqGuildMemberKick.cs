// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildMemberKick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildMemberKick(instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Guild_RaidKickFaild)
        {
          Network.RemoveAPI();
          Network.IsIndicator = true;
          Network.ResetError();
          this.enabled = false;
          UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
        }
        else
          this.OnRetry();
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
        this.enabled = false;
      }
    }

    public class Json_ResGuildMemberKick
    {
      public JSON_Guild guild;
    }
  }
}
