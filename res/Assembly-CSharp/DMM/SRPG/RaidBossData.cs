// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidBossData
  {
    private string mOwnerUID;
    private string mOwnerName;
    private int mAreaId;
    private RaidBossInfo mRaidBossInfo;
    private RaidSOSStatus mSOSStatus;
    private List<RaidSOSMember> mSOSMember;

    public string OwnerUID => this.mOwnerUID;

    public string OwnerName => this.mOwnerName;

    public int AreaId => this.mAreaId;

    public RaidBossInfo RaidBossInfo => this.mRaidBossInfo;

    public RaidSOSStatus SOSStatus => this.mSOSStatus;

    public List<RaidSOSMember> SOSMember => this.mSOSMember;

    public bool Deserialize(JSON_RaidBossData json)
    {
      this.mOwnerUID = json.uid;
      this.mOwnerName = json.name;
      this.mAreaId = json.area_id;
      if (json.boss_info == null)
        return false;
      this.mRaidBossInfo = new RaidBossInfo();
      if (!this.mRaidBossInfo.Deserialize(json.boss_info))
        return false;
      this.mSOSStatus = (RaidSOSStatus) json.sos_status;
      this.mSOSMember = new List<RaidSOSMember>();
      if (json.sos_member != null)
      {
        for (int index = 0; index < json.sos_member.Length; ++index)
        {
          RaidSOSMember raidSosMember = new RaidSOSMember();
          if (!raidSosMember.Deserialize(json.sos_member[index]))
            return false;
          this.mSOSMember.Add(raidSosMember);
        }
        this.mSOSMember.Sort((Comparison<RaidSOSMember>) ((a, b) => (int) (b.LastBattleTime - a.LastBattleTime)));
      }
      return true;
    }

    public void SOSDone() => this.mSOSStatus = RaidSOSStatus.Done;
  }
}
