// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidReportSelf
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidReportSelf : WebAPI
  {
    public ReqGuildRaidReportSelf(
      int gid,
      int boss_id,
      GuildRaidBattleType battle_type,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/report/self";
      this.body = WebAPI.GetRequestString<ReqGuildRaidReportSelf.RequestParam>(new ReqGuildRaidReportSelf.RequestParam()
      {
        gid = gid,
        boss_id = boss_id,
        battle_type = (int) battle_type,
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
      public int battle_type;
      public int page;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidReport[] reports;
      public int totalPage;
    }
  }
}
