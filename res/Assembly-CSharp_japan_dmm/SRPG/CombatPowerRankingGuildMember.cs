// Decompiled with JetBrains decompiler
// Type: SRPG.CombatPowerRankingGuildMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class CombatPowerRankingGuildMember : GuildMemberData, IRankingContent, ICombatPowerContent
  {
    private int mRank;
    private long mCombatPower;

    public int Rank => this.mRank;

    public long CombatPower => this.mCombatPower;

    public bool Deserialize(JSON_CombatPowerRankingGuildMember json)
    {
      if (json == null)
      {
        DebugUtility.LogError("json == null");
        return false;
      }
      this.Deserialize((JSON_GuildMember) json);
      this.mRank = json.rank;
      this.mCombatPower = json.combat_power;
      return true;
    }
  }
}
