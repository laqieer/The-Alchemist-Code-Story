// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRanking : WebAPI
  {
    public ReqGuildRanking(
      int gid,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guild/ranking";
      this.body = WebAPI.GetRequestString<ReqGuildRanking.Request>(new ReqGuildRanking.Request()
      {
        gid = gid
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class Request
    {
      public int gid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_CombatPowerRankingViewGuild self_rank;
      public JSON_CombatPowerRankingViewGuild[] ranking;
    }
  }
}
