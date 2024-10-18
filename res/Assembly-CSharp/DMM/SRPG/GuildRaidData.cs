// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidData
  {
    public int PeriodId { get; private set; }

    public int Round { get; private set; }

    public GuildRaidBattlePoint BP { get; private set; }

    public GuildRaidBossInfo BossInfo { get; private set; }

    public int CanOpenAreaId { get; private set; }

    public int RefreshWaitSec { get; private set; }

    public bool Deserialize(JSON_GuildRaidData json)
    {
      if (json == null || json.current == null)
        return false;
      this.PeriodId = json.current.period_id;
      this.Round = json.current.round;
      if (json.bp == null)
        return false;
      this.BP.Deserialize(json.bp);
      if (json.bossinfo == null)
        return false;
      this.BossInfo = new GuildRaidBossInfo();
      if (!this.BossInfo.Deserialize(json.bossinfo))
        return false;
      this.CanOpenAreaId = json.can_open_area_id;
      this.RefreshWaitSec = json.refresh_wait_sec;
      return true;
    }
  }
}
