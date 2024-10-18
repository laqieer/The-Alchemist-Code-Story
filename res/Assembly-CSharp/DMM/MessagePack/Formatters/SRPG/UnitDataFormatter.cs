// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.UnitDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class UnitDataFormatter : IMessagePackFormatter<UnitData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public UnitDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "QuestUnlockedLeaderSkill",
          0
        },
        {
          "QuestUnlockedAbilitys",
          1
        },
        {
          "QuestUnlockedSkills",
          2
        },
        {
          "SkillUnlocks",
          3
        },
        {
          "IsMyUnit",
          4
        },
        {
          "UniqueID",
          5
        },
        {
          "UnitParam",
          6
        },
        {
          "UnitID",
          7
        },
        {
          "Status",
          8
        },
        {
          "Lv",
          9
        },
        {
          "Exp",
          10
        },
        {
          "Rarity",
          11
        },
        {
          "AwakeLv",
          12
        },
        {
          "CurrentEquips",
          13
        },
        {
          "LeaderSkill",
          14
        },
        {
          "MasterAbility",
          15
        },
        {
          "CollaboAbility",
          16
        },
        {
          "MapEffectAbility",
          17
        },
        {
          "TobiraMasterAbilitys",
          18
        },
        {
          "CurrentAbilitySlots",
          19
        },
        {
          "LearnAbilitys",
          20
        },
        {
          "BattleAbilitys",
          21
        },
        {
          "BattleSkills",
          22
        },
        {
          "CurrentLeaderSkill",
          23
        },
        {
          "Element",
          24
        },
        {
          "JobType",
          25
        },
        {
          "RoleType",
          26
        },
        {
          "ConceptCards",
          27
        },
        {
          "MainConceptCard",
          28
        },
        {
          "EquipRunes",
          29
        },
        {
          "IsRiding",
          30
        },
        {
          "NumJobsAvailable",
          31
        },
        {
          "CurrentJob",
          32
        },
        {
          "CurrentJobId",
          33
        },
        {
          "IsUnlockTobira",
          34
        },
        {
          "TobiraData",
          35
        },
        {
          "UnlockTobriaNum",
          36
        },
        {
          "Jobs",
          37
        },
        {
          "JobCount",
          38
        },
        {
          "JobIndex",
          39
        },
        {
          "SexPrefix",
          40
        },
        {
          "IsFavorite",
          41
        },
        {
          "SupportElement",
          42
        },
        {
          "IsRental",
          43
        },
        {
          "RentalFavoritePoint",
          44
        },
        {
          "RentalIname",
          45
        },
        {
          "IsNotFindUniqueID",
          46
        },
        {
          "IsIntoUnit",
          47
        },
        {
          "UnlockedSkills",
          48
        },
        {
          "CharacterQuestRarity",
          49
        },
        {
          "IsThrow",
          50
        },
        {
          "IsKnockBack",
          51
        },
        {
          "IsChanging",
          52
        },
        {
          "LastSyncTime",
          53
        },
        {
          "TempFlags",
          54
        },
        {
          "BadgeState",
          55
        }
      };
      this.____stringByteKeys = new byte[56][]
      {
        MessagePackBinary.GetEncodedStringBytes("QuestUnlockedLeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("QuestUnlockedAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("QuestUnlockedSkills"),
        MessagePackBinary.GetEncodedStringBytes("SkillUnlocks"),
        MessagePackBinary.GetEncodedStringBytes("IsMyUnit"),
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("UnitParam"),
        MessagePackBinary.GetEncodedStringBytes("UnitID"),
        MessagePackBinary.GetEncodedStringBytes("Status"),
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("Exp"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("AwakeLv"),
        MessagePackBinary.GetEncodedStringBytes("CurrentEquips"),
        MessagePackBinary.GetEncodedStringBytes("LeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("MasterAbility"),
        MessagePackBinary.GetEncodedStringBytes("CollaboAbility"),
        MessagePackBinary.GetEncodedStringBytes("MapEffectAbility"),
        MessagePackBinary.GetEncodedStringBytes("TobiraMasterAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("CurrentAbilitySlots"),
        MessagePackBinary.GetEncodedStringBytes("LearnAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("BattleAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("BattleSkills"),
        MessagePackBinary.GetEncodedStringBytes("CurrentLeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("Element"),
        MessagePackBinary.GetEncodedStringBytes("JobType"),
        MessagePackBinary.GetEncodedStringBytes("RoleType"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCards"),
        MessagePackBinary.GetEncodedStringBytes("MainConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("EquipRunes"),
        MessagePackBinary.GetEncodedStringBytes("IsRiding"),
        MessagePackBinary.GetEncodedStringBytes("NumJobsAvailable"),
        MessagePackBinary.GetEncodedStringBytes("CurrentJob"),
        MessagePackBinary.GetEncodedStringBytes("CurrentJobId"),
        MessagePackBinary.GetEncodedStringBytes("IsUnlockTobira"),
        MessagePackBinary.GetEncodedStringBytes("TobiraData"),
        MessagePackBinary.GetEncodedStringBytes("UnlockTobriaNum"),
        MessagePackBinary.GetEncodedStringBytes("Jobs"),
        MessagePackBinary.GetEncodedStringBytes("JobCount"),
        MessagePackBinary.GetEncodedStringBytes("JobIndex"),
        MessagePackBinary.GetEncodedStringBytes("SexPrefix"),
        MessagePackBinary.GetEncodedStringBytes("IsFavorite"),
        MessagePackBinary.GetEncodedStringBytes("SupportElement"),
        MessagePackBinary.GetEncodedStringBytes("IsRental"),
        MessagePackBinary.GetEncodedStringBytes("RentalFavoritePoint"),
        MessagePackBinary.GetEncodedStringBytes("RentalIname"),
        MessagePackBinary.GetEncodedStringBytes("IsNotFindUniqueID"),
        MessagePackBinary.GetEncodedStringBytes("IsIntoUnit"),
        MessagePackBinary.GetEncodedStringBytes("UnlockedSkills"),
        MessagePackBinary.GetEncodedStringBytes("CharacterQuestRarity"),
        MessagePackBinary.GetEncodedStringBytes("IsThrow"),
        MessagePackBinary.GetEncodedStringBytes("IsKnockBack"),
        MessagePackBinary.GetEncodedStringBytes("IsChanging"),
        MessagePackBinary.GetEncodedStringBytes("LastSyncTime"),
        MessagePackBinary.GetEncodedStringBytes("TempFlags"),
        MessagePackBinary.GetEncodedStringBytes("BadgeState")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      UnitData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 56);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<QuestClearUnlockUnitDataParam>().Serialize(ref bytes, offset, value.QuestUnlockedLeaderSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Serialize(ref bytes, offset, value.QuestUnlockedAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Serialize(ref bytes, offset, value.QuestUnlockedSkills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Serialize(ref bytes, offset, value.SkillUnlocks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsMyUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.UniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<UnitParam>().Serialize(ref bytes, offset, value.UnitParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UnitID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<BaseStatus>().Serialize(ref bytes, offset, value.Status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AwakeLv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<EquipData[]>().Serialize(ref bytes, offset, value.CurrentEquips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.LeaderSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.MasterAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.CollaboAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.MapEffectAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.TobiraMasterAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.CurrentAbilitySlots, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.LearnAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.BattleAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<List<SkillData>>().Serialize(ref bytes, offset, value.BattleSkills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.CurrentLeaderSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.Element, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<JobTypes>().Serialize(ref bytes, offset, value.JobType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.RoleType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardData[]>().Serialize(ref bytes, offset, value.ConceptCards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardData>().Serialize(ref bytes, offset, value.MainConceptCard, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<RuneData[]>().Serialize(ref bytes, offset, value.EquipRunes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsRiding);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.NumJobsAvailable);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<JobData>().Serialize(ref bytes, offset, value.CurrentJob, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CurrentJobId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUnlockTobira);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<List<TobiraData>>().Serialize(ref bytes, offset, value.TobiraData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.UnlockTobriaNum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<JobData[]>().Serialize(ref bytes, offset, value.Jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JobCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JobIndex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SexPrefix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsFavorite);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.SupportElement, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsRental);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RentalFavoritePoint);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.RentalIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNotFindUniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsIntoUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<QuestClearUnlockUnitDataParam[]>().Serialize(ref bytes, offset, value.UnlockedSkills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CharacterQuestRarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsThrow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsKnockBack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsChanging);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.LastSyncTime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<UnitData.TemporaryFlags>().Serialize(ref bytes, offset, value.TempFlags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<UnitBadgeTypes>().Serialize(ref bytes, offset, value.BadgeState, formatterResolver);
      return offset - num;
    }

    public UnitData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (UnitData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      QuestClearUnlockUnitDataParam unlockUnitDataParam = (QuestClearUnlockUnitDataParam) null;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList1 = (List<QuestClearUnlockUnitDataParam>) null;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList2 = (List<QuestClearUnlockUnitDataParam>) null;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList3 = (List<QuestClearUnlockUnitDataParam>) null;
      bool flag1 = false;
      long num3 = 0;
      UnitParam unitParam = (UnitParam) null;
      string str1 = (string) null;
      BaseStatus baseStatus = (BaseStatus) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      EquipData[] equipDataArray = (EquipData[]) null;
      SkillData skillData1 = (SkillData) null;
      AbilityData abilityData1 = (AbilityData) null;
      AbilityData abilityData2 = (AbilityData) null;
      AbilityData abilityData3 = (AbilityData) null;
      List<AbilityData> abilityDataList1 = (List<AbilityData>) null;
      long[] numArray = (long[]) null;
      List<AbilityData> abilityDataList2 = (List<AbilityData>) null;
      List<AbilityData> abilityDataList3 = (List<AbilityData>) null;
      List<SkillData> skillDataList = (List<SkillData>) null;
      SkillData skillData2 = (SkillData) null;
      EElement eelement1 = EElement.None;
      JobTypes jobTypes = JobTypes.None;
      RoleTypes roleTypes = RoleTypes.None;
      ConceptCardData[] conceptCardDataArray = (ConceptCardData[]) null;
      ConceptCardData conceptCardData = (ConceptCardData) null;
      RuneData[] runeDataArray = (RuneData[]) null;
      bool flag2 = false;
      int num8 = 0;
      JobData jobData = (JobData) null;
      string str2 = (string) null;
      bool flag3 = false;
      List<TobiraData> tobiraDataList = (List<TobiraData>) null;
      int num9 = 0;
      JobData[] jobDataArray = (JobData[]) null;
      int num10 = 0;
      int num11 = 0;
      string str3 = (string) null;
      bool flag4 = false;
      EElement eelement2 = EElement.None;
      bool flag5 = false;
      int num12 = 0;
      string str4 = (string) null;
      bool flag6 = false;
      bool flag7 = false;
      QuestClearUnlockUnitDataParam[] unlockUnitDataParamArray = (QuestClearUnlockUnitDataParam[]) null;
      int num13 = 0;
      bool flag8 = false;
      bool flag9 = false;
      bool flag10 = false;
      float num14 = 0.0f;
      UnitData.TemporaryFlags temporaryFlags = (UnitData.TemporaryFlags) 0;
      UnitBadgeTypes unitBadgeTypes = (UnitBadgeTypes) 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num15;
        if (!this.____keyMapping.TryGetValueSafe(key, out num15))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num15)
          {
            case 0:
              unlockUnitDataParam = formatterResolver.GetFormatterWithVerify<QuestClearUnlockUnitDataParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              unlockUnitDataParamList1 = formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              unlockUnitDataParamList2 = formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              unlockUnitDataParamList3 = formatterResolver.GetFormatterWithVerify<List<QuestClearUnlockUnitDataParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 6:
              unitParam = formatterResolver.GetFormatterWithVerify<UnitParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              baseStatus = formatterResolver.GetFormatterWithVerify<BaseStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              equipDataArray = formatterResolver.GetFormatterWithVerify<EquipData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              skillData1 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              abilityData1 = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              abilityData2 = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              abilityData3 = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              abilityDataList1 = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              numArray = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              abilityDataList2 = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              abilityDataList3 = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              skillDataList = formatterResolver.GetFormatterWithVerify<List<SkillData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              skillData2 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              eelement1 = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              jobTypes = formatterResolver.GetFormatterWithVerify<JobTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              conceptCardDataArray = formatterResolver.GetFormatterWithVerify<ConceptCardData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              conceptCardData = formatterResolver.GetFormatterWithVerify<ConceptCardData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              runeDataArray = formatterResolver.GetFormatterWithVerify<RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 31:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              jobData = formatterResolver.GetFormatterWithVerify<JobData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 35:
              tobiraDataList = formatterResolver.GetFormatterWithVerify<List<TobiraData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              jobDataArray = formatterResolver.GetFormatterWithVerify<JobData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 42:
              eelement2 = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 44:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 46:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 47:
              flag7 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 48:
              unlockUnitDataParamArray = formatterResolver.GetFormatterWithVerify<QuestClearUnlockUnitDataParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              flag8 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 51:
              flag9 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 52:
              flag10 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 53:
              num14 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 54:
              temporaryFlags = formatterResolver.GetFormatterWithVerify<UnitData.TemporaryFlags>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              unitBadgeTypes = formatterResolver.GetFormatterWithVerify<UnitBadgeTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new UnitData()
      {
        ConceptCards = conceptCardDataArray,
        EquipRunes = runeDataArray,
        IsFavorite = flag4,
        IsNotFindUniqueID = flag6,
        LastSyncTime = num14,
        TempFlags = temporaryFlags,
        BadgeState = unitBadgeTypes
      };
    }
  }
}
