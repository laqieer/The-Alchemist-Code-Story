// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class PlayerGuildData
  {
    private int mGid;
    private string mName;
    private GuildMemberData.eRole mRoleId;
    private int mInvestPoint;
    private long mAppliedAt;
    private long mJoinedAt;
    private long mLeavedAt;

    public int Gid
    {
      get
      {
        return this.mGid;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public GuildMemberData.eRole RoleId
    {
      get
      {
        return this.mRoleId;
      }
    }

    public int InvestPoint
    {
      get
      {
        return this.mInvestPoint;
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

    public long LeavedAt
    {
      get
      {
        return this.mLeavedAt;
      }
    }

    public bool IsJoined
    {
      get
      {
        return this.mJoinedAt > 0L;
      }
    }

    public bool IsGuildMaster
    {
      get
      {
        return this.mRoleId == GuildMemberData.eRole.MASTAER;
      }
    }

    public bool IsSubGuildMaster
    {
      get
      {
        return this.mRoleId == GuildMemberData.eRole.SUB_MASTAER;
      }
    }

    public bool Deserialize(JSON_PlayerGuild json)
    {
      this.mGid = json.gid;
      this.mName = json.guild_name;
      this.mRoleId = (GuildMemberData.eRole) json.role_id;
      this.mInvestPoint = json.invest_point;
      this.mAppliedAt = json.applied_at;
      this.mJoinedAt = json.joined_at;
      this.mLeavedAt = json.leaved_at;
      return true;
    }
  }
}
