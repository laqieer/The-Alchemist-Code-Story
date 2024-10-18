// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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

    public string UID => this.mUID;

    public string Name => this.mName;

    public int Lv => this.mLv;

    public RaidRescueMemberType MemberType => this.mMemberType;

    public UnitData Unit => this.mUnit;

    public string SelectedAward => this.mSelectedAward;

    public DateTime LastLogin => this.mLastLogin;

    public int AreaId => this.mAreaId;

    public int BossId => this.mBossId;

    public int Round => this.mRound;

    public int CurrentHp => this.mCurrentHp;

    public long StartTime => this.mStartTime;

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
