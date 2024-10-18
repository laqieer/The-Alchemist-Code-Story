// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_MasterParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_MasterParamFormatter : 
    IMessagePackFormatter<JSON_MasterParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_MasterParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Fix",
          0
        },
        {
          "Unit",
          1
        },
        {
          "UnitJobOverwrite",
          2
        },
        {
          "Skill",
          3
        },
        {
          "Buff",
          4
        },
        {
          "Cond",
          5
        },
        {
          "Ability",
          6
        },
        {
          "Item",
          7
        },
        {
          "Artifact",
          8
        },
        {
          "Weapon",
          9
        },
        {
          "Recipe",
          10
        },
        {
          "Job",
          11
        },
        {
          "JobSet",
          12
        },
        {
          "Evaluation",
          13
        },
        {
          "AI",
          14
        },
        {
          "Geo",
          15
        },
        {
          "Rarity",
          16
        },
        {
          "Shop",
          17
        },
        {
          "AbilityRank",
          18
        },
        {
          "UnitLvTbl",
          19
        },
        {
          "PlayerLvTbl",
          20
        },
        {
          "ArtifactLvTbl",
          21
        },
        {
          "AwakePieceTbl",
          22
        },
        {
          "Player",
          23
        },
        {
          "Grow",
          24
        },
        {
          "LocalNotification",
          25
        },
        {
          "TrophyCategory",
          26
        },
        {
          "GuildTrophyCategory",
          27
        },
        {
          "ChallengeCategory",
          28
        },
        {
          "Trophy",
          29
        },
        {
          "Challenge",
          30
        },
        {
          "GuildTrophy",
          31
        },
        {
          "Unlock",
          32
        },
        {
          "Vip",
          33
        },
        {
          "ArenaWinResult",
          34
        },
        {
          "ArenaRankResult",
          35
        },
        {
          "Mov",
          36
        },
        {
          "Banner",
          37
        },
        {
          "QuestClearUnlockUnitData",
          38
        },
        {
          "Award",
          39
        },
        {
          "LoginInfo",
          40
        },
        {
          "CollaboSkill",
          41
        },
        {
          "Trick",
          42
        },
        {
          "BreakObj",
          43
        },
        {
          "VersusMatchKey",
          44
        },
        {
          "VersusMatchCond",
          45
        },
        {
          "TowerScore",
          46
        },
        {
          "TowerRank",
          47
        },
        {
          "MultilimitUnitLv",
          48
        },
        {
          "FriendPresentItem",
          49
        },
        {
          "Weather",
          50
        },
        {
          "UnitUnlockTime",
          51
        },
        {
          "Tobira",
          52
        },
        {
          "TobiraCategories",
          53
        },
        {
          "TobiraConds",
          54
        },
        {
          "TobiraCondsUnit",
          55
        },
        {
          "TobiraRecipe",
          56
        },
        {
          "ConceptCard",
          57
        },
        {
          "ConceptCardLvTbl1",
          58
        },
        {
          "ConceptCardLvTbl2",
          59
        },
        {
          "ConceptCardLvTbl3",
          60
        },
        {
          "ConceptCardLvTbl4",
          61
        },
        {
          "ConceptCardLvTbl5",
          62
        },
        {
          "ConceptCardLvTbl6",
          63
        },
        {
          "ConceptCardConditions",
          64
        },
        {
          "ConceptCardTrustReward",
          65
        },
        {
          "ConceptCardSellCoinRate",
          66
        },
        {
          "ConceptCardLsBuffCoef",
          67
        },
        {
          "ConceptCardGroup",
          68
        },
        {
          "ConceptLimitUpItem",
          69
        },
        {
          "UnitGroup",
          70
        },
        {
          "JobGroup",
          71
        },
        {
          "StatusCoefficient",
          72
        },
        {
          "CustomTarget",
          73
        },
        {
          "SkillAbilityDerive",
          74
        },
        {
          "RaidPeriod",
          75
        },
        {
          "RaidPeriodTime",
          76
        },
        {
          "RaidArea",
          77
        },
        {
          "RaidBoss",
          78
        },
        {
          "RaidBattleReward",
          79
        },
        {
          "RaidBeatReward",
          80
        },
        {
          "RaidDamageRatioReward",
          81
        },
        {
          "RaidDamageAmountReward",
          82
        },
        {
          "RaidAreaClearReward",
          83
        },
        {
          "RaidCompleteReward",
          84
        },
        {
          "RaidReward",
          85
        },
        {
          "Tips",
          86
        },
        {
          "GuildEmblem",
          87
        },
        {
          "GuildFacility",
          88
        },
        {
          "GuildFacilityLvTbl",
          89
        },
        {
          "ConvertUnitPieceExclude",
          90
        },
        {
          "Premium",
          91
        },
        {
          "BuyCoinShop",
          92
        },
        {
          "BuyCoinProduct",
          93
        },
        {
          "BuyCoinReward",
          94
        },
        {
          "BuyCoinProductConvert",
          95
        },
        {
          "DynamicTransformUnit",
          96
        },
        {
          "RecommendedArtifact",
          97
        },
        {
          "SkillMotion",
          98
        },
        {
          "DependStateSpcEff",
          99
        },
        {
          "InspirationSkill",
          100
        },
        {
          "InspSkillTrigger",
          101
        },
        {
          "InspSkillOpenCost",
          102
        },
        {
          "InspSkillResetCost",
          103
        },
        {
          "InspSkillLvUpCost",
          104
        },
        {
          "Highlight",
          105
        },
        {
          "HighlightGift",
          106
        },
        {
          "Genesis",
          107
        },
        {
          "CoinBuyUseBonus",
          108
        },
        {
          "CoinBuyUseBonusRewardSet",
          109
        },
        {
          "CoinBuyUseBonusReward",
          110
        },
        {
          "UnitRentalNotification",
          111
        },
        {
          "UnitRental",
          112
        },
        {
          "DrawCardReward",
          113
        },
        {
          "DrawCard",
          114
        },
        {
          "TrophyStarMissionReward",
          115
        },
        {
          "TrophyStarMission",
          116
        },
        {
          "UnitPieceShop",
          117
        },
        {
          "UnitPieceShopGroup",
          118
        },
        {
          "TwitterMessage",
          119
        },
        {
          "FilterConceptCard",
          120
        },
        {
          "FilterRune",
          121
        },
        {
          "FilterUnit",
          122
        },
        {
          "FilterArtifact",
          123
        },
        {
          "SortRune",
          124
        },
        {
          "Rune",
          125
        },
        {
          "RuneLotteryBaseState",
          126
        },
        {
          "RuneLotteryEvoState",
          (int) sbyte.MaxValue
        },
        {
          "RuneMaterial",
          128
        },
        {
          "RuneCost",
          129
        },
        {
          "RuneSetEff",
          130
        },
        {
          "JukeBox",
          131
        },
        {
          "JukeBoxSection",
          132
        },
        {
          "UnitSameGroup",
          133
        },
        {
          "AutoRepeatQuestBox",
          134
        },
        {
          "GuildAttend",
          135
        },
        {
          "GuildAttendReward",
          136
        },
        {
          "GuildRoleBonus",
          137
        },
        {
          "GuildRoleBonusReward",
          138
        },
        {
          "ResetCost",
          139
        },
        {
          "ProtectSkill",
          140
        },
        {
          "GuildSearchFilter",
          141
        },
        {
          "ReplaceSprite",
          142
        },
        {
          "ExpansionPurchase",
          143
        },
        {
          "ExpansionPurchaseQuest",
          144
        },
        {
          "SkillAdditional",
          145
        },
        {
          "SkillAntiShield",
          146
        },
        {
          "InitPlayer",
          147
        },
        {
          "InitUnit",
          148
        },
        {
          "InitItem",
          149
        }
      };
      this.____stringByteKeys = new byte[150][]
      {
        MessagePackBinary.GetEncodedStringBytes("Fix"),
        MessagePackBinary.GetEncodedStringBytes("Unit"),
        MessagePackBinary.GetEncodedStringBytes("UnitJobOverwrite"),
        MessagePackBinary.GetEncodedStringBytes("Skill"),
        MessagePackBinary.GetEncodedStringBytes("Buff"),
        MessagePackBinary.GetEncodedStringBytes("Cond"),
        MessagePackBinary.GetEncodedStringBytes("Ability"),
        MessagePackBinary.GetEncodedStringBytes("Item"),
        MessagePackBinary.GetEncodedStringBytes("Artifact"),
        MessagePackBinary.GetEncodedStringBytes("Weapon"),
        MessagePackBinary.GetEncodedStringBytes("Recipe"),
        MessagePackBinary.GetEncodedStringBytes("Job"),
        MessagePackBinary.GetEncodedStringBytes("JobSet"),
        MessagePackBinary.GetEncodedStringBytes("Evaluation"),
        MessagePackBinary.GetEncodedStringBytes("AI"),
        MessagePackBinary.GetEncodedStringBytes("Geo"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("Shop"),
        MessagePackBinary.GetEncodedStringBytes("AbilityRank"),
        MessagePackBinary.GetEncodedStringBytes("UnitLvTbl"),
        MessagePackBinary.GetEncodedStringBytes("PlayerLvTbl"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactLvTbl"),
        MessagePackBinary.GetEncodedStringBytes("AwakePieceTbl"),
        MessagePackBinary.GetEncodedStringBytes("Player"),
        MessagePackBinary.GetEncodedStringBytes("Grow"),
        MessagePackBinary.GetEncodedStringBytes("LocalNotification"),
        MessagePackBinary.GetEncodedStringBytes("TrophyCategory"),
        MessagePackBinary.GetEncodedStringBytes("GuildTrophyCategory"),
        MessagePackBinary.GetEncodedStringBytes("ChallengeCategory"),
        MessagePackBinary.GetEncodedStringBytes("Trophy"),
        MessagePackBinary.GetEncodedStringBytes("Challenge"),
        MessagePackBinary.GetEncodedStringBytes("GuildTrophy"),
        MessagePackBinary.GetEncodedStringBytes("Unlock"),
        MessagePackBinary.GetEncodedStringBytes("Vip"),
        MessagePackBinary.GetEncodedStringBytes("ArenaWinResult"),
        MessagePackBinary.GetEncodedStringBytes("ArenaRankResult"),
        MessagePackBinary.GetEncodedStringBytes("Mov"),
        MessagePackBinary.GetEncodedStringBytes("Banner"),
        MessagePackBinary.GetEncodedStringBytes("QuestClearUnlockUnitData"),
        MessagePackBinary.GetEncodedStringBytes("Award"),
        MessagePackBinary.GetEncodedStringBytes("LoginInfo"),
        MessagePackBinary.GetEncodedStringBytes("CollaboSkill"),
        MessagePackBinary.GetEncodedStringBytes("Trick"),
        MessagePackBinary.GetEncodedStringBytes("BreakObj"),
        MessagePackBinary.GetEncodedStringBytes("VersusMatchKey"),
        MessagePackBinary.GetEncodedStringBytes("VersusMatchCond"),
        MessagePackBinary.GetEncodedStringBytes("TowerScore"),
        MessagePackBinary.GetEncodedStringBytes("TowerRank"),
        MessagePackBinary.GetEncodedStringBytes("MultilimitUnitLv"),
        MessagePackBinary.GetEncodedStringBytes("FriendPresentItem"),
        MessagePackBinary.GetEncodedStringBytes("Weather"),
        MessagePackBinary.GetEncodedStringBytes("UnitUnlockTime"),
        MessagePackBinary.GetEncodedStringBytes("Tobira"),
        MessagePackBinary.GetEncodedStringBytes("TobiraCategories"),
        MessagePackBinary.GetEncodedStringBytes("TobiraConds"),
        MessagePackBinary.GetEncodedStringBytes("TobiraCondsUnit"),
        MessagePackBinary.GetEncodedStringBytes("TobiraRecipe"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl1"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl2"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl3"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl4"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl5"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvTbl6"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardConditions"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardTrustReward"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardSellCoinRate"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLsBuffCoef"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardGroup"),
        MessagePackBinary.GetEncodedStringBytes("ConceptLimitUpItem"),
        MessagePackBinary.GetEncodedStringBytes("UnitGroup"),
        MessagePackBinary.GetEncodedStringBytes("JobGroup"),
        MessagePackBinary.GetEncodedStringBytes("StatusCoefficient"),
        MessagePackBinary.GetEncodedStringBytes("CustomTarget"),
        MessagePackBinary.GetEncodedStringBytes("SkillAbilityDerive"),
        MessagePackBinary.GetEncodedStringBytes("RaidPeriod"),
        MessagePackBinary.GetEncodedStringBytes("RaidPeriodTime"),
        MessagePackBinary.GetEncodedStringBytes("RaidArea"),
        MessagePackBinary.GetEncodedStringBytes("RaidBoss"),
        MessagePackBinary.GetEncodedStringBytes("RaidBattleReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidBeatReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidDamageRatioReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidDamageAmountReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidAreaClearReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidCompleteReward"),
        MessagePackBinary.GetEncodedStringBytes("RaidReward"),
        MessagePackBinary.GetEncodedStringBytes("Tips"),
        MessagePackBinary.GetEncodedStringBytes("GuildEmblem"),
        MessagePackBinary.GetEncodedStringBytes("GuildFacility"),
        MessagePackBinary.GetEncodedStringBytes("GuildFacilityLvTbl"),
        MessagePackBinary.GetEncodedStringBytes("ConvertUnitPieceExclude"),
        MessagePackBinary.GetEncodedStringBytes("Premium"),
        MessagePackBinary.GetEncodedStringBytes("BuyCoinShop"),
        MessagePackBinary.GetEncodedStringBytes("BuyCoinProduct"),
        MessagePackBinary.GetEncodedStringBytes("BuyCoinReward"),
        MessagePackBinary.GetEncodedStringBytes("BuyCoinProductConvert"),
        MessagePackBinary.GetEncodedStringBytes("DynamicTransformUnit"),
        MessagePackBinary.GetEncodedStringBytes("RecommendedArtifact"),
        MessagePackBinary.GetEncodedStringBytes("SkillMotion"),
        MessagePackBinary.GetEncodedStringBytes("DependStateSpcEff"),
        MessagePackBinary.GetEncodedStringBytes("InspirationSkill"),
        MessagePackBinary.GetEncodedStringBytes("InspSkillTrigger"),
        MessagePackBinary.GetEncodedStringBytes("InspSkillOpenCost"),
        MessagePackBinary.GetEncodedStringBytes("InspSkillResetCost"),
        MessagePackBinary.GetEncodedStringBytes("InspSkillLvUpCost"),
        MessagePackBinary.GetEncodedStringBytes("Highlight"),
        MessagePackBinary.GetEncodedStringBytes("HighlightGift"),
        MessagePackBinary.GetEncodedStringBytes("Genesis"),
        MessagePackBinary.GetEncodedStringBytes("CoinBuyUseBonus"),
        MessagePackBinary.GetEncodedStringBytes("CoinBuyUseBonusRewardSet"),
        MessagePackBinary.GetEncodedStringBytes("CoinBuyUseBonusReward"),
        MessagePackBinary.GetEncodedStringBytes("UnitRentalNotification"),
        MessagePackBinary.GetEncodedStringBytes("UnitRental"),
        MessagePackBinary.GetEncodedStringBytes("DrawCardReward"),
        MessagePackBinary.GetEncodedStringBytes("DrawCard"),
        MessagePackBinary.GetEncodedStringBytes("TrophyStarMissionReward"),
        MessagePackBinary.GetEncodedStringBytes("TrophyStarMission"),
        MessagePackBinary.GetEncodedStringBytes("UnitPieceShop"),
        MessagePackBinary.GetEncodedStringBytes("UnitPieceShopGroup"),
        MessagePackBinary.GetEncodedStringBytes("TwitterMessage"),
        MessagePackBinary.GetEncodedStringBytes("FilterConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("FilterRune"),
        MessagePackBinary.GetEncodedStringBytes("FilterUnit"),
        MessagePackBinary.GetEncodedStringBytes("FilterArtifact"),
        MessagePackBinary.GetEncodedStringBytes("SortRune"),
        MessagePackBinary.GetEncodedStringBytes("Rune"),
        MessagePackBinary.GetEncodedStringBytes("RuneLotteryBaseState"),
        MessagePackBinary.GetEncodedStringBytes("RuneLotteryEvoState"),
        MessagePackBinary.GetEncodedStringBytes("RuneMaterial"),
        MessagePackBinary.GetEncodedStringBytes("RuneCost"),
        MessagePackBinary.GetEncodedStringBytes("RuneSetEff"),
        MessagePackBinary.GetEncodedStringBytes("JukeBox"),
        MessagePackBinary.GetEncodedStringBytes("JukeBoxSection"),
        MessagePackBinary.GetEncodedStringBytes("UnitSameGroup"),
        MessagePackBinary.GetEncodedStringBytes("AutoRepeatQuestBox"),
        MessagePackBinary.GetEncodedStringBytes("GuildAttend"),
        MessagePackBinary.GetEncodedStringBytes("GuildAttendReward"),
        MessagePackBinary.GetEncodedStringBytes("GuildRoleBonus"),
        MessagePackBinary.GetEncodedStringBytes("GuildRoleBonusReward"),
        MessagePackBinary.GetEncodedStringBytes("ResetCost"),
        MessagePackBinary.GetEncodedStringBytes("ProtectSkill"),
        MessagePackBinary.GetEncodedStringBytes("GuildSearchFilter"),
        MessagePackBinary.GetEncodedStringBytes("ReplaceSprite"),
        MessagePackBinary.GetEncodedStringBytes("ExpansionPurchase"),
        MessagePackBinary.GetEncodedStringBytes("ExpansionPurchaseQuest"),
        MessagePackBinary.GetEncodedStringBytes("SkillAdditional"),
        MessagePackBinary.GetEncodedStringBytes("SkillAntiShield"),
        MessagePackBinary.GetEncodedStringBytes("InitPlayer"),
        MessagePackBinary.GetEncodedStringBytes("InitUnit"),
        MessagePackBinary.GetEncodedStringBytes("InitItem")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_MasterParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 150);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FixParam[]>().Serialize(ref bytes, offset, value.Fix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitParam[]>().Serialize(ref bytes, offset, value.Unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitJobOverwriteParam[]>().Serialize(ref bytes, offset, value.UnitJobOverwrite, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillParam[]>().Serialize(ref bytes, offset, value.Skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuffEffectParam[]>().Serialize(ref bytes, offset, value.Buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CondEffectParam[]>().Serialize(ref bytes, offset, value.Cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AbilityParam[]>().Serialize(ref bytes, offset, value.Ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ItemParam[]>().Serialize(ref bytes, offset, value.Item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ArtifactParam[]>().Serialize(ref bytes, offset, value.Artifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WeaponParam[]>().Serialize(ref bytes, offset, value.Weapon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RecipeParam[]>().Serialize(ref bytes, offset, value.Recipe, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobParam[]>().Serialize(ref bytes, offset, value.Job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobSetParam[]>().Serialize(ref bytes, offset, value.JobSet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_EvaluationParam[]>().Serialize(ref bytes, offset, value.Evaluation, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AIParam[]>().Serialize(ref bytes, offset, value.AI, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GeoParam[]>().Serialize(ref bytes, offset, value.Geo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RarityParam[]>().Serialize(ref bytes, offset, value.Rarity, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ShopParam[]>().Serialize(ref bytes, offset, value.Shop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.AbilityRank, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.UnitLvTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.PlayerLvTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ArtifactLvTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.AwakePieceTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PlayerParam[]>().Serialize(ref bytes, offset, value.Player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GrowParam[]>().Serialize(ref bytes, offset, value.Grow, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_LocalNotificationParam[]>().Serialize(ref bytes, offset, value.LocalNotification, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyCategoryParam[]>().Serialize(ref bytes, offset, value.TrophyCategory, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyCategoryParam[]>().Serialize(ref bytes, offset, value.GuildTrophyCategory, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ChallengeCategoryParam[]>().Serialize(ref bytes, offset, value.ChallengeCategory, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Serialize(ref bytes, offset, value.Trophy, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Serialize(ref bytes, offset, value.Challenge, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Serialize(ref bytes, offset, value.GuildTrophy, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnlockParam[]>().Serialize(ref bytes, offset, value.Unlock, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VipParam[]>().Serialize(ref bytes, offset, value.Vip, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ArenaWinResult[]>().Serialize(ref bytes, offset, value.ArenaWinResult, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ArenaResult[]>().Serialize(ref bytes, offset, value.ArenaRankResult, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_StreamingMovie[]>().Serialize(ref bytes, offset, value.Mov, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BannerParam[]>().Serialize(ref bytes, offset, value.Banner, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestClearUnlockUnitDataParam[]>().Serialize(ref bytes, offset, value.QuestClearUnlockUnitData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AwardParam[]>().Serialize(ref bytes, offset, value.Award, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_LoginInfoParam[]>().Serialize(ref bytes, offset, value.LoginInfo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CollaboSkillParam[]>().Serialize(ref bytes, offset, value.CollaboSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrickParam[]>().Serialize(ref bytes, offset, value.Trick, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BreakObjParam[]>().Serialize(ref bytes, offset, value.BreakObj, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusMatchingParam[]>().Serialize(ref bytes, offset, value.VersusMatchKey, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusMatchCondParam[]>().Serialize(ref bytes, offset, value.VersusMatchCond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TowerScore[]>().Serialize(ref bytes, offset, value.TowerScore, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.TowerRank, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.MultilimitUnitLv, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FriendPresentItemParam[]>().Serialize(ref bytes, offset, value.FriendPresentItem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WeatherParam[]>().Serialize(ref bytes, offset, value.Weather, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitUnlockTimeParam[]>().Serialize(ref bytes, offset, value.UnitUnlockTime, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraParam[]>().Serialize(ref bytes, offset, value.Tobira, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraCategoriesParam[]>().Serialize(ref bytes, offset, value.TobiraCategories, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsParam[]>().Serialize(ref bytes, offset, value.TobiraConds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsUnitParam[]>().Serialize(ref bytes, offset, value.TobiraCondsUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraRecipeParam[]>().Serialize(ref bytes, offset, value.TobiraRecipe, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCardParam[]>().Serialize(ref bytes, offset, value.ConceptCard, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardLvTbl6, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCardConditionsParam[]>().Serialize(ref bytes, offset, value.ConceptCardConditions, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCardTrustRewardParam[]>().Serialize(ref bytes, offset, value.ConceptCardTrustReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ConceptCardSellCoinRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCardLsBuffCoefParam[]>().Serialize(ref bytes, offset, value.ConceptCardLsBuffCoef, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCardGroup[]>().Serialize(ref bytes, offset, value.ConceptCardGroup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptLimitUpItem[]>().Serialize(ref bytes, offset, value.ConceptLimitUpItem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitGroupParam[]>().Serialize(ref bytes, offset, value.UnitGroup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobGroupParam[]>().Serialize(ref bytes, offset, value.JobGroup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_StatusCoefficientParam[]>().Serialize(ref bytes, offset, value.StatusCoefficient, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CustomTargetParam[]>().Serialize(ref bytes, offset, value.CustomTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillAbilityDeriveParam[]>().Serialize(ref bytes, offset, value.SkillAbilityDerive, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidPeriodParam[]>().Serialize(ref bytes, offset, value.RaidPeriod, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidPeriodTimeParam[]>().Serialize(ref bytes, offset, value.RaidPeriodTime, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidAreaParam[]>().Serialize(ref bytes, offset, value.RaidArea, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidBossParam[]>().Serialize(ref bytes, offset, value.RaidBoss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidBattleRewardParam[]>().Serialize(ref bytes, offset, value.RaidBattleReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidBeatRewardParam[]>().Serialize(ref bytes, offset, value.RaidBeatReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidDamageRatioRewardParam[]>().Serialize(ref bytes, offset, value.RaidDamageRatioReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidDamageAmountRewardParam[]>().Serialize(ref bytes, offset, value.RaidDamageAmountReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidAreaClearRewardParam[]>().Serialize(ref bytes, offset, value.RaidAreaClearReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidCompleteRewardParam[]>().Serialize(ref bytes, offset, value.RaidCompleteReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RaidRewardParam[]>().Serialize(ref bytes, offset, value.RaidReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TipsParam[]>().Serialize(ref bytes, offset, value.Tips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildEmblemParam[]>().Serialize(ref bytes, offset, value.GuildEmblem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityParam[]>().Serialize(ref bytes, offset, value.GuildFacility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityLvParam[]>().Serialize(ref bytes, offset, value.GuildFacilityLvTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConvertUnitPieceExcludeParam[]>().Serialize(ref bytes, offset, value.ConvertUnitPieceExclude, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PremiumParam[]>().Serialize(ref bytes, offset, value.Premium, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuyCoinShopParam[]>().Serialize(ref bytes, offset, value.BuyCoinShop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuyCoinProductParam[]>().Serialize(ref bytes, offset, value.BuyCoinProduct, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuyCoinRewardParam[]>().Serialize(ref bytes, offset, value.BuyCoinReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuyCoinProductConvertParam[]>().Serialize(ref bytes, offset, value.BuyCoinProductConvert, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_DynamicTransformUnitParam[]>().Serialize(ref bytes, offset, value.DynamicTransformUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RecommendedArtifactParam[]>().Serialize(ref bytes, offset, value.RecommendedArtifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillMotionParam[]>().Serialize(ref bytes, offset, value.SkillMotion, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_DependStateSpcEffParam[]>().Serialize(ref bytes, offset, value.DependStateSpcEff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InspSkillParam[]>().Serialize(ref bytes, offset, value.InspirationSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InspSkillTriggerParam[]>().Serialize(ref bytes, offset, value.InspSkillTrigger, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InspSkillCostParam[]>().Serialize(ref bytes, offset, value.InspSkillOpenCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InspSkillCostParam[]>().Serialize(ref bytes, offset, value.InspSkillResetCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InspSkillLvUpCostParam[]>().Serialize(ref bytes, offset, value.InspSkillLvUpCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_HighlightParam[]>().Serialize(ref bytes, offset, value.Highlight, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_HighlightGift[]>().Serialize(ref bytes, offset, value.HighlightGift, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GenesisParam[]>().Serialize(ref bytes, offset, value.Genesis, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusParam[]>().Serialize(ref bytes, offset, value.CoinBuyUseBonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusRewardSetParam[]>().Serialize(ref bytes, offset, value.CoinBuyUseBonusRewardSet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusRewardParam[]>().Serialize(ref bytes, offset, value.CoinBuyUseBonusReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitRentalNotificationParam[]>().Serialize(ref bytes, offset, value.UnitRentalNotification, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitRentalParam[]>().Serialize(ref bytes, offset, value.UnitRental, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_DrawCardRewardParam[]>().Serialize(ref bytes, offset, value.DrawCardReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_DrawCardParam[]>().Serialize(ref bytes, offset, value.DrawCard, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyStarMissionRewardParam[]>().Serialize(ref bytes, offset, value.TrophyStarMissionReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyStarMissionParam[]>().Serialize(ref bytes, offset, value.TrophyStarMission, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitPieceShopParam[]>().Serialize(ref bytes, offset, value.UnitPieceShop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitPieceShopGroupParam[]>().Serialize(ref bytes, offset, value.UnitPieceShopGroup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TwitterMessageParam[]>().Serialize(ref bytes, offset, value.TwitterMessage, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FilterConceptCardParam[]>().Serialize(ref bytes, offset, value.FilterConceptCard, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FilterRuneParam[]>().Serialize(ref bytes, offset, value.FilterRune, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FilterUnitParam[]>().Serialize(ref bytes, offset, value.FilterUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FilterArtifactParam[]>().Serialize(ref bytes, offset, value.FilterArtifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SortRuneParam[]>().Serialize(ref bytes, offset, value.SortRune, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneParam[]>().Serialize(ref bytes, offset, value.Rune, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneLotteryBaseState[]>().Serialize(ref bytes, offset, value.RuneLotteryBaseState, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneLotteryEvoState[]>().Serialize(ref bytes, offset, value.RuneLotteryEvoState, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[128]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneMaterial[]>().Serialize(ref bytes, offset, value.RuneMaterial, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[129]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneCost[]>().Serialize(ref bytes, offset, value.RuneCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[130]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneSetEff[]>().Serialize(ref bytes, offset, value.RuneSetEff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[131]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JukeBoxParam[]>().Serialize(ref bytes, offset, value.JukeBox, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[132]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JukeBoxSectionParam[]>().Serialize(ref bytes, offset, value.JukeBoxSection, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[133]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitSameGroupParam[]>().Serialize(ref bytes, offset, value.UnitSameGroup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[134]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AutoRepeatQuestBoxParam[]>().Serialize(ref bytes, offset, value.AutoRepeatQuestBox, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[135]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildAttendParam[]>().Serialize(ref bytes, offset, value.GuildAttend, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[136]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildAttendRewardParam[]>().Serialize(ref bytes, offset, value.GuildAttendReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[137]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRoleBonus[]>().Serialize(ref bytes, offset, value.GuildRoleBonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[138]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRoleBonusRewardParam[]>().Serialize(ref bytes, offset, value.GuildRoleBonusReward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[139]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ResetCostParam[]>().Serialize(ref bytes, offset, value.ResetCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[140]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ProtectSkillParam[]>().Serialize(ref bytes, offset, value.ProtectSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[141]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildSearchFilterParam[]>().Serialize(ref bytes, offset, value.GuildSearchFilter, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[142]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ReplaceSprite[]>().Serialize(ref bytes, offset, value.ReplaceSprite, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[143]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ExpansionPurchaseParam[]>().Serialize(ref bytes, offset, value.ExpansionPurchase, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[144]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ExpansionPurchaseQuestParam[]>().Serialize(ref bytes, offset, value.ExpansionPurchaseQuest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[145]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillAdditionalParam[]>().Serialize(ref bytes, offset, value.SkillAdditional, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[146]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillAntiShieldParam[]>().Serialize(ref bytes, offset, value.SkillAntiShield, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[147]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InitPlayer[]>().Serialize(ref bytes, offset, value.InitPlayer, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[148]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InitUnit[]>().Serialize(ref bytes, offset, value.InitUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[149]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_InitItem[]>().Serialize(ref bytes, offset, value.InitItem, formatterResolver);
      return offset - num;
    }

    public JSON_MasterParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_MasterParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_FixParam[] jsonFixParamArray = (JSON_FixParam[]) null;
      JSON_UnitParam[] jsonUnitParamArray = (JSON_UnitParam[]) null;
      JSON_UnitJobOverwriteParam[] jobOverwriteParamArray = (JSON_UnitJobOverwriteParam[]) null;
      JSON_SkillParam[] jsonSkillParamArray = (JSON_SkillParam[]) null;
      JSON_BuffEffectParam[] jsonBuffEffectParamArray = (JSON_BuffEffectParam[]) null;
      JSON_CondEffectParam[] jsonCondEffectParamArray = (JSON_CondEffectParam[]) null;
      JSON_AbilityParam[] jsonAbilityParamArray = (JSON_AbilityParam[]) null;
      JSON_ItemParam[] jsonItemParamArray = (JSON_ItemParam[]) null;
      JSON_ArtifactParam[] jsonArtifactParamArray = (JSON_ArtifactParam[]) null;
      JSON_WeaponParam[] jsonWeaponParamArray = (JSON_WeaponParam[]) null;
      JSON_RecipeParam[] jsonRecipeParamArray = (JSON_RecipeParam[]) null;
      JSON_JobParam[] jsonJobParamArray = (JSON_JobParam[]) null;
      JSON_JobSetParam[] jsonJobSetParamArray = (JSON_JobSetParam[]) null;
      JSON_EvaluationParam[] jsonEvaluationParamArray = (JSON_EvaluationParam[]) null;
      JSON_AIParam[] jsonAiParamArray = (JSON_AIParam[]) null;
      JSON_GeoParam[] jsonGeoParamArray = (JSON_GeoParam[]) null;
      JSON_RarityParam[] jsonRarityParamArray = (JSON_RarityParam[]) null;
      JSON_ShopParam[] jsonShopParamArray = (JSON_ShopParam[]) null;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int[] numArray4 = (int[]) null;
      int[] numArray5 = (int[]) null;
      JSON_PlayerParam[] jsonPlayerParamArray = (JSON_PlayerParam[]) null;
      JSON_GrowParam[] jsonGrowParamArray = (JSON_GrowParam[]) null;
      JSON_LocalNotificationParam[] notificationParamArray1 = (JSON_LocalNotificationParam[]) null;
      JSON_TrophyCategoryParam[] trophyCategoryParamArray1 = (JSON_TrophyCategoryParam[]) null;
      JSON_TrophyCategoryParam[] trophyCategoryParamArray2 = (JSON_TrophyCategoryParam[]) null;
      JSON_ChallengeCategoryParam[] challengeCategoryParamArray = (JSON_ChallengeCategoryParam[]) null;
      JSON_TrophyParam[] jsonTrophyParamArray1 = (JSON_TrophyParam[]) null;
      JSON_TrophyParam[] jsonTrophyParamArray2 = (JSON_TrophyParam[]) null;
      JSON_TrophyParam[] jsonTrophyParamArray3 = (JSON_TrophyParam[]) null;
      JSON_UnlockParam[] jsonUnlockParamArray = (JSON_UnlockParam[]) null;
      JSON_VipParam[] jsonVipParamArray = (JSON_VipParam[]) null;
      JSON_ArenaWinResult[] jsonArenaWinResultArray = (JSON_ArenaWinResult[]) null;
      JSON_ArenaResult[] jsonArenaResultArray = (JSON_ArenaResult[]) null;
      JSON_StreamingMovie[] jsonStreamingMovieArray = (JSON_StreamingMovie[]) null;
      JSON_BannerParam[] jsonBannerParamArray = (JSON_BannerParam[]) null;
      JSON_QuestClearUnlockUnitDataParam[] unlockUnitDataParamArray = (JSON_QuestClearUnlockUnitDataParam[]) null;
      JSON_AwardParam[] jsonAwardParamArray = (JSON_AwardParam[]) null;
      JSON_LoginInfoParam[] jsonLoginInfoParamArray = (JSON_LoginInfoParam[]) null;
      JSON_CollaboSkillParam[] collaboSkillParamArray = (JSON_CollaboSkillParam[]) null;
      JSON_TrickParam[] jsonTrickParamArray = (JSON_TrickParam[]) null;
      JSON_BreakObjParam[] jsonBreakObjParamArray = (JSON_BreakObjParam[]) null;
      JSON_VersusMatchingParam[] versusMatchingParamArray = (JSON_VersusMatchingParam[]) null;
      JSON_VersusMatchCondParam[] versusMatchCondParamArray = (JSON_VersusMatchCondParam[]) null;
      JSON_TowerScore[] jsonTowerScoreArray = (JSON_TowerScore[]) null;
      int[] numArray6 = (int[]) null;
      int[] numArray7 = (int[]) null;
      JSON_FriendPresentItemParam[] presentItemParamArray = (JSON_FriendPresentItemParam[]) null;
      JSON_WeatherParam[] jsonWeatherParamArray = (JSON_WeatherParam[]) null;
      JSON_UnitUnlockTimeParam[] unitUnlockTimeParamArray = (JSON_UnitUnlockTimeParam[]) null;
      JSON_TobiraParam[] jsonTobiraParamArray = (JSON_TobiraParam[]) null;
      JSON_TobiraCategoriesParam[] tobiraCategoriesParamArray = (JSON_TobiraCategoriesParam[]) null;
      JSON_TobiraCondsParam[] tobiraCondsParamArray = (JSON_TobiraCondsParam[]) null;
      JSON_TobiraCondsUnitParam[] tobiraCondsUnitParamArray = (JSON_TobiraCondsUnitParam[]) null;
      JSON_TobiraRecipeParam[] tobiraRecipeParamArray = (JSON_TobiraRecipeParam[]) null;
      JSON_ConceptCardParam[] conceptCardParamArray1 = (JSON_ConceptCardParam[]) null;
      int[] numArray8 = (int[]) null;
      int[] numArray9 = (int[]) null;
      int[] numArray10 = (int[]) null;
      int[] numArray11 = (int[]) null;
      int[] numArray12 = (int[]) null;
      int[] numArray13 = (int[]) null;
      JSON_ConceptCardConditionsParam[] cardConditionsParamArray = (JSON_ConceptCardConditionsParam[]) null;
      JSON_ConceptCardTrustRewardParam[] trustRewardParamArray = (JSON_ConceptCardTrustRewardParam[]) null;
      int[] numArray14 = (int[]) null;
      JSON_ConceptCardLsBuffCoefParam[] cardLsBuffCoefParamArray = (JSON_ConceptCardLsBuffCoefParam[]) null;
      JSON_ConceptCardGroup[] conceptCardGroupArray = (JSON_ConceptCardGroup[]) null;
      JSON_ConceptLimitUpItem[] conceptLimitUpItemArray = (JSON_ConceptLimitUpItem[]) null;
      JSON_UnitGroupParam[] jsonUnitGroupParamArray = (JSON_UnitGroupParam[]) null;
      JSON_JobGroupParam[] jsonJobGroupParamArray = (JSON_JobGroupParam[]) null;
      JSON_StatusCoefficientParam[] coefficientParamArray = (JSON_StatusCoefficientParam[]) null;
      JSON_CustomTargetParam[] customTargetParamArray = (JSON_CustomTargetParam[]) null;
      JSON_SkillAbilityDeriveParam[] abilityDeriveParamArray = (JSON_SkillAbilityDeriveParam[]) null;
      JSON_RaidPeriodParam[] jsonRaidPeriodParamArray = (JSON_RaidPeriodParam[]) null;
      JSON_RaidPeriodTimeParam[] raidPeriodTimeParamArray = (JSON_RaidPeriodTimeParam[]) null;
      JSON_RaidAreaParam[] jsonRaidAreaParamArray = (JSON_RaidAreaParam[]) null;
      JSON_RaidBossParam[] jsonRaidBossParamArray = (JSON_RaidBossParam[]) null;
      JSON_RaidBattleRewardParam[] battleRewardParamArray = (JSON_RaidBattleRewardParam[]) null;
      JSON_RaidBeatRewardParam[] raidBeatRewardParamArray = (JSON_RaidBeatRewardParam[]) null;
      JSON_RaidDamageRatioRewardParam[] ratioRewardParamArray = (JSON_RaidDamageRatioRewardParam[]) null;
      JSON_RaidDamageAmountRewardParam[] amountRewardParamArray = (JSON_RaidDamageAmountRewardParam[]) null;
      JSON_RaidAreaClearRewardParam[] clearRewardParamArray = (JSON_RaidAreaClearRewardParam[]) null;
      JSON_RaidCompleteRewardParam[] completeRewardParamArray = (JSON_RaidCompleteRewardParam[]) null;
      JSON_RaidRewardParam[] jsonRaidRewardParamArray = (JSON_RaidRewardParam[]) null;
      JSON_TipsParam[] jsonTipsParamArray = (JSON_TipsParam[]) null;
      JSON_GuildEmblemParam[] guildEmblemParamArray = (JSON_GuildEmblemParam[]) null;
      JSON_GuildFacilityParam[] guildFacilityParamArray = (JSON_GuildFacilityParam[]) null;
      JSON_GuildFacilityLvParam[] guildFacilityLvParamArray = (JSON_GuildFacilityLvParam[]) null;
      JSON_ConvertUnitPieceExcludeParam[] pieceExcludeParamArray = (JSON_ConvertUnitPieceExcludeParam[]) null;
      JSON_PremiumParam[] jsonPremiumParamArray = (JSON_PremiumParam[]) null;
      JSON_BuyCoinShopParam[] buyCoinShopParamArray = (JSON_BuyCoinShopParam[]) null;
      JSON_BuyCoinProductParam[] coinProductParamArray = (JSON_BuyCoinProductParam[]) null;
      JSON_BuyCoinRewardParam[] buyCoinRewardParamArray = (JSON_BuyCoinRewardParam[]) null;
      JSON_BuyCoinProductConvertParam[] productConvertParamArray = (JSON_BuyCoinProductConvertParam[]) null;
      JSON_DynamicTransformUnitParam[] transformUnitParamArray = (JSON_DynamicTransformUnitParam[]) null;
      JSON_RecommendedArtifactParam[] recommendedArtifactParamArray = (JSON_RecommendedArtifactParam[]) null;
      JSON_SkillMotionParam[] skillMotionParamArray = (JSON_SkillMotionParam[]) null;
      JSON_DependStateSpcEffParam[] stateSpcEffParamArray = (JSON_DependStateSpcEffParam[]) null;
      JSON_InspSkillParam[] jsonInspSkillParamArray = (JSON_InspSkillParam[]) null;
      JSON_InspSkillTriggerParam[] skillTriggerParamArray = (JSON_InspSkillTriggerParam[]) null;
      JSON_InspSkillCostParam[] inspSkillCostParamArray1 = (JSON_InspSkillCostParam[]) null;
      JSON_InspSkillCostParam[] inspSkillCostParamArray2 = (JSON_InspSkillCostParam[]) null;
      JSON_InspSkillLvUpCostParam[] skillLvUpCostParamArray = (JSON_InspSkillLvUpCostParam[]) null;
      JSON_HighlightParam[] jsonHighlightParamArray = (JSON_HighlightParam[]) null;
      JSON_HighlightGift[] jsonHighlightGiftArray = (JSON_HighlightGift[]) null;
      JSON_GenesisParam[] jsonGenesisParamArray = (JSON_GenesisParam[]) null;
      JSON_CoinBuyUseBonusParam[] buyUseBonusParamArray = (JSON_CoinBuyUseBonusParam[]) null;
      JSON_CoinBuyUseBonusRewardSetParam[] bonusRewardSetParamArray = (JSON_CoinBuyUseBonusRewardSetParam[]) null;
      JSON_CoinBuyUseBonusRewardParam[] bonusRewardParamArray1 = (JSON_CoinBuyUseBonusRewardParam[]) null;
      JSON_UnitRentalNotificationParam[] notificationParamArray2 = (JSON_UnitRentalNotificationParam[]) null;
      JSON_UnitRentalParam[] jsonUnitRentalParamArray = (JSON_UnitRentalParam[]) null;
      JSON_DrawCardRewardParam[] drawCardRewardParamArray = (JSON_DrawCardRewardParam[]) null;
      JSON_DrawCardParam[] jsonDrawCardParamArray = (JSON_DrawCardParam[]) null;
      JSON_TrophyStarMissionRewardParam[] missionRewardParamArray = (JSON_TrophyStarMissionRewardParam[]) null;
      JSON_TrophyStarMissionParam[] starMissionParamArray = (JSON_TrophyStarMissionParam[]) null;
      JSON_UnitPieceShopParam[] unitPieceShopParamArray = (JSON_UnitPieceShopParam[]) null;
      JSON_UnitPieceShopGroupParam[] pieceShopGroupParamArray = (JSON_UnitPieceShopGroupParam[]) null;
      JSON_TwitterMessageParam[] twitterMessageParamArray = (JSON_TwitterMessageParam[]) null;
      JSON_FilterConceptCardParam[] conceptCardParamArray2 = (JSON_FilterConceptCardParam[]) null;
      JSON_FilterRuneParam[] jsonFilterRuneParamArray = (JSON_FilterRuneParam[]) null;
      JSON_FilterUnitParam[] jsonFilterUnitParamArray = (JSON_FilterUnitParam[]) null;
      JSON_FilterArtifactParam[] filterArtifactParamArray = (JSON_FilterArtifactParam[]) null;
      JSON_SortRuneParam[] jsonSortRuneParamArray = (JSON_SortRuneParam[]) null;
      JSON_RuneParam[] jsonRuneParamArray = (JSON_RuneParam[]) null;
      JSON_RuneLotteryBaseState[] lotteryBaseStateArray = (JSON_RuneLotteryBaseState[]) null;
      JSON_RuneLotteryEvoState[] runeLotteryEvoStateArray = (JSON_RuneLotteryEvoState[]) null;
      JSON_RuneMaterial[] jsonRuneMaterialArray = (JSON_RuneMaterial[]) null;
      JSON_RuneCost[] jsonRuneCostArray = (JSON_RuneCost[]) null;
      JSON_RuneSetEff[] jsonRuneSetEffArray = (JSON_RuneSetEff[]) null;
      JSON_JukeBoxParam[] jsonJukeBoxParamArray = (JSON_JukeBoxParam[]) null;
      JSON_JukeBoxSectionParam[] jukeBoxSectionParamArray = (JSON_JukeBoxSectionParam[]) null;
      JSON_UnitSameGroupParam[] unitSameGroupParamArray = (JSON_UnitSameGroupParam[]) null;
      JSON_AutoRepeatQuestBoxParam[] repeatQuestBoxParamArray = (JSON_AutoRepeatQuestBoxParam[]) null;
      JSON_GuildAttendParam[] guildAttendParamArray = (JSON_GuildAttendParam[]) null;
      JSON_GuildAttendRewardParam[] attendRewardParamArray = (JSON_GuildAttendRewardParam[]) null;
      JSON_GuildRoleBonus[] jsonGuildRoleBonusArray = (JSON_GuildRoleBonus[]) null;
      JSON_GuildRoleBonusRewardParam[] bonusRewardParamArray2 = (JSON_GuildRoleBonusRewardParam[]) null;
      JSON_ResetCostParam[] jsonResetCostParamArray = (JSON_ResetCostParam[]) null;
      JSON_ProtectSkillParam[] protectSkillParamArray = (JSON_ProtectSkillParam[]) null;
      JSON_GuildSearchFilterParam[] searchFilterParamArray = (JSON_GuildSearchFilterParam[]) null;
      JSON_ReplaceSprite[] jsonReplaceSpriteArray = (JSON_ReplaceSprite[]) null;
      JSON_ExpansionPurchaseParam[] expansionPurchaseParamArray = (JSON_ExpansionPurchaseParam[]) null;
      JSON_ExpansionPurchaseQuestParam[] purchaseQuestParamArray = (JSON_ExpansionPurchaseQuestParam[]) null;
      JSON_SkillAdditionalParam[] skillAdditionalParamArray = (JSON_SkillAdditionalParam[]) null;
      JSON_SkillAntiShieldParam[] skillAntiShieldParamArray = (JSON_SkillAntiShieldParam[]) null;
      JSON_InitPlayer[] jsonInitPlayerArray = (JSON_InitPlayer[]) null;
      JSON_InitUnit[] jsonInitUnitArray = (JSON_InitUnit[]) null;
      JSON_InitItem[] jsonInitItemArray = (JSON_InitItem[]) null;
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
              jsonFixParamArray = formatterResolver.GetFormatterWithVerify<JSON_FixParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jobOverwriteParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitJobOverwriteParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonSkillParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonBuffEffectParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuffEffectParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonCondEffectParamArray = formatterResolver.GetFormatterWithVerify<JSON_CondEffectParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonAbilityParamArray = formatterResolver.GetFormatterWithVerify<JSON_AbilityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonItemParamArray = formatterResolver.GetFormatterWithVerify<JSON_ItemParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonArtifactParamArray = formatterResolver.GetFormatterWithVerify<JSON_ArtifactParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonWeaponParamArray = formatterResolver.GetFormatterWithVerify<JSON_WeaponParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonRecipeParamArray = formatterResolver.GetFormatterWithVerify<JSON_RecipeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              jsonJobParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              jsonJobSetParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobSetParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              jsonEvaluationParamArray = formatterResolver.GetFormatterWithVerify<JSON_EvaluationParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              jsonAiParamArray = formatterResolver.GetFormatterWithVerify<JSON_AIParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              jsonGeoParamArray = formatterResolver.GetFormatterWithVerify<JSON_GeoParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              jsonRarityParamArray = formatterResolver.GetFormatterWithVerify<JSON_RarityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              jsonShopParamArray = formatterResolver.GetFormatterWithVerify<JSON_ShopParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              numArray5 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              jsonPlayerParamArray = formatterResolver.GetFormatterWithVerify<JSON_PlayerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              jsonGrowParamArray = formatterResolver.GetFormatterWithVerify<JSON_GrowParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              notificationParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_LocalNotificationParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              trophyCategoryParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyCategoryParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              trophyCategoryParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyCategoryParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              challengeCategoryParamArray = formatterResolver.GetFormatterWithVerify<JSON_ChallengeCategoryParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              jsonTrophyParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              jsonTrophyParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              jsonTrophyParamArray3 = formatterResolver.GetFormatterWithVerify<JSON_TrophyParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              jsonUnlockParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnlockParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              jsonVipParamArray = formatterResolver.GetFormatterWithVerify<JSON_VipParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              jsonArenaWinResultArray = formatterResolver.GetFormatterWithVerify<JSON_ArenaWinResult[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              jsonArenaResultArray = formatterResolver.GetFormatterWithVerify<JSON_ArenaResult[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              jsonStreamingMovieArray = formatterResolver.GetFormatterWithVerify<JSON_StreamingMovie[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              jsonBannerParamArray = formatterResolver.GetFormatterWithVerify<JSON_BannerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              unlockUnitDataParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestClearUnlockUnitDataParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              jsonAwardParamArray = formatterResolver.GetFormatterWithVerify<JSON_AwardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 40:
              jsonLoginInfoParamArray = formatterResolver.GetFormatterWithVerify<JSON_LoginInfoParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              collaboSkillParamArray = formatterResolver.GetFormatterWithVerify<JSON_CollaboSkillParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              jsonTrickParamArray = formatterResolver.GetFormatterWithVerify<JSON_TrickParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              jsonBreakObjParamArray = formatterResolver.GetFormatterWithVerify<JSON_BreakObjParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 44:
              versusMatchingParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusMatchingParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 45:
              versusMatchCondParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusMatchCondParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 46:
              jsonTowerScoreArray = formatterResolver.GetFormatterWithVerify<JSON_TowerScore[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              numArray6 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              numArray7 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              presentItemParamArray = formatterResolver.GetFormatterWithVerify<JSON_FriendPresentItemParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              jsonWeatherParamArray = formatterResolver.GetFormatterWithVerify<JSON_WeatherParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              unitUnlockTimeParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitUnlockTimeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 52:
              jsonTobiraParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 53:
              tobiraCategoriesParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraCategoriesParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              tobiraCondsParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              tobiraCondsUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsUnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              tobiraRecipeParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraRecipeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 57:
              conceptCardParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_ConceptCardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 58:
              numArray8 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 59:
              numArray9 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 60:
              numArray10 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 61:
              numArray11 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 62:
              numArray12 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 63:
              numArray13 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 64:
              cardConditionsParamArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCardConditionsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 65:
              trustRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCardTrustRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 66:
              numArray14 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 67:
              cardLsBuffCoefParamArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCardLsBuffCoefParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 68:
              conceptCardGroupArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCardGroup[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 69:
              conceptLimitUpItemArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptLimitUpItem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 70:
              jsonUnitGroupParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitGroupParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 71:
              jsonJobGroupParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobGroupParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              coefficientParamArray = formatterResolver.GetFormatterWithVerify<JSON_StatusCoefficientParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              customTargetParamArray = formatterResolver.GetFormatterWithVerify<JSON_CustomTargetParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 74:
              abilityDeriveParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillAbilityDeriveParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 75:
              jsonRaidPeriodParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidPeriodParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 76:
              raidPeriodTimeParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidPeriodTimeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 77:
              jsonRaidAreaParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidAreaParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 78:
              jsonRaidBossParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidBossParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              battleRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidBattleRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 80:
              raidBeatRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidBeatRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 81:
              ratioRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidDamageRatioRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 82:
              amountRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidDamageAmountRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 83:
              clearRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidAreaClearRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 84:
              completeRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidCompleteRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 85:
              jsonRaidRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_RaidRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 86:
              jsonTipsParamArray = formatterResolver.GetFormatterWithVerify<JSON_TipsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 87:
              guildEmblemParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildEmblemParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 88:
              guildFacilityParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 89:
              guildFacilityLvParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityLvParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 90:
              pieceExcludeParamArray = formatterResolver.GetFormatterWithVerify<JSON_ConvertUnitPieceExcludeParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 91:
              jsonPremiumParamArray = formatterResolver.GetFormatterWithVerify<JSON_PremiumParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 92:
              buyCoinShopParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuyCoinShopParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 93:
              coinProductParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuyCoinProductParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 94:
              buyCoinRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuyCoinRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 95:
              productConvertParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuyCoinProductConvertParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 96:
              transformUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_DynamicTransformUnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 97:
              recommendedArtifactParamArray = formatterResolver.GetFormatterWithVerify<JSON_RecommendedArtifactParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 98:
              skillMotionParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillMotionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 99:
              stateSpcEffParamArray = formatterResolver.GetFormatterWithVerify<JSON_DependStateSpcEffParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 100:
              jsonInspSkillParamArray = formatterResolver.GetFormatterWithVerify<JSON_InspSkillParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 101:
              skillTriggerParamArray = formatterResolver.GetFormatterWithVerify<JSON_InspSkillTriggerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 102:
              inspSkillCostParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_InspSkillCostParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 103:
              inspSkillCostParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_InspSkillCostParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 104:
              skillLvUpCostParamArray = formatterResolver.GetFormatterWithVerify<JSON_InspSkillLvUpCostParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 105:
              jsonHighlightParamArray = formatterResolver.GetFormatterWithVerify<JSON_HighlightParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 106:
              jsonHighlightGiftArray = formatterResolver.GetFormatterWithVerify<JSON_HighlightGift[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 107:
              jsonGenesisParamArray = formatterResolver.GetFormatterWithVerify<JSON_GenesisParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 108:
              buyUseBonusParamArray = formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 109:
              bonusRewardSetParamArray = formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusRewardSetParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 110:
              bonusRewardParamArray1 = formatterResolver.GetFormatterWithVerify<JSON_CoinBuyUseBonusRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 111:
              notificationParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_UnitRentalNotificationParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 112:
              jsonUnitRentalParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitRentalParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 113:
              drawCardRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_DrawCardRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 114:
              jsonDrawCardParamArray = formatterResolver.GetFormatterWithVerify<JSON_DrawCardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 115:
              missionRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_TrophyStarMissionRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 116:
              starMissionParamArray = formatterResolver.GetFormatterWithVerify<JSON_TrophyStarMissionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 117:
              unitPieceShopParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitPieceShopParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 118:
              pieceShopGroupParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitPieceShopGroupParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 119:
              twitterMessageParamArray = formatterResolver.GetFormatterWithVerify<JSON_TwitterMessageParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 120:
              conceptCardParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_FilterConceptCardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 121:
              jsonFilterRuneParamArray = formatterResolver.GetFormatterWithVerify<JSON_FilterRuneParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 122:
              jsonFilterUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_FilterUnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 123:
              filterArtifactParamArray = formatterResolver.GetFormatterWithVerify<JSON_FilterArtifactParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 124:
              jsonSortRuneParamArray = formatterResolver.GetFormatterWithVerify<JSON_SortRuneParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 125:
              jsonRuneParamArray = formatterResolver.GetFormatterWithVerify<JSON_RuneParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 126:
              lotteryBaseStateArray = formatterResolver.GetFormatterWithVerify<JSON_RuneLotteryBaseState[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case (int) sbyte.MaxValue:
              runeLotteryEvoStateArray = formatterResolver.GetFormatterWithVerify<JSON_RuneLotteryEvoState[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 128:
              jsonRuneMaterialArray = formatterResolver.GetFormatterWithVerify<JSON_RuneMaterial[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 129:
              jsonRuneCostArray = formatterResolver.GetFormatterWithVerify<JSON_RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 130:
              jsonRuneSetEffArray = formatterResolver.GetFormatterWithVerify<JSON_RuneSetEff[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 131:
              jsonJukeBoxParamArray = formatterResolver.GetFormatterWithVerify<JSON_JukeBoxParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 132:
              jukeBoxSectionParamArray = formatterResolver.GetFormatterWithVerify<JSON_JukeBoxSectionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 133:
              unitSameGroupParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitSameGroupParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 134:
              repeatQuestBoxParamArray = formatterResolver.GetFormatterWithVerify<JSON_AutoRepeatQuestBoxParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 135:
              guildAttendParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildAttendParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 136:
              attendRewardParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildAttendRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 137:
              jsonGuildRoleBonusArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRoleBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 138:
              bonusRewardParamArray2 = formatterResolver.GetFormatterWithVerify<JSON_GuildRoleBonusRewardParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 139:
              jsonResetCostParamArray = formatterResolver.GetFormatterWithVerify<JSON_ResetCostParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 140:
              protectSkillParamArray = formatterResolver.GetFormatterWithVerify<JSON_ProtectSkillParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 141:
              searchFilterParamArray = formatterResolver.GetFormatterWithVerify<JSON_GuildSearchFilterParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 142:
              jsonReplaceSpriteArray = formatterResolver.GetFormatterWithVerify<JSON_ReplaceSprite[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 143:
              expansionPurchaseParamArray = formatterResolver.GetFormatterWithVerify<JSON_ExpansionPurchaseParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 144:
              purchaseQuestParamArray = formatterResolver.GetFormatterWithVerify<JSON_ExpansionPurchaseQuestParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 145:
              skillAdditionalParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillAdditionalParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 146:
              skillAntiShieldParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillAntiShieldParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 147:
              jsonInitPlayerArray = formatterResolver.GetFormatterWithVerify<JSON_InitPlayer[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 148:
              jsonInitUnitArray = formatterResolver.GetFormatterWithVerify<JSON_InitUnit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 149:
              jsonInitItemArray = formatterResolver.GetFormatterWithVerify<JSON_InitItem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_MasterParam()
      {
        Fix = jsonFixParamArray,
        Unit = jsonUnitParamArray,
        UnitJobOverwrite = jobOverwriteParamArray,
        Skill = jsonSkillParamArray,
        Buff = jsonBuffEffectParamArray,
        Cond = jsonCondEffectParamArray,
        Ability = jsonAbilityParamArray,
        Item = jsonItemParamArray,
        Artifact = jsonArtifactParamArray,
        Weapon = jsonWeaponParamArray,
        Recipe = jsonRecipeParamArray,
        Job = jsonJobParamArray,
        JobSet = jsonJobSetParamArray,
        Evaluation = jsonEvaluationParamArray,
        AI = jsonAiParamArray,
        Geo = jsonGeoParamArray,
        Rarity = jsonRarityParamArray,
        Shop = jsonShopParamArray,
        AbilityRank = numArray1,
        UnitLvTbl = numArray2,
        PlayerLvTbl = numArray3,
        ArtifactLvTbl = numArray4,
        AwakePieceTbl = numArray5,
        Player = jsonPlayerParamArray,
        Grow = jsonGrowParamArray,
        LocalNotification = notificationParamArray1,
        TrophyCategory = trophyCategoryParamArray1,
        GuildTrophyCategory = trophyCategoryParamArray2,
        ChallengeCategory = challengeCategoryParamArray,
        Trophy = jsonTrophyParamArray1,
        Challenge = jsonTrophyParamArray2,
        GuildTrophy = jsonTrophyParamArray3,
        Unlock = jsonUnlockParamArray,
        Vip = jsonVipParamArray,
        ArenaWinResult = jsonArenaWinResultArray,
        ArenaRankResult = jsonArenaResultArray,
        Mov = jsonStreamingMovieArray,
        Banner = jsonBannerParamArray,
        QuestClearUnlockUnitData = unlockUnitDataParamArray,
        Award = jsonAwardParamArray,
        LoginInfo = jsonLoginInfoParamArray,
        CollaboSkill = collaboSkillParamArray,
        Trick = jsonTrickParamArray,
        BreakObj = jsonBreakObjParamArray,
        VersusMatchKey = versusMatchingParamArray,
        VersusMatchCond = versusMatchCondParamArray,
        TowerScore = jsonTowerScoreArray,
        TowerRank = numArray6,
        MultilimitUnitLv = numArray7,
        FriendPresentItem = presentItemParamArray,
        Weather = jsonWeatherParamArray,
        UnitUnlockTime = unitUnlockTimeParamArray,
        Tobira = jsonTobiraParamArray,
        TobiraCategories = tobiraCategoriesParamArray,
        TobiraConds = tobiraCondsParamArray,
        TobiraCondsUnit = tobiraCondsUnitParamArray,
        TobiraRecipe = tobiraRecipeParamArray,
        ConceptCard = conceptCardParamArray1,
        ConceptCardLvTbl1 = numArray8,
        ConceptCardLvTbl2 = numArray9,
        ConceptCardLvTbl3 = numArray10,
        ConceptCardLvTbl4 = numArray11,
        ConceptCardLvTbl5 = numArray12,
        ConceptCardLvTbl6 = numArray13,
        ConceptCardConditions = cardConditionsParamArray,
        ConceptCardTrustReward = trustRewardParamArray,
        ConceptCardSellCoinRate = numArray14,
        ConceptCardLsBuffCoef = cardLsBuffCoefParamArray,
        ConceptCardGroup = conceptCardGroupArray,
        ConceptLimitUpItem = conceptLimitUpItemArray,
        UnitGroup = jsonUnitGroupParamArray,
        JobGroup = jsonJobGroupParamArray,
        StatusCoefficient = coefficientParamArray,
        CustomTarget = customTargetParamArray,
        SkillAbilityDerive = abilityDeriveParamArray,
        RaidPeriod = jsonRaidPeriodParamArray,
        RaidPeriodTime = raidPeriodTimeParamArray,
        RaidArea = jsonRaidAreaParamArray,
        RaidBoss = jsonRaidBossParamArray,
        RaidBattleReward = battleRewardParamArray,
        RaidBeatReward = raidBeatRewardParamArray,
        RaidDamageRatioReward = ratioRewardParamArray,
        RaidDamageAmountReward = amountRewardParamArray,
        RaidAreaClearReward = clearRewardParamArray,
        RaidCompleteReward = completeRewardParamArray,
        RaidReward = jsonRaidRewardParamArray,
        Tips = jsonTipsParamArray,
        GuildEmblem = guildEmblemParamArray,
        GuildFacility = guildFacilityParamArray,
        GuildFacilityLvTbl = guildFacilityLvParamArray,
        ConvertUnitPieceExclude = pieceExcludeParamArray,
        Premium = jsonPremiumParamArray,
        BuyCoinShop = buyCoinShopParamArray,
        BuyCoinProduct = coinProductParamArray,
        BuyCoinReward = buyCoinRewardParamArray,
        BuyCoinProductConvert = productConvertParamArray,
        DynamicTransformUnit = transformUnitParamArray,
        RecommendedArtifact = recommendedArtifactParamArray,
        SkillMotion = skillMotionParamArray,
        DependStateSpcEff = stateSpcEffParamArray,
        InspirationSkill = jsonInspSkillParamArray,
        InspSkillTrigger = skillTriggerParamArray,
        InspSkillOpenCost = inspSkillCostParamArray1,
        InspSkillResetCost = inspSkillCostParamArray2,
        InspSkillLvUpCost = skillLvUpCostParamArray,
        Highlight = jsonHighlightParamArray,
        HighlightGift = jsonHighlightGiftArray,
        Genesis = jsonGenesisParamArray,
        CoinBuyUseBonus = buyUseBonusParamArray,
        CoinBuyUseBonusRewardSet = bonusRewardSetParamArray,
        CoinBuyUseBonusReward = bonusRewardParamArray1,
        UnitRentalNotification = notificationParamArray2,
        UnitRental = jsonUnitRentalParamArray,
        DrawCardReward = drawCardRewardParamArray,
        DrawCard = jsonDrawCardParamArray,
        TrophyStarMissionReward = missionRewardParamArray,
        TrophyStarMission = starMissionParamArray,
        UnitPieceShop = unitPieceShopParamArray,
        UnitPieceShopGroup = pieceShopGroupParamArray,
        TwitterMessage = twitterMessageParamArray,
        FilterConceptCard = conceptCardParamArray2,
        FilterRune = jsonFilterRuneParamArray,
        FilterUnit = jsonFilterUnitParamArray,
        FilterArtifact = filterArtifactParamArray,
        SortRune = jsonSortRuneParamArray,
        Rune = jsonRuneParamArray,
        RuneLotteryBaseState = lotteryBaseStateArray,
        RuneLotteryEvoState = runeLotteryEvoStateArray,
        RuneMaterial = jsonRuneMaterialArray,
        RuneCost = jsonRuneCostArray,
        RuneSetEff = jsonRuneSetEffArray,
        JukeBox = jsonJukeBoxParamArray,
        JukeBoxSection = jukeBoxSectionParamArray,
        UnitSameGroup = unitSameGroupParamArray,
        AutoRepeatQuestBox = repeatQuestBoxParamArray,
        GuildAttend = guildAttendParamArray,
        GuildAttendReward = attendRewardParamArray,
        GuildRoleBonus = jsonGuildRoleBonusArray,
        GuildRoleBonusReward = bonusRewardParamArray2,
        ResetCost = jsonResetCostParamArray,
        ProtectSkill = protectSkillParamArray,
        GuildSearchFilter = searchFilterParamArray,
        ReplaceSprite = jsonReplaceSpriteArray,
        ExpansionPurchase = expansionPurchaseParamArray,
        ExpansionPurchaseQuest = purchaseQuestParamArray,
        SkillAdditional = skillAdditionalParamArray,
        SkillAntiShield = skillAntiShieldParamArray,
        InitPlayer = jsonInitPlayerArray,
        InitUnit = jsonInitUnitArray,
        InitItem = jsonInitItemArray
      };
    }
  }
}
