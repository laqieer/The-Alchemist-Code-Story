// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildAttend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildAttend : WebAPI
  {
    public ReqGuildAttend(
      ReqGuildAttend.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guild/attend";
      this.body = WebAPI.GetRequestString<ReqGuildAttend.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long gid;

      public RequestParam()
      {
      }

      public RequestParam(long guild_id) => this.gid = guild_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public int yesterday_attendance;
      public string[] attend_guild_member_uids;
      public int status;
      public JSON_GuildAttendReward[] rewards;
      public Json_PlayerData player;
      public Json_Item[] items;
      public JSON_ConceptCard[] cards;
      public Json_RuneData[] runes;
      public int rune_storage_used;
      public JSON_TrophyProgress[] guild_trophies;
    }
  }
}
