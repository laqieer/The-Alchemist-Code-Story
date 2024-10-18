// Decompiled with JetBrains decompiler
// Type: PartyEditData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;

#nullable disable
public class PartyEditData
{
  public UnitData[] Units;
  public PartyData PartyData;
  public string Name;

  public PartyEditData(UnitData[] src, string name, PartyData party)
  {
    this.Units = new UnitData[party.MAX_UNIT];
    this.PartyData = party;
    this.Name = name;
    this.SetUnits(src);
  }

  public PartyEditData(string name, PartyData party)
  {
    this.Units = new UnitData[party.MAX_UNIT];
    PlayerData player = MonoSingleton<GameManager>.Instance.Player;
    this.PartyData = party;
    this.Name = name;
    for (int index = 0; index < party.MAX_UNIT; ++index)
    {
      long unitUniqueId = party.GetUnitUniqueID(index);
      if (unitUniqueId > 0L)
        this.Units[index] = player.FindUnitDataByUniqueID(unitUniqueId);
    }
  }

  public PartyEditData(JSON_MyPhotonPlayerParam src)
  {
    for (int index = 0; index < src.units.Length; ++index)
    {
      JSON_MyPhotonPlayerParam.UnitDataElem unit = src.units[index];
      if (0 <= unit.slotID && unit.slotID < this.PartyData.MAX_UNIT)
        this.Units[index] = unit.unit;
    }
  }

  public int IndexOf(UnitData unit)
  {
    for (int index = 0; index < this.Units.Length; ++index)
    {
      if (this.Units[index] != null && this.Units[index].UniqueID == unit.UniqueID)
        return index;
    }
    return -1;
  }

  public void SetUnits(UnitData[] src)
  {
    for (int index = 0; index < src.Length && index < this.Units.Length; ++index)
      this.Units[index] = src[index];
    for (int mainmemberStart = this.PartyData.MAINMEMBER_START; mainmemberStart < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER; ++mainmemberStart)
    {
      if (this.Units[mainmemberStart] == null)
      {
        for (int index = mainmemberStart + 1; index < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER; ++index)
        {
          if (this.Units[index] != null)
            this.Units[mainmemberStart++] = this.Units[index];
        }
        while (mainmemberStart < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER)
          this.Units[mainmemberStart++] = (UnitData) null;
      }
    }
    for (int submemberStart = this.PartyData.SUBMEMBER_START; submemberStart < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER; ++submemberStart)
    {
      if (this.Units[submemberStart] == null)
      {
        for (int index = submemberStart + 1; index < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER; ++index)
        {
          if (this.Units[index] != null)
            this.Units[submemberStart++] = this.Units[index];
        }
        while (submemberStart < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER)
          this.Units[submemberStart++] = (UnitData) null;
      }
    }
  }

  public void SetUnitsForce(UnitData[] src)
  {
    for (int index = 0; index < src.Length && index < this.Units.Length; ++index)
      this.Units[index] = src[index];
  }

  public int GetMainMemberCount()
  {
    int mainMemberCount = 0;
    for (int mainmemberStart = this.PartyData.MAINMEMBER_START; mainmemberStart <= this.PartyData.MAINMEMBER_END; ++mainmemberStart)
    {
      if (this.Units[mainmemberStart] != null)
        ++mainMemberCount;
    }
    return mainMemberCount;
  }

  public int GetSubMemberCount()
  {
    int subMemberCount = 0;
    for (int submemberStart = this.PartyData.SUBMEMBER_START; submemberStart <= this.PartyData.SUBMEMBER_END; ++submemberStart)
    {
      if (this.Units[submemberStart] != null)
        ++subMemberCount;
    }
    return subMemberCount;
  }

  public int GetMainSlotNum(long uniqId)
  {
    int mainSlotNum = -1;
    for (int mainmemberStart = this.PartyData.MAINMEMBER_START; mainmemberStart <= this.PartyData.MAINMEMBER_END; ++mainmemberStart)
    {
      if (this.Units[mainmemberStart] != null && this.Units[mainmemberStart].UniqueID == uniqId)
        mainSlotNum = mainmemberStart - this.PartyData.MAINMEMBER_START;
    }
    return mainSlotNum;
  }

  public int GetSubSlotNum(long uniqId)
  {
    int subSlotNum = -1;
    for (int submemberStart = this.PartyData.SUBMEMBER_START; submemberStart <= this.PartyData.SUBMEMBER_END; ++submemberStart)
    {
      if (this.Units[submemberStart] != null && this.Units[submemberStart].UniqueID == uniqId)
        subSlotNum = submemberStart - this.PartyData.SUBMEMBER_START;
    }
    return subSlotNum;
  }

  public bool IsSubSlot(int slotNo)
  {
    return this.PartyData.SUBMEMBER_START <= slotNo && slotNo <= this.PartyData.SUBMEMBER_END;
  }

  public void Reset()
  {
    for (int index = 0; index < this.Units.Length; ++index)
      this.Units[index] = (UnitData) null;
  }
}
