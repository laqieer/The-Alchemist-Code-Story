// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class RaidRescueMember
  {
    private string mUID;
    private string mName;
    private int mLv;
    private RaidRescueMemberType mMemberType;
    private UnitData mUnit;
    private string mSelectedAward;
    private DateTime mLastLogin;
    private int mAreaId;
    private int mBossId;
    private int mRound;
    private int mCurrentHp;
    private long mStartTime;

    public string UID
    {
      get
      {
        return this.mUID;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int Lv
    {
      get
      {
        return this.mLv;
      }
    }

    public RaidRescueMemberType MemberType
    {
      get
      {
        return this.mMemberType;
      }
    }

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public string SelectedAward
    {
      get
      {
        return this.mSelectedAward;
      }
    }

    public DateTime LastLogin
    {
      get
      {
        return this.mLastLogin;
      }
    }

    public int AreaId
    {
      get
      {
        return this.mAreaId;
      }
    }

    public int BossId
    {
      get
      {
        return this.mBossId;
      }
    }

    public int Round
    {
      get
      {
        return this.mRound;
      }
    }

    public int CurrentHp
    {
      get
      {
        return this.mCurrentHp;
      }
    }

    public long StartTime
    {
      get
      {
        return this.mStartTime;
      }
    }

    public bool Deserialize(JSON_RaidRescueMember json)
    {
      this.mUID = json.uid;
      this.mName = json.name;
      this.mLv = json.lv;
      this.mMemberType = (RaidRescueMemberType) json.member_type;
      this.mSelectedAward = json.selected_award;
      this.mLastLogin = TimeManager.FromUnixTime((long) json.lastlogin);
      this.mAreaId = json.area_id;
      this.mBossId = json.boss_id;
      this.mRound = json.round;
      this.mCurrentHp = json.current_hp;
      this.mStartTime = json.start_time;
      if (json.unit != null)
      {
        this.mUnit = new UnitData();
        this.mUnit.Deserialize(json.unit);
      }
      return true;
    }
  }
}
