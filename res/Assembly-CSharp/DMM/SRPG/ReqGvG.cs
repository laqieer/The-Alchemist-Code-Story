// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvG : WebAPI
  {
    public ReqGvG(
      int gid,
      int gvg_group_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg";
      this.body = WebAPI.GetRequestString<ReqGvG.RequestParam>(new ReqGvG.RequestParam()
      {
        gid = gid,
        gvg_group_id = gvg_group_id
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int gvg_group_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGNodeData[] nodes;
      public int[] matching_order;
      public JSON_GvGLeagueViewGuild[] guilds;
      public JSON_GvGLeagueViewGuild my_guild;
      public JSON_GvGUsedUnitData[] used_units;
      public int declare_num;
      public int refresh_wait_sec;
      public int auto_refresh_wait_sec;
      public int declare_cool_time;
      public JSON_GvGResult result_daily;
      public JSON_GvGResult result;
      public JSON_GvGLeagueResult my_league;
      public int[] used_cards;
      public int[] used_artifacts;
      public int[] used_runes;
    }
  }
}
