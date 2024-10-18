// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidBossInfo
  {
    public int BossId { get; private set; }

    public int CurrentHP { get; private set; }

    public int MaxHP { get; private set; }

    public bool Deserialize(JSON_GuildRaidBossInfo json)
    {
      if (json == null)
        return false;
      this.BossId = json.boss_id;
      this.CurrentHP = json.current_hp;
      GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(this.BossId);
      if (guildRaidBossParam == null)
        return false;
      int round = 1;
      if (Object.op_Inequality((Object) GuildRaidManager.Instance, (Object) null))
        round = GuildRaidManager.Instance.CurrentRound;
      this.MaxHP = GuildRaidBossParam.CalcMaxHP(guildRaidBossParam, round);
      return true;
    }
  }
}
