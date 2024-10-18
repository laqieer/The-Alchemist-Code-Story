// Decompiled with JetBrains decompiler
// Type: SRPG.RaidSOSMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public string FUID => this.mFUID;

    public string Name => this.mName;

    public int Lv => this.mLv;

    public UnitData Unit => this.mUnit;

    public RaidRescueMemberType MemberType => this.mMemberType;

    public long LastBattleTime => this.mLastBattleTime;

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
