// Decompiled with JetBrains decompiler
// Type: SRPG.HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class HomeApi : WebAPI
  {
    public HomeApi(
      bool isMultiPush,
      bool isGuildInvite,
      bool isReqCoinBonus,
      bool isAutoRepeatQuest,
      bool isReqPartyOverWrite,
      bool isGuildRoleReward,
      bool isGuildGvGRate,
      bool isAppearingWorldRaid,
      Network.ResponseCallback response)
    {
      this.name = "home";
      if (!isMultiPush && !isGuildInvite && !isReqCoinBonus)
      {
        this.body = WebAPI.GetRequestString((string) null);
      }
      else
      {
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        if (isMultiPush)
        {
          stringBuilder.Append("\"is_multi_push\":1");
          stringBuilder.Append(",");
        }
        if (isGuildInvite)
        {
          stringBuilder.Append("\"is_chk_guild_applied\":1");
          stringBuilder.Append(",");
        }
        if (isReqCoinBonus)
        {
          stringBuilder.Append("\"is_chk_coin_bonus\":1");
          stringBuilder.Append(",");
        }
        if (isAutoRepeatQuest)
        {
          stringBuilder.Append("\"is_auto_repeat\":1");
          stringBuilder.Append(",");
        }
        if (isReqPartyOverWrite)
        {
          stringBuilder.Append("\"is_party_decks\":1");
          stringBuilder.Append(",");
        }
        if (isGuildRoleReward)
        {
          stringBuilder.Append("\"is_guild_role_reward\":1");
          stringBuilder.Append(",");
        }
        if (isGuildGvGRate)
        {
          stringBuilder.Append("\"is_gvg_rate\":1");
          stringBuilder.Append(",");
        }
        if (isAppearingWorldRaid)
        {
          stringBuilder.Append("\"is_worldraid\":true");
          stringBuilder.Append(",");
        }
        if (stringBuilder[stringBuilder.Length - 1] == ',')
          --stringBuilder.Length;
        this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      }
      this.callback = response;
    }
  }
}
