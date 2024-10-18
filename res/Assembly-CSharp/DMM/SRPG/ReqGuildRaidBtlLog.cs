// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidBtlLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidBtlLog : WebAPI
  {
    public ReqGuildRaidBtlLog(
      int gid,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/btl/log";
      this.body = WebAPI.GetRequestString<ReqGuildRaidBtlLog.RequestParam>(new ReqGuildRaidBtlLog.RequestParam()
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
      public JSON_GuildRaidBattleLog[] battle_logs;
    }
  }
}
