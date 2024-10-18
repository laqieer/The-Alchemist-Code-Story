// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidRankingDamageRound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidRankingDamageRound : WebAPI
  {
    public ReqGuildRaidRankingDamageRound(
      int gid,
      int boss_id,
      int round,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/ranking/damage/round";
      this.body = WebAPI.GetRequestString<ReqGuildRaidRankingDamageRound.RequestParam>(new ReqGuildRaidRankingDamageRound.RequestParam()
      {
        gid = gid,
        boss_id = boss_id,
        round = round,
        page = page
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int boss_id;
      public int round;
      public int page;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidRankingDamage[] ranking_damage;
      public int totalPage;
    }
  }
}
