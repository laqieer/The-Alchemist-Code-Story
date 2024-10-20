﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JSON_MasterParam
  {
    public JSON_FixParam[] Fix;
    public JSON_UnitParam[] Unit;
    public JSON_UnitJobOverwriteParam[] UnitJobOverwrite;
    public JSON_SkillParam[] Skill;
    public JSON_BuffEffectParam[] Buff;
    public JSON_CondEffectParam[] Cond;
    public JSON_AbilityParam[] Ability;
    public JSON_ItemParam[] Item;
    public JSON_ArtifactParam[] Artifact;
    public JSON_WeaponParam[] Weapon;
    public JSON_RecipeParam[] Recipe;
    public JSON_JobParam[] Job;
    public JSON_JobSetParam[] JobSet;
    public JSON_EvaluationParam[] Evaluation;
    public JSON_AIParam[] AI;
    public JSON_GeoParam[] Geo;
    public JSON_RarityParam[] Rarity;
    public JSON_ShopParam[] Shop;
    public int[] AbilityRank;
    public int[] UnitLvTbl;
    public int[] PlayerLvTbl;
    public int[] ArtifactLvTbl;
    public int[] AwakePieceTbl;
    public JSON_PlayerParam[] Player;
    public JSON_GrowParam[] Grow;
    public JSON_LocalNotificationParam[] LocalNotification;
    public JSON_TrophyCategoryParam[] TrophyCategory;
    public JSON_TrophyCategoryParam[] GuildTrophyCategory;
    public JSON_ChallengeCategoryParam[] ChallengeCategory;
    public JSON_TrophyParam[] Trophy;
    public JSON_TrophyParam[] Challenge;
    public JSON_TrophyParam[] GuildTrophy;
    public JSON_UnlockParam[] Unlock;
    public JSON_VipParam[] Vip;
    public JSON_ArenaWinResult[] ArenaWinResult;
    public JSON_ArenaResult[] ArenaRankResult;
    public JSON_StreamingMovie[] Mov;
    public JSON_BannerParam[] Banner;
    public JSON_QuestClearUnlockUnitDataParam[] QuestClearUnlockUnitData;
    public JSON_AwardParam[] Award;
    public JSON_LoginInfoParam[] LoginInfo;
    public JSON_CollaboSkillParam[] CollaboSkill;
    public JSON_TrickParam[] Trick;
    public JSON_BreakObjParam[] BreakObj;
    public JSON_VersusMatchingParam[] VersusMatchKey;
    public JSON_VersusMatchCondParam[] VersusMatchCond;
    public JSON_TowerScore[] TowerScore;
    public int[] TowerRank;
    public int[] MultilimitUnitLv;
    public JSON_FriendPresentItemParam[] FriendPresentItem;
    public JSON_WeatherParam[] Weather;
    public JSON_UnitUnlockTimeParam[] UnitUnlockTime;
    public JSON_TobiraParam[] Tobira;
    public JSON_TobiraCategoriesParam[] TobiraCategories;
    public JSON_TobiraCondsParam[] TobiraConds;
    public JSON_TobiraCondsUnitParam[] TobiraCondsUnit;
    public JSON_TobiraRecipeParam[] TobiraRecipe;
    public JSON_ConceptCardParam[] ConceptCard;
    public int[] ConceptCardLvTbl1;
    public int[] ConceptCardLvTbl2;
    public int[] ConceptCardLvTbl3;
    public int[] ConceptCardLvTbl4;
    public int[] ConceptCardLvTbl5;
    public int[] ConceptCardLvTbl6;
    public JSON_ConceptCardConditionsParam[] ConceptCardConditions;
    public JSON_ConceptCardTrustRewardParam[] ConceptCardTrustReward;
    public int[] ConceptCardSellCoinRate;
    public JSON_ConceptCardLsBuffCoefParam[] ConceptCardLsBuffCoef;
    public JSON_ConceptCardGroup[] ConceptCardGroup;
    public JSON_ConceptLimitUpItem[] ConceptLimitUpItem;
    public JSON_UnitGroupParam[] UnitGroup;
    public JSON_JobGroupParam[] JobGroup;
    public JSON_StatusCoefficientParam[] StatusCoefficient;
    public JSON_CustomTargetParam[] CustomTarget;
    public JSON_SkillAbilityDeriveParam[] SkillAbilityDerive;
    public JSON_RaidPeriodParam[] RaidPeriod;
    public JSON_RaidPeriodTimeParam[] RaidPeriodTime;
    public JSON_RaidAreaParam[] RaidArea;
    public JSON_RaidBossParam[] RaidBoss;
    public JSON_RaidBattleRewardParam[] RaidBattleReward;
    public JSON_RaidBeatRewardParam[] RaidBeatReward;
    public JSON_RaidDamageRatioRewardParam[] RaidDamageRatioReward;
    public JSON_RaidDamageAmountRewardParam[] RaidDamageAmountReward;
    public JSON_RaidAreaClearRewardParam[] RaidAreaClearReward;
    public JSON_RaidCompleteRewardParam[] RaidCompleteReward;
    public JSON_RaidRewardParam[] RaidReward;
    public JSON_TipsParam[] Tips;
    public JSON_GuildEmblemParam[] GuildEmblem;
    public JSON_GuildFacilityParam[] GuildFacility;
    public JSON_GuildFacilityLvParam[] GuildFacilityLvTbl;
    public JSON_ConvertUnitPieceExcludeParam[] ConvertUnitPieceExclude;
    public JSON_PremiumParam[] Premium;
    public JSON_BuyCoinShopParam[] BuyCoinShop;
    public JSON_BuyCoinProductParam[] BuyCoinProduct;
    public JSON_BuyCoinRewardParam[] BuyCoinReward;
    public JSON_BuyCoinProductConvertParam[] BuyCoinProductConvert;
    public JSON_DynamicTransformUnitParam[] DynamicTransformUnit;
    public JSON_RecommendedArtifactParam[] RecommendedArtifact;
    public JSON_SkillMotionParam[] SkillMotion;
    public JSON_DependStateSpcEffParam[] DependStateSpcEff;
    public JSON_InspSkillParam[] InspirationSkill;
    public JSON_InspSkillTriggerParam[] InspSkillTrigger;
    public JSON_InspSkillCostParam[] InspSkillOpenCost;
    public JSON_InspSkillCostParam[] InspSkillResetCost;
    public JSON_InspSkillLvUpCostParam[] InspSkillLvUpCost;
    public JSON_HighlightParam[] Highlight;
    public JSON_HighlightGift[] HighlightGift;
    public JSON_GenesisParam[] Genesis;
    public JSON_CoinBuyUseBonusParam[] CoinBuyUseBonus;
    public JSON_CoinBuyUseBonusRewardSetParam[] CoinBuyUseBonusRewardSet;
    public JSON_CoinBuyUseBonusRewardParam[] CoinBuyUseBonusReward;
    public JSON_UnitRentalNotificationParam[] UnitRentalNotification;
    public JSON_UnitRentalParam[] UnitRental;
    public JSON_DrawCardRewardParam[] DrawCardReward;
    public JSON_DrawCardParam[] DrawCard;
    public JSON_TrophyStarMissionRewardParam[] TrophyStarMissionReward;
    public JSON_TrophyStarMissionParam[] TrophyStarMission;
    public JSON_UnitPieceShopParam[] UnitPieceShop;
    public JSON_UnitPieceShopGroupParam[] UnitPieceShopGroup;
    public JSON_TwitterMessageParam[] TwitterMessage;
    public JSON_FilterConceptCardParam[] FilterConceptCard;
    public JSON_FilterRuneParam[] FilterRune;
    public JSON_FilterUnitParam[] FilterUnit;
    public JSON_FilterArtifactParam[] FilterArtifact;
    public JSON_SortRuneParam[] SortRune;
    public JSON_RuneParam[] Rune;
    public JSON_RuneLotteryBaseState[] RuneLotteryBaseState;
    public JSON_RuneLotteryEvoState[] RuneLotteryEvoState;
    public JSON_RuneMaterial[] RuneMaterial;
    public JSON_RuneCost[] RuneCost;
    public JSON_RuneSetEff[] RuneSetEff;
    public JSON_JukeBoxParam[] JukeBox;
    public JSON_JukeBoxSectionParam[] JukeBoxSection;
    public JSON_UnitSameGroupParam[] UnitSameGroup;
    public JSON_AutoRepeatQuestBoxParam[] AutoRepeatQuestBox;
    public JSON_GuildAttendParam[] GuildAttend;
    public JSON_GuildAttendRewardParam[] GuildAttendReward;
    public JSON_GuildRoleBonus[] GuildRoleBonus;
    public JSON_GuildRoleBonusRewardParam[] GuildRoleBonusReward;
    public JSON_ResetCostParam[] ResetCost;
    public JSON_ProtectSkillParam[] ProtectSkill;
    public JSON_GuildSearchFilterParam[] GuildSearchFilter;
    public JSON_ReplaceSprite[] ReplaceSprite;
    public JSON_ExpansionPurchaseParam[] ExpansionPurchase;
    public JSON_ExpansionPurchaseQuestParam[] ExpansionPurchaseQuest;
    public JSON_SkillAdditionalParam[] SkillAdditional;
    public JSON_SkillAntiShieldParam[] SkillAntiShield;
    public JSON_InitPlayer[] InitPlayer;
    public JSON_InitUnit[] InitUnit;
    public JSON_InitItem[] InitItem;
  }
}