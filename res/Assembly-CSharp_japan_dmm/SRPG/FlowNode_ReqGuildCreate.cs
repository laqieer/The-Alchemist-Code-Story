// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildCreate", 32741)]
  [FlowNode.Pin(1, "ギルド設立", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド設立完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildCreate : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_CREATE = 1;
    private const int PIN_OUTPUT_END_GUILD_CREATE = 101;

    public override void OnActivate(int pinID)
    {
      GuildCreateMenu instance = GuildCreateMenu.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildCreate(instance.TempGuildData, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
          case Network.EErrCode.Guild_InputNgWord:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_PayerCoinShort:
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildCreate.Json_ResGuildCreate> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildCreate.Json_ResGuildCreate>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.guild);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player_guild);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.currencies);
          MonoSingleton<GameManager>.Instance.Player.UpdateJoinGuild();
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

    public class Json_ResGuildCreate
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public Json_Currencies currencies;
    }
  }
}
