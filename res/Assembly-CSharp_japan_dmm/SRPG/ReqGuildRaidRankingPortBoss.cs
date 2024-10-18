// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidRankingPortBoss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidRankingPortBoss : WebAPI
  {
    public ReqGuildRaidRankingPortBoss(
      int gid,
      int page,
      int boss_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/ranking/port/boss";
      this.body = WebAPI.GetRequestString<ReqGuildRaidRankingPortBoss.RequestParam>(new ReqGuildRaidRankingPortBoss.RequestParam()
      {
        gid = gid,
        page = page,
        boss_id = boss_id
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int page;
      public int boss_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidRankingMember[] ranking_port_boss;
      public int totalPage;
    }
  }
}
