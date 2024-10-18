// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class WorldRaidBossData
  {
    public string BossIname { get; private set; }

    public long CurrentHP { get; private set; }

    public long CalcMaxHP()
    {
      long num = 0;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return num;
      WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.GetBossInfo(this.BossIname);
      long hp = bossInfo.BossParam.Hp;
      BuffEffectParam bossBuffParam = bossInfo.BossBuffParam;
      return bossBuffParam == null ? hp : bossBuffParam.CalcMaxHP(hp, 1, 1);
    }

    public bool Deserialize(JSON_WorldRaidBossData json)
    {
      this.BossIname = json.boss_iname;
      this.CurrentHP = json.current_hp;
      return true;
    }
  }
}
