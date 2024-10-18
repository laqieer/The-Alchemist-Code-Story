// Decompiled with JetBrains decompiler
// Type: SRPG.RaidSOSMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidSOSMember
  {
    private string mFUID;
    private string mName;
    private int mLv;
    private UnitData mUnit;
    private RaidRescueMemberType mMemberType;
    private long mLastBattleTime;

    public string FUID
    {
      get
      {
        return this.mFUID;
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

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public RaidRescueMemberType MemberType
    {
      get
      {
        return this.mMemberType;
      }
    }

    public long LastBattleTime
    {
      get
      {
        return this.mLastBattleTime;
      }
    }

    public bool Deserialize(JSON_RaidSOSMember json)
    {
      this.mFUID = json.fuid;
      this.mName = json.name;
      this.mLv = json.lv;
      this.mMemberType = (RaidRescueMemberType) json.member_type;
      this.mLastBattleTime = json.last_battle_time;
      if (json.unit != null)
      {
        this.mUnit = new UnitData();
        this.mUnit.Deserialize(json.unit);
      }
      return true;
    }
  }
}
