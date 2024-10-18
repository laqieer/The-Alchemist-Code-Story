// Decompiled with JetBrains decompiler
// Type: SRPG.MasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MasterParam
  {
    private FixParam mFixParam = new FixParam();
    private List<UnitParam> mUnitParam;
    private List<UnitJobOverwriteParam> mUnitJobOverwriteParam;
    private List<SkillParam> mSkillParam;
    private List<AbilityParam> mAbilityParam;
    private List<ItemParam> mItemParam;
    private List<ArtifactParam> mArtifactParam;
    private List<WeaponParam> mWeaponParam;
    private List<RecipeParam> mRecipeParam;
    private List<JobParam> mJobParam;
    private Dictionary<string, JobParam> mJobParamDict = new Dictionary<string, JobParam>();
    private List<QuestClearUnlockUnitDataParam> mUnlockUnitDataParam;
    private List<CollaboSkillParam> mCollaboSkillParam;
    private List<TrickParam> mTrickParam;
    private List<BreakObjParam> mBreakObjParam;
    private List<WeatherParam> mWeatherParam;
    private Dictionary<string, UnitUnlockTimeParam> mUnitUnlockTimeParam;
    private List<TobiraParam> mTobiraParam = new List<TobiraParam>();
    private Dictionary<TobiraParam.Category, TobiraCategoriesParam> mTobiraCategoriesParam = new Dictionary<TobiraParam.Category, TobiraCategoriesParam>();
    private List<TobiraCondsParam> mTobiraCondParam = new List<TobiraCondsParam>();
    private Dictionary<string, TobiraCondsUnitParam> mTobiraCondUnitParam = new Dictionary<string, TobiraCondsUnitParam>();
    private List<TobiraRecipeParam> mTobiraRecipeParam = new List<TobiraRecipeParam>();
    private List<ConceptCardParam> mConceptCard;
    private Dictionary<string, ConceptCardParam> mConceptCardDict;
    private OInt[,] mConceptCardLvTbl;
    private OInt[] mConceptCardSellCoinRate;
    private List<ConceptCardLsBuffCoefParam> mConceptCardLsBuffCoefParam;
    private Dictionary<string, List<ConceptCardParam>> mConceptCardGroup;
    private Dictionary<string, ConceptCardConditionsParam> mConceptCardConditions;
    private Dictionary<string, ConceptCardTrustRewardParam> mConceptCardTrustReward;
    private Dictionary<string, UnitGroupParam> mUnitGroup;
    private Dictionary<string, JobGroupParam> mJobGroup;
    private List<JobSetParam> mJobSetParam;
    private List<GrowParam> mGrowParam;
    private List<AIParam> mAIParam;
    private List<GeoParam> mGeoParam;
    private List<RarityParam> mRarityParam;
    private List<ShopParam> mShopParam;
    private PlayerParam[] mPlayerParamTbl;
    private OInt[] mPlayerExpTbl;
    private OInt[] mUnitExpTbl;
    private OInt[] mArtifactExpTbl;
    private OInt[] mAbilityExpTbl;
    private OInt[] mAwakePieceTbl;
    private LocalNotificationParam mLocalNotificationParam;
    private TrophyCategoryParam[] mTrophyCategory;
    private TrophyParam[] mTrophy;
    public Dictionary<string, TrophyParam> mTrophyInameDict;
    private TrophyObjective[][] mTrophyDict;
    private ChallengeCategoryParam[] mChallengeCategory;
    private TrophyParam[] mChallenge;
    private TrophyCategoryParam[] mGuildTrophyCategory;
    private GuildTrophyParam[] mGuildTrophy;
    public Dictionary<string, GuildTrophyParam> mGuildTrophyInameDict;
    private TrophyObjective[][] mGuildTrophyDict;
    private UnlockParam[] mUnlock;
    private VipParam[] mVip;
    private PremiumParam[] mPremium;
    private List<BuyCoinShopParam> mBuyCoinShopParam;
    private List<BuyCoinProductParam> mBuyCoinProductParam;
    private List<BuyCoinRewardParam> mBuyCoinRewardParam;
    private List<BuyCoinProductConvertParam> mBuyCoinProductConvertParam;
    private JSON_StreamingMovie[] mStreamingMovies;
    private BannerParam[] mBanner;
    private List<VersusMatchingParam> mVersusMatching;
    private List<VersusMatchCondParam> mVersusMatchCond;
    private Dictionary<string, TowerScoreParam[]> mTowerScores;
    private OInt[] mTowerRankTbl;
    public OInt[] mMultiLimitUnitLv;
    private Dictionary<string, UnitParam> mUnitDictionary;
    private Dictionary<string, Dictionary<string, UnitJobOverwriteParam>> mUnitJobOverwriteDictionary;
    private Dictionary<string, SkillParam> mSkillDictionary;
    private Dictionary<string, AbilityParam> mAbilityDictionary;
    private Dictionary<string, ItemParam> mItemDictionary;
    private Dictionary<string, ArtifactParam> mArtifactDictionary;
    private Dictionary<string, BuffEffectParam> mBuffEffectDictionary;
    private Dictionary<string, CondEffectParam> mCondEffectDictionary;
    private Dictionary<string, RecipeParam> mRecipeDictionary;
    private Dictionary<string, JobSetParam> mJobsetDictionary;
    private Dictionary<string, List<JobSetParam>> mJobsetTargetDictionary;
    private List<AwardParam> mAwardParam;
    private Dictionary<string, AwardParam> mAwardDictionary;
    private LoginInfoParam[] mLoginInfoParam;
    private Dictionary<string, FriendPresentItemParam> mFriendPresentItemParam;
    public StatusCoefficientParam mStatusCoefficient;
    private Dictionary<string, CustomTargetParam> mCustomTarget;
    public SkillAbilityDeriveParam[] mSkillAbilityDeriveParam;
    private List<SkillAbilityDeriveData> mSkillAbilityDerives = new List<SkillAbilityDeriveData>();
    private TipsParam[] mTipsParam;
    private List<GuildEmblemParam> mGuildEmblemParam;
    private Dictionary<string, GuildEmblemParam> mGuildEmblemDictionary;
    private List<GuildFacilityParam> mGuildFacilityParam;
    private Dictionary<string, GuildFacilityParam> mGuildFacilityDictionary;
    private GuildFacilityLvParam[] mGuildFacilityLvParam;
    private List<DynamicTransformUnitParam> mDynamicTransformUnitParam;
    private ConvertUnitPieceExcludeParam[] mConvertUnitPieceExcludeParam;
    private RecommendedArtifactList mRecommendedArtifactList;
    private List<RaidPeriodParam> mRaidPeriodParam;
    private List<RaidPeriodTimeParam> mRaidPeriodTimeParam;
    private List<RaidAreaParam> mRaidAreaParam;
    private List<RaidBossParam> mRaidBossParam;
    private List<RaidBattleRewardParam> mRaidBattleRewardParam;
    private List<RaidBeatRewardParam> mRaidBeatRewardParam;
    private List<RaidDamageRatioRewardParam> mRaidDamageRatioRewardParam;
    private List<RaidDamageAmountRewardParam> mRaidDamageAmountRewardParam;
    private List<RaidAreaClearRewardParam> mRaidAreaClearRewardParam;
    private List<RaidCompleteRewardParam> mRaidCompleteRewardParam;
    private List<RaidRewardParam> mRaidRewardParam;
    private List<ArenaRewardParam> mArenaRewardParams;
    private List<SkillMotionParam> mSkillMotionParam;
    private List<DependStateSpcEffParam> mDependStateSpcEffParam;
    private List<InspSkillLvUpCostParam> mInspSkillLvUpCostParam;
    private Dictionary<string, InspSkillParam> mInspSkillDictionary;
    private Dictionary<int, InspSkillCostParam> mInspSkillResetCostDictionary;
    private Dictionary<int, InspSkillCostParam> mInspSkillOpenCostDictionary;
    private SRPG.HighlightParam[] mHighlightParam;
    private List<GenesisParam> mGenesisParam;
    private CoinBuyUseBonusParam[] mCoinBuyUseBonusParam;
    private CoinBuyUseBonusRewardSetParam[] mCoinBuyUseBonusRewardSetParam;
    private CoinBuyUseBonusRewardParam[] mCoinBuyUseBonusRewardParam;
    private Dictionary<string, UnitRentalNotificationParam> mUnitRentalNotificationParams = new Dictionary<string, UnitRentalNotificationParam>();
    private Dictionary<string, UnitRentalParam> mUnitRentalParams = new Dictionary<string, UnitRentalParam>();
    private Dictionary<string, DrawCardRewardParam> mDrawCardRewardDict = new Dictionary<string, DrawCardRewardParam>();
    private Dictionary<string, DrawCardParam> mDrawCardDict = new Dictionary<string, DrawCardParam>();
    private Dictionary<string, TrophyStarMissionRewardParam> mTrophyStarMissionRewardDict;
    private Dictionary<string, TrophyStarMissionParam> mTrophyStarMissionDict;
    private List<UnitPieceShopParam> mUnitPieceShop;
    private List<UnitPieceShopGroupParam> mUnitPieceShopGroup;
    private TwitterMessageParam[] mTwitterMessageParams;
    private FilterConceptCardParam[] mFilterConceptCardParams;
    private FilterRuneParam[] mFilterRuneParams;
    private FilterUnitParam[] mFilterUnitParams;
    private FilterArtifactParam[] mFilterArtifactParams;
    private SortRuneParam[] mSortRuneParams;
    private List<RuneParam> mRuneParam;
    private Dictionary<string, RuneParam> mRuneParamDict;
    private List<RuneLotteryBaseState> mRuneLotteryBaseState;
    private List<RuneLotteryEvoState> mRuneLotteryEvoState;
    private List<RuneMaterial> mRuneMaterial;
    private List<RuneCost> mRuneCost;
    private List<RuneSetEff> mRuneSetEff;
    private List<JukeBoxParam> mJukeBoxParams;
    private List<JukeBoxSectionParam> mJukeBoxSectionParams;
    private List<UnitSameGroupParam> mUnitSameGroup;
    private AutoRepeatQuestBoxParam[] mAutoRepeatQuestBoxParams;
    private GuildAttendParam[] mGuildAttendParams;
    private List<GuildAttendRewardParam> mGuildAttendRewardParams;
    private GuildRoleBonusParam[] mGuildRoleBonusParams;
    private List<GuildRoleBonusRewardParam> mGuildRoleBonusRewardParams;
    private Dictionary<string, ResetCostParam> mResetCostParams = new Dictionary<string, ResetCostParam>();
    private List<ProtectSkillParam> mProtectSkillParams;
    private GuildSearchFilterParam[] mGuildSeartchFilterParams;
    private List<ReplaceSprite> mRepraseSprite;
    private List<ExpansionPurchaseParam> mExpansionPurchaseParams;
    private Dictionary<string, List<string>> mExpansionPurchaseQuestParams = new Dictionary<string, List<string>>();
    private List<SkillAdditionalParam> mSkillAdditionalList;
    private List<SkillAntiShieldParam> mSkillAntiShieldList;

    public TrophyCategoryParam[] GuildTrophyCategory => this.mGuildTrophyCategory;

    public GuildTrophyParam[] GuildTrophy => this.mGuildTrophy;

    public PremiumParam[] Premium => this.mPremium;

    public List<SkillAbilityDeriveData> SkillAbilityDerives => this.mSkillAbilityDerives;

    public TipsParam[] Tips => this.mTipsParam;

    public ConvertUnitPieceExcludeParam[] ConvertUnitPieceExclude
    {
      get => this.mConvertUnitPieceExcludeParam;
    }

    public SRPG.HighlightParam[] HighlightParam => this.mHighlightParam;

    public CoinBuyUseBonusRewardSetParam[] CoinBuyUseBonusRewardSetParams
    {
      get => this.mCoinBuyUseBonusRewardSetParam;
    }

    public CoinBuyUseBonusRewardParam[] CoinBuyUseBonusRewardParams
    {
      get => this.mCoinBuyUseBonusRewardParam;
    }

    public Dictionary<string, UnitRentalNotificationParam> UnitRentalNotificationParams
    {
      get => this.mUnitRentalNotificationParams;
    }

    public Dictionary<string, UnitRentalParam> UnitRentalParams => this.mUnitRentalParams;

    public Dictionary<string, DrawCardRewardParam> DrawCardRewardDict => this.mDrawCardRewardDict;

    public Dictionary<string, DrawCardParam> DrawCardDict => this.mDrawCardDict;

    public Dictionary<string, TrophyStarMissionRewardParam> TrophyStarMissionRewardDict
    {
      get => this.mTrophyStarMissionRewardDict;
    }

    public Dictionary<string, TrophyStarMissionParam> TrophyStarMissionDict
    {
      get => this.mTrophyStarMissionDict;
    }

    public List<UnitPieceShopParam> UnitPieceShop => this.mUnitPieceShop;

    public TwitterMessageParam[] TwitterMessageParams => this.mTwitterMessageParams;

    public FilterConceptCardParam[] FilterConceptCardParams => this.mFilterConceptCardParams;

    public FilterRuneParam[] FilterRuneParams => this.mFilterRuneParams;

    public FilterUnitParam[] FilterUnitParams => this.mFilterUnitParams;

    public FilterArtifactParam[] FilterArtifactParams => this.mFilterArtifactParams;

    public SortRuneParam[] SortRuneParams => this.mSortRuneParams;

    public List<JukeBoxParam> JukeBoxParams => this.mJukeBoxParams;

    public List<JukeBoxSectionParam> JukeBoxSectionParams => this.mJukeBoxSectionParams;

    public AutoRepeatQuestBoxParam[] AutoRepeatQuestBoxParams => this.mAutoRepeatQuestBoxParams;

    public GuildAttendParam[] GuildAttendParams => this.mGuildAttendParams;

    public List<GuildAttendRewardParam> GuildAttendRewardParams => this.mGuildAttendRewardParams;

    public GuildRoleBonusParam[] GuildRoleBonusParams => this.mGuildRoleBonusParams;

    public List<GuildRoleBonusRewardParam> GuildRoleBonusRewardParams
    {
      get => this.mGuildRoleBonusRewardParams;
    }

    public Dictionary<string, ResetCostParam> ResetCostParams => this.mResetCostParams;

    public List<ProtectSkillParam> ProtectSkillParams => this.mProtectSkillParams;

    public GuildSearchFilterParam[] GuildSearchFilterParams => this.mGuildSeartchFilterParams;

    public List<ReplaceSprite> RepraseSprite => this.mRepraseSprite;

    public List<ExpansionPurchaseParam> ExpansionPurchaseParams => this.mExpansionPurchaseParams;

    public Dictionary<string, List<string>> ExpansionPurchaseQuestParams
    {
      get => this.mExpansionPurchaseQuestParams;
    }

    public List<SkillAdditionalParam> SkillAdditionalList => this.mSkillAdditionalList;

    public List<SkillAntiShieldParam> SkillAntiShieldList => this.mSkillAntiShieldList;

    public bool Loaded { get; set; }

    public FixParam FixParam => this.mFixParam;

    public LocalNotificationParam LocalNotificationParam => this.mLocalNotificationParam;

    public List<ItemParam> Items => this.mItemParam;

    public JobSetParam[] JobSets => this.mJobSetParam.ToArray();

    public List<ArtifactParam> Artifacts => this.mArtifactParam;

    public List<CollaboSkillParam> CollaboSkills => this.mCollaboSkillParam;

    public Dictionary<string, InspSkillParam> InspirationSkillParams => this.mInspSkillDictionary;

    private void CreateTrophyDict()
    {
      if (this.mTrophy == null)
        return;
      List<TrophyObjective> trophyObjectiveList = new List<TrophyObjective>(this.mTrophy.Length);
      Array values = Enum.GetValues(typeof (TrophyConditionTypes));
      this.mTrophyDict = new TrophyObjective[values.Length][];
      for (int index1 = 0; index1 < values.Length; ++index1)
      {
        TrophyConditionTypes trophyConditionTypes = (TrophyConditionTypes) values.GetValue(index1);
        trophyObjectiveList.Clear();
        for (int index2 = 0; index2 < this.mTrophy.Length; ++index2)
        {
          TrophyParam trophyParam = this.mTrophy[index2];
          for (int index3 = 0; index3 < trophyParam.Objectives.Length; ++index3)
          {
            if (trophyParam.Objectives[index3].type == trophyConditionTypes)
              trophyObjectiveList.Add(trophyParam.Objectives[index3]);
          }
        }
        this.mTrophyDict[index1] = trophyObjectiveList.ToArray();
      }
    }

    private void AddUnitToItem()
    {
      if (this.mUnitDictionary.Count <= 0)
        return;
      foreach (KeyValuePair<string, UnitParam> mUnit in this.mUnitDictionary)
      {
        if (mUnit.Value != null && !string.IsNullOrEmpty(mUnit.Value.piece))
        {
          ItemParam itemParam = new ItemParam();
          itemParam.iname = mUnit.Value.iname;
          if (this.mItemDictionary.ContainsKey(mUnit.Value.piece))
          {
            ItemParam mItem = this.mItemDictionary[mUnit.Value.piece];
            if (mItem != null)
            {
              itemParam.name = mUnit.Value.name;
              itemParam.icon = mItem.icon;
              itemParam.type = EItemType.Unit;
              itemParam.cap = 999;
              this.mItemDictionary.Add(itemParam.iname, itemParam);
            }
          }
        }
      }
    }

    public bool Deserialize(JSON_MasterParam json) => this.Deserialize2(json);

    public void CacheReferences()
    {
      for (int index = 0; index < this.mUnitParam.Count; ++index)
      {
        if (this.mUnitParam[index] != null)
          this.mUnitParam[index].CacheReferences(this);
      }
    }

    private void DebugLogError(string bufs)
    {
    }

    public void DumpLoadedLog()
    {
    }

    public UnitParam[] GetAllUnits()
    {
      return this.mUnitParam != null ? this.mUnitParam.ToArray() : new UnitParam[0];
    }

    public bool ContainsUnitID(string key) => this.mUnitDictionary.ContainsKey(key);

    public UnitParam GetUnitParam(string key)
    {
      try
      {
        return this.mUnitDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<UnitParam>(key);
      }
    }

    public Dictionary<string, UnitJobOverwriteParam> GetUnitJobOverwriteParamsForUnit(
      string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
      {
        DebugUtility.LogError("Unknown UnitJobOverwriteParam \"" + unit_iname + "\"");
        return (Dictionary<string, UnitJobOverwriteParam>) null;
      }
      Dictionary<string, UnitJobOverwriteParam> overwriteParamsForUnit;
      this.mUnitJobOverwriteDictionary.TryGetValue(unit_iname, out overwriteParamsForUnit);
      return overwriteParamsForUnit;
    }

    public SkillParam GetSkillParam(string key)
    {
      try
      {
        return this.mSkillDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<SkillParam>(key);
      }
    }

    public BuffEffectParam GetBuffEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (BuffEffectParam) null;
      try
      {
        return this.mBuffEffectDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown BuffEffectParam \"" + key + "\"");
        return (BuffEffectParam) null;
      }
    }

    public CondEffectParam GetCondEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (CondEffectParam) null;
      try
      {
        return this.mCondEffectDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown CondEffectParam \"" + key + "\"");
        return (CondEffectParam) null;
      }
    }

    public AbilityParam GetAbilityParam(string key)
    {
      try
      {
        return this.mAbilityDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<AbilityParam>(key);
      }
    }

    public ItemParam GetItemParam(string key)
    {
      try
      {
        return this.mItemDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ItemParam \"" + key + "\"");
        return (ItemParam) null;
      }
    }

    public ItemParam GetItemParam(string key, bool showLogError)
    {
      if (showLogError)
        return this.GetItemParam(key);
      return !this.mItemDictionary.ContainsKey(key) ? (ItemParam) null : this.mItemDictionary[key];
    }

    public ArtifactParam GetArtifactParam(string key)
    {
      try
      {
        return this.mArtifactDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ArtifactParam \"" + key + "\"");
        return (ArtifactParam) null;
      }
    }

    public ArtifactParam GetArtifactParam(string key, bool showLogError)
    {
      if (showLogError)
        return this.GetArtifactParam(key);
      return !this.mArtifactDictionary.ContainsKey(key) ? (ArtifactParam) null : this.mArtifactDictionary[key];
    }

    public bool GetArtifactMaxNum(ArtifactParam param)
    {
      List<ArtifactData> artifacts = MonoSingleton<GameManager>.Instance.Player.Artifacts;
      int num = 0;
      for (int index = 0; index < artifacts.Count; ++index)
      {
        if (artifacts[index].ArtifactParam == param)
          ++num;
      }
      return num < param.maxnum;
    }

    public WeaponParam GetWeaponParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (WeaponParam) null;
      WeaponParam weaponParam = this.mWeaponParam.Find((Predicate<WeaponParam>) (p => p.iname == key));
      if (weaponParam == null)
        DebugUtility.LogError("Unknown WeaponParam \"" + key + "\"");
      return weaponParam;
    }

    public RecipeParam GetRecipeParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (RecipeParam) null;
      try
      {
        return this.mRecipeDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown mRecipeParam \"" + key + "\"");
        return (RecipeParam) null;
      }
    }

    public JobParam GetJobParam(string key)
    {
      try
      {
        return this.mJobParamDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<JobParam>(key);
      }
    }

    public JobParam[] GetAllJobs()
    {
      return this.mJobParam != null ? this.mJobParam.ToArray() : new JobParam[0];
    }

    public QuestClearUnlockUnitDataParam[] GetAllUnlockUnitDatas()
    {
      return this.mUnlockUnitDataParam != null ? this.mUnlockUnitDataParam.ToArray() : new QuestClearUnlockUnitDataParam[0];
    }

    public QuestClearUnlockUnitDataParam GetUnlockUnitData(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (QuestClearUnlockUnitDataParam) null;
      return this.mUnlockUnitDataParam.Find((Predicate<QuestClearUnlockUnitDataParam>) (p => p.iname == key)) ?? throw new KeyNotFoundException<QuestClearUnlockUnitDataParam>(key);
    }

    public CollaboSkillParam GetCollaboSkillData(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (CollaboSkillParam) null;
      if (this.mCollaboSkillParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData no data!</color>"));
        return (CollaboSkillParam) null;
      }
      CollaboSkillParam collaboSkillData = this.mCollaboSkillParam.Find((Predicate<CollaboSkillParam>) (d => d.UnitIname == unit_iname));
      if (collaboSkillData == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData data not found! unit_iname={0}</color>", (object) unit_iname));
      return collaboSkillData;
    }

    public TrickParam GetTrickParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (TrickParam) null;
      if (this.mTrickParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam no data!</color>"));
        return (TrickParam) null;
      }
      TrickParam trickParam = this.mTrickParam.Find((Predicate<TrickParam>) (d => d.Iname == iname));
      if (trickParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam data not found! iname={0}</color>", (object) iname));
      return trickParam;
    }

    public BreakObjParam GetBreakObjParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (BreakObjParam) null;
      if (this.mBreakObjParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam no data!</color>"));
        return (BreakObjParam) null;
      }
      BreakObjParam breakObjParam = this.mBreakObjParam.Find((Predicate<BreakObjParam>) (d => d.Iname == iname));
      if (breakObjParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam data not found! iname={0}</color>", (object) iname));
      return breakObjParam;
    }

    public WeatherParam GetWeatherParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherParam) null;
      if (this.mWeatherParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam no data!</color>"));
        return (WeatherParam) null;
      }
      WeatherParam weatherParam = this.mWeatherParam.Find((Predicate<WeatherParam>) (d => d.Iname == iname));
      if (weatherParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam data not found! iname={0}</color>", (object) iname));
      return weatherParam;
    }

    public DynamicTransformUnitParam GetDynamicTransformUnitParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (DynamicTransformUnitParam) null;
      if (this.mDynamicTransformUnitParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDynamicTransformUnitParam no data!</color>"));
        return (DynamicTransformUnitParam) null;
      }
      DynamicTransformUnitParam transformUnitParam = this.mDynamicTransformUnitParam.Find((Predicate<DynamicTransformUnitParam>) (d => d.Iname == iname));
      if (transformUnitParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDynamicTransformUnitParam data not found! iname={0}</color>", (object) iname));
      return transformUnitParam;
    }

    public SkillMotionParam GetSkillMotionParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SkillMotionParam) null;
      if (this.mSkillMotionParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetSkillMotionParam no data!</color>"));
        return (SkillMotionParam) null;
      }
      SkillMotionParam skillMotionParam = this.mSkillMotionParam.Find((Predicate<SkillMotionParam>) (d => d.Iname == iname));
      if (skillMotionParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetSkillMotionParam data not found! iname={0}</color>", (object) iname));
      return skillMotionParam;
    }

    public DependStateSpcEffParam GetDependStateSpcEffParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (DependStateSpcEffParam) null;
      if (this.mDependStateSpcEffParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDependStateSpcEffParam no data!</color>"));
        return (DependStateSpcEffParam) null;
      }
      DependStateSpcEffParam stateSpcEffParam = this.mDependStateSpcEffParam.Find((Predicate<DependStateSpcEffParam>) (d => d.Iname == iname));
      if (stateSpcEffParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDependStateSpcEffParam data not found! iname={0}</color>", (object) iname));
      return stateSpcEffParam;
    }

    public GenesisParam GetGenesisParam()
    {
      if (this.mGenesisParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetGenesisParam no data!</color>"));
        return (GenesisParam) null;
      }
      GenesisParam genesisParam = this.mGenesisParam.Find((Predicate<GenesisParam>) (d => d.IsValid));
      if (genesisParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetGenesisParam valid data not found!</color>"));
      return genesisParam;
    }

    public TobiraParam GetTobiraParam(string unit_iname, TobiraParam.Category category)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (TobiraParam) null;
      if (this.mTobiraParam != null)
        return this.mTobiraParam.Find((Predicate<TobiraParam>) (param => param.UnitIname == unit_iname && param.TobiraCategory == category));
      DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>"));
      return (TobiraParam) null;
    }

    public TobiraParam[] GetTobiraListForUnit(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (TobiraParam[]) null;
      if (this.mTobiraParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>"));
        return (TobiraParam[]) null;
      }
      TobiraParam[] tobira_list = new TobiraParam[TobiraParam.MAX_TOBIRA_COUNT];
      this.mTobiraParam.ForEach((Action<TobiraParam>) (param =>
      {
        if (!(param.UnitIname == unit_iname))
          return;
        tobira_list[(int) param.TobiraCategory] = param;
      }));
      return tobira_list;
    }

    public bool CanUnlockTobira(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return false;
      if (this.mTobiraCondParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>"));
        return false;
      }
      return this.mTobiraCondParam.Find((Predicate<TobiraCondsParam>) (param => !(param.UnitIname != unit_iname) && param.TobiraCategory == TobiraParam.Category.START)) != null;
    }

    public TobiraConditionParam[] GetTobiraConditionsForUnit(
      string unit_iname,
      TobiraParam.Category category)
    {
      Dictionary<TobiraParam.Category, TobiraConditionParam[]> conditionsForUnit = this.GetTobiraConditionsForUnit(unit_iname);
      if (conditionsForUnit == null)
        return (TobiraConditionParam[]) null;
      TobiraConditionParam[] tobiraConditionParamArray;
      conditionsForUnit.TryGetValue(category, out tobiraConditionParamArray);
      return tobiraConditionParamArray ?? (TobiraConditionParam[]) null;
    }

    public Dictionary<TobiraParam.Category, TobiraConditionParam[]> GetTobiraConditionsForUnit(
      string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (Dictionary<TobiraParam.Category, TobiraConditionParam[]>) null;
      if (this.mTobiraCondParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>"));
        return (Dictionary<TobiraParam.Category, TobiraConditionParam[]>) null;
      }
      Dictionary<TobiraParam.Category, TobiraConditionParam[]> condition_list = new Dictionary<TobiraParam.Category, TobiraConditionParam[]>();
      this.mTobiraCondParam.ForEach((Action<TobiraCondsParam>) (param =>
      {
        if (!(param.UnitIname == unit_iname))
          return;
        condition_list.Add(param.TobiraCategory, param.Conditions);
        Array.ForEach<TobiraConditionParam>(param.Conditions, (Action<TobiraConditionParam>) (tcp =>
        {
          if (tcp.CondType != TobiraConditionParam.ConditionType.Unit)
            return;
          TobiraCondsUnitParam cond_unit;
          this.mTobiraCondUnitParam.TryGetValue(tcp.CondIname, out cond_unit);
          if (cond_unit == null)
            return;
          tcp.SetCondUnit(cond_unit);
        }));
      }));
      return condition_list;
    }

    public TobiraRecipeParam GetTobiraRecipe(
      string unit_iname,
      TobiraParam.Category category,
      int level)
    {
      TobiraParam tobiraParam = this.GetTobiraParam(unit_iname, category);
      return tobiraParam == null ? (TobiraRecipeParam) null : this.GetTobiraRecipe(tobiraParam.RecipeId, level);
    }

    public TobiraRecipeParam GetTobiraRecipe(string recipe_iname, int level)
    {
      return this.mTobiraRecipeParam.Find((Predicate<TobiraRecipeParam>) (param => param.RecipeIname == recipe_iname && param.Level == level));
    }

    public JobSetParam GetJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam) null;
      try
      {
        return this.mJobsetDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown mJobSetParam \"" + key + "\"");
        return (JobSetParam) null;
      }
    }

    public JobSetParam[] GetClassChangeJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam[]) null;
      try
      {
        return this.mJobsetTargetDictionary[key].ToArray();
      }
      catch
      {
        return (JobSetParam[]) null;
      }
    }

    public GrowParam GetGrowParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (GrowParam) null;
      GrowParam growParam = this.mGrowParam.Find((Predicate<GrowParam>) (p => p.type == key));
      if (growParam == null)
        DebugUtility.LogError("Unknown GrowParam \"" + key + "\"");
      return growParam;
    }

    public AIParam GetAIParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (AIParam) null;
      AIParam aiParam = this.mAIParam.Find((Predicate<AIParam>) (p => p.iname == key));
      if (aiParam == null)
        DebugUtility.LogError("Failed AIParam iname \"" + key + "\" not found.");
      return aiParam;
    }

    public GeoParam GetGeoParam(string key)
    {
      if (this.mGeoParam == null)
        return (GeoParam) null;
      if (string.IsNullOrEmpty(key))
        return (GeoParam) null;
      GeoParam geoParam = this.mGeoParam.Find((Predicate<GeoParam>) (p => p.iname == key));
      if (geoParam == null)
        DebugUtility.LogError("Failed GeoParam iname \"" + key + "\" not found.");
      return geoParam;
    }

    public RarityParam GetRarityParam(int rarity)
    {
      if (this.mRarityParam != null && (rarity >= 0 || rarity < this.mRarityParam.Count))
        return this.mRarityParam[rarity];
      DebugUtility.LogError("mRarityParam Stack Overflow.");
      return (RarityParam) null;
    }

    public ShopParam GetShopParam(EShopType type)
    {
      int index1 = (int) type;
      switch (type)
      {
        case EShopType.Event:
          index1 = -1;
          for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
          {
            string[] strArray = GlobalVars.EventShopItem.shops.gname.Split('-');
            if (this.mShopParam[index2].iname.Equals(strArray[0]))
            {
              index1 = index2;
              break;
            }
          }
          if (index1 < 0)
          {
            DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.EventShopItem.shops.gname.Split('-'));
            return (ShopParam) null;
          }
          break;
        case EShopType.Limited:
        case EShopType.Port:
          index1 = -1;
          for (int index3 = 0; index3 < this.mShopParam.Count; ++index3)
          {
            string[] strArray = GlobalVars.LimitedShopItem.shops.gname.Split('-');
            if (this.mShopParam[index3].iname.Equals(strArray[0]))
            {
              index1 = index3;
              break;
            }
          }
          if (index1 < 0)
          {
            DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.LimitedShopItem.shops.gname.Split('-'));
            return (ShopParam) null;
          }
          break;
        case EShopType.Guerrilla:
          index1 = -1;
          for (int index4 = 0; index4 < this.mShopParam.Count; ++index4)
          {
            if (this.mShopParam[index4].iname.Equals("Guerrilla"))
            {
              index1 = index4;
              break;
            }
          }
          break;
      }
      if (this.mShopParam != null && index1 >= 0 && index1 < this.mShopParam.Count)
        return this.mShopParam[index1];
      DebugUtility.LogError("mShopParam Stack Overflow.");
      return (ShopParam) null;
    }

    public int GetShopType(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return 0;
      int shopType = Array.FindIndex<string>(GameSettings.Instance.Shop_ShopIdToShopTypeConvTbl, (Predicate<string>) (p => 0 == iname.IndexOf(p)));
      if (shopType < 0)
      {
        DebugUtility.LogError("Failed. not found EShopType. iname=\"" + iname + "\"");
        shopType = 0;
      }
      return shopType;
    }

    public List<UnitPieceShopGroupParam> GetUnitPieceShopGroups() => this.mUnitPieceShopGroup;

    public PlayerParam GetPlayerParam(int lv)
    {
      return lv > 0 && lv <= this.GetPlayerLevelCap() ? this.mPlayerParamTbl[lv - 1] : (PlayerParam) null;
    }

    public int GetAbilityNextGold(int rank)
    {
      DebugUtility.Assert(rank > 0 && rank <= this.mAbilityExpTbl.Length, "指定ランク" + (object) rank + "がアビリティのランク範囲に存在しない。");
      return (int) this.mAbilityExpTbl[rank];
    }

    public int GetAwakeNeedPieces(int awakeLv)
    {
      DebugUtility.Assert(awakeLv >= 0 && awakeLv < this.mAwakePieceTbl.Length, "覚醒回数" + (object) awakeLv + "が覚醒可能な範囲に存在しない。");
      return (int) this.mAwakePieceTbl[awakeLv];
    }

    public int GetUnitNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      return (int) this.mUnitExpTbl[lv - 1];
    }

    public int GetUnitLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      int unitLevelExp = 0;
      for (int index = 0; index < lv; ++index)
        unitLevelExp += (int) this.mUnitExpTbl[index];
      return unitLevelExp;
    }

    public int CalcUnitLevel(int totalExp, int levelCap)
    {
      int val2 = levelCap;
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < val2; ++index)
      {
        num += this.GetUnitNextExp(index + 1);
        if (num <= totalExp)
          ++val1;
        else
          break;
      }
      return Math.Min(Math.Max(val1, 1), val2);
    }

    public int GetUnitMaxLevel() => this.mUnitExpTbl.Length;

    public int GetPlayerNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      return (int) this.mPlayerExpTbl[lv - 1];
    }

    public int GetPlayerLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      int playerLevelExp = 0;
      for (int index = 0; index < lv; ++index)
        playerLevelExp += (int) this.mPlayerExpTbl[index];
      return playerLevelExp;
    }

    public int GetPlayerLevelCap() => this.mPlayerExpTbl.Length;

    public int GetVipArenaResetCount(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].ResetArenaNum;
    }

    public int GetVipRankNextPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].NextRankNeedPoint;
    }

    public int GetVipRankTotalNeedPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      int rankTotalNeedPoint = 0;
      for (int index = 0; index < rank; ++index)
        rankTotalNeedPoint += this.mVip[index].NextRankNeedPoint;
      return rankTotalNeedPoint;
    }

    public int GetVipBuyStaminaLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyStaminaNum;
    }

    public int GetVipBuyGoldLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyCoinNum;
    }

    public int GetVipRankCap() => this.mVip == null ? 0 : Math.Max(this.mVip.Length - 1, 0);

    public float GetConceptCardCoinRate(int awake)
    {
      if (this.mConceptCardSellCoinRate != null && awake >= 0 && awake < this.mConceptCardSellCoinRate.Length)
        return (float) (int) this.mConceptCardSellCoinRate[awake] / 100f;
      DebugUtility.LogError("コイン取得倍率が限突数分用意されてない。");
      return 0.0f;
    }

    public int GetConceptCardLsBuffCoefInt(int rare, int bt_limit)
    {
      return ConceptCardLsBuffCoefParam.GetCoef(this.mConceptCardLsBuffCoefParam, rare, bt_limit);
    }

    public int GetConceptCardLsBuffFriendCoefInt(int rare, int bt_limit)
    {
      return ConceptCardLsBuffCoefParam.GetFriendCoef(this.mConceptCardLsBuffCoefParam, rare, bt_limit);
    }

    public float GetConceptCardLsBuffFriendCoef(int rare, int bt_limit)
    {
      return (float) this.GetConceptCardLsBuffFriendCoefInt(rare, bt_limit) / 10000f;
    }

    public TrophyCategoryParam[] TrophyCategories => this.mTrophyCategory;

    public ChallengeCategoryParam[] ChallengeCategories => this.mChallengeCategory;

    public TrophyParam[] Trophies => this.mTrophy;

    public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
    {
      return this.mTrophyDict == null ? new TrophyObjective[0] : this.mTrophyDict[(int) type];
    }

    public TrophyParam GetTrophy(string iname)
    {
      if (this.mTrophy == null)
        return (TrophyParam) null;
      TrophyParam trophyParam;
      return this.mTrophyInameDict.TryGetValue(iname, out trophyParam) ? trophyParam : (TrophyParam) null;
    }

    public TrophyParam GetGuildTrophy(string iname)
    {
      if (this.mGuildTrophy == null)
        return (TrophyParam) null;
      GuildTrophyParam guildTrophyParam;
      return this.mGuildTrophyInameDict.TryGetValue(iname, out guildTrophyParam) ? (TrophyParam) guildTrophyParam : (TrophyParam) null;
    }

    public UnlockParam[] Unlocks => this.mUnlock;

    public UnlockParam GetUnlockParam(string iname)
    {
      for (int index = this.mUnlock.Length - 1; index >= 0; --index)
      {
        if (this.mUnlock[index].iname == iname)
          return this.mUnlock[index];
      }
      return (UnlockParam) null;
    }

    public UnlockParam FindUnlockParam(UnlockTargets value)
    {
      for (int index = this.mUnlock.Length - 1; index >= 0; --index)
      {
        if (this.mUnlock[index].UnlockTarget == value)
          return this.mUnlock[index];
      }
      return (UnlockParam) null;
    }

    public UnitParam GetUnitParamForPiece(string key, bool doCheck = true)
    {
      if (string.IsNullOrEmpty(key))
        return (UnitParam) null;
      if (key == (string) this.FixParam.CommonPieceAll || key == (string) this.FixParam.CommonPieceDark || key == (string) this.FixParam.CommonPieceFire || key == (string) this.FixParam.CommonPieceShine || key == (string) this.FixParam.CommonPieceThunder || key == (string) this.FixParam.CommonPieceWater || key == (string) this.FixParam.CommonPieceWind)
        return (UnitParam) null;
      UnitParam unitParamForPiece = this.mUnitParam.Find((Predicate<UnitParam>) (p => p.piece == key));
      if (doCheck && unitParamForPiece == null)
        DebugUtility.LogError("Failed UnitParam iname \"" + key + "\" not found.");
      return unitParamForPiece;
    }

    public OInt[] GetArtifactExpTable() => this.mArtifactExpTbl;

    public string TranslateMoviePath(string path)
    {
      if (this.mStreamingMovies == null)
        return path;
      for (int index = 0; index < this.mStreamingMovies.Length; ++index)
      {
        if (this.mStreamingMovies[index].alias == path)
          return this.mStreamingMovies[index].path;
      }
      return path;
    }

    public ArtifactParam GetSkinParamFromItemId(string itemId)
    {
      return Array.Find<ArtifactParam>(this.mArtifactParam.ToArray(), (Predicate<ArtifactParam>) (s => s.kakera == itemId));
    }

    public bool IsSkinItem(string itemId) => this.GetSkinParamFromItemId(itemId) != null;

    public BannerParam[] Banners => this.mBanner;

    public bool ContainsAwardID(string key) => this.mAwardDictionary.ContainsKey(key);

    public AwardParam GetAwardParam(string key, bool showLogError = true)
    {
      try
      {
        return this.mAwardDictionary[key];
      }
      catch (Exception ex)
      {
        if (showLogError)
          DebugUtility.LogError("Unknown AwardParam \"" + key + "\"");
        return (AwardParam) null;
      }
    }

    public AwardParam[] GetAllAwards()
    {
      return this.mAwardParam != null ? this.mAwardParam.ToArray() : new AwardParam[0];
    }

    public LoginInfoParam[] GetAllLoginInfos()
    {
      return this.mLoginInfoParam != null ? this.mLoginInfoParam : new LoginInfoParam[0];
    }

    public LoginInfoParam[] GetActiveLoginInfos()
    {
      if (this.mLoginInfoParam == null)
        return (LoginInfoParam[]) null;
      List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>();
      int player_level = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
      bool is_beginner = MonoSingleton<GameManager>.Instance.Player.IsBeginner();
      int num = 0;
      for (int index = 0; index < this.mLoginInfoParam.Length; ++index)
      {
        if (this.mLoginInfoParam[index].IsDisplayable(TimeManager.ServerTime, player_level, is_beginner))
        {
          string key = this.mLoginInfoParam[index].iname + (object) this.mLoginInfoParam[index].begin_at;
          if (PlayerPrefsUtility.HasKey(key) && PlayerPrefsUtility.GetInt(key) >= this.mLoginInfoParam[index].draw_count && this.mLoginInfoParam[index].priority > 0)
            this.mLoginInfoParam[index].priority = 0;
          loginInfoParamList.Add(this.mLoginInfoParam[index]);
          if (this.mLoginInfoParam[index].priority > 0)
            ++num;
        }
      }
      if (num > 0)
      {
        for (int index = 0; index < loginInfoParamList.Count; ++index)
        {
          if (loginInfoParamList[index].priority == 0)
          {
            loginInfoParamList.Remove(loginInfoParamList[index]);
            --index;
          }
        }
      }
      return loginInfoParamList.ToArray();
    }

    public VersusMatchingParam[] GetVersusMatchingParam() => this.mVersusMatching.ToArray();

    public VersusMatchCondParam[] GetVersusMatchingCondition() => this.mVersusMatchCond.ToArray();

    public OInt[] TowerRank => this.mTowerRankTbl;

    public OInt[] GetMultiPlayLimitUnitLv() => this.mMultiLimitUnitLv;

    public bool Deserialize2(JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      DebugUtility.Verify((object) json, typeof (JSON_MasterParam));
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          JSON_UnitParam json1 = json.Unit[index];
          UnitParam unitParam = new UnitParam();
          this.mUnitParam.Add(unitParam);
          unitParam.Deserialize(json1);
          if (!this.mUnitDictionary.ContainsKey(json1.iname))
            this.mUnitDictionary.Add(json1.iname, unitParam);
        }
      }
      if (json.UnitJobOverwrite != null)
      {
        if (this.mUnitJobOverwriteParam == null)
          this.mUnitJobOverwriteParam = new List<UnitJobOverwriteParam>();
        if (this.mUnitJobOverwriteDictionary == null)
          this.mUnitJobOverwriteDictionary = new Dictionary<string, Dictionary<string, UnitJobOverwriteParam>>();
        foreach (JSON_UnitJobOverwriteParam json2 in json.UnitJobOverwrite)
        {
          UnitJobOverwriteParam jobOverwriteParam = new UnitJobOverwriteParam();
          this.mUnitJobOverwriteParam.Add(jobOverwriteParam);
          jobOverwriteParam.Deserialize(json2);
          Dictionary<string, UnitJobOverwriteParam> dictionary;
          this.mUnitJobOverwriteDictionary.TryGetValue(json2.unit_iname, out dictionary);
          if (dictionary == null)
          {
            dictionary = new Dictionary<string, UnitJobOverwriteParam>();
            this.mUnitJobOverwriteDictionary.Add(json2.unit_iname, dictionary);
          }
          if (!dictionary.ContainsKey(json2.job_iname))
            dictionary.Add(json2.job_iname, jobOverwriteParam);
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          JSON_SkillParam json3 = json.Skill[index];
          SkillParam skillParam = new SkillParam();
          this.mSkillParam.Add(skillParam);
          skillParam.Deserialize(json3);
          if (!this.mSkillDictionary.ContainsKey(json3.iname))
            this.mSkillDictionary.Add(json3.iname, skillParam);
        }
        SkillParam.UpdateReplaceSkill(this.mSkillParam);
      }
      if (json.Buff != null)
      {
        this.mBuffEffectDictionary = new Dictionary<string, BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          JSON_BuffEffectParam json4 = json.Buff[index];
          BuffEffectParam buffEffectParam = new BuffEffectParam();
          buffEffectParam.Deserialize(json4);
          if (!this.mBuffEffectDictionary.ContainsKey(json4.iname))
            this.mBuffEffectDictionary.Add(json4.iname, buffEffectParam);
        }
      }
      if (json.Cond != null)
      {
        this.mCondEffectDictionary = new Dictionary<string, CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          JSON_CondEffectParam json5 = json.Cond[index];
          CondEffectParam condEffectParam = new CondEffectParam();
          condEffectParam.Deserialize(json5);
          if (!this.mCondEffectDictionary.ContainsKey(json5.iname))
            this.mCondEffectDictionary.Add(json5.iname, condEffectParam);
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          JSON_AbilityParam json6 = json.Ability[index];
          AbilityParam abilityParam = new AbilityParam();
          this.mAbilityParam.Add(abilityParam);
          abilityParam.Deserialize(json6);
          if (!this.mAbilityDictionary.ContainsKey(json6.iname))
            this.mAbilityDictionary.Add(json6.iname, abilityParam);
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          JSON_ItemParam json7 = json.Item[index];
          ItemParam itemParam = new ItemParam();
          this.mItemParam.Add(itemParam);
          itemParam.Deserialize(json7);
          itemParam.no = index + 1;
          if (!this.mItemDictionary.ContainsKey(json7.iname))
            this.mItemDictionary.Add(json7.iname, itemParam);
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          JSON_ArtifactParam json8 = json.Artifact[index];
          if (json8.iname != null)
          {
            ArtifactParam artifactParam = new ArtifactParam();
            this.mArtifactParam.Add(artifactParam);
            artifactParam.Deserialize(json8);
            if (!this.mArtifactDictionary.ContainsKey(json8.iname))
              this.mArtifactDictionary.Add(json8.iname, artifactParam);
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          JSON_WeaponParam json9 = json.Weapon[index];
          WeaponParam weaponParam = new WeaponParam();
          this.mWeaponParam.Add(weaponParam);
          weaponParam.Deserialize(json9);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        if (this.mRecipeDictionary == null)
          this.mRecipeDictionary = new Dictionary<string, RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          JSON_RecipeParam json10 = json.Recipe[index];
          RecipeParam recipeParam = new RecipeParam();
          this.mRecipeParam.Add(recipeParam);
          recipeParam.Deserialize(json10);
          if (!this.mRecipeDictionary.ContainsKey(json10.iname))
            this.mRecipeDictionary.Add(json10.iname, recipeParam);
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          JSON_JobParam json11 = json.Job[index];
          JobParam jobParam = new JobParam();
          this.mJobParam.Add(jobParam);
          this.mJobParamDict[json11.iname] = jobParam;
          jobParam.Deserialize(json11, this);
        }
        JobParam.UpdateCache(this.mJobParam);
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, JobSetParam>(json.JobSet.Length);
        if (this.mJobsetTargetDictionary == null)
          this.mJobsetTargetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          JSON_JobSetParam job = json.JobSet[index];
          JobSetParam jobSetParam = new JobSetParam();
          this.mJobSetParam.Add(jobSetParam);
          jobSetParam.Deserialize(job);
          if (!this.mJobsetDictionary.ContainsKey(job.iname))
            this.mJobsetDictionary.Add(job.iname, jobSetParam);
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> cache;
            if (this.mJobsetTargetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              cache = this.mJobsetTargetDictionary[jobSetParam.target_unit];
            }
            else
            {
              cache = new List<JobSetParam>(3);
              this.mJobsetTargetDictionary.Add(jobSetParam.target_unit, cache);
              this.GetUnitParam(jobSetParam.target_unit)?.SetClassChangeJobsetCache(cache);
            }
            cache.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          JSON_GrowParam json12 = json.Grow[index];
          GrowParam growParam = new GrowParam();
          this.mGrowParam.Add(growParam);
          growParam.Deserialize(json12);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          JSON_AIParam json13 = json.AI[index];
          AIParam aiParam = new AIParam();
          this.mAIParam.Add(aiParam);
          aiParam.Deserialize(json13);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          JSON_GeoParam json14 = json.Geo[index];
          GeoParam geoParam = new GeoParam();
          this.mGeoParam.Add(geoParam);
          geoParam.Deserialize(json14);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam = new RarityParam();
          this.mRarityParam.Add(rarityParam);
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam = new ShopParam();
          this.mShopParam.Add(shopParam);
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json15 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json15);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary1 = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary1.ContainsKey(trophyCategoryParam.hash_code))
              dictionary1.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null && dictionary1.Count > 0)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Trophy[index]))
          {
            if (dictionary1.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary1[trophyParam.category_hash_code];
            trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
        {
          if (!this.mTrophyInameDict.ContainsKey(trophyParam.iname))
            this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
        }
      }
      Dictionary<string, ChallengeCategoryParam> dictionary2 = new Dictionary<string, ChallengeCategoryParam>();
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
          {
            dictionary2[challengeCategoryParam.iname] = challengeCategoryParam;
            challengeCategoryParamList.Add(challengeCategoryParam);
          }
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Challenge[index]))
          {
            if (dictionary2.ContainsKey(trophyParam.Category))
              trophyParam.ChallengeCategoryParam = dictionary2[trophyParam.Category];
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
        {
          if (!this.mTrophyInameDict.ContainsKey(trophyParam.iname))
            this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
        }
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Premium != null)
      {
        List<PremiumParam> premiumParamList = new List<PremiumParam>(json.Premium.Length);
        for (int index = 0; index < json.Premium.Length; ++index)
        {
          PremiumParam premiumParam = new PremiumParam();
          if (premiumParam.Deserialize(json.Premium[index]))
            premiumParamList.Add(premiumParam);
        }
        this.mPremium = premiumParamList.ToArray();
      }
      if (json.BuyCoinProduct != null)
      {
        this.mBuyCoinProductParam = new List<BuyCoinProductParam>(json.BuyCoinProduct.Length);
        for (int index = 0; index < json.BuyCoinProduct.Length; ++index)
        {
          BuyCoinProductParam coinProductParam = new BuyCoinProductParam();
          if (coinProductParam.Deserialize(json.BuyCoinProduct[index]))
            this.mBuyCoinProductParam.Add(coinProductParam);
        }
      }
      if (json.BuyCoinShop != null)
      {
        this.mBuyCoinShopParam = new List<BuyCoinShopParam>(json.BuyCoinShop.Length);
        for (int index = 0; index < json.BuyCoinShop.Length; ++index)
        {
          BuyCoinShopParam buyCoinShopParam = new BuyCoinShopParam();
          if (buyCoinShopParam.Deserialize(json.BuyCoinShop[index]))
            this.mBuyCoinShopParam.Add(buyCoinShopParam);
        }
      }
      if (json.BuyCoinReward != null)
      {
        this.mBuyCoinRewardParam = new List<BuyCoinRewardParam>(json.BuyCoinReward.Length);
        for (int index = 0; index < json.BuyCoinReward.Length; ++index)
        {
          BuyCoinRewardParam buyCoinRewardParam = new BuyCoinRewardParam();
          if (buyCoinRewardParam.Deserialize(json.BuyCoinReward[index]))
            this.mBuyCoinRewardParam.Add(buyCoinRewardParam);
        }
      }
      if (json.BuyCoinProductConvert != null)
      {
        this.mBuyCoinProductConvertParam = new List<BuyCoinProductConvertParam>(json.BuyCoinProductConvert.Length);
        for (int index = 0; index < json.BuyCoinProductConvert.Length; ++index)
        {
          BuyCoinProductConvertParam productConvertParam = new BuyCoinProductConvertParam();
          if (productConvertParam.Deserialize(json.BuyCoinProductConvert[index]))
            this.mBuyCoinProductConvertParam.Add(productConvertParam);
        }
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          JSON_AwardParam json16 = json.Award[index];
          AwardParam awardParam = new AwardParam();
          this.mAwardParam.Add(awardParam);
          awardParam.Deserialize(json16);
          if (!this.mAwardDictionary.ContainsKey(awardParam.iname))
            this.mAwardDictionary.Add(awardParam.iname, awardParam);
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      this.mArenaRewardParams = new List<ArenaRewardParam>();
      ArenaRewardParam.Deserialize(ref this.mArenaRewardParams, json.ArenaRankResult);
      if (json.TowerScore != null)
      {
        this.mTowerScores = new Dictionary<string, TowerScoreParam[]>(json.TowerScore.Length);
        for (int index1 = 0; index1 < json.TowerScore.Length; ++index1)
        {
          JSON_TowerScore jsonTowerScore = json.TowerScore[index1];
          int length = jsonTowerScore.threshold_vals.Length;
          TowerScoreParam[] towerScoreParamArray = new TowerScoreParam[length];
          for (int index2 = 0; index2 < length; ++index2)
          {
            JSON_TowerScoreThreshold thresholdVal = jsonTowerScore.threshold_vals[index2];
            towerScoreParamArray[index2] = new TowerScoreParam();
            towerScoreParamArray[index2].Deserialize(thresholdVal);
          }
          if (!this.mTowerScores.ContainsKey(jsonTowerScore.iname))
            this.mTowerScores.Add(jsonTowerScore.iname, towerScoreParamArray);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(json.FriendPresentItem[index]);
          if (!this.mFriendPresentItemParam.ContainsKey(presentItemParam.iname))
            this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      if (json.UnitUnlockTime != null)
      {
        this.mUnitUnlockTimeParam = new Dictionary<string, UnitUnlockTimeParam>();
        for (int index = 0; index < json.UnitUnlockTime.Length; ++index)
        {
          UnitUnlockTimeParam unitUnlockTimeParam = new UnitUnlockTimeParam();
          unitUnlockTimeParam.Deserialize(json.UnitUnlockTime[index]);
          if (!this.mUnitUnlockTimeParam.ContainsKey(unitUnlockTimeParam.iname))
            this.mUnitUnlockTimeParam.Add(unitUnlockTimeParam.iname, unitUnlockTimeParam);
        }
      }
      if (json.Tobira != null)
      {
        for (int index = 0; index < json.Tobira.Length; ++index)
        {
          TobiraParam tobiraParam = new TobiraParam();
          tobiraParam.Deserialize(json.Tobira[index]);
          this.mTobiraParam.Add(tobiraParam);
        }
      }
      if (json.TobiraCategories != null)
      {
        for (int index = 0; index < json.TobiraCategories.Length; ++index)
        {
          TobiraCategoriesParam tobiraCategoriesParam = new TobiraCategoriesParam();
          tobiraCategoriesParam.Deserialize(json.TobiraCategories[index]);
          if (!this.mTobiraCategoriesParam.ContainsKey(tobiraCategoriesParam.TobiraCategory))
            this.mTobiraCategoriesParam.Add(tobiraCategoriesParam.TobiraCategory, tobiraCategoriesParam);
        }
      }
      if (json.TobiraConds != null)
      {
        for (int index = 0; index < json.TobiraConds.Length; ++index)
        {
          TobiraCondsParam tobiraCondsParam = new TobiraCondsParam();
          tobiraCondsParam.Deserialize(json.TobiraConds[index]);
          this.mTobiraCondParam.Add(tobiraCondsParam);
        }
      }
      if (json.TobiraCondsUnit != null)
      {
        for (int index = 0; index < json.TobiraCondsUnit.Length; ++index)
        {
          TobiraCondsUnitParam tobiraCondsUnitParam = new TobiraCondsUnitParam();
          tobiraCondsUnitParam.Deserialize(json.TobiraCondsUnit[index]);
          if (!this.mTobiraCondUnitParam.ContainsKey(tobiraCondsUnitParam.Id))
            this.mTobiraCondUnitParam.Add(tobiraCondsUnitParam.Id, tobiraCondsUnitParam);
        }
      }
      if (json.TobiraRecipe != null)
      {
        for (int index = 0; index < json.TobiraRecipe.Length; ++index)
        {
          TobiraRecipeParam tobiraRecipeParam = new TobiraRecipeParam();
          tobiraRecipeParam.Deserialize(json.TobiraRecipe[index]);
          this.mTobiraRecipeParam.Add(tobiraRecipeParam);
        }
      }
      if (json.ConceptCard != null)
      {
        this.mConceptCard = new List<ConceptCardParam>();
        this.mConceptCardDict = new Dictionary<string, ConceptCardParam>();
        for (int index = 0; index < json.ConceptCard.Length; ++index)
        {
          ConceptCardParam conceptCardParam = new ConceptCardParam();
          conceptCardParam.Deserialize(json.ConceptCard[index]);
          this.mConceptCard.Add(conceptCardParam);
          if (!this.mConceptCardDict.ContainsKey(conceptCardParam.iname))
            this.mConceptCardDict.Add(conceptCardParam.iname, conceptCardParam);
        }
      }
      if (json.ConceptCardGroup != null && json.ConceptCardGroup.Length > 0)
      {
        this.mConceptCardGroup = (Dictionary<string, List<ConceptCardParam>>) null;
        ConceptCardGroupParam.Deserialize(json.ConceptCardGroup, this.mConceptCardDict, ref this.mConceptCardGroup);
      }
      if (json.ConceptLimitUpItem != null && json.ConceptLimitUpItem.Length > 0)
        ConceptLimitUpItemParam.Desirialize(json.ConceptLimitUpItem, this.mConceptCardDict, this.mConceptCardGroup);
      int[][] numArray = new int[6][]
      {
        json.ConceptCardLvTbl1,
        json.ConceptCardLvTbl2,
        json.ConceptCardLvTbl3,
        json.ConceptCardLvTbl4,
        json.ConceptCardLvTbl5,
        json.ConceptCardLvTbl6
      };
      if (0 < numArray.Length && numArray[0] != null && 0 < numArray[0].Length)
      {
        this.mConceptCardLvTbl = new OInt[numArray.Length, numArray[0].Length];
        for (int index3 = 0; index3 < numArray.Length; ++index3)
        {
          for (int index4 = 0; index4 < numArray[index3].Length; ++index4)
            this.mConceptCardLvTbl[index3, index4] = (OInt) numArray[index3][index4];
        }
      }
      if (json.ConceptCardConditions != null)
      {
        this.mConceptCardConditions = new Dictionary<string, ConceptCardConditionsParam>();
        for (int index = 0; index < json.ConceptCardConditions.Length; ++index)
        {
          ConceptCardConditionsParam cardConditionsParam = new ConceptCardConditionsParam();
          cardConditionsParam.Deserialize(json.ConceptCardConditions[index]);
          if (!this.mConceptCardConditions.ContainsKey(cardConditionsParam.iname))
            this.mConceptCardConditions.Add(cardConditionsParam.iname, cardConditionsParam);
        }
      }
      if (json.ConceptCardTrustReward != null)
      {
        this.mConceptCardTrustReward = new Dictionary<string, ConceptCardTrustRewardParam>();
        for (int index = 0; index < json.ConceptCardTrustReward.Length; ++index)
        {
          ConceptCardTrustRewardParam trustRewardParam = new ConceptCardTrustRewardParam();
          trustRewardParam.Deserialize(json.ConceptCardTrustReward[index]);
          if (!this.mConceptCardTrustReward.ContainsKey(trustRewardParam.iname))
            this.mConceptCardTrustReward.Add(trustRewardParam.iname, trustRewardParam);
        }
      }
      if (json.ConceptCardSellCoinRate != null)
      {
        this.mConceptCardSellCoinRate = new OInt[json.ConceptCardSellCoinRate.Length];
        for (int index = 0; index < json.ConceptCardSellCoinRate.Length; ++index)
          this.mConceptCardSellCoinRate[index] = (OInt) json.ConceptCardSellCoinRate[index];
      }
      ConceptCardLsBuffCoefParam.Deserialize(ref this.mConceptCardLsBuffCoefParam, json.ConceptCardLsBuffCoef);
      if (json.UnitGroup != null)
      {
        this.mUnitGroup = new Dictionary<string, UnitGroupParam>();
        for (int index = 0; index < json.UnitGroup.Length; ++index)
        {
          UnitGroupParam unitGroupParam = new UnitGroupParam();
          unitGroupParam.Deserialize(json.UnitGroup[index]);
          if (!this.mUnitGroup.ContainsKey(unitGroupParam.iname))
            this.mUnitGroup.Add(unitGroupParam.iname, unitGroupParam);
        }
      }
      if (json.JobGroup != null)
      {
        this.mJobGroup = new Dictionary<string, JobGroupParam>();
        for (int index = 0; index < json.JobGroup.Length; ++index)
        {
          JobGroupParam jobGroupParam = new JobGroupParam();
          jobGroupParam.Deserialize(json.JobGroup[index]);
          if (!this.mJobGroup.ContainsKey(jobGroupParam.iname))
            this.mJobGroup.Add(jobGroupParam.iname, jobGroupParam);
        }
      }
      if (json.StatusCoefficient != null && json.StatusCoefficient.Length > 0)
      {
        this.mStatusCoefficient = new StatusCoefficientParam();
        this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0]);
      }
      if (json.CustomTarget != null)
      {
        this.mCustomTarget = new Dictionary<string, CustomTargetParam>();
        for (int index = 0; index < json.CustomTarget.Length; ++index)
        {
          CustomTargetParam customTargetParam = new CustomTargetParam();
          customTargetParam.Deserialize(json.CustomTarget[index]);
          if (!this.mCustomTarget.ContainsKey(customTargetParam.iname))
            this.mCustomTarget.Add(customTargetParam.iname, customTargetParam);
        }
      }
      if (json.SkillAbilityDerive != null && json.SkillAbilityDerive.Length > 0)
      {
        this.mSkillAbilityDeriveParam = new SkillAbilityDeriveParam[json.SkillAbilityDerive.Length];
        for (int index = 0; index < json.SkillAbilityDerive.Length; ++index)
        {
          this.mSkillAbilityDeriveParam[index] = new SkillAbilityDeriveParam(index);
          this.mSkillAbilityDeriveParam[index].Deserialize(json.SkillAbilityDerive[index], this);
        }
        for (int index = 0; index < this.mSkillAbilityDeriveParam.Length; ++index)
        {
          SkillAbilityDeriveData abilityDeriveData = new SkillAbilityDeriveData();
          List<SkillAbilityDeriveParam> abilityDeriveParam = this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam[index]);
          abilityDeriveData.Setup(this.mSkillAbilityDeriveParam[index], abilityDeriveParam);
          this.mSkillAbilityDerives.Add(abilityDeriveData);
        }
      }
      if (json.Tips != null && json.Tips.Length > 0)
      {
        this.mTipsParam = new TipsParam[json.Tips.Length];
        for (int index = 0; index < json.Tips.Length; ++index)
        {
          this.mTipsParam[index] = new TipsParam();
          this.mTipsParam[index].Deserialize(json.Tips[index]);
        }
      }
      if (json.GuildEmblem != null)
      {
        if (this.mGuildEmblemParam == null)
          this.mGuildEmblemParam = new List<GuildEmblemParam>(json.GuildEmblem.Length);
        if (this.mGuildEmblemDictionary == null)
          this.mGuildEmblemDictionary = new Dictionary<string, GuildEmblemParam>(json.GuildEmblem.Length);
        for (int index = 0; index < json.GuildEmblem.Length; ++index)
        {
          JSON_GuildEmblemParam json17 = json.GuildEmblem[index];
          if (!string.IsNullOrEmpty(json17.iname))
          {
            GuildEmblemParam guildEmblemParam = new GuildEmblemParam();
            this.mGuildEmblemParam.Add(guildEmblemParam);
            guildEmblemParam.Deserialize(json17);
            if (!this.mGuildEmblemDictionary.ContainsKey(guildEmblemParam.Iname))
              this.mGuildEmblemDictionary.Add(guildEmblemParam.Iname, guildEmblemParam);
          }
        }
      }
      if (json.GuildFacility != null)
      {
        if (this.mGuildFacilityParam == null)
          this.mGuildFacilityParam = new List<GuildFacilityParam>(json.GuildFacility.Length);
        if (this.mGuildFacilityDictionary == null)
          this.mGuildFacilityDictionary = new Dictionary<string, GuildFacilityParam>(json.GuildFacility.Length);
        for (int index = 0; index < json.GuildFacility.Length; ++index)
        {
          JSON_GuildFacilityParam json18 = json.GuildFacility[index];
          if (!string.IsNullOrEmpty(json18.iname))
          {
            GuildFacilityParam guildFacilityParam = new GuildFacilityParam();
            this.mGuildFacilityParam.Add(guildFacilityParam);
            guildFacilityParam.Deserialize(json18);
            if (!this.mGuildFacilityDictionary.ContainsKey(guildFacilityParam.Iname))
              this.mGuildFacilityDictionary.Add(guildFacilityParam.Iname, guildFacilityParam);
          }
        }
      }
      if (json.GuildFacilityLvTbl != null)
      {
        if (this.mGuildFacilityLvParam == null)
          this.mGuildFacilityLvParam = new GuildFacilityLvParam[json.GuildFacilityLvTbl.Length];
        for (int index = 0; index < json.GuildFacilityLvTbl.Length; ++index)
        {
          GuildFacilityLvParam guildFacilityLvParam = new GuildFacilityLvParam();
          guildFacilityLvParam.Deserialize(json.GuildFacilityLvTbl[index]);
          this.mGuildFacilityLvParam[index] = guildFacilityLvParam;
        }
      }
      if (json.DynamicTransformUnit != null)
      {
        List<DynamicTransformUnitParam> transformUnitParamList = new List<DynamicTransformUnitParam>(json.DynamicTransformUnit.Length);
        for (int index = 0; index < json.DynamicTransformUnit.Length; ++index)
        {
          DynamicTransformUnitParam transformUnitParam = new DynamicTransformUnitParam();
          transformUnitParam.Deserialize(json.DynamicTransformUnit[index]);
          transformUnitParamList.Add(transformUnitParam);
        }
        this.mDynamicTransformUnitParam = transformUnitParamList;
      }
      if (json.ConvertUnitPieceExclude != null && json.ConvertUnitPieceExclude.Length > 0)
      {
        this.mConvertUnitPieceExcludeParam = new ConvertUnitPieceExcludeParam[json.ConvertUnitPieceExclude.Length];
        for (int index = 0; index < json.ConvertUnitPieceExclude.Length; ++index)
        {
          this.mConvertUnitPieceExcludeParam[index] = new ConvertUnitPieceExcludeParam();
          this.mConvertUnitPieceExcludeParam[index].Deserialize(json.ConvertUnitPieceExclude[index]);
        }
      }
      if (json.RecommendedArtifact != null && json.RecommendedArtifact.Length > 0)
      {
        this.mRecommendedArtifactList = new RecommendedArtifactList();
        this.mRecommendedArtifactList.Deserialize(json.RecommendedArtifact);
      }
      RaidMaster.Deserialize<RaidPeriodParam, JSON_RaidPeriodParam>(ref this.mRaidPeriodParam, json.RaidPeriod);
      RaidMaster.Deserialize<RaidPeriodTimeParam, JSON_RaidPeriodTimeParam>(ref this.mRaidPeriodTimeParam, json.RaidPeriodTime);
      RaidMaster.Deserialize<RaidAreaParam, JSON_RaidAreaParam>(ref this.mRaidAreaParam, json.RaidArea);
      RaidMaster.Deserialize<RaidBossParam, JSON_RaidBossParam>(ref this.mRaidBossParam, json.RaidBoss);
      RaidMaster.Deserialize<RaidBattleRewardParam, JSON_RaidBattleRewardParam>(ref this.mRaidBattleRewardParam, json.RaidBattleReward);
      RaidMaster.Deserialize<RaidBeatRewardParam, JSON_RaidBeatRewardParam>(ref this.mRaidBeatRewardParam, json.RaidBeatReward);
      RaidMaster.Deserialize<RaidDamageRatioRewardParam, JSON_RaidDamageRatioRewardParam>(ref this.mRaidDamageRatioRewardParam, json.RaidDamageRatioReward);
      RaidMaster.Deserialize<RaidDamageAmountRewardParam, JSON_RaidDamageAmountRewardParam>(ref this.mRaidDamageAmountRewardParam, json.RaidDamageAmountReward);
      RaidMaster.Deserialize<RaidAreaClearRewardParam, JSON_RaidAreaClearRewardParam>(ref this.mRaidAreaClearRewardParam, json.RaidAreaClearReward);
      RaidMaster.Deserialize<RaidCompleteRewardParam, JSON_RaidCompleteRewardParam>(ref this.mRaidCompleteRewardParam, json.RaidCompleteReward);
      RaidMaster.Deserialize<RaidRewardParam, JSON_RaidRewardParam>(ref this.mRaidRewardParam, json.RaidReward);
      if (json.SkillMotion != null)
      {
        List<SkillMotionParam> skillMotionParamList = new List<SkillMotionParam>(json.SkillMotion.Length);
        for (int index = 0; index < json.SkillMotion.Length; ++index)
        {
          SkillMotionParam skillMotionParam = new SkillMotionParam();
          skillMotionParam.Deserialize(json.SkillMotion[index]);
          skillMotionParamList.Add(skillMotionParam);
        }
        this.mSkillMotionParam = skillMotionParamList;
      }
      DependStateSpcEffParam.Deserialize(ref this.mDependStateSpcEffParam, json.DependStateSpcEff);
      Dictionary<string, HighlightGift> dictionary3 = new Dictionary<string, HighlightGift>();
      if (json.HighlightGift != null)
      {
        foreach (JSON_HighlightGift json19 in json.HighlightGift)
        {
          HighlightGift highlightGift = new HighlightGift();
          highlightGift.Deserialize(json19);
          dictionary3[highlightGift.iname] = highlightGift;
        }
      }
      if (json.Highlight != null)
      {
        SRPG.HighlightParam[] highlightParamArray = new SRPG.HighlightParam[json.Highlight.Length];
        for (int index = 0; index < json.Highlight.Length; ++index)
        {
          SRPG.HighlightParam highlightParam = new SRPG.HighlightParam();
          highlightParam.Deserialze(json.Highlight[index]);
          HighlightGift highlightGift;
          if (dictionary3.TryGetValue(json.Highlight[index].gift, out highlightGift))
            highlightParam.gift = highlightGift;
          highlightParamArray[index] = highlightParam;
        }
        this.mHighlightParam = highlightParamArray;
      }
      InspSkillParam.Deserialize(json.InspirationSkill, json.InspSkillTrigger, ref this.mInspSkillDictionary);
      InspSkillLvUpCostParam.Desirialize(json.InspSkillLvUpCost, ref this.mInspSkillLvUpCostParam);
      InspSkillCostParam.Deserialize(json.InspSkillOpenCost, ref this.mInspSkillOpenCostDictionary);
      InspSkillCostParam.Deserialize(json.InspSkillResetCost, ref this.mInspSkillResetCostDictionary);
      GenesisParam.Deserialize(ref this.mGenesisParam, json.Genesis);
      CoinBuyUseBonusRewardParam.Deserialize(ref this.mCoinBuyUseBonusRewardParam, json.CoinBuyUseBonusReward);
      CoinBuyUseBonusRewardSetParam.Deserialize(ref this.mCoinBuyUseBonusRewardSetParam, json.CoinBuyUseBonusRewardSet, this.mCoinBuyUseBonusRewardParam);
      CoinBuyUseBonusParam.Deserialize(ref this.mCoinBuyUseBonusParam, json.CoinBuyUseBonus, this.mCoinBuyUseBonusRewardSetParam);
      UnitRentalNotificationParam.Deserialize(ref this.mUnitRentalNotificationParams, json.UnitRentalNotification);
      UnitRentalParam.Deserialize(json.UnitRental, ref this.mUnitRentalParams);
      DrawCardRewardParam.Deserialize(ref this.mDrawCardRewardDict, json.DrawCardReward);
      DrawCardParam.Deserialize(ref this.mDrawCardDict, json.DrawCard);
      TrophyStarMissionRewardParam.Deserialize(json.TrophyStarMissionReward, ref this.mTrophyStarMissionRewardDict);
      TrophyStarMissionParam.Deserialize(json.TrophyStarMission, ref this.mTrophyStarMissionDict);
      UnitPieceShopParam.Deserialize(ref this.mUnitPieceShop, json.UnitPieceShop);
      UnitPieceShopGroupParam.Deserialize(ref this.mUnitPieceShopGroup, json.UnitPieceShopGroup);
      TwitterMessageParam.Deserialize(ref this.mTwitterMessageParams, json.TwitterMessage);
      FilterConceptCardParam.Deserialize(ref this.mFilterConceptCardParams, json.FilterConceptCard);
      FilterRuneParam.Deserialize(ref this.mFilterRuneParams, json.FilterRune);
      FilterUnitParam.Deserialize(ref this.mFilterUnitParams, json.FilterUnit);
      FilterArtifactParam.Deserialize(json.FilterArtifact, ref this.mFilterArtifactParams);
      SortRuneParam.Deserialize(ref this.mSortRuneParams, json.SortRune);
      JukeBoxParam.Deserialize(ref this.mJukeBoxParams, json.JukeBox);
      JukeBoxSectionParam.Deserialize(ref this.mJukeBoxSectionParams, json.JukeBoxSection);
      UnitSameGroupParam.Deserialize(ref this.mUnitSameGroup, json.UnitSameGroup);
      RuneMasterParam.Deserialize(json.RuneCost, ref this.mRuneCost);
      RuneMasterParam.Deserialize(json.RuneMaterial, ref this.mRuneMaterial, this);
      RuneMasterParam.Deserialize(json.RuneSetEff, ref this.mRuneSetEff);
      RuneMasterParam.Deserialize(json.RuneLotteryBaseState, ref this.mRuneLotteryBaseState);
      RuneMasterParam.Deserialize(json.RuneLotteryEvoState, ref this.mRuneLotteryEvoState);
      RuneMasterParam.Deserialize(json.Rune, ref this.mRuneParam, ref this.mRuneParamDict);
      AutoRepeatQuestBoxParam.Deserialize(ref this.mAutoRepeatQuestBoxParams, json.AutoRepeatQuestBox);
      GuildAttendParam.Deserialize(ref this.mGuildAttendParams, json.GuildAttend);
      GuildAttendRewardParam.Deserialize(ref this.mGuildAttendRewardParams, json.GuildAttendReward);
      GuildRoleBonusParam.Deserialize(ref this.mGuildRoleBonusParams, json.GuildRoleBonus);
      GuildRoleBonusRewardParam.Deserialize(ref this.mGuildRoleBonusRewardParams, json.GuildRoleBonusReward);
      ResetCostParam.Deserialize(ref this.mResetCostParams, json.ResetCost);
      ProtectSkillParam.Deserialize(ref this.mProtectSkillParams, json.ProtectSkill);
      GuildSearchFilterParam.Deserialize(ref this.mGuildSeartchFilterParams, json.GuildSearchFilter);
      ReplaceSprite.Deserialize(ref this.mRepraseSprite, json.ReplaceSprite);
      ExpansionPurchaseParam.Deserialize(ref this.mExpansionPurchaseParams, json.ExpansionPurchase);
      ExpansionPurchaseQuestParam.Deserialize(ref this.mExpansionPurchaseQuestParams, json.ExpansionPurchaseQuest);
      SkillAdditionalParam.Deserialize(ref this.mSkillAdditionalList, json.SkillAdditional);
      SkillAntiShieldParam.Deserialize(ref this.mSkillAntiShieldList, json.SkillAntiShield);
      GuildTrophyMasterParam.Deserialize(ref this.mGuildTrophy, ref this.mGuildTrophyCategory, ref this.mGuildTrophyInameDict, json.GuildTrophy, json.GuildTrophyCategory);
      this.Loaded = true;
      return true;
    }

    public ItemParam GetCommonEquip(ItemParam item_param, bool is_soul)
    {
      if (!is_soul)
        return !item_param.IsCommon ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.EquipCmn[(int) item_param.cmn_type - 1]);
      int rare = item_param.rare;
      return this.FixParam.SoulCommonPiece == null || this.FixParam.SoulCommonPiece.Length <= rare ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.SoulCommonPiece[rare]);
    }

    public bool IsFriendPresentItemParamValid()
    {
      return this.mFriendPresentItemParam != null && this.mFriendPresentItemParam.Count > 1;
    }

    public FriendPresentItemParam[] GetFriendPresentItemParams()
    {
      if (this.mFriendPresentItemParam == null)
        return new FriendPresentItemParam[0];
      FriendPresentItemParam[] array = new FriendPresentItemParam[this.mFriendPresentItemParam.Values.Count];
      this.mFriendPresentItemParam.Values.CopyTo(array, 0);
      return array;
    }

    public FriendPresentItemParam GetFriendPresentItemParam(string key)
    {
      if (this.mFriendPresentItemParam == null)
        return (FriendPresentItemParam) null;
      if (string.IsNullOrEmpty(key))
        return (FriendPresentItemParam) null;
      FriendPresentItemParam presentItemParam = (FriendPresentItemParam) null;
      if (!this.mFriendPresentItemParam.TryGetValue(key, out presentItemParam))
        DebugUtility.LogError("存在しないフレンドプレゼントアイテムパラメータを参照しています > " + key);
      return presentItemParam;
    }

    public void MakeMapEffectHaveJobLists()
    {
      if (this.mJobParam == null || MapEffectParam.IsMakeHaveJobLists())
        return;
      MapEffectParam.MakeHaveJobLists();
      foreach (JobParam job_param in this.mJobParam)
      {
        if (!string.IsNullOrEmpty(job_param.MapEffectAbility) && job_param.IsMapEffectRevReso)
        {
          AbilityParam abilityParam = this.GetAbilityParam(job_param.MapEffectAbility);
          if (abilityParam != null)
          {
            foreach (LearningSkill skill in abilityParam.skills)
              MapEffectParam.AddHaveJob(skill.iname, job_param);
          }
        }
      }
    }

    public List<ConceptCardParam> GetConceptCardGroup(string iname)
    {
      if (this.mConceptCardGroup != null && this.mConceptCardGroup.ContainsKey(iname))
        return this.mConceptCardGroup[iname];
      Debug.LogWarning((object) "存在しない真理念装グループ識別子が設定されている");
      return (List<ConceptCardParam>) null;
    }

    public bool CheckConceptCardgroup(string group_iname, ConceptCardParam concept_card_param)
    {
      List<ConceptCardParam> conceptCardGroup = this.GetConceptCardGroup(group_iname);
      return conceptCardGroup != null && conceptCardGroup.FindIndex((Predicate<ConceptCardParam>) (param => param == concept_card_param)) >= 0;
    }

    public UnitUnlockTimeParam[] GetUnitUnlockTimeParams()
    {
      if (this.mUnitUnlockTimeParam == null)
        return (UnitUnlockTimeParam[]) null;
      UnitUnlockTimeParam[] array = new UnitUnlockTimeParam[this.mUnitUnlockTimeParam.Values.Count];
      this.mUnitUnlockTimeParam.Values.CopyTo(array, 0);
      return array;
    }

    public UnitUnlockTimeParam GetUnitUnlockTimeParam(string _key)
    {
      if (string.IsNullOrEmpty(_key))
        return (UnitUnlockTimeParam) null;
      if (this.mUnitUnlockTimeParam == null)
        return (UnitUnlockTimeParam) null;
      UnitUnlockTimeParam unitUnlockTimeParam = (UnitUnlockTimeParam) null;
      return !this.mUnitUnlockTimeParam.TryGetValue(_key, out unitUnlockTimeParam) ? (UnitUnlockTimeParam) null : unitUnlockTimeParam;
    }

    public bool IsUnlockableUnit(string _key, DateTime _time)
    {
      if (string.IsNullOrEmpty(_key))
        return true;
      UnitUnlockTimeParam unitUnlockTimeParam = (UnitUnlockTimeParam) null;
      return !this.mUnitUnlockTimeParam.TryGetValue(_key, out unitUnlockTimeParam) || unitUnlockTimeParam.begin_at.CompareTo(_time) <= 0 && unitUnlockTimeParam.end_at.CompareTo(_time) >= 0;
    }

    public ConceptCardParam GetConceptCardParam(string iname)
    {
      return !this.mConceptCardDict.ContainsKey(iname) ? (ConceptCardParam) null : this.mConceptCardDict[iname];
    }

    public int GetConceptCardNextExp(int rarity, int lv)
    {
      return (int) this.mConceptCardLvTbl[rarity, lv - 1];
    }

    public int GetConceptCardLevelExp(int rarity, int lv)
    {
      int conceptCardLevelExp = 0;
      for (int index = 0; index < lv; ++index)
        conceptCardLevelExp += (int) this.mConceptCardLvTbl[rarity, index];
      return conceptCardLevelExp;
    }

    public int CalcConceptCardLevel(int rarity, int totalExp, int levelCap)
    {
      int val2 = levelCap;
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < val2; ++index)
      {
        num += this.GetConceptCardNextExp(rarity, index + 1);
        if (num <= totalExp)
          ++val1;
        else
          break;
      }
      return Math.Min(Math.Max(val1, 1), val2);
    }

    public ConceptCardConditionsParam GetConceptCardConditions(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ConceptCardConditionsParam) null;
      return !this.mConceptCardConditions.ContainsKey(iname) ? (ConceptCardConditionsParam) null : this.mConceptCardConditions[iname];
    }

    public int FindUnitConceptCardConditions(UnitData unit, ConceptCardData card, bool allcheck = false)
    {
      int conceptCardConditions1 = 0;
      if (unit == null || card == null)
        return conceptCardConditions1;
      JobData currentJob = unit.CurrentJob;
      if (card.Param.effects.Length <= 0)
        return 0;
      for (int index = 0; index < card.Param.effects.Length; ++index)
      {
        if (!string.IsNullOrEmpty(card.Param.effects[index].card_skill))
        {
          ConceptCardConditionsParam conceptCardConditions2 = this.GetConceptCardConditions(card.Param.effects[index].cnds_iname);
          if (conceptCardConditions2 != null)
          {
            if (allcheck && currentJob != null && conceptCardConditions2.IsMatchConditions(unit.UnitParam, currentJob) && (conceptCardConditions1 == 0 || conceptCardConditions1 != 0 && conceptCardConditions1 > (int) byte.MaxValue))
              conceptCardConditions1 = (int) byte.MaxValue;
            UnitGroupParam unitGroup = this.GetUnitGroup(conceptCardConditions2.unit_group);
            if (unitGroup != null && unitGroup.IsInGroup(unit.UnitParam.iname) && (conceptCardConditions1 == 0 || conceptCardConditions1 != 0 && conceptCardConditions1 > unitGroup.units.Length))
              conceptCardConditions1 = unitGroup.units.Length;
          }
        }
      }
      return conceptCardConditions1;
    }

    public int GetConceptCardTrustMax(ConceptCardData data)
    {
      return data == null ? (int) this.FixParam.CardTrustMax : (int) this.FixParam.CardTrustMax + (int) this.FixParam.CardTrustMax * (int) data.AwakeCount;
    }

    public UnitGroupParam GetUnitGroup(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (UnitGroupParam) null;
      return !this.mUnitGroup.ContainsKey(iname) ? (UnitGroupParam) null : this.mUnitGroup[iname];
    }

    public JobGroupParam GetJobGroup(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (JobGroupParam) null;
      return !this.mJobGroup.ContainsKey(iname) ? (JobGroupParam) null : this.mJobGroup[iname];
    }

    public ConceptCardTrustRewardParam GetTrustReward(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ConceptCardTrustRewardParam) null;
      return !this.mConceptCardTrustReward.ContainsKey(iname) ? (ConceptCardTrustRewardParam) null : this.mConceptCardTrustReward[iname];
    }

    public CustomTargetParam GetCustomTarget(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (CustomTargetParam) null;
      return !this.mCustomTarget.ContainsKey(iname) ? (CustomTargetParam) null : this.mCustomTarget[iname];
    }

    public List<ConceptCardParam> GetConceptCardParams() => this.mConceptCard;

    public List<SkillAbilityDeriveParam> FindSkillAbilityDeriveParamWithArtifact(
      string artifactIname)
    {
      List<SkillAbilityDeriveParam> paramWithArtifact = new List<SkillAbilityDeriveParam>();
      if (this.mSkillAbilityDeriveParam == null)
        return paramWithArtifact;
      for (int index = 0; index < this.mSkillAbilityDeriveParam.Length; ++index)
      {
        if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerIname(ESkillAbilityDeriveConds.EquipArtifact, artifactIname))
          paramWithArtifact.Add(this.mSkillAbilityDeriveParam[index]);
      }
      return paramWithArtifact;
    }

    public List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(
      SkillAbilityDeriveParam skillAbilityDeriveParam,
      ESkillAbilityDeriveConds triggerType,
      string triggerIname)
    {
      List<SkillAbilityDeriveParam> abilityDeriveParam = new List<SkillAbilityDeriveParam>();
      if (this.mSkillAbilityDeriveParam == null)
        return abilityDeriveParam;
      SkillAbilityDeriveTriggerParam[] array = ((IEnumerable<SkillAbilityDeriveTriggerParam>) skillAbilityDeriveParam.deriveTriggers).Where<SkillAbilityDeriveTriggerParam>((Func<SkillAbilityDeriveTriggerParam, bool>) (triggerParam => triggerParam.m_TriggerIname != triggerIname)).ToArray<SkillAbilityDeriveTriggerParam>();
      SkillAbilityDeriveTriggerParam deriveTriggerParam1 = new SkillAbilityDeriveTriggerParam(triggerType, triggerIname);
      for (int index = skillAbilityDeriveParam.m_OriginIndex + 1; index < this.mSkillAbilityDeriveParam.Length; ++index)
      {
        bool flag = false;
        foreach (SkillAbilityDeriveTriggerParam deriveTriggerParam2 in array)
        {
          if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerInames(new SkillAbilityDeriveTriggerParam[2]
          {
            deriveTriggerParam2,
            deriveTriggerParam1
          }))
          {
            abilityDeriveParam.Add(this.mSkillAbilityDeriveParam[index]);
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerInames(new SkillAbilityDeriveTriggerParam[1]
          {
            deriveTriggerParam1
          }))
            abilityDeriveParam.Add(this.mSkillAbilityDeriveParam[index]);
        }
      }
      return abilityDeriveParam;
    }

    public List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(
      SkillAbilityDeriveParam skillAbilityDeriveParam)
    {
      List<SkillAbilityDeriveParam> abilityDeriveParam1 = new List<SkillAbilityDeriveParam>();
      if (skillAbilityDeriveParam == null)
        return abilityDeriveParam1;
      foreach (SkillAbilityDeriveTriggerParam deriveTrigger in skillAbilityDeriveParam.deriveTriggers)
      {
        foreach (SkillAbilityDeriveParam abilityDeriveParam2 in this.FindAditionalSkillAbilityDeriveParam(skillAbilityDeriveParam, deriveTrigger.m_TriggerType, deriveTrigger.m_TriggerIname))
        {
          if (!abilityDeriveParam1.Contains(abilityDeriveParam2))
            abilityDeriveParam1.Add(abilityDeriveParam2);
        }
      }
      return abilityDeriveParam1;
    }

    public Dictionary<string, SkillAbilityDeriveData> CreateSkillAbilityDeriveDataWithArtifacts(
      JobData[] jobData)
    {
      if (jobData == null || jobData.Length < 1)
        return (Dictionary<string, SkillAbilityDeriveData>) null;
      Dictionary<string, SkillAbilityDeriveData> dataWithArtifacts1 = (Dictionary<string, SkillAbilityDeriveData>) null;
      for (int index = 0; index < jobData.Length; ++index)
      {
        if (jobData[index] != null)
        {
          SkillAbilityDeriveData dataWithArtifacts2 = this.CreateSkillAbilityDeriveDataWithArtifacts(((IEnumerable<ArtifactData>) jobData[index].ArtifactDatas).Where<ArtifactData>((Func<ArtifactData, bool>) (artifact => artifact != null)).Select<ArtifactData, string>((Func<ArtifactData, string>) (artifact => artifact.ArtifactParam.iname)).ToArray<string>());
          if (dataWithArtifacts2 != null)
          {
            if (dataWithArtifacts1 == null)
              dataWithArtifacts1 = new Dictionary<string, SkillAbilityDeriveData>();
            dataWithArtifacts1.Add(jobData[index].Param.iname, dataWithArtifacts2);
          }
        }
      }
      return dataWithArtifacts1;
    }

    public List<SkillAbilityDeriveData> FindAllSkillAbilityDeriveDataWithArtifact(
      string artifactIname)
    {
      List<SkillAbilityDeriveData> dataWithArtifact = new List<SkillAbilityDeriveData>();
      foreach (SkillAbilityDeriveData skillAbilityDerive in this.mSkillAbilityDerives)
      {
        if (skillAbilityDerive.CheckContainsTriggerIname(ESkillAbilityDeriveConds.EquipArtifact, artifactIname))
          dataWithArtifact.Add(skillAbilityDerive);
      }
      return dataWithArtifact;
    }

    public bool ExistSkillAbilityDeriveDataWithArtifact(string artifactIname)
    {
      return this.FindAllSkillAbilityDeriveDataWithArtifact(artifactIname).Count > 0;
    }

    public SkillAbilityDeriveData CreateSkillAbilityDeriveDataWithArtifacts(string[] artifactInames)
    {
      if (artifactInames.Length < 1)
        return (SkillAbilityDeriveData) null;
      List<SkillAbilityDeriveData> abilityDeriveDataList = new List<SkillAbilityDeriveData>();
      List<SkillAbilityDeriveTriggerParam[]> combination = SkillAbilityDeriveTriggerParam.CreateCombination(((IEnumerable<string>) artifactInames).Select<string, SkillAbilityDeriveTriggerParam>((Func<string, SkillAbilityDeriveTriggerParam>) (iname => new SkillAbilityDeriveTriggerParam(ESkillAbilityDeriveConds.EquipArtifact, iname))).ToArray<SkillAbilityDeriveTriggerParam>());
      combination.Sort((Comparison<SkillAbilityDeriveTriggerParam[]>) ((triggers1, triggers2) => triggers2.Length.CompareTo(triggers1.Length)));
      foreach (SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam in combination)
      {
        foreach (SkillAbilityDeriveData skillAbilityDerive in this.mSkillAbilityDerives)
        {
          if (skillAbilityDerive.CheckContainsTriggerInames(searchKeyTriggerParam))
            abilityDeriveDataList.Add(skillAbilityDerive);
        }
        if (abilityDeriveDataList.Count > 0)
        {
          if (searchKeyTriggerParam.Length == artifactInames.Length)
            break;
        }
      }
      SkillAbilityDeriveData dataWithArtifacts = (SkillAbilityDeriveData) null;
      if (abilityDeriveDataList.Count > 0)
      {
        dataWithArtifacts = new SkillAbilityDeriveData();
        SkillAbilityDeriveData abilityDeriveData = abilityDeriveDataList[0];
        List<SkillAbilityDeriveParam> additionalSkillAbilityDeriveParams = new List<SkillAbilityDeriveParam>();
        foreach (SkillAbilityDeriveParam abilityDeriveParam in abilityDeriveData.m_AdditionalSkillAbilityDeriveParam)
        {
          if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveParam))
            additionalSkillAbilityDeriveParams.Add(abilityDeriveParam);
        }
        for (int index = 1; index < abilityDeriveDataList.Count; ++index)
        {
          if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveDataList[index].m_SkillAbilityDeriveParam))
            additionalSkillAbilityDeriveParams.Add(abilityDeriveDataList[index].m_SkillAbilityDeriveParam);
          foreach (SkillAbilityDeriveParam abilityDeriveParam in abilityDeriveDataList[index].m_AdditionalSkillAbilityDeriveParam)
          {
            if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveParam))
              additionalSkillAbilityDeriveParams.Add(abilityDeriveParam);
          }
        }
        dataWithArtifacts.Setup(abilityDeriveData.m_SkillAbilityDeriveParam, additionalSkillAbilityDeriveParams);
      }
      return dataWithArtifacts;
    }

    public TowerScoreParam[] FindTowerScoreParam(string score_iname)
    {
      TowerScoreParam[] towerScoreParam = (TowerScoreParam[]) null;
      this.mTowerScores.TryGetValue(score_iname, out towerScoreParam);
      return towerScoreParam;
    }

    public GuildEmblemParam GetGuildEmbleme(string iname)
    {
      return !this.mGuildEmblemDictionary.ContainsKey(iname) ? (GuildEmblemParam) null : this.mGuildEmblemDictionary[iname];
    }

    public GuildEmblemParam[] GetGuildEmblemes() => this.mGuildEmblemParam.ToArray();

    public GuildEmblemParam[] GetNoConditionsGuildEmblemes()
    {
      List<GuildEmblemParam> guildEmblemParamList = new List<GuildEmblemParam>();
      for (int index = 0; index < this.mGuildEmblemParam.Count; ++index)
      {
        if (this.mGuildEmblemParam[index].ConditionsType <= 0)
          guildEmblemParamList.Add(this.mGuildEmblemParam[index]);
      }
      return guildEmblemParamList.ToArray();
    }

    public GuildFacilityParam GetGuildFacility(string iname)
    {
      return !this.mGuildFacilityDictionary.ContainsKey(iname) ? (GuildFacilityParam) null : this.mGuildFacilityDictionary[iname];
    }

    public GuildFacilityParam GetGuildFacilityFromFacilityType(GuildFacilityParam.eFacilityType type)
    {
      if (this.mGuildFacilityDictionary == null)
        return (GuildFacilityParam) null;
      foreach (GuildFacilityParam fromFacilityType in this.mGuildFacilityDictionary.Values)
      {
        if (fromFacilityType.Type == type)
          return fromFacilityType;
      }
      return (GuildFacilityParam) null;
    }

    public GuildFacilityLvParam[] GetGuildFacilityLevelTable() => this.mGuildFacilityLvParam;

    public GuildAttendRewardParam GetGuildAttendRewardParam(string reward_id)
    {
      return this.mGuildAttendRewardParams.Find((Predicate<GuildAttendRewardParam>) (ga => ga.id == reward_id)) ?? new GuildAttendRewardParam();
    }

    public GuildRoleBonusRewardParam GetGuildRoleBonusRewardParam(string reward_id)
    {
      GuildRoleBonusRewardParam bonusRewardParam = this.mGuildRoleBonusRewardParams.Find((Predicate<GuildRoleBonusRewardParam>) (rb => rb.id == reward_id));
      return reward_id == null ? new GuildRoleBonusRewardParam() : bonusRewardParam;
    }

    public RecommendArtifactParams GetRecommendedArtifactParams(
      UnitData unitData,
      bool isMasterOnly,
      int jobIndex = -1)
    {
      if (isMasterOnly)
        return this.mRecommendedArtifactList.GetRecommendedArtifacts(unitData, this);
      RecommendArtifactParams recommendedArtifacts = this.mRecommendedArtifactList.GetRecommendedArtifacts(unitData, this);
      List<ArtifactParam> unitUsableAbility = this.GetArtifactsWithUnitUsableAbility(unitData, jobIndex);
      recommendedArtifacts.AbilityArtifacts.AddRange((IEnumerable<ArtifactParam>) unitUsableAbility);
      return recommendedArtifacts;
    }

    public List<ArtifactParam> GetArtifactsWithUnitUsableAbility(UnitData unitData, int jobIndex)
    {
      List<ArtifactParam> unitUsableAbility = new List<ArtifactParam>();
      if (unitData == null || this.mArtifactParam == null || this.mAbilityParam == null)
        return unitUsableAbility;
      if (jobIndex < 0)
        jobIndex = unitData.JobIndex;
      List<ArtifactParam> mArtifactParam = this.mArtifactParam;
      // ISSUE: reference to a compiler-generated field
      if (MasterParam.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MasterParam.\u003C\u003Ef__mg\u0024cache0 = new Func<ArtifactParam, bool>(ArtifactParam.IsDetachableArtifact);
      }
      // ISSUE: reference to a compiler-generated field
      Func<ArtifactParam, bool> fMgCache0 = MasterParam.\u003C\u003Ef__mg\u0024cache0;
      List<ArtifactParam> list = mArtifactParam.Where<ArtifactParam>(fMgCache0).ToList<ArtifactParam>();
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        ArtifactParam artifactParam = list[index1];
        if (artifactParam.CheckEnableEquip(unitData) && artifactParam.HasAbility)
        {
          bool flag = false;
          for (int index2 = 0; index2 < artifactParam.abil_inames.Length; ++index2)
          {
            AbilityParam abilityParam = this.GetAbilityParam(artifactParam.abil_inames[index2]);
            if (abilityParam.condition_units != null && ((IEnumerable<string>) abilityParam.condition_units).Contains<string>(unitData.UnitParam.iname) && abilityParam.CheckEnableUseAbility(unitData, jobIndex))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            unitUsableAbility.Add(artifactParam);
        }
      }
      return unitUsableAbility;
    }

    public RaidPeriodParam GetActiveRaidPeriod(int begin_offset_hour = 0, int end_offset_hour = 0)
    {
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mRaidPeriodParam.Count; ++index)
      {
        if (this.mRaidPeriodParam[index].BeginAt.AddHours((double) begin_offset_hour) <= serverTime && serverTime <= this.mRaidPeriodParam[index].EndAt.AddHours((double) end_offset_hour))
          return this.mRaidPeriodParam[index];
      }
      return (RaidPeriodParam) null;
    }

    public RaidPeriodTimeParam GetActiveRaidPeriodTimeDay()
    {
      RaidPeriodTimeParam raidPeriodTimeDay = (RaidPeriodTimeParam) null;
      DateTime serverTime = TimeManager.ServerTime;
      if (this.mRaidPeriodTimeParam != null && this.mRaidPeriodTimeParam.Count != 0)
      {
        for (int index = 0; index < this.mRaidPeriodTimeParam.Count; ++index)
        {
          if ((!UnityEngine.Object.op_Inequality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null) || RaidManager.Instance.RaidPeriodId == this.mRaidPeriodTimeParam[index].PeriodId) && this.mRaidPeriodTimeParam[index].BeginAt <= serverTime && serverTime <= this.mRaidPeriodTimeParam[index].EndAt)
            raidPeriodTimeDay = this.mRaidPeriodTimeParam[index];
        }
      }
      return raidPeriodTimeDay;
    }

    public RaidPeriodTimeScheduleParam GetRaidScheduleTime(out bool nowCheck)
    {
      nowCheck = false;
      RaidPeriodTimeScheduleParam raidScheduleTime = (RaidPeriodTimeScheduleParam) null;
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod() != null)
      {
        RaidPeriodTimeScheduleParam activeRaidPeriodTime = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriodTime();
        if (activeRaidPeriodTime != null)
        {
          nowCheck = true;
          return activeRaidPeriodTime;
        }
        DateTime serverTime = TimeManager.ServerTime;
        RaidPeriodTimeParam raidPeriodTimeDay = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriodTimeDay();
        if (raidPeriodTimeDay == null)
          return (RaidPeriodTimeScheduleParam) null;
        foreach (RaidPeriodTimeScheduleParam timeScheduleParam in raidPeriodTimeDay.Schedule)
        {
          DateTime dateTime = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam.Begin + ":00");
          if (serverTime < dateTime && raidPeriodTimeDay.EndAt > dateTime)
          {
            raidScheduleTime = timeScheduleParam;
            break;
          }
        }
        if (raidScheduleTime == null && DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " 0:00:00").AddDays(1.0) < raidPeriodTimeDay.EndAt)
        {
          using (List<RaidPeriodTimeScheduleParam>.Enumerator enumerator = raidPeriodTimeDay.Schedule.GetEnumerator())
          {
            if (enumerator.MoveNext())
              raidScheduleTime = enumerator.Current;
          }
        }
        if (raidScheduleTime != null)
          return raidScheduleTime;
      }
      return (RaidPeriodTimeScheduleParam) null;
    }

    public RaidManager.RaidScheduleType GetRaidScheduleStatus()
    {
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod() == null)
        return RaidManager.RaidScheduleType.Close;
      if (this.GetActiveRaidPeriodTimeDay() == null)
        return RaidManager.RaidScheduleType.Open;
      return MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriodTime() != null ? RaidManager.RaidScheduleType.OpenSchedule : RaidManager.RaidScheduleType.CloseSchedule;
    }

    public RaidPeriodTimeScheduleParam GetActiveRaidPeriodTime()
    {
      RaidPeriodTimeScheduleParam activeRaidPeriodTime = (RaidPeriodTimeScheduleParam) null;
      DateTime serverTime = TimeManager.ServerTime;
      if (this.mRaidPeriodTimeParam != null && this.mRaidPeriodTimeParam.Count != 0)
      {
        for (int index = 0; index < this.mRaidPeriodTimeParam.Count; ++index)
        {
          if ((!UnityEngine.Object.op_Inequality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null) || RaidManager.Instance.RaidPeriodId == this.mRaidPeriodTimeParam[index].PeriodId) && this.mRaidPeriodTimeParam[index].BeginAt <= serverTime && serverTime <= this.mRaidPeriodTimeParam[index].EndAt)
          {
            activeRaidPeriodTime = (RaidPeriodTimeScheduleParam) null;
            foreach (RaidPeriodTimeScheduleParam timeScheduleParam in this.mRaidPeriodTimeParam[index].Schedule)
            {
              DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam.Begin + ":00");
              TimeSpan timeSpan = RaidManager.GetTimeSpan(timeScheduleParam.Open);
              DateTime dateTime2 = dateTime1 + timeSpan;
              if (dateTime1 <= serverTime && serverTime <= dateTime2)
                activeRaidPeriodTime = timeScheduleParam;
            }
          }
        }
      }
      return activeRaidPeriodTime;
    }

    public RaidAreaParam GetRaidArea(int area_id)
    {
      return this.mRaidAreaParam.Find((Predicate<RaidAreaParam>) (ra => ra.Id == area_id));
    }

    public RaidAreaParam GetRaidAreaByOrder(int period_id, int order)
    {
      return this.mRaidAreaParam.Find((Predicate<RaidAreaParam>) (ra => ra.PeriodId == period_id && ra.Order == order));
    }

    public int GetRaidAreaCount(int period_id)
    {
      return this.mRaidAreaParam.FindAll((Predicate<RaidAreaParam>) (ra => ra.PeriodId == period_id)).Count;
    }

    public RaidBossParam GetRaidBoss(int boss_id)
    {
      return this.mRaidBossParam.Find((Predicate<RaidBossParam>) (rbg => rbg.Id == boss_id));
    }

    public List<RaidBossParam> GetRaidBossAll(int period_id)
    {
      return this.mRaidBossParam.FindAll((Predicate<RaidBossParam>) (rbp => rbp.PeriodId == period_id));
    }

    public string GetRaidBeatReward(int beat_reward_id, int round)
    {
      RaidBeatRewardParam raidBeatRewardParam = this.mRaidBeatRewardParam.Find((Predicate<RaidBeatRewardParam>) (rbr => rbr.Id == beat_reward_id));
      if (raidBeatRewardParam == null || raidBeatRewardParam.Rewards.Count == 0)
        return string.Empty;
      RaidBeatRewardDataParam reward_data = raidBeatRewardParam.Rewards.Find((Predicate<RaidBeatRewardDataParam>) (rbrd => rbrd.Round == round));
      if (reward_data == null)
      {
        reward_data = raidBeatRewardParam.Rewards[0];
        raidBeatRewardParam.Rewards.ForEach((Action<RaidBeatRewardDataParam>) (rbrd => reward_data = reward_data.Round >= rbrd.Round ? reward_data : rbrd));
      }
      return reward_data.RewardId;
    }

    public List<RaidReward> GetRaidRewardList(string reward_id)
    {
      RaidRewardParam raidRewardParam = this.mRaidRewardParam.Find((Predicate<RaidRewardParam>) (rr => rr.Id == reward_id));
      return raidRewardParam == null ? new List<RaidReward>() : raidRewardParam.Rewards;
    }

    public RaidPeriodParam GetRaidPeriod(int period_id)
    {
      return this.mRaidPeriodParam.Find((Predicate<RaidPeriodParam>) (param => param.Id == period_id));
    }

    public List<RaidPeriodTimeParam> GetRaidPeriodTime() => this.mRaidPeriodTimeParam;

    public RaidCompleteRewardParam GetRaidCompleteReward(int comp_reward_id)
    {
      return this.mRaidCompleteRewardParam.Find((Predicate<RaidCompleteRewardParam>) (param => param.Id == comp_reward_id));
    }

    public RaidPeriodParam GetActiveRaidRewardPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mRaidPeriodParam.Count; ++index)
      {
        if (this.mRaidPeriodParam[index].RewardBeginAt <= serverTime && serverTime <= this.mRaidPeriodParam[index].RewardEndAt)
          return this.mRaidPeriodParam[index];
      }
      return (RaidPeriodParam) null;
    }

    public List<ArenaRewardParam> GetArenaRewardParams()
    {
      if (this.mArenaRewardParams == null)
        this.mArenaRewardParams = new List<ArenaRewardParam>();
      return ArenaRewardParam.GetSortedRewardParams(this.mArenaRewardParams);
    }

    public InspSkillParam GetInspirationSkillParam(string iname)
    {
      return this.mInspSkillDictionary == null || string.IsNullOrEmpty(iname) || !this.mInspSkillDictionary.ContainsKey(iname) ? (InspSkillParam) null : this.mInspSkillDictionary[iname];
    }

    public InspSkillCostParam GetInspSkillResetCost(int gen)
    {
      return this.mInspSkillResetCostDictionary == null || !this.mInspSkillResetCostDictionary.ContainsKey(gen) ? (InspSkillCostParam) null : this.mInspSkillResetCostDictionary[gen];
    }

    public InspSkillCostParam GetInspSkillOpenCost(int count)
    {
      return this.mInspSkillOpenCostDictionary == null || !this.mInspSkillOpenCostDictionary.ContainsKey(count) ? (InspSkillCostParam) null : this.mInspSkillOpenCostDictionary[count];
    }

    public int GetInspSkillMaxOpenCount()
    {
      return this.mInspSkillOpenCostDictionary == null ? 0 : this.mInspSkillOpenCostDictionary.Count;
    }

    public int GetInspLvUpCostTotal(int id, int current_lv, int up)
    {
      if (this.mInspSkillLvUpCostParam == null || this.mInspSkillLvUpCostParam.Count <= 0)
        return -1;
      InspSkillLvUpCostParam skillLvUpCostParam = this.mInspSkillLvUpCostParam.Find((Predicate<InspSkillLvUpCostParam>) (item => item.id == id));
      if (skillLvUpCostParam == null || skillLvUpCostParam.costs == null || skillLvUpCostParam.costs.Count <= 0)
        return -1;
      int inspLvUpCostTotal = 0;
      int num1 = current_lv - 1;
      int num2 = num1 + up;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        int index2 = index1;
        if (index2 >= skillLvUpCostParam.costs.Count)
          index2 = skillLvUpCostParam.costs.Count - 1;
        inspLvUpCostTotal += skillLvUpCostParam.costs[index2].gold;
      }
      return inspLvUpCostTotal;
    }

    public UnitSameGroupParam GetUnitSameGroup(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname) || this.mUnitSameGroup == null)
        return (UnitSameGroupParam) null;
      for (int index = 0; index < this.mUnitSameGroup.Count; ++index)
      {
        if (this.mUnitSameGroup[index].IsInGroup(unit_iname))
          return this.mUnitSameGroup[index];
      }
      return (UnitSameGroupParam) null;
    }

    public List<BuyCoinProductParam> GetBuyCoinProductList() => this.mBuyCoinProductParam;

    public BuyCoinProductParam GetBuyCoinProductParam(string key)
    {
      if (key == null)
        return (BuyCoinProductParam) null;
      foreach (BuyCoinProductParam coinProductParam in this.mBuyCoinProductParam)
      {
        if (coinProductParam.Iname == key)
          return coinProductParam;
      }
      return (BuyCoinProductParam) null;
    }

    public List<BuyCoinShopParam> GetBuyCoinShopParam() => this.mBuyCoinShopParam;

    public void DeleteOutsidePeriodBuyCoinParam()
    {
      long unixSec = TimeManager.GetUnixSec(TimeManager.ServerTime);
      int index = 0;
      while (index < this.mBuyCoinShopParam.Count)
      {
        if (this.mBuyCoinShopParam[index].EndAt <= unixSec)
        {
          this.mBuyCoinShopParam.Remove(this.mBuyCoinShopParam[index]);
          index = 0;
        }
        else
          ++index;
      }
    }

    public bool IsBuyCoinShopOpen(BuyCoinShopParam param)
    {
      long serverTime = Network.GetServerTime();
      return param.BeginAt <= serverTime && serverTime <= param.EndAt || param.AlwaysOpen;
    }

    public BuyCoinShopParam GetBuyCoinShopParam(BuyCoinManager.BuyCoinShopType type)
    {
      foreach (BuyCoinShopParam buyCoinShopParam in this.mBuyCoinShopParam)
      {
        if (buyCoinShopParam.ShopType == type && this.IsBuyCoinShopOpen(buyCoinShopParam))
          return buyCoinShopParam;
      }
      return (BuyCoinShopParam) null;
    }

    public BuyCoinRewardParam GetBuyCoinRewardParam(string key)
    {
      foreach (BuyCoinRewardParam buyCoinRewardParam in this.mBuyCoinRewardParam)
      {
        if (buyCoinRewardParam.Id == key)
          return buyCoinRewardParam;
      }
      return (BuyCoinRewardParam) null;
    }

    public List<BuyCoinProductConvertParam> GetBuyCoinProductConvertParam()
    {
      return this.mBuyCoinProductConvertParam;
    }

    public List<UnitParam> GetAllPlayableUnitParams()
    {
      return this.mUnitParam.Where<UnitParam>((Func<UnitParam, bool>) (unit => !string.IsNullOrEmpty(unit.piece) || unit.IsHero())).ToList<UnitParam>();
    }

    public CoinBuyUseBonusParam[] GetEnableCoinBuyUseBonusParams()
    {
      if (this.mCoinBuyUseBonusParam == null)
        return (CoinBuyUseBonusParam[]) null;
      List<CoinBuyUseBonusParam> buyUseBonusParamList = new List<CoinBuyUseBonusParam>();
      for (int index = 0; index < this.mCoinBuyUseBonusParam.Length; ++index)
      {
        if (this.mCoinBuyUseBonusParam[index] != null && this.mCoinBuyUseBonusParam[index].IsEnable)
          buyUseBonusParamList.Add(this.mCoinBuyUseBonusParam[index]);
      }
      return buyUseBonusParamList.Count <= 0 ? (CoinBuyUseBonusParam[]) null : ((IEnumerable<CoinBuyUseBonusParam>) this.mCoinBuyUseBonusParam).ToArray<CoinBuyUseBonusParam>();
    }

    public CoinBuyUseBonusParam GetActiveCoinBuyUseBonusParam(
      eCoinBuyUseBonusTrigger trigger,
      eCoinBuyUseBonusType type)
    {
      if (this.mCoinBuyUseBonusParam == null)
        return (CoinBuyUseBonusParam) null;
      CoinBuyUseBonusParam[] all = Array.FindAll<CoinBuyUseBonusParam>(this.mCoinBuyUseBonusParam, (Predicate<CoinBuyUseBonusParam>) (param => param.Trigger == trigger && param.Type == type && param.IsEnable));
      if (all == null || all.Length <= 0)
        return (CoinBuyUseBonusParam) null;
      List<CoinBuyUseBonusParam> buyUseBonusParamList = new List<CoinBuyUseBonusParam>((IEnumerable<CoinBuyUseBonusParam>) all);
      buyUseBonusParamList.Sort((Comparison<CoinBuyUseBonusParam>) ((a, b) => (int) (TimeManager.FromDateTime(a.EndAt) - TimeManager.FromDateTime(b.EndAt))));
      return buyUseBonusParamList[0];
    }

    public CoinBuyUseBonusParam FindCoinBuyUseBonusParam(string iname)
    {
      CoinBuyUseBonusParam buyUseBonusParam = (CoinBuyUseBonusParam) null;
      if (this.mCoinBuyUseBonusParam != null)
      {
        buyUseBonusParam = Array.Find<CoinBuyUseBonusParam>(this.mCoinBuyUseBonusParam, (Predicate<CoinBuyUseBonusParam>) (p => p.Iname == iname));
        if (buyUseBonusParam == null || !buyUseBonusParam.IsEnable)
          return (CoinBuyUseBonusParam) null;
      }
      return buyUseBonusParam;
    }

    public SortRuneConditionParam FindSortRuneConditionParam(SortUtility.SortPrefsData prefsData)
    {
      foreach (SortRuneParam sortRuneParam1 in this.SortRuneParams)
      {
        SortRuneParam sortRuneParam2;
        if ((sortRuneParam2 = sortRuneParam1) != null && !(sortRuneParam2.iname != prefsData.MajorKey))
        {
          for (int index = 0; index < sortRuneParam2.conditions.Length; ++index)
          {
            SortRuneConditionParam condition;
            if ((condition = sortRuneParam2.conditions[index]) != null && condition.cnds_iname == prefsData.MinorKey)
              return condition;
          }
        }
      }
      return (SortRuneConditionParam) null;
    }

    public RuneParam GetRuneParam(string iname)
    {
      return !this.mRuneParamDict.ContainsKey(iname) ? (RuneParam) null : this.mRuneParamDict[iname];
    }

    public RuneParam GetRuneParamByItemId(string item_iname)
    {
      foreach (RuneParam runeParamByItemId in this.mRuneParamDict.Values)
      {
        if (runeParamByItemId.item_iname == item_iname)
          return runeParamByItemId;
      }
      return (RuneParam) null;
    }

    public RuneLotteryBaseState GetRuneLotteryBaseState(string iname)
    {
      return this.mRuneLotteryBaseState.Find((Predicate<RuneLotteryBaseState>) (p => p.iname == iname));
    }

    public RuneLotteryEvoState GetRuneLotteryEvoState(string iname)
    {
      return this.mRuneLotteryEvoState.Find((Predicate<RuneLotteryEvoState>) (p => p.iname == iname));
    }

    public RuneMaterial GetRuneMaterial(int rarity)
    {
      return this.mRuneMaterial.Find((Predicate<RuneMaterial>) (p => (int) p.rarity == rarity));
    }

    public RuneCost GetRuneCost(string iname)
    {
      return this.mRuneCost.Find((Predicate<RuneCost>) (p => p.iname == iname));
    }

    public RuneSetEff GetRuneSetEff(int seteff_type)
    {
      return this.mRuneSetEff.Find((Predicate<RuneSetEff>) (p => p.seteff_type == seteff_type));
    }

    public Dictionary<string, RuneParam> CreateRuneParamTable_KeyItemIname()
    {
      Dictionary<string, RuneParam> tableKeyItemIname = new Dictionary<string, RuneParam>();
      foreach (RuneParam runeParam in this.mRuneParamDict.Values)
      {
        if (!tableKeyItemIname.ContainsKey(runeParam.item_iname))
          tableKeyItemIname.Add(runeParam.item_iname, runeParam);
      }
      return tableKeyItemIname;
    }

    public AutoRepeatQuestBoxParam GetAutoRepeatQuestBoxParam(int box_add_count)
    {
      return this.mAutoRepeatQuestBoxParams != null && this.mAutoRepeatQuestBoxParams.Length > box_add_count ? this.mAutoRepeatQuestBoxParams[box_add_count] : (AutoRepeatQuestBoxParam) null;
    }

    public AutoRepeatQuestBoxParam GetLastAutoRepeatQuestBoxParam()
    {
      return this.mAutoRepeatQuestBoxParams != null && this.mAutoRepeatQuestBoxParams.Length > 0 ? this.GetAutoRepeatQuestBoxParam(this.mAutoRepeatQuestBoxParams.Length - 1) : (AutoRepeatQuestBoxParam) null;
    }

    public ResetCostParam FindResetCost(string iname)
    {
      return !this.mResetCostParams.ContainsKey(iname) ? (ResetCostParam) null : this.mResetCostParams[iname];
    }

    public ProtectSkillParam GetProtectSkillParam(string iname)
    {
      return string.IsNullOrEmpty(iname) || this.mProtectSkillParams == null ? (ProtectSkillParam) null : this.mProtectSkillParams.Find((Predicate<ProtectSkillParam>) (p => p.Iname == iname));
    }

    public ExpansionPurchaseParam[] GetExpansionPurchaseParams(
      ExpansionPurchaseParam.eExpansionType type)
    {
      List<ExpansionPurchaseParam> expansionPurchaseParamList = new List<ExpansionPurchaseParam>();
      if (this.mExpansionPurchaseParams != null)
      {
        for (int index = 0; index < this.mExpansionPurchaseParams.Count; ++index)
        {
          if (this.mExpansionPurchaseParams[index] != null && this.mExpansionPurchaseParams[index].ExpansionType == type)
            expansionPurchaseParamList.Add(this.mExpansionPurchaseParams[index]);
        }
      }
      return expansionPurchaseParamList.ToArray();
    }

    public ExpansionPurchaseParam GetExpansionPurchaseParam(string iname)
    {
      return string.IsNullOrEmpty(iname) || this.mExpansionPurchaseParams == null ? (ExpansionPurchaseParam) null : this.mExpansionPurchaseParams.Find((Predicate<ExpansionPurchaseParam>) (p => p.Iname == iname));
    }

    public ExpansionPurchaseParam GetExpansionPurchaseParamForProductId(string product_iname)
    {
      return string.IsNullOrEmpty(product_iname) || this.mExpansionPurchaseParams == null ? (ExpansionPurchaseParam) null : this.mExpansionPurchaseParams.Find((Predicate<ExpansionPurchaseParam>) (p => p.BuyCoinProduct == product_iname));
    }

    public GuildSearchFilterParam FindGuildSearchFilter(eGuildSearchFilterTypes type)
    {
      if (this.mGuildSeartchFilterParams == null)
        return (GuildSearchFilterParam) null;
      for (int index = 0; index < this.mGuildSeartchFilterParams.Length; ++index)
      {
        if (this.mGuildSeartchFilterParams[index].filter_type == type)
          return this.mGuildSeartchFilterParams[index];
      }
      return (GuildSearchFilterParam) null;
    }

    public SkillAdditionalParam GetSkillAdditionalParam(string iname)
    {
      return string.IsNullOrEmpty(iname) || this.mSkillAdditionalList == null ? (SkillAdditionalParam) null : this.mSkillAdditionalList.Find((Predicate<SkillAdditionalParam>) (p => p.Iname == iname));
    }

    public SkillAntiShieldParam GetSkillAntiShieldParam(string iname)
    {
      return string.IsNullOrEmpty(iname) || this.mSkillAntiShieldList == null ? (SkillAntiShieldParam) null : this.mSkillAntiShieldList.Find((Predicate<SkillAntiShieldParam>) (p => p.Iname == iname));
    }
  }
}
