// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidGuildStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidGuildStats : WebAPI
  {
    public ReqRaidGuildStats(int period_id, Network.ResponseCallback response)
    {
      this.name = "raidboss/guild/stats";
      this.body = WebAPI.GetRequestString<ReqRaidGuildStats.RequestParam>(new ReqRaidGuildStats.RequestParam()
      {
        period_id = period_id
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int period_id;
    }

    [Serializable]
    public class Response
    {
      public Json_RaidGuildInfo my_guild_info;
      public JSON_RaidGuildMember[] member;
    }
  }
}
