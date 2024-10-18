// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidMailListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidMailListItem
  {
    public int MailId { get; private set; }

    public int Round { get; private set; }

    public string Message { get; private set; }

    public int BossId { get; private set; }

    public string RewardId { get; private set; }

    public GuildRaidRewardType RewardType { get; private set; }

    public bool Deserialize(JSON_GuildRaidMailListItem json)
    {
      if (json == null)
        return false;
      this.MailId = json.mid;
      this.Round = json.round;
      this.Message = json.msg;
      this.BossId = json.boss_id;
      this.RewardId = json.reward_id;
      this.RewardType = (GuildRaidRewardType) json.reward_type;
      return true;
    }
  }
}
