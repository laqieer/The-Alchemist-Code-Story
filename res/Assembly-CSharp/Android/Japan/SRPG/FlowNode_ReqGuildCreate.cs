// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildCreate(instance.TempGuildData, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
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

    public class Json_ResGuildCreate
    {
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public Json_Currencies currencies;
    }
  }
}
