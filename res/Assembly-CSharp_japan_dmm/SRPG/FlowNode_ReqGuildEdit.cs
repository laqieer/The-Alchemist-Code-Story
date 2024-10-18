// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildEdit", 32741)]
  [FlowNode.Pin(1, "ギルド編集", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ギルドデータ更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "ギルド編集完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildEdit : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_EDIT = 1;
    private const int PIN_INPUT_START_DEFAULTGUILD_EDIT = 2;
    private const int PIN_OUTPUT_END_GUILD_EDIT = 101;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          GuildCommand instance = GuildCommand.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            return;
          this.ExecRequest((WebAPI) new ReqGuildEdit(instance.TempGuild, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 2:
          if (MonoSingleton<GameManager>.Instance.Player == null || MonoSingleton<GameManager>.Instance.Player.Guild == null)
            return;
          this.ExecRequest((WebAPI) new ReqGuildEdit(MonoSingleton<GameManager>.Instance.Player.Guild, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_InputNgWord:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_PayerCoinShort:
            this.RollbackGuildCommand();
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildEdit.Json_ResGuildEdit> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildEdit.Json_ResGuildEdit>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.guild);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.currencies);
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

    private void RollbackGuildCommand()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildCommand.Instance, (UnityEngine.Object) null))
        return;
      GuildCommand.Instance.Back();
      GuildCommand.Instance.ResetTempGuild();
    }

    public class Json_ResGuildEdit
    {
      public JSON_Guild guild;
      public Json_Currencies currencies;
    }
  }
}
