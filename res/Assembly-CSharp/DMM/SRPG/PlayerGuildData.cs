// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class PlayerGuildData
  {
    private int mGid;
    private string mName;
    private GuildMemberData.eRole mRoleId;
    private long mAppliedAt;
    private long mJoinedAt;
    private long mLeavedAt;

    public int Gid => this.mGid;

    public string Name => this.mName;

    public GuildMemberData.eRole RoleId => this.mRoleId;

    public long AppliedAt => this.mAppliedAt;

    public long JoinedAt => this.mJoinedAt;

    public long LeavedAt => this.mLeavedAt;

    public bool IsJoined => this.mJoinedAt > 0L;

    public bool IsGuildMaster => this.mRoleId == GuildMemberData.eRole.MASTAER;

    public bool IsSubGuildMaster => this.mRoleId == GuildMemberData.eRole.SUB_MASTAER;

    public bool Deserialize(JSON_PlayerGuild json)
    {
      this.mGid = json.gid;
      this.mName = json.guild_name;
      this.mRoleId = (GuildMemberData.eRole) json.role_id;
      this.mAppliedAt = json.applied_at;
      this.mJoinedAt = json.joined_at;
      this.mLeavedAt = json.leaved_at;
      return true;
    }
  }
}
