// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidRankingPort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidRankingPort : WebAPI
  {
    public ReqGuildRaidRankingPort(
      int gid,
      int page,
      GuildRaidManager.GuildRaidRankingType type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      if (type == GuildRaidManager.GuildRaidRankingType.Current)
        this.name = "guildraid/ranking/port";
      else
        this.name = "guildraid/ranking/port/history";
      this.body = WebAPI.GetRequestString<ReqGuildRaidRankingPort.RequestParam>(new ReqGuildRaidRankingPort.RequestParam()
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
      public JSON_GuildRaidRankingMember[] ranking_port;
      public JSON_GuildRaidRankingMember my_info;
      public int totalPage;
    }
  }
}
