// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildAttendRewardParam
  {
    private string mID;
    private GuildAttendReward[] mRewards;

    public string id => this.mID;

    public GuildAttendReward[] rewards => this.mRewards;

    public static bool Deserialize(
      ref List<GuildAttendRewardParam> param,
      JSON_GuildAttendRewardParam[] json)
    {
      if (json == null)
        return false;
      param = new List<GuildAttendRewardParam>();
      for (int index = 0; index < json.Length; ++index)
      {
        GuildAttendRewardParam attendRewardParam = new GuildAttendRewardParam();
        attendRewardParam.Deserialize(json[index]);
        param.Add(attendRewardParam);
      }
      return true;
    }

    public void Deserialize(JSON_GuildAttendRewardParam json)
    {
      if (json == null || json.rewards == null)
        return;
      this.mID = json.id;
      this.mRewards = new GuildAttendReward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        GuildAttendReward guildAttendReward = new GuildAttendReward();
        guildAttendReward.Deserialize(json.rewards[index]);
        this.mRewards[index] = guildAttendReward;
      }
    }
  }
}
