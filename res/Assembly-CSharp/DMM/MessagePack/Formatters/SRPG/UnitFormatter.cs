// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.UnitFormatter
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
  public sealed class UnitFormatter : IMessagePackFormatter<Unit>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public UnitFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "AIActionIndex",
          0
        },
        {
          "AIActionTurnCount",
          1
        },
        {
          "AIPatrolIndex",
          2
        },
        {
          "IsNPC",
          3
        },
        {
          "IsRaidBoss",
          4
        },
        {
          "IsWithdrawDrop",
          5
        },
        {
          "Gems",
          6
        },
        {
          "WaitClock",
          7
        },
        {
          "WaitMoveTurn",
          8
        },
        {
          "JudgeHpLists",
          9
        },
        {
          "TurnCount",
          10
        },
        {
          "EntryUnit",
          11
        },
        {
          "UniqueName",
          12
        },
        {
          "UnitName",
          13
        },
        {
          "UnitData",
          14
        },
        {
          "UnitParam",
          15
        },
        {
          "UnitType",
          16
        },
        {
          "Lv",
          17
        },
        {
          "Job",
          18
        },
        {
          "LeaderSkill",
          19
        },
        {
          "BattleAbilitys",
          20
        },
        {
          "BattleSkills",
          21
        },
        {
          "BuffAttachments",
          22
        },
        {
          "CondAttachments",
          23
        },
        {
          "CurrentEquips",
          24
        },
        {
          "AI",
          25
        },
        {
          "AIForceSkill",
          26
        },
        {
          "MaximumStatus",
          27
        },
        {
          "MaximumStatusHp",
          28
        },
        {
          "CurrentStatus",
          29
        },
        {
          "UnitChangedHp",
          30
        },
        {
          "IsDead",
          31
        },
        {
          "IsEntry",
          32
        },
        {
          "NeedDead",
          33
        },
        {
          "IsControl",
          34
        },
        {
          "IsGimmick",
          35
        },
        {
          "IsIntoUnit",
          36
        },
        {
          "IsJump",
          37
        },
        {
          "Side",
          38
        },
        {
          "UnitFlag",
          39
        },
        {
          "Drop",
          40
        },
        {
          "Steal",
          41
        },
        {
          "Shields",
          42
        },
        {
          "Protects",
          43
        },
        {
          "Guards",
          44
        },
        {
          "ProtectDamageCount",
          45
        },
        {
          "MhmDamageLists",
          46
        },
        {
          "InspInsList",
          47
        },
        {
          "InspUseList",
          48
        },
        {
          "FtgtTargetList",
          49
        },
        {
          "FtgtFromList",
          50
        },
        {
          "FtgtFromActiveUnit",
          51
        },
        {
          "DisableMoveGridHeight",
          52
        },
        {
          "Element",
          53
        },
        {
          "JobType",
          54
        },
        {
          "RoleType",
          55
        },
        {
          "x",
          56
        },
        {
          "y",
          57
        },
        {
          "startX",
          58
        },
        {
          "startY",
          59
        },
        {
          "startDir",
          60
        },
        {
          "SettingNPC",
          61
        },
        {
          "SizeX",
          62
        },
        {
          "SizeY",
          63
        },
        {
          "SizeZ",
          64
        },
        {
          "OffsZ",
          65
        },
        {
          "MinColX",
          66
        },
        {
          "MaxColX",
          67
        },
        {
          "MinColY",
          68
        },
        {
          "MaxColY",
          69
        },
        {
          "EventTrigger",
          70
        },
        {
          "EntryTriggers",
          71
        },
        {
          "IsEntryTriggerAndCheck",
          72
        },
        {
          "ActionCount",
          73
        },
        {
          "DeathCount",
          74
        },
        {
          "AutoJewel",
          75
        },
        {
          "ChargeTime",
          76
        },
        {
          "ChargeTimeMax",
          77
        },
        {
          "CastSkill",
          78
        },
        {
          "CastTimeMax",
          79
        },
        {
          "CastTime",
          80
        },
        {
          "CastIndex",
          81
        },
        {
          "UnitTarget",
          82
        },
        {
          "GridTarget",
          83
        },
        {
          "CastSkillGridMap",
          84
        },
        {
          "RageTarget",
          85
        },
        {
          "UnitIndex",
          86
        },
        {
          "ParentUniqueName",
          87
        },
        {
          "NotifyUniqueNames",
          88
        },
        {
          "TowerStartHP",
          89
        },
        {
          "KillCount",
          90
        },
        {
          "IsBreakObj",
          91
        },
        {
          "BreakObjClashType",
          92
        },
        {
          "BreakObjAIType",
          93
        },
        {
          "BreakObjSideType",
          94
        },
        {
          "BreakObjRayType",
          95
        },
        {
          "IsBreakDispUI",
          96
        },
        {
          "CreateBreakObjId",
          97
        },
        {
          "CreateBreakObjClock",
          98
        },
        {
          "TeamId",
          99
        },
        {
          "FriendStates",
          100
        },
        {
          "KeepHp",
          101
        },
        {
          "InfinitySpawnTag",
          102
        },
        {
          "InfinitySpawnDeck",
          103
        },
        {
          "AbilityChangeLists",
          104
        },
        {
          "AddedAbilitys",
          105
        },
        {
          "DtuParam",
          106
        },
        {
          "DtuFromUnit",
          107
        },
        {
          "DtuRemainTurn",
          108
        },
        {
          "OverKillDamage",
          109
        },
        {
          "MovType",
          110
        },
        {
          "IsPassMovType",
          111
        },
        {
          "IsRiding",
          112
        },
        {
          "IsFlyingPass",
          113
        },
        {
          "ReqRevive",
          114
        },
        {
          "IsNormalSize",
          115
        },
        {
          "IsThrow",
          116
        },
        {
          "IsKnockBack",
          117
        },
        {
          "IsChanging",
          118
        },
        {
          "BossLongBaseMaxHP",
          119
        },
        {
          "BossLongMaxHP",
          120
        },
        {
          "BossLongCurHP",
          121
        },
        {
          "IsInitialized",
          122
        },
        {
          "IsPaused",
          123
        },
        {
          "Target",
          124
        },
        {
          "TreasureGainTarget",
          125
        },
        {
          "Direction",
          126
        },
        {
          "IsPartyMember",
          (int) sbyte.MaxValue
        },
        {
          "IsSub",
          128
        },
        {
          "mNotifyUniqueNames",
          129
        },
        {
          "OwnerPlayerIndex",
          130
        },
        {
          "CondLinkageBuff",
          131
        },
        {
          "CondLinkageDebuff",
          132
        }
      };
      this.____stringByteKeys = new byte[133][]
      {
        MessagePackBinary.GetEncodedStringBytes("AIActionIndex"),
        MessagePackBinary.GetEncodedStringBytes("AIActionTurnCount"),
        MessagePackBinary.GetEncodedStringBytes("AIPatrolIndex"),
        MessagePackBinary.GetEncodedStringBytes("IsNPC"),
        MessagePackBinary.GetEncodedStringBytes("IsRaidBoss"),
        MessagePackBinary.GetEncodedStringBytes("IsWithdrawDrop"),
        MessagePackBinary.GetEncodedStringBytes("Gems"),
        MessagePackBinary.GetEncodedStringBytes("WaitClock"),
        MessagePackBinary.GetEncodedStringBytes("WaitMoveTurn"),
        MessagePackBinary.GetEncodedStringBytes("JudgeHpLists"),
        MessagePackBinary.GetEncodedStringBytes("TurnCount"),
        MessagePackBinary.GetEncodedStringBytes("EntryUnit"),
        MessagePackBinary.GetEncodedStringBytes("UniqueName"),
        MessagePackBinary.GetEncodedStringBytes("UnitName"),
        MessagePackBinary.GetEncodedStringBytes("UnitData"),
        MessagePackBinary.GetEncodedStringBytes("UnitParam"),
        MessagePackBinary.GetEncodedStringBytes("UnitType"),
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("Job"),
        MessagePackBinary.GetEncodedStringBytes("LeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("BattleAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("BattleSkills"),
        MessagePackBinary.GetEncodedStringBytes("BuffAttachments"),
        MessagePackBinary.GetEncodedStringBytes("CondAttachments"),
        MessagePackBinary.GetEncodedStringBytes("CurrentEquips"),
        MessagePackBinary.GetEncodedStringBytes("AI"),
        MessagePackBinary.GetEncodedStringBytes("AIForceSkill"),
        MessagePackBinary.GetEncodedStringBytes("MaximumStatus"),
        MessagePackBinary.GetEncodedStringBytes("MaximumStatusHp"),
        MessagePackBinary.GetEncodedStringBytes("CurrentStatus"),
        MessagePackBinary.GetEncodedStringBytes("UnitChangedHp"),
        MessagePackBinary.GetEncodedStringBytes("IsDead"),
        MessagePackBinary.GetEncodedStringBytes("IsEntry"),
        MessagePackBinary.GetEncodedStringBytes("NeedDead"),
        MessagePackBinary.GetEncodedStringBytes("IsControl"),
        MessagePackBinary.GetEncodedStringBytes("IsGimmick"),
        MessagePackBinary.GetEncodedStringBytes("IsIntoUnit"),
        MessagePackBinary.GetEncodedStringBytes("IsJump"),
        MessagePackBinary.GetEncodedStringBytes("Side"),
        MessagePackBinary.GetEncodedStringBytes("UnitFlag"),
        MessagePackBinary.GetEncodedStringBytes("Drop"),
        MessagePackBinary.GetEncodedStringBytes("Steal"),
        MessagePackBinary.GetEncodedStringBytes("Shields"),
        MessagePackBinary.GetEncodedStringBytes("Protects"),
        MessagePackBinary.GetEncodedStringBytes("Guards"),
        MessagePackBinary.GetEncodedStringBytes("ProtectDamageCount"),
        MessagePackBinary.GetEncodedStringBytes("MhmDamageLists"),
        MessagePackBinary.GetEncodedStringBytes("InspInsList"),
        MessagePackBinary.GetEncodedStringBytes("InspUseList"),
        MessagePackBinary.GetEncodedStringBytes("FtgtTargetList"),
        MessagePackBinary.GetEncodedStringBytes("FtgtFromList"),
        MessagePackBinary.GetEncodedStringBytes("FtgtFromActiveUnit"),
        MessagePackBinary.GetEncodedStringBytes("DisableMoveGridHeight"),
        MessagePackBinary.GetEncodedStringBytes("Element"),
        MessagePackBinary.GetEncodedStringBytes("JobType"),
        MessagePackBinary.GetEncodedStringBytes("RoleType"),
        MessagePackBinary.GetEncodedStringBytes("x"),
        MessagePackBinary.GetEncodedStringBytes("y"),
        MessagePackBinary.GetEncodedStringBytes("startX"),
        MessagePackBinary.GetEncodedStringBytes("startY"),
        MessagePackBinary.GetEncodedStringBytes("startDir"),
        MessagePackBinary.GetEncodedStringBytes("SettingNPC"),
        MessagePackBinary.GetEncodedStringBytes("SizeX"),
        MessagePackBinary.GetEncodedStringBytes("SizeY"),
        MessagePackBinary.GetEncodedStringBytes("SizeZ"),
        MessagePackBinary.GetEncodedStringBytes("OffsZ"),
        MessagePackBinary.GetEncodedStringBytes("MinColX"),
        MessagePackBinary.GetEncodedStringBytes("MaxColX"),
        MessagePackBinary.GetEncodedStringBytes("MinColY"),
        MessagePackBinary.GetEncodedStringBytes("MaxColY"),
        MessagePackBinary.GetEncodedStringBytes("EventTrigger"),
        MessagePackBinary.GetEncodedStringBytes("EntryTriggers"),
        MessagePackBinary.GetEncodedStringBytes("IsEntryTriggerAndCheck"),
        MessagePackBinary.GetEncodedStringBytes("ActionCount"),
        MessagePackBinary.GetEncodedStringBytes("DeathCount"),
        MessagePackBinary.GetEncodedStringBytes("AutoJewel"),
        MessagePackBinary.GetEncodedStringBytes("ChargeTime"),
        MessagePackBinary.GetEncodedStringBytes("ChargeTimeMax"),
        MessagePackBinary.GetEncodedStringBytes("CastSkill"),
        MessagePackBinary.GetEncodedStringBytes("CastTimeMax"),
        MessagePackBinary.GetEncodedStringBytes("CastTime"),
        MessagePackBinary.GetEncodedStringBytes("CastIndex"),
        MessagePackBinary.GetEncodedStringBytes("UnitTarget"),
        MessagePackBinary.GetEncodedStringBytes("GridTarget"),
        MessagePackBinary.GetEncodedStringBytes("CastSkillGridMap"),
        MessagePackBinary.GetEncodedStringBytes("RageTarget"),
        MessagePackBinary.GetEncodedStringBytes("UnitIndex"),
        MessagePackBinary.GetEncodedStringBytes("ParentUniqueName"),
        MessagePackBinary.GetEncodedStringBytes("NotifyUniqueNames"),
        MessagePackBinary.GetEncodedStringBytes("TowerStartHP"),
        MessagePackBinary.GetEncodedStringBytes("KillCount"),
        MessagePackBinary.GetEncodedStringBytes("IsBreakObj"),
        MessagePackBinary.GetEncodedStringBytes("BreakObjClashType"),
        MessagePackBinary.GetEncodedStringBytes("BreakObjAIType"),
        MessagePackBinary.GetEncodedStringBytes("BreakObjSideType"),
        MessagePackBinary.GetEncodedStringBytes("BreakObjRayType"),
        MessagePackBinary.GetEncodedStringBytes("IsBreakDispUI"),
        MessagePackBinary.GetEncodedStringBytes("CreateBreakObjId"),
        MessagePackBinary.GetEncodedStringBytes("CreateBreakObjClock"),
        MessagePackBinary.GetEncodedStringBytes("TeamId"),
        MessagePackBinary.GetEncodedStringBytes("FriendStates"),
        MessagePackBinary.GetEncodedStringBytes("KeepHp"),
        MessagePackBinary.GetEncodedStringBytes("InfinitySpawnTag"),
        MessagePackBinary.GetEncodedStringBytes("InfinitySpawnDeck"),
        MessagePackBinary.GetEncodedStringBytes("AbilityChangeLists"),
        MessagePackBinary.GetEncodedStringBytes("AddedAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("DtuParam"),
        MessagePackBinary.GetEncodedStringBytes("DtuFromUnit"),
        MessagePackBinary.GetEncodedStringBytes("DtuRemainTurn"),
        MessagePackBinary.GetEncodedStringBytes("OverKillDamage"),
        MessagePackBinary.GetEncodedStringBytes("MovType"),
        MessagePackBinary.GetEncodedStringBytes("IsPassMovType"),
        MessagePackBinary.GetEncodedStringBytes("IsRiding"),
        MessagePackBinary.GetEncodedStringBytes("IsFlyingPass"),
        MessagePackBinary.GetEncodedStringBytes("ReqRevive"),
        MessagePackBinary.GetEncodedStringBytes("IsNormalSize"),
        MessagePackBinary.GetEncodedStringBytes("IsThrow"),
        MessagePackBinary.GetEncodedStringBytes("IsKnockBack"),
        MessagePackBinary.GetEncodedStringBytes("IsChanging"),
        MessagePackBinary.GetEncodedStringBytes("BossLongBaseMaxHP"),
        MessagePackBinary.GetEncodedStringBytes("BossLongMaxHP"),
        MessagePackBinary.GetEncodedStringBytes("BossLongCurHP"),
        MessagePackBinary.GetEncodedStringBytes("IsInitialized"),
        MessagePackBinary.GetEncodedStringBytes("IsPaused"),
        MessagePackBinary.GetEncodedStringBytes("Target"),
        MessagePackBinary.GetEncodedStringBytes("TreasureGainTarget"),
        MessagePackBinary.GetEncodedStringBytes("Direction"),
        MessagePackBinary.GetEncodedStringBytes("IsPartyMember"),
        MessagePackBinary.GetEncodedStringBytes("IsSub"),
        MessagePackBinary.GetEncodedStringBytes("mNotifyUniqueNames"),
        MessagePackBinary.GetEncodedStringBytes("OwnerPlayerIndex"),
        MessagePackBinary.GetEncodedStringBytes("CondLinkageBuff"),
        MessagePackBinary.GetEncodedStringBytes("CondLinkageDebuff")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 133);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.AIActionIndex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.AIActionTurnCount, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.AIPatrolIndex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNPC);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsRaidBoss);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsWithdrawDrop);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.WaitClock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.WaitMoveTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<List<SkillData>>().Serialize(ref bytes, offset, value.JudgeHpLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TurnCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.EntryUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UniqueName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UnitName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<UnitData>().Serialize(ref bytes, offset, value.UnitData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<UnitParam>().Serialize(ref bytes, offset, value.UnitParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitType>().Serialize(ref bytes, offset, value.UnitType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<JobData>().Serialize(ref bytes, offset, value.Job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.LeaderSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.BattleAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<List<SkillData>>().Serialize(ref bytes, offset, value.BattleSkills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<List<BuffAttachment>>().Serialize(ref bytes, offset, value.BuffAttachments, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<List<CondAttachment>>().Serialize(ref bytes, offset, value.CondAttachments, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<EquipData[]>().Serialize(ref bytes, offset, value.CurrentEquips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<AIParam>().Serialize(ref bytes, offset, value.AI, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.AIForceSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<BaseStatus>().Serialize(ref bytes, offset, value.MaximumStatus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaximumStatusHp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<BaseStatus>().Serialize(ref bytes, offset, value.CurrentStatus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.UnitChangedHp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDead);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsEntry);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.NeedDead);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsControl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsGimmick);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsIntoUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsJump);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitSide>().Serialize(ref bytes, offset, value.Side, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.UnitFlag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<Unit.UnitDrop>().Serialize(ref bytes, offset, value.Drop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<Unit.UnitSteal>().Serialize(ref bytes, offset, value.Steal, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitShield>>().Serialize(ref bytes, offset, value.Shields, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitProtect>>().Serialize(ref bytes, offset, value.Protects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitProtect>>().Serialize(ref bytes, offset, value.Guards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ProtectDamageCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitMhmDamage>>().Serialize(ref bytes, offset, value.MhmDamageLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitInsp>>().Serialize(ref bytes, offset, value.InspInsList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitInsp>>().Serialize(ref bytes, offset, value.InspUseList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitForcedTargeting>>().Serialize(ref bytes, offset, value.FtgtTargetList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.UnitForcedTargeting>>().Serialize(ref bytes, offset, value.FtgtFromList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.FtgtFromActiveUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DisableMoveGridHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.Element, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<JobTypes>().Serialize(ref bytes, offset, value.JobType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.RoleType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.x);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.y);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.startX);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.startY);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitDirection>().Serialize(ref bytes, offset, value.startDir, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += formatterResolver.GetFormatterWithVerify<NPCSetting>().Serialize(ref bytes, offset, value.SettingNPC, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SizeX);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SizeY);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SizeZ);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.OffsZ);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MinColX);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaxColX);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MinColY);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaxColY);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += formatterResolver.GetFormatterWithVerify<EventTrigger>().Serialize(ref bytes, offset, value.EventTrigger, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<List<UnitEntryTrigger>>().Serialize(ref bytes, offset, value.EntryTriggers, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.IsEntryTriggerAndCheck, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ActionCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DeathCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AutoJewel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ChargeTime, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ChargeTimeMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.CastSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.CastTimeMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.CastTime, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.CastIndex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.UnitTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += formatterResolver.GetFormatterWithVerify<Grid>().Serialize(ref bytes, offset, value.GridTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += formatterResolver.GetFormatterWithVerify<GridMap<bool>>().Serialize(ref bytes, offset, value.CastSkillGridMap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.RageTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitIndex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ParentUniqueName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += formatterResolver.GetFormatterWithVerify<List<OString>>().Serialize(ref bytes, offset, value.NotifyUniqueNames, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TowerStartHP);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.KillCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsBreakObj);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += formatterResolver.GetFormatterWithVerify<eMapBreakClashType>().Serialize(ref bytes, offset, value.BreakObjClashType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += formatterResolver.GetFormatterWithVerify<eMapBreakAIType>().Serialize(ref bytes, offset, value.BreakObjAIType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += formatterResolver.GetFormatterWithVerify<eMapBreakSideType>().Serialize(ref bytes, offset, value.BreakObjSideType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += formatterResolver.GetFormatterWithVerify<eMapBreakRayType>().Serialize(ref bytes, offset, value.BreakObjRayType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsBreakDispUI);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CreateBreakObjId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CreateBreakObjClock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TeamId);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += formatterResolver.GetFormatterWithVerify<FriendStates>().Serialize(ref bytes, offset, value.FriendStates, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.KeepHp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.InfinitySpawnTag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.InfinitySpawnDeck);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.AbilityChange>>().Serialize(ref bytes, offset, value.AbilityChangeLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.AddedAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += formatterResolver.GetFormatterWithVerify<DynamicTransformUnitParam>().Serialize(ref bytes, offset, value.DtuParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.DtuFromUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DtuRemainTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.OverKillDamage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += formatterResolver.GetFormatterWithVerify<eMovType>().Serialize(ref bytes, offset, value.MovType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsPassMovType);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsRiding);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsFlyingPass);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.ReqRevive);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNormalSize);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsThrow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsKnockBack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsChanging);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.BossLongBaseMaxHP);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.BossLongMaxHP);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.BossLongCurHP);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsInitialized);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsPaused);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.Target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += formatterResolver.GetFormatterWithVerify<Grid>().Serialize(ref bytes, offset, value.TreasureGainTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitDirection>().Serialize(ref bytes, offset, value.Direction, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsPartyMember);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[128]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsSub);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[129]);
      offset += formatterResolver.GetFormatterWithVerify<List<OString>>().Serialize(ref bytes, offset, value.mNotifyUniqueNames, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[130]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.OwnerPlayerIndex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[131]);
      offset += formatterResolver.GetFormatterWithVerify<BuffBit>().Serialize(ref bytes, offset, value.CondLinkageBuff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[132]);
      offset += formatterResolver.GetFormatterWithVerify<BuffBit>().Serialize(ref bytes, offset, value.CondLinkageDebuff, formatterResolver);
      return offset - num;
    }

    public Unit Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      List<SkillData> skillDataList1 = (List<SkillData>) null;
      int num6 = 0;
      bool flag4 = false;
      string str1 = (string) null;
      string str2 = (string) null;
      UnitData unitData = (UnitData) null;
      UnitParam unitParam = (UnitParam) null;
      EUnitType eunitType = EUnitType.Unit;
      int num7 = 0;
      JobData jobData = (JobData) null;
      SkillData skillData1 = (SkillData) null;
      List<AbilityData> abilityDataList1 = (List<AbilityData>) null;
      List<SkillData> skillDataList2 = (List<SkillData>) null;
      List<BuffAttachment> buffAttachmentList = (List<BuffAttachment>) null;
      List<CondAttachment> condAttachmentList = (List<CondAttachment>) null;
      EquipData[] equipDataArray = (EquipData[]) null;
      AIParam aiParam = (AIParam) null;
      SkillData skillData2 = (SkillData) null;
      BaseStatus baseStatus1 = (BaseStatus) null;
      int num8 = 0;
      BaseStatus baseStatus2 = (BaseStatus) null;
      int num9 = 0;
      bool flag5 = false;
      bool flag6 = false;
      bool flag7 = false;
      bool flag8 = false;
      bool flag9 = false;
      bool flag10 = false;
      bool flag11 = false;
      EUnitSide eunitSide = EUnitSide.Player;
      int num10 = 0;
      Unit.UnitDrop unitDrop = (Unit.UnitDrop) null;
      Unit.UnitSteal unitSteal = (Unit.UnitSteal) null;
      List<Unit.UnitShield> unitShieldList = (List<Unit.UnitShield>) null;
      List<Unit.UnitProtect> unitProtectList1 = (List<Unit.UnitProtect>) null;
      List<Unit.UnitProtect> unitProtectList2 = (List<Unit.UnitProtect>) null;
      int num11 = 0;
      List<Unit.UnitMhmDamage> unitMhmDamageList = (List<Unit.UnitMhmDamage>) null;
      List<Unit.UnitInsp> unitInspList1 = (List<Unit.UnitInsp>) null;
      List<Unit.UnitInsp> unitInspList2 = (List<Unit.UnitInsp>) null;
      List<Unit.UnitForcedTargeting> unitForcedTargetingList1 = (List<Unit.UnitForcedTargeting>) null;
      List<Unit.UnitForcedTargeting> unitForcedTargetingList2 = (List<Unit.UnitForcedTargeting>) null;
      Unit unit1 = (Unit) null;
      int num12 = 0;
      EElement eelement = EElement.None;
      JobTypes jobTypes = JobTypes.None;
      RoleTypes roleTypes = RoleTypes.None;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      EUnitDirection eunitDirection1 = EUnitDirection.PositiveX;
      NPCSetting npcSetting = (NPCSetting) null;
      int num17 = 0;
      int num18 = 0;
      int num19 = 0;
      float num20 = 0.0f;
      int num21 = 0;
      int num22 = 0;
      int num23 = 0;
      int num24 = 0;
      EventTrigger eventTrigger = (EventTrigger) null;
      List<UnitEntryTrigger> unitEntryTriggerList = (List<UnitEntryTrigger>) null;
      OBool obool = new OBool();
      int num25 = 0;
      int num26 = 0;
      int num27 = 0;
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      SkillData skillData3 = (SkillData) null;
      OInt oint6 = new OInt();
      OInt oint7 = new OInt();
      OInt oint8 = new OInt();
      Unit unit2 = (Unit) null;
      Grid grid1 = (Grid) null;
      GridMap<bool> gridMap = (GridMap<bool>) null;
      Unit unit3 = (Unit) null;
      OInt oint9 = new OInt();
      string str3 = (string) null;
      List<OString> ostringList1 = (List<OString>) null;
      int num28 = 0;
      int num29 = 0;
      bool flag12 = false;
      eMapBreakClashType mapBreakClashType = eMapBreakClashType.ALL;
      eMapBreakAIType eMapBreakAiType = eMapBreakAIType.NONE;
      eMapBreakSideType mapBreakSideType = eMapBreakSideType.UNKNOWN;
      eMapBreakRayType eMapBreakRayType = eMapBreakRayType.PASS;
      bool flag13 = false;
      string str4 = (string) null;
      int num30 = 0;
      int num31 = 0;
      FriendStates friendStates = FriendStates.None;
      OInt oint10 = new OInt();
      int num32 = 0;
      int num33 = 0;
      List<Unit.AbilityChange> abilityChangeList = (List<Unit.AbilityChange>) null;
      List<AbilityData> abilityDataList2 = (List<AbilityData>) null;
      DynamicTransformUnitParam transformUnitParam = (DynamicTransformUnitParam) null;
      Unit unit4 = (Unit) null;
      int num34 = 0;
      int num35 = 0;
      eMovType eMovType = eMovType.Normal;
      bool flag14 = false;
      bool flag15 = false;
      bool flag16 = false;
      bool flag17 = false;
      bool flag18 = false;
      bool flag19 = false;
      bool flag20 = false;
      bool flag21 = false;
      long num36 = 0;
      long num37 = 0;
      long num38 = 0;
      bool flag22 = false;
      bool flag23 = false;
      Unit unit5 = (Unit) null;
      Grid grid2 = (Grid) null;
      EUnitDirection eunitDirection2 = EUnitDirection.PositiveX;
      bool flag24 = false;
      bool flag25 = false;
      List<OString> ostringList2 = (List<OString>) null;
      int num39 = 0;
      BuffBit buffBit1 = (BuffBit) null;
      BuffBit buffBit2 = (BuffBit) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num40;
        if (!this.____keyMapping.TryGetValueSafe(key, out num40))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num40)
          {
            case 0:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              skillDataList1 = formatterResolver.GetFormatterWithVerify<List<SkillData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              unitData = formatterResolver.GetFormatterWithVerify<UnitData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              unitParam = formatterResolver.GetFormatterWithVerify<UnitParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              eunitType = formatterResolver.GetFormatterWithVerify<EUnitType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              jobData = formatterResolver.GetFormatterWithVerify<JobData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              skillData1 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              abilityDataList1 = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              skillDataList2 = formatterResolver.GetFormatterWithVerify<List<SkillData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              buffAttachmentList = formatterResolver.GetFormatterWithVerify<List<BuffAttachment>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              condAttachmentList = formatterResolver.GetFormatterWithVerify<List<CondAttachment>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              equipDataArray = formatterResolver.GetFormatterWithVerify<EquipData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              aiParam = formatterResolver.GetFormatterWithVerify<AIParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              skillData2 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              baseStatus1 = formatterResolver.GetFormatterWithVerify<BaseStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              baseStatus2 = formatterResolver.GetFormatterWithVerify<BaseStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 32:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 33:
              flag7 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 34:
              flag8 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 35:
              flag9 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 36:
              flag10 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 37:
              flag11 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 38:
              eunitSide = formatterResolver.GetFormatterWithVerify<EUnitSide>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              unitDrop = formatterResolver.GetFormatterWithVerify<Unit.UnitDrop>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              unitSteal = formatterResolver.GetFormatterWithVerify<Unit.UnitSteal>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              unitShieldList = formatterResolver.GetFormatterWithVerify<List<Unit.UnitShield>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              unitProtectList1 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitProtect>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 44:
              unitProtectList2 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitProtect>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 45:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              unitMhmDamageList = formatterResolver.GetFormatterWithVerify<List<Unit.UnitMhmDamage>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              unitInspList1 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitInsp>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              unitInspList2 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitInsp>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              unitForcedTargetingList1 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitForcedTargeting>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              unitForcedTargetingList2 = formatterResolver.GetFormatterWithVerify<List<Unit.UnitForcedTargeting>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              unit1 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 52:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              jobTypes = formatterResolver.GetFormatterWithVerify<JobTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              eunitDirection1 = formatterResolver.GetFormatterWithVerify<EUnitDirection>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 61:
              npcSetting = formatterResolver.GetFormatterWithVerify<NPCSetting>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 62:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 65:
              num20 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 66:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 67:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 68:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 69:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 70:
              eventTrigger = formatterResolver.GetFormatterWithVerify<EventTrigger>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 71:
              unitEntryTriggerList = formatterResolver.GetFormatterWithVerify<List<UnitEntryTrigger>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 74:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 75:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 76:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 77:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 78:
              skillData3 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 80:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 81:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 82:
              unit2 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 83:
              grid1 = formatterResolver.GetFormatterWithVerify<Grid>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 84:
              gridMap = formatterResolver.GetFormatterWithVerify<GridMap<bool>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 85:
              unit3 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 86:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 87:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 88:
              ostringList1 = formatterResolver.GetFormatterWithVerify<List<OString>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 89:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 90:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 91:
              flag12 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 92:
              mapBreakClashType = formatterResolver.GetFormatterWithVerify<eMapBreakClashType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 93:
              eMapBreakAiType = formatterResolver.GetFormatterWithVerify<eMapBreakAIType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 94:
              mapBreakSideType = formatterResolver.GetFormatterWithVerify<eMapBreakSideType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 95:
              eMapBreakRayType = formatterResolver.GetFormatterWithVerify<eMapBreakRayType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 96:
              flag13 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 97:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 98:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 99:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 100:
              friendStates = formatterResolver.GetFormatterWithVerify<FriendStates>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 101:
              oint10 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 102:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 103:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 104:
              abilityChangeList = formatterResolver.GetFormatterWithVerify<List<Unit.AbilityChange>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 105:
              abilityDataList2 = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 106:
              transformUnitParam = formatterResolver.GetFormatterWithVerify<DynamicTransformUnitParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 107:
              unit4 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 108:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 109:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 110:
              eMovType = formatterResolver.GetFormatterWithVerify<eMovType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 111:
              flag14 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 112:
              flag15 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 113:
              flag16 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 114:
              flag17 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 115:
              flag18 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 116:
              flag19 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 117:
              flag20 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 118:
              flag21 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 119:
              num36 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 120:
              num37 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 121:
              num38 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 122:
              flag22 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 123:
              flag23 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 124:
              unit5 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 125:
              grid2 = formatterResolver.GetFormatterWithVerify<Grid>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 126:
              eunitDirection2 = formatterResolver.GetFormatterWithVerify<EUnitDirection>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case (int) sbyte.MaxValue:
              flag24 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 128:
              flag25 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 129:
              ostringList2 = formatterResolver.GetFormatterWithVerify<List<OString>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 130:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 131:
              buffBit1 = formatterResolver.GetFormatterWithVerify<BuffBit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 132:
              buffBit2 = formatterResolver.GetFormatterWithVerify<BuffBit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Unit unit6 = new Unit();
      unit6.Gems = num3;
      unit6.WaitClock = num4;
      unit6.WaitMoveTurn = num5;
      unit6.TurnCount = num6;
      unit6.UnitName = str2;
      unit6.UnitChangedHp = num9;
      unit6.Side = eunitSide;
      unit6.UnitFlag = num10;
      unit6.ProtectDamageCount = num11;
      unit6.x = num13;
      unit6.y = num14;
      unit6.startX = num15;
      unit6.startY = num16;
      unit6.startDir = eunitDirection1;
      unit6.ChargeTime = oint4;
      unit6.CastTime = oint7;
      unit6.UnitTarget = unit2;
      unit6.CastSkillGridMap = gridMap;
      unit6.TowerStartHP = num28;
      unit6.KillCount = num29;
      unit6.TeamId = num31;
      unit6.FriendStates = friendStates;
      unit6.KeepHp = oint10;
      unit6.InfinitySpawnTag = num32;
      unit6.InfinitySpawnDeck = num33;
      unit6.DtuParam = transformUnitParam;
      unit6.DtuFromUnit = unit4;
      unit6.DtuRemainTurn = num34;
      unit6.ReqRevive = flag17;
      unit6.IsInitialized = flag22;
      unit6.IsPaused = flag23;
      unit6.Target = unit5;
      unit6.TreasureGainTarget = grid2;
      unit6.Direction = eunitDirection2;
      unit6.IsPartyMember = flag24;
      unit6.IsSub = flag25;
      unit6.mNotifyUniqueNames = ostringList2;
      unit6.OwnerPlayerIndex = num39;
      unit6.CondLinkageBuff = buffBit1;
      unit6.CondLinkageDebuff = buffBit2;
      return unit6;
    }
  }
}
