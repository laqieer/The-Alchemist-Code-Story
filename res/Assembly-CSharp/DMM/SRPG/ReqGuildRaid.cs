// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaid : WebAPI
  {
    public ReqGuildRaid(
      int gid,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid";
      this.body = WebAPI.GetRequestString<ReqGuildRaid.RequestParam>(new ReqGuildRaid.RequestParam()
      {
        gid = gid
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildFacilityData[] facilities;
      public JSON_GuildRaidPrev prev;
      public JSON_GuildRaidCurrent current;
      public JSON_GuildRaidBattlePoint bp;
      public JSON_GuildRaidBossInfo boss_info;
      public int refresh_wait_sec;
      public int receive_mail_count;
      public string[] selected_units;
      public JSON_GuildRaidDeck forced_deck;
      public int ranking;
      public JSON_GuildRaidBattleLog battle_log;
    }
  }
}
