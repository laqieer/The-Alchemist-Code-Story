// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidLogData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class WorldRaidLogData
  {
    public int LogId { get; private set; }

    public string BossIname { get; private set; }

    public string PlayerName { get; private set; }

    public long Damage { get; private set; }

    public bool Deserialize(JSON_WorldRaidLogData json)
    {
      this.LogId = json.log_id;
      this.BossIname = json.boss_iname;
      this.PlayerName = json.player_name;
      this.Damage = json.damage;
      return true;
    }
  }
}
