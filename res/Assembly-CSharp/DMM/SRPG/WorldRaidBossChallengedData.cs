// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossChallengedData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class WorldRaidBossChallengedData : WorldRaidBossData
  {
    public bool Challenged { get; private set; }

    public bool Deserialize(JSON_WorldRaidBossChallengedData json)
    {
      if (json == null || !this.Deserialize((JSON_WorldRaidBossData) json))
        return false;
      this.Challenged = json.challenged != 0;
      return true;
    }
  }
}
