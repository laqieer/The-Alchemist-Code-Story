// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildFacilityEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildFacilityEnhance", 32741)]
  [FlowNode.Pin(1, "ギルド施設強化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド施設強化完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildFacilityEnhance : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_FACILITY_ENHANCE = 1;
    private const int PIN_OUTPUT_END_GUILD_FACILITY_ENHANCE = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      GuildEnhanceMaterial guildEnhanceMaterial = component.list.GetObject<GuildEnhanceMaterial>(GuildSVB_Key.ENHANCE_MATERIAL, (GuildEnhanceMaterial) null);
      if (guildEnhanceMaterial == null || string.IsNullOrEmpty(guildEnhanceMaterial.facility_iname))
        return;
      this.ExecRequest((WebAPI) new ReqGuildFacilityEnhance(guildEnhanceMaterial.facility_iname, guildEnhanceMaterial.materials.ToArray(), guildEnhanceMaterial.gold, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_InvestLimitOneDay:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_InvestCoolTime:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildFacilityEnhance.Json_ResGuildFacilityEnhance> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildFacilityEnhance.Json_ResGuildFacilityEnhance>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body != null)
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player_guild);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
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

    public class Json_ResGuildFacilityEnhance
    {
      public Json_PlayerData player;
      public JSON_Guild guild;
      public JSON_PlayerGuild player_guild;
      public JSON_GuildMember[] applied_member;
      public Json_Item[] items;
    }
  }
}
