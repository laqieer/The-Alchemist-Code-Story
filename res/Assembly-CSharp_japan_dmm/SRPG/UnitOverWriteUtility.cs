// Decompiled with JetBrains decompiler
// Type: SRPG.UnitOverWriteUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitOverWriteUtility
  {
    public static eOverWritePartyType[] SupportPartyTypes = new eOverWritePartyType[7]
    {
      eOverWritePartyType.Support_Favorite,
      eOverWritePartyType.Support_Fire,
      eOverWritePartyType.Support_Water,
      eOverWritePartyType.Support_Wind,
      eOverWritePartyType.Support_Thunder,
      eOverWritePartyType.Support_Shine,
      eOverWritePartyType.Support_Dark
    };

    public static bool IsInitalized
    {
      get => MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas != null;
    }

    public static eOverWritePartyType String2OverWritePartyType(string str)
    {
      if (str != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (UnitOverWriteUtility.\u003C\u003Ef__switch\u0024map4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UnitOverWriteUtility.\u003C\u003Ef__switch\u0024map4 = new Dictionary<string, int>(9)
          {
            {
              "col",
              0
            },
            {
              "coldef",
              1
            },
            {
              "support0",
              2
            },
            {
              "support1",
              3
            },
            {
              "support2",
              4
            },
            {
              "support3",
              5
            },
            {
              "support4",
              6
            },
            {
              "support5",
              7
            },
            {
              "support6",
              8
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (UnitOverWriteUtility.\u003C\u003Ef__switch\u0024map4.TryGetValue(str, out num))
        {
          switch (num)
          {
            case 0:
              return eOverWritePartyType.Arena;
            case 1:
              return eOverWritePartyType.ArenaDef;
            case 2:
              return eOverWritePartyType.Support_Favorite;
            case 3:
              return eOverWritePartyType.Support_Fire;
            case 4:
              return eOverWritePartyType.Support_Water;
            case 5:
              return eOverWritePartyType.Support_Wind;
            case 6:
              return eOverWritePartyType.Support_Thunder;
            case 7:
              return eOverWritePartyType.Support_Shine;
            case 8:
              return eOverWritePartyType.Support_Dark;
          }
        }
      }
      return eOverWritePartyType.None;
    }

    public static string OverWritePartyType2String(eOverWritePartyType party_type)
    {
      switch (party_type)
      {
        case eOverWritePartyType.Arena:
          return "col";
        case eOverWritePartyType.ArenaDef:
          return "coldef";
        case eOverWritePartyType.Support_Favorite:
          return "support0";
        case eOverWritePartyType.Support_Fire:
          return "support1";
        case eOverWritePartyType.Support_Water:
          return "support2";
        case eOverWritePartyType.Support_Wind:
          return "support3";
        case eOverWritePartyType.Support_Thunder:
          return "support4";
        case eOverWritePartyType.Support_Shine:
          return "support5";
        case eOverWritePartyType.Support_Dark:
          return "support6";
        default:
          return string.Empty;
      }
    }

    public static PlayerPartyTypes OverWritePartyType2PlayerPartyType(
      eOverWritePartyType ow_party_type)
    {
      if (ow_party_type == eOverWritePartyType.Arena)
        return PlayerPartyTypes.Arena;
      return ow_party_type == eOverWritePartyType.ArenaDef ? PlayerPartyTypes.ArenaDef : PlayerPartyTypes.Normal;
    }

    public static eOverWritePartyType PlayerPartyType2OverWritePartyType(PlayerPartyTypes party_type)
    {
      if (party_type == PlayerPartyTypes.Arena)
        return eOverWritePartyType.Arena;
      return party_type == PlayerPartyTypes.ArenaDef ? eOverWritePartyType.ArenaDef : eOverWritePartyType.None;
    }

    public static PartyWindow2.EditPartyTypes OverWritePartyType2EditPartyType(
      eOverWritePartyType ow_party_type)
    {
      if (ow_party_type == eOverWritePartyType.Arena)
        return PartyWindow2.EditPartyTypes.Arena;
      return ow_party_type == eOverWritePartyType.ArenaDef ? PartyWindow2.EditPartyTypes.ArenaDef : PartyWindow2.EditPartyTypes.Auto;
    }

    public static eOverWritePartyType EditPartyType2OverWritePartyType(
      PartyWindow2.EditPartyTypes party_type)
    {
      if (party_type == PartyWindow2.EditPartyTypes.Arena)
        return eOverWritePartyType.Arena;
      return party_type == PartyWindow2.EditPartyTypes.ArenaDef ? eOverWritePartyType.ArenaDef : eOverWritePartyType.None;
    }

    public static eOverWritePartyType Element2OverWritePartyType(EElement element)
    {
      switch (element)
      {
        case EElement.None:
          return eOverWritePartyType.Support_Favorite;
        case EElement.Fire:
          return eOverWritePartyType.Support_Fire;
        case EElement.Water:
          return eOverWritePartyType.Support_Water;
        case EElement.Wind:
          return eOverWritePartyType.Support_Wind;
        case EElement.Thunder:
          return eOverWritePartyType.Support_Thunder;
        case EElement.Shine:
          return eOverWritePartyType.Support_Shine;
        case EElement.Dark:
          return eOverWritePartyType.Support_Dark;
        default:
          return eOverWritePartyType.None;
      }
    }

    public static UnitData Apply(
      UnitData unit,
      eOverWritePartyType party_type,
      bool is_refresh_derive = true)
    {
      if (unit == null || MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(party_type))
        return unit;
      List<PartyOverWrite> partyOverWriteData = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas[party_type];
      if (partyOverWriteData == null || partyOverWriteData.Count <= 0)
        return unit;
      UnitOverWriteData over_write_data = (UnitOverWriteData) null;
      for (int index = 0; index < partyOverWriteData.Count; ++index)
      {
        if (partyOverWriteData[index] != null && partyOverWriteData[index].Units != null)
        {
          over_write_data = Array.Find<UnitOverWriteData>(partyOverWriteData[index].Units, (Predicate<UnitOverWriteData>) (ow_unit => ow_unit != null && ow_unit.UniqueID == unit.UniqueID));
          if (over_write_data != null)
            break;
        }
      }
      if (over_write_data == null)
        return unit;
      UnitData unitData = new UnitData();
      unitData.TempFlags = unit.TempFlags;
      unitData.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
      unitData.Setup(unit);
      unitData.OverWrite(over_write_data, party_type);
      if (is_refresh_derive)
      {
        unitData.DtuSetupSkillAbilityDerive();
        unitData.SetJobIndex(unitData.JobIndex);
      }
      return unitData;
    }

    public static UnitData Apply(
      UnitData unit,
      PlayerPartyTypes party_type,
      bool is_refresh_derive = true)
    {
      eOverWritePartyType party_type1 = UnitOverWriteUtility.PlayerPartyType2OverWritePartyType(party_type);
      return UnitOverWriteUtility.Apply(unit, party_type1, is_refresh_derive);
    }

    public static UnitData Apply(
      UnitData unit,
      PartyWindow2.EditPartyTypes party_type,
      bool is_refresh_derive = true)
    {
      eOverWritePartyType party_type1 = UnitOverWriteUtility.EditPartyType2OverWritePartyType(party_type);
      return UnitOverWriteUtility.Apply(unit, party_type1, is_refresh_derive);
    }

    public static List<PartyEditData> ApplyTeams(
      List<PartyEditData> teams,
      eOverWritePartyType party_type)
    {
      if (MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(party_type))
        return teams;
      List<PartyEditData> partyEditDataList = new List<PartyEditData>();
      for (int index = 0; index < teams.Count; ++index)
      {
        PartyEditData partyEditData = new PartyEditData(teams[index].Units, teams[index].Name, teams[index].PartyData);
        partyEditDataList.Add(partyEditData);
      }
      for (int index1 = 0; index1 < partyEditDataList.Count; ++index1)
      {
        for (int index2 = 0; index2 < partyEditDataList[index1].Units.Length; ++index2)
          partyEditDataList[index1].Units[index2] = UnitOverWriteUtility.Apply(partyEditDataList[index1].Units[index2], party_type);
      }
      return partyEditDataList;
    }

    public static List<PartyEditData> ApplyTeams(
      List<PartyEditData> teams,
      PartyWindow2.EditPartyTypes party_type)
    {
      eOverWritePartyType party_type1 = UnitOverWriteUtility.EditPartyType2OverWritePartyType(party_type);
      return UnitOverWriteUtility.ApplyTeams(teams, party_type1);
    }

    public static bool IsSupportPartyType(eOverWritePartyType party_type)
    {
      return Array.IndexOf<eOverWritePartyType>(UnitOverWriteUtility.SupportPartyTypes, party_type) >= 0;
    }

    public static List<UnitData> GetSupportAllUnits(bool is_refresh_derive = true)
    {
      List<UnitData> supportAllUnits = new List<UnitData>();
      for (int index = 0; index < UnitOverWriteUtility.SupportPartyTypes.Length; ++index)
        supportAllUnits.AddRange((IEnumerable<UnitData>) UnitOverWriteUtility.GetSupportUnits(UnitOverWriteUtility.SupportPartyTypes[index], is_refresh_derive));
      return supportAllUnits;
    }

    public static List<UnitData> GetSupportUnits(
      eOverWritePartyType party_type,
      bool is_refresh_derive = true)
    {
      if (!UnitOverWriteUtility.IsSupportPartyType(party_type))
        return new List<UnitData>();
      Dictionary<eOverWritePartyType, List<PartyOverWrite>> partyOverWriteDatas = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas;
      if (partyOverWriteDatas == null || !partyOverWriteDatas.ContainsKey(party_type))
        return new List<UnitData>();
      List<UnitData> supportUnits = new List<UnitData>();
      foreach (PartyOverWrite partyOverWrite in partyOverWriteDatas[party_type])
      {
        if (partyOverWrite != null && partyOverWrite.Units != null)
        {
          foreach (UnitOverWriteData unit in partyOverWrite.Units)
          {
            if (unit != null)
            {
              UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UniqueID);
              if (unitDataByUniqueId != null)
              {
                UnitData unitData = UnitOverWriteUtility.Apply(unitDataByUniqueId, party_type, is_refresh_derive);
                supportUnits.Add(unitData);
              }
            }
          }
        }
      }
      return supportUnits;
    }

    public static List<UnitData> GetPartyUnits(
      eOverWritePartyType party_type,
      bool is_refresh_derive = true)
    {
      UnitData[] unitDataArray = !UnitOverWriteUtility.IsSupportPartyType(party_type) ? MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(UnitOverWriteUtility.OverWritePartyType2PlayerPartyType(party_type)).ToUnits() : UnitOverWriteUtility.GetSupportUnits(party_type, is_refresh_derive).ToArray();
      List<UnitData> partyUnits = new List<UnitData>();
      if (!UnitOverWriteUtility.IsNeedOverWrite(party_type))
      {
        for (int index = 0; index < unitDataArray.Length; ++index)
        {
          if (unitDataArray[index] != null)
            partyUnits.Add(unitDataArray[index]);
        }
        return partyUnits;
      }
      for (int index = 0; index < unitDataArray.Length; ++index)
      {
        if (unitDataArray[index] != null)
        {
          UnitData unitData = UnitOverWriteUtility.Apply(unitDataArray[index], party_type, is_refresh_derive);
          partyUnits.Add(unitData);
        }
      }
      return partyUnits;
    }

    public static List<UnitData> GetAllPartyUnits()
    {
      List<UnitData> allPartyUnits = new List<UnitData>();
      foreach (eOverWritePartyType party_type in Enum.GetValues(typeof (eOverWritePartyType)))
      {
        if (UnitOverWriteUtility.IsNeedOverWrite(party_type))
          allPartyUnits.AddRange((IEnumerable<UnitData>) UnitOverWriteUtility.GetPartyUnits(party_type));
      }
      return allPartyUnits;
    }

    public static bool IsNeedOverWrite(eOverWritePartyType party_type)
    {
      switch (party_type)
      {
        case eOverWritePartyType.Arena:
        case eOverWritePartyType.ArenaDef:
        case eOverWritePartyType.Support_Favorite:
        case eOverWritePartyType.Support_Fire:
        case eOverWritePartyType.Support_Water:
        case eOverWritePartyType.Support_Wind:
        case eOverWritePartyType.Support_Thunder:
        case eOverWritePartyType.Support_Shine:
        case eOverWritePartyType.Support_Dark:
          return true;
        default:
          return false;
      }
    }

    public static bool IsNeedOverWrite(PlayerPartyTypes party_type)
    {
      return UnitOverWriteUtility.IsNeedOverWrite(UnitOverWriteUtility.PlayerPartyType2OverWritePartyType(party_type));
    }

    public static bool IsNeedOverWrite(PartyWindow2.EditPartyTypes party_type)
    {
      return UnitOverWriteUtility.IsNeedOverWrite(UnitOverWriteUtility.EditPartyType2OverWritePartyType(party_type));
    }

    public static UnitData GetOwner(ConceptCardData card)
    {
      UnitData owner = (UnitData) null;
      foreach (eOverWritePartyType ow_party_type in Enum.GetValues(typeof (eOverWritePartyType)))
      {
        owner = UnitOverWriteUtility.GetOwner(card, ow_party_type);
        if (owner != null)
          break;
      }
      return owner;
    }

    public static UnitData GetOwner(ConceptCardData card, eOverWritePartyType ow_party_type)
    {
      if (card == null)
        return (UnitData) null;
      if (MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null)
        return (UnitData) null;
      if (!UnitOverWriteUtility.IsNeedOverWrite(ow_party_type) || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(ow_party_type))
        return (UnitData) null;
      List<PartyOverWrite> partyOverWriteData = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas[ow_party_type];
      if (partyOverWriteData == null)
        return (UnitData) null;
      UnitData unit1 = (UnitData) null;
      foreach (PartyOverWrite partyOverWrite in partyOverWriteData)
      {
        if (partyOverWrite != null && partyOverWrite.Units != null)
        {
          foreach (UnitOverWriteData unit2 in partyOverWrite.Units)
          {
            if (unit2 != null && unit2.IsEquipConceptCard((long) card.UniqueID))
            {
              unit1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit2.UniqueID);
              break;
            }
          }
        }
      }
      return UnitOverWriteUtility.Apply(unit1, ow_party_type, false);
    }

    public static UnitData GetOwner(ArtifactData artifact)
    {
      UnitData owner = (UnitData) null;
      foreach (eOverWritePartyType ow_party_type in Enum.GetValues(typeof (eOverWritePartyType)))
      {
        owner = UnitOverWriteUtility.GetOwner(artifact, ow_party_type);
        if (owner != null)
          break;
      }
      return owner;
    }

    public static UnitData GetOwner(ArtifactData artifact, eOverWritePartyType ow_party_type)
    {
      if (artifact == null)
        return (UnitData) null;
      if (MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null)
        return (UnitData) null;
      if (!UnitOverWriteUtility.IsNeedOverWrite(ow_party_type) || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(ow_party_type))
        return (UnitData) null;
      List<PartyOverWrite> partyOverWriteData = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas[ow_party_type];
      if (partyOverWriteData == null)
        return (UnitData) null;
      UnitData unit1 = (UnitData) null;
      foreach (PartyOverWrite partyOverWrite in partyOverWriteData)
      {
        if (partyOverWrite != null && partyOverWrite.Units != null)
        {
          foreach (UnitOverWriteData unit2 in partyOverWrite.Units)
          {
            if (unit2 != null && unit2.Artifacts != null && Array.IndexOf<long>(unit2.Artifacts, (long) artifact.UniqueID) >= 0)
            {
              unit1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit2.UniqueID);
              break;
            }
          }
        }
      }
      return UnitOverWriteUtility.Apply(unit1, ow_party_type, false);
    }

    public static long GetEquipedUnitId(ArtifactData artifact, eOverWritePartyType ow_party_type)
    {
      if (artifact == null || MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(ow_party_type))
        return -1;
      List<PartyOverWrite> partyOverWriteData = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas[ow_party_type];
      for (int index1 = 0; index1 < partyOverWriteData.Count; ++index1)
      {
        PartyOverWrite partyOverWrite = partyOverWriteData[index1];
        if (partyOverWrite != null && partyOverWrite.Units != null)
        {
          for (int index2 = 0; index2 < partyOverWrite.Units.Length; ++index2)
          {
            UnitOverWriteData unit = partyOverWrite.Units[index2];
            if (unit != null && unit.Artifacts != null && Array.FindIndex<long>(unit.Artifacts, (Predicate<long>) (ow_arti => ow_arti == (long) artifact.UniqueID)) >= 0)
              return unit.UniqueID;
          }
        }
      }
      return -1;
    }

    public static long GetEquipedUnitId(ConceptCardData card, eOverWritePartyType ow_party_type)
    {
      if (card == null || MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas == null || !MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas.ContainsKey(ow_party_type))
        return -1;
      List<PartyOverWrite> partyOverWriteData = MonoSingleton<GameManager>.Instance.Player.PartyOverWriteDatas[ow_party_type];
      for (int index1 = 0; index1 < partyOverWriteData.Count; ++index1)
      {
        PartyOverWrite partyOverWrite = partyOverWriteData[index1];
        if (partyOverWrite != null && partyOverWrite.Units != null)
        {
          for (int index2 = 0; index2 < partyOverWrite.Units.Length; ++index2)
          {
            UnitOverWriteData unit = partyOverWrite.Units[index2];
            if (unit != null && unit.IsEquipConceptCard((long) card.UniqueID))
              return unit.UniqueID;
          }
        }
      }
      return -1;
    }
  }
}
