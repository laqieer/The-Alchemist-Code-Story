// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidReportData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidReportData
  {
    public int ReportId { get; private set; }

    public int BossId { get; private set; }

    public int Round { get; private set; }

    public int Damage { get; private set; }

    public DateTime PostedAt { get; private set; }

    public List<UnitData> Deck { get; private set; }

    public bool CanPost { get; private set; }

    public void Deserialize(JSON_GuildRaidReport json, bool canPost = false)
    {
      this.ReportId = json.report_id;
      this.BossId = json.boss_id;
      this.Round = json.round;
      this.Damage = json.damage;
      this.PostedAt = TimeManager.FromUnixTime((long) json.posted_at);
      this.Deck = new List<UnitData>();
      if (json.deck != null)
      {
        for (int index = 0; index < json.deck.units.Length; ++index)
        {
          UnitData unitData = (UnitData) null;
          if (json.deck.units[index] != null && !string.IsNullOrEmpty(json.deck.units[index].iname))
          {
            unitData = new UnitData();
            unitData.Deserialize(json.deck.units[index]);
          }
          this.Deck.Add(unitData);
        }
      }
      this.CanPost = canPost;
    }
  }
}
