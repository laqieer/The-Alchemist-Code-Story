// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingDamage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class GuildRaidRankingDamage
  {
    public int Role { get; private set; }

    public int Rank { get; private set; }

    public long Score { get; private set; }

    public string Name { get; private set; }

    public int Lv { get; private set; }

    public UnitParam Unit { get; private set; }

    public int UnitStrengthTotal { get; private set; }

    public int BossId { get; private set; }

    public int Round { get; private set; }

    public DateTime KnockDownAt { get; private set; }

    public bool Deserialize(JSON_GuildRaidRankingDamage json)
    {
      this.Role = json.role;
      this.Rank = json.rank;
      this.Score = json.score;
      this.Name = json.name;
      this.Lv = json.lv;
      if (json.unit != null)
      {
        this.Unit = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.unit);
        if (this.Unit == null)
          return false;
      }
      this.UnitStrengthTotal = json.unit_strength_total;
      this.BossId = json.boss_id;
      this.Round = json.round;
      if (json.knock_down_at > 0)
        this.KnockDownAt = TimeManager.FromUnixTime((long) json.knock_down_at);
      return true;
    }
  }
}
