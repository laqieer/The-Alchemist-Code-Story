// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.GeneratedResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Formatters.SRPG;
using SRPG;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Resolvers
{
  internal static class GeneratedResolverGetFormatterHelper
  {
    private static readonly Dictionary<System.Type, int> lookup = new Dictionary<System.Type, int>(1289)
    {
      {
        typeof (List<int>),
        0
      },
      {
        typeof (EquipSkillSetting[]),
        1
      },
      {
        typeof (EquipAbilitySetting[]),
        2
      },
      {
        typeof (List<AIAction>),
        3
      },
      {
        typeof (AIPatrolPoint[]),
        4
      },
      {
        typeof (List<OString>),
        5
      },
      {
        typeof (List<UnitEntryTrigger>),
        6
      },
      {
        typeof (List<MultiPlayResumeBuff.ResistStatus>),
        7
      },
      {
        typeof (MultiPlayResumeAbilChg.Data[]),
        8
      },
      {
        typeof (MultiPlayResumeBuff[]),
        9
      },
      {
        typeof (MultiPlayResumeShield[]),
        10
      },
      {
        typeof (MultiPlayResumeMhmDmg[]),
        11
      },
      {
        typeof (MultiPlayResumeFtgt[]),
        12
      },
      {
        typeof (MultiPlayResumeAbilChg[]),
        13
      },
      {
        typeof (MultiPlayResumeAddedAbil[]),
        14
      },
      {
        typeof (List<MultiPlayResumeProtect>),
        15
      },
      {
        typeof (MultiPlayResumeUnitData[]),
        16
      },
      {
        typeof (MultiPlayGimmickEventParam[]),
        17
      },
      {
        typeof (MultiPlayTrickParam[]),
        18
      },
      {
        typeof (List<AttackDetailTypes>),
        19
      },
      {
        typeof (List<string>),
        20
      },
      {
        typeof (List<QuestClearUnlockUnitDataParam>),
        21
      },
      {
        typeof (OShort[]),
        22
      },
      {
        typeof (List<TokkouValue>),
        23
      },
      {
        typeof (RecipeItem[]),
        24
      },
      {
        typeof (ReturnItem[]),
        25
      },
      {
        typeof (RarityEquipEnhanceParam.RankParam[]),
        26
      },
      {
        typeof (EquipData[]),
        27
      },
      {
        typeof (List<AbilityData>),
        28
      },
      {
        typeof (ConceptCardEffectsParam[]),
        29
      },
      {
        typeof (List<ConceptLimitUpItemParam>),
        30
      },
      {
        typeof (BuffEffectParam.Buff[]),
        31
      },
      {
        typeof (List<BuffEffect.BuffTarget>),
        32
      },
      {
        typeof (List<ConceptCardEquipEffect>),
        33
      },
      {
        typeof (ConceptCardData[]),
        34
      },
      {
        typeof (List<RuneBuffDataEvoState>),
        35
      },
      {
        typeof (RuneData[]),
        36
      },
      {
        typeof (BuffEffect.BuffValues[]),
        37
      },
      {
        typeof (OString[]),
        38
      },
      {
        typeof (JobRankParam[]),
        39
      },
      {
        typeof (LearningSkill[]),
        40
      },
      {
        typeof (List<InspSkillTriggerParam.TriggerData>),
        41
      },
      {
        typeof (List<InspSkillTriggerParam>),
        42
      },
      {
        typeof (List<InspSkillParam>),
        43
      },
      {
        typeof (List<InspirationSkillData>),
        44
      },
      {
        typeof (ArtifactData[]),
        45
      },
      {
        typeof (TobiraLearnAbilityParam[]),
        46
      },
      {
        typeof (List<TobiraData>),
        47
      },
      {
        typeof (JobData[]),
        48
      },
      {
        typeof (QuestClearUnlockUnitDataParam[]),
        49
      },
      {
        typeof (List<SkillDeriveParam>),
        50
      },
      {
        typeof (List<AbilityDeriveParam>),
        51
      },
      {
        typeof (SkillAbilityDeriveTriggerParam[]),
        52
      },
      {
        typeof (EUnitCondition[]),
        53
      },
      {
        typeof (List<SkillData>),
        54
      },
      {
        typeof (List<Unit>),
        55
      },
      {
        typeof (List<BuffAttachment.ResistStatusBuff>),
        56
      },
      {
        typeof (List<BuffAttachment>),
        57
      },
      {
        typeof (List<CondAttachment>),
        58
      },
      {
        typeof (SkillCategory[]),
        59
      },
      {
        typeof (ParamTypes[]),
        60
      },
      {
        typeof (List<Unit.DropItem>),
        61
      },
      {
        typeof (List<Unit.UnitShield>),
        62
      },
      {
        typeof (List<Unit.UnitProtect>),
        63
      },
      {
        typeof (List<Unit.UnitMhmDamage>),
        64
      },
      {
        typeof (List<Unit.UnitInsp>),
        65
      },
      {
        typeof (List<Unit.UnitForcedTargeting>),
        66
      },
      {
        typeof (List<Unit.AbilityChange.Data>),
        67
      },
      {
        typeof (List<Unit.AbilityChange>),
        68
      },
      {
        typeof (Json_InspirationSkill[]),
        69
      },
      {
        typeof (Json_CollaboSkill[]),
        70
      },
      {
        typeof (Json_Equip[]),
        71
      },
      {
        typeof (Json_Ability[]),
        72
      },
      {
        typeof (Json_Artifact[]),
        73
      },
      {
        typeof (Json_InspirationSkillExt[]),
        74
      },
      {
        typeof (Json_Job[]),
        75
      },
      {
        typeof (Json_UnitJob[]),
        76
      },
      {
        typeof (JSON_ConceptCard[]),
        77
      },
      {
        typeof (Json_Tobira[]),
        78
      },
      {
        typeof (Json_RuneBuffData[]),
        79
      },
      {
        typeof (Json_RuneData[]),
        80
      },
      {
        typeof (JSON_GuildRaidMailListItem[]),
        81
      },
      {
        typeof (Json_Unit[]),
        82
      },
      {
        typeof (JSON_GuildRaidRankingMemberBoss[]),
        83
      },
      {
        typeof (Json_Item[]),
        84
      },
      {
        typeof (Json_Gift[]),
        85
      },
      {
        typeof (Json_Mail[]),
        86
      },
      {
        typeof (Json_Party[]),
        87
      },
      {
        typeof (Json_Friend[]),
        88
      },
      {
        typeof (Json_Skin[]),
        89
      },
      {
        typeof (Json_LoginBonus[]),
        90
      },
      {
        typeof (Json_PremiumLoginBonusItem[]),
        91
      },
      {
        typeof (Json_PremiumLoginBonus[]),
        92
      },
      {
        typeof (Json_LoginBonusTable[]),
        93
      },
      {
        typeof (Json_MultiFuids[]),
        94
      },
      {
        typeof (Json_VersusCount[]),
        95
      },
      {
        typeof (Json_ExpireItem[]),
        96
      },
      {
        typeof (JSON_TrophyProgress[]),
        97
      },
      {
        typeof (JSON_UnitOverWriteData[]),
        98
      },
      {
        typeof (JSON_PartyOverWrite[]),
        99
      },
      {
        typeof (Json_ExpansionPurchase[]),
        100
      },
      {
        typeof (Json_TrophyConceptCard[]),
        101
      },
      {
        typeof (JSON_SupportRanking[]),
        102
      },
      {
        typeof (JSON_SupportUnitRanking[]),
        103
      },
      {
        typeof (BattleCore.Json_BtlDrop[]),
        104
      },
      {
        typeof (BattleCore.Json_BtlDrop[][]),
        105
      },
      {
        typeof (Json_BtlRewardConceptCard[]),
        106
      },
      {
        typeof (JSON_QuestProgress[]),
        107
      },
      {
        typeof (JSON_CombatPowerRankingViewGuild[]),
        108
      },
      {
        typeof (JSON_CombatPowerRankingGuildMember[]),
        109
      },
      {
        typeof (ReqDrawCard.CardInfo[]),
        110
      },
      {
        typeof (ReqDrawCard.CardInfo.Card[]),
        111
      },
      {
        typeof (JSON_FixParam[]),
        112
      },
      {
        typeof (JSON_UnitParam[]),
        113
      },
      {
        typeof (JSON_UnitJobOverwriteParam[]),
        114
      },
      {
        typeof (JSON_SkillParam[]),
        115
      },
      {
        typeof (JSON_BuffEffectParam[]),
        116
      },
      {
        typeof (JSON_CondEffectParam[]),
        117
      },
      {
        typeof (JSON_AbilityParam[]),
        118
      },
      {
        typeof (JSON_ItemParam[]),
        119
      },
      {
        typeof (JSON_ArtifactParam[]),
        120
      },
      {
        typeof (JSON_WeaponParam[]),
        121
      },
      {
        typeof (JSON_RecipeParam[]),
        122
      },
      {
        typeof (JSON_JobRankParam[]),
        123
      },
      {
        typeof (JSON_JobParam[]),
        124
      },
      {
        typeof (JSON_JobSetParam[]),
        125
      },
      {
        typeof (JSON_EvaluationParam[]),
        126
      },
      {
        typeof (JSON_AIParam[]),
        (int) sbyte.MaxValue
      },
      {
        typeof (JSON_GeoParam[]),
        128
      },
      {
        typeof (JSON_RarityParam[]),
        129
      },
      {
        typeof (JSON_ShopParam[]),
        130
      },
      {
        typeof (JSON_PlayerParam[]),
        131
      },
      {
        typeof (JSON_GrowCurve[]),
        132
      },
      {
        typeof (JSON_GrowParam[]),
        133
      },
      {
        typeof (JSON_LocalNotificationParam[]),
        134
      },
      {
        typeof (JSON_TrophyCategoryParam[]),
        135
      },
      {
        typeof (JSON_ChallengeCategoryParam[]),
        136
      },
      {
        typeof (JSON_TrophyParam[]),
        137
      },
      {
        typeof (JSON_UnlockParam[]),
        138
      },
      {
        typeof (JSON_VipParam[]),
        139
      },
      {
        typeof (JSON_ArenaWinResult[]),
        140
      },
      {
        typeof (JSON_ArenaResult[]),
        141
      },
      {
        typeof (JSON_StreamingMovie[]),
        142
      },
      {
        typeof (JSON_BannerParam[]),
        143
      },
      {
        typeof (JSON_QuestClearUnlockUnitDataParam[]),
        144
      },
      {
        typeof (JSON_AwardParam[]),
        145
      },
      {
        typeof (JSON_LoginInfoParam[]),
        146
      },
      {
        typeof (JSON_CollaboSkillParam[]),
        147
      },
      {
        typeof (JSON_TrickParam[]),
        148
      },
      {
        typeof (JSON_BreakObjParam[]),
        149
      },
      {
        typeof (JSON_VersusMatchingParam[]),
        150
      },
      {
        typeof (JSON_VersusMatchCondParam[]),
        151
      },
      {
        typeof (JSON_TowerScoreThreshold[]),
        152
      },
      {
        typeof (JSON_TowerScore[]),
        153
      },
      {
        typeof (JSON_FriendPresentItemParam[]),
        154
      },
      {
        typeof (JSON_WeatherParam[]),
        155
      },
      {
        typeof (JSON_UnitUnlockTimeParam[]),
        156
      },
      {
        typeof (JSON_TobiraLearnAbilityParam[]),
        157
      },
      {
        typeof (JSON_TobiraParam[]),
        158
      },
      {
        typeof (JSON_TobiraCategoriesParam[]),
        159
      },
      {
        typeof (JSON_TobiraConditionParam[]),
        160
      },
      {
        typeof (JSON_TobiraCondsParam[]),
        161
      },
      {
        typeof (JSON_TobiraCondsUnitParam.JobCond[]),
        162
      },
      {
        typeof (JSON_TobiraCondsUnitParam[]),
        163
      },
      {
        typeof (JSON_TobiraRecipeMaterialParam[]),
        164
      },
      {
        typeof (JSON_TobiraRecipeParam[]),
        165
      },
      {
        typeof (JSON_ConceptCardEquipParam[]),
        166
      },
      {
        typeof (JSON_ConceptCardParam[]),
        167
      },
      {
        typeof (JSON_ConceptCardConditionsParam[]),
        168
      },
      {
        typeof (JSON_ConceptCardTrustRewardItemParam[]),
        169
      },
      {
        typeof (JSON_ConceptCardTrustRewardParam[]),
        170
      },
      {
        typeof (JSON_ConceptCardLsBuffCoefParam[]),
        171
      },
      {
        typeof (JSON_ConceptCardGroup[]),
        172
      },
      {
        typeof (JSON_ConceptLimitUpItem[]),
        173
      },
      {
        typeof (JSON_UnitGroupParam[]),
        174
      },
      {
        typeof (JSON_JobGroupParam[]),
        175
      },
      {
        typeof (JSON_StatusCoefficientParam[]),
        176
      },
      {
        typeof (JSON_CustomTargetParam[]),
        177
      },
      {
        typeof (JSON_SkillAbilityDeriveParam[]),
        178
      },
      {
        typeof (JSON_RaidPeriodParam[]),
        179
      },
      {
        typeof (JSON_RaidPeriodTimeScheduleParam[]),
        180
      },
      {
        typeof (JSON_RaidPeriodTimeParam[]),
        181
      },
      {
        typeof (JSON_RaidAreaParam[]),
        182
      },
      {
        typeof (JSON_RaidBossParam[]),
        183
      },
      {
        typeof (JSON_RaidBattleRewardWeightParam[]),
        184
      },
      {
        typeof (JSON_RaidBattleRewardParam[]),
        185
      },
      {
        typeof (JSON_RaidBeatRewardDataParam[]),
        186
      },
      {
        typeof (JSON_RaidBeatRewardParam[]),
        187
      },
      {
        typeof (JSON_RaidDamageRatioRewardRatioParam[]),
        188
      },
      {
        typeof (JSON_RaidDamageRatioRewardParam[]),
        189
      },
      {
        typeof (JSON_RaidDamageAmountRewardAmountParam[]),
        190
      },
      {
        typeof (JSON_RaidDamageAmountRewardParam[]),
        191
      },
      {
        typeof (JSON_RaidAreaClearRewardDataParam[]),
        192
      },
      {
        typeof (JSON_RaidAreaClearRewardParam[]),
        193
      },
      {
        typeof (JSON_RaidCompleteRewardDataParam[]),
        194
      },
      {
        typeof (JSON_RaidCompleteRewardParam[]),
        195
      },
      {
        typeof (JSON_RaidReward[]),
        196
      },
      {
        typeof (JSON_RaidRewardParam[]),
        197
      },
      {
        typeof (JSON_TipsParam[]),
        198
      },
      {
        typeof (JSON_GuildEmblemParam[]),
        199
      },
      {
        typeof (JSON_GuildFacilityEffectParam[]),
        200
      },
      {
        typeof (JSON_GuildFacilityParam[]),
        201
      },
      {
        typeof (JSON_GuildFacilityLvParam[]),
        202
      },
      {
        typeof (JSON_ConvertUnitPieceExcludeParam[]),
        203
      },
      {
        typeof (JSON_PremiumParam[]),
        204
      },
      {
        typeof (JSON_BuyCoinShopParam[]),
        205
      },
      {
        typeof (JSON_BuyCoinProductParam[]),
        206
      },
      {
        typeof (JSON_BuyCoinRewardItemParam[]),
        207
      },
      {
        typeof (JSON_BuyCoinRewardParam[]),
        208
      },
      {
        typeof (JSON_BuyCoinProductConvertParam[]),
        209
      },
      {
        typeof (JSON_DynamicTransformUnitParam[]),
        210
      },
      {
        typeof (JSON_RecommendedArtifactParam[]),
        211
      },
      {
        typeof (JSON_SkillMotionDataParam[]),
        212
      },
      {
        typeof (JSON_SkillMotionParam[]),
        213
      },
      {
        typeof (JSON_DependStateSpcEffParam[]),
        214
      },
      {
        typeof (JSON_InspSkillDerivation[]),
        215
      },
      {
        typeof (JSON_InspSkillParam[]),
        216
      },
      {
        typeof (JSON_InspSkillTriggerParam.JSON_TriggerData[]),
        217
      },
      {
        typeof (JSON_InspSkillTriggerParam[]),
        218
      },
      {
        typeof (JSON_InspSkillCostParam[]),
        219
      },
      {
        typeof (JSON_InspSkillLvUpCostParam.JSON_CostData[]),
        220
      },
      {
        typeof (JSON_InspSkillLvUpCostParam[]),
        221
      },
      {
        typeof (JSON_HighlightResource[]),
        222
      },
      {
        typeof (JSON_HighlightParam[]),
        223
      },
      {
        typeof (JSON_HighlightGiftData[]),
        224
      },
      {
        typeof (JSON_HighlightGift[]),
        225
      },
      {
        typeof (JSON_GenesisParam[]),
        226
      },
      {
        typeof (JSON_CoinBuyUseBonusParam[]),
        227
      },
      {
        typeof (JSON_CoinBuyUseBonusContentParam[]),
        228
      },
      {
        typeof (JSON_CoinBuyUseBonusRewardSetParam[]),
        229
      },
      {
        typeof (JSON_CoinBuyUseBonusItemParam[]),
        230
      },
      {
        typeof (JSON_CoinBuyUseBonusRewardParam[]),
        231
      },
      {
        typeof (JSON_UnitRentalNotificationDataParam[]),
        232
      },
      {
        typeof (JSON_UnitRentalNotificationParam[]),
        233
      },
      {
        typeof (JSON_UnitRentalParam.QuestInfo[]),
        234
      },
      {
        typeof (JSON_UnitRentalParam[]),
        235
      },
      {
        typeof (JSON_DrawCardRewardParam.Data[]),
        236
      },
      {
        typeof (JSON_DrawCardRewardParam[]),
        237
      },
      {
        typeof (JSON_DrawCardParam.DrawInfo[]),
        238
      },
      {
        typeof (JSON_DrawCardParam[]),
        239
      },
      {
        typeof (JSON_TrophyStarMissionRewardParam.Data[]),
        240
      },
      {
        typeof (JSON_TrophyStarMissionRewardParam[]),
        241
      },
      {
        typeof (JSON_TrophyStarMissionParam.StarSetParam[]),
        242
      },
      {
        typeof (JSON_TrophyStarMissionParam[]),
        243
      },
      {
        typeof (JSON_UnitPieceShopParam[]),
        244
      },
      {
        typeof (JSON_UnitPieceShopGroupCost[]),
        245
      },
      {
        typeof (JSON_UnitPieceShopGroupParam[]),
        246
      },
      {
        typeof (JSON_TwitterMessageDetailParam[]),
        247
      },
      {
        typeof (JSON_TwitterMessageParam[]),
        248
      },
      {
        typeof (JSON_FilterConceptCardConditionParam[]),
        249
      },
      {
        typeof (JSON_FilterConceptCardParam[]),
        250
      },
      {
        typeof (JSON_FilterRuneConditionParam[]),
        251
      },
      {
        typeof (JSON_FilterRuneParam[]),
        252
      },
      {
        typeof (JSON_FilterUnitConditionParam[]),
        253
      },
      {
        typeof (JSON_FilterUnitParam[]),
        254
      },
      {
        typeof (JSON_FilterArtifactParam.Condition[]),
        (int) byte.MaxValue
      },
      {
        typeof (JSON_FilterArtifactParam[]),
        256
      },
      {
        typeof (JSON_SortRuneConditionParam[]),
        257
      },
      {
        typeof (JSON_SortRuneParam[]),
        258
      },
      {
        typeof (JSON_RuneParam[]),
        259
      },
      {
        typeof (JSON_RuneLottery[]),
        260
      },
      {
        typeof (JSON_RuneLotteryBaseState[]),
        261
      },
      {
        typeof (JSON_RuneLotteryEvoState[]),
        262
      },
      {
        typeof (JSON_RuneDisassembly[]),
        263
      },
      {
        typeof (JSON_RuneMaterial[]),
        264
      },
      {
        typeof (JSON_RuneCost[]),
        265
      },
      {
        typeof (JSON_RuneSetEffState[]),
        266
      },
      {
        typeof (JSON_RuneSetEff[]),
        267
      },
      {
        typeof (JSON_JukeBoxParam[]),
        268
      },
      {
        typeof (JSON_JukeBoxSectionParam[]),
        269
      },
      {
        typeof (JSON_UnitSameGroupParam[]),
        270
      },
      {
        typeof (JSON_AutoRepeatQuestBoxParam[]),
        271
      },
      {
        typeof (JSON_GuildAttendRewardDetail[]),
        272
      },
      {
        typeof (JSON_GuildAttendParam[]),
        273
      },
      {
        typeof (JSON_GuildAttendReward[]),
        274
      },
      {
        typeof (JSON_GuildAttendRewardParam[]),
        275
      },
      {
        typeof (JSON_GuildRoleBonusDetail[]),
        276
      },
      {
        typeof (JSON_GuildRoleBonus[]),
        277
      },
      {
        typeof (JSON_GUildRoleBonusReward[]),
        278
      },
      {
        typeof (JSON_GuildRoleBonusRewardParam[]),
        279
      },
      {
        typeof (JSON_ResetCostInfoParam[]),
        280
      },
      {
        typeof (JSON_ResetCostParam[]),
        281
      },
      {
        typeof (JSON_ProtectSkillParam[]),
        282
      },
      {
        typeof (JSON_GuildSearchFilterConditionParam[]),
        283
      },
      {
        typeof (JSON_GuildSearchFilterParam[]),
        284
      },
      {
        typeof (JSON_ReplacePeriod[]),
        285
      },
      {
        typeof (JSON_ReplaceSprite[]),
        286
      },
      {
        typeof (JSON_ExpansionPurchaseParam[]),
        287
      },
      {
        typeof (JSON_ExpansionPurchaseQuestParam[]),
        288
      },
      {
        typeof (JSON_SkillAdditionalParam[]),
        289
      },
      {
        typeof (JSON_SkillAntiShieldParam[]),
        290
      },
      {
        typeof (JSON_InitPlayer[]),
        291
      },
      {
        typeof (JSON_InitUnit[]),
        292
      },
      {
        typeof (JSON_InitItem[]),
        293
      },
      {
        typeof (JSON_SectionParam[]),
        294
      },
      {
        typeof (JSON_ArchiveItemsParam[]),
        295
      },
      {
        typeof (JSON_ArchiveParam[]),
        296
      },
      {
        typeof (JSON_ChapterParam[]),
        297
      },
      {
        typeof (JSON_MapParam[]),
        298
      },
      {
        typeof (JSON_QuestParam[]),
        299
      },
      {
        typeof (JSON_InnerObjective[]),
        300
      },
      {
        typeof (JSON_ObjectiveParam[]),
        301
      },
      {
        typeof (JSON_MagnificationParam[]),
        302
      },
      {
        typeof (JSON_QuestCondParam[]),
        303
      },
      {
        typeof (JSON_QuestPartyParam[]),
        304
      },
      {
        typeof (JSON_QuestCampaignParentParam[]),
        305
      },
      {
        typeof (JSON_QuestCampaignChildParam[]),
        306
      },
      {
        typeof (JSON_QuestCampaignTrust[]),
        307
      },
      {
        typeof (JSON_QuestCampaignInspSkill[]),
        308
      },
      {
        typeof (JSON_TowerFloorParam[]),
        309
      },
      {
        typeof (JSON_TowerRewardItem[]),
        310
      },
      {
        typeof (JSON_TowerRewardParam[]),
        311
      },
      {
        typeof (JSON_TowerRoundRewardItem[]),
        312
      },
      {
        typeof (JSON_TowerRoundRewardParam[]),
        313
      },
      {
        typeof (JSON_TowerParam[]),
        314
      },
      {
        typeof (JSON_VersusTowerParam[]),
        315
      },
      {
        typeof (JSON_VersusSchedule[]),
        316
      },
      {
        typeof (JSON_VersusCoin[]),
        317
      },
      {
        typeof (JSON_MultiTowerFloorParam[]),
        318
      },
      {
        typeof (JSON_MultiTowerRewardItem[]),
        319
      },
      {
        typeof (JSON_MultiTowerRewardParam[]),
        320
      },
      {
        typeof (JSON_MapEffectParam[]),
        321
      },
      {
        typeof (JSON_WeatherSetParam[]),
        322
      },
      {
        typeof (JSON_RankingQuestParam[]),
        323
      },
      {
        typeof (JSON_RankingQuestScheduleParam[]),
        324
      },
      {
        typeof (JSON_RankingQuestRewardParam[]),
        325
      },
      {
        typeof (JSON_VersusWinBonusReward[]),
        326
      },
      {
        typeof (JSON_VersusFirstWinBonus[]),
        327
      },
      {
        typeof (JSON_VersusStreakWinSchedule[]),
        328
      },
      {
        typeof (JSON_VersusStreakWinBonus[]),
        329
      },
      {
        typeof (JSON_VersusRule[]),
        330
      },
      {
        typeof (JSON_VersusCoinCampParam[]),
        331
      },
      {
        typeof (JSON_VersusEnableTimeScheduleParam[]),
        332
      },
      {
        typeof (JSON_VersusEnableTimeParam[]),
        333
      },
      {
        typeof (JSON_VersusRankParam[]),
        334
      },
      {
        typeof (JSON_VersusRankClassParam[]),
        335
      },
      {
        typeof (JSON_VersusRankRankingRewardParam[]),
        336
      },
      {
        typeof (JSON_VersusRankRewardRewardParam[]),
        337
      },
      {
        typeof (JSON_VersusRankRewardParam[]),
        338
      },
      {
        typeof (JSON_VersusRankMissionScheduleParam[]),
        339
      },
      {
        typeof (JSON_VersusRankMissionParam[]),
        340
      },
      {
        typeof (JSON_QuestLobbyNewsParam[]),
        341
      },
      {
        typeof (JSON_GuerrillaShopAdventQuestParam[]),
        342
      },
      {
        typeof (JSON_GuerrillaShopScheduleAdventParam[]),
        343
      },
      {
        typeof (JSON_GuerrillaShopScheduleParam[]),
        344
      },
      {
        typeof (JSON_VersusDraftDeckParam[]),
        345
      },
      {
        typeof (JSON_VersusDraftUnitParam[]),
        346
      },
      {
        typeof (JSON_GenesisStarRewardParam[]),
        347
      },
      {
        typeof (JSON_GenesisStarParam[]),
        348
      },
      {
        typeof (JSON_GenesisChapterModeInfoParam[]),
        349
      },
      {
        typeof (JSON_GenesisChapterParam[]),
        350
      },
      {
        typeof (JSON_GenesisRewardDataParam[]),
        351
      },
      {
        typeof (JSON_GenesisRewardParam[]),
        352
      },
      {
        typeof (JSON_GenesisLapBossParam.LapInfo[]),
        353
      },
      {
        typeof (JSON_GenesisLapBossParam[]),
        354
      },
      {
        typeof (JSON_AdvanceStarRewardParam[]),
        355
      },
      {
        typeof (JSON_AdvanceStarParam[]),
        356
      },
      {
        typeof (JSON_AdvanceEventModeInfoParam[]),
        357
      },
      {
        typeof (JSON_AdvanceEventParam[]),
        358
      },
      {
        typeof (JSON_AdvanceRewardDataParam[]),
        359
      },
      {
        typeof (JSON_AdvanceRewardParam[]),
        360
      },
      {
        typeof (JSON_AdvanceLapBossParam.LapInfo[]),
        361
      },
      {
        typeof (JSON_AdvanceLapBossParam[]),
        362
      },
      {
        typeof (JSON_GuildRaidBossParam[]),
        363
      },
      {
        typeof (JSON_GuildRaidCoolDaysParam[]),
        364
      },
      {
        typeof (JSON_GuildRaidScoreDataParam[]),
        365
      },
      {
        typeof (JSON_GuildRaidScoreParam[]),
        366
      },
      {
        typeof (JSON_GuildRaidPeriodTime[]),
        367
      },
      {
        typeof (JSON_GuildRaidPeriodParam[]),
        368
      },
      {
        typeof (JSON_GuildRaidReward[]),
        369
      },
      {
        typeof (JSON_GuildRaidRewardParam[]),
        370
      },
      {
        typeof (JSON_GuildRaidRewardDmgRankingRankParam[]),
        371
      },
      {
        typeof (JSON_GuildRaidRewardDmgRankingParam[]),
        372
      },
      {
        typeof (JSON_GuildRaidRewardDmgRatio[]),
        373
      },
      {
        typeof (JSON_GuildRaidRewardDmgRatioParam[]),
        374
      },
      {
        typeof (JSON_GuildRaidRewardRound[]),
        375
      },
      {
        typeof (JSON_GuildRaidRewardRoundParam[]),
        376
      },
      {
        typeof (JSON_GuildRaidRewardRankingDataParam[]),
        377
      },
      {
        typeof (JSON_GuildRaidRewardRankingParam[]),
        378
      },
      {
        typeof (JSON_GvGPeriodParam[]),
        379
      },
      {
        typeof (JSON_GvGNodeParam[]),
        380
      },
      {
        typeof (JSON_GvGNPCPartyDetailParam[]),
        381
      },
      {
        typeof (JSON_GvGNPCPartyParam[]),
        382
      },
      {
        typeof (JSON_GvGNPCUnitParam[]),
        383
      },
      {
        typeof (JSON_GvGRewardRankingDetailParam[]),
        384
      },
      {
        typeof (JSON_GvGRewardRankingParam[]),
        385
      },
      {
        typeof (JSON_GvGRewardDetailParam[]),
        386
      },
      {
        typeof (JSON_GvGRewardParam[]),
        387
      },
      {
        typeof (JSON_GvGRuleParam[]),
        388
      },
      {
        typeof (JSON_GvGNodeReward[]),
        389
      },
      {
        typeof (JSON_GvGNodeRewardParam[]),
        390
      },
      {
        typeof (JSON_GvGLeagueParam[]),
        391
      },
      {
        typeof (JSON_GvGCalcRateSettingParam[]),
        392
      },
      {
        typeof (JSON_WorldRaidParam.BossInfo[]),
        393
      },
      {
        typeof (JSON_WorldRaidParam[]),
        394
      },
      {
        typeof (JSON_WorldRaidBossParam[]),
        395
      },
      {
        typeof (JSON_WorldRaidRewardParam.Reward[]),
        396
      },
      {
        typeof (JSON_WorldRaidRewardParam[]),
        397
      },
      {
        typeof (JSON_WorldRaidDamageRewardParam.Reward[]),
        398
      },
      {
        typeof (JSON_WorldRaidDamageRewardParam[]),
        399
      },
      {
        typeof (JSON_WorldRaidDamageLotteryParam.Reward[]),
        400
      },
      {
        typeof (JSON_WorldRaidDamageLotteryParam[]),
        401
      },
      {
        typeof (JSON_WorldRaidRankingRewardParam.Reward[]),
        402
      },
      {
        typeof (JSON_WorldRaidRankingRewardParam[]),
        403
      },
      {
        typeof (JSON_SupportHistory[]),
        404
      },
      {
        typeof (Json_UnitPieceShopItem[]),
        405
      },
      {
        typeof (Json_MyPhotonPlayerBinaryParam.UnitDataElem[]),
        406
      },
      {
        typeof (Json_MyPhotonPlayerBinaryParam[]),
        407
      },
      {
        typeof (Json_GachaPickups[]),
        408
      },
      {
        typeof (JukeBoxWindow.ResPlayList[]),
        409
      },
      {
        typeof (JSON_GuildFacilityData[]),
        410
      },
      {
        typeof (JSON_GuildRaidBattleLog[]),
        411
      },
      {
        typeof (JSON_GuildRaidChallengingPlayer[]),
        412
      },
      {
        typeof (JSON_GuildRaidRanking[]),
        413
      },
      {
        typeof (JSON_GuildRaidRankingDamage[]),
        414
      },
      {
        typeof (JSON_GuildRaidRankingMember[]),
        415
      },
      {
        typeof (JSON_GuildRaidReport[]),
        416
      },
      {
        typeof (JSON_GvGNodeData[]),
        417
      },
      {
        typeof (JSON_GvGLeagueViewGuild[]),
        418
      },
      {
        typeof (JSON_GvGUsedUnitData[]),
        419
      },
      {
        typeof (JSON_GvGPartyUnit[]),
        420
      },
      {
        typeof (JSON_GvGPartyNPC[]),
        421
      },
      {
        typeof (JSON_GvGParty[]),
        422
      },
      {
        typeof (JSON_GvGScoreRanking[]),
        423
      },
      {
        typeof (JSON_GvGHalfTime[]),
        424
      },
      {
        typeof (JSON_GvGUsedItems[]),
        425
      },
      {
        typeof (JSON_GvGRankingData[]),
        426
      },
      {
        typeof (Json_ChatChannelMasterParam[]),
        427
      },
      {
        typeof (JSON_MultiSupport[]),
        428
      },
      {
        typeof (Json_RuneEnforceGaugeData[]),
        429
      },
      {
        typeof (ReqRuneDisassembly.Response.Rewards[]),
        430
      },
      {
        typeof (ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[]),
        431
      },
      {
        typeof (JSON_WorldRaidBossChallengedData[]),
        432
      },
      {
        typeof (JSON_WorldRaidLogData[]),
        433
      },
      {
        typeof (JSON_WorldRaidRankingData[]),
        434
      },
      {
        typeof (JSON_ProductParam[]),
        435
      },
      {
        typeof (JSON_ProductBuyCoinParam[]),
        436
      },
      {
        typeof (RuneSetEffState[]),
        437
      },
      {
        typeof (RuneCost[]),
        438
      },
      {
        typeof (List<RuneDisassembly.Materials>),
        439
      },
      {
        typeof (List<ReplacePeriod>),
        440
      },
      {
        typeof (EBattleRewardType),
        441
      },
      {
        typeof (eAIActionNoExecAct),
        442
      },
      {
        typeof (eAIActionNextTurnAct),
        443
      },
      {
        typeof (eMapUnitCtCalcType),
        444
      },
      {
        typeof (AIActionType),
        445
      },
      {
        typeof (EEventTrigger),
        446
      },
      {
        typeof (EEventType),
        447
      },
      {
        typeof (EEventGimmick),
        448
      },
      {
        typeof (ProtectTypes),
        449
      },
      {
        typeof (DamageTypes),
        450
      },
      {
        typeof (SkillAdditionalParam.eAddtionalCond),
        451
      },
      {
        typeof (ESkillType),
        452
      },
      {
        typeof (ESkillTiming),
        453
      },
      {
        typeof (ESkillCondition),
        454
      },
      {
        typeof (ELineType),
        455
      },
      {
        typeof (ESelectType),
        456
      },
      {
        typeof (ECastTypes),
        457
      },
      {
        typeof (ESkillTarget),
        458
      },
      {
        typeof (SkillEffectTypes),
        459
      },
      {
        typeof (SkillParamCalcTypes),
        460
      },
      {
        typeof (EElement),
        461
      },
      {
        typeof (AttackTypes),
        462
      },
      {
        typeof (AttackDetailTypes),
        463
      },
      {
        typeof (ShieldTypes),
        464
      },
      {
        typeof (JewelDamageTypes),
        465
      },
      {
        typeof (eKnockBackDir),
        466
      },
      {
        typeof (eKnockBackDs),
        467
      },
      {
        typeof (eDamageDispType),
        468
      },
      {
        typeof (eTeleportType),
        469
      },
      {
        typeof (eTrickSetType),
        470
      },
      {
        typeof (eAbsorbAndGive),
        471
      },
      {
        typeof (eSkillTargetEx),
        472
      },
      {
        typeof (eTeleportSkillPos),
        473
      },
      {
        typeof (QuestClearUnlockUnitDataParam.EUnlockTypes),
        474
      },
      {
        typeof (ESex),
        475
      },
      {
        typeof (EUnitType),
        476
      },
      {
        typeof (JobTypes),
        477
      },
      {
        typeof (RoleTypes),
        478
      },
      {
        typeof (EItemType),
        479
      },
      {
        typeof (EItemTabType),
        480
      },
      {
        typeof (GalleryVisibilityType),
        481
      },
      {
        typeof (eCardType),
        482
      },
      {
        typeof (EffectCheckTargets),
        483
      },
      {
        typeof (EffectCheckTimings),
        484
      },
      {
        typeof (EAppType),
        485
      },
      {
        typeof (EEffRange),
        486
      },
      {
        typeof (BuffFlags),
        487
      },
      {
        typeof (ParamTypes),
        488
      },
      {
        typeof (BuffTypes),
        489
      },
      {
        typeof (ArtifactTypes),
        490
      },
      {
        typeof (BuffMethodTypes),
        491
      },
      {
        typeof (eMovType),
        492
      },
      {
        typeof (EAbilityType),
        493
      },
      {
        typeof (EAbilitySlot),
        494
      },
      {
        typeof (EUseConditionsType),
        495
      },
      {
        typeof (EAbilityTypeDetail),
        496
      },
      {
        typeof (eInspSkillTriggerType),
        497
      },
      {
        typeof (TobiraParam.Category),
        498
      },
      {
        typeof (TobiraLearnAbilityParam.AddType),
        499
      },
      {
        typeof (UnitData.TemporaryFlags),
        500
      },
      {
        typeof (UnitBadgeTypes),
        501
      },
      {
        typeof (ESkillAbilityDeriveConds),
        502
      },
      {
        typeof (ConditionEffectTypes),
        503
      },
      {
        typeof (EUnitCondition),
        504
      },
      {
        typeof (SkillEffectTargets),
        505
      },
      {
        typeof (StatusTypes),
        506
      },
      {
        typeof (ParamPriorities),
        507
      },
      {
        typeof (SkillCategory),
        508
      },
      {
        typeof (EUnitSide),
        509
      },
      {
        typeof (Unit.eTypeMhmDamage),
        510
      },
      {
        typeof (EUnitDirection),
        511
      },
      {
        typeof (eMapBreakClashType),
        512
      },
      {
        typeof (eMapBreakAIType),
        513
      },
      {
        typeof (eMapBreakSideType),
        514
      },
      {
        typeof (eMapBreakRayType),
        515
      },
      {
        typeof (FriendStates),
        516
      },
      {
        typeof (ReqWorldRaidReward.RewardStatus),
        517
      },
      {
        typeof (JukeBoxParam.eUnlockType),
        518
      },
      {
        typeof (BattleCore.Json_BtlDrop),
        519
      },
      {
        typeof (UnitEntryTrigger),
        520
      },
      {
        typeof (OInt),
        521
      },
      {
        typeof (OBool),
        522
      },
      {
        typeof (GeoParam),
        523
      },
      {
        typeof (Grid),
        524
      },
      {
        typeof (OString),
        525
      },
      {
        typeof (SkillLockCondition),
        526
      },
      {
        typeof (EquipSkillSetting),
        527
      },
      {
        typeof (EquipAbilitySetting),
        528
      },
      {
        typeof (AIAction),
        529
      },
      {
        typeof (AIActionTable),
        530
      },
      {
        typeof (AIPatrolPoint),
        531
      },
      {
        typeof (AIPatrolTable),
        532
      },
      {
        typeof (MapBreakObj),
        533
      },
      {
        typeof (OIntVector2),
        534
      },
      {
        typeof (EventTrigger),
        535
      },
      {
        typeof (NPCSetting),
        536
      },
      {
        typeof (MultiPlayResumeBuff.ResistStatus),
        537
      },
      {
        typeof (MultiPlayResumeBuff),
        538
      },
      {
        typeof (MultiPlayResumeShield),
        539
      },
      {
        typeof (MultiPlayResumeMhmDmg),
        540
      },
      {
        typeof (MultiPlayResumeFtgt),
        541
      },
      {
        typeof (MultiPlayResumeAbilChg.Data),
        542
      },
      {
        typeof (MultiPlayResumeAbilChg),
        543
      },
      {
        typeof (MultiPlayResumeAddedAbil),
        544
      },
      {
        typeof (MultiPlayResumeProtect),
        545
      },
      {
        typeof (MultiPlayResumeUnitData),
        546
      },
      {
        typeof (MultiPlayGimmickEventParam),
        547
      },
      {
        typeof (MultiPlayTrickParam),
        548
      },
      {
        typeof (MultiPlayResumeParam.WeatherInfo),
        549
      },
      {
        typeof (MultiPlayResumeParam),
        550
      },
      {
        typeof (SkillRankUpValue),
        551
      },
      {
        typeof (ProtectSkillParam),
        552
      },
      {
        typeof (SkillAdditionalParam),
        553
      },
      {
        typeof (SkillRankUpValueShort),
        554
      },
      {
        typeof (SkillAntiShieldParam),
        555
      },
      {
        typeof (SkillParam),
        556
      },
      {
        typeof (QuestClearUnlockUnitDataParam),
        557
      },
      {
        typeof (FlagManager),
        558
      },
      {
        typeof (OShort),
        559
      },
      {
        typeof (StatusParam),
        560
      },
      {
        typeof (EnchantParam),
        561
      },
      {
        typeof (UnitParam.Status),
        562
      },
      {
        typeof (UnitParam.NoJobStatus),
        563
      },
      {
        typeof (UnitParam),
        564
      },
      {
        typeof (ElementParam),
        565
      },
      {
        typeof (BattleBonusParam),
        566
      },
      {
        typeof (TokkouValue),
        567
      },
      {
        typeof (TokkouParam),
        568
      },
      {
        typeof (BaseStatus),
        569
      },
      {
        typeof (RecipeItem),
        570
      },
      {
        typeof (RecipeParam),
        571
      },
      {
        typeof (ItemParam),
        572
      },
      {
        typeof (ReturnItem),
        573
      },
      {
        typeof (RarityEquipEnhanceParam.RankParam),
        574
      },
      {
        typeof (RarityEquipEnhanceParam),
        575
      },
      {
        typeof (RarityParam),
        576
      },
      {
        typeof (EquipData),
        577
      },
      {
        typeof (OLong),
        578
      },
      {
        typeof (ConceptCardEffectsParam),
        579
      },
      {
        typeof (ConceptLimitUpItemParam),
        580
      },
      {
        typeof (ConceptCardParam),
        581
      },
      {
        typeof (BuffEffectParam.Buff),
        582
      },
      {
        typeof (BuffEffectParam),
        583
      },
      {
        typeof (BuffEffect.BuffTarget),
        584
      },
      {
        typeof (BuffEffect),
        585
      },
      {
        typeof (ConceptCardEquipEffect),
        586
      },
      {
        typeof (ConceptCardData),
        587
      },
      {
        typeof (RuneLotteryBaseState),
        588
      },
      {
        typeof (RuneBuffDataBaseState),
        589
      },
      {
        typeof (RuneLotteryEvoState),
        590
      },
      {
        typeof (RuneBuffDataEvoState),
        591
      },
      {
        typeof (RuneStateData),
        592
      },
      {
        typeof (RuneData),
        593
      },
      {
        typeof (ArtifactParam),
        594
      },
      {
        typeof (BuffEffect.BuffValues),
        595
      },
      {
        typeof (JobRankParam),
        596
      },
      {
        typeof (JobParam),
        597
      },
      {
        typeof (ItemData),
        598
      },
      {
        typeof (LearningSkill),
        599
      },
      {
        typeof (AbilityParam),
        600
      },
      {
        typeof (InspSkillTriggerParam.TriggerData),
        601
      },
      {
        typeof (InspSkillTriggerParam),
        602
      },
      {
        typeof (InspSkillParam),
        603
      },
      {
        typeof (InspirationSkillData),
        604
      },
      {
        typeof (ArtifactData),
        605
      },
      {
        typeof (JobData),
        606
      },
      {
        typeof (TobiraLearnAbilityParam),
        607
      },
      {
        typeof (TobiraParam),
        608
      },
      {
        typeof (TobiraData),
        609
      },
      {
        typeof (UnitData),
        610
      },
      {
        typeof (SkillDeriveParam),
        611
      },
      {
        typeof (SkillAbilityDeriveTriggerParam),
        612
      },
      {
        typeof (SkillAbilityDeriveParam),
        613
      },
      {
        typeof (AbilityDeriveParam),
        614
      },
      {
        typeof (AbilityData),
        615
      },
      {
        typeof (CondEffectParam),
        616
      },
      {
        typeof (CondEffect),
        617
      },
      {
        typeof (SkillData),
        618
      },
      {
        typeof (BuffAttachment.ResistStatusBuff),
        619
      },
      {
        typeof (BuffAttachment),
        620
      },
      {
        typeof (CondAttachment),
        621
      },
      {
        typeof (AIParam),
        622
      },
      {
        typeof (Unit.DropItem),
        623
      },
      {
        typeof (Unit.UnitDrop),
        624
      },
      {
        typeof (Unit.UnitSteal),
        625
      },
      {
        typeof (Unit.UnitShield),
        626
      },
      {
        typeof (Unit.UnitProtect),
        627
      },
      {
        typeof (Unit.UnitMhmDamage),
        628
      },
      {
        typeof (Unit.UnitInsp),
        629
      },
      {
        typeof (Unit.UnitForcedTargeting),
        630
      },
      {
        typeof (Unit.AbilityChange.Data),
        631
      },
      {
        typeof (Unit.AbilityChange),
        632
      },
      {
        typeof (DynamicTransformUnitParam),
        633
      },
      {
        typeof (BuffBit),
        634
      },
      {
        typeof (Unit),
        635
      },
      {
        typeof (MultiPlayResumeSkillData),
        636
      },
      {
        typeof (Json_BtlRewardConceptCard),
        637
      },
      {
        typeof (SceneBattle.MultiPlayRecvData),
        638
      },
      {
        typeof (Json_InspirationSkill),
        639
      },
      {
        typeof (Json_Artifact),
        640
      },
      {
        typeof (JSON_ConceptCard),
        641
      },
      {
        typeof (Json_GachaPickups),
        642
      },
      {
        typeof (Json_DropInfo),
        643
      },
      {
        typeof (JSON_PlayerGuild),
        644
      },
      {
        typeof (JSON_ViewGuild),
        645
      },
      {
        typeof (Json_MasterAbility),
        646
      },
      {
        typeof (Json_CollaboSkill),
        647
      },
      {
        typeof (Json_CollaboAbility),
        648
      },
      {
        typeof (Json_Equip),
        649
      },
      {
        typeof (Json_Ability),
        650
      },
      {
        typeof (Json_InspirationSkillExt),
        651
      },
      {
        typeof (Json_JobSelectable),
        652
      },
      {
        typeof (Json_Job),
        653
      },
      {
        typeof (Json_UnitJob),
        654
      },
      {
        typeof (Json_UnitSelectable),
        655
      },
      {
        typeof (Json_Tobira),
        656
      },
      {
        typeof (Json_RuneBuffData),
        657
      },
      {
        typeof (Json_RuneStateData),
        658
      },
      {
        typeof (Json_RuneData),
        659
      },
      {
        typeof (Json_Unit),
        660
      },
      {
        typeof (JSON_GuildMember),
        661
      },
      {
        typeof (JSON_CombatPowerRankingViewGuild),
        662
      },
      {
        typeof (JSON_CombatPowerRankingGuildMember),
        663
      },
      {
        typeof (JSON_GuildFacilityData),
        664
      },
      {
        typeof (JSON_GuildRaidPrev),
        665
      },
      {
        typeof (JSON_GuildRaidCurrent),
        666
      },
      {
        typeof (JSON_GuildRaidBattlePoint),
        667
      },
      {
        typeof (JSON_GuildRaidBossInfo),
        668
      },
      {
        typeof (JSON_GuildRaidData),
        669
      },
      {
        typeof (JSON_GuildRaidChallengingPlayer),
        670
      },
      {
        typeof (JSON_GuildRaidKnockDownInfo),
        671
      },
      {
        typeof (JSON_GuildRaidMailListItem),
        672
      },
      {
        typeof (JSON_GuildRaidMailOption),
        673
      },
      {
        typeof (JSON_GuildRaidMail),
        674
      },
      {
        typeof (JSON_GuildRaidDeck),
        675
      },
      {
        typeof (JSON_GuildRaidBattleLog),
        676
      },
      {
        typeof (JSON_GuildRaidReport),
        677
      },
      {
        typeof (JSON_GuildRaidRankingGuild),
        678
      },
      {
        typeof (JSON_GuildRaidRanking),
        679
      },
      {
        typeof (JSON_GuildRaidRankingMemberBoss),
        680
      },
      {
        typeof (JSON_GuildRaidRankingMember),
        681
      },
      {
        typeof (JSON_GuildRaidRankingDamage),
        682
      },
      {
        typeof (JSON_GuildRaidGuildData),
        683
      },
      {
        typeof (JSON_GuildRaidRankingRewardData),
        684
      },
      {
        typeof (JSON_GuildRaidGuildRanking),
        685
      },
      {
        typeof (OByte),
        686
      },
      {
        typeof (OSbyte),
        687
      },
      {
        typeof (OUInt),
        688
      },
      {
        typeof (Json_Coin),
        689
      },
      {
        typeof (Json_Hikkoshi),
        690
      },
      {
        typeof (Json_Stamina),
        691
      },
      {
        typeof (Json_Cave),
        692
      },
      {
        typeof (Json_AbilityUp),
        693
      },
      {
        typeof (Json_Arena),
        694
      },
      {
        typeof (Json_Tour),
        695
      },
      {
        typeof (Json_Vip),
        696
      },
      {
        typeof (Json_Premium),
        697
      },
      {
        typeof (Json_FreeGacha),
        698
      },
      {
        typeof (Json_PaidGacha),
        699
      },
      {
        typeof (Json_Friends),
        700
      },
      {
        typeof (Json_MultiOption),
        701
      },
      {
        typeof (Json_GuerrillaShopPeriod),
        702
      },
      {
        typeof (Json_PlayerData),
        703
      },
      {
        typeof (Json_Item),
        704
      },
      {
        typeof (Json_GiftConceptCard),
        705
      },
      {
        typeof (Json_Gift),
        706
      },
      {
        typeof (Json_Mail),
        707
      },
      {
        typeof (Json_Party),
        708
      },
      {
        typeof (Json_Friend),
        709
      },
      {
        typeof (Json_Skin),
        710
      },
      {
        typeof (Json_LoginBonusVip),
        711
      },
      {
        typeof (Json_LoginBonus),
        712
      },
      {
        typeof (Json_PremiumLoginBonusItem),
        713
      },
      {
        typeof (Json_PremiumLoginBonus),
        714
      },
      {
        typeof (Json_LoginBonusTable),
        715
      },
      {
        typeof (Json_Notify),
        716
      },
      {
        typeof (Json_MultiFuids),
        717
      },
      {
        typeof (Json_VersusCount),
        718
      },
      {
        typeof (Json_Versus),
        719
      },
      {
        typeof (Json_ExpireItem),
        720
      },
      {
        typeof (JSON_TrophyProgress),
        721
      },
      {
        typeof (JSON_UnitOverWriteData),
        722
      },
      {
        typeof (JSON_PartyOverWrite),
        723
      },
      {
        typeof (JSON_StoryExChallengeCount),
        724
      },
      {
        typeof (Json_ExpansionPurchase),
        725
      },
      {
        typeof (Json_PlayerDataAll),
        726
      },
      {
        typeof (Json_TrophyPlayerData),
        727
      },
      {
        typeof (Json_TrophyConceptCard),
        728
      },
      {
        typeof (Json_TrophyConceptCards),
        729
      },
      {
        typeof (ReqTrophyStarMission.StarMission.Info),
        730
      },
      {
        typeof (ReqTrophyStarMission.StarMission),
        731
      },
      {
        typeof (Json_TrophyPlayerDataAll),
        732
      },
      {
        typeof (Json_UnitSkin),
        733
      },
      {
        typeof (JSON_SupportHistory),
        734
      },
      {
        typeof (JSON_SupportMyInfo),
        735
      },
      {
        typeof (JSON_SupportRankingGuild),
        736
      },
      {
        typeof (JSON_SupportRanking),
        737
      },
      {
        typeof (JSON_SupportRankingUser),
        738
      },
      {
        typeof (JSON_SupportUnitRanking),
        739
      },
      {
        typeof (JSON_SupportRankingUnit),
        740
      },
      {
        typeof (Json_RuneEnforceGaugeData),
        741
      },
      {
        typeof (Json_UnitPieceShopItem),
        742
      },
      {
        typeof (ReqAutoRepeatQuestBox.Response),
        743
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestBox.MP_ReqAutoRepeatQuestBoxResponse),
        744
      },
      {
        typeof (ReqAutoRepeatQuestBoxAdd.Response),
        745
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestBoxAdd.MP_ReqAutoRepeatQuestBoxAddResponse),
        746
      },
      {
        typeof (Json_AutoRepeatQuestData),
        747
      },
      {
        typeof (ReqAutoRepeatQuestProgress.Response),
        748
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestProgress.MP_AutoRepeatQuestProgressResponse),
        749
      },
      {
        typeof (JSON_QuestCount),
        750
      },
      {
        typeof (JSON_QuestProgress),
        751
      },
      {
        typeof (JSON_ChapterCount),
        752
      },
      {
        typeof (ReqAutoRepeatQuestEnd.Response),
        753
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestResult.MP_AutoRepeatQuestEndResponse),
        754
      },
      {
        typeof (ReqAutoRepeatQuestSetApItemPriority.Response),
        755
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestSetApItemPriority.MP_ReqAutoRepeatQuestSetApItemPriority),
        756
      },
      {
        typeof (ReqAutoRepeatQuestStart.Response),
        757
      },
      {
        typeof (FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse),
        758
      },
      {
        typeof (ReqCombatPowerUpdate.Response),
        759
      },
      {
        typeof (FlowNode_ReqCombatPowerUpdate.MP_Response),
        760
      },
      {
        typeof (ReqGuildRanking.Response),
        761
      },
      {
        typeof (FlowNode_ReqGuildRanking.MP_Response),
        762
      },
      {
        typeof (ReqGuildRankingMembers.Response),
        763
      },
      {
        typeof (FlowNode_ReqGuildRankingMembers.MP_Response),
        764
      },
      {
        typeof (ReqDrawCard.CardInfo.Card),
        765
      },
      {
        typeof (ReqDrawCard.CardInfo),
        766
      },
      {
        typeof (ReqDrawCard.Response.Status),
        767
      },
      {
        typeof (ReqDrawCard.Response),
        768
      },
      {
        typeof (FlowNode_ReqDrawCard.MP_Response),
        769
      },
      {
        typeof (ReqDrawCardExec.Response),
        770
      },
      {
        typeof (FlowNode_ReqDrawCardExec.MP_Response),
        771
      },
      {
        typeof (ReqExpansionPurchase.Response),
        772
      },
      {
        typeof (FlowNode_ReqExpansionPurchaseData.MP_ReqExpansionPurchaseResponse),
        773
      },
      {
        typeof (ReqAllEquipExpAdd.Response),
        774
      },
      {
        typeof (FlowNode_AllEnhanceEquip.MP_Response),
        775
      },
      {
        typeof (FlowNode_Login.MP_PlayerDataAll),
        776
      },
      {
        typeof (FlowNode_PlayNew.MP_PlayNew),
        777
      },
      {
        typeof (ReqArtifactSet_OverWrite.Response),
        778
      },
      {
        typeof (FlowNode_ReqArtifactsSet.MP_ArtifactSet_OverWriteResponse),
        779
      },
      {
        typeof (FlowNode_ReqBingoProgress.JSON_BingoResponse),
        780
      },
      {
        typeof (FlowNode_ReqBingoProgress.MP_BingoResponse),
        781
      },
      {
        typeof (ReqSetConceptCard_OverWrite.Response),
        782
      },
      {
        typeof (FlowNode_ReqConceptCardSet.MP_SetConceptCard_OverWriteResponse),
        783
      },
      {
        typeof (ReqSetConceptLeaderSkill_OverWrite.Response),
        784
      },
      {
        typeof (FlowNode_ReqConceptLeaderSkillSet.MP_SetConceptLeaderSkill_OverWriteResponse),
        785
      },
      {
        typeof (JSON_FixParam),
        786
      },
      {
        typeof (JSON_UnitParam),
        787
      },
      {
        typeof (JSON_UnitJobOverwriteParam),
        788
      },
      {
        typeof (JSON_SkillParam),
        789
      },
      {
        typeof (JSON_BuffEffectParam),
        790
      },
      {
        typeof (JSON_CondEffectParam),
        791
      },
      {
        typeof (JSON_AbilityParam),
        792
      },
      {
        typeof (JSON_ItemParam),
        793
      },
      {
        typeof (JSON_ArtifactParam),
        794
      },
      {
        typeof (JSON_WeaponParam),
        795
      },
      {
        typeof (JSON_RecipeParam),
        796
      },
      {
        typeof (JSON_JobRankParam),
        797
      },
      {
        typeof (JSON_JobParam),
        798
      },
      {
        typeof (JSON_JobSetParam),
        799
      },
      {
        typeof (JSON_EvaluationParam),
        800
      },
      {
        typeof (JSON_AIParam),
        801
      },
      {
        typeof (JSON_GeoParam),
        802
      },
      {
        typeof (JSON_RarityParam),
        803
      },
      {
        typeof (JSON_ShopParam),
        804
      },
      {
        typeof (JSON_PlayerParam),
        805
      },
      {
        typeof (JSON_GrowCurve),
        806
      },
      {
        typeof (JSON_GrowParam),
        807
      },
      {
        typeof (JSON_LocalNotificationParam),
        808
      },
      {
        typeof (JSON_TrophyCategoryParam),
        809
      },
      {
        typeof (JSON_ChallengeCategoryParam),
        810
      },
      {
        typeof (JSON_TrophyParam),
        811
      },
      {
        typeof (JSON_UnlockParam),
        812
      },
      {
        typeof (JSON_VipParam),
        813
      },
      {
        typeof (JSON_ArenaWinResult),
        814
      },
      {
        typeof (JSON_ArenaResult),
        815
      },
      {
        typeof (JSON_StreamingMovie),
        816
      },
      {
        typeof (JSON_BannerParam),
        817
      },
      {
        typeof (JSON_QuestClearUnlockUnitDataParam),
        818
      },
      {
        typeof (JSON_AwardParam),
        819
      },
      {
        typeof (JSON_LoginInfoParam),
        820
      },
      {
        typeof (JSON_CollaboSkillParam),
        821
      },
      {
        typeof (JSON_TrickParam),
        822
      },
      {
        typeof (JSON_BreakObjParam),
        823
      },
      {
        typeof (JSON_VersusMatchingParam),
        824
      },
      {
        typeof (JSON_VersusMatchCondParam),
        825
      },
      {
        typeof (JSON_TowerScoreThreshold),
        826
      },
      {
        typeof (JSON_TowerScore),
        827
      },
      {
        typeof (JSON_FriendPresentItemParam),
        828
      },
      {
        typeof (JSON_WeatherParam),
        829
      },
      {
        typeof (JSON_UnitUnlockTimeParam),
        830
      },
      {
        typeof (JSON_TobiraLearnAbilityParam),
        831
      },
      {
        typeof (JSON_TobiraParam),
        832
      },
      {
        typeof (JSON_TobiraCategoriesParam),
        833
      },
      {
        typeof (JSON_TobiraConditionParam),
        834
      },
      {
        typeof (JSON_TobiraCondsParam),
        835
      },
      {
        typeof (JSON_TobiraCondsUnitParam.JobCond),
        836
      },
      {
        typeof (JSON_TobiraCondsUnitParam),
        837
      },
      {
        typeof (JSON_TobiraRecipeMaterialParam),
        838
      },
      {
        typeof (JSON_TobiraRecipeParam),
        839
      },
      {
        typeof (JSON_ConceptCardEquipParam),
        840
      },
      {
        typeof (JSON_ConceptCardParam),
        841
      },
      {
        typeof (JSON_ConceptCardConditionsParam),
        842
      },
      {
        typeof (JSON_ConceptCardTrustRewardItemParam),
        843
      },
      {
        typeof (JSON_ConceptCardTrustRewardParam),
        844
      },
      {
        typeof (JSON_ConceptCardLsBuffCoefParam),
        845
      },
      {
        typeof (JSON_ConceptCardGroup),
        846
      },
      {
        typeof (JSON_ConceptLimitUpItem),
        847
      },
      {
        typeof (JSON_UnitGroupParam),
        848
      },
      {
        typeof (JSON_JobGroupParam),
        849
      },
      {
        typeof (JSON_StatusCoefficientParam),
        850
      },
      {
        typeof (JSON_CustomTargetParam),
        851
      },
      {
        typeof (JSON_SkillAbilityDeriveParam),
        852
      },
      {
        typeof (JSON_RaidPeriodParam),
        853
      },
      {
        typeof (JSON_RaidPeriodTimeScheduleParam),
        854
      },
      {
        typeof (JSON_RaidPeriodTimeParam),
        855
      },
      {
        typeof (JSON_RaidAreaParam),
        856
      },
      {
        typeof (JSON_RaidBossParam),
        857
      },
      {
        typeof (JSON_RaidBattleRewardWeightParam),
        858
      },
      {
        typeof (JSON_RaidBattleRewardParam),
        859
      },
      {
        typeof (JSON_RaidBeatRewardDataParam),
        860
      },
      {
        typeof (JSON_RaidBeatRewardParam),
        861
      },
      {
        typeof (JSON_RaidDamageRatioRewardRatioParam),
        862
      },
      {
        typeof (JSON_RaidDamageRatioRewardParam),
        863
      },
      {
        typeof (JSON_RaidDamageAmountRewardAmountParam),
        864
      },
      {
        typeof (JSON_RaidDamageAmountRewardParam),
        865
      },
      {
        typeof (JSON_RaidAreaClearRewardDataParam),
        866
      },
      {
        typeof (JSON_RaidAreaClearRewardParam),
        867
      },
      {
        typeof (JSON_RaidCompleteRewardDataParam),
        868
      },
      {
        typeof (JSON_RaidCompleteRewardParam),
        869
      },
      {
        typeof (JSON_RaidReward),
        870
      },
      {
        typeof (JSON_RaidRewardParam),
        871
      },
      {
        typeof (JSON_TipsParam),
        872
      },
      {
        typeof (JSON_GuildEmblemParam),
        873
      },
      {
        typeof (JSON_GuildFacilityEffectParam),
        874
      },
      {
        typeof (JSON_GuildFacilityParam),
        875
      },
      {
        typeof (JSON_GuildFacilityLvParam),
        876
      },
      {
        typeof (JSON_ConvertUnitPieceExcludeParam),
        877
      },
      {
        typeof (JSON_PremiumParam),
        878
      },
      {
        typeof (JSON_BuyCoinShopParam),
        879
      },
      {
        typeof (JSON_BuyCoinProductParam),
        880
      },
      {
        typeof (JSON_BuyCoinRewardItemParam),
        881
      },
      {
        typeof (JSON_BuyCoinRewardParam),
        882
      },
      {
        typeof (JSON_BuyCoinProductConvertParam),
        883
      },
      {
        typeof (JSON_DynamicTransformUnitParam),
        884
      },
      {
        typeof (JSON_RecommendedArtifactParam),
        885
      },
      {
        typeof (JSON_SkillMotionDataParam),
        886
      },
      {
        typeof (JSON_SkillMotionParam),
        887
      },
      {
        typeof (JSON_DependStateSpcEffParam),
        888
      },
      {
        typeof (JSON_InspSkillDerivation),
        889
      },
      {
        typeof (JSON_InspSkillParam),
        890
      },
      {
        typeof (JSON_InspSkillTriggerParam.JSON_TriggerData),
        891
      },
      {
        typeof (JSON_InspSkillTriggerParam),
        892
      },
      {
        typeof (JSON_InspSkillCostParam),
        893
      },
      {
        typeof (JSON_InspSkillLvUpCostParam.JSON_CostData),
        894
      },
      {
        typeof (JSON_InspSkillLvUpCostParam),
        895
      },
      {
        typeof (JSON_HighlightResource),
        896
      },
      {
        typeof (JSON_HighlightParam),
        897
      },
      {
        typeof (JSON_HighlightGiftData),
        898
      },
      {
        typeof (JSON_HighlightGift),
        899
      },
      {
        typeof (JSON_GenesisParam),
        900
      },
      {
        typeof (JSON_CoinBuyUseBonusParam),
        901
      },
      {
        typeof (JSON_CoinBuyUseBonusContentParam),
        902
      },
      {
        typeof (JSON_CoinBuyUseBonusRewardSetParam),
        903
      },
      {
        typeof (JSON_CoinBuyUseBonusItemParam),
        904
      },
      {
        typeof (JSON_CoinBuyUseBonusRewardParam),
        905
      },
      {
        typeof (JSON_UnitRentalNotificationDataParam),
        906
      },
      {
        typeof (JSON_UnitRentalNotificationParam),
        907
      },
      {
        typeof (JSON_UnitRentalParam.QuestInfo),
        908
      },
      {
        typeof (JSON_UnitRentalParam),
        909
      },
      {
        typeof (JSON_DrawCardRewardParam.Data),
        910
      },
      {
        typeof (JSON_DrawCardRewardParam),
        911
      },
      {
        typeof (JSON_DrawCardParam.DrawInfo),
        912
      },
      {
        typeof (JSON_DrawCardParam),
        913
      },
      {
        typeof (JSON_TrophyStarMissionRewardParam.Data),
        914
      },
      {
        typeof (JSON_TrophyStarMissionRewardParam),
        915
      },
      {
        typeof (JSON_TrophyStarMissionParam.StarSetParam),
        916
      },
      {
        typeof (JSON_TrophyStarMissionParam),
        917
      },
      {
        typeof (JSON_UnitPieceShopParam),
        918
      },
      {
        typeof (JSON_UnitPieceShopGroupCost),
        919
      },
      {
        typeof (JSON_UnitPieceShopGroupParam),
        920
      },
      {
        typeof (JSON_TwitterMessageDetailParam),
        921
      },
      {
        typeof (JSON_TwitterMessageParam),
        922
      },
      {
        typeof (JSON_FilterConceptCardConditionParam),
        923
      },
      {
        typeof (JSON_FilterConceptCardParam),
        924
      },
      {
        typeof (JSON_FilterRuneConditionParam),
        925
      },
      {
        typeof (JSON_FilterRuneParam),
        926
      },
      {
        typeof (JSON_FilterUnitConditionParam),
        927
      },
      {
        typeof (JSON_FilterUnitParam),
        928
      },
      {
        typeof (JSON_FilterArtifactParam.Condition),
        929
      },
      {
        typeof (JSON_FilterArtifactParam),
        930
      },
      {
        typeof (JSON_SortRuneConditionParam),
        931
      },
      {
        typeof (JSON_SortRuneParam),
        932
      },
      {
        typeof (JSON_RuneParam),
        933
      },
      {
        typeof (JSON_RuneLottery),
        934
      },
      {
        typeof (JSON_RuneLotteryBaseState),
        935
      },
      {
        typeof (JSON_RuneLotteryEvoState),
        936
      },
      {
        typeof (JSON_RuneDisassembly),
        937
      },
      {
        typeof (JSON_RuneMaterial),
        938
      },
      {
        typeof (JSON_RuneCost),
        939
      },
      {
        typeof (JSON_RuneSetEffState),
        940
      },
      {
        typeof (JSON_RuneSetEff),
        941
      },
      {
        typeof (JSON_JukeBoxParam),
        942
      },
      {
        typeof (JSON_JukeBoxSectionParam),
        943
      },
      {
        typeof (JSON_UnitSameGroupParam),
        944
      },
      {
        typeof (JSON_AutoRepeatQuestBoxParam),
        945
      },
      {
        typeof (JSON_GuildAttendRewardDetail),
        946
      },
      {
        typeof (JSON_GuildAttendParam),
        947
      },
      {
        typeof (JSON_GuildAttendReward),
        948
      },
      {
        typeof (JSON_GuildAttendRewardParam),
        949
      },
      {
        typeof (JSON_GuildRoleBonusDetail),
        950
      },
      {
        typeof (JSON_GuildRoleBonus),
        951
      },
      {
        typeof (JSON_GUildRoleBonusReward),
        952
      },
      {
        typeof (JSON_GuildRoleBonusRewardParam),
        953
      },
      {
        typeof (JSON_ResetCostInfoParam),
        954
      },
      {
        typeof (JSON_ResetCostParam),
        955
      },
      {
        typeof (JSON_ProtectSkillParam),
        956
      },
      {
        typeof (JSON_GuildSearchFilterConditionParam),
        957
      },
      {
        typeof (JSON_GuildSearchFilterParam),
        958
      },
      {
        typeof (JSON_ReplacePeriod),
        959
      },
      {
        typeof (JSON_ReplaceSprite),
        960
      },
      {
        typeof (JSON_ExpansionPurchaseParam),
        961
      },
      {
        typeof (JSON_ExpansionPurchaseQuestParam),
        962
      },
      {
        typeof (JSON_SkillAdditionalParam),
        963
      },
      {
        typeof (JSON_SkillAntiShieldParam),
        964
      },
      {
        typeof (JSON_InitPlayer),
        965
      },
      {
        typeof (JSON_InitUnit),
        966
      },
      {
        typeof (JSON_InitItem),
        967
      },
      {
        typeof (JSON_MasterParam),
        968
      },
      {
        typeof (FlowNode_ReqMasterParam.MP_MasterParam),
        969
      },
      {
        typeof (ReqOverWriteParty.Response),
        970
      },
      {
        typeof (FlowNode_ReqOverWriteParty.MP_OverWritePartyResponse),
        971
      },
      {
        typeof (JSON_SectionParam),
        972
      },
      {
        typeof (JSON_ArchiveItemsParam),
        973
      },
      {
        typeof (JSON_ArchiveParam),
        974
      },
      {
        typeof (JSON_ChapterParam),
        975
      },
      {
        typeof (JSON_MapParam),
        976
      },
      {
        typeof (JSON_QuestParam),
        977
      },
      {
        typeof (JSON_InnerObjective),
        978
      },
      {
        typeof (JSON_ObjectiveParam),
        979
      },
      {
        typeof (JSON_MagnificationParam),
        980
      },
      {
        typeof (JSON_QuestCondParam),
        981
      },
      {
        typeof (JSON_QuestPartyParam),
        982
      },
      {
        typeof (JSON_QuestCampaignParentParam),
        983
      },
      {
        typeof (JSON_QuestCampaignChildParam),
        984
      },
      {
        typeof (JSON_QuestCampaignTrust),
        985
      },
      {
        typeof (JSON_QuestCampaignInspSkill),
        986
      },
      {
        typeof (JSON_TowerFloorParam),
        987
      },
      {
        typeof (JSON_TowerRewardItem),
        988
      },
      {
        typeof (JSON_TowerRewardParam),
        989
      },
      {
        typeof (JSON_TowerRoundRewardItem),
        990
      },
      {
        typeof (JSON_TowerRoundRewardParam),
        991
      },
      {
        typeof (JSON_TowerParam),
        992
      },
      {
        typeof (JSON_VersusTowerParam),
        993
      },
      {
        typeof (JSON_VersusSchedule),
        994
      },
      {
        typeof (JSON_VersusCoin),
        995
      },
      {
        typeof (JSON_MultiTowerFloorParam),
        996
      },
      {
        typeof (JSON_MultiTowerRewardItem),
        997
      },
      {
        typeof (JSON_MultiTowerRewardParam),
        998
      },
      {
        typeof (JSON_MapEffectParam),
        999
      },
      {
        typeof (JSON_WeatherSetParam),
        1000
      },
      {
        typeof (JSON_RankingQuestParam),
        1001
      },
      {
        typeof (JSON_RankingQuestScheduleParam),
        1002
      },
      {
        typeof (JSON_RankingQuestRewardParam),
        1003
      },
      {
        typeof (JSON_VersusWinBonusReward),
        1004
      },
      {
        typeof (JSON_VersusFirstWinBonus),
        1005
      },
      {
        typeof (JSON_VersusStreakWinSchedule),
        1006
      },
      {
        typeof (JSON_VersusStreakWinBonus),
        1007
      },
      {
        typeof (JSON_VersusRule),
        1008
      },
      {
        typeof (JSON_VersusCoinCampParam),
        1009
      },
      {
        typeof (JSON_VersusEnableTimeScheduleParam),
        1010
      },
      {
        typeof (JSON_VersusEnableTimeParam),
        1011
      },
      {
        typeof (JSON_VersusRankParam),
        1012
      },
      {
        typeof (JSON_VersusRankClassParam),
        1013
      },
      {
        typeof (JSON_VersusRankRankingRewardParam),
        1014
      },
      {
        typeof (JSON_VersusRankRewardRewardParam),
        1015
      },
      {
        typeof (JSON_VersusRankRewardParam),
        1016
      },
      {
        typeof (JSON_VersusRankMissionScheduleParam),
        1017
      },
      {
        typeof (JSON_VersusRankMissionParam),
        1018
      },
      {
        typeof (JSON_QuestLobbyNewsParam),
        1019
      },
      {
        typeof (JSON_GuerrillaShopAdventQuestParam),
        1020
      },
      {
        typeof (JSON_GuerrillaShopScheduleAdventParam),
        1021
      },
      {
        typeof (JSON_GuerrillaShopScheduleParam),
        1022
      },
      {
        typeof (JSON_VersusDraftDeckParam),
        1023
      },
      {
        typeof (JSON_VersusDraftUnitParam),
        1024
      },
      {
        typeof (JSON_GenesisStarRewardParam),
        1025
      },
      {
        typeof (JSON_GenesisStarParam),
        1026
      },
      {
        typeof (JSON_GenesisChapterModeInfoParam),
        1027
      },
      {
        typeof (JSON_GenesisChapterParam),
        1028
      },
      {
        typeof (JSON_GenesisRewardDataParam),
        1029
      },
      {
        typeof (JSON_GenesisRewardParam),
        1030
      },
      {
        typeof (JSON_GenesisLapBossParam.LapInfo),
        1031
      },
      {
        typeof (JSON_GenesisLapBossParam),
        1032
      },
      {
        typeof (JSON_AdvanceStarRewardParam),
        1033
      },
      {
        typeof (JSON_AdvanceStarParam),
        1034
      },
      {
        typeof (JSON_AdvanceEventModeInfoParam),
        1035
      },
      {
        typeof (JSON_AdvanceEventParam),
        1036
      },
      {
        typeof (JSON_AdvanceRewardDataParam),
        1037
      },
      {
        typeof (JSON_AdvanceRewardParam),
        1038
      },
      {
        typeof (JSON_AdvanceLapBossParam.LapInfo),
        1039
      },
      {
        typeof (JSON_AdvanceLapBossParam),
        1040
      },
      {
        typeof (JSON_GuildRaidBossParam),
        1041
      },
      {
        typeof (JSON_GuildRaidCoolDaysParam),
        1042
      },
      {
        typeof (JSON_GuildRaidScoreDataParam),
        1043
      },
      {
        typeof (JSON_GuildRaidScoreParam),
        1044
      },
      {
        typeof (JSON_GuildRaidPeriodTime),
        1045
      },
      {
        typeof (JSON_GuildRaidPeriodParam),
        1046
      },
      {
        typeof (JSON_GuildRaidReward),
        1047
      },
      {
        typeof (JSON_GuildRaidRewardParam),
        1048
      },
      {
        typeof (JSON_GuildRaidRewardDmgRankingRankParam),
        1049
      },
      {
        typeof (JSON_GuildRaidRewardDmgRankingParam),
        1050
      },
      {
        typeof (JSON_GuildRaidRewardDmgRatio),
        1051
      },
      {
        typeof (JSON_GuildRaidRewardDmgRatioParam),
        1052
      },
      {
        typeof (JSON_GuildRaidRewardRound),
        1053
      },
      {
        typeof (JSON_GuildRaidRewardRoundParam),
        1054
      },
      {
        typeof (JSON_GuildRaidRewardRankingDataParam),
        1055
      },
      {
        typeof (JSON_GuildRaidRewardRankingParam),
        1056
      },
      {
        typeof (JSON_GvGPeriodParam),
        1057
      },
      {
        typeof (JSON_GvGNodeParam),
        1058
      },
      {
        typeof (JSON_GvGNPCPartyDetailParam),
        1059
      },
      {
        typeof (JSON_GvGNPCPartyParam),
        1060
      },
      {
        typeof (JSON_GvGNPCUnitParam),
        1061
      },
      {
        typeof (JSON_GvGRewardRankingDetailParam),
        1062
      },
      {
        typeof (JSON_GvGRewardRankingParam),
        1063
      },
      {
        typeof (JSON_GvGRewardDetailParam),
        1064
      },
      {
        typeof (JSON_GvGRewardParam),
        1065
      },
      {
        typeof (JSON_GvGRuleParam),
        1066
      },
      {
        typeof (JSON_GvGNodeReward),
        1067
      },
      {
        typeof (JSON_GvGNodeRewardParam),
        1068
      },
      {
        typeof (JSON_GvGLeagueParam),
        1069
      },
      {
        typeof (JSON_GvGCalcRateSettingParam),
        1070
      },
      {
        typeof (JSON_WorldRaidParam.BossInfo),
        1071
      },
      {
        typeof (JSON_WorldRaidParam),
        1072
      },
      {
        typeof (JSON_WorldRaidBossParam),
        1073
      },
      {
        typeof (JSON_WorldRaidRewardParam.Reward),
        1074
      },
      {
        typeof (JSON_WorldRaidRewardParam),
        1075
      },
      {
        typeof (JSON_WorldRaidDamageRewardParam.Reward),
        1076
      },
      {
        typeof (JSON_WorldRaidDamageRewardParam),
        1077
      },
      {
        typeof (JSON_WorldRaidDamageLotteryParam.Reward),
        1078
      },
      {
        typeof (JSON_WorldRaidDamageLotteryParam),
        1079
      },
      {
        typeof (JSON_WorldRaidRankingRewardParam.Reward),
        1080
      },
      {
        typeof (JSON_WorldRaidRankingRewardParam),
        1081
      },
      {
        typeof (Json_QuestList),
        1082
      },
      {
        typeof (FlowNode_ReqQuestParam.MP_QuestParam),
        1083
      },
      {
        typeof (ReqSetSupportRanking.Response),
        1084
      },
      {
        typeof (FlowNode_ReqSupportRanking.MP_Response),
        1085
      },
      {
        typeof (ReqSetSupportUsed.Response),
        1086
      },
      {
        typeof (FlowNode_ReqSupportUsed.MP_Response),
        1087
      },
      {
        typeof (ReqUnitPieceShopItemList.Response),
        1088
      },
      {
        typeof (FlowNode_RequestUnitPieceShopItems.MP_Response),
        1089
      },
      {
        typeof (FlowNode_ReqUpdateBingo.MP_UpdateBingoResponse),
        1090
      },
      {
        typeof (FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse),
        1091
      },
      {
        typeof (ReqJobAbility_OverWrite.Response),
        1092
      },
      {
        typeof (FlowNode_SetAbility.MP_JobAbilityt_OverWriteResponse),
        1093
      },
      {
        typeof (Json_MyPhotonPlayerBinaryParam.UnitDataElem),
        1094
      },
      {
        typeof (Json_MyPhotonPlayerBinaryParam),
        1095
      },
      {
        typeof (FlowNode_StartMultiPlay.RecvData),
        1096
      },
      {
        typeof (ReqUnitPieceShopBuypaid.Response),
        1097
      },
      {
        typeof (FlowNode_UnitPieceBuyItem.MP_Response),
        1098
      },
      {
        typeof (ReqGachaPickup.Response),
        1099
      },
      {
        typeof (FlowNode_ReqGachaPickup.MP_Response),
        1100
      },
      {
        typeof (JukeBoxWindow.ResPlayList),
        1101
      },
      {
        typeof (ReqJukeBox.Response),
        1102
      },
      {
        typeof (FlowNode_ReqJukeBox.MP_Response),
        1103
      },
      {
        typeof (ReqJukeBoxPlaylistAdd.Response),
        1104
      },
      {
        typeof (FlowNode_ReqJukeBoxMylistAdd.MP_Add_Response),
        1105
      },
      {
        typeof (ReqJukeBoxPlaylistDel.Response),
        1106
      },
      {
        typeof (FlowNode_ReqJukeBoxMylistDel.MP_Del_Response),
        1107
      },
      {
        typeof (ReqGuildAttend.Response),
        1108
      },
      {
        typeof (FlowNode_ReqGuildAttend.MP_Response),
        1109
      },
      {
        typeof (ReqGuildRoleBonus.Response),
        1110
      },
      {
        typeof (FlowNode_ReqGuildRoleBonus.MP_Response),
        1111
      },
      {
        typeof (ReqGuildRaid.Response),
        1112
      },
      {
        typeof (FlowNode_ReqGuildRaid.MP_Response),
        1113
      },
      {
        typeof (ReqGuildRaidBtlLog.Response),
        1114
      },
      {
        typeof (FlowNode_ReqGuildRaidBtlLog.MP_Response),
        1115
      },
      {
        typeof (ReqGuildRaidInfo.Response),
        1116
      },
      {
        typeof (FlowNode_ReqGuildRaidInfo.MP_Response),
        1117
      },
      {
        typeof (ReqGuildRaidMail.Response),
        1118
      },
      {
        typeof (FlowNode_ReqGuildRaidMail.MP_Response),
        1119
      },
      {
        typeof (ReqGuildRaidMailRead.Response),
        1120
      },
      {
        typeof (FlowNode_ReqGuildRaidMailRead.MP_Response),
        1121
      },
      {
        typeof (ReqGuildRaidRanking.Response),
        1122
      },
      {
        typeof (FlowNode_ReqGuildRaidRanking.MP_Response),
        1123
      },
      {
        typeof (ReqGuildRaidRankingDamageRound.Response),
        1124
      },
      {
        typeof (FlowNode_ReqGuildRaidRankingDamageRound.MP_Response),
        1125
      },
      {
        typeof (ReqGuildRaidRankingDamageSummary.Response),
        1126
      },
      {
        typeof (FlowNode_ReqGuildRaidRankingDamageSummary.MP_Response),
        1127
      },
      {
        typeof (ReqGuildRaidRankingPort.Response),
        1128
      },
      {
        typeof (FlowNode_ReqGuildRaidRankingPort.MP_Response),
        1129
      },
      {
        typeof (ReqGuildRaidRankingPortBoss.Response),
        1130
      },
      {
        typeof (FlowNode_ReqGuildRaidRankingPortBoss.MP_Response),
        1131
      },
      {
        typeof (FlowNode_ReqGuildRaidReportDetail.MP_Response),
        1132
      },
      {
        typeof (ReqGuildRaidReportSelf.Response),
        1133
      },
      {
        typeof (FlowNode_ReqGuildRaidReportSelf.MP_Response),
        1134
      },
      {
        typeof (ReqGuildRaidRankingReward.Response),
        1135
      },
      {
        typeof (FlowNode_ReqGuildRaidReward.MP_Response),
        1136
      },
      {
        typeof (JSON_GvGNodeData),
        1137
      },
      {
        typeof (JSON_GvGLeagueData),
        1138
      },
      {
        typeof (JSON_GvGLeagueViewGuild),
        1139
      },
      {
        typeof (JSON_GvGUsedUnitData),
        1140
      },
      {
        typeof (JSON_GvGResult),
        1141
      },
      {
        typeof (JSON_GvGLeagueResult),
        1142
      },
      {
        typeof (ReqGvG.Response),
        1143
      },
      {
        typeof (FlowNode_ReqGvG.MP_Response),
        1144
      },
      {
        typeof (JSON_GvGPartyUnit),
        1145
      },
      {
        typeof (ReqGvGBattle.Response),
        1146
      },
      {
        typeof (FlowNode_ReqGvGBattle.MP_Response),
        1147
      },
      {
        typeof (ReqGvGBattleCapture.Response),
        1148
      },
      {
        typeof (FlowNode_ReqGvGBattleCapture.MP_Response),
        1149
      },
      {
        typeof (JSON_GvGPartyNPC),
        1150
      },
      {
        typeof (JSON_GvGParty),
        1151
      },
      {
        typeof (ReqGvGBattleEnemy.Response),
        1152
      },
      {
        typeof (FlowNode_ReqGvGBattleEnemy.MP_Response),
        1153
      },
      {
        typeof (JSON_GvGScoreRanking),
        1154
      },
      {
        typeof (ReqGvGBeatRanking.Response),
        1155
      },
      {
        typeof (FlowNode_ReqGvGBeatRanking.MP_Response),
        1156
      },
      {
        typeof (ReqGvGDefenseRanking.Response),
        1157
      },
      {
        typeof (FlowNode_ReqGvGDefenseRanking.MP_Response),
        1158
      },
      {
        typeof (JSON_GvGHalfTime),
        1159
      },
      {
        typeof (ReqGvGHalfTime.Response),
        1160
      },
      {
        typeof (FlowNode_ReqGvGHalfTime.MP_Response),
        1161
      },
      {
        typeof (ReqGvGLeague.Response),
        1162
      },
      {
        typeof (FlowNode_ReqGvGLeague.MP_Response),
        1163
      },
      {
        typeof (ReqGvGNodeDeclare.Response),
        1164
      },
      {
        typeof (FlowNode_ReqGvGNodeDeclare.MP_Response),
        1165
      },
      {
        typeof (JSON_GvGUsedItems),
        1166
      },
      {
        typeof (ReqGvGNodeDefenseEntry.Response),
        1167
      },
      {
        typeof (FlowNode_ReqGvGNodeDefenseEntry.MP_Response),
        1168
      },
      {
        typeof (ReqGvGNodeDetail.Response),
        1169
      },
      {
        typeof (FlowNode_ReqGvGNodeDetail.MP_Response),
        1170
      },
      {
        typeof (ReqGvGNodeOffenseEntry.Response),
        1171
      },
      {
        typeof (FlowNode_ReqGvGNodeOffenseEntry.MP_Response),
        1172
      },
      {
        typeof (JSON_GvGRankingData),
        1173
      },
      {
        typeof (ReqGvGRankingGroup.Response),
        1174
      },
      {
        typeof (FlowNode_ReqGvGRankingGroup.MP_Response),
        1175
      },
      {
        typeof (ReqGvGReward.Response),
        1176
      },
      {
        typeof (FlowNode_ReqGvGReward.MP_Response),
        1177
      },
      {
        typeof (Json_ChatChannelMasterParam.Fields),
        1178
      },
      {
        typeof (Json_ChatChannelMasterParam),
        1179
      },
      {
        typeof (LoginNewsInfo.JSON_PubInfo),
        1180
      },
      {
        typeof (FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse),
        1181
      },
      {
        typeof (FlowNode_ReqLoginPack.MP_ReqLoginPackResponse),
        1182
      },
      {
        typeof (Json_Notify_Monthly),
        1183
      },
      {
        typeof (ReqMonthlyRecover.Response),
        1184
      },
      {
        typeof (FlowNode_ReqRecoverMonthly.MP_Response),
        1185
      },
      {
        typeof (JSON_MultiSupport),
        1186
      },
      {
        typeof (ReqMultiSupportList.Response),
        1187
      },
      {
        typeof (FlowNode_MultiPlaySupport.MP_Response_GetList),
        1188
      },
      {
        typeof (ReqStoryExChallengeCountReset.Response),
        1189
      },
      {
        typeof (FlowNode_ReqStoryExTotalChallengeCountReset.MP_ReqStoryExChallengeCountResetResponse),
        1190
      },
      {
        typeof (ReqGetRune.Response),
        1191
      },
      {
        typeof (FlowNode_ReqRune.MP_Response),
        1192
      },
      {
        typeof (ReqRuneDisassembly.Response.Rewards),
        1193
      },
      {
        typeof (ReqRuneDisassembly.Response),
        1194
      },
      {
        typeof (FlowNode_ReqRuneDisassembly.MP_Response),
        1195
      },
      {
        typeof (ReqRuneEnhance.Response),
        1196
      },
      {
        typeof (FlowNode_ReqRuneEnhance.MP_Response),
        1197
      },
      {
        typeof (ReqRuneEquip.Response),
        1198
      },
      {
        typeof (FlowNode_ReqRuneEquip.MP_Response),
        1199
      },
      {
        typeof (ReqRuneEvo.Response),
        1200
      },
      {
        typeof (FlowNode_ReqRuneEvo.MP_Response),
        1201
      },
      {
        typeof (ReqRuneFavorite.Response),
        1202
      },
      {
        typeof (FlowNode_ReqRuneFavorite.MP_Response),
        1203
      },
      {
        typeof (ReqRuneParamEnhEvo.Response),
        1204
      },
      {
        typeof (FlowNode_ReqRuneParamEnhEvo.MP_Response),
        1205
      },
      {
        typeof (ReqRuneResetParamBase.Response),
        1206
      },
      {
        typeof (FlowNode_ReqRuneResetParamBase.MP_Response),
        1207
      },
      {
        typeof (ReqRuneResetStatusEvo.Response),
        1208
      },
      {
        typeof (FlowNode_ReqRuneResetStatusEvo.MP_Response),
        1209
      },
      {
        typeof (ReqRuneStorageAdd.Response),
        1210
      },
      {
        typeof (FlowNode_ReqRuneStorageAdd.MP_Response),
        1211
      },
      {
        typeof (ReqGuildTrophy.Response),
        1212
      },
      {
        typeof (FlowNode_ReqGuildTrophy.MP_Response),
        1213
      },
      {
        typeof (ReqGuildTrophyReward.Response),
        1214
      },
      {
        typeof (FlowNode_ReqGuildTrophyReward.MP_Response),
        1215
      },
      {
        typeof (ReqTrophyStarMission.Response),
        1216
      },
      {
        typeof (FlowNode_ReqTrophyStarMission.MP_Response),
        1217
      },
      {
        typeof (ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard),
        1218
      },
      {
        typeof (ReqTrophyStarMissionGetReward.Response),
        1219
      },
      {
        typeof (FlowNode_ReqTrophyStarMissionGetReward.MP_Response),
        1220
      },
      {
        typeof (ReqUnitRentalAdd.Response),
        1221
      },
      {
        typeof (FlowNode_ReqUnitRentalAdd.MP_Response),
        1222
      },
      {
        typeof (ReqUnitRentalExec.Response),
        1223
      },
      {
        typeof (FlowNode_ReqUnitRentalExec.MP_Response),
        1224
      },
      {
        typeof (ReqUnitRentalLeave.Response),
        1225
      },
      {
        typeof (FlowNode_ReqUnitRentalLeave.MP_Response),
        1226
      },
      {
        typeof (JSON_WorldRaidBossChallengedData),
        1227
      },
      {
        typeof (JSON_WorldRaidLogData),
        1228
      },
      {
        typeof (ReqWorldRaid.Response),
        1229
      },
      {
        typeof (FlowNode_ReqWorldRaid.MP_Response),
        1230
      },
      {
        typeof (JSON_WorldRaidBossDetailData),
        1231
      },
      {
        typeof (ReqWorldRaidBoss.Response),
        1232
      },
      {
        typeof (FlowNode_ReqWorldRaidBoss.MP_Response),
        1233
      },
      {
        typeof (JSON_WorldRaidRankingData),
        1234
      },
      {
        typeof (ReqWorldRaidRanking.Response),
        1235
      },
      {
        typeof (FlowNode_ReqWorldRaidRanking.MP_Response),
        1236
      },
      {
        typeof (ReqWorldRaidReward.Response),
        1237
      },
      {
        typeof (FlowNode_ReqWorldRaidReward.MP_Response),
        1238
      },
      {
        typeof (EmbeddedTutorialMasterParams.JSON_EmbededMasterParam),
        1239
      },
      {
        typeof (EmbeddedTutorialMasterParams.JSON_EmbededQuestParam),
        1240
      },
      {
        typeof (JukeBoxParam),
        1241
      },
      {
        typeof (JukeBoxSectionParam),
        1242
      },
      {
        typeof (GuildEmblemParam),
        1243
      },
      {
        typeof (JSON_ProductSaleInfo),
        1244
      },
      {
        typeof (JSON_ProductParam),
        1245
      },
      {
        typeof (JSON_ProductBuyCoinParam),
        1246
      },
      {
        typeof (JSON_ProductParamResponse),
        1247
      },
      {
        typeof (RaidPeriodTimeScheduleParam),
        1248
      },
      {
        typeof (RuneSetEffState),
        1249
      },
      {
        typeof (RuneSetEff),
        1250
      },
      {
        typeof (RuneSlotIndex),
        1251
      },
      {
        typeof (RuneParam),
        1252
      },
      {
        typeof (JSON_RuneLotteryState),
        1253
      },
      {
        typeof (RuneLotteryState),
        1254
      },
      {
        typeof (RuneCost),
        1255
      },
      {
        typeof (RuneDisassembly.Materials),
        1256
      },
      {
        typeof (RuneDisassembly),
        1257
      },
      {
        typeof (RuneMaterial),
        1258
      },
      {
        typeof (JSON_TrophyResponse),
        1259
      },
      {
        typeof (MP_TrophyResponse),
        1260
      },
      {
        typeof (AbilitySlots.MP_JobAbilityt_OverWriteResponse),
        1261
      },
      {
        typeof (ArtifactSlots.MP_ArtifactSet_OverWriteResponse),
        1262
      },
      {
        typeof (JSON_GvGBattleEndParam),
        1263
      },
      {
        typeof (JSON_GvGLeagueGuild),
        1264
      },
      {
        typeof (ReplacePeriod),
        1265
      },
      {
        typeof (ReplaceSprite),
        1266
      },
      {
        typeof (VersusDraftList.VersusDraftMessageData),
        1267
      },
      {
        typeof (ReqSetConceptCardList.Response),
        1268
      },
      {
        typeof (PartyWindow2.MP_Response_SetConceptCardList),
        1269
      },
      {
        typeof (ReqUnitJob_OverWrite.Response),
        1270
      },
      {
        typeof (UnitJobDropdown.MP_UnitJob_OverWriteResponse),
        1271
      },
      {
        typeof (JSON_WorldRaidBossData),
        1272
      },
      {
        typeof (JSON_WorldRaidBossReqData),
        1273
      },
      {
        typeof (JSON_WorldRaidBattleRewardData),
        1274
      },
      {
        typeof (WebAPI.JSON_BaseResponse),
        1275
      },
      {
        typeof (ReqMultiSupportSet.Response),
        1276
      },
      {
        typeof (ReqGuildAttend.RequestParam),
        1277
      },
      {
        typeof (ReqGuildRoleBonus.RequestParam),
        1278
      },
      {
        typeof (ReqGvGBattleExec.Response),
        1279
      },
      {
        typeof (ReqGetRune.RequestParam),
        1280
      },
      {
        typeof (ReqRuneEquip.RequestParam),
        1281
      },
      {
        typeof (ReqRuneEnhance.RequestParam),
        1282
      },
      {
        typeof (ReqRuneEvo.RequestParam),
        1283
      },
      {
        typeof (ReqRuneDisassembly.RequestParam),
        1284
      },
      {
        typeof (ReqRuneResetParamBase.RequestParam),
        1285
      },
      {
        typeof (ReqRuneResetStatusEvo.RequestParam),
        1286
      },
      {
        typeof (ReqRuneParamEnhEvo.RequestParam),
        1287
      },
      {
        typeof (ReqRuneFavorite.RequestParam),
        1288
      }
    };

    internal static object GetFormatter(System.Type t)
    {
      int num;
      if (!GeneratedResolverGetFormatterHelper.lookup.TryGetValue(t, out num))
        return (object) null;
      switch (num)
      {
        case 0:
          return (object) new ListFormatter<int>();
        case 1:
          return (object) new ArrayFormatter<EquipSkillSetting>();
        case 2:
          return (object) new ArrayFormatter<EquipAbilitySetting>();
        case 3:
          return (object) new ListFormatter<AIAction>();
        case 4:
          return (object) new ArrayFormatter<AIPatrolPoint>();
        case 5:
          return (object) new ListFormatter<OString>();
        case 6:
          return (object) new ListFormatter<UnitEntryTrigger>();
        case 7:
          return (object) new ListFormatter<MultiPlayResumeBuff.ResistStatus>();
        case 8:
          return (object) new ArrayFormatter<MultiPlayResumeAbilChg.Data>();
        case 9:
          return (object) new ArrayFormatter<MultiPlayResumeBuff>();
        case 10:
          return (object) new ArrayFormatter<MultiPlayResumeShield>();
        case 11:
          return (object) new ArrayFormatter<MultiPlayResumeMhmDmg>();
        case 12:
          return (object) new ArrayFormatter<MultiPlayResumeFtgt>();
        case 13:
          return (object) new ArrayFormatter<MultiPlayResumeAbilChg>();
        case 14:
          return (object) new ArrayFormatter<MultiPlayResumeAddedAbil>();
        case 15:
          return (object) new ListFormatter<MultiPlayResumeProtect>();
        case 16:
          return (object) new ArrayFormatter<MultiPlayResumeUnitData>();
        case 17:
          return (object) new ArrayFormatter<MultiPlayGimmickEventParam>();
        case 18:
          return (object) new ArrayFormatter<MultiPlayTrickParam>();
        case 19:
          return (object) new ListFormatter<AttackDetailTypes>();
        case 20:
          return (object) new ListFormatter<string>();
        case 21:
          return (object) new ListFormatter<QuestClearUnlockUnitDataParam>();
        case 22:
          return (object) new ArrayFormatter<OShort>();
        case 23:
          return (object) new ListFormatter<TokkouValue>();
        case 24:
          return (object) new ArrayFormatter<RecipeItem>();
        case 25:
          return (object) new ArrayFormatter<ReturnItem>();
        case 26:
          return (object) new ArrayFormatter<RarityEquipEnhanceParam.RankParam>();
        case 27:
          return (object) new ArrayFormatter<EquipData>();
        case 28:
          return (object) new ListFormatter<AbilityData>();
        case 29:
          return (object) new ArrayFormatter<ConceptCardEffectsParam>();
        case 30:
          return (object) new ListFormatter<ConceptLimitUpItemParam>();
        case 31:
          return (object) new ArrayFormatter<BuffEffectParam.Buff>();
        case 32:
          return (object) new ListFormatter<BuffEffect.BuffTarget>();
        case 33:
          return (object) new ListFormatter<ConceptCardEquipEffect>();
        case 34:
          return (object) new ArrayFormatter<ConceptCardData>();
        case 35:
          return (object) new ListFormatter<RuneBuffDataEvoState>();
        case 36:
          return (object) new ArrayFormatter<RuneData>();
        case 37:
          return (object) new ArrayFormatter<BuffEffect.BuffValues>();
        case 38:
          return (object) new ArrayFormatter<OString>();
        case 39:
          return (object) new ArrayFormatter<JobRankParam>();
        case 40:
          return (object) new ArrayFormatter<LearningSkill>();
        case 41:
          return (object) new ListFormatter<InspSkillTriggerParam.TriggerData>();
        case 42:
          return (object) new ListFormatter<InspSkillTriggerParam>();
        case 43:
          return (object) new ListFormatter<InspSkillParam>();
        case 44:
          return (object) new ListFormatter<InspirationSkillData>();
        case 45:
          return (object) new ArrayFormatter<ArtifactData>();
        case 46:
          return (object) new ArrayFormatter<TobiraLearnAbilityParam>();
        case 47:
          return (object) new ListFormatter<TobiraData>();
        case 48:
          return (object) new ArrayFormatter<JobData>();
        case 49:
          return (object) new ArrayFormatter<QuestClearUnlockUnitDataParam>();
        case 50:
          return (object) new ListFormatter<SkillDeriveParam>();
        case 51:
          return (object) new ListFormatter<AbilityDeriveParam>();
        case 52:
          return (object) new ArrayFormatter<SkillAbilityDeriveTriggerParam>();
        case 53:
          return (object) new ArrayFormatter<EUnitCondition>();
        case 54:
          return (object) new ListFormatter<SkillData>();
        case 55:
          return (object) new ListFormatter<Unit>();
        case 56:
          return (object) new ListFormatter<BuffAttachment.ResistStatusBuff>();
        case 57:
          return (object) new ListFormatter<BuffAttachment>();
        case 58:
          return (object) new ListFormatter<CondAttachment>();
        case 59:
          return (object) new ArrayFormatter<SkillCategory>();
        case 60:
          return (object) new ArrayFormatter<ParamTypes>();
        case 61:
          return (object) new ListFormatter<Unit.DropItem>();
        case 62:
          return (object) new ListFormatter<Unit.UnitShield>();
        case 63:
          return (object) new ListFormatter<Unit.UnitProtect>();
        case 64:
          return (object) new ListFormatter<Unit.UnitMhmDamage>();
        case 65:
          return (object) new ListFormatter<Unit.UnitInsp>();
        case 66:
          return (object) new ListFormatter<Unit.UnitForcedTargeting>();
        case 67:
          return (object) new ListFormatter<Unit.AbilityChange.Data>();
        case 68:
          return (object) new ListFormatter<Unit.AbilityChange>();
        case 69:
          return (object) new ArrayFormatter<Json_InspirationSkill>();
        case 70:
          return (object) new ArrayFormatter<Json_CollaboSkill>();
        case 71:
          return (object) new ArrayFormatter<Json_Equip>();
        case 72:
          return (object) new ArrayFormatter<Json_Ability>();
        case 73:
          return (object) new ArrayFormatter<Json_Artifact>();
        case 74:
          return (object) new ArrayFormatter<Json_InspirationSkillExt>();
        case 75:
          return (object) new ArrayFormatter<Json_Job>();
        case 76:
          return (object) new ArrayFormatter<Json_UnitJob>();
        case 77:
          return (object) new ArrayFormatter<JSON_ConceptCard>();
        case 78:
          return (object) new ArrayFormatter<Json_Tobira>();
        case 79:
          return (object) new ArrayFormatter<Json_RuneBuffData>();
        case 80:
          return (object) new ArrayFormatter<Json_RuneData>();
        case 81:
          return (object) new ArrayFormatter<JSON_GuildRaidMailListItem>();
        case 82:
          return (object) new ArrayFormatter<Json_Unit>();
        case 83:
          return (object) new ArrayFormatter<JSON_GuildRaidRankingMemberBoss>();
        case 84:
          return (object) new ArrayFormatter<Json_Item>();
        case 85:
          return (object) new ArrayFormatter<Json_Gift>();
        case 86:
          return (object) new ArrayFormatter<Json_Mail>();
        case 87:
          return (object) new ArrayFormatter<Json_Party>();
        case 88:
          return (object) new ArrayFormatter<Json_Friend>();
        case 89:
          return (object) new ArrayFormatter<Json_Skin>();
        case 90:
          return (object) new ArrayFormatter<Json_LoginBonus>();
        case 91:
          return (object) new ArrayFormatter<Json_PremiumLoginBonusItem>();
        case 92:
          return (object) new ArrayFormatter<Json_PremiumLoginBonus>();
        case 93:
          return (object) new ArrayFormatter<Json_LoginBonusTable>();
        case 94:
          return (object) new ArrayFormatter<Json_MultiFuids>();
        case 95:
          return (object) new ArrayFormatter<Json_VersusCount>();
        case 96:
          return (object) new ArrayFormatter<Json_ExpireItem>();
        case 97:
          return (object) new ArrayFormatter<JSON_TrophyProgress>();
        case 98:
          return (object) new ArrayFormatter<JSON_UnitOverWriteData>();
        case 99:
          return (object) new ArrayFormatter<JSON_PartyOverWrite>();
        case 100:
          return (object) new ArrayFormatter<Json_ExpansionPurchase>();
        case 101:
          return (object) new ArrayFormatter<Json_TrophyConceptCard>();
        case 102:
          return (object) new ArrayFormatter<JSON_SupportRanking>();
        case 103:
          return (object) new ArrayFormatter<JSON_SupportUnitRanking>();
        case 104:
          return (object) new ArrayFormatter<BattleCore.Json_BtlDrop>();
        case 105:
          return (object) new ArrayFormatter<BattleCore.Json_BtlDrop[]>();
        case 106:
          return (object) new ArrayFormatter<Json_BtlRewardConceptCard>();
        case 107:
          return (object) new ArrayFormatter<JSON_QuestProgress>();
        case 108:
          return (object) new ArrayFormatter<JSON_CombatPowerRankingViewGuild>();
        case 109:
          return (object) new ArrayFormatter<JSON_CombatPowerRankingGuildMember>();
        case 110:
          return (object) new ArrayFormatter<ReqDrawCard.CardInfo>();
        case 111:
          return (object) new ArrayFormatter<ReqDrawCard.CardInfo.Card>();
        case 112:
          return (object) new ArrayFormatter<JSON_FixParam>();
        case 113:
          return (object) new ArrayFormatter<JSON_UnitParam>();
        case 114:
          return (object) new ArrayFormatter<JSON_UnitJobOverwriteParam>();
        case 115:
          return (object) new ArrayFormatter<JSON_SkillParam>();
        case 116:
          return (object) new ArrayFormatter<JSON_BuffEffectParam>();
        case 117:
          return (object) new ArrayFormatter<JSON_CondEffectParam>();
        case 118:
          return (object) new ArrayFormatter<JSON_AbilityParam>();
        case 119:
          return (object) new ArrayFormatter<JSON_ItemParam>();
        case 120:
          return (object) new ArrayFormatter<JSON_ArtifactParam>();
        case 121:
          return (object) new ArrayFormatter<JSON_WeaponParam>();
        case 122:
          return (object) new ArrayFormatter<JSON_RecipeParam>();
        case 123:
          return (object) new ArrayFormatter<JSON_JobRankParam>();
        case 124:
          return (object) new ArrayFormatter<JSON_JobParam>();
        case 125:
          return (object) new ArrayFormatter<JSON_JobSetParam>();
        case 126:
          return (object) new ArrayFormatter<JSON_EvaluationParam>();
        case (int) sbyte.MaxValue:
          return (object) new ArrayFormatter<JSON_AIParam>();
        case 128:
          return (object) new ArrayFormatter<JSON_GeoParam>();
        case 129:
          return (object) new ArrayFormatter<JSON_RarityParam>();
        case 130:
          return (object) new ArrayFormatter<JSON_ShopParam>();
        case 131:
          return (object) new ArrayFormatter<JSON_PlayerParam>();
        case 132:
          return (object) new ArrayFormatter<JSON_GrowCurve>();
        case 133:
          return (object) new ArrayFormatter<JSON_GrowParam>();
        case 134:
          return (object) new ArrayFormatter<JSON_LocalNotificationParam>();
        case 135:
          return (object) new ArrayFormatter<JSON_TrophyCategoryParam>();
        case 136:
          return (object) new ArrayFormatter<JSON_ChallengeCategoryParam>();
        case 137:
          return (object) new ArrayFormatter<JSON_TrophyParam>();
        case 138:
          return (object) new ArrayFormatter<JSON_UnlockParam>();
        case 139:
          return (object) new ArrayFormatter<JSON_VipParam>();
        case 140:
          return (object) new ArrayFormatter<JSON_ArenaWinResult>();
        case 141:
          return (object) new ArrayFormatter<JSON_ArenaResult>();
        case 142:
          return (object) new ArrayFormatter<JSON_StreamingMovie>();
        case 143:
          return (object) new ArrayFormatter<JSON_BannerParam>();
        case 144:
          return (object) new ArrayFormatter<JSON_QuestClearUnlockUnitDataParam>();
        case 145:
          return (object) new ArrayFormatter<JSON_AwardParam>();
        case 146:
          return (object) new ArrayFormatter<JSON_LoginInfoParam>();
        case 147:
          return (object) new ArrayFormatter<JSON_CollaboSkillParam>();
        case 148:
          return (object) new ArrayFormatter<JSON_TrickParam>();
        case 149:
          return (object) new ArrayFormatter<JSON_BreakObjParam>();
        case 150:
          return (object) new ArrayFormatter<JSON_VersusMatchingParam>();
        case 151:
          return (object) new ArrayFormatter<JSON_VersusMatchCondParam>();
        case 152:
          return (object) new ArrayFormatter<JSON_TowerScoreThreshold>();
        case 153:
          return (object) new ArrayFormatter<JSON_TowerScore>();
        case 154:
          return (object) new ArrayFormatter<JSON_FriendPresentItemParam>();
        case 155:
          return (object) new ArrayFormatter<JSON_WeatherParam>();
        case 156:
          return (object) new ArrayFormatter<JSON_UnitUnlockTimeParam>();
        case 157:
          return (object) new ArrayFormatter<JSON_TobiraLearnAbilityParam>();
        case 158:
          return (object) new ArrayFormatter<JSON_TobiraParam>();
        case 159:
          return (object) new ArrayFormatter<JSON_TobiraCategoriesParam>();
        case 160:
          return (object) new ArrayFormatter<JSON_TobiraConditionParam>();
        case 161:
          return (object) new ArrayFormatter<JSON_TobiraCondsParam>();
        case 162:
          return (object) new ArrayFormatter<JSON_TobiraCondsUnitParam.JobCond>();
        case 163:
          return (object) new ArrayFormatter<JSON_TobiraCondsUnitParam>();
        case 164:
          return (object) new ArrayFormatter<JSON_TobiraRecipeMaterialParam>();
        case 165:
          return (object) new ArrayFormatter<JSON_TobiraRecipeParam>();
        case 166:
          return (object) new ArrayFormatter<JSON_ConceptCardEquipParam>();
        case 167:
          return (object) new ArrayFormatter<JSON_ConceptCardParam>();
        case 168:
          return (object) new ArrayFormatter<JSON_ConceptCardConditionsParam>();
        case 169:
          return (object) new ArrayFormatter<JSON_ConceptCardTrustRewardItemParam>();
        case 170:
          return (object) new ArrayFormatter<JSON_ConceptCardTrustRewardParam>();
        case 171:
          return (object) new ArrayFormatter<JSON_ConceptCardLsBuffCoefParam>();
        case 172:
          return (object) new ArrayFormatter<JSON_ConceptCardGroup>();
        case 173:
          return (object) new ArrayFormatter<JSON_ConceptLimitUpItem>();
        case 174:
          return (object) new ArrayFormatter<JSON_UnitGroupParam>();
        case 175:
          return (object) new ArrayFormatter<JSON_JobGroupParam>();
        case 176:
          return (object) new ArrayFormatter<JSON_StatusCoefficientParam>();
        case 177:
          return (object) new ArrayFormatter<JSON_CustomTargetParam>();
        case 178:
          return (object) new ArrayFormatter<JSON_SkillAbilityDeriveParam>();
        case 179:
          return (object) new ArrayFormatter<JSON_RaidPeriodParam>();
        case 180:
          return (object) new ArrayFormatter<JSON_RaidPeriodTimeScheduleParam>();
        case 181:
          return (object) new ArrayFormatter<JSON_RaidPeriodTimeParam>();
        case 182:
          return (object) new ArrayFormatter<JSON_RaidAreaParam>();
        case 183:
          return (object) new ArrayFormatter<JSON_RaidBossParam>();
        case 184:
          return (object) new ArrayFormatter<JSON_RaidBattleRewardWeightParam>();
        case 185:
          return (object) new ArrayFormatter<JSON_RaidBattleRewardParam>();
        case 186:
          return (object) new ArrayFormatter<JSON_RaidBeatRewardDataParam>();
        case 187:
          return (object) new ArrayFormatter<JSON_RaidBeatRewardParam>();
        case 188:
          return (object) new ArrayFormatter<JSON_RaidDamageRatioRewardRatioParam>();
        case 189:
          return (object) new ArrayFormatter<JSON_RaidDamageRatioRewardParam>();
        case 190:
          return (object) new ArrayFormatter<JSON_RaidDamageAmountRewardAmountParam>();
        case 191:
          return (object) new ArrayFormatter<JSON_RaidDamageAmountRewardParam>();
        case 192:
          return (object) new ArrayFormatter<JSON_RaidAreaClearRewardDataParam>();
        case 193:
          return (object) new ArrayFormatter<JSON_RaidAreaClearRewardParam>();
        case 194:
          return (object) new ArrayFormatter<JSON_RaidCompleteRewardDataParam>();
        case 195:
          return (object) new ArrayFormatter<JSON_RaidCompleteRewardParam>();
        case 196:
          return (object) new ArrayFormatter<JSON_RaidReward>();
        case 197:
          return (object) new ArrayFormatter<JSON_RaidRewardParam>();
        case 198:
          return (object) new ArrayFormatter<JSON_TipsParam>();
        case 199:
          return (object) new ArrayFormatter<JSON_GuildEmblemParam>();
        case 200:
          return (object) new ArrayFormatter<JSON_GuildFacilityEffectParam>();
        case 201:
          return (object) new ArrayFormatter<JSON_GuildFacilityParam>();
        case 202:
          return (object) new ArrayFormatter<JSON_GuildFacilityLvParam>();
        case 203:
          return (object) new ArrayFormatter<JSON_ConvertUnitPieceExcludeParam>();
        case 204:
          return (object) new ArrayFormatter<JSON_PremiumParam>();
        case 205:
          return (object) new ArrayFormatter<JSON_BuyCoinShopParam>();
        case 206:
          return (object) new ArrayFormatter<JSON_BuyCoinProductParam>();
        case 207:
          return (object) new ArrayFormatter<JSON_BuyCoinRewardItemParam>();
        case 208:
          return (object) new ArrayFormatter<JSON_BuyCoinRewardParam>();
        case 209:
          return (object) new ArrayFormatter<JSON_BuyCoinProductConvertParam>();
        case 210:
          return (object) new ArrayFormatter<JSON_DynamicTransformUnitParam>();
        case 211:
          return (object) new ArrayFormatter<JSON_RecommendedArtifactParam>();
        case 212:
          return (object) new ArrayFormatter<JSON_SkillMotionDataParam>();
        case 213:
          return (object) new ArrayFormatter<JSON_SkillMotionParam>();
        case 214:
          return (object) new ArrayFormatter<JSON_DependStateSpcEffParam>();
        case 215:
          return (object) new ArrayFormatter<JSON_InspSkillDerivation>();
        case 216:
          return (object) new ArrayFormatter<JSON_InspSkillParam>();
        case 217:
          return (object) new ArrayFormatter<JSON_InspSkillTriggerParam.JSON_TriggerData>();
        case 218:
          return (object) new ArrayFormatter<JSON_InspSkillTriggerParam>();
        case 219:
          return (object) new ArrayFormatter<JSON_InspSkillCostParam>();
        case 220:
          return (object) new ArrayFormatter<JSON_InspSkillLvUpCostParam.JSON_CostData>();
        case 221:
          return (object) new ArrayFormatter<JSON_InspSkillLvUpCostParam>();
        case 222:
          return (object) new ArrayFormatter<JSON_HighlightResource>();
        case 223:
          return (object) new ArrayFormatter<JSON_HighlightParam>();
        case 224:
          return (object) new ArrayFormatter<JSON_HighlightGiftData>();
        case 225:
          return (object) new ArrayFormatter<JSON_HighlightGift>();
        case 226:
          return (object) new ArrayFormatter<JSON_GenesisParam>();
        case 227:
          return (object) new ArrayFormatter<JSON_CoinBuyUseBonusParam>();
        case 228:
          return (object) new ArrayFormatter<JSON_CoinBuyUseBonusContentParam>();
        case 229:
          return (object) new ArrayFormatter<JSON_CoinBuyUseBonusRewardSetParam>();
        case 230:
          return (object) new ArrayFormatter<JSON_CoinBuyUseBonusItemParam>();
        case 231:
          return (object) new ArrayFormatter<JSON_CoinBuyUseBonusRewardParam>();
        case 232:
          return (object) new ArrayFormatter<JSON_UnitRentalNotificationDataParam>();
        case 233:
          return (object) new ArrayFormatter<JSON_UnitRentalNotificationParam>();
        case 234:
          return (object) new ArrayFormatter<JSON_UnitRentalParam.QuestInfo>();
        case 235:
          return (object) new ArrayFormatter<JSON_UnitRentalParam>();
        case 236:
          return (object) new ArrayFormatter<JSON_DrawCardRewardParam.Data>();
        case 237:
          return (object) new ArrayFormatter<JSON_DrawCardRewardParam>();
        case 238:
          return (object) new ArrayFormatter<JSON_DrawCardParam.DrawInfo>();
        case 239:
          return (object) new ArrayFormatter<JSON_DrawCardParam>();
        case 240:
          return (object) new ArrayFormatter<JSON_TrophyStarMissionRewardParam.Data>();
        case 241:
          return (object) new ArrayFormatter<JSON_TrophyStarMissionRewardParam>();
        case 242:
          return (object) new ArrayFormatter<JSON_TrophyStarMissionParam.StarSetParam>();
        case 243:
          return (object) new ArrayFormatter<JSON_TrophyStarMissionParam>();
        case 244:
          return (object) new ArrayFormatter<JSON_UnitPieceShopParam>();
        case 245:
          return (object) new ArrayFormatter<JSON_UnitPieceShopGroupCost>();
        case 246:
          return (object) new ArrayFormatter<JSON_UnitPieceShopGroupParam>();
        case 247:
          return (object) new ArrayFormatter<JSON_TwitterMessageDetailParam>();
        case 248:
          return (object) new ArrayFormatter<JSON_TwitterMessageParam>();
        case 249:
          return (object) new ArrayFormatter<JSON_FilterConceptCardConditionParam>();
        case 250:
          return (object) new ArrayFormatter<JSON_FilterConceptCardParam>();
        case 251:
          return (object) new ArrayFormatter<JSON_FilterRuneConditionParam>();
        case 252:
          return (object) new ArrayFormatter<JSON_FilterRuneParam>();
        case 253:
          return (object) new ArrayFormatter<JSON_FilterUnitConditionParam>();
        case 254:
          return (object) new ArrayFormatter<JSON_FilterUnitParam>();
        case (int) byte.MaxValue:
          return (object) new ArrayFormatter<JSON_FilterArtifactParam.Condition>();
        case 256:
          return (object) new ArrayFormatter<JSON_FilterArtifactParam>();
        case 257:
          return (object) new ArrayFormatter<JSON_SortRuneConditionParam>();
        case 258:
          return (object) new ArrayFormatter<JSON_SortRuneParam>();
        case 259:
          return (object) new ArrayFormatter<JSON_RuneParam>();
        case 260:
          return (object) new ArrayFormatter<JSON_RuneLottery>();
        case 261:
          return (object) new ArrayFormatter<JSON_RuneLotteryBaseState>();
        case 262:
          return (object) new ArrayFormatter<JSON_RuneLotteryEvoState>();
        case 263:
          return (object) new ArrayFormatter<JSON_RuneDisassembly>();
        case 264:
          return (object) new ArrayFormatter<JSON_RuneMaterial>();
        case 265:
          return (object) new ArrayFormatter<JSON_RuneCost>();
        case 266:
          return (object) new ArrayFormatter<JSON_RuneSetEffState>();
        case 267:
          return (object) new ArrayFormatter<JSON_RuneSetEff>();
        case 268:
          return (object) new ArrayFormatter<JSON_JukeBoxParam>();
        case 269:
          return (object) new ArrayFormatter<JSON_JukeBoxSectionParam>();
        case 270:
          return (object) new ArrayFormatter<JSON_UnitSameGroupParam>();
        case 271:
          return (object) new ArrayFormatter<JSON_AutoRepeatQuestBoxParam>();
        case 272:
          return (object) new ArrayFormatter<JSON_GuildAttendRewardDetail>();
        case 273:
          return (object) new ArrayFormatter<JSON_GuildAttendParam>();
        case 274:
          return (object) new ArrayFormatter<JSON_GuildAttendReward>();
        case 275:
          return (object) new ArrayFormatter<JSON_GuildAttendRewardParam>();
        case 276:
          return (object) new ArrayFormatter<JSON_GuildRoleBonusDetail>();
        case 277:
          return (object) new ArrayFormatter<JSON_GuildRoleBonus>();
        case 278:
          return (object) new ArrayFormatter<JSON_GUildRoleBonusReward>();
        case 279:
          return (object) new ArrayFormatter<JSON_GuildRoleBonusRewardParam>();
        case 280:
          return (object) new ArrayFormatter<JSON_ResetCostInfoParam>();
        case 281:
          return (object) new ArrayFormatter<JSON_ResetCostParam>();
        case 282:
          return (object) new ArrayFormatter<JSON_ProtectSkillParam>();
        case 283:
          return (object) new ArrayFormatter<JSON_GuildSearchFilterConditionParam>();
        case 284:
          return (object) new ArrayFormatter<JSON_GuildSearchFilterParam>();
        case 285:
          return (object) new ArrayFormatter<JSON_ReplacePeriod>();
        case 286:
          return (object) new ArrayFormatter<JSON_ReplaceSprite>();
        case 287:
          return (object) new ArrayFormatter<JSON_ExpansionPurchaseParam>();
        case 288:
          return (object) new ArrayFormatter<JSON_ExpansionPurchaseQuestParam>();
        case 289:
          return (object) new ArrayFormatter<JSON_SkillAdditionalParam>();
        case 290:
          return (object) new ArrayFormatter<JSON_SkillAntiShieldParam>();
        case 291:
          return (object) new ArrayFormatter<JSON_InitPlayer>();
        case 292:
          return (object) new ArrayFormatter<JSON_InitUnit>();
        case 293:
          return (object) new ArrayFormatter<JSON_InitItem>();
        case 294:
          return (object) new ArrayFormatter<JSON_SectionParam>();
        case 295:
          return (object) new ArrayFormatter<JSON_ArchiveItemsParam>();
        case 296:
          return (object) new ArrayFormatter<JSON_ArchiveParam>();
        case 297:
          return (object) new ArrayFormatter<JSON_ChapterParam>();
        case 298:
          return (object) new ArrayFormatter<JSON_MapParam>();
        case 299:
          return (object) new ArrayFormatter<JSON_QuestParam>();
        case 300:
          return (object) new ArrayFormatter<JSON_InnerObjective>();
        case 301:
          return (object) new ArrayFormatter<JSON_ObjectiveParam>();
        case 302:
          return (object) new ArrayFormatter<JSON_MagnificationParam>();
        case 303:
          return (object) new ArrayFormatter<JSON_QuestCondParam>();
        case 304:
          return (object) new ArrayFormatter<JSON_QuestPartyParam>();
        case 305:
          return (object) new ArrayFormatter<JSON_QuestCampaignParentParam>();
        case 306:
          return (object) new ArrayFormatter<JSON_QuestCampaignChildParam>();
        case 307:
          return (object) new ArrayFormatter<JSON_QuestCampaignTrust>();
        case 308:
          return (object) new ArrayFormatter<JSON_QuestCampaignInspSkill>();
        case 309:
          return (object) new ArrayFormatter<JSON_TowerFloorParam>();
        case 310:
          return (object) new ArrayFormatter<JSON_TowerRewardItem>();
        case 311:
          return (object) new ArrayFormatter<JSON_TowerRewardParam>();
        case 312:
          return (object) new ArrayFormatter<JSON_TowerRoundRewardItem>();
        case 313:
          return (object) new ArrayFormatter<JSON_TowerRoundRewardParam>();
        case 314:
          return (object) new ArrayFormatter<JSON_TowerParam>();
        case 315:
          return (object) new ArrayFormatter<JSON_VersusTowerParam>();
        case 316:
          return (object) new ArrayFormatter<JSON_VersusSchedule>();
        case 317:
          return (object) new ArrayFormatter<JSON_VersusCoin>();
        case 318:
          return (object) new ArrayFormatter<JSON_MultiTowerFloorParam>();
        case 319:
          return (object) new ArrayFormatter<JSON_MultiTowerRewardItem>();
        case 320:
          return (object) new ArrayFormatter<JSON_MultiTowerRewardParam>();
        case 321:
          return (object) new ArrayFormatter<JSON_MapEffectParam>();
        case 322:
          return (object) new ArrayFormatter<JSON_WeatherSetParam>();
        case 323:
          return (object) new ArrayFormatter<JSON_RankingQuestParam>();
        case 324:
          return (object) new ArrayFormatter<JSON_RankingQuestScheduleParam>();
        case 325:
          return (object) new ArrayFormatter<JSON_RankingQuestRewardParam>();
        case 326:
          return (object) new ArrayFormatter<JSON_VersusWinBonusReward>();
        case 327:
          return (object) new ArrayFormatter<JSON_VersusFirstWinBonus>();
        case 328:
          return (object) new ArrayFormatter<JSON_VersusStreakWinSchedule>();
        case 329:
          return (object) new ArrayFormatter<JSON_VersusStreakWinBonus>();
        case 330:
          return (object) new ArrayFormatter<JSON_VersusRule>();
        case 331:
          return (object) new ArrayFormatter<JSON_VersusCoinCampParam>();
        case 332:
          return (object) new ArrayFormatter<JSON_VersusEnableTimeScheduleParam>();
        case 333:
          return (object) new ArrayFormatter<JSON_VersusEnableTimeParam>();
        case 334:
          return (object) new ArrayFormatter<JSON_VersusRankParam>();
        case 335:
          return (object) new ArrayFormatter<JSON_VersusRankClassParam>();
        case 336:
          return (object) new ArrayFormatter<JSON_VersusRankRankingRewardParam>();
        case 337:
          return (object) new ArrayFormatter<JSON_VersusRankRewardRewardParam>();
        case 338:
          return (object) new ArrayFormatter<JSON_VersusRankRewardParam>();
        case 339:
          return (object) new ArrayFormatter<JSON_VersusRankMissionScheduleParam>();
        case 340:
          return (object) new ArrayFormatter<JSON_VersusRankMissionParam>();
        case 341:
          return (object) new ArrayFormatter<JSON_QuestLobbyNewsParam>();
        case 342:
          return (object) new ArrayFormatter<JSON_GuerrillaShopAdventQuestParam>();
        case 343:
          return (object) new ArrayFormatter<JSON_GuerrillaShopScheduleAdventParam>();
        case 344:
          return (object) new ArrayFormatter<JSON_GuerrillaShopScheduleParam>();
        case 345:
          return (object) new ArrayFormatter<JSON_VersusDraftDeckParam>();
        case 346:
          return (object) new ArrayFormatter<JSON_VersusDraftUnitParam>();
        case 347:
          return (object) new ArrayFormatter<JSON_GenesisStarRewardParam>();
        case 348:
          return (object) new ArrayFormatter<JSON_GenesisStarParam>();
        case 349:
          return (object) new ArrayFormatter<JSON_GenesisChapterModeInfoParam>();
        case 350:
          return (object) new ArrayFormatter<JSON_GenesisChapterParam>();
        case 351:
          return (object) new ArrayFormatter<JSON_GenesisRewardDataParam>();
        case 352:
          return (object) new ArrayFormatter<JSON_GenesisRewardParam>();
        case 353:
          return (object) new ArrayFormatter<JSON_GenesisLapBossParam.LapInfo>();
        case 354:
          return (object) new ArrayFormatter<JSON_GenesisLapBossParam>();
        case 355:
          return (object) new ArrayFormatter<JSON_AdvanceStarRewardParam>();
        case 356:
          return (object) new ArrayFormatter<JSON_AdvanceStarParam>();
        case 357:
          return (object) new ArrayFormatter<JSON_AdvanceEventModeInfoParam>();
        case 358:
          return (object) new ArrayFormatter<JSON_AdvanceEventParam>();
        case 359:
          return (object) new ArrayFormatter<JSON_AdvanceRewardDataParam>();
        case 360:
          return (object) new ArrayFormatter<JSON_AdvanceRewardParam>();
        case 361:
          return (object) new ArrayFormatter<JSON_AdvanceLapBossParam.LapInfo>();
        case 362:
          return (object) new ArrayFormatter<JSON_AdvanceLapBossParam>();
        case 363:
          return (object) new ArrayFormatter<JSON_GuildRaidBossParam>();
        case 364:
          return (object) new ArrayFormatter<JSON_GuildRaidCoolDaysParam>();
        case 365:
          return (object) new ArrayFormatter<JSON_GuildRaidScoreDataParam>();
        case 366:
          return (object) new ArrayFormatter<JSON_GuildRaidScoreParam>();
        case 367:
          return (object) new ArrayFormatter<JSON_GuildRaidPeriodTime>();
        case 368:
          return (object) new ArrayFormatter<JSON_GuildRaidPeriodParam>();
        case 369:
          return (object) new ArrayFormatter<JSON_GuildRaidReward>();
        case 370:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardParam>();
        case 371:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardDmgRankingRankParam>();
        case 372:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardDmgRankingParam>();
        case 373:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardDmgRatio>();
        case 374:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardDmgRatioParam>();
        case 375:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardRound>();
        case 376:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardRoundParam>();
        case 377:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardRankingDataParam>();
        case 378:
          return (object) new ArrayFormatter<JSON_GuildRaidRewardRankingParam>();
        case 379:
          return (object) new ArrayFormatter<JSON_GvGPeriodParam>();
        case 380:
          return (object) new ArrayFormatter<JSON_GvGNodeParam>();
        case 381:
          return (object) new ArrayFormatter<JSON_GvGNPCPartyDetailParam>();
        case 382:
          return (object) new ArrayFormatter<JSON_GvGNPCPartyParam>();
        case 383:
          return (object) new ArrayFormatter<JSON_GvGNPCUnitParam>();
        case 384:
          return (object) new ArrayFormatter<JSON_GvGRewardRankingDetailParam>();
        case 385:
          return (object) new ArrayFormatter<JSON_GvGRewardRankingParam>();
        case 386:
          return (object) new ArrayFormatter<JSON_GvGRewardDetailParam>();
        case 387:
          return (object) new ArrayFormatter<JSON_GvGRewardParam>();
        case 388:
          return (object) new ArrayFormatter<JSON_GvGRuleParam>();
        case 389:
          return (object) new ArrayFormatter<JSON_GvGNodeReward>();
        case 390:
          return (object) new ArrayFormatter<JSON_GvGNodeRewardParam>();
        case 391:
          return (object) new ArrayFormatter<JSON_GvGLeagueParam>();
        case 392:
          return (object) new ArrayFormatter<JSON_GvGCalcRateSettingParam>();
        case 393:
          return (object) new ArrayFormatter<JSON_WorldRaidParam.BossInfo>();
        case 394:
          return (object) new ArrayFormatter<JSON_WorldRaidParam>();
        case 395:
          return (object) new ArrayFormatter<JSON_WorldRaidBossParam>();
        case 396:
          return (object) new ArrayFormatter<JSON_WorldRaidRewardParam.Reward>();
        case 397:
          return (object) new ArrayFormatter<JSON_WorldRaidRewardParam>();
        case 398:
          return (object) new ArrayFormatter<JSON_WorldRaidDamageRewardParam.Reward>();
        case 399:
          return (object) new ArrayFormatter<JSON_WorldRaidDamageRewardParam>();
        case 400:
          return (object) new ArrayFormatter<JSON_WorldRaidDamageLotteryParam.Reward>();
        case 401:
          return (object) new ArrayFormatter<JSON_WorldRaidDamageLotteryParam>();
        case 402:
          return (object) new ArrayFormatter<JSON_WorldRaidRankingRewardParam.Reward>();
        case 403:
          return (object) new ArrayFormatter<JSON_WorldRaidRankingRewardParam>();
        case 404:
          return (object) new ArrayFormatter<JSON_SupportHistory>();
        case 405:
          return (object) new ArrayFormatter<Json_UnitPieceShopItem>();
        case 406:
          return (object) new ArrayFormatter<Json_MyPhotonPlayerBinaryParam.UnitDataElem>();
        case 407:
          return (object) new ArrayFormatter<Json_MyPhotonPlayerBinaryParam>();
        case 408:
          return (object) new ArrayFormatter<Json_GachaPickups>();
        case 409:
          return (object) new ArrayFormatter<JukeBoxWindow.ResPlayList>();
        case 410:
          return (object) new ArrayFormatter<JSON_GuildFacilityData>();
        case 411:
          return (object) new ArrayFormatter<JSON_GuildRaidBattleLog>();
        case 412:
          return (object) new ArrayFormatter<JSON_GuildRaidChallengingPlayer>();
        case 413:
          return (object) new ArrayFormatter<JSON_GuildRaidRanking>();
        case 414:
          return (object) new ArrayFormatter<JSON_GuildRaidRankingDamage>();
        case 415:
          return (object) new ArrayFormatter<JSON_GuildRaidRankingMember>();
        case 416:
          return (object) new ArrayFormatter<JSON_GuildRaidReport>();
        case 417:
          return (object) new ArrayFormatter<JSON_GvGNodeData>();
        case 418:
          return (object) new ArrayFormatter<JSON_GvGLeagueViewGuild>();
        case 419:
          return (object) new ArrayFormatter<JSON_GvGUsedUnitData>();
        case 420:
          return (object) new ArrayFormatter<JSON_GvGPartyUnit>();
        case 421:
          return (object) new ArrayFormatter<JSON_GvGPartyNPC>();
        case 422:
          return (object) new ArrayFormatter<JSON_GvGParty>();
        case 423:
          return (object) new ArrayFormatter<JSON_GvGScoreRanking>();
        case 424:
          return (object) new ArrayFormatter<JSON_GvGHalfTime>();
        case 425:
          return (object) new ArrayFormatter<JSON_GvGUsedItems>();
        case 426:
          return (object) new ArrayFormatter<JSON_GvGRankingData>();
        case 427:
          return (object) new ArrayFormatter<Json_ChatChannelMasterParam>();
        case 428:
          return (object) new ArrayFormatter<JSON_MultiSupport>();
        case 429:
          return (object) new ArrayFormatter<Json_RuneEnforceGaugeData>();
        case 430:
          return (object) new ArrayFormatter<ReqRuneDisassembly.Response.Rewards>();
        case 431:
          return (object) new ArrayFormatter<ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard>();
        case 432:
          return (object) new ArrayFormatter<JSON_WorldRaidBossChallengedData>();
        case 433:
          return (object) new ArrayFormatter<JSON_WorldRaidLogData>();
        case 434:
          return (object) new ArrayFormatter<JSON_WorldRaidRankingData>();
        case 435:
          return (object) new ArrayFormatter<JSON_ProductParam>();
        case 436:
          return (object) new ArrayFormatter<JSON_ProductBuyCoinParam>();
        case 437:
          return (object) new ArrayFormatter<RuneSetEffState>();
        case 438:
          return (object) new ArrayFormatter<RuneCost>();
        case 439:
          return (object) new ListFormatter<RuneDisassembly.Materials>();
        case 440:
          return (object) new ListFormatter<ReplacePeriod>();
        case 441:
          return (object) new EBattleRewardTypeFormatter();
        case 442:
          return (object) new eAIActionNoExecActFormatter();
        case 443:
          return (object) new eAIActionNextTurnActFormatter();
        case 444:
          return (object) new eMapUnitCtCalcTypeFormatter();
        case 445:
          return (object) new AIActionTypeFormatter();
        case 446:
          return (object) new EEventTriggerFormatter();
        case 447:
          return (object) new EEventTypeFormatter();
        case 448:
          return (object) new EEventGimmickFormatter();
        case 449:
          return (object) new ProtectTypesFormatter();
        case 450:
          return (object) new DamageTypesFormatter();
        case 451:
          return (object) new eAddtionalCondFormatter();
        case 452:
          return (object) new ESkillTypeFormatter();
        case 453:
          return (object) new ESkillTimingFormatter();
        case 454:
          return (object) new ESkillConditionFormatter();
        case 455:
          return (object) new ELineTypeFormatter();
        case 456:
          return (object) new ESelectTypeFormatter();
        case 457:
          return (object) new ECastTypesFormatter();
        case 458:
          return (object) new ESkillTargetFormatter();
        case 459:
          return (object) new SkillEffectTypesFormatter();
        case 460:
          return (object) new SkillParamCalcTypesFormatter();
        case 461:
          return (object) new EElementFormatter();
        case 462:
          return (object) new AttackTypesFormatter();
        case 463:
          return (object) new AttackDetailTypesFormatter();
        case 464:
          return (object) new ShieldTypesFormatter();
        case 465:
          return (object) new JewelDamageTypesFormatter();
        case 466:
          return (object) new eKnockBackDirFormatter();
        case 467:
          return (object) new eKnockBackDsFormatter();
        case 468:
          return (object) new eDamageDispTypeFormatter();
        case 469:
          return (object) new eTeleportTypeFormatter();
        case 470:
          return (object) new eTrickSetTypeFormatter();
        case 471:
          return (object) new eAbsorbAndGiveFormatter();
        case 472:
          return (object) new eSkillTargetExFormatter();
        case 473:
          return (object) new eTeleportSkillPosFormatter();
        case 474:
          return (object) new EUnlockTypesFormatter();
        case 475:
          return (object) new ESexFormatter();
        case 476:
          return (object) new EUnitTypeFormatter();
        case 477:
          return (object) new JobTypesFormatter();
        case 478:
          return (object) new RoleTypesFormatter();
        case 479:
          return (object) new EItemTypeFormatter();
        case 480:
          return (object) new EItemTabTypeFormatter();
        case 481:
          return (object) new GalleryVisibilityTypeFormatter();
        case 482:
          return (object) new eCardTypeFormatter();
        case 483:
          return (object) new EffectCheckTargetsFormatter();
        case 484:
          return (object) new EffectCheckTimingsFormatter();
        case 485:
          return (object) new EAppTypeFormatter();
        case 486:
          return (object) new EEffRangeFormatter();
        case 487:
          return (object) new BuffFlagsFormatter();
        case 488:
          return (object) new ParamTypesFormatter();
        case 489:
          return (object) new BuffTypesFormatter();
        case 490:
          return (object) new ArtifactTypesFormatter();
        case 491:
          return (object) new BuffMethodTypesFormatter();
        case 492:
          return (object) new eMovTypeFormatter();
        case 493:
          return (object) new EAbilityTypeFormatter();
        case 494:
          return (object) new EAbilitySlotFormatter();
        case 495:
          return (object) new EUseConditionsTypeFormatter();
        case 496:
          return (object) new EAbilityTypeDetailFormatter();
        case 497:
          return (object) new eInspSkillTriggerTypeFormatter();
        case 498:
          return (object) new CategoryFormatter();
        case 499:
          return (object) new AddTypeFormatter();
        case 500:
          return (object) new TemporaryFlagsFormatter();
        case 501:
          return (object) new UnitBadgeTypesFormatter();
        case 502:
          return (object) new ESkillAbilityDeriveCondsFormatter();
        case 503:
          return (object) new ConditionEffectTypesFormatter();
        case 504:
          return (object) new EUnitConditionFormatter();
        case 505:
          return (object) new SkillEffectTargetsFormatter();
        case 506:
          return (object) new StatusTypesFormatter();
        case 507:
          return (object) new ParamPrioritiesFormatter();
        case 508:
          return (object) new SkillCategoryFormatter();
        case 509:
          return (object) new EUnitSideFormatter();
        case 510:
          return (object) new eTypeMhmDamageFormatter();
        case 511:
          return (object) new EUnitDirectionFormatter();
        case 512:
          return (object) new eMapBreakClashTypeFormatter();
        case 513:
          return (object) new eMapBreakAITypeFormatter();
        case 514:
          return (object) new eMapBreakSideTypeFormatter();
        case 515:
          return (object) new eMapBreakRayTypeFormatter();
        case 516:
          return (object) new FriendStatesFormatter();
        case 517:
          return (object) new RewardStatusFormatter();
        case 518:
          return (object) new eUnlockTypeFormatter();
        case 519:
          return (object) new BattleCore_Json_BtlDropFormatter();
        case 520:
          return (object) new UnitEntryTriggerFormatter();
        case 521:
          return (object) new OIntFormatter();
        case 522:
          return (object) new OBoolFormatter();
        case 523:
          return (object) new GeoParamFormatter();
        case 524:
          return (object) new GridFormatter();
        case 525:
          return (object) new OStringFormatter();
        case 526:
          return (object) new SkillLockConditionFormatter();
        case 527:
          return (object) new EquipSkillSettingFormatter();
        case 528:
          return (object) new EquipAbilitySettingFormatter();
        case 529:
          return (object) new AIActionFormatter();
        case 530:
          return (object) new AIActionTableFormatter();
        case 531:
          return (object) new AIPatrolPointFormatter();
        case 532:
          return (object) new AIPatrolTableFormatter();
        case 533:
          return (object) new MapBreakObjFormatter();
        case 534:
          return (object) new OIntVector2Formatter();
        case 535:
          return (object) new EventTriggerFormatter();
        case 536:
          return (object) new NPCSettingFormatter();
        case 537:
          return (object) new MultiPlayResumeBuff_ResistStatusFormatter();
        case 538:
          return (object) new MultiPlayResumeBuffFormatter();
        case 539:
          return (object) new MultiPlayResumeShieldFormatter();
        case 540:
          return (object) new MultiPlayResumeMhmDmgFormatter();
        case 541:
          return (object) new MultiPlayResumeFtgtFormatter();
        case 542:
          return (object) new MultiPlayResumeAbilChg_DataFormatter();
        case 543:
          return (object) new MultiPlayResumeAbilChgFormatter();
        case 544:
          return (object) new MultiPlayResumeAddedAbilFormatter();
        case 545:
          return (object) new MultiPlayResumeProtectFormatter();
        case 546:
          return (object) new MultiPlayResumeUnitDataFormatter();
        case 547:
          return (object) new MultiPlayGimmickEventParamFormatter();
        case 548:
          return (object) new MultiPlayTrickParamFormatter();
        case 549:
          return (object) new MultiPlayResumeParam_WeatherInfoFormatter();
        case 550:
          return (object) new MultiPlayResumeParamFormatter();
        case 551:
          return (object) new SkillRankUpValueFormatter();
        case 552:
          return (object) new ProtectSkillParamFormatter();
        case 553:
          return (object) new SkillAdditionalParamFormatter();
        case 554:
          return (object) new SkillRankUpValueShortFormatter();
        case 555:
          return (object) new SkillAntiShieldParamFormatter();
        case 556:
          return (object) new SkillParamFormatter();
        case 557:
          return (object) new QuestClearUnlockUnitDataParamFormatter();
        case 558:
          return (object) new FlagManagerFormatter();
        case 559:
          return (object) new OShortFormatter();
        case 560:
          return (object) new StatusParamFormatter();
        case 561:
          return (object) new EnchantParamFormatter();
        case 562:
          return (object) new UnitParam_StatusFormatter();
        case 563:
          return (object) new UnitParam_NoJobStatusFormatter();
        case 564:
          return (object) new UnitParamFormatter();
        case 565:
          return (object) new ElementParamFormatter();
        case 566:
          return (object) new BattleBonusParamFormatter();
        case 567:
          return (object) new TokkouValueFormatter();
        case 568:
          return (object) new TokkouParamFormatter();
        case 569:
          return (object) new BaseStatusFormatter();
        case 570:
          return (object) new RecipeItemFormatter();
        case 571:
          return (object) new RecipeParamFormatter();
        case 572:
          return (object) new ItemParamFormatter();
        case 573:
          return (object) new ReturnItemFormatter();
        case 574:
          return (object) new RarityEquipEnhanceParam_RankParamFormatter();
        case 575:
          return (object) new RarityEquipEnhanceParamFormatter();
        case 576:
          return (object) new RarityParamFormatter();
        case 577:
          return (object) new EquipDataFormatter();
        case 578:
          return (object) new OLongFormatter();
        case 579:
          return (object) new ConceptCardEffectsParamFormatter();
        case 580:
          return (object) new ConceptLimitUpItemParamFormatter();
        case 581:
          return (object) new ConceptCardParamFormatter();
        case 582:
          return (object) new BuffEffectParam_BuffFormatter();
        case 583:
          return (object) new BuffEffectParamFormatter();
        case 584:
          return (object) new BuffEffect_BuffTargetFormatter();
        case 585:
          return (object) new BuffEffectFormatter();
        case 586:
          return (object) new ConceptCardEquipEffectFormatter();
        case 587:
          return (object) new ConceptCardDataFormatter();
        case 588:
          return (object) new RuneLotteryBaseStateFormatter();
        case 589:
          return (object) new RuneBuffDataBaseStateFormatter();
        case 590:
          return (object) new RuneLotteryEvoStateFormatter();
        case 591:
          return (object) new RuneBuffDataEvoStateFormatter();
        case 592:
          return (object) new RuneStateDataFormatter();
        case 593:
          return (object) new RuneDataFormatter();
        case 594:
          return (object) new ArtifactParamFormatter();
        case 595:
          return (object) new BuffEffect_BuffValuesFormatter();
        case 596:
          return (object) new JobRankParamFormatter();
        case 597:
          return (object) new JobParamFormatter();
        case 598:
          return (object) new ItemDataFormatter();
        case 599:
          return (object) new LearningSkillFormatter();
        case 600:
          return (object) new AbilityParamFormatter();
        case 601:
          return (object) new InspSkillTriggerParam_TriggerDataFormatter();
        case 602:
          return (object) new InspSkillTriggerParamFormatter();
        case 603:
          return (object) new InspSkillParamFormatter();
        case 604:
          return (object) new InspirationSkillDataFormatter();
        case 605:
          return (object) new ArtifactDataFormatter();
        case 606:
          return (object) new JobDataFormatter();
        case 607:
          return (object) new TobiraLearnAbilityParamFormatter();
        case 608:
          return (object) new TobiraParamFormatter();
        case 609:
          return (object) new TobiraDataFormatter();
        case 610:
          return (object) new UnitDataFormatter();
        case 611:
          return (object) new SkillDeriveParamFormatter();
        case 612:
          return (object) new SkillAbilityDeriveTriggerParamFormatter();
        case 613:
          return (object) new SkillAbilityDeriveParamFormatter();
        case 614:
          return (object) new AbilityDeriveParamFormatter();
        case 615:
          return (object) new AbilityDataFormatter();
        case 616:
          return (object) new CondEffectParamFormatter();
        case 617:
          return (object) new CondEffectFormatter();
        case 618:
          return (object) new SkillDataFormatter();
        case 619:
          return (object) new BuffAttachment_ResistStatusBuffFormatter();
        case 620:
          return (object) new BuffAttachmentFormatter();
        case 621:
          return (object) new CondAttachmentFormatter();
        case 622:
          return (object) new AIParamFormatter();
        case 623:
          return (object) new Unit_DropItemFormatter();
        case 624:
          return (object) new Unit_UnitDropFormatter();
        case 625:
          return (object) new Unit_UnitStealFormatter();
        case 626:
          return (object) new Unit_UnitShieldFormatter();
        case 627:
          return (object) new Unit_UnitProtectFormatter();
        case 628:
          return (object) new Unit_UnitMhmDamageFormatter();
        case 629:
          return (object) new Unit_UnitInspFormatter();
        case 630:
          return (object) new Unit_UnitForcedTargetingFormatter();
        case 631:
          return (object) new Unit_AbilityChange_DataFormatter();
        case 632:
          return (object) new Unit_AbilityChangeFormatter();
        case 633:
          return (object) new DynamicTransformUnitParamFormatter();
        case 634:
          return (object) new BuffBitFormatter();
        case 635:
          return (object) new UnitFormatter();
        case 636:
          return (object) new MultiPlayResumeSkillDataFormatter();
        case 637:
          return (object) new Json_BtlRewardConceptCardFormatter();
        case 638:
          return (object) new SceneBattle_MultiPlayRecvDataFormatter();
        case 639:
          return (object) new Json_InspirationSkillFormatter();
        case 640:
          return (object) new Json_ArtifactFormatter();
        case 641:
          return (object) new JSON_ConceptCardFormatter();
        case 642:
          return (object) new Json_GachaPickupsFormatter();
        case 643:
          return (object) new Json_DropInfoFormatter();
        case 644:
          return (object) new JSON_PlayerGuildFormatter();
        case 645:
          return (object) new JSON_ViewGuildFormatter();
        case 646:
          return (object) new Json_MasterAbilityFormatter();
        case 647:
          return (object) new Json_CollaboSkillFormatter();
        case 648:
          return (object) new Json_CollaboAbilityFormatter();
        case 649:
          return (object) new Json_EquipFormatter();
        case 650:
          return (object) new Json_AbilityFormatter();
        case 651:
          return (object) new Json_InspirationSkillExtFormatter();
        case 652:
          return (object) new Json_JobSelectableFormatter();
        case 653:
          return (object) new Json_JobFormatter();
        case 654:
          return (object) new Json_UnitJobFormatter();
        case 655:
          return (object) new Json_UnitSelectableFormatter();
        case 656:
          return (object) new Json_TobiraFormatter();
        case 657:
          return (object) new Json_RuneBuffDataFormatter();
        case 658:
          return (object) new Json_RuneStateDataFormatter();
        case 659:
          return (object) new Json_RuneDataFormatter();
        case 660:
          return (object) new Json_UnitFormatter();
        case 661:
          return (object) new JSON_GuildMemberFormatter();
        case 662:
          return (object) new JSON_CombatPowerRankingViewGuildFormatter();
        case 663:
          return (object) new JSON_CombatPowerRankingGuildMemberFormatter();
        case 664:
          return (object) new JSON_GuildFacilityDataFormatter();
        case 665:
          return (object) new JSON_GuildRaidPrevFormatter();
        case 666:
          return (object) new JSON_GuildRaidCurrentFormatter();
        case 667:
          return (object) new JSON_GuildRaidBattlePointFormatter();
        case 668:
          return (object) new JSON_GuildRaidBossInfoFormatter();
        case 669:
          return (object) new JSON_GuildRaidDataFormatter();
        case 670:
          return (object) new JSON_GuildRaidChallengingPlayerFormatter();
        case 671:
          return (object) new JSON_GuildRaidKnockDownInfoFormatter();
        case 672:
          return (object) new JSON_GuildRaidMailListItemFormatter();
        case 673:
          return (object) new JSON_GuildRaidMailOptionFormatter();
        case 674:
          return (object) new JSON_GuildRaidMailFormatter();
        case 675:
          return (object) new JSON_GuildRaidDeckFormatter();
        case 676:
          return (object) new JSON_GuildRaidBattleLogFormatter();
        case 677:
          return (object) new JSON_GuildRaidReportFormatter();
        case 678:
          return (object) new JSON_GuildRaidRankingGuildFormatter();
        case 679:
          return (object) new JSON_GuildRaidRankingFormatter();
        case 680:
          return (object) new JSON_GuildRaidRankingMemberBossFormatter();
        case 681:
          return (object) new JSON_GuildRaidRankingMemberFormatter();
        case 682:
          return (object) new JSON_GuildRaidRankingDamageFormatter();
        case 683:
          return (object) new JSON_GuildRaidGuildDataFormatter();
        case 684:
          return (object) new JSON_GuildRaidRankingRewardDataFormatter();
        case 685:
          return (object) new JSON_GuildRaidGuildRankingFormatter();
        case 686:
          return (object) new OByteFormatter();
        case 687:
          return (object) new OSbyteFormatter();
        case 688:
          return (object) new OUIntFormatter();
        case 689:
          return (object) new Json_CoinFormatter();
        case 690:
          return (object) new Json_HikkoshiFormatter();
        case 691:
          return (object) new Json_StaminaFormatter();
        case 692:
          return (object) new Json_CaveFormatter();
        case 693:
          return (object) new Json_AbilityUpFormatter();
        case 694:
          return (object) new Json_ArenaFormatter();
        case 695:
          return (object) new Json_TourFormatter();
        case 696:
          return (object) new Json_VipFormatter();
        case 697:
          return (object) new Json_PremiumFormatter();
        case 698:
          return (object) new Json_FreeGachaFormatter();
        case 699:
          return (object) new Json_PaidGachaFormatter();
        case 700:
          return (object) new Json_FriendsFormatter();
        case 701:
          return (object) new Json_MultiOptionFormatter();
        case 702:
          return (object) new Json_GuerrillaShopPeriodFormatter();
        case 703:
          return (object) new Json_PlayerDataFormatter();
        case 704:
          return (object) new Json_ItemFormatter();
        case 705:
          return (object) new Json_GiftConceptCardFormatter();
        case 706:
          return (object) new Json_GiftFormatter();
        case 707:
          return (object) new Json_MailFormatter();
        case 708:
          return (object) new Json_PartyFormatter();
        case 709:
          return (object) new Json_FriendFormatter();
        case 710:
          return (object) new Json_SkinFormatter();
        case 711:
          return (object) new Json_LoginBonusVipFormatter();
        case 712:
          return (object) new Json_LoginBonusFormatter();
        case 713:
          return (object) new Json_PremiumLoginBonusItemFormatter();
        case 714:
          return (object) new Json_PremiumLoginBonusFormatter();
        case 715:
          return (object) new Json_LoginBonusTableFormatter();
        case 716:
          return (object) new Json_NotifyFormatter();
        case 717:
          return (object) new Json_MultiFuidsFormatter();
        case 718:
          return (object) new Json_VersusCountFormatter();
        case 719:
          return (object) new Json_VersusFormatter();
        case 720:
          return (object) new Json_ExpireItemFormatter();
        case 721:
          return (object) new JSON_TrophyProgressFormatter();
        case 722:
          return (object) new JSON_UnitOverWriteDataFormatter();
        case 723:
          return (object) new JSON_PartyOverWriteFormatter();
        case 724:
          return (object) new JSON_StoryExChallengeCountFormatter();
        case 725:
          return (object) new Json_ExpansionPurchaseFormatter();
        case 726:
          return (object) new Json_PlayerDataAllFormatter();
        case 727:
          return (object) new Json_TrophyPlayerDataFormatter();
        case 728:
          return (object) new Json_TrophyConceptCardFormatter();
        case 729:
          return (object) new Json_TrophyConceptCardsFormatter();
        case 730:
          return (object) new ReqTrophyStarMission_StarMission_InfoFormatter();
        case 731:
          return (object) new ReqTrophyStarMission_StarMissionFormatter();
        case 732:
          return (object) new Json_TrophyPlayerDataAllFormatter();
        case 733:
          return (object) new Json_UnitSkinFormatter();
        case 734:
          return (object) new JSON_SupportHistoryFormatter();
        case 735:
          return (object) new JSON_SupportMyInfoFormatter();
        case 736:
          return (object) new JSON_SupportRankingGuildFormatter();
        case 737:
          return (object) new JSON_SupportRankingFormatter();
        case 738:
          return (object) new JSON_SupportRankingUserFormatter();
        case 739:
          return (object) new JSON_SupportUnitRankingFormatter();
        case 740:
          return (object) new JSON_SupportRankingUnitFormatter();
        case 741:
          return (object) new Json_RuneEnforceGaugeDataFormatter();
        case 742:
          return (object) new Json_UnitPieceShopItemFormatter();
        case 743:
          return (object) new ReqAutoRepeatQuestBox_ResponseFormatter();
        case 744:
          return (object) new FlowNode_ReqAutoRepeatQuestBox_MP_ReqAutoRepeatQuestBoxResponseFormatter();
        case 745:
          return (object) new ReqAutoRepeatQuestBoxAdd_ResponseFormatter();
        case 746:
          return (object) new FlowNode_ReqAutoRepeatQuestBoxAdd_MP_ReqAutoRepeatQuestBoxAddResponseFormatter();
        case 747:
          return (object) new Json_AutoRepeatQuestDataFormatter();
        case 748:
          return (object) new ReqAutoRepeatQuestProgress_ResponseFormatter();
        case 749:
          return (object) new FlowNode_ReqAutoRepeatQuestProgress_MP_AutoRepeatQuestProgressResponseFormatter();
        case 750:
          return (object) new JSON_QuestCountFormatter();
        case 751:
          return (object) new JSON_QuestProgressFormatter();
        case 752:
          return (object) new JSON_ChapterCountFormatter();
        case 753:
          return (object) new ReqAutoRepeatQuestEnd_ResponseFormatter();
        case 754:
          return (object) new FlowNode_ReqAutoRepeatQuestResult_MP_AutoRepeatQuestEndResponseFormatter();
        case 755:
          return (object) new ReqAutoRepeatQuestSetApItemPriority_ResponseFormatter();
        case 756:
          return (object) new FlowNode_ReqAutoRepeatQuestSetApItemPriority_MP_ReqAutoRepeatQuestSetApItemPriorityFormatter();
        case 757:
          return (object) new ReqAutoRepeatQuestStart_ResponseFormatter();
        case 758:
          return (object) new FlowNode_ReqAutoRepeatQuestStart_MP_AutoRepeatQuestStartResponseFormatter();
        case 759:
          return (object) new ReqCombatPowerUpdate_ResponseFormatter();
        case 760:
          return (object) new FlowNode_ReqCombatPowerUpdate_MP_ResponseFormatter();
        case 761:
          return (object) new ReqGuildRanking_ResponseFormatter();
        case 762:
          return (object) new FlowNode_ReqGuildRanking_MP_ResponseFormatter();
        case 763:
          return (object) new ReqGuildRankingMembers_ResponseFormatter();
        case 764:
          return (object) new FlowNode_ReqGuildRankingMembers_MP_ResponseFormatter();
        case 765:
          return (object) new ReqDrawCard_CardInfo_CardFormatter();
        case 766:
          return (object) new ReqDrawCard_CardInfoFormatter();
        case 767:
          return (object) new ReqDrawCard_Response_StatusFormatter();
        case 768:
          return (object) new ReqDrawCard_ResponseFormatter();
        case 769:
          return (object) new FlowNode_ReqDrawCard_MP_ResponseFormatter();
        case 770:
          return (object) new ReqDrawCardExec_ResponseFormatter();
        case 771:
          return (object) new FlowNode_ReqDrawCardExec_MP_ResponseFormatter();
        case 772:
          return (object) new ReqExpansionPurchase_ResponseFormatter();
        case 773:
          return (object) new FlowNode_ReqExpansionPurchaseData_MP_ReqExpansionPurchaseResponseFormatter();
        case 774:
          return (object) new ReqAllEquipExpAdd_ResponseFormatter();
        case 775:
          return (object) new FlowNode_AllEnhanceEquip_MP_ResponseFormatter();
        case 776:
          return (object) new FlowNode_Login_MP_PlayerDataAllFormatter();
        case 777:
          return (object) new FlowNode_PlayNew_MP_PlayNewFormatter();
        case 778:
          return (object) new ReqArtifactSet_OverWrite_ResponseFormatter();
        case 779:
          return (object) new FlowNode_ReqArtifactsSet_MP_ArtifactSet_OverWriteResponseFormatter();
        case 780:
          return (object) new FlowNode_ReqBingoProgress_JSON_BingoResponseFormatter();
        case 781:
          return (object) new FlowNode_ReqBingoProgress_MP_BingoResponseFormatter();
        case 782:
          return (object) new ReqSetConceptCard_OverWrite_ResponseFormatter();
        case 783:
          return (object) new FlowNode_ReqConceptCardSet_MP_SetConceptCard_OverWriteResponseFormatter();
        case 784:
          return (object) new ReqSetConceptLeaderSkill_OverWrite_ResponseFormatter();
        case 785:
          return (object) new FlowNode_ReqConceptLeaderSkillSet_MP_SetConceptLeaderSkill_OverWriteResponseFormatter();
        case 786:
          return (object) new JSON_FixParamFormatter();
        case 787:
          return (object) new JSON_UnitParamFormatter();
        case 788:
          return (object) new JSON_UnitJobOverwriteParamFormatter();
        case 789:
          return (object) new JSON_SkillParamFormatter();
        case 790:
          return (object) new JSON_BuffEffectParamFormatter();
        case 791:
          return (object) new JSON_CondEffectParamFormatter();
        case 792:
          return (object) new JSON_AbilityParamFormatter();
        case 793:
          return (object) new JSON_ItemParamFormatter();
        case 794:
          return (object) new JSON_ArtifactParamFormatter();
        case 795:
          return (object) new JSON_WeaponParamFormatter();
        case 796:
          return (object) new JSON_RecipeParamFormatter();
        case 797:
          return (object) new JSON_JobRankParamFormatter();
        case 798:
          return (object) new JSON_JobParamFormatter();
        case 799:
          return (object) new JSON_JobSetParamFormatter();
        case 800:
          return (object) new JSON_EvaluationParamFormatter();
        case 801:
          return (object) new JSON_AIParamFormatter();
        case 802:
          return (object) new JSON_GeoParamFormatter();
        case 803:
          return (object) new JSON_RarityParamFormatter();
        case 804:
          return (object) new JSON_ShopParamFormatter();
        case 805:
          return (object) new JSON_PlayerParamFormatter();
        case 806:
          return (object) new JSON_GrowCurveFormatter();
        case 807:
          return (object) new JSON_GrowParamFormatter();
        case 808:
          return (object) new JSON_LocalNotificationParamFormatter();
        case 809:
          return (object) new JSON_TrophyCategoryParamFormatter();
        case 810:
          return (object) new JSON_ChallengeCategoryParamFormatter();
        case 811:
          return (object) new JSON_TrophyParamFormatter();
        case 812:
          return (object) new JSON_UnlockParamFormatter();
        case 813:
          return (object) new JSON_VipParamFormatter();
        case 814:
          return (object) new JSON_ArenaWinResultFormatter();
        case 815:
          return (object) new JSON_ArenaResultFormatter();
        case 816:
          return (object) new JSON_StreamingMovieFormatter();
        case 817:
          return (object) new JSON_BannerParamFormatter();
        case 818:
          return (object) new JSON_QuestClearUnlockUnitDataParamFormatter();
        case 819:
          return (object) new JSON_AwardParamFormatter();
        case 820:
          return (object) new JSON_LoginInfoParamFormatter();
        case 821:
          return (object) new JSON_CollaboSkillParamFormatter();
        case 822:
          return (object) new JSON_TrickParamFormatter();
        case 823:
          return (object) new JSON_BreakObjParamFormatter();
        case 824:
          return (object) new JSON_VersusMatchingParamFormatter();
        case 825:
          return (object) new JSON_VersusMatchCondParamFormatter();
        case 826:
          return (object) new JSON_TowerScoreThresholdFormatter();
        case 827:
          return (object) new JSON_TowerScoreFormatter();
        case 828:
          return (object) new JSON_FriendPresentItemParamFormatter();
        case 829:
          return (object) new JSON_WeatherParamFormatter();
        case 830:
          return (object) new JSON_UnitUnlockTimeParamFormatter();
        case 831:
          return (object) new JSON_TobiraLearnAbilityParamFormatter();
        case 832:
          return (object) new JSON_TobiraParamFormatter();
        case 833:
          return (object) new JSON_TobiraCategoriesParamFormatter();
        case 834:
          return (object) new JSON_TobiraConditionParamFormatter();
        case 835:
          return (object) new JSON_TobiraCondsParamFormatter();
        case 836:
          return (object) new JSON_TobiraCondsUnitParam_JobCondFormatter();
        case 837:
          return (object) new JSON_TobiraCondsUnitParamFormatter();
        case 838:
          return (object) new JSON_TobiraRecipeMaterialParamFormatter();
        case 839:
          return (object) new JSON_TobiraRecipeParamFormatter();
        case 840:
          return (object) new JSON_ConceptCardEquipParamFormatter();
        case 841:
          return (object) new JSON_ConceptCardParamFormatter();
        case 842:
          return (object) new JSON_ConceptCardConditionsParamFormatter();
        case 843:
          return (object) new JSON_ConceptCardTrustRewardItemParamFormatter();
        case 844:
          return (object) new JSON_ConceptCardTrustRewardParamFormatter();
        case 845:
          return (object) new JSON_ConceptCardLsBuffCoefParamFormatter();
        case 846:
          return (object) new JSON_ConceptCardGroupFormatter();
        case 847:
          return (object) new JSON_ConceptLimitUpItemFormatter();
        case 848:
          return (object) new JSON_UnitGroupParamFormatter();
        case 849:
          return (object) new JSON_JobGroupParamFormatter();
        case 850:
          return (object) new JSON_StatusCoefficientParamFormatter();
        case 851:
          return (object) new JSON_CustomTargetParamFormatter();
        case 852:
          return (object) new JSON_SkillAbilityDeriveParamFormatter();
        case 853:
          return (object) new JSON_RaidPeriodParamFormatter();
        case 854:
          return (object) new JSON_RaidPeriodTimeScheduleParamFormatter();
        case 855:
          return (object) new JSON_RaidPeriodTimeParamFormatter();
        case 856:
          return (object) new JSON_RaidAreaParamFormatter();
        case 857:
          return (object) new JSON_RaidBossParamFormatter();
        case 858:
          return (object) new JSON_RaidBattleRewardWeightParamFormatter();
        case 859:
          return (object) new JSON_RaidBattleRewardParamFormatter();
        case 860:
          return (object) new JSON_RaidBeatRewardDataParamFormatter();
        case 861:
          return (object) new JSON_RaidBeatRewardParamFormatter();
        case 862:
          return (object) new JSON_RaidDamageRatioRewardRatioParamFormatter();
        case 863:
          return (object) new JSON_RaidDamageRatioRewardParamFormatter();
        case 864:
          return (object) new JSON_RaidDamageAmountRewardAmountParamFormatter();
        case 865:
          return (object) new JSON_RaidDamageAmountRewardParamFormatter();
        case 866:
          return (object) new JSON_RaidAreaClearRewardDataParamFormatter();
        case 867:
          return (object) new JSON_RaidAreaClearRewardParamFormatter();
        case 868:
          return (object) new JSON_RaidCompleteRewardDataParamFormatter();
        case 869:
          return (object) new JSON_RaidCompleteRewardParamFormatter();
        case 870:
          return (object) new JSON_RaidRewardFormatter();
        case 871:
          return (object) new JSON_RaidRewardParamFormatter();
        case 872:
          return (object) new JSON_TipsParamFormatter();
        case 873:
          return (object) new JSON_GuildEmblemParamFormatter();
        case 874:
          return (object) new JSON_GuildFacilityEffectParamFormatter();
        case 875:
          return (object) new JSON_GuildFacilityParamFormatter();
        case 876:
          return (object) new JSON_GuildFacilityLvParamFormatter();
        case 877:
          return (object) new JSON_ConvertUnitPieceExcludeParamFormatter();
        case 878:
          return (object) new JSON_PremiumParamFormatter();
        case 879:
          return (object) new JSON_BuyCoinShopParamFormatter();
        case 880:
          return (object) new JSON_BuyCoinProductParamFormatter();
        case 881:
          return (object) new JSON_BuyCoinRewardItemParamFormatter();
        case 882:
          return (object) new JSON_BuyCoinRewardParamFormatter();
        case 883:
          return (object) new JSON_BuyCoinProductConvertParamFormatter();
        case 884:
          return (object) new JSON_DynamicTransformUnitParamFormatter();
        case 885:
          return (object) new JSON_RecommendedArtifactParamFormatter();
        case 886:
          return (object) new JSON_SkillMotionDataParamFormatter();
        case 887:
          return (object) new JSON_SkillMotionParamFormatter();
        case 888:
          return (object) new JSON_DependStateSpcEffParamFormatter();
        case 889:
          return (object) new JSON_InspSkillDerivationFormatter();
        case 890:
          return (object) new JSON_InspSkillParamFormatter();
        case 891:
          return (object) new JSON_InspSkillTriggerParam_JSON_TriggerDataFormatter();
        case 892:
          return (object) new JSON_InspSkillTriggerParamFormatter();
        case 893:
          return (object) new JSON_InspSkillCostParamFormatter();
        case 894:
          return (object) new JSON_InspSkillLvUpCostParam_JSON_CostDataFormatter();
        case 895:
          return (object) new JSON_InspSkillLvUpCostParamFormatter();
        case 896:
          return (object) new JSON_HighlightResourceFormatter();
        case 897:
          return (object) new JSON_HighlightParamFormatter();
        case 898:
          return (object) new JSON_HighlightGiftDataFormatter();
        case 899:
          return (object) new JSON_HighlightGiftFormatter();
        case 900:
          return (object) new JSON_GenesisParamFormatter();
        case 901:
          return (object) new JSON_CoinBuyUseBonusParamFormatter();
        case 902:
          return (object) new JSON_CoinBuyUseBonusContentParamFormatter();
        case 903:
          return (object) new JSON_CoinBuyUseBonusRewardSetParamFormatter();
        case 904:
          return (object) new JSON_CoinBuyUseBonusItemParamFormatter();
        case 905:
          return (object) new JSON_CoinBuyUseBonusRewardParamFormatter();
        case 906:
          return (object) new JSON_UnitRentalNotificationDataParamFormatter();
        case 907:
          return (object) new JSON_UnitRentalNotificationParamFormatter();
        case 908:
          return (object) new JSON_UnitRentalParam_QuestInfoFormatter();
        case 909:
          return (object) new JSON_UnitRentalParamFormatter();
        case 910:
          return (object) new JSON_DrawCardRewardParam_DataFormatter();
        case 911:
          return (object) new JSON_DrawCardRewardParamFormatter();
        case 912:
          return (object) new JSON_DrawCardParam_DrawInfoFormatter();
        case 913:
          return (object) new JSON_DrawCardParamFormatter();
        case 914:
          return (object) new JSON_TrophyStarMissionRewardParam_DataFormatter();
        case 915:
          return (object) new JSON_TrophyStarMissionRewardParamFormatter();
        case 916:
          return (object) new JSON_TrophyStarMissionParam_StarSetParamFormatter();
        case 917:
          return (object) new JSON_TrophyStarMissionParamFormatter();
        case 918:
          return (object) new JSON_UnitPieceShopParamFormatter();
        case 919:
          return (object) new JSON_UnitPieceShopGroupCostFormatter();
        case 920:
          return (object) new JSON_UnitPieceShopGroupParamFormatter();
        case 921:
          return (object) new JSON_TwitterMessageDetailParamFormatter();
        case 922:
          return (object) new JSON_TwitterMessageParamFormatter();
        case 923:
          return (object) new JSON_FilterConceptCardConditionParamFormatter();
        case 924:
          return (object) new JSON_FilterConceptCardParamFormatter();
        case 925:
          return (object) new JSON_FilterRuneConditionParamFormatter();
        case 926:
          return (object) new JSON_FilterRuneParamFormatter();
        case 927:
          return (object) new JSON_FilterUnitConditionParamFormatter();
        case 928:
          return (object) new JSON_FilterUnitParamFormatter();
        case 929:
          return (object) new JSON_FilterArtifactParam_ConditionFormatter();
        case 930:
          return (object) new JSON_FilterArtifactParamFormatter();
        case 931:
          return (object) new JSON_SortRuneConditionParamFormatter();
        case 932:
          return (object) new JSON_SortRuneParamFormatter();
        case 933:
          return (object) new JSON_RuneParamFormatter();
        case 934:
          return (object) new JSON_RuneLotteryFormatter();
        case 935:
          return (object) new JSON_RuneLotteryBaseStateFormatter();
        case 936:
          return (object) new JSON_RuneLotteryEvoStateFormatter();
        case 937:
          return (object) new JSON_RuneDisassemblyFormatter();
        case 938:
          return (object) new JSON_RuneMaterialFormatter();
        case 939:
          return (object) new JSON_RuneCostFormatter();
        case 940:
          return (object) new JSON_RuneSetEffStateFormatter();
        case 941:
          return (object) new JSON_RuneSetEffFormatter();
        case 942:
          return (object) new JSON_JukeBoxParamFormatter();
        case 943:
          return (object) new JSON_JukeBoxSectionParamFormatter();
        case 944:
          return (object) new JSON_UnitSameGroupParamFormatter();
        case 945:
          return (object) new JSON_AutoRepeatQuestBoxParamFormatter();
        case 946:
          return (object) new JSON_GuildAttendRewardDetailFormatter();
        case 947:
          return (object) new JSON_GuildAttendParamFormatter();
        case 948:
          return (object) new JSON_GuildAttendRewardFormatter();
        case 949:
          return (object) new JSON_GuildAttendRewardParamFormatter();
        case 950:
          return (object) new JSON_GuildRoleBonusDetailFormatter();
        case 951:
          return (object) new JSON_GuildRoleBonusFormatter();
        case 952:
          return (object) new JSON_GUildRoleBonusRewardFormatter();
        case 953:
          return (object) new JSON_GuildRoleBonusRewardParamFormatter();
        case 954:
          return (object) new JSON_ResetCostInfoParamFormatter();
        case 955:
          return (object) new JSON_ResetCostParamFormatter();
        case 956:
          return (object) new JSON_ProtectSkillParamFormatter();
        case 957:
          return (object) new JSON_GuildSearchFilterConditionParamFormatter();
        case 958:
          return (object) new JSON_GuildSearchFilterParamFormatter();
        case 959:
          return (object) new JSON_ReplacePeriodFormatter();
        case 960:
          return (object) new JSON_ReplaceSpriteFormatter();
        case 961:
          return (object) new JSON_ExpansionPurchaseParamFormatter();
        case 962:
          return (object) new JSON_ExpansionPurchaseQuestParamFormatter();
        case 963:
          return (object) new JSON_SkillAdditionalParamFormatter();
        case 964:
          return (object) new JSON_SkillAntiShieldParamFormatter();
        case 965:
          return (object) new JSON_InitPlayerFormatter();
        case 966:
          return (object) new JSON_InitUnitFormatter();
        case 967:
          return (object) new JSON_InitItemFormatter();
        case 968:
          return (object) new JSON_MasterParamFormatter();
        case 969:
          return (object) new FlowNode_ReqMasterParam_MP_MasterParamFormatter();
        case 970:
          return (object) new ReqOverWriteParty_ResponseFormatter();
        case 971:
          return (object) new FlowNode_ReqOverWriteParty_MP_OverWritePartyResponseFormatter();
        case 972:
          return (object) new JSON_SectionParamFormatter();
        case 973:
          return (object) new JSON_ArchiveItemsParamFormatter();
        case 974:
          return (object) new JSON_ArchiveParamFormatter();
        case 975:
          return (object) new JSON_ChapterParamFormatter();
        case 976:
          return (object) new JSON_MapParamFormatter();
        case 977:
          return (object) new JSON_QuestParamFormatter();
        case 978:
          return (object) new JSON_InnerObjectiveFormatter();
        case 979:
          return (object) new JSON_ObjectiveParamFormatter();
        case 980:
          return (object) new JSON_MagnificationParamFormatter();
        case 981:
          return (object) new JSON_QuestCondParamFormatter();
        case 982:
          return (object) new JSON_QuestPartyParamFormatter();
        case 983:
          return (object) new JSON_QuestCampaignParentParamFormatter();
        case 984:
          return (object) new JSON_QuestCampaignChildParamFormatter();
        case 985:
          return (object) new JSON_QuestCampaignTrustFormatter();
        case 986:
          return (object) new JSON_QuestCampaignInspSkillFormatter();
        case 987:
          return (object) new JSON_TowerFloorParamFormatter();
        case 988:
          return (object) new JSON_TowerRewardItemFormatter();
        case 989:
          return (object) new JSON_TowerRewardParamFormatter();
        case 990:
          return (object) new JSON_TowerRoundRewardItemFormatter();
        case 991:
          return (object) new JSON_TowerRoundRewardParamFormatter();
        case 992:
          return (object) new JSON_TowerParamFormatter();
        case 993:
          return (object) new JSON_VersusTowerParamFormatter();
        case 994:
          return (object) new JSON_VersusScheduleFormatter();
        case 995:
          return (object) new JSON_VersusCoinFormatter();
        case 996:
          return (object) new JSON_MultiTowerFloorParamFormatter();
        case 997:
          return (object) new JSON_MultiTowerRewardItemFormatter();
        case 998:
          return (object) new JSON_MultiTowerRewardParamFormatter();
        case 999:
          return (object) new JSON_MapEffectParamFormatter();
        case 1000:
          return (object) new JSON_WeatherSetParamFormatter();
        case 1001:
          return (object) new JSON_RankingQuestParamFormatter();
        case 1002:
          return (object) new JSON_RankingQuestScheduleParamFormatter();
        case 1003:
          return (object) new JSON_RankingQuestRewardParamFormatter();
        case 1004:
          return (object) new JSON_VersusWinBonusRewardFormatter();
        case 1005:
          return (object) new JSON_VersusFirstWinBonusFormatter();
        case 1006:
          return (object) new JSON_VersusStreakWinScheduleFormatter();
        case 1007:
          return (object) new JSON_VersusStreakWinBonusFormatter();
        case 1008:
          return (object) new JSON_VersusRuleFormatter();
        case 1009:
          return (object) new JSON_VersusCoinCampParamFormatter();
        case 1010:
          return (object) new JSON_VersusEnableTimeScheduleParamFormatter();
        case 1011:
          return (object) new JSON_VersusEnableTimeParamFormatter();
        case 1012:
          return (object) new JSON_VersusRankParamFormatter();
        case 1013:
          return (object) new JSON_VersusRankClassParamFormatter();
        case 1014:
          return (object) new JSON_VersusRankRankingRewardParamFormatter();
        case 1015:
          return (object) new JSON_VersusRankRewardRewardParamFormatter();
        case 1016:
          return (object) new JSON_VersusRankRewardParamFormatter();
        case 1017:
          return (object) new JSON_VersusRankMissionScheduleParamFormatter();
        case 1018:
          return (object) new JSON_VersusRankMissionParamFormatter();
        case 1019:
          return (object) new JSON_QuestLobbyNewsParamFormatter();
        case 1020:
          return (object) new JSON_GuerrillaShopAdventQuestParamFormatter();
        case 1021:
          return (object) new JSON_GuerrillaShopScheduleAdventParamFormatter();
        case 1022:
          return (object) new JSON_GuerrillaShopScheduleParamFormatter();
        case 1023:
          return (object) new JSON_VersusDraftDeckParamFormatter();
        case 1024:
          return (object) new JSON_VersusDraftUnitParamFormatter();
        case 1025:
          return (object) new JSON_GenesisStarRewardParamFormatter();
        case 1026:
          return (object) new JSON_GenesisStarParamFormatter();
        case 1027:
          return (object) new JSON_GenesisChapterModeInfoParamFormatter();
        case 1028:
          return (object) new JSON_GenesisChapterParamFormatter();
        case 1029:
          return (object) new JSON_GenesisRewardDataParamFormatter();
        case 1030:
          return (object) new JSON_GenesisRewardParamFormatter();
        case 1031:
          return (object) new JSON_GenesisLapBossParam_LapInfoFormatter();
        case 1032:
          return (object) new JSON_GenesisLapBossParamFormatter();
        case 1033:
          return (object) new JSON_AdvanceStarRewardParamFormatter();
        case 1034:
          return (object) new JSON_AdvanceStarParamFormatter();
        case 1035:
          return (object) new JSON_AdvanceEventModeInfoParamFormatter();
        case 1036:
          return (object) new JSON_AdvanceEventParamFormatter();
        case 1037:
          return (object) new JSON_AdvanceRewardDataParamFormatter();
        case 1038:
          return (object) new JSON_AdvanceRewardParamFormatter();
        case 1039:
          return (object) new JSON_AdvanceLapBossParam_LapInfoFormatter();
        case 1040:
          return (object) new JSON_AdvanceLapBossParamFormatter();
        case 1041:
          return (object) new JSON_GuildRaidBossParamFormatter();
        case 1042:
          return (object) new JSON_GuildRaidCoolDaysParamFormatter();
        case 1043:
          return (object) new JSON_GuildRaidScoreDataParamFormatter();
        case 1044:
          return (object) new JSON_GuildRaidScoreParamFormatter();
        case 1045:
          return (object) new JSON_GuildRaidPeriodTimeFormatter();
        case 1046:
          return (object) new JSON_GuildRaidPeriodParamFormatter();
        case 1047:
          return (object) new JSON_GuildRaidRewardFormatter();
        case 1048:
          return (object) new JSON_GuildRaidRewardParamFormatter();
        case 1049:
          return (object) new JSON_GuildRaidRewardDmgRankingRankParamFormatter();
        case 1050:
          return (object) new JSON_GuildRaidRewardDmgRankingParamFormatter();
        case 1051:
          return (object) new JSON_GuildRaidRewardDmgRatioFormatter();
        case 1052:
          return (object) new JSON_GuildRaidRewardDmgRatioParamFormatter();
        case 1053:
          return (object) new JSON_GuildRaidRewardRoundFormatter();
        case 1054:
          return (object) new JSON_GuildRaidRewardRoundParamFormatter();
        case 1055:
          return (object) new JSON_GuildRaidRewardRankingDataParamFormatter();
        case 1056:
          return (object) new JSON_GuildRaidRewardRankingParamFormatter();
        case 1057:
          return (object) new JSON_GvGPeriodParamFormatter();
        case 1058:
          return (object) new JSON_GvGNodeParamFormatter();
        case 1059:
          return (object) new JSON_GvGNPCPartyDetailParamFormatter();
        case 1060:
          return (object) new JSON_GvGNPCPartyParamFormatter();
        case 1061:
          return (object) new JSON_GvGNPCUnitParamFormatter();
        case 1062:
          return (object) new JSON_GvGRewardRankingDetailParamFormatter();
        case 1063:
          return (object) new JSON_GvGRewardRankingParamFormatter();
        case 1064:
          return (object) new JSON_GvGRewardDetailParamFormatter();
        case 1065:
          return (object) new JSON_GvGRewardParamFormatter();
        case 1066:
          return (object) new JSON_GvGRuleParamFormatter();
        case 1067:
          return (object) new JSON_GvGNodeRewardFormatter();
        case 1068:
          return (object) new JSON_GvGNodeRewardParamFormatter();
        case 1069:
          return (object) new JSON_GvGLeagueParamFormatter();
        case 1070:
          return (object) new JSON_GvGCalcRateSettingParamFormatter();
        case 1071:
          return (object) new JSON_WorldRaidParam_BossInfoFormatter();
        case 1072:
          return (object) new JSON_WorldRaidParamFormatter();
        case 1073:
          return (object) new JSON_WorldRaidBossParamFormatter();
        case 1074:
          return (object) new JSON_WorldRaidRewardParam_RewardFormatter();
        case 1075:
          return (object) new JSON_WorldRaidRewardParamFormatter();
        case 1076:
          return (object) new JSON_WorldRaidDamageRewardParam_RewardFormatter();
        case 1077:
          return (object) new JSON_WorldRaidDamageRewardParamFormatter();
        case 1078:
          return (object) new JSON_WorldRaidDamageLotteryParam_RewardFormatter();
        case 1079:
          return (object) new JSON_WorldRaidDamageLotteryParamFormatter();
        case 1080:
          return (object) new JSON_WorldRaidRankingRewardParam_RewardFormatter();
        case 1081:
          return (object) new JSON_WorldRaidRankingRewardParamFormatter();
        case 1082:
          return (object) new Json_QuestListFormatter();
        case 1083:
          return (object) new FlowNode_ReqQuestParam_MP_QuestParamFormatter();
        case 1084:
          return (object) new ReqSetSupportRanking_ResponseFormatter();
        case 1085:
          return (object) new FlowNode_ReqSupportRanking_MP_ResponseFormatter();
        case 1086:
          return (object) new ReqSetSupportUsed_ResponseFormatter();
        case 1087:
          return (object) new FlowNode_ReqSupportUsed_MP_ResponseFormatter();
        case 1088:
          return (object) new ReqUnitPieceShopItemList_ResponseFormatter();
        case 1089:
          return (object) new FlowNode_RequestUnitPieceShopItems_MP_ResponseFormatter();
        case 1090:
          return (object) new FlowNode_ReqUpdateBingo_MP_UpdateBingoResponseFormatter();
        case 1091:
          return (object) new FlowNode_ReqUpdateTrophy_MP_TrophyPlayerDataAllResponseFormatter();
        case 1092:
          return (object) new ReqJobAbility_OverWrite_ResponseFormatter();
        case 1093:
          return (object) new FlowNode_SetAbility_MP_JobAbilityt_OverWriteResponseFormatter();
        case 1094:
          return (object) new Json_MyPhotonPlayerBinaryParam_UnitDataElemFormatter();
        case 1095:
          return (object) new Json_MyPhotonPlayerBinaryParamFormatter();
        case 1096:
          return (object) new FlowNode_StartMultiPlay_RecvDataFormatter();
        case 1097:
          return (object) new ReqUnitPieceShopBuypaid_ResponseFormatter();
        case 1098:
          return (object) new FlowNode_UnitPieceBuyItem_MP_ResponseFormatter();
        case 1099:
          return (object) new ReqGachaPickup_ResponseFormatter();
        case 1100:
          return (object) new FlowNode_ReqGachaPickup_MP_ResponseFormatter();
        case 1101:
          return (object) new JukeBoxWindow_ResPlayListFormatter();
        case 1102:
          return (object) new ReqJukeBox_ResponseFormatter();
        case 1103:
          return (object) new FlowNode_ReqJukeBox_MP_ResponseFormatter();
        case 1104:
          return (object) new ReqJukeBoxPlaylistAdd_ResponseFormatter();
        case 1105:
          return (object) new FlowNode_ReqJukeBoxMylistAdd_MP_Add_ResponseFormatter();
        case 1106:
          return (object) new ReqJukeBoxPlaylistDel_ResponseFormatter();
        case 1107:
          return (object) new FlowNode_ReqJukeBoxMylistDel_MP_Del_ResponseFormatter();
        case 1108:
          return (object) new ReqGuildAttend_ResponseFormatter();
        case 1109:
          return (object) new FlowNode_ReqGuildAttend_MP_ResponseFormatter();
        case 1110:
          return (object) new ReqGuildRoleBonus_ResponseFormatter();
        case 1111:
          return (object) new FlowNode_ReqGuildRoleBonus_MP_ResponseFormatter();
        case 1112:
          return (object) new ReqGuildRaid_ResponseFormatter();
        case 1113:
          return (object) new FlowNode_ReqGuildRaid_MP_ResponseFormatter();
        case 1114:
          return (object) new ReqGuildRaidBtlLog_ResponseFormatter();
        case 1115:
          return (object) new FlowNode_ReqGuildRaidBtlLog_MP_ResponseFormatter();
        case 1116:
          return (object) new ReqGuildRaidInfo_ResponseFormatter();
        case 1117:
          return (object) new FlowNode_ReqGuildRaidInfo_MP_ResponseFormatter();
        case 1118:
          return (object) new ReqGuildRaidMail_ResponseFormatter();
        case 1119:
          return (object) new FlowNode_ReqGuildRaidMail_MP_ResponseFormatter();
        case 1120:
          return (object) new ReqGuildRaidMailRead_ResponseFormatter();
        case 1121:
          return (object) new FlowNode_ReqGuildRaidMailRead_MP_ResponseFormatter();
        case 1122:
          return (object) new ReqGuildRaidRanking_ResponseFormatter();
        case 1123:
          return (object) new FlowNode_ReqGuildRaidRanking_MP_ResponseFormatter();
        case 1124:
          return (object) new ReqGuildRaidRankingDamageRound_ResponseFormatter();
        case 1125:
          return (object) new FlowNode_ReqGuildRaidRankingDamageRound_MP_ResponseFormatter();
        case 1126:
          return (object) new ReqGuildRaidRankingDamageSummary_ResponseFormatter();
        case 1127:
          return (object) new FlowNode_ReqGuildRaidRankingDamageSummary_MP_ResponseFormatter();
        case 1128:
          return (object) new ReqGuildRaidRankingPort_ResponseFormatter();
        case 1129:
          return (object) new FlowNode_ReqGuildRaidRankingPort_MP_ResponseFormatter();
        case 1130:
          return (object) new ReqGuildRaidRankingPortBoss_ResponseFormatter();
        case 1131:
          return (object) new FlowNode_ReqGuildRaidRankingPortBoss_MP_ResponseFormatter();
        case 1132:
          return (object) new FlowNode_ReqGuildRaidReportDetail_MP_ResponseFormatter();
        case 1133:
          return (object) new ReqGuildRaidReportSelf_ResponseFormatter();
        case 1134:
          return (object) new FlowNode_ReqGuildRaidReportSelf_MP_ResponseFormatter();
        case 1135:
          return (object) new ReqGuildRaidRankingReward_ResponseFormatter();
        case 1136:
          return (object) new FlowNode_ReqGuildRaidReward_MP_ResponseFormatter();
        case 1137:
          return (object) new JSON_GvGNodeDataFormatter();
        case 1138:
          return (object) new JSON_GvGLeagueDataFormatter();
        case 1139:
          return (object) new JSON_GvGLeagueViewGuildFormatter();
        case 1140:
          return (object) new JSON_GvGUsedUnitDataFormatter();
        case 1141:
          return (object) new JSON_GvGResultFormatter();
        case 1142:
          return (object) new JSON_GvGLeagueResultFormatter();
        case 1143:
          return (object) new ReqGvG_ResponseFormatter();
        case 1144:
          return (object) new FlowNode_ReqGvG_MP_ResponseFormatter();
        case 1145:
          return (object) new JSON_GvGPartyUnitFormatter();
        case 1146:
          return (object) new ReqGvGBattle_ResponseFormatter();
        case 1147:
          return (object) new FlowNode_ReqGvGBattle_MP_ResponseFormatter();
        case 1148:
          return (object) new ReqGvGBattleCapture_ResponseFormatter();
        case 1149:
          return (object) new FlowNode_ReqGvGBattleCapture_MP_ResponseFormatter();
        case 1150:
          return (object) new JSON_GvGPartyNPCFormatter();
        case 1151:
          return (object) new JSON_GvGPartyFormatter();
        case 1152:
          return (object) new ReqGvGBattleEnemy_ResponseFormatter();
        case 1153:
          return (object) new FlowNode_ReqGvGBattleEnemy_MP_ResponseFormatter();
        case 1154:
          return (object) new JSON_GvGScoreRankingFormatter();
        case 1155:
          return (object) new ReqGvGBeatRanking_ResponseFormatter();
        case 1156:
          return (object) new FlowNode_ReqGvGBeatRanking_MP_ResponseFormatter();
        case 1157:
          return (object) new ReqGvGDefenseRanking_ResponseFormatter();
        case 1158:
          return (object) new FlowNode_ReqGvGDefenseRanking_MP_ResponseFormatter();
        case 1159:
          return (object) new JSON_GvGHalfTimeFormatter();
        case 1160:
          return (object) new ReqGvGHalfTime_ResponseFormatter();
        case 1161:
          return (object) new FlowNode_ReqGvGHalfTime_MP_ResponseFormatter();
        case 1162:
          return (object) new ReqGvGLeague_ResponseFormatter();
        case 1163:
          return (object) new FlowNode_ReqGvGLeague_MP_ResponseFormatter();
        case 1164:
          return (object) new ReqGvGNodeDeclare_ResponseFormatter();
        case 1165:
          return (object) new FlowNode_ReqGvGNodeDeclare_MP_ResponseFormatter();
        case 1166:
          return (object) new JSON_GvGUsedItemsFormatter();
        case 1167:
          return (object) new ReqGvGNodeDefenseEntry_ResponseFormatter();
        case 1168:
          return (object) new FlowNode_ReqGvGNodeDefenseEntry_MP_ResponseFormatter();
        case 1169:
          return (object) new ReqGvGNodeDetail_ResponseFormatter();
        case 1170:
          return (object) new FlowNode_ReqGvGNodeDetail_MP_ResponseFormatter();
        case 1171:
          return (object) new ReqGvGNodeOffenseEntry_ResponseFormatter();
        case 1172:
          return (object) new FlowNode_ReqGvGNodeOffenseEntry_MP_ResponseFormatter();
        case 1173:
          return (object) new JSON_GvGRankingDataFormatter();
        case 1174:
          return (object) new ReqGvGRankingGroup_ResponseFormatter();
        case 1175:
          return (object) new FlowNode_ReqGvGRankingGroup_MP_ResponseFormatter();
        case 1176:
          return (object) new ReqGvGReward_ResponseFormatter();
        case 1177:
          return (object) new FlowNode_ReqGvGReward_MP_ResponseFormatter();
        case 1178:
          return (object) new Json_ChatChannelMasterParam_FieldsFormatter();
        case 1179:
          return (object) new Json_ChatChannelMasterParamFormatter();
        case 1180:
          return (object) new LoginNewsInfo_JSON_PubInfoFormatter();
        case 1181:
          return (object) new FlowNode_ReqLoginPack_JSON_ReqLoginPackResponseFormatter();
        case 1182:
          return (object) new FlowNode_ReqLoginPack_MP_ReqLoginPackResponseFormatter();
        case 1183:
          return (object) new Json_Notify_MonthlyFormatter();
        case 1184:
          return (object) new ReqMonthlyRecover_ResponseFormatter();
        case 1185:
          return (object) new FlowNode_ReqRecoverMonthly_MP_ResponseFormatter();
        case 1186:
          return (object) new JSON_MultiSupportFormatter();
        case 1187:
          return (object) new ReqMultiSupportList_ResponseFormatter();
        case 1188:
          return (object) new FlowNode_MultiPlaySupport_MP_Response_GetListFormatter();
        case 1189:
          return (object) new ReqStoryExChallengeCountReset_ResponseFormatter();
        case 1190:
          return (object) new FlowNode_ReqStoryExTotalChallengeCountReset_MP_ReqStoryExChallengeCountResetResponseFormatter();
        case 1191:
          return (object) new ReqGetRune_ResponseFormatter();
        case 1192:
          return (object) new FlowNode_ReqRune_MP_ResponseFormatter();
        case 1193:
          return (object) new ReqRuneDisassembly_Response_RewardsFormatter();
        case 1194:
          return (object) new ReqRuneDisassembly_ResponseFormatter();
        case 1195:
          return (object) new FlowNode_ReqRuneDisassembly_MP_ResponseFormatter();
        case 1196:
          return (object) new ReqRuneEnhance_ResponseFormatter();
        case 1197:
          return (object) new FlowNode_ReqRuneEnhance_MP_ResponseFormatter();
        case 1198:
          return (object) new ReqRuneEquip_ResponseFormatter();
        case 1199:
          return (object) new FlowNode_ReqRuneEquip_MP_ResponseFormatter();
        case 1200:
          return (object) new ReqRuneEvo_ResponseFormatter();
        case 1201:
          return (object) new FlowNode_ReqRuneEvo_MP_ResponseFormatter();
        case 1202:
          return (object) new ReqRuneFavorite_ResponseFormatter();
        case 1203:
          return (object) new FlowNode_ReqRuneFavorite_MP_ResponseFormatter();
        case 1204:
          return (object) new ReqRuneParamEnhEvo_ResponseFormatter();
        case 1205:
          return (object) new FlowNode_ReqRuneParamEnhEvo_MP_ResponseFormatter();
        case 1206:
          return (object) new ReqRuneResetParamBase_ResponseFormatter();
        case 1207:
          return (object) new FlowNode_ReqRuneResetParamBase_MP_ResponseFormatter();
        case 1208:
          return (object) new ReqRuneResetStatusEvo_ResponseFormatter();
        case 1209:
          return (object) new FlowNode_ReqRuneResetStatusEvo_MP_ResponseFormatter();
        case 1210:
          return (object) new ReqRuneStorageAdd_ResponseFormatter();
        case 1211:
          return (object) new FlowNode_ReqRuneStorageAdd_MP_ResponseFormatter();
        case 1212:
          return (object) new ReqGuildTrophy_ResponseFormatter();
        case 1213:
          return (object) new FlowNode_ReqGuildTrophy_MP_ResponseFormatter();
        case 1214:
          return (object) new ReqGuildTrophyReward_ResponseFormatter();
        case 1215:
          return (object) new FlowNode_ReqGuildTrophyReward_MP_ResponseFormatter();
        case 1216:
          return (object) new ReqTrophyStarMission_ResponseFormatter();
        case 1217:
          return (object) new FlowNode_ReqTrophyStarMission_MP_ResponseFormatter();
        case 1218:
          return (object) new ReqTrophyStarMissionGetReward_Response_JSON_StarMissionConceptCardFormatter();
        case 1219:
          return (object) new ReqTrophyStarMissionGetReward_ResponseFormatter();
        case 1220:
          return (object) new FlowNode_ReqTrophyStarMissionGetReward_MP_ResponseFormatter();
        case 1221:
          return (object) new ReqUnitRentalAdd_ResponseFormatter();
        case 1222:
          return (object) new FlowNode_ReqUnitRentalAdd_MP_ResponseFormatter();
        case 1223:
          return (object) new ReqUnitRentalExec_ResponseFormatter();
        case 1224:
          return (object) new FlowNode_ReqUnitRentalExec_MP_ResponseFormatter();
        case 1225:
          return (object) new ReqUnitRentalLeave_ResponseFormatter();
        case 1226:
          return (object) new FlowNode_ReqUnitRentalLeave_MP_ResponseFormatter();
        case 1227:
          return (object) new JSON_WorldRaidBossChallengedDataFormatter();
        case 1228:
          return (object) new JSON_WorldRaidLogDataFormatter();
        case 1229:
          return (object) new ReqWorldRaid_ResponseFormatter();
        case 1230:
          return (object) new FlowNode_ReqWorldRaid_MP_ResponseFormatter();
        case 1231:
          return (object) new JSON_WorldRaidBossDetailDataFormatter();
        case 1232:
          return (object) new ReqWorldRaidBoss_ResponseFormatter();
        case 1233:
          return (object) new FlowNode_ReqWorldRaidBoss_MP_ResponseFormatter();
        case 1234:
          return (object) new JSON_WorldRaidRankingDataFormatter();
        case 1235:
          return (object) new ReqWorldRaidRanking_ResponseFormatter();
        case 1236:
          return (object) new FlowNode_ReqWorldRaidRanking_MP_ResponseFormatter();
        case 1237:
          return (object) new ReqWorldRaidReward_ResponseFormatter();
        case 1238:
          return (object) new FlowNode_ReqWorldRaidReward_MP_ResponseFormatter();
        case 1239:
          return (object) new EmbeddedTutorialMasterParams_JSON_EmbededMasterParamFormatter();
        case 1240:
          return (object) new EmbeddedTutorialMasterParams_JSON_EmbededQuestParamFormatter();
        case 1241:
          return (object) new JukeBoxParamFormatter();
        case 1242:
          return (object) new JukeBoxSectionParamFormatter();
        case 1243:
          return (object) new GuildEmblemParamFormatter();
        case 1244:
          return (object) new JSON_ProductSaleInfoFormatter();
        case 1245:
          return (object) new JSON_ProductParamFormatter();
        case 1246:
          return (object) new JSON_ProductBuyCoinParamFormatter();
        case 1247:
          return (object) new JSON_ProductParamResponseFormatter();
        case 1248:
          return (object) new RaidPeriodTimeScheduleParamFormatter();
        case 1249:
          return (object) new RuneSetEffStateFormatter();
        case 1250:
          return (object) new RuneSetEffFormatter();
        case 1251:
          return (object) new RuneSlotIndexFormatter();
        case 1252:
          return (object) new RuneParamFormatter();
        case 1253:
          return (object) new JSON_RuneLotteryStateFormatter();
        case 1254:
          return (object) new RuneLotteryStateFormatter();
        case 1255:
          return (object) new RuneCostFormatter();
        case 1256:
          return (object) new RuneDisassembly_MaterialsFormatter();
        case 1257:
          return (object) new RuneDisassemblyFormatter();
        case 1258:
          return (object) new RuneMaterialFormatter();
        case 1259:
          return (object) new JSON_TrophyResponseFormatter();
        case 1260:
          return (object) new MP_TrophyResponseFormatter();
        case 1261:
          return (object) new AbilitySlots_MP_JobAbilityt_OverWriteResponseFormatter();
        case 1262:
          return (object) new ArtifactSlots_MP_ArtifactSet_OverWriteResponseFormatter();
        case 1263:
          return (object) new JSON_GvGBattleEndParamFormatter();
        case 1264:
          return (object) new JSON_GvGLeagueGuildFormatter();
        case 1265:
          return (object) new ReplacePeriodFormatter();
        case 1266:
          return (object) new ReplaceSpriteFormatter();
        case 1267:
          return (object) new VersusDraftList_VersusDraftMessageDataFormatter();
        case 1268:
          return (object) new ReqSetConceptCardList_ResponseFormatter();
        case 1269:
          return (object) new PartyWindow2_MP_Response_SetConceptCardListFormatter();
        case 1270:
          return (object) new ReqUnitJob_OverWrite_ResponseFormatter();
        case 1271:
          return (object) new UnitJobDropdown_MP_UnitJob_OverWriteResponseFormatter();
        case 1272:
          return (object) new JSON_WorldRaidBossDataFormatter();
        case 1273:
          return (object) new JSON_WorldRaidBossReqDataFormatter();
        case 1274:
          return (object) new JSON_WorldRaidBattleRewardDataFormatter();
        case 1275:
          return (object) new WebAPI_JSON_BaseResponseFormatter();
        case 1276:
          return (object) new ReqMultiSupportSet_ResponseFormatter();
        case 1277:
          return (object) new ReqGuildAttend_RequestParamFormatter();
        case 1278:
          return (object) new ReqGuildRoleBonus_RequestParamFormatter();
        case 1279:
          return (object) new ReqGvGBattleExec_ResponseFormatter();
        case 1280:
          return (object) new ReqGetRune_RequestParamFormatter();
        case 1281:
          return (object) new ReqRuneEquip_RequestParamFormatter();
        case 1282:
          return (object) new ReqRuneEnhance_RequestParamFormatter();
        case 1283:
          return (object) new ReqRuneEvo_RequestParamFormatter();
        case 1284:
          return (object) new ReqRuneDisassembly_RequestParamFormatter();
        case 1285:
          return (object) new ReqRuneResetParamBase_RequestParamFormatter();
        case 1286:
          return (object) new ReqRuneResetStatusEvo_RequestParamFormatter();
        case 1287:
          return (object) new ReqRuneParamEnhEvo_RequestParamFormatter();
        case 1288:
          return (object) new ReqRuneFavorite_RequestParamFormatter();
        default:
          return (object) null;
      }
    }
  }
}
