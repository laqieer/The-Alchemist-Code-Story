// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidGuildStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
