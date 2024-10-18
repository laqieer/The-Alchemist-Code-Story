// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidBtlReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidBtlReq : WebAPI
  {
    public ReqGuildRaidBtlReq(
      int gid,
      int round,
      int boss_id,
      GuildRaidBattleType battle_type,
      int unit_strength_total,
      Network.ResponseCallback response)
    {
      this.name = "guildraid/btl/req";
      this.body = WebAPI.GetRequestString<ReqGuildRaidBtlReq.RequestParam>(new ReqGuildRaidBtlReq.RequestParam()
      {
        gid = gid,
        round = round,
        boss_id = boss_id,
        battle_type = (int) battle_type,
        unit_strength_total = unit_strength_total
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int round;
      public int boss_id;
      public int battle_type;
      public int unit_strength_total;
    }
  }
}
