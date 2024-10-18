// Decompiled with JetBrains decompiler
// Type: SRPG.GuildMemberData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildMemberData
  {
    private long mGid;
    private string mUid;
    private GuildMemberData.eRole mRoleId;
    private string mName;
    private int mLevel;
    private string mAward;
    private UnitData mUnit;
    private long mAppliedAt;
    private long mJoinedAt;
    private long mLeaveAt;
    private long mLastLogin;

    public long Gid => this.mGid;

    public string Uid => this.mUid;

    public GuildMemberData.eRole RoleId => this.mRoleId;

    public string Name => this.mName;

    public int Level => this.mLevel;

    public string Award => this.mAward;

    public UnitData Unit => this.mUnit;

    public long AppliedAt => this.mAppliedAt;

    public long JoinedAt => this.mJoinedAt;

    public long LeaveAt => this.mLeaveAt;

    public long LastLogin => this.mLastLogin;

    public bool Deserialize(JSON_GuildMember json)
    {
      this.mGid = json.gid;
      this.mUid = json.uid;
      this.mRoleId = (GuildMemberData.eRole) json.role_id;
      this.mName = json.name;
      this.mLevel = json.lv;
      this.mAward = json.award_id;
      this.mAppliedAt = json.applied_at;
      this.mJoinedAt = json.joined_at;
      this.mLeaveAt = json.leave_at;
      this.mLastLogin = json.lastlogin;
      this.mUnit = new UnitData();
      this.mUnit.Deserialize(json.units);
      return true;
    }

    public enum eRole
    {
      NONE,
      MASTAER,
      MEMBER,
      SUB_MASTAER,
    }
  }
}
