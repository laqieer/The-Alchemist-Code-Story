// Decompiled with JetBrains decompiler
// Type: SRPG.Json_QuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_QuestList
  {
    public JSON_SectionParam[] worlds;
    public JSON_ArchiveParam[] archives;
    public JSON_ChapterParam[] areas;
    public JSON_QuestParam[] quests;
    public JSON_ObjectiveParam[] objectives;
    public JSON_ObjectiveParam[] towerObjectives;
    public JSON_MagnificationParam[] magnifications;
    public JSON_QuestCondParam[] conditions;
    public JSON_QuestPartyParam[] parties;
    public JSON_QuestCampaignParentParam[] CampaignParents;
    public JSON_QuestCampaignChildParam[] CampaignChildren;
    public JSON_QuestCampaignTrust[] CampaignTrust;
    public JSON_QuestCampaignInspSkill[] CampaignInspSkill;
    public JSON_TowerFloorParam[] towerFloors;
    public JSON_TowerRewardParam[] towerRewards;
    public JSON_TowerRoundRewardParam[] towerRoundRewards;
    public JSON_TowerParam[] towers;
    public JSON_VersusTowerParam[] versusTowerFloor;
    public JSON_VersusSchedule[] versusschedule;
    public JSON_VersusCoin[] versuscoin;
    public JSON_MultiTowerFloorParam[] multitowerFloor;
    public JSON_MultiTowerRewardParam[] multitowerRewards;
    public JSON_MapEffectParam[] MapEffect;
    public JSON_WeatherSetParam[] WeatherSet;
    public JSON_RankingQuestParam[] rankingQuests;
    public JSON_RankingQuestScheduleParam[] rankingQuestSchedule;
    public JSON_RankingQuestRewardParam[] rankingQuestRewards;
    public JSON_VersusFirstWinBonus[] versusfirstwinbonus;
    public JSON_VersusStreakWinSchedule[] versusstreakwinschedule;
    public JSON_VersusStreakWinBonus[] versusstreakwinbonus;
    public JSON_VersusRule[] versusrule;
    public JSON_VersusCoinCampParam[] versuscoincamp;
    public JSON_VersusEnableTimeParam[] versusenabletime;
    public JSON_VersusRankParam[] VersusRank;
    public JSON_VersusRankClassParam[] VersusRankClass;
    public JSON_VersusRankRankingRewardParam[] VersusRankRankingReward;
    public JSON_VersusRankRewardParam[] VersusRankReward;
    public JSON_VersusRankMissionScheduleParam[] VersusRankMissionSchedule;
    public JSON_VersusRankMissionParam[] VersusRankMission;
    public JSON_QuestLobbyNewsParam[] questLobbyNews;
    public JSON_GuerrillaShopAdventQuestParam[] GuerrillaShopAdventQuest;
    public JSON_GuerrillaShopScheduleParam[] GuerrillaShopSchedule;
    public JSON_VersusDraftDeckParam[] VersusDraftDeck;
    public JSON_VersusDraftUnitParam[] VersusDraftUnit;
    public JSON_GenesisStarParam[] GenesisStar;
    public JSON_GenesisChapterParam[] GenesisChapter;
    public JSON_GenesisRewardParam[] GenesisReward;
    public JSON_GenesisLapBossParam[] GenesisLapBoss;
    public JSON_AdvanceStarParam[] AdvanceStar;
    public JSON_AdvanceEventParam[] AdvanceEvent;
    public JSON_AdvanceRewardParam[] AdvanceReward;
    public JSON_AdvanceLapBossParam[] AdvanceLapBoss;
    public JSON_GuildRaidBossParam[] GuildRaidBoss;
    public JSON_GuildRaidCoolDaysParam[] GuildRaidCoolDays;
    public JSON_GuildRaidScoreParam[] GuildRaidScore;
    public JSON_GuildRaidPeriodParam[] GuildRaidPeriod;
    public JSON_GuildRaidRewardParam[] GuildRaidReward;
    public JSON_GuildRaidRewardDmgRankingParam[] GuildRaidRewardDmgRanking;
    public JSON_GuildRaidRewardDmgRatioParam[] GuildRaidRewardDmgRatio;
    public JSON_GuildRaidRewardRoundParam[] GuildRaidRewardRound;
    public JSON_GuildRaidRewardRankingParam[] GuildRaidRewardRanking;
    public JSON_GvGPeriodParam[] GvGPeriod;
    public JSON_GvGNodeParam[] GvGNode;
    public JSON_GvGNPCPartyParam[] GvGNPCParty;
    public JSON_GvGNPCUnitParam[] GvGNPCUnit;
    public JSON_GvGRewardRankingParam[] GvGRewardRanking;
    public JSON_GvGRewardParam[] GvGReward;
    public JSON_GvGRuleParam[] GvGRule;
    public JSON_GvGNodeRewardParam[] GvGNodeReward;
    public JSON_GvGLeagueParam[] GvGLeague;
    public JSON_GvGCalcRateSettingParam[] GvGCalcRateSetting;
    public JSON_WorldRaidParam[] WorldRaid;
    public JSON_WorldRaidBossParam[] WorldRaidBoss;
    public JSON_WorldRaidRewardParam[] WorldRaidReward;
    public JSON_WorldRaidDamageRewardParam[] WorldRaidDamageReward;
    public JSON_WorldRaidDamageLotteryParam[] WorldRaidDamageLottery;
    public JSON_WorldRaidRankingRewardParam[] WorldRaidRankingReward;
  }
}
