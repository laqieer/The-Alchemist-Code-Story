// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

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
      this.enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
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
            if ((UnityEngine.Object) GuildManager.Instance != (UnityEngine.Object) null)
              GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
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

    public class Json_ResGuild
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public JSON_GuildMember[] applied_member;
    }
  }
}
