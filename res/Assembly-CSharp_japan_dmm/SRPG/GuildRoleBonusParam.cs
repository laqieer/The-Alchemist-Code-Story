// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusParam
  {
    private int mID;
    private GuildMemberData.eRole mRoleID;
    private long mStartAt;
    private long mEndAt;
    private GuildRoleBonusDetail[] mRewards;

    public int id => this.mID;

    public GuildMemberData.eRole role => this.mRoleID;

    public long start_at => this.mStartAt;

    public long end_at => this.mEndAt;

    public GuildRoleBonusDetail[] rewards => this.mRewards;

    public static bool Deserialize(ref GuildRoleBonusParam[] param, JSON_GuildRoleBonus[] json)
    {
      if (json == null)
        return false;
      param = new GuildRoleBonusParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        GuildRoleBonusParam guildRoleBonusParam = new GuildRoleBonusParam();
        guildRoleBonusParam.Deserialize(json[index]);
        param[index] = guildRoleBonusParam;
      }
      return true;
    }

    public void Deserialize(JSON_GuildRoleBonus json)
    {
      if (json == null || json.rewards == null)
        return;
      this.mID = json.id;
      this.mRoleID = (GuildMemberData.eRole) json.role;
      this.mStartAt = 0L;
      DateTime result;
      if (!string.IsNullOrEmpty(json.start_at))
      {
        DateTime.TryParse(json.start_at, out result);
        this.mStartAt = TimeManager.GetUnixSec(result);
      }
      this.mEndAt = 0L;
      if (!string.IsNullOrEmpty(json.end_at))
      {
        DateTime.TryParse(json.end_at, out result);
        this.mEndAt = TimeManager.GetUnixSec(result);
      }
      this.mRewards = new GuildRoleBonusDetail[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        GuildRoleBonusDetail guildRoleBonusDetail = new GuildRoleBonusDetail();
        guildRoleBonusDetail.Deserialize(json.rewards[index]);
        this.mRewards[index] = guildRoleBonusDetail;
      }
    }
  }
}
