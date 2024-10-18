// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGLeague
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGLeague : WebAPI
  {
    public ReqGvGLeague(
      string league_id,
      int gid,
      int taret_rank,
      int limit,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/league";
      this.body = WebAPI.GetRequestString<ReqGvGLeague.RequestParam>(new ReqGvGLeague.RequestParam()
      {
        league_id = league_id,
        gid = gid,
        target_rank = taret_rank,
        limit = limit
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string league_id;
      public int gid;
      public int target_rank;
      public int limit;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGLeagueViewGuild[] guilds;
      public int total_count;
      public JSON_GvGLeagueData my_league;
    }
  }
}
