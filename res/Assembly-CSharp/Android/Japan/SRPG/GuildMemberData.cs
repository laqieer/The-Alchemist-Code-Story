// Decompiled with JetBrains decompiler
// Type: SRPG.GuildMemberData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    public long Gid
    {
      get
      {
        return this.mGid;
      }
    }

    public string Uid
    {
      get
      {
        return this.mUid;
      }
    }

    public GuildMemberData.eRole RoleId
    {
      get
      {
        return this.mRoleId;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int Level
    {
      get
      {
        return this.mLevel;
      }
    }

    public string Award
    {
      get
      {
        return this.mAward;
      }
    }

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public long AppliedAt
    {
      get
      {
        return this.mAppliedAt;
      }
    }

    public long JoinedAt
    {
      get
      {
        return this.mJoinedAt;
      }
    }

    public long LeaveAt
    {
      get
      {
        return this.mLeaveAt;
      }
    }

    public long LastLogin
    {
      get
      {
        return this.mLastLogin;
      }
    }

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
