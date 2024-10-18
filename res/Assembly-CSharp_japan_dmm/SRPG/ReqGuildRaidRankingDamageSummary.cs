// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidRankingDamageSummary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidRankingDamageSummary : WebAPI
  {
    public ReqGuildRaidRankingDamageSummary(
      int gid,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/ranking/damage/summary";
      this.body = WebAPI.GetRequestString<ReqGuildRaidRankingDamageSummary.RequestParam>(new ReqGuildRaidRankingDamageSummary.RequestParam()
      {
        gid = gid,
        page = page
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
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
