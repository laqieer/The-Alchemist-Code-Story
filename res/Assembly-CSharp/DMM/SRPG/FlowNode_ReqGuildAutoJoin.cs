// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildAutoJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildAutoJoin", 32741)]
  [FlowNode.Pin(1, "自動ギルド加入", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "自動ギルド加入完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildAutoJoin : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_AUTO_JOIN = 1;
    private const int PIN_OUTPUT_END_GUILD_AUTO_JOIN = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildAutoJoin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Guild_AutoJoinTargetMissing)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildAutoJoin.Json_ReqGuildAutoJoin> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildAutoJoin.Json_ReqGuildAutoJoin>>(www.text);
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
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ReqGuildAutoJoin
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
    }
  }
}
