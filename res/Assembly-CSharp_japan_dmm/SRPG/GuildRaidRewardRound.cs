// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardRound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardRound
  {
    public int Round { get; private set; }

    public string RewardId { get; private set; }

    public bool Deserialize(JSON_GuildRaidRewardRound json)
    {
      if (json == null)
        return false;
      this.Round = json.round;
      this.RewardId = json.reward_id;
      return true;
    }
  }
}
