// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRequestEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildRequestEntry", 32741)]
  [FlowNode.Pin(1, "ギルド加入申請", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド加入申請完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ギルド加入申請_自動承認", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGuildRequestEntry : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY = 1;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY = 101;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_AND_JOIN = 102;

    public override void OnActivate(int pinID)
    {
      GuildConfirm instance = GuildConfirm.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildRequestEntry(instance.TargetGuild.UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_JoinedAlready:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_NotFound:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_MemberMax:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_ShortPlayerLv:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_ApplySendLevelShort:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_EntryCoolTime:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntry.Json_ResGuildRequestEntry> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntry.Json_ResGuildRequestEntry>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        int pinID = 101;
        try
        {
          if (jsonObject.body != null)
          {
            if (jsonObject.body.guild != null)
            {
              if (jsonObject.body.player_guild != null)
              {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.guild);
                MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player_guild);
                MonoSingleton<GameManager>.Instance.Player.UpdateJoinGuild();
                pinID = 102;
              }
            }
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(pinID);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ResGuildRequestEntry
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
    }
  }
}
