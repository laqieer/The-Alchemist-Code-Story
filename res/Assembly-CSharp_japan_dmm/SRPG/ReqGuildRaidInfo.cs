// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidInfo : WebAPI
  {
    public ReqGuildRaidInfo(
      int gid,
      int boss_id,
      int round,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/info";
      this.body = WebAPI.GetRequestString<ReqGuildRaidInfo.RequestParam>(new ReqGuildRaidInfo.RequestParam()
      {
        gid = gid,
        round = round,
        boss_id = boss_id
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int round;
      public int boss_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidBossInfo boss_info;
      public JSON_GuildRaidChallengingPlayer[] players_challenging;
    }
  }
}
