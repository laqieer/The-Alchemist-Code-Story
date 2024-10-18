// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBossData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

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

    public string OwnerUID
    {
      get
      {
        return this.mOwnerUID;
      }
    }

    public string OwnerName
    {
      get
      {
        return this.mOwnerName;
      }
    }

    public int AreaId
    {
      get
      {
        return this.mAreaId;
      }
    }

    public RaidBossInfo RaidBossInfo
    {
      get
      {
        return this.mRaidBossInfo;
      }
    }

    public RaidSOSStatus SOSStatus
    {
      get
      {
        return this.mSOSStatus;
      }
    }

    public List<RaidSOSMember> SOSMember
    {
      get
      {
        return this.mSOSMember;
      }
    }

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

    public void SOSDone()
    {
      this.mSOSStatus = RaidSOSStatus.Done;
    }
  }
}
