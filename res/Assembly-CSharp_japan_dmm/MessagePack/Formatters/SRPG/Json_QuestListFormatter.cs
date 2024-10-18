// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_QuestListFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_QuestListFormatter : 
    IMessagePackFormatter<Json_QuestList>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_QuestListFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "worlds",
          0
        },
        {
          "archives",
          1
        },
        {
          "areas",
          2
        },
        {
          "quests",
          3
        },
        {
          "objectives",
          4
        },
        {
          "towerObjectives",
          5
        },
        {
          "magnifications",
          6
        },
        {
          "conditions",
          7
        },
        {
          "parties",
          8
        },
        {
          "CampaignParents",
          9
        },
        {
          "CampaignChildren",
          10
        },
        {
          "CampaignTrust",
          11
        },
        {
          "CampaignInspSkill",
          12
        },
        {
          "towerFloors",
          13
        },
        {
          "towerRewards",
          14
        },
        {
          "towerRoundRewards",
          15
        },
        {
          "towers",
          16
        },
        {
          "versusTowerFloor",
          17
        },
        {
          "versusschedule",
          18
        },
        {
          "versuscoin",
          19
        },
        {
          "multitowerFloor",
          20
        },
        {
          "multitowerRewards",
          21
        },
        {
          "MapEffect",
          22
        },
        {
          "WeatherSet",
          23
        },
        {
          "rankingQuests",
          24
        },
        {
          "rankingQuestSchedule",
          25
        },
        {
          "rankingQuestRewards",
          26
        },
        {
          "versusfirstwinbonus",
          27
        },
        {
          "versusstreakwinschedule",
          28
        },
        {
          "versusstreakwinbonus",
          29
        },
        {
          "versusrule",
          30
        },
        {
          "versuscoincamp",
          31
        },
        {
          "versusenabletime",
          32
        },
        {
          "VersusRank",
          33
        },
        {
          "VersusRankClass",
          34
        },
        {
          "VersusRankRankingReward",
          35
        },
        {
          "VersusRankReward",
          36
        },
        {
          "VersusRankMissionSchedule",
          37
        },
        {
          "VersusRankMission",
          38
        },
        {
          "questLobbyNews",
          39
        },
        {
          "GuerrillaShopAdventQuest",
          40
        },
        {
          "GuerrillaShopSchedule",
          41
        },
        {
          "VersusDraftDeck",
          42
        },
        {
          "VersusDraftUnit",
          43
        },
        {
          "GenesisStar",
          44
        },
        {
          "GenesisChapter",
          45
        },
        {
          "GenesisReward",
          46
        },
        {
          "GenesisLapBoss",
          47
        },
        {
          "AdvanceStar",
          48
        },
        {
          "AdvanceEvent",
          49
        },
        {
          "AdvanceReward",
          50
        },
        {
          "AdvanceLapBoss",
          51
        },
        {
          "GuildRaidBoss",
          52
        },
        {
          "GuildRaidCoolDays",
          53
        },
        {
          "GuildRaidScore",
          54
        },
        {
          "GuildRaidPeriod",
          55
        },
        {
          "GuildRaidReward",
          56
        },
        {
          "GuildRaidRewardDmgRanking",
          57
        },
        {
          "GuildRaidRewardDmgRatio",
          58
        },
        {
          "GuildRaidRewardRound",
          59
        },
        {
          "GuildRaidRewardRanking",
          60
        },
        {
          "GvGPeriod",
          61
        },
        {
          "GvGNode",
          62
        },
        {
          "GvGNPCParty",
          63
        },
        {
          "GvGNPCUnit",
          64
        },
        {
          "GvGRewardRanking",
          65
        },
        {
          "GvGReward",
          66
        },
        {
          "GvGRule",
          67
        },
        {
          "GvGNodeReward",
          68
        },
        {
          "GvGLeague",
          69
        },
        {
          "GvGCalcRateSetting",
          70
        },
        {
          "WorldRaid",
          71
        },
        {
          "WorldRaidBoss",
          72
        },
        {
          "WorldRaidReward",
          73
        },
        {
          "WorldRaidDamageReward",
          74
        },
        {
          "WorldRaidDamageLottery",
          75
        },
        {
          "WorldRaidRankingReward",
          76
        }
      };
      this.____stringByteKeys = new byte[77][]
      {
        MessagePackBinary.GetEncodedStringBytes("worlds"),
        MessagePackBinary.GetEncodedStringBytes("archives"),
        MessagePackBinary.GetEncodedStringBytes("areas"),
        MessagePackBinary.GetEncodedStringBytes("quests"),
        MessagePackBinary.GetEncodedStringBytes("objectives"),
        MessagePackBinary.GetEncodedStringBytes("towerObjectives"),
        MessagePackBinary.GetEncodedStringBytes("magnifications"),
        MessagePackBinary.GetEncodedStringBytes("conditions"),
        MessagePackBinary.GetEncodedStringBytes("parties"),
        MessagePackBinary.GetEncodedStringBytes("CampaignParents"),
        MessagePackBinary.GetEncodedStringBytes("CampaignChildren"),
        MessagePackBinary.GetEncodedStringBytes("CampaignTrust"),
        MessagePackBinary.GetEncodedStringBytes("CampaignInspSkill"),
        MessagePackBinary.GetEncodedStringBytes("towerFloors"),
        MessagePackBinary.GetEncodedStringBytes("towerRewards"),
        MessagePackBinary.GetEncodedStringBytes("towerRoundRewards"),
        MessagePackBinary.GetEncodedStringBytes("towers"),
        MessagePackBinary.GetEncodedStringBytes("versusTowerFloor"),
        MessagePackBinary.GetEncodedStringBytes("versusschedule"),
        MessagePackBinary.GetEncodedStringBytes("versuscoin"),
        MessagePackBinary.GetEncodedStringBytes("multitowerFloor"),
        MessagePackBinary.GetEncodedStringBytes("multitowerRewards"),
        MessagePackBinary.GetEncodedStringBytes("MapEffect"),
        MessagePackBinary.GetEncodedStringBytes("WeatherSet"),
        MessagePackBinary.GetEncodedStringBytes("rankingQuests"),
        MessagePackBinary.GetEncodedStringBytes("rankingQuestSchedule"),
        MessagePackBinary.GetEncodedStringBytes("rankingQuestRewards"),
        MessagePackBinary.GetEncodedStringBytes("versusfirstwinbonus"),
        MessagePackBinary.GetEncodedStringBytes("versusstreakwinschedule"),
        MessagePackBinary.GetEncodedStringBytes("versusstreakwinbonus"),
        MessagePackBinary.GetEncodedStringBytes("versusrule"),
        MessagePackBinary.GetEncodedStringBytes("versuscoincamp"),
        MessagePackBinary.GetEncodedStringBytes("versusenabletime"),
        MessagePackBinary.GetEncodedStringBytes("VersusRank"),
        MessagePackBinary.GetEncodedStringBytes("VersusRankClass"),
        MessagePackBinary.GetEncodedStringBytes("VersusRankRankingReward"),
        MessagePackBinary.GetEncodedStringBytes("VersusRankReward"),
        MessagePackBinary.GetEncodedStringBytes("VersusRankMissionSchedule"),
        MessagePackBinary.GetEncodedStringBytes("VersusRankMission"),
        MessagePackBinary.GetEncodedStringBytes("questLobbyNews"),
        MessagePackBinary.GetEncodedStringBytes("GuerrillaShopAdventQuest"),
        MessagePackBinary.GetEncodedStringBytes("GuerrillaShopSchedule"),
        MessagePackBinary.GetEncodedStringBytes("VersusDraftDeck"),
        MessagePackBinary.GetEncodedStringBytes("VersusDraftUnit"),
        MessagePackBinary.GetEncodedStringBytes("GenesisStar"),
        MessagePackBinary.GetEncodedStringBytes("GenesisChapter"),
        MessagePackBinary.GetEncodedStringBytes("GenesisReward"),
        MessagePackBinary.GetEncodedStringBytes("GenesisLapBoss"),
        MessagePackBinary.GetEncodedStringBytes("AdvanceStar"),
        MessagePackBinary.GetEncodedStringBytes("AdvanceEvent"),
        MessagePackBinary.GetEncodedStringBytes("AdvanceReward"),
        MessagePackBinary.GetEncodedStringBytes("AdvanceLapBoss"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidBoss"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidCoolDays"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidScore"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidPeriod"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidReward"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidRewardDmgRanking"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidRewardDmgRatio"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidRewardRound"),
        MessagePackBinary.GetEncodedStringBytes("GuildRaidRewardRanking"),
        MessagePackBinary.GetEncodedStringBytes("GvGPeriod"),
        MessagePackBinary.GetEncodedStringBytes("GvGNode"),
        MessagePackBinary.GetEncodedStringBytes("GvGNPCParty"),
        MessagePackBinary.GetEncodedStringBytes("GvGNPCUnit"),
        MessagePackBinary.GetEncodedStringBytes("GvGRewardRanking"),
        MessagePackBinary.GetEncodedStringBytes("GvGReward"),
        MessagePackBinary.GetEncodedStringBytes("GvGRule"),
        MessagePackBinary.GetEncodedStringBytes("GvGNodeReward"),
        MessagePackBinary.GetEncodedStringBytes("GvGLeague"),
        MessagePackBinary.GetEncodedStringBytes("GvGCalcRateSetting"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaid"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaidBoss"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaidReward"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaidDamageReward"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaidDamageLottery"),
        MessagePackBinary.GetEncodedStringBytes("WorldRaidRankingReward")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_QuestList value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 77);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SectionParam[]>().Serialize(ref bytes, offset, value.worlds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ArchiveParam[]>().Serialize(ref bytes, offset, value.archives, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ChapterParam[]>().Serialize(ref bytes, offset, value.areas, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestParam[]>().Serialize(ref bytes, offset, value.quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ObjectiveParam[]>().Serialize(ref bytes, offset, value.objectives, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ObjectiveParam[]>().Serialize(ref bytes, offset, value.towerObjectives, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MagnificationParam[]>().Serialize(ref bytes, offset, value.magnifications, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCondParam[]>().Serialize(ref bytes, offset, value.conditions, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestPartyParam[]>().Serialize(ref bytes, offset, value.parties, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignParentParam[]>().Serialize(ref bytes, offset, value.CampaignParents, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignChildParam[]>().Serialize(ref bytes, offset, value.CampaignChildren, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignTrust[]>().Serialize(ref bytes, offset, value.CampaignTrust, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignInspSkill[]>().Serialize(ref bytes, offset, value.CampaignInspSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TowerFloorParam[]>().Serialize(ref bytes, offset, value.towerFloors, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TowerRewardParam[]>().Serialize(ref bytes, offset, value.towerRewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TowerRoundRewardParam[]>().Serialize(ref bytes, offset, value.towerRoundRewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TowerParam[]>().Serialize(ref bytes, offset, value.towers, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusTowerParam[]>().Serialize(ref bytes, offset, value.versusTowerFloor, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusSchedule[]>().Serialize(ref bytes, offset, value.versusschedule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusCoin[]>().Serialize(ref bytes, offset, value.versuscoin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MultiTowerFloorParam[]>().Serialize(ref bytes, offset, value.multitowerFloor, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MultiTowerRewardParam[]>().Serialize(ref bytes, offset, value.multitowerRewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MapEffectParam[]>().Serialize(ref bytes, offset, value.MapEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WeatherSetParam[]>().Serialize(ref bytes, offset, value.WeatherSet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RankingQuestParam[]>().Serialize(ref bytes, offset, value.rankingQuests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RankingQuestScheduleParam[]>().Serialize(ref bytes, offset, value.rankingQuestSchedule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RankingQuestRewardParam[]>().Serialize(ref bytes, offset, value.rankingQuestRewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusFirstWinBonus[]>().Serialize(ref bytes, offset, value.versusfirstwinbonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusStreakWinSchedule[]>().Serialize(ref bytes, offset, value.versusstreakwinschedule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusStreakWinBonus[]>().Serialize(ref bytes, offset, value.versusstreakwinbonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRule[]>().Serialize(ref bytes, offset, value.versusrule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusCoinCampParam[]>().Serialize(ref bytes, offset, value.versuscoincamp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusEnableTimeParam[]>().Serialize(ref bytes, offset, value.versusenabletime, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankParam[]>().Serialize(ref bytes, offset, value.VersusRank, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankClassParam[]>().Serialize(ref bytes, offset, value.VersusRankClass, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankRankingRewardParam[]>().Serialize(ref bytes, offset, value.VersusRankRankingReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankRewardParam[]>().Serialize(ref bytes, offset, value.VersusRankReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankMissionScheduleParam[]>().Serialize(ref bytes, offset, value.VersusRankMissionSchedule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusRankMissionParam[]>().Serialize(ref bytes, offset, value.VersusRankMission, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestLobbyNewsParam[]>().Serialize(ref bytes, offset, value.questLobbyNews, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuerrillaShopAdventQuestParam[]>().Serialize(ref bytes, offset, value.GuerrillaShopAdventQuest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuerrillaShopScheduleParam[]>().Serialize(ref bytes, offset, value.GuerrillaShopSchedule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusDraftDeckParam[]>().Serialize(ref bytes, offset, value.VersusDraftDeck, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusDraftUnitParam[]>().Serialize(ref bytes, offset, value.VersusDraftUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GenesisStarParam[]>().Serialize(ref bytes, offset, value.GenesisStar, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GenesisChapterParam[]>().Serialize(ref bytes, offset, value.GenesisChapter, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GenesisRewardParam[]>().Serialize(ref bytes, offset, value.GenesisReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GenesisLapBossParam[]>().Serialize(ref bytes, offset, value.GenesisLapBoss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AdvanceStarParam[]>().Serialize(ref bytes, offset, value.AdvanceStar, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AdvanceEventParam[]>().Serialize(ref bytes, offset, value.AdvanceEvent, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AdvanceRewardParam[]>().Serialize(ref bytes, offset, value.AdvanceReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AdvanceLapBossParam[]>().Serialize(ref bytes, offset, value.AdvanceLapBoss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossParam[]>().Serialize(ref bytes, offset, value.GuildRaidBoss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCoolDaysParam[]>().Serialize(ref bytes, offset, value.GuildRaidCoolDays, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidScoreParam[]>().Serialize(ref bytes, offset, value.GuildRaidScore, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPeriodParam[]>().Serialize(ref bytes, offset, value.GuildRaidPeriod, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardParam[]>().Serialize(ref bytes, offset, value.GuildRaidReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardDmgRankingParam[]>().Serialize(ref bytes, offset, value.GuildRaidRewardDmgRanking, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardDmgRatioParam[]>().Serialize(ref bytes, offset, value.GuildRaidRewardDmgRatio, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardRoundParam[]>().Serialize(ref bytes, offset, value.GuildRaidRewardRound, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardRankingParam[]>().Serialize(ref bytes, offset, value.GuildRaidRewardRanking, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGPeriodParam[]>().Serialize(ref bytes, offset, value.GvGPeriod, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNodeParam[]>().Serialize(ref bytes, offset, value.GvGNode, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNPCPartyParam[]>().Serialize(ref bytes, offset, value.GvGNPCParty, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNPCUnitParam[]>().Serialize(ref bytes, offset, value.GvGNPCUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGRewardRankingParam[]>().Serialize(ref bytes, offset, value.GvGRewardRanking, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGRewardParam[]>().Serialize(ref bytes, offset, value.GvGReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGRuleParam[]>().Serialize(ref bytes, offset, value.GvGRule, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNodeRewardParam[]>().Serialize(ref bytes, offset, value.GvGNodeReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueParam[]>().Serialize(ref bytes, offset, value.GvGLeague, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGCalcRateSettingParam[]>().Serialize(ref bytes, offset, value.GvGCalcRateSetting, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidParam[]>().Serialize(ref bytes, offset, value.WorldRaid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidBossParam[]>().Serialize(ref bytes, offset, value.WorldRaidBoss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRewardParam[]>().Serialize(ref bytes, offset, value.WorldRaidReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidDamageRewardParam[]>().Serialize(ref bytes, offset, value.WorldRaidDamageReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidDamageLotteryParam[]>().Serialize(ref bytes, offset, value.WorldRaidDamageLottery, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingRewardParam[]>().Serialize(ref bytes, offset, value.WorldRaidRankingReward, formatterResolver);
      return offset - num;
    }

    public Json_QuestList Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_QuestList) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_SectionParam[] jsonSectionParamArray = (JSON_SectionParam[]) null;
      JSON_ArchiveParam[] jsonArchiveParamArray = (JSON_ArchiveParam[]) null;
      JSON_ChapterParam[] jsonChapterParamArray = (JSON_ChapterParam[]) null;
      JSON_QuestParam[] jsonQuestParamArray = (JSON_QuestParam[]) null;
      JSON_ObjectiveParam[] jsonObjectiveParamArray1 = (JSON_ObjectiveParam[]) null;
      JSON_ObjectiveParam[] jsonObjectiveParamArray2 = (JSON_ObjectiveParam[]) null;
      JSON_MagnificationParam[] magnificationParamArray = (JSON_MagnificationParam[]) null;
      JSON_QuestCondParam[] jsonQuestCondParamArray = (JSON_QuestCondParam[]) null;
      JSON_QuestPartyParam[] jsonQuestPartyParamArray = (JSON_QuestPartyParam[]) null;
      JSON_QuestCampaignParentParam[] campaignParentParamArray = (JSON_QuestCampaignParentParam[]) null;
      JSON_QuestCampaignChildParam[] campaignChildParamArray = (JSON_QuestCampaignChildParam[]) null;
      JSON_QuestCampaignTrust[] questCampaignTrustArray = (JSON_QuestCampaignTrust[]) null;
      JSON_QuestCampaignInspSkill[] campaignInspSkillArray = (JSON_QuestCampaignInspSkill[]) null;
      JSON_TowerFloorParam[] jsonTowerFloorParamArray = (JSON_TowerFloorParam[]) null;
      JSON_TowerRewardParam[] towerRewardParamArray1 = (JSON_TowerRewardParam[]) null;
      JSON_TowerRoundRewardParam[] roundRewardParamArray = (JSON_TowerRoundRewardParam[]) null;
      JSON_TowerParam[] jsonTowerParamArray = (JSON_TowerParam[]) null;
      JSON_VersusTowerParam[] versusTowerParamArray = (JSON_VersusTowerParam[]) null;
      JSON_VersusSchedule[] jsonVersusScheduleArray = (JSON_VersusSchedule[]) null;
      JSON_VersusCoin[] jsonVersusCoinArray = (JSON_VersusCoin[]) null;
      JSON_MultiTowerFloorParam[] multiTowerFloorParamArray = (JSON_MultiTowerFloorParam[]) null;
      JSON_MultiTowerRewardParam[] towerRewardParamArray2 = (JSON_MultiTowerRewardParam[]) null;
      JSON_MapEffectParam[] jsonMapEffectParamArray = (JSON_MapEffectParam[]) null;
      JSON_WeatherSetParam[] jsonWeatherSetParamArray = (JSON_WeatherSetParam[]) null;
      JSON_RankingQuestParam[] rankingQuestParamArray = (JSON_RankingQuestParam[]) null;
      JSON_RankingQuestScheduleParam[] questScheduleParamArray = (JSON_RankingQuestScheduleParam[]) null;
      JSON_RankingQuestRewardParam[] questRewardParamArray = (JSON_RankingQuestRewardParam[]) null;
      JSON_VersusFirstWinBonus[] versusFirstWinBonusArray = (JSON_VersusFirstWinBonus[]) null;
      JSON_VersusStreakWinSchedule[] streakWinScheduleArray = (JSON_VersusStreakWinSchedule[]) null;
      JSON_VersusStreakWinBonus[] versusStreakWinBonusArray = (JSON_VersusStreakWinBonus[]) null;
      JSON_VersusRule[] jsonVersusRuleArray = (JSON_VersusRule[]) null;
      JSON_VersusCoinCampParam[] versusCoinCampParamArray = (JSON_VersusCoinCampParam[]) null;
      JSON_VersusEnableTimeParam[] versusEnableTimeParamArray = (JSON_VersusEnableTimeParam[]) null;
      JSON_VersusRankParam[] jsonVersusRankParamArray = (JSON_VersusRankParam[]) null;
      JSON_VersusRankClassParam[] versusRankClassParamArray = (JSON_VersusRankClassParam[]) null;
      JSON_VersusRankRankingRewardParam[] rankingRewardParamArray1 = (JSON_VersusRankRankingRewardParam[]) null;
      JSON_VersusRankRewardParam[] versusRankRewardParamArray = (JSON_VersusRankRewardParam[]) null;
      JSON_VersusRankMissionScheduleParam[] missionScheduleParamArray = (JSON_VersusRankMissionScheduleParam[]) null;
      JSON_VersusRankMissionParam[] rankMissionParamArray = (JSON_VersusRankMissionParam[]) null;
      JSON_QuestLobbyNewsParam[] questLobbyNewsParamArray = (JSON_QuestLobbyNewsParam[]) null;
      JSON_GuerrillaShopAdventQuestParam[] adventQuestParamArray = (JSON_GuerrillaShopAdventQuestParam[]) null;
      JSON_GuerrillaShopScheduleParam[] shopScheduleParamArray = (JSON_GuerrillaShopScheduleParam[]) null;
      JSON_VersusDraftDeckParam[] versusDraftDeckParamArray = (JSON_VersusDraftDeckParam[]) null;
      JSON_VersusDraftUnitParam[] versusDraftUnitParamArray = (JSON_VersusDraftUnitParam[]) null;
      JSON_GenesisStarParam[] genesisStarParamArray = (JSON_GenesisStarParam[]) null;
      JSON_GenesisChapterParam[] genesisChapterParamArray = (JSON_GenesisChapterParam[]) null;
      JSON_GenesisRewardParam[] genesisRewardParamArray = (JSON_GenesisRewardParam[]) null;
      JSON_GenesisLapBossParam[] genesisLapBossParamArray = (JSON_GenesisLapBossParam[]) null;
      JSON_AdvanceStarParam[] advanceStarParamArray = (JSON_AdvanceStarParam[]) null;
      JSON_AdvanceEventParam[] advanceEventParamArray = (JSON_AdvanceEventParam[]) null;
      JSON_AdvanceRewardParam[] advanceRewardParamArray = (JSON_AdvanceRewardParam[]) null;
      JSON_AdvanceLapBossParam[] advanceLapBossParamArray = (JSON_AdvanceLapBossParam[]) null;
      JSON_GuildRaidBossParam[] guildRaidBossParamArray = (JSON_GuildRaidBossParam[]) null;
      JSON_GuildRaidCoolDaysParam[] raidCoolDaysParamArray = (JSON_GuildRaidCoolDaysParam[]) null;
      JSON_GuildRaidScoreParam[] guildRaidScoreParamArray = (JSON_GuildRaidScoreParam[]) null;
      JSON_GuildRaidPeriodParam[] guildRaidPeriodParamArray = (JSON_GuildRaidPeriodParam[]) null;
      JSON_GuildRaidRewardParam[] guildRaidRewardParamArray = (JSON_GuildRaidRewardParam[]) null;
      JSON_GuildRaidRewardDmgRankingParam[] rewardDmgRankingParamArray = (JSON_GuildRaidRewardDmgRankingParam[]) null;
      JSON_GuildRaidRewardDmgRatioParam[] rewardDmgRatioParamArray = (JSON_GuildRaidRewardDmgRatioParam[]) null;
      JSON_GuildRaidRewardRoundParam[] rewardRoundParamArray = (JSON_GuildRaidRewardRoundParam[]) null;
      JSON_GuildRaidRewardRankingParam[] rewardRankingParamArray = (JSON_GuildRaidRewardRankingParam[]) null;
      JSON_GvGPeriodParam[] jsonGvGperiodParamArray = (JSON_GvGPeriodParam[]) null;
      JSON_GvGNodeParam[] jsonGvGnodeParamArray = (JSON_GvGNodeParam[]) null;
      JSON_GvGNPCPartyParam[] gvGnpcPartyParamArray = (JSON_GvGNPCPartyParam[]) null;
      JSON_GvGNPCUnitParam[] jsonGvGnpcUnitParamArray = (JSON_GvGNPCUnitParam[]) null;
      JSON_GvGRewardRankingParam[] grewardRankingParamArray = (JSON_GvGRewardRankingParam[]) null;
      JSON_GvGRewardParam[] jsonGvGrewardParamArray = (JSON_GvGRewardParam[]) null;
      JSON_GvGRuleParam[] jsonGvGruleParamArray = (JSON_GvGRuleParam[]) null;
      JSON_GvGNodeRewardParam[] gnodeRewardParamArray = (JSON_GvGNodeRewardParam[]) null;
      JSON_GvGLeagueParam[] jsonGvGleagueParamArray = (JSON_GvGLeagueParam[]) null;
      JSON_GvGCalcRateSettingParam[] rateSettingParamArray = (JSON_GvGCalcRateSettingParam[]) null;
      JSON_WorldRaidParam[] jsonWorldRaidParamArray = (JSON_WorldRaidParam[]) null;
      JSON_WorldRaidBossParam[] worldRaidBossParamArray = (JSON_WorldRaidBossParam[]) null;
      JSON_WorldRaidRewardParam[] worldRaidRewardParamArray = (JSON_WorldRaidRewardParam[]) null;
      JSON_WorldRaidDamageRewardParam[] damageRewardParamArray = (JSON_WorldRaidDamageRewardParam[]) null;
      JSON_WorldRaidDamageLotteryParam[] damageLotteryParamArray = (JSON_WorldRaidDamageLotteryParam[]) null;
      JSON_WorldRaidRankingRewardParam[] rankingRewardParamArray2 = (JSON_WorldRaidRankingRewardParam[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              jsonSectionParamArray = formatterResolver.GetFormatterWithVerify<JSON_SectionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonArchiveParamArray = formatterResolver.GetFormatterWithVerify<JSON_ArchiveParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonChapterParamArray = formatterResolver.GetFormatterWithVerify<JSON_ChapterParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonQuestParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonObjectiveParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_ObjectiveParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonObjectiveParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_ObjectiveParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              magnificationParamArray = formatterResolver.GetFormatterWithVerify<JSON_MagnificationParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonQuestCondParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestCondParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonQuestPartyParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestPartyParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              campaignParentParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignParentParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              campaignChildParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignChildParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              questCampaignTrustArray = formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignTrust[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              campaignInspSkillArray = formatterResolver.GetFormatterWithVerify<JSON_QuestCampaignInspSkill[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              jsonTowerFloorParamArray = formatterResolver.GetFormatterWithVerify<JSON_TowerFloorParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              towerRewardParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_TowerRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              roundRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_TowerRoundRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              jsonTowerParamArray = formatterResolver.GetFormatterWithVerify<JSON_TowerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              versusTowerParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusTowerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              jsonVersusScheduleArray = formatterResolver.GetFormatterWithVerify<JSON_VersusSchedule[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              jsonVersusCoinArray = formatterResolver.GetFormatterWithVerify<JSON_VersusCoin[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              multiTowerFloorParamArray = formatterResolver.GetFormatterWithVerify<JSON_MultiTowerFloorParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              towerRewardParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_MultiTowerRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              jsonMapEffectParamArray = formatterResolver.GetFormatterWithVerify<JSON_MapEffectParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              jsonWeatherSetParamArray = formatterResolver.GetFormatterWithVerify<JSON_WeatherSetParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              rankingQuestParamArray = formatterResolver.GetFormatterWithVerify<JSON_RankingQuestParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              questScheduleParamArray = formatterResolver.GetFormatterWithVerify<JSON_RankingQuestScheduleParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              questRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RankingQuestRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              versusFirstWinBonusArray = formatterResolver.GetFormatterWithVerify<JSON_VersusFirstWinBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              streakWinScheduleArray = formatterResolver.GetFormatterWithVerify<JSON_VersusStreakWinSchedule[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              versusStreakWinBonusArray = formatterResolver.GetFormatterWithVerify<JSON_VersusStreakWinBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              jsonVersusRuleArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRule[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              versusCoinCampParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusCoinCampParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              versusEnableTimeParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusEnableTimeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              jsonVersusRankParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRankParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              versusRankClassParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRankClassParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              rankingRewardParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_VersusRankRankingRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              versusRankRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRankRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              missionScheduleParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRankMissionScheduleParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              rankMissionParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusRankMissionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              questLobbyNewsParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestLobbyNewsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 40:
              adventQuestParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuerrillaShopAdventQuestParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              shopScheduleParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuerrillaShopScheduleParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              versusDraftDeckParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusDraftDeckParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              versusDraftUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusDraftUnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 44:
              genesisStarParamArray = formatterResolver.GetFormatterWithVerify<JSON_GenesisStarParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 45:
              genesisChapterParamArray = formatterResolver.GetFormatterWithVerify<JSON_GenesisChapterParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 46:
              genesisRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_GenesisRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              genesisLapBossParamArray = formatterResolver.GetFormatterWithVerify<JSON_GenesisLapBossParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              advanceStarParamArray = formatterResolver.GetFormatterWithVerify<JSON_AdvanceStarParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              advanceEventParamArray = formatterResolver.GetFormatterWithVerify<JSON_AdvanceEventParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              advanceRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_AdvanceRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              advanceLapBossParamArray = formatterResolver.GetFormatterWithVerify<JSON_AdvanceLapBossParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 52:
              guildRaidBossParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 53:
              raidCoolDaysParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCoolDaysParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              guildRaidScoreParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidScoreParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              guildRaidPeriodParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPeriodParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              guildRaidRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 57:
              rewardDmgRankingParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardDmgRankingParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 58:
              rewardDmgRatioParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardDmgRatioParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 59:
              rewardRoundParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardRoundParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 60:
              rewardRankingParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidRewardRankingParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 61:
              jsonGvGperiodParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGPeriodParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 62:
              jsonGvGnodeParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNodeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 63:
              gvGnpcPartyParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNPCPartyParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 64:
              jsonGvGnpcUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNPCUnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 65:
              grewardRankingParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGRewardRankingParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 66:
              jsonGvGrewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 67:
              jsonGvGruleParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGRuleParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 68:
              gnodeRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNodeRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 69:
              jsonGvGleagueParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 70:
              rateSettingParamArray = formatterResolver.GetFormatterWithVerify<JSON_GvGCalcRateSettingParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 71:
              jsonWorldRaidParamArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              worldRaidBossParamArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidBossParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              worldRaidRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 74:
              damageRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidDamageRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 75:
              damageLotteryParamArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidDamageLotteryParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 76:
              rankingRewardParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_QuestList()
      {
        worlds = jsonSectionParamArray,
        archives = jsonArchiveParamArray,
        areas = jsonChapterParamArray,
        quests = jsonQuestParamArray,
        objectives = jsonObjectiveParamArray1,
        towerObjectives = jsonObjectiveParamArray2,
        magnifications = magnificationParamArray,
        conditions = jsonQuestCondParamArray,
        parties = jsonQuestPartyParamArray,
        CampaignParents = campaignParentParamArray,
        CampaignChildren = campaignChildParamArray,
        CampaignTrust = questCampaignTrustArray,
        CampaignInspSkill = campaignInspSkillArray,
        towerFloors = jsonTowerFloorParamArray,
        towerRewards = towerRewardParamArray1,
        towerRoundRewards = roundRewardParamArray,
        towers = jsonTowerParamArray,
        versusTowerFloor = versusTowerParamArray,
        versusschedule = jsonVersusScheduleArray,
        versuscoin = jsonVersusCoinArray,
        multitowerFloor = multiTowerFloorParamArray,
        multitowerRewards = towerRewardParamArray2,
        MapEffect = jsonMapEffectParamArray,
        WeatherSet = jsonWeatherSetParamArray,
        rankingQuests = rankingQuestParamArray,
        rankingQuestSchedule = questScheduleParamArray,
        rankingQuestRewards = questRewardParamArray,
        versusfirstwinbonus = versusFirstWinBonusArray,
        versusstreakwinschedule = streakWinScheduleArray,
        versusstreakwinbonus = versusStreakWinBonusArray,
        versusrule = jsonVersusRuleArray,
        versuscoincamp = versusCoinCampParamArray,
        versusenabletime = versusEnableTimeParamArray,
        VersusRank = jsonVersusRankParamArray,
        VersusRankClass = versusRankClassParamArray,
        VersusRankRankingReward = rankingRewardParamArray1,
        VersusRankReward = versusRankRewardParamArray,
        VersusRankMissionSchedule = missionScheduleParamArray,
        VersusRankMission = rankMissionParamArray,
        questLobbyNews = questLobbyNewsParamArray,
        GuerrillaShopAdventQuest = adventQuestParamArray,
        GuerrillaShopSchedule = shopScheduleParamArray,
        VersusDraftDeck = versusDraftDeckParamArray,
        VersusDraftUnit = versusDraftUnitParamArray,
        GenesisStar = genesisStarParamArray,
        GenesisChapter = genesisChapterParamArray,
        GenesisReward = genesisRewardParamArray,
        GenesisLapBoss = genesisLapBossParamArray,
        AdvanceStar = advanceStarParamArray,
        AdvanceEvent = advanceEventParamArray,
        AdvanceReward = advanceRewardParamArray,
        AdvanceLapBoss = advanceLapBossParamArray,
        GuildRaidBoss = guildRaidBossParamArray,
        GuildRaidCoolDays = raidCoolDaysParamArray,
        GuildRaidScore = guildRaidScoreParamArray,
        GuildRaidPeriod = guildRaidPeriodParamArray,
        GuildRaidReward = guildRaidRewardParamArray,
        GuildRaidRewardDmgRanking = rewardDmgRankingParamArray,
        GuildRaidRewardDmgRatio = rewardDmgRatioParamArray,
        GuildRaidRewardRound = rewardRoundParamArray,
        GuildRaidRewardRanking = rewardRankingParamArray,
        GvGPeriod = jsonGvGperiodParamArray,
        GvGNode = jsonGvGnodeParamArray,
        GvGNPCParty = gvGnpcPartyParamArray,
        GvGNPCUnit = jsonGvGnpcUnitParamArray,
        GvGRewardRanking = grewardRankingParamArray,
        GvGReward = jsonGvGrewardParamArray,
        GvGRule = jsonGvGruleParamArray,
        GvGNodeReward = gnodeRewardParamArray,
        GvGLeague = jsonGvGleagueParamArray,
        GvGCalcRateSetting = rateSettingParamArray,
        WorldRaid = jsonWorldRaidParamArray,
        WorldRaidBoss = worldRaidBossParamArray,
        WorldRaidReward = worldRaidRewardParamArray,
        WorldRaidDamageReward = damageRewardParamArray,
        WorldRaidDamageLottery = damageLotteryParamArray,
        WorldRaidRankingReward = rankingRewardParamArray2
      };
    }
  }
}
